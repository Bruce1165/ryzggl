<template>
  <div class="department-create-container">
    <!-- App Header -->
    <AppHeader />

    <!-- Page Header -->
    <div class="page-header">
      <h2>新增部门</h2>
      <div class="breadcrumb">当前位置: 部门管理 &gt; 新增部门</div>
    </div>

    <!-- Department Form -->
    <el-card class="form-card">
      <el-form
        ref="formRef"
        :model="form"
        :rules="rules"
        label-width="140px"
        class="department-form"
      >
        <el-form-item label="部门名称" prop="departmentName">
          <el-input v-model="form.departmentName" placeholder="输入部门名称" />
        </el-form-item>

        <el-form-item label="上级部门" prop="parentId">
          <el-select
            v-model="form.parentId"
            placeholder="选择上级部门"
            filterable
            clearable
            style="width: 100%"
          >
            <el-option
              v-for="dept in departments"
              :key="dept.departmentId"
              :label="dept.departmentName"
              :value="dept.departmentId"
            />
          </el-select>
        </el-form-item>

        <el-form-item label="部门描述" prop="description">
          <el-input
            v-model="form.description"
            type="textarea"
            :rows="3"
            placeholder="输入部门描述"
          />
        </el-form-item>

        <el-form-item label="排序" prop="sortOrder">
          <el-input-number
            v-model="form.sortOrder"
            :min="0"
            placeholder="输入排序"
            style="width: 100%"
          />
        </el-form-item>

        <el-form-item label="部门类型" prop="departmentType">
          <el-select
            v-model="form.departmentType"
            placeholder="选择部门类型"
            style="width: 100%"
          >
            <el-option label="企业部门" value="企业部门" />
            <el-option label="管理组织" value="管理组织" />
          <el-option label="政府部门" value="政府部门" />
          <el-option label="其他" value="其他" />
          </el-select>
        </el-form-item>

        <!-- Form Actions -->
        <el-form-item>
          <el-button type="primary" :loading="submitting" @click="handleSubmit">
            {{ submitting ? '提交中...' : '提交' }}
          </el-button>
          <el-button @click="handleCancel">取消</el-button>
        </el-form-item>
      </el-form>
    </el-card>
  </div>
</template>

<script>
import { ref, reactive, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { ElMessage } from 'element-plus'
import { getDepartmentList, createDepartment } from '@/api/department'
import AppHeader from '@/components/AppHeader.vue'

export default {
  name: 'DepartmentCreate',

  components: {
    AppHeader
  },

  setup() {
    const router = useRouter()
    const formRef = ref(null)
    const submitting = ref(false)

    // Form data
    const form = reactive({
      departmentName: '',
      parentId: '',
      description: '',
      sortOrder: 0,
      departmentType: '企业部门'
    })

    const departments = ref([])

    // Form validation rules
    const rules = {
      departmentName: [{ required: true, message: '请输入部门名称', trigger: 'blur' }],
      description: [{ required: true, message: '请输入部门描述', trigger: 'blur' }]
    }

    /**
     * Load departments
     */
    const loadDepartments = async () => {
      const res = await getDepartmentList({})
      if (res.code === 0) {
        departments.value = res.data || []
      } else {
        ElMessage.error(res.message || '加载部门列表失败')
      }
    }

    /**
     * Handle submit
     */
    const handleSubmit = async () => {
      if (!formRef.value) return

      try {
        const valid = await formRef.value.validate()
        if (!valid) return

        submitting.value = true

        const res = await createDepartment(form)

        if (res.code === 0) {
          ElMessage.success('部门创建成功')
          router.push('/department/list')
        } else {
          ElMessage.error(res.message || '部门创建失败')
        }
      } catch (error) {
        ElMessage.error('部门创建失败: ' + error.message)
      } finally {
        submitting.value = false
      }
    }

    /**
     * Handle cancel
     */
    const handleCancel = () => {
      router.back()
    }

    onMounted(() => {
      loadDepartments()
    })

    return {
      formRef,
      submitting,
      form,
      departments,
      rules,
      loadDepartments,
      handleSubmit,
      handleCancel
    }
  }
}
</script>

<style scoped>
.department-create-container {
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

.card-header {
  font-weight: bold;
  color: #333;
}

.department-form {
  max-width: 800px;
}
</style>
