<template>
  <el-form
    ref="formRef"
    :model="formData"
    :rules="formRules"
    :label-width="labelWidth"
    :label-position="labelPosition"
    :disabled="disabled"
    @validate="handleValidate"
  >
    <el-row :gutter="gutter">
      <el-col
        v-for="field in visibleFields"
        :key="field.prop"
        :span="getFieldColSpan(field)"
      >
        <el-form-item
          :prop="field.prop"
          :label="field.label"
          :required="field.required"
        >
          <!-- Text Input -->
          <el-input
            v-if="field.type === 'text'"
            v-model="formData[field.prop]"
            :placeholder="`请输入${field.label}`"
            :clearable="true"
            :disabled="field.disabled"
            @input="handleFieldChange(field.prop, $event)"
          />

          <!-- Number Input -->
          <el-input-number
            v-else-if="field.type === 'number'"
            v-model="formData[field.prop]"
            :placeholder="`请输入${field.label}`"
            :controls="false"
            :disabled="field.disabled"
            :precision="field.precision || 0"
            :min="field.min"
            :max="field.max"
            style="width: 100%"
            @change="handleFieldChange(field.prop, $event)"
          />

          <!-- Date Picker -->
          <el-date-picker
            v-else-if="field.type === 'date'"
            v-model="formData[field.prop]"
            type="date"
            :placeholder="`请选择${field.label}`"
            :disabled="field.disabled"
            :format="field.format || 'YYYY-MM-DD'"
            :value-format="field.valueFormat || 'YYYY-MM-DD'"
            :clearable="true"
            style="width: 100%"
            @change="handleFieldChange(field.prop, $event)"
          />

          <!-- DateTime Picker -->
          <el-date-picker
            v-else-if="field.type === 'datetime'"
            v-model="formData[field.prop]"
            type="datetime"
            :placeholder="`请选择${field.label}`"
            :disabled="field.disabled"
            :format="field.format || 'YYYY-MM-DD HH:mm:ss'"
            :value-format="field.valueFormat || 'YYYY-MM-DD HH:mm:ss'"
            :clearable="true"
            style="width: 100%"
            @change="handleFieldChange(field.prop, $event)"
          />

          <!-- Select Dropdown -->
          <el-select
            v-else-if="field.type === 'select'"
            v-model="formData[field.prop]"
            :placeholder="`请选择${field.label}`"
            :clearable="true"
            :disabled="field.disabled"
            :filterable="field.filterable !== false"
            style="width: 100%"
            @change="handleFieldChange(field.prop, $event)"
          >
            <el-option
              v-for="option in field.options"
              :key="option.value !== undefined ? option.value : option"
              :label="option.label !== undefined ? option.label : option"
              :value="option.value !== undefined ? option.value : option"
            />
          </el-select>

          <!-- Textarea -->
          <el-input
            v-else-if="field.type === 'textarea'"
            v-model="formData[field.prop]"
            type="textarea"
            :placeholder="`请输入${field.label}`"
            :rows="field.rows || 3"
            :maxlength="field.maxlength"
            :show-word-limit="field.showWordLimit !== false"
            :disabled="field.disabled"
            @input="handleFieldChange(field.prop, $event)"
          />

          <!-- Boolean Switch -->
          <el-switch
            v-else-if="field.type === 'boolean'"
            v-model="formData[field.prop]"
            :disabled="field.disabled"
            @change="handleFieldChange(field.prop, $event)"
          />

          <!-- Phone Input -->
          <el-input
            v-else-if="field.type === 'phone'"
            v-model="formData[field.prop]"
            :placeholder="`请输入${field.label}`"
            :clearable="true"
            :disabled="field.disabled"
            maxlength="11"
            show-word-limit
            @input="handleFieldChange(field.prop, $event)"
          />

          <!-- Email Input -->
          <el-input
            v-else-if="field.type === 'email'"
            v-model="formData[field.prop]"
            :placeholder="`请输入${field.label}`"
            :clearable="true"
            :disabled="field.disabled"
            @input="handleFieldChange(field.prop, $event)"
          />

          <!-- ID Card Input -->
          <el-input
            v-else-if="field.type === 'idcard'"
            v-model="formData[field.prop]"
            :placeholder="`请输入${field.label}`"
            :clearable="true"
            :disabled="field.disabled"
            maxlength="18"
            show-word-limit
            @input="handleFieldChange(field.prop, $event)"
          />

          <!-- Certificate Code Input -->
          <el-input
            v-else-if="field.type === 'certificatecode'"
            v-model="formData[field.prop]"
            :placeholder="`请输入${field.label}`"
            :clearable="true"
            :disabled="field.disabled"
            maxlength="20"
            show-word-limit
            @input="handleFieldChange(field.prop, $event)"
          />

          <!-- Custom Slot -->
          <slot
            v-else-if="field.type === 'custom'"
            :name="field.prop"
            :field="field"
            :value="formData[field.prop]"
            :on-input="(val) => handleFieldChange(field.prop, val)"
          />
        </el-form-item>
      </el-col>
    </el-row>
  </el-form>
