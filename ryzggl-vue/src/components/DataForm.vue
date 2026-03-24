<template>
  <el-form
    ref="formRef"
    :model="formData"
    :rules="rules"
    :label-width="labelWidth"
    :label-position="labelPosition"
    :size="size"
    :disabled="disabled"
    :validate-on-rule-change="validateOnRuleChange"
    :inline="inline"
    @validate="handleValidate"
    @validate-error="handleValidateError"
  >
    <slot></slot>

    <!-- 默认表单按钮 -->
    <template v-if="showActions">
      <el-form-item>
        <slot name="actions">
          <el-button
            type="primary"
            :loading="submitting"
            @click="handleSubmit"
          >
            {{ submitText }}
          </el-button>
          <el-button @click="handleReset">
            {{ cancelText }}
          </el-button>
        </slot>
      </el-form-item>
    </template>
  </el-form>
</template>

<script>
import { ref } from 'vue'

export default {
  name: 'DataForm',

  props: {
    modelValue: {
      type: Object,
      default: () => ({})
    },
    rules: {
      type: Object,
      default: () => ({})
    },
    labelWidth: {
      type: String,
      default: '120px'
    },
    labelPosition: {
      type: String,
      default: 'right'
    },
    size: {
      type: String,
      default: 'default'
    },
    disabled: {
      type: Boolean,
      default: false
    },
    validateOnRuleChange: {
      type: Boolean,
      default: true
    },
    inline: {
      type: Boolean,
      default: false
    },
    showActions: {
      type: Boolean,
      default: true
    },
    submitting: {
      type: Boolean,
      default: false
    },
    submitText: {
      type: String,
      default: '提交'
    },
    cancelText: {
      type: String,
      default: '取消'
    }
  },

  emits: [
    'update:modelValue',
    'submit',
    'reset',
    'validate',
    'validate-error'
  ],

  setup(props, { emit }) {
    const formRef = ref(null)

    /**
     * Computed form data with two-way binding
     */
    const formData = computed({
      get: () => props.modelValue,
      set: (value) => emit('update:modelValue', value)
    })

    /**
     * Handle form submit
     */
    const handleSubmit = async () => {
      if (!formRef.value) return

      try {
        const valid = await formRef.value.validate()
        if (valid) {
          emit('submit', props.modelValue)
        }
      } catch (error) {
        console.error('Form validation error:', error)
      }
    }

    /**
     * Handle form reset
     */
    const handleReset = () => {
      if (formRef.value) {
        formRef.value.resetFields()
      }
      emit('reset')
    }

    /**
     * Handle validate event
     */
    const handleValidate = (...args) => {
      emit('validate', ...args)
    }

    /**
     * Handle validate-error event
     */
    const handleValidateError = (...args) => {
      emit('validate-error', ...args)
    }

    // Expose form methods
    const validate = () => {
      return formRef.value ? formRef.value.validate() : Promise.resolve(false)
    }

    const validateField = (props) => {
      return formRef.value ? formRef.value.validateField(props) : Promise.resolve(false)
    }

    const resetFields = (props) => {
      if (formRef.value) {
        formRef.value.resetFields(props)
      }
    }

    const clearValidate = (props) => {
      if (formRef.value) {
        formRef.value.clearValidate(props)
      }
    }

    return {
      formRef,
      formData,
      handleSubmit,
      handleReset,
      handleValidate,
      handleValidateError,
      validate,
      validateField,
      resetFields,
      clearValidate
    }
  }
}
</script>
