<template>
  <div class="department-detail-container">
    <!-- App Header -->
    <AppHeader />

    <!-- Page Header -->
    <div class="page-header">
      <h2>部门详情</h2>
      <div class="breadcrumb">当前位置: 部门管理 &gt; 部门详情</div>
    </div>

    <!-- Department Detail Card -->
    <el-card class="detail-card">
      <template #header>
        <div class="card-header">
          <span>部门信息</span>
          <div class="header-actions">
            <el-button type="primary" @click="handleEdit">编辑</el-button>
            <el-button type="danger" @click="handleDelete" :loading="deleting">删除</el-button>
          </div>
        </div>
      </template>

      <el-descriptions :column="2" border>
        <el-descriptions-item label="部门ID">
          {{ department.departmentId }}
        </el-descriptions-item>
        <el-descriptions-item label="部门名称">
          {{ department.departmentName }}
        </el-descriptions-item>
        <el-descriptions-item label="上级部门ID">
          {{ department.parentId || '-' }}
        </el-descriptions-item>
        <el-descriptions-item label="上级部门名称">
          {{ department.parentName || '-' }}
        </el-descriptions-item>
        <el-descriptions-item label="部门描述" :span="2">
          {{ department.description || '-' }}
        </el-descriptions-item>
        <el-descriptions-item label="排序">
          {{ department.sortOrder }}
        </el-descriptions-item>
        <el-descriptions-item label="部门类型">
          <el-tag :type="getTypeTagType(department.departmentType)">
            {{ department.departmentType }}
          </el-tag>
        </el-descriptions-item>
        <el-descriptions-item label="创建时间">
          {{ formatDate(department.createTime) }}
        </el-descriptions-item>
        <el-descriptions-item label="更新时间">
          {{ formatDate(department.updateTime) }}
        </el-descriptions-item>
      </el-descriptions>
    </el-card>

    <!-- Child Departments Card -->
    <el-card class="detail-card">
      <template #header>
        <div class="card-header">
          <span>下级部门</span>
        </div>
      </template>

      <el-table
        :data="childDepartments"
        v-loading="loadingChildren"
        stripe
        style="width: 100%"
      >
        <el-table-column prop="departmentId" label="部门ID" width="120" />
        <el-table-column prop="departmentName" label="部门名称" />
        <el-table-column prop="description" label="部门描述" show-overflow-tooltip />
        <el-table-column prop="sortOrder" label="排序" width="80" />
        <el-table-column prop="departmentType" label="部门类型" width="120">
          <template #default="{ row }">
            <el-tag :type="getTypeTagType(row.departmentType)">
              {{ row.departmentType }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column label="操作" width="200">
          <template #default="{ row }">
            <el-button
              type="primary"
              size="small"
              link
              @click="handleViewChild(row)"
            >
              查看
            </el-button>
          </template>
        </el-table-column>
      </el-table>
    </el-card>
  </div>
</template>

<script>
import { ref, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import { getDepartmentList, deleteDepartment } from '@/api/department'
import AppHeader from '@/components/AppHeader.vue'

export default {
  name: 'DepartmentDetail',

  components: {
    AppHeader
  },

  setup() {
    const router = useRouter()
    const route = useRoute()
    const loadingChildren = ref(false)
    const deleting = ref(false)

    const department = ref({})
    const childDepartments = ref([])

    const formatDate = (dateStr) => {
      if (!dateStr) return '-'
      return new Date(dateStr).toLocaleString('zh-CN')
    }

    const getTypeTagType = (type) => {
      const typeMap = {
        '企业部门': 'primary',
        '管理组织': 'success',
        '政府部门': 'warning',
        '其他': 'info'
      }
      return typeMap[type] || 'info'
    }

    const loadDepartment = async () => {
      const id = route.query.id
      if (!id) {
        ElMessage.error('缺少部门ID参数')
        return
      }

      try {
        const res = await getDepartmentList({ current: 1, size: 1 })
        if (res.code === 0 && res.data && res.data.records) {
          const dept = res.data.records.find(d => d.departmentId === id)
          if (dept) {
            department.value = dept
          } else {
            ElMessage.error('部门不存在')
          }
        }
      } catch (error) {
        ElMessage.error('加载部门信息失败: ' + error.message)
      }
    }

    const loadChildren = async () => {
      const id = route.query.id
      if (!id) return

      loadingChildren.value = true
      try {
        const res = await getDepartmentList({ current: 1, size: 1000 })
        if (res.code === 0 && res.data && res.data.records) {
          childDepartments.value = res.data.records.filter(d => d.parentId === id)
        }
      } catch (error) {
        ElMessage.error('加载下级部门失败: ' + error.message)
      } finally {
        loadingChildren.value = false
      }
    }

    const handleEdit = () => {
      router.push({
        path: '/department/edit',
        query: { id: department.value.departmentId }
      })
    }

    const handleDelete = async () => {
      try {
        await ElMessageBox.confirm(
          '删除部门将同时删除所有下级部门，确定要删除吗？',
          '确认删除',
          {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'warning'
          }
        )

        deleting.value = true
        const res = await deleteDepartment(department.value.departmentId)

        if (res.code === 0) {
          ElMessage.success('部门删除成功')
          router.push('/department/list')
        } else {
          ElMessage.error(res.message || '部门删除失败')
        }
      } catch (error) {
        if (error !== 'cancel') {
          ElMessage.error('部门删除失败: ' + error.message)
        }
      } finally {
        deleting.value = false
      }
    }

    const handleViewChild = (row) => {
      router.push({
        path: '/department/detail',
        query: { id: row.departmentId }
      })
    }

    onMounted(() => {
      loadDepartment()
      loadChildren()
    })

    return {
      loadingChildren,
      deleting,
      department,
      childDepartments,
      formatDate,
      getTypeTagType,
      handleEdit,
      handleDelete,
      handleViewChild
    }
  }
}
</script>

<style scoped>
.department-detail-container {
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

.detail-card {
  margin-bottom: 20px;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  font-weight: bold;
  color: #333;
}

.header-actions {
  display: flex;
  gap: 10px;
}
</style>