</template>

<script setup>
import { ref, reactive, computed, watch, nextTick } from 'vue'
import { validateFieldValue } from '../utils/entityRegistry'

/**
 * Props
 */
const props = defineProps({
  // Form fields configuration
  fields: {
    type: Array,
    required: true,
    validator: (value) => {
      return value.every(field =>
        field.prop &&
        field.label &&
        field.type
      )
    }
  },

  // Form data (v-model)
  modelValue: {
    type: Object,
    default: () => ({})
  },

  // Form rules (Element Plus format)
  rules: {
    type: Object,
    default: () => ({})
  },

  // Layout options
  columns: {
    type: Number,
    default: 2,
    validator: (value) => value >= 1 && value <= 4
  },

  gutter: {
    type: Number,
    default: 20
  },

  labelWidth: {
    type: [String, Number],
    default: '120px'
  },

  labelPosition: {
    type: String,
    default: 'right',
    validator: (value) => ['left', 'right', 'top'].includes(value)
  },

  // Form state
  disabled: {
    type: Boolean,
    default: false
  },

  // Field visibility conditions
  visibilityConditions: {
    type: Object,
    default: () => ({})
  }
})

/**
 * Emits
 */
const emit = defineEmits([
  'update:modelValue',
  'validate',
  'field-change',
  'submit'
])

/**
 * Refs
 */
const formRef = ref(null)

/**
 * Form data (local reactive copy)
 */
const formData = reactive({})

/**
 * Computed form rules with dynamic validation
 */
const formRules = computed(() => {
  const rules = { ...props.rules }

  // Generate rules for each field based on configuration
  props.fields.forEach(field => {
    if (!rules[field.prop]) {
      rules[field.prop] = []
    }

    // Required validation
    if (field.required) {
      rules[field.prop].push({
        required: true,
        message: `${field.label}不能为空`,
        trigger: ['blur', 'change']
      })
    }

    // Type-specific validation
    if (field.type === 'email') {
      rules[field.prop].push({
        validator: (rule, value, callback) => {
          if (!value) {
            callback()
            return
          }
          const validation = validateFieldValue('email', value)
          if (validation.isValid) {
            callback()
          } else {
            callback(new Error(validation.message))
          }
        },
        trigger: ['blur', 'change']
      })
    }

    if (field.type === 'phone') {
      rules[field.prop].push({
        validator: (rule, value, callback) => {
          if (!value) {
            callback()
            return
          }
          const validation = validateFieldValue('phone', value)
          if (validation.isValid) {
            callback()
          } else {
            callback(new Error(validation.message))
          }
        },
        trigger: ['blur', 'change']
      })
    }

    if (field.type === 'idcard') {
      rules[field.prop].push({
        validator: (rule, value, callback) => {
          if (!value) {
            callback()
            return
          }
          const validation = validateFieldValue('idcard', value)
          if (validation.isValid) {
            callback()
          } else {
            callback(new Error(validation.message))
          }
        },
        trigger: ['blur', 'change']
      })
    }

    if (field.type === 'certificatecode') {
      rules[field.prop].push({
        validator: (rule, value, callback) => {
          if (!value) {
            callback()
            return
          }
          const validation = validateFieldValue('certificatecode', value)
          if (validation.isValid) {
            callback()
          } else {
            callback(new Error(validation.message))
          }
        },
        trigger: ['blur', 'change']
      })
    }

    // Custom validator
    if (field.validator) {
      rules[field.prop].push({
        validator: field.validator,
        trigger: ['blur', 'change']
      })
    }
  })

  return rules
})

