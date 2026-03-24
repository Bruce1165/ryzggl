<template>
  <div class="import-export-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>数据导入导出</h2>
      <div class="breadcrumb">当前位置: 系统管理 &gt; 数据导入导出</div>
    </div>

    <!-- Tabs -->
    <el-tabs v-model="activeTab" @tab-change="handleTabChange">
      <!-- Import Tab -->
      <el-tab-pane label="数据导入" name="import">
        <el-card>
          <el-form :model="importForm" label-width="120px">
            <el-form-item label="导入类型">
              <el-select v-model="importForm.type" placeholder="选择导入类型" style="width: 100%">
                <el-option label="企业信息" value="enterprise" />
                <el-option label="人员信息" value="worker" />
                <el-option label="证书信息" value="certificate" />
                <el-option label="考试信息" value="exam" />
                <el-option label="资格类型" value="qualification" />
              </el-select>
            </el-form-item>

            <el-form-item label="文件上传">
              <el-upload
                ref="importUploadRef"
                class="upload-demo"
                drag
                :action="importUrl"
                :headers="uploadHeaders"
                :on-success="handleImportSuccess"
                :on-error="handleImportError"
                :before-upload="beforeImportUpload"
                :auto-upload="false"
                :limit="1"
                accept=".xlsx,.xls,.csv"
              >
                <el-icon class="el-icon--upload"><upload-filled /></el-icon>
                <div class="el-upload__text">
                  拖拽Excel文件到此处或 <em>点击上传</em>
                </div>
                <template #tip>
                  <div class="el-upload__tip">
                    仅支持.xlsx、.xls格式，文件大小不超过5MB
                  </div>
                </template>
              </el-upload>
            </el-form-item>

            <el-form-item label="导入模式">
              <el-radio-group v-model="importForm.mode">
                <el-radio label="insert">新增模式（仅新增）</el-radio>
                <el-radio label="update">更新模式（覆盖更新）</el-radio>
                <el-radio label="upsert">混合模式（新增+更新）</el-radio>
              </el-radio-group>
            </el-form-item>

            <el-form-item label="数据验证">
              <el-checkbox v-model="importForm.validate">启用数据验证</el-checkbox>
            </el-form-item>

            <el-form-item>
              <el-button type="primary" :loading="importLoading" @click="startImport">开始导入</el-button>
              <el-button @click="resetImportForm">重置</el-button>
              <el-button type="info" @click="downloadTemplate">下载模板</el-button>
            </el-form-item>
          </el-form>
        </el-card>

        <!-- Import History -->
        <el-card style="margin-top: 20px;">
          <template #header>
            <div class="card-header">
              <span>导入历史</span>
            </div>
          </template>
          <el-table :data="importHistory" stripe border>
            <el-table-column prop="id" label="序号" width="80" />
            <el-table-column prop="type" label="导入类型" width="120">
              <template #default="{row}">
                <el-tag>{{ getTypeName(row.type) }}</el-tag>
              </template>
            </el-table-column>
            <el-table-column prop="fileName" label="文件名" width="200" show-overflow-tooltip />
            <el-table-column prop="totalCount" label="总记录数" width="100" align="center" />
            <el-table-column prop="successCount" label="成功数" width="100" align="center">
              <template #default="{row}">
                <span style="color: #67c23a;">{{ row.successCount }}</span>
              </template>
            </el-table-column>
            <el-table-column prop="failCount" label="失败数" width="100" align="center">
              <template #default="{row}">
                <span style="color: #f56c6c;">{{ row.failCount }}</span>
              </template>
            </el-table-column>
            <el-table-column prop="status" label="状态" width="100">
              <template #default="{row}">
                <el-tag :type="row.status === 'completed' ? 'success' : 'processing'">
                  {{ row.status === 'completed' ? '完成' : '进行中' }}
                </el-tag>
              </template>
            </el-table-column>
            <el-table-column prop="importTime" label="导入时间" width="160" />
            <el-table-column prop="importer" label="操作人" width="100" />
            <el-table-column label="操作" width="120">
              <template #default="{row}">
                <el-button type="primary" size="small" link @click="viewImportDetail(row)">
                  查看详情
                </el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-card>
      </el-tab-pane>

      <!-- Export Tab -->
      <el-tab-pane label="数据导出" name="export">
        <el-card>
          <el-form :model="exportForm" label-width="120px">
            <el-form-item label="导出类型">
              <el-select v-model="exportForm.type" placeholder="选择导出类型" style="width: 100%">
                <el-option label="企业信息" value="enterprise" />
                <el-option label="人员信息" value="worker" />
                <el-option label="证书信息" value="certificate" />
                <el-option label="考试信息" value="exam" />
                <el-option label="申请记录" value="apply" />
                <el-option label="资格统计" value="qualification" />
              </el-select>
            </el-form-item>

            <el-form-item label="导出格式">
              <el-radio-group v-model="exportForm.format">
                <el-radio label="xlsx">Excel (.xlsx)</el-radio>
                <el-radio label="xls">Excel (.xls)</el-radio>
                <el-radio label="csv">CSV (.csv)</el-radio>
              </el-radio-group>
            </el-form-item>

            <el-form-item label="数据范围">
              <el-radio-group v-model="exportForm.range">
                <el-radio label="all">全部数据</el-radio>
                <el-radio label="filtered">当前查询结果</el-radio>
                <el-radio label="selected">选中数据</el-radio>
              </el-radio-group>
            </el-form-item>

            <el-form-item label="导出字段" v-if="exportForm.type">
              <el-checkbox-group v-model="exportForm.fields">
                <el-checkbox
                  v-for="field in getExportFields(exportForm.type)"
                  :key="field.value"
                  :label="field.value"
                >
                  {{ field.label }}
                </el-checkbox>
              </el-checkbox-group>
            </el-form-item>

            <el-form-item label="日期范围">
              <el-date-picker
                v-model="exportForm.dateRange"
                type="daterange"
                range-separator="至"
                start-placeholder="开始日期"
                end-placeholder="结束日期"
                clearable
                style="width: 100%"
              />
            </el-form-item>

            <el-form-item>
              <el-button type="primary" :loading="exportLoading" @click="startExport">
                开始导出
              </el-button>
              <el-button @click="resetExportForm">重置</el-button>
            </el-form-item>
          </el-form>
        </el-card>

        <!-- Export History -->
        <el-card style="margin-top: 20px;">
          <template #header>
            <div class="card-header">
              <span>导出历史</span>
            </div>
          </template>
          <el-table :data="exportHistory" stripe border>
            <el-table-column prop="id" label="序号" width="80" />
            <el-table-column prop="type" label="导出类型" width="120">
              <template #default="{row}">
                <el-tag>{{ getTypeName(row.type) }}</el-tag>
              </template>
            </el-table-column>
            <el-table-column prop="fileName" label="文件名" width="250" show-overflow-tooltip />
            <el-table-column prop="recordCount" label="记录数" width="100" align="center" />
            <el-table-column prop="fileSize" label="文件大小" width="100" align="right">
              <template #default="{row}">
                {{ formatFileSize(row.fileSize) }}
              </template>
            </el-table-column>
            <el-table-column prop="exportTime" label="导出时间" width="160" />
            <el-table-column prop="exporter" label="操作人" width="100" />
            <el-table-column label="操作" width="150">
              <template #default="{row}">
                <el-button type="success" size="small" link @click="downloadExport(row)">
                  下载
                </el-button>
                <el-button type="danger" size="small" link @click="deleteExport(row)">
                  删除
                </el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-card>
      </el-tab-pane>
    </el-tabs>

    <!-- Import Detail Dialog -->
    <el-dialog v-model="importDetailVisible" title="导入详情" width="800px">
      <el-descriptions v-if="selectedImport" :column="2" border>
        <el-descriptions-item label="导入类型">
          {{ getTypeName(selectedImport.type) }}
        </el-descriptions-item>
        <el-descriptions-item label="导入模式">
          {{ getModeName(selectedImport.mode) }}
        </el-descriptions-item>
        <el-descriptions-item label="文件名">{{ selectedImport.fileName }}</el-descriptions-item>
        <el-descriptions-item label="操作人">{{ selectedImport.importer }}</el-descriptions-item>
        <el-descriptions-item label="总记录数">{{ selectedImport.totalCount }}</el-descriptions-item>
        <el-descriptions-item label="成功数" label-class-name="success-label">
          {{ selectedImport.successCount }}
        </el-descriptions-item>
        <el-descriptions-item label="失败数" label-class-name="error-label">
          {{ selectedImport.failCount }}
        </el-descriptions-item>
        <el-descriptions-item label="导入时间">{{ selectedImport.importTime }}</el-descriptions-item>
        <el-descriptions-item label="状态" :span="2">
          <el-tag :type="selectedImport.status === 'completed' ? 'success' : 'processing'">
            {{ selectedImport.status === 'completed' ? '完成' : '进行中' }}
          </el-tag>
        </el-descriptions-item>
      </el-descriptions>

      <div v-if="selectedImport && selectedImport.errors && selectedImport.errors.length > 0" style="margin-top: 20px;">
        <h4>错误明细</h4>
        <el-table :data="selectedImport.errors" max-height="300">
          <el-table-column prop="row" label="行号" width="80" />
          <el-table-column prop="field" label="字段" width="120" />
          <el-table-column prop="message" label="错误信息" show-overflow-tooltip />
        </el-table>
      </div>

      <template #footer>
        <el-button @click="importDetailVisible = false">关闭</el-button>
        <el-button v-if="selectedImport?.errors?.length > 0" type="primary" @click="downloadErrorLog">
          下载错误日志
        </el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script>
