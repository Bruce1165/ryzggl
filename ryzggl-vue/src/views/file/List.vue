<template>
  <div class="file-list-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>文件管理</h2>
      <div class="breadcrumb">当前位置: 系统管理 &gt; 文件列表</div>
    </div>

    <!-- Upload Area -->
    <el-card class="upload-card">
      <el-upload
        ref="uploadRef"
        class="upload-area"
        drag
        :action="uploadUrl"
        :headers="uploadHeaders"
        :on-success="handleUploadSuccess"
        :on-error="handleUploadError"
        :on-progress="handleUploadProgress"
        :before-upload="beforeUpload"
        :file-list="fileList"
        :auto-upload="false"
        multiple
      >
        <el-icon class="el-icon--upload"><upload-filled /></el-icon>
        <div class="el-upload__text">
          拖拽文件到此处或 <em>点击上传</em>
        </div>
        <template #tip>
          <div class="el-upload__tip">
            支持jpg、png、pdf、doc、docx、xls、xlsx格式，单个文件不超过10MB
          </div>
        </template>
      </el-upload>
      <div class="upload-actions">
        <el-button type="primary" @click="startUpload">开始上传</el-button>
        <el-button @click="clearFileList">清空列表</el-button>
      </div>
    </el-card>

    <!-- Search and Filter -->
    <el-form :inline="true" :model="queryParams" class="search-form">
      <el-form-item label="文件名称">
        <el-input v-model="queryParams.fileName" placeholder="输入文件名称" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="文件类型">
        <el-select v-model="queryParams.fileType" placeholder="选择文件类型" clearable style="width: 150px">
          <el-option label="全部" value="" />
          <el-option label="图片" value="image" />
          <el-option label="文档" value="document" />
          <el-option label="表格" value="spreadsheet" />
          <el-option label="其他" value="other" />
        </el-select>
      </el-form-item>
      <el-form-item label="上传日期">
        <el-date-picker
          v-model="queryParams.uploadDate"
          type="daterange"
          range-separator="至"
          start-placeholder="开始日期"
          end-placeholder="结束日期"
          clearable
          style="width: 260px"
        />
      </el-form-item>
      <el-form-item>
        <el-button type="primary" icon="Search" @click="handleSearch">查询</el-button>
        <el-button type="danger" icon="Delete" @click="handleBatchDelete" :disabled="selectedRows.length === 0">批量删除</el-button>
      </el-form-item>
    </el-form>

    <!-- Data Table -->
    <el-card>
      <el-table
        v-loading="loading"
        :data="tableData"
        stripe
        border
        @selection-change="handleSelectionChange"
      >
        <el-table-column type="selection" width="55" />
        <el-table-column prop="fileId" label="文件ID" width="80" />
        <el-table-column prop="fileName" label="文件名称" width="250" show-overflow-tooltip>
          <template #default="{row}">
            <el-icon v-if="isImage(row.fileType)" style="vertical-align: middle; margin-right: 5px;">
              <picture />
            </el-icon>
            <el-icon v-else-if="isDocument(row.fileType)" style="vertical-align: middle; margin-right: 5px;">
              <document />
            </el-icon>
            <el-icon v-else-if="isSpreadsheet(row.fileType)" style="vertical-align: middle; margin-right: 5px;">
              <tickets />
            </el-icon>
            <el-icon v-else style="vertical-align: middle; margin-right: 5px;">
              <document />
            </el-icon>
            {{ row.fileName }}
          </template>
        </el-table-column>
        <el-table-column prop="fileType" label="文件类型" width="120">
          <template #default="{row}">
            <el-tag :type="getTypeColor(row.fileType)">{{ getTypeName(row.fileType) }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="fileSize" label="文件大小" width="100" align="right">
          <template #default="{row}">
            {{ formatFileSize(row.fileSize) }}
          </template>
        </el-table-column>
        <el-table-column prop="uploader" label="上传人" width="100" />
        <el-table-column prop="uploadTime" label="上传时间" width="160" />
        <el-table-column prop="downloadCount" label="下载次数" width="100" align="center">
          <template #default="{row}">
            <el-tag type="info">{{ row.downloadCount || 0 }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="status" label="状态" width="80">
          <template #default="{row}">
            <el-tag :type="row.status === '1' ? 'success' : 'danger'" size="small">
              {{ row.status === '1' ? '正常' : '停用' }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column label="操作" width="220" fixed="right">
          <template #default="{row}">
            <el-button type="primary" size="small" link @click="handlePreview(row)">
              预览
            </el-button>
            <el-button type="success" size="small" link @click="handleDownload(row)">
              下载
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

    <!-- Preview Dialog -->
    <el-dialog v-model="previewDialogVisible" :title="`预览: ${previewFile?.fileName}`" width="800px">
      <div v-if="previewFile" class="preview-content">
        <el-image
          v-if="isImage(previewFile.fileType)"
          :src="previewFile.url"
          fit="contain"
          style="width: 100%; max-height: 600px;"
        />
        <div v-else class="file-info">
          <el-descriptions :column="1" border>
            <el-descriptions-item label="文件名称">{{ previewFile.fileName }}</el-descriptions-item>
            <el-descriptions-item label="文件类型">{{ getTypeName(previewFile.fileType) }}</el-descriptions-item>
            <el-descriptions-item label="文件大小">{{ formatFileSize(previewFile.fileSize) }}</el-descriptions-item>
            <el-descriptions-item label="上传人">{{ previewFile.uploader }}</el-descriptions-item>
            <el-descriptions-item label="上传时间">{{ previewFile.uploadTime }}</el-descriptions-item>
            <el-descriptions-item label="下载次数">{{ previewFile.downloadCount }}</el-descriptions-item>
          </el-descriptions>
          <div class="download-link">
            <el-button type="primary" @click="handleDownload(previewFile)">立即下载</el-button>
          </div>
        </div>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { ref, reactive, computed, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import { UploadFilled, Picture, Document, Tickets } from '@element-plus/icons-vue'
import { uploadFile, getFileList, deleteFile, downloadFile } from '@/api/file'

export default {
  name: 'FileList',
  components: { UploadFilled, Picture, Document, Tickets },

  setup() {
    const loading = ref(false)
    const tableData = ref([])
    const previewDialogVisible = ref(false)
    const previewFile = ref(null)
    const selectedRows = ref([])
    const uploadRef = ref(null)
    const fileList = ref([])

    const queryParams = reactive({
      fileName: '',
      fileType: '',
      uploadDate: null
    })

    const page = reactive({
      current: 1,
      size: 10,
      total: 0
    })

    const uploadUrl = computed(() => import.meta.env.VITE_API_BASE_URL + '/api/file/upload')
    const uploadHeaders = computed(() => ({
      Authorization: 'Bearer ' + localStorage.getItem('token')
    }))

    onMounted(() => {
      loadTableData()
    })

    /**
     * Load file data
     */
    const loadTableData = async () => {
      loading.value = true
      try {
        // TODO: Implement with actual API call
        tableData.value = [
          { fileId: 1, fileName: '企业营业执照.pdf', fileType: 'document', fileSize: 1048576, uploader: '张三', uploadTime: '2020-01-01 10:00:00', downloadCount: 45, status: '1', url: '' },
          { fileId: 2, fileName: '人员身份证.jpg', fileType: 'image', fileSize: 524288, uploader: '李四', uploadTime: '2020-01-02 14:30:00', downloadCount: 23, status: '1', url: '' },
          { fileId: 3, fileName: '证书信息表.xlsx', fileType: 'spreadsheet', fileSize: 2097152, uploader: '王五', uploadTime: '2020-01-03 09:15:00', downloadCount: 67, status: '1', url: '' },
          { fileId: 4, fileName: '申请材料.zip', fileType: 'other', fileSize: 5242880, uploader: '赵六', uploadTime: '2020-01-04 16:45:00', downloadCount: 12, status: '0', url: '' },
          { fileId: 5, fileName: '培训材料.docx', fileType: 'document', fileSize: 3145728, uploader: '钱七', uploadTime: '2020-01-05 11:20:00', downloadCount: 89, status: '1', url: '' }
        ]
        page.total = 5
        loading.value = false
      } catch (error) {
        loading.value = false
        ElMessage.error('加载数据失败: ' + error.message)
      }
    }

    /**
     * Before upload validation
     */
    const beforeUpload = (file) => {
      const allowedTypes = ['image/jpeg', 'image/png', 'image/jpg', 'application/pdf',
                          'application/msword', 'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
                          'application/vnd.ms-excel', 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet']
      const isValidType = allowedTypes.includes(file.type)
      const isValidSize = file.size / 1024 / 1024 < 10

      if (!isValidType) {
        ElMessage.error('文件格式不支持')
        return false
      }
      if (!isValidSize) {
        ElMessage.error('文件大小不能超过10MB')
        return false
      }
      return true
    }

    /**
     * Start upload
     */
    const startUpload = () => {
      if (uploadRef.value) {
        uploadRef.value.submit()
      }
    }

    /**
     * Clear file list
     */
    const clearFileList = () => {
      fileList.value = []
    }

    /**
     * Upload success
     */
    const handleUploadSuccess = (response, file) => {
      ElMessage.success('上传成功: ' + file.name)
      loadTableData()
    }

    /**
     * Upload error
     */
    const handleUploadError = (error, file) => {
      ElMessage.error('上传失败: ' + file.name)
    }

    /**
     * Upload progress
     */
    const handleUploadProgress = (event, file) => {
      // Can show progress in UI if needed
    }

    /**
     * File type helpers
     */
    const isImage = (type) => type === 'image'
    const isDocument = (type) => type === 'document'
    const isSpreadsheet = (type) => type === 'spreadsheet'

    const getTypeName = (type) => {
      const typeMap = {
        'image': '图片',
        'document': '文档',
        'spreadsheet': '表格',
        'other': '其他'
      }
      return typeMap[type] || '其他'
    }

    const getTypeColor = (type) => {
      const colorMap = {
        'image': 'success',
        'document': 'primary',
        'spreadsheet': 'warning',
        'other': 'info'
      }
      return colorMap[type] || 'info'
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
     * Handle search
     */
    const handleSearch = () => {
      page.current = 1
      loadTableData()
    }

    /**
     * Handle selection change
     */
    const handleSelectionChange = (selection) => {
      selectedRows.value = selection
    }

    /**
     * Handle preview
     */
    const handlePreview = (row) => {
      previewFile.value = row
      previewDialogVisible.value = true
    }

    /**
     * Handle download
     */
    const handleDownload = async (row) => {
      try {
        // TODO: Implement with actual API call
        ElMessage.info('下载功能待实现: ' + row.fileName)
        row.downloadCount++
      } catch (error) {
        ElMessage.error('下载失败: ' + error.message)
      }
    }

    /**
     * Handle delete
     */
    const handleDelete = (row) => {
      ElMessageBox.confirm(`确定要删除文件 ${row.fileName} 吗？`, '删除确认', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        ElMessage.info('删除功能待实现')
        loadTableData()
      }).catch(() => {
        ElMessage.info('已取消删除')
      })
    }

    /**
     * Handle batch delete
     */
    const handleBatchDelete = () => {
      ElMessageBox.confirm(`确定要删除选中的 ${selectedRows.value.length} 个文件吗？`, '批量删除确认', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        ElMessage.info('批量删除功能待实现')
        selectedRows.value = []
        loadTableData()
      }).catch(() => {
        ElMessage.info('已取消删除')
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
      previewDialogVisible,
      previewFile,
      selectedRows,
      uploadRef,
      fileList,
      uploadUrl,
      uploadHeaders,
      loadTableData,
      beforeUpload,
      startUpload,
      clearFileList,
      handleUploadSuccess,
      handleUploadError,
      handleUploadProgress,
      isImage,
      isDocument,
      isSpreadsheet,
      getTypeName,
      getTypeColor,
      formatFileSize,
      handleSearch,
      handleSelectionChange,
      handlePreview,
      handleDownload,
      handleDelete,
      handleBatchDelete,
      handleSizeChange,
      handleCurrentChange
    }
  }
}
</script>

<style scoped>
.file-list-container {
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

.upload-card {
  margin-bottom: 20px;
}

.upload-area {
  width: 100%;
}

.upload-actions {
  margin-top: 20px;
  text-align: center;
}

.el-icon--upload {
  font-size: 67px;
  color: #409eff;
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

.preview-content {
  text-align: center;
}

.file-info {
  padding: 20px;
}

.download-link {
  margin-top: 20px;
}
</style>