/**
 * Filter visible fields based on conditions
 */
const visibleFields = computed(() => {
  return props.fields.filter(field => {
    const condition = props.visibilityConditions[field.prop]
    if (condition) {
      try {
        return condition(formData)
      } catch (error) {
        console.error(`Visibility condition error for ${field.prop}:`, error)
        return true
      }
    }
    return true
  })
})

/**
 * Get field column span based on field configuration
 */
function getFieldColSpan(field) {
  // Field-specific span
  if (field.span) {
    return field.span
  }

  // Calculate span based on number of columns
  return Math.floor(24 / props.columns)
}

/**
 * Handle field value change
 */
function handleFieldChange(prop, value) {
  // Update local form data
  formData[prop] = value

  // Emit to parent
  emit('update:modelValue', { ...formData })
  emit('field-change', prop, value)
}

/**
 * Handle form validation
 */
function handleValidate(prop, isValid, message) {
  emit('validate', prop, isValid, message)
}

/**
 * Validate entire form
 * @returns {Promise<boolean>} Is valid
 */
async function validate() {
  try {
    await formRef.value.validate()
    return true
  } catch (error) {
    console.error('Form validation failed:', error)
    return false
  }
}

/**
 * Validate specific field
 * @param {string} prop - Field property name
 * @returns {Promise<boolean>} Is valid
 */
async function validateField(prop) {
  try {
    await formRef.value.validateField(prop)
    return true
  } catch (error) {
    console.error(`Field validation failed for ${prop}:`, error)
    return false
  }
}

/**
 * Reset form fields
 * @param {Object} [data] - Optional data to reset to
 */
function resetFields(data = {}) {
  formRef.value?.resetFields()

  // Reset form data
  Object.keys(formData).forEach(key => {
    delete formData[key]
  })

  // Set initial data if provided
  Object.assign(formData, data)

  // Emit to parent
  emit('update:modelValue', { ...formData })
}

/**
 * Clear validation state
 */
function clearValidate(props = null) {
  formRef.value?.clearValidate(props)
}

/**
 * Get form data
 * @returns {Object} Current form data
 */
function getFormData() {
  return { ...formData }
}

/**
 * Set form data
 * @param {Object} data - Data to set
 */
function setFormData(data) {
  Object.assign(formData, data)
  emit('update:modelValue', { ...formData })
}

/**
 * Focus on specific field
 * @param {string} prop - Field property name
 */
function focusField(prop) {
  const fieldEl = formRef.value?.$el?.querySelector(`[prop="${prop}"]`)
  if (fieldEl) {
    fieldEl.focus()
  }
}

/**
 * Watch for model value changes
 */
watch(() => props.modelValue, (newValue) => {
  if (newValue && typeof newValue === 'object') {
    Object.assign(formData, newValue)
  }
}, { immediate: true, deep: true })

/**
 * Expose methods for parent component access
 */
defineExpose({
  validate,
  validateField,
  resetFields,
  clearValidate,
  getFormData,
  setFormData,
  focusField
})
</script>

<style scoped>
/* Additional form styling if needed */
.el-form-item {
  margin-bottom: 18px;
}

/* Accessibility improvements */
.el-form-item :deep(.el-form-item__label) {
  font-weight: 500;
}

/* Focus state for keyboard navigation */
.el-form-item :deep(.el-input__wrapper):focus-within,
.el-form-item :deep(.el-select):focus-within {
  box-shadow: 0 0 0 2px var(--el-color-primary) inset;
}

/* Error state styling */
.el-form-item.is-error :deep(.el-input__wrapper),
.el-form-item.is-error :deep(.el-select) {
  box-shadow: 0 0 0 2px var(--el-color-danger) inset;
}
</style>