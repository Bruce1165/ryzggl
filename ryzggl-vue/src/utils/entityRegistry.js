/**
 * Entity Configuration Registry
 *
 * Central registry for entity configurations providing schema structure,
 * validation, and metadata for all entity types in the RYZGGL system.
 *
 * Follows the Repository pattern with centralized entity configuration
 * for type safety, validation, and consistent API interactions.
 */

/**
 * Field types supported in entity forms
 */
export const FIELD_TYPES = {
  TEXT: 'text',
  NUMBER: 'number',
  DATE: 'date',
  DATETIME: 'datetime',
  SELECT: 'select',
  TEXTAREA: 'textarea',
  BOOLEAN: 'boolean',
  PHONE: 'phone',
  EMAIL: 'email',
  ID_CARD: 'idcard',
  CERTIFICATE_CODE: 'certificatecode'
}

/**
 * Validation rules for field types
 */
export const VALIDATION_RULES = {
  required: (value) => value !== null && value !== undefined && value !== '',
  email: (value) => /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value),
  phone: (value) => /^1[3-9]\d{9}$/.test(value),
  idCard: (value) => /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/.test(value),
  certificateCode: (value) => /^[A-Z0-9]{4,20}$/.test(value),
  positiveNumber: (value) => Number(value) > 0,
  date: (value) => {
    const date = new Date(value)
    return date instanceof Date && !isNaN(date.getTime())
  }
}

/**
 * Entity configuration structure validation
 */
function validateEntityConfig(config) {
  const errors = []

  if (!config.name || typeof config.name !== 'string') {
    errors.push('Entity must have a valid name')
  }

  if (!config.apiPath || typeof config.apiPath !== 'string') {
    errors.push('Entity must have a valid apiPath')
  }

  if (!Array.isArray(config.tableColumns) || config.tableColumns.length === 0) {
    errors.push('Entity must have at least one table column')
  } else {
    config.tableColumns.forEach((col, index) => {
      if (!col.prop || !col.label) {
        errors.push(`Table column ${index} must have prop and label`)
      }
    })
  }

  if (!Array.isArray(config.formFields) || config.formFields.length === 0) {
    errors.push('Entity must have at least one form field')
  } else {
    config.formFields.forEach((field, index) => {
      if (!field.prop || !field.label || !field.type) {
        errors.push(`Form field ${index} must have prop, label, and type`)
      }
      if (!Object.values(FIELD_TYPES).includes(field.type)) {
        errors.push(`Form field ${index} has invalid type: ${field.type}`)
      }
      if (field.type === FIELD_TYPES.SELECT && !Array.isArray(field.options)) {
        errors.push(`Select field ${field.prop} must have options array`)
      }
    })
  }

  return errors
}

/**
 * Entity Registry
 * Central storage for all entity configurations
 */
class EntityRegistry {
  constructor() {
    this.entities = new Map()
  }

  /**
   * Register a new entity configuration
   * @param {string} entityType - Unique entity type identifier
   * @param {Object} config - Entity configuration object
   * @throws {Error} If configuration is invalid or entity already exists
   */
  register(entityType, config) {
    if (this.entities.has(entityType)) {
      throw new Error(`Entity type ${entityType} is already registered`)
    }

    const errors = validateEntityConfig(config)
    if (errors.length > 0) {
      throw new Error(`Invalid entity configuration for ${entityType}: ${errors.join(', ')}`)
    }

    this.entities.set(entityType, {
      ...config,
      // Add computed properties
      searchFields: this._extractSearchFields(config),
      displayFields: this._extractDisplayFields(config)
    })
  }

  /**
   * Get entity configuration by type
   * @param {string} entityType - Entity type identifier
   * @returns {Object|null} Entity configuration
   */
  get(entityType) {
    return this.entities.get(entityType) || null
  }

  /**
   * Check if entity type is registered
   * @param {string} entityType - Entity type identifier
   * @returns {boolean} Is registered
   */
  has(entityType) {
    return this.entities.has(entityType)
  }