import { ref, reactive, computed, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import { UploadFilled } from '@element-plus/icons-vue'

export default {
  name: 'ImportExport',
  components: { UploadFilled },

  setup() {
    const activeTab = ref('import')
    const importLoading = ref(false)
    const exportLoading = ref(false)
    const importDetailVisible = ref(false)
    const selectedImport = ref(null)
    const importUploadRef = ref(null)

    const importForm = reactive({
      type: '',
      mode: 'insert',
      validate: true
    })

    const exportForm = reactive({
      type: '',
      format: 'xlsx',
      range: 'all',
      fields: [],
      dateRange: null
    })

    const importUrl = computed(() => import.meta.env.VITE_API_BASE_URL + '/api/import/upload')
    const uploadHeaders = computed(() => ({
      Authorization: 'Bearer ' + localStorage.getItem('token')
    }))

    const importHistory = ref([
      { id: 1, type: 'worker', fileName: '人员信息_20250114.xlsx', mode: 'upsert', totalCount: 156, successCount: 148, failCount: 8, status: 'completed', importer: '张三', importTime: '2025-01-14 10:30:00', errors: [] },
      { id: 2, type: 'certificate', fileName: '证书信息_20250113.xlsx', mode: 'insert', totalCount: 89, successCount: 89, failCount: 0, status: 'completed', importer: '李四', importTime: '2025-01-13 15:20:00', errors: [] },
      { id: 3, type: 'enterprise', fileName: '企业信息_20250112.xlsx', mode: 'update', totalCount: 45, successCount: 42, failCount: 3, status: 'completed', importer: '王五', importTime: '2025-01-12 09:45:00', errors: [
        { row: 5, field: '组织机构代码', message: '格式不正确' },
        { row: 12, field: '联系电话', message: '手机号格式错误' },
        { row: 23, field: '企业名称', message: '不能为空' }
      ]}
    ])

    const exportHistory = ref([
      { id: 1, type: 'certificate', fileName: '证书信息_20250114.xlsx', recordCount: 328, fileSize: 2097152, exportTime: '2025-01-14 14:20:00', exporter: '张三' },
      { id: 2, type: 'worker', fileName: '人员信息_20250113.csv', recordCount: 456, fileSize: 1048576, exportTime: '2025-01-13 11:30:00', exporter: '李四' },
      { id: 3, type: 'exam', fileName: '考试信息_20250112.xlsx', recordCount: 67, fileSize: 524288, exportTime: '2025-01-12 16:45:00', exporter: '王五' }
    ])

    onMounted(() => {
      loadImportHistory()
      loadExportHistory()
    })

    /**
     * Load import history
     */
    const loadImportHistory = async () => {
      try {
        // TODO: Implement with actual API call
      } catch (error) {
        ElMessage.error('加载导入历史失败: ' + error.message)
      }
    }

    /**
     * Load export history
     */
    const loadExportHistory = async () => {
      try {
        // TODO: Implement with actual API call
      } catch (error) {
        ElMessage.error('加载导出历史失败: ' + error.message)
      }
    }

    /**
     * Before import upload
     */
    const beforeImportUpload = (file) => {
      if (!importForm.type) {
        ElMessage.warning('请先选择导入类型')
        return false
      }
      const isValidType = ['application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
                          'application/vnd.ms-excel',
                          'text/csv'].includes(file.type)
      const isValidSize = file.size / 1024 / 1024 < 5

      if (!isValidType) {
        ElMessage.error('文件格式不支持')
        return false
      }
      if (!isValidSize) {
        ElMessage.error('文件大小不能超过5MB')
        return false
      }
      return true
    }

    /**
     * Start import
     */
    const startImport = () => {
      if (!importForm.type) {
        ElMessage.warning('请选择导入类型')
        return
      }
      if (importUploadRef.value) {
        importUploadRef.value.submit()
      }
    }

    /**
     * Import success
     */
    const handleImportSuccess = (response) => {
      importLoading.value = false
      ElMessage.success('导入成功')
      loadImportHistory()
      resetImportForm()
    }

    /**
     * Import error
     */
    const handleImportError = (error) => {
      importLoading.value = false
      ElMessage.error('导入失败')
    }

    /**
     * Reset import form
     */
    const resetImportForm = () => {
      Object.assign(importForm, {
        type: '',
        mode: 'insert',
        validate: true
      })
      if (importUploadRef.value) {
        importUploadRef.value.clearFiles()
      }
    }

    /**
     * Download template
     */
    const downloadTemplate = () => {
      if (!importForm.type) {
        ElMessage.warning('请先选择导入类型')
        return
      }
      ElMessage.info('下载模板功能待实现')
    }

    /**
     * Start export
     */
    const startExport = async () => {
      if (!exportForm.type) {
        ElMessage.warning('请选择导出类型')
        return
      }
      if (exportForm.fields.length === 0) {
        ElMessage.warning('请至少选择一个导出字段')
        return
      }

      exportLoading.value = true
      try {
        // TODO: Implement with actual API call
        ElMessage.success('导出成功')
        loadExportHistory()
      } catch (error) {
        ElMessage.error('导出失败: ' + error.message)
      } finally {
        exportLoading.value = false
      }
    }

    /**
     * Reset export form
     */
    const resetExportForm = () => {
      Object.assign(exportForm, {
        type: '',
        format: 'xlsx',
        range: 'all',
        fields: [],
        dateRange: null
      })
    }

    /**
     * Get export fields
     */
    const getExportFields = (type) => {
      const fieldsMap = {
        enterprise: [
          { label: '企业代码', value: 'unitCode' },
          { label: '企业名称', value: 'unitName' },
          { label: '组织机构代码', value: 'orgCode' },
          { label: '法人代表', value: 'legalPerson' },
          { label: '联系电话', value: 'phone' }
        ],
        worker: [
          { label: '人员代码', value: 'workerCode' },
          { label: '姓名', value: 'name' },
          { label: '身份证号', value: 'idCard' },
          { label: '手机号', value: 'phone' },
          { label: '所属企业', value: 'unitName' }
        ],
        certificate: [
          { label: '证书编号', value: 'certNo' },
          { label: '持证人', value: 'holderName' },
          { label: '资格名称', value: 'qualificationName' },
          { label: '有效期起始', value: 'validStart' },
          { label: '有效期结束', value: 'validEnd' }
        ],
        exam: [
          { label: '考试名称', value: 'examName' },
          { label: '考试时间', value: 'examDate' },
          { label: '考点', value: 'placeName' },
          { label: '报名人数', value: 'signUpCount' },
          { label: '实考人数', value: 'actualCount' }
        ],
        apply: [
          { label: '申请编号', value: 'applyNo' },
          { label: '申请人', value: 'applicantName' },
          { label: '申请类型', value: 'applyType' },
          { label: '申请日期', value: 'applyDate' },
          { label: '审批状态', value: 'status' }
        ],
        qualification: [
          { label: '资格代码', value: 'qualificationCode' },
          { label: '资格名称', value: 'qualificationName' },
          { label: '资格等级', value: 'level' },
          { label: '专业类别', value: 'profession' },
          { label: '有效期', value: 'validPeriod' }
        ]
      }
      return fieldsMap[type] || []
    }

    /**
     * Format file size
     */
    const formatFileSize = (bytes) => {
      if (bytes === 0) return '0 B'
      const k = 1024
      const sizes = ['B', 'KB', 'MB', 'GB']
      const i = Math.floor(Math.log(bytes) / Math.log(k))
      return Math.round(bytes / Math.pow(k, i) * 100) / 100 + ' ' + sizes[i]
    }

    /**
     * Get type name
     */
    const getTypeName = (type) => {
      const typeMap = {
        'enterprise': '企业信息',
        'worker': '人员信息',
        'certificate': '证书信息',
        'exam': '考试信息',
        'apply': '申请记录',
        'qualification': '资格类型'
      }
      return typeMap[type] || type
    }

    /**
     * Get mode name
     */
    const getModeName = (mode) => {
      const modeMap = {
        'insert': '新增模式',
        'update': '更新模式',
        'upsert': '混合模式'
      }
      return modeMap[mode] || mode
    }

    /**
     * Handle tab change
     */
    const handleTabChange = (tab) => {
      // Refresh data when switching tabs
    }

    /**
     * View import detail
     */
    const viewImportDetail = (row) => {
      selectedImport.value = row
      importDetailVisible.value = true
    }

    /**
     * Download export
     */
    const downloadExport = (row) => {
      ElMessage.info('下载功能待实现: ' + row.fileName)
    }

    /**
     * Delete export
     */
    const deleteExport = (row) => {
      ElMessageBox.confirm(`确定要删除文件 ${row.fileName} 吗？`, '删除确认', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        ElMessage.info('删除功能待实现')
        loadExportHistory()
      }).catch(() => {
        ElMessage.info('已取消删除')
      })
    }

    /**
     * Download error log
     */
    const downloadErrorLog = () => {
      ElMessage.info('下载错误日志功能待实现')
    }

    return {
      activeTab,
      importLoading,
      exportLoading,
      importDetailVisible,
      selectedImport,
      importForm,
      exportForm,
      importHistory,
      exportHistory,
      importUploadRef,
      importUrl,
      uploadHeaders,
      loadImportHistory,
      loadExportHistory,
      beforeImportUpload,
      startImport,
      handleImportSuccess,
      handleImportError,
      resetImportForm,
      downloadTemplate,
      startExport,
      resetExportForm,
      getExportFields,
      formatFileSize,
      getTypeName,
      getModeName,
      handleTabChange,
      viewImportDetail,
      downloadExport,
      deleteExport,
      downloadErrorLog
    }
  }
}
</script>

<style scoped>
.import-export-container {
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
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.el-icon--upload {
  font-size: 67px;
  color: #409eff;
}

.upload-demo {
  width: 100%;
}

:deep(.el-checkbox-group) {
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
}

:deep(.success-label .el-descriptions__label) {
  color: #67c23a;
}

:deep(.error-label .el-descriptions__label) {
  color: #f56c6c;
}
</style>
