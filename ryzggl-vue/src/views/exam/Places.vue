<template>
  <div class="exam-places-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>考点管理</h2>
      <div class="breadcrumb">当前位置: 考试管理 &gt; 考点列表</div>
    </div>

    <!-- Search and Filter -->
    <el-form :inline="true" :model="queryParams" class="search-form">
      <el-form-item label="考点名称">
        <el-input v-model="queryParams.placeName" placeholder="输入考点名称" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="考点代码">
        <el-input v-model="queryParams.placeCode" placeholder="输入考点代码" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="所在城市">
        <el-input v-model="queryParams.city" placeholder="输入所在城市" clearable style="width: 150px" />
      </el-form-item>
      <el-form-item label="状态">
        <el-select v-model="queryParams.status" placeholder="选择状态" clearable style="width: 120px">
          <el-option label="全部" value="" />
          <el-option label="正常" value="1" />
          <el-option label="停用" value="0" />
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" icon="Search" @click="handleSearch">查询</el-button>
        <el-button type="success" icon="Plus" @click="handleCreate">新增考点</el-button>
      </el-form-item>
    </el-form>

    <!-- Data Table -->
    <el-card>
      <el-table
        v-loading="loading"
        :data="tableData"
        stripe
        border
      >
        <el-table-column prop="placeId" label="考点ID" width="80" />
        <el-table-column prop="placeCode" label="考点代码" width="120" />
        <el-table-column prop="placeName" label="考点名称" width="200" show-overflow-tooltip />
        <el-table-column prop="address" label="地址" width="250" show-overflow-tooltip />
        <el-table-column prop="city" label="所在城市" width="120" />
        <el-table-column prop="capacity" label="容纳人数" width="100" align="center">
          <template #default="{row}">
            <el-tag type="primary">{{ row.capacity }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="contactPerson" label="联系人" width="100" />
        <el-table-column prop="contactPhone" label="联系电话" width="130" />
        <el-table-column prop="status" label="状态" width="80">
          <template #default="{row}">
            <el-tag :type="row.status === '1' ? 'success' : 'danger'" size="small">
              {{ row.status === '1' ? '正常' : '停用' }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="createTime" label="创建时间" width="160" />
        <el-table-column label="操作" width="220" fixed="right">
          <template #default="{row}">
            <el-button type="primary" size="small" link @click="handleView(row)">
              查看
            </el-button>
            <el-button type="primary" size="small" link @click="handleEdit(row)">
              编辑
            </el-button>
            <el-button type="success" size="small" link @click="handleViewExams(row)">
              考试安排
            </el-button>
            <el-button type="danger" size="small" link @click="handleDelete(row)">
              删除
            </el-button>
          </template>
        </el-table-column>
      </el-table>

      <!-- Pagination -->
      <div class="pagination-container">
        <el-pagination
          v-model:current-page="page.current"
          v-model:page-size="page.size"
          :total="page.total"
          :page-sizes="[10, 20, 50]"
          layout="total, sizes, prev, pager, next"
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
        />
      </div>
    </el-card>

    <!-- Place Detail Dialog -->
    <el-dialog
      v-model="detailDialogVisible"
      :title="dialogMode === 'create' ? '新增考点' : '编辑考点'"
      width="700px"
    >
      <el-form
        ref="formRef"
        :model="form"
        :rules="rules"
        label-width="120px"
      >
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="考点代码" prop="placeCode">
              <el-input v-model="form.placeCode" placeholder="输入考点代码" :disabled="dialogMode !== 'create'" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="考点名称" prop="placeName">
              <el-input v-model="form.placeName" placeholder="输入考点名称" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-form-item label="所在城市" prop="city">
          <el-input v-model="form.city" placeholder="输入所在城市" />
        </el-form-item>

        <el-form-item label="详细地址" prop="address">
          <el-input v-model="form.address" placeholder="输入详细地址" />
        </el-form-item>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="容纳人数" prop="capacity">
              <el-input-number v-model="form.capacity" :min="1" :max="1000" style="width: 100%" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="考场数量" prop="roomCount">
              <el-input-number v-model="form.roomCount" :min="1" :max="50" style="width: 100%" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="联系人" prop="contactPerson">
              <el-input v-model="form.contactPerson" placeholder="输入联系人" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="联系电话" prop="contactPhone">
              <el-input v-model="form.contactPhone" placeholder="输入联系电话" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-form-item label="考场设施">
          <el-checkbox-group v-model="form.facilities">
            <el-checkbox label="wifi">WiFi</el-checkbox>
            <el-checkbox label="projector">投影仪</el-checkbox>
            <el-checkbox label="computer">电脑</el-checkbox>
            <el-checkbox label="camera">监控</el-checkbox>
            <el-checkbox label="aircon">空调</el-checkbox>
          </el-checkbox-group>
        </el-form-item>

        <el-form-item label="状态" prop="status">
          <el-radio-group v-model="form.status">
            <el-radio label="1">正常</el-radio>
            <el-radio label="0">停用</el-radio>
          </el-radio-group>
        </el-form-item>

        <el-form-item label="备注">
          <el-input
            v-model="form.remark"
            type="textarea"
            :rows="3"
            placeholder="输入备注信息"
          />
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="detailDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="handleSave">确定</el-button>
      </template>
    </el-dialog>

    <!-- Exam Arrangement Dialog -->
    <el-dialog v-model="examsDialogVisible" title="考试安排" width="800px">
      <el-descriptions :column="2" border>
        <el-descriptions-item label="考点名称" :span="2">
          {{ selectedPlace?.placeName }}
        </el-descriptions-item>
        <el-descriptions-item label="考点代码">
          {{ selectedPlace?.placeCode }}
        </el-descriptions-item>
        <el-descriptions-item label="所在城市">
          {{ selectedPlace?.city }}
        </el-descriptions-item>
        <el-descriptions-item label="地址" :span="2">
          {{ selectedPlace?.address }}
        </el-descriptions-item>
      </el-descriptions>

      <el-divider>已安排考试</el-divider>

      <el-table :data="examList" stripe border>
        <el-table-column prop="examName" label="考试名称" width="200" />
        <el-table-column prop="examDate" label="考试日期" width="120" />
        <el-table-column prop="examTime" label="考试时间" width="120" />
        <el-table-column prop="participantCount" label="参考人数" width="100" align="center" />
        <el-table-column prop="room" label="考场号" width="100" />
      </el-table>
      <template #footer>
        <el-button @click="examsDialogVisible = false">关闭</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script>
import { ref, reactive, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import { getExamPlaceList } from '@/api/exam'

export default {
  name: 'ExamPlaces',

  setup() {
    const loading = ref(false)
    const tableData = ref([])
    const detailDialogVisible = ref(false)
    const examsDialogVisible = ref(false)
    const dialogMode = ref('create')
    const formRef = ref(null)
    const selectedPlace = ref(null)
    const examList = ref([])

    const queryParams = reactive({
      placeName: '',
      placeCode: '',
      city: '',
      status: ''
    })

    const page = reactive({
      current: 1,
      size: 10,
      total: 0
    })

    const form = reactive({
      placeId: '',
      placeCode: '',
      placeName: '',
      city: '',
      address: '',
      capacity: 100,
      roomCount: 5,
      contactPerson: '',
      contactPhone: '',
      facilities: [],
      status: '1',
      remark: ''
    })

    const rules = {
      placeCode: [{ required: true, message: '请输入考点代码', trigger: 'blur' }],
      placeName: [{ required: true, message: '请输入考点名称', trigger: 'blur' }],
      city: [{ required: true, message: '请输入所在城市', trigger: 'blur' }],
      address: [{ required: true, message: '请输入详细地址', trigger: 'blur' }],
      capacity: [{ required: true, message: '请输入容纳人数', trigger: 'blur' }],
      roomCount: [{ required: true, message: '请输入考场数量', trigger: 'blur' }],
      contactPerson: [{ required: true, message: '请输入联系人', trigger: 'blur' }],
      contactPhone: [
        { required: true, message: '请输入联系电话', trigger: 'blur' },
        { pattern: /^1[3-9]\d{9}$/, message: '请输入正确的手机号', trigger: 'blur' }
      ],
      status: [{ required: true, message: '请选择状态', trigger: 'change' }]
    }

    onMounted(() => {
      loadTableData()
    })

    /**
     * Load exam place data
     */
    const loadTableData = async () => {
      loading.value = true
      try {
        // TODO: Implement with actual API call to get exam places
        const res = await getExamPlaceList({
          current: page.current,
          size: page.size,
          ...queryParams
        })

        if (res.code === 0) {
          tableData.value = res.data.records || []
          page.total = res.data.total || 0
        } else {
          ElMessage.error(res.message || '加载数据失败')
        }
      } catch (error) {
        // Fallback to mock data for development
        tableData.value = [
          { placeId: 1, placeCode: 'PLACE001', placeName: '北京市第一中学', city: '北京', address: '北京市东城区和平里街道1号', capacity: 200, roomCount: 8, contactPerson: '王老师', contactPhone: '13800138000', facilities: ['wifi', 'projector', 'computer', 'camera', 'aircon'], status: '1', createTime: '2020-01-01 00:00:00' },
          { placeId: 2, placeCode: 'PLACE002', placeName: '上海市第二职业中学', city: '上海', address: '上海市浦东新区张江路100号', capacity: 150, roomCount: 6, contactPerson: '李老师', contactPhone: '13800138001', facilities: ['wifi', 'projector', 'camera'], status: '1', createTime: '2020-02-01 00:00:00' },
          { placeId: 3, placeCode: 'PLACE003', placeName: '广州市第三职业技术学校', city: '广州', address: '广州市天河区中山大道123号', capacity: 180, roomCount: 7, contactPerson: '张老师', contactPhone: '13800138002', facilities: ['wifi', 'projector', 'computer', 'aircon'], status: '0', createTime: '2020-03-01 00:00:00' }
        ]
        page.total = 3
        ElMessage.warning('使用模拟数据，请实现真实的API接口')
      } finally {
        loading.value = false
      }
    }

    /**
     * Handle search
     */
    const handleSearch = () => {
      page.current = 1
      loadTableData()
    }

    /**
     * Handle create place
     */
    const handleCreate = () => {
      dialogMode.value = 'create'
      resetForm()
      detailDialogVisible.value = true
    }

    /**
     * Handle view place
     */
    const handleView = (row) => {
      dialogMode.value = 'view'
      Object.assign(form, row)
      detailDialogVisible.value = true
    }

    /**
     * Handle edit place
     */
    const handleEdit = (row) => {
      dialogMode.value = 'edit'
      Object.assign(form, row)
      detailDialogVisible.value = true
    }

    /**
     * Handle view exams
     */
    const handleViewExams = (row) => {
      selectedPlace.value = row
      // TODO: Implement with actual API call to get exam arrangements for this place
      examList.value = [
        { examName: '2024年二级造价工程师考试', examDate: '2024-03-10', examTime: '09:00-11:30', participantCount: 45, room: '第一考场' },
        { examName: '2024年一级造价工程师考试', examDate: '2024-03-15', examTime: '14:00-17:00', participantCount: 38, room: '第二考场' }
      ]
      examsDialogVisible.value = true
    }

    /**
     * Handle delete place
     */
    const handleDelete = (row) => {
      ElMessageBox.confirm(
        `确定要删除考点 ${row.placeName} 吗？此操作不可恢复。`,
        '删除确认',
        {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning',
          dangerouslyUseHTMLString: true
        }
      ).then(async () => {
        try {
          // TODO: Implement with actual API call to delete exam place
          ElMessage.info('删除功能待实现，需要实现删除考点的API接口')
          loadTableData()
        } catch (error) {
          ElMessage.error('删除失败: ' + error.message)
        }
      }).catch(() => {
        ElMessage.info('已取消删除')
      })
    }

    /**
     * Handle save place
     */
    const handleSave = async () => {
      if (!formRef.value) return

      try {
        const valid = await formRef.value.validate()
        if (!valid) return

        // TODO: Implement with actual API call to create/update exam place
        // This would typically call endpoints like createExamPlace or updateExamPlace
        ElMessage.info('保存功能待实现，需要实现考点管理的API接口')

        ElMessage.success(dialogMode.value === 'create' ? '创建成功' : '更新成功')
        detailDialogVisible.value = false
        loadTableData()
      } catch (error) {
        ElMessage.error('保存失败: ' + error.message)
      }
    }

    /**
     * Reset form
     */
    const resetForm = () => {
      Object.assign(form, {
        placeId: '',
        placeCode: '',
        placeName: '',
        city: '',
        address: '',
        capacity: 100,
        roomCount: 5,
        contactPerson: '',
        contactPhone: '',
        facilities: [],
        status: '1',
        remark: ''
      })
    }

    /**
     * Handle pagination size change
     */
    const handleSizeChange = (val) => {
      page.size = val
      page.current = 1
      loadTableData()
    }

    /**
     * Handle current page change
     */
    const handleCurrentChange = (val) => {
      page.current = val
      loadTableData()
    }

    return {
      loading,
      tableData,
      queryParams,
      page,
      detailDialogVisible,
      examsDialogVisible,
      dialogMode,
      form,
      rules,
      selectedPlace,
      examList,
      formRef,
      loadTableData,
      handleSearch,
      handleCreate,
      handleView,
      handleEdit,
      handleViewExams,
      handleDelete,
      handleSave,
      handleSizeChange,
      handleCurrentChange
    }
  }
}
</script>

<style scoped>
.exam-places-container {
  padding: 20px;
}

.page-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

.page-header h2 {
  margin: 0;
  font-size: 18px;
  font-weight: bold;
}

.breadcrumb {
  color: #666;
  font-size: 14px;
}

.search-form {
  background: #f5f7fa;
  padding: 20px;
  margin-bottom: 20px;
  border-radius: 4px;
}

.pagination-container {
  margin-top: 20px;
  display: flex;
  justify-content: center;
}
</style>