  /**
   * Get all registered entity types
   * @returns {Array<string>} List of entity type identifiers
   */
  list() {
    return Array.from(this.entities.keys())
  }

  /**
   * Extract searchable fields from table columns
   * @private
   */
  _extractSearchFields(config) {
    const excludedLabels = ['审批意见', '备注', '续期原因', '变更原因', '操作']
    return config.tableColumns
      .filter(col => !excludedLabels.includes(col.label))
      .map(col => ({
        value: col.prop,
        label: col.label
      }))
  }

  /**
   * Extract display fields for list views
   * @private
   */
  _extractDisplayFields(config) {
    return config.tableColumns
      .slice(0, 6) // Limit to 6 columns for display
      .map(col => col.prop)
  }
}

// Create singleton instance
const registry = new EntityRegistry()

/**
 * Core Entity Configurations
 * Migrated from existing EntityManagement.vue configurations
 */

// Worker Entity Configuration
registry.register('worker', {
  name: '人员',
  title: '人员管理',
  apiPath: '/api/worker',
  primaryKey: 'id',
  tableColumns: [
    { prop: 'id', label: 'ID', width: 80 },
    { prop: 'name', label: '姓名', width: 100 },
    { prop: 'idCard', label: '身份证号', width: 180 },
    { prop: 'phone', label: '手机号', width: 120 },
    { prop: 'unitName', label: '单位名称', width: 200 },
    { prop: 'certificateStatus', label: '证书状态', width: 100 }
  ],
  formFields: [
    { prop: 'name', label: '姓名', type: FIELD_TYPES.TEXT, required: true },
    { prop: 'idCard', label: '身份证号', type: FIELD_TYPES.ID_CARD, required: true },
    { prop: 'phone', label: '手机号', type: FIELD_TYPES.PHONE },
    { prop: 'email', label: '邮箱', type: FIELD_TYPES.EMAIL },
    { prop: 'unitCode', label: '单位代码', type: FIELD_TYPES.TEXT },
    { prop: 'unitName', label: '单位名称', type: FIELD_TYPES.TEXT }
  ]
})

// Certificate Entity Configuration
registry.register('certificate', {
  name: '证书',
  title: '证书管理',
  apiPath: '/api/certificate',
  primaryKey: 'id',
  tableColumns: [
    { prop: 'certificateCode', label: '证书编号', width: 150 },
    { prop: 'workerName', label: '持证人', width: 100 },
    { prop: 'qualificationName', label: '资格名称', width: 200 },
    { prop: 'issueDate', label: '发证日期', width: 120 },
    { prop: 'validEndDate', label: '有效期至', width: 120 },
    { prop: 'status', label: '状态', width: 100 }
  ],
  formFields: [
    { prop: 'certificateCode', label: '证书编号', type: FIELD_TYPES.CERTIFICATE_CODE, required: true },
    { prop: 'workerId', label: '持证人ID', type: FIELD_TYPES.NUMBER, required: true },
    { prop: 'qualificationId', label: '资格ID', type: FIELD_TYPES.NUMBER },
    { prop: 'issueDate', label: '发证日期', type: FIELD_TYPES.DATE },
    { prop: 'validEndDate', label: '有效期至', type: FIELD_TYPES.DATE },
    { prop: 'status', label: '状态', type: FIELD_TYPES.SELECT, options: ['有效', '过期', '暂停', '锁定'] }
  ]
})

// Exam Entity Configuration
registry.register('exam', {
  name: '考试',
  title: '考试管理',
  apiPath: '/api/exam',
  primaryKey: 'id',
  tableColumns: [
    { prop: 'examCode', label: '考试编号', width: 150 },
    { prop: 'examName', label: '考试名称', width: 200 },
    { prop: 'examDate', label: '考试日期', width: 120 },
    { prop: 'placeName', label: '考试地点', width: 150 },
    { prop: 'maxParticipants', label: '最大人数', width: 100 },
    { prop: 'status', label: '状态', width: 100 }
  ],
  formFields: [
    { prop: 'examCode', label: '考试编号', type: FIELD_TYPES.TEXT, required: true },
    { prop: 'examName', label: '考试名称', type: FIELD_TYPES.TEXT, required: true },
    { prop: 'examDate', label: '考试日期', type: FIELD_TYPES.DATETIME },
    { prop: 'placeId', label: '考点ID', type: FIELD_TYPES.NUMBER },
    { prop: 'maxParticipants', label: '最大人数', type: FIELD_TYPES.NUMBER },
    { prop: 'status', label: '状态', type: FIELD_TYPES.SELECT, options: ['报名中', '已截止', '进行中', '已结束'] }
  ]
})

// Apply Entity Configuration
registry.register('apply', {
  name: '申请',
  title: '申请管理',
  apiPath: '/api/apply',
  primaryKey: 'id',
  tableColumns: [
    { prop: 'applyNo', label: '申请编号', width: 150 },
    { prop: 'applicantName', label: '申请人', width: 100 },
    { prop: 'idCard', label: '身份证号', width: 180 },
    { prop: 'phone', label: '手机号', width: 120 },
    { prop: 'unitName', label: '单位名称', width: 200 },
    { prop: 'applyStatus', label: '申请状态', width: 100 }
  ],
  formFields: [
    { prop: 'applyNo', label: '申请编号', type: FIELD_TYPES.TEXT },
    { prop: 'workerId', label: '申请人ID', type: FIELD_TYPES.NUMBER, required: true },
    { prop: 'applicantName', label: '申请人姓名', type: FIELD_TYPES.TEXT },
    { prop: 'idCard', label: '身份证号', type: FIELD_TYPES.ID_CARD },
    { prop: 'phone', label: '手机号', type: FIELD_TYPES.PHONE },
    { prop: 'unitCode', label: '企业代码', type: FIELD_TYPES.TEXT },
    { prop: 'unitName', label: '企业名称', type: FIELD_TYPES.TEXT },
    { prop: 'applyStatus', label: '申请状态', type: FIELD_TYPES.SELECT, options: ['待提交', '待审核', '已通过', '已拒绝'] }
  ]
})

// Apply Continue Entity Configuration
registry.register('applyContinue', {
  name: '延续申请',
  title: '延续申请管理',
  apiPath: '/api/v1/apply-continue',
  primaryKey: 'id',
  tableColumns: [
    { prop: 'applyNo', label: '申请编号', width: 150 },
    { prop: 'applicantName', label: '申请人', width: 120 },
    { prop: 'idCard', label: '身份证号', width: 180 },
    { prop: 'phone', label: '手机号', width: 120 },
    { prop: 'unitName', label: '企业名称', width: 200 },
    { prop: 'certificateCode', label: '证书编号', width: 150 },
    { prop: 'continueType', label: '延续类型', width: 100 },
    { prop: 'applyStatus', label: '申请状态', width: 100 }
  ],
  formFields: [
    { prop: 'applyNo', label: '申请编号', type: FIELD_TYPES.TEXT },
    { prop: 'workerId', label: '申请人ID', type: FIELD_TYPES.NUMBER },
    { prop: 'applicantName', label: '申请人姓名', type: FIELD_TYPES.TEXT },
    { prop: 'idCard', label: '身份证号', type: FIELD_TYPES.ID_CARD },
    { prop: 'phone', label: '手机号', type: FIELD_TYPES.PHONE },
    { prop: 'unitCode', label: '企业代码', type: FIELD_TYPES.TEXT },
    { prop: 'unitName', label: '企业名称', type: FIELD_TYPES.TEXT },
    { prop: 'certificateCode', label: '证书编号', type: FIELD_TYPES.CERTIFICATE_CODE },
    { prop: 'continueType', label: '延续类型', type: FIELD_TYPES.SELECT, options: ['个人', '企业'] },
    { prop: 'applyStatus', label: '申请状态', type: FIELD_TYPES.SELECT, options: ['待确认', '已通过', '已拒绝'] }
  ]
})

// Apply Renew Entity Configuration
registry.register('applyRenew', {
  name: '续期申请',
  title: '续期申请管理',
  apiPath: '/api/v1/apply/renew',
  primaryKey: 'id',
  tableColumns: [
    { prop: 'applyNo', label: '申请编号', width: 150 },
    { prop: 'applicantName', label: '申请人', width: 120 },
    { prop: 'idCard', label: '身份证号', width: 180 },
    { prop: 'phone', label: '手机号', width: 120 },
    { prop: 'unitName', label: '企业名称', width: 200 },
    { prop: 'certificateCode', label: '证书编号', width: 150 },
    { prop: 'renewReason', label: '续期原因', width: 300 },
    { prop: 'applyStatus', label: '申请状态', width: 100 }
  ],
  formFields: [
    { prop: 'applyNo', label: '申请编号', type: FIELD_TYPES.TEXT },
    { prop: 'workerId', label: '申请人ID', type: FIELD_TYPES.NUMBER },
    { prop: 'applicantName', label: '申请人姓名', type: FIELD_TYPES.TEXT },
    { prop: 'idCard', label: '身份证号', type: FIELD_TYPES.ID_CARD },
    { prop: 'phone', label: '手机号', type: FIELD_TYPES.PHONE },
    { prop: 'unitCode', label: '企业代码', type: FIELD_TYPES.TEXT },
    { prop: 'unitName', label: '企业名称', type: FIELD_TYPES.TEXT },
    { prop: 'certificateCode', label: '证书编号', type: FIELD_TYPES.CERTIFICATE_CODE },
    { prop: 'renewReason', label: '续期原因', type: FIELD_TYPES.TEXTAREA },
    { prop: 'applyStatus', label: '申请状态', type: FIELD_TYPES.SELECT, options: ['待确认', '已通过', '已拒绝'] }
  ]
})

// Certificate Change Entity Configuration
registry.register('certificateChange', {
  name: '证书变更',
  title: '证书变更管理',
  apiPath: '/api/v1/certificate/change',
  primaryKey: 'id',
  tableColumns: [
    { prop: 'certificateCode', label: '证书编号', width: 150 },
    { prop: 'changeType', label: '变更类型', width: 120 },
    { prop: 'changeReason', label: '变更原因', width: 300 },
    { prop: 'oldValue', label: '变更前值', width: 200 },
    { prop: 'newValue', label: '变更后值', width: 200 },
    { prop: 'changeDate', label: '变更日期', width: 120 },
    { prop: 'changeMan', label: '变更人', width: 120 }
  ],
  formFields: [
    { prop: 'certificateId', label: '证书ID', type: FIELD_TYPES.NUMBER },
    { prop: 'certificateCode', label: '证书编号', type: FIELD_TYPES.CERTIFICATE_CODE },
    { prop: 'changeType', label: '变更类型', type: FIELD_TYPES.SELECT, options: ['基本信息', '工作单位', '证书状态'] },
    { prop: 'changeReason', label: '变更原因', type: FIELD_TYPES.TEXTAREA },
    { prop: 'oldValue', label: '变更前值', type: FIELD_TYPES.TEXTAREA },
    { prop: 'newValue', label: '变更后值', type: FIELD_TYPES.TEXTAREA },
    { prop: 'changeDate', label: '变更日期', type: FIELD_TYPES.DATE },
    { prop: 'changeMan', label: '变更人', type: FIELD_TYPES.TEXT }
  ]
})

// Certificate Continue Entity Configuration
registry.register('certificateContinue', {
  name: '证书延续',
  title: '证书延续管理',
  apiPath: '/api/v1/certificate/continue',
  primaryKey: 'id',
  tableColumns: [
    { prop: 'certificateCode', label: '证书编号', width: 150 },
    { prop: 'continueType', label: '延续类型', width: 120 },
    { prop: 'continueStartDate', label: '延续开始日期', width: 150 },
    { prop: 'continueEndDate', label: '延续结束日期', width: 150 },
    { prop: 'continueYears', label: '延续年数', width: 100 },
    { prop: 'continueFee', label: '延续费用', width: 120 },
    { prop: 'continueStatus', label: '延续状态', width: 100 },
    { prop: 'continueMan', label: '延续人', width: 120 }
  ],
  formFields: [
    { prop: 'certificateId', label: '证书ID', type: FIELD_TYPES.NUMBER },
    { prop: 'certificateCode', label: '证书编号', type: FIELD_TYPES.CERTIFICATE_CODE },
    { prop: 'continueType', label: '延续类型', type: FIELD_TYPES.SELECT, options: ['年度延续', '专项延续'] },
    { prop: 'continueStartDate', label: '延续开始日期', type: FIELD_TYPES.DATE },
    { prop: 'continueEndDate', label: '延续结束日期', type: FIELD_TYPES.DATE },
    { prop: 'continueYears', label: '延续年数', type: FIELD_TYPES.NUMBER },
    { prop: 'continueFee', label: '延续费用', type: FIELD_TYPES.NUMBER },
    { prop: 'continueStatus', label: '延续状态', type: FIELD_TYPES.SELECT, options: ['未延续', '已延续', '已过期'] },
    { prop: 'continueMan', label: '延续人', type: FIELD_TYPES.TEXT }
  ]
})

/**
 * Export registry instance and utilities
 */
export { registry }
export default registry

/**
 * Helper functions for working with entity configurations
 */

/**
 * Get entity configuration with error handling
 * @param {string} entityType - Entity type identifier
 * @returns {Object} Entity configuration
 * @throws {Error} If entity not found
 */
export function getEntityConfig(entityType) {
  const config = registry.get(entityType)
  if (!config) {
    throw new Error(`Entity type "${entityType}" is not registered in the entity registry`)
  }
  return config
}

/**
 * Validate field value based on field type
 * @param {string} fieldType - Field type
 * @param {any} value - Field value
 * @returns {Object} { isValid: boolean, message?: string }
 */
export function validateFieldValue(fieldType, value) {
  if (value === null || value === undefined || value === '') {
    return { isValid: true, message: '' } // Required validation handled separately
  }

  switch (fieldType) {
    case FIELD_TYPES.EMAIL:
      return {
        isValid: VALIDATION_RULES.email(value),
        message: VALIDATION_RULES.email(value) ? '' : '请输入有效的邮箱地址'
      }
    case FIELD_TYPES.PHONE:
      return {
        isValid: VALIDATION_RULES.phone(value),
        message: VALIDATION_RULES.phone(value) ? '' : '请输入有效的手机号码'
      }
    case FIELD_TYPES.ID_CARD:
      return {
        isValid: VALIDATION_RULES.idCard(value),
        message: VALIDATION_RULES.idCard(value) ? '' : '请输入有效的身份证号'
      }
    case FIELD_TYPES.CERTIFICATE_CODE:
      return {
        isValid: VALIDATION_RULES.certificateCode(value),
        message: VALIDATION_RULES.certificateCode(value) ? '' : '请输入有效的证书编号'
      }
    case FIELD_TYPES.NUMBER:
      return {
        isValid: VALIDATION_RULES.positiveNumber(value),
        message: VALIDATION_RULES.positiveNumber(value) ? '' : '请输入有效的数字'
      }
    case FIELD_TYPES.DATE:
    case FIELD_TYPES.DATETIME:
      return {
        isValid: VALIDATION_RULES.date(value),
        message: VALIDATION_RULES.date(value) ? '' : '请输入有效的日期'
      }
    default:
      return { isValid: true, message: '' }
  }
}

/**
 * Get all registered entity types as options for select
 * @returns {Array<{label: string, value: string}>}
 */
export function getEntityOptions() {
  return registry.list().map(type => ({
    label: registry.get(type).title,
    value: type
  }))
}