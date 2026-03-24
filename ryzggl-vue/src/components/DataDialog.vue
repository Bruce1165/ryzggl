<template>
  <el-dialog
    v-model="dialogVisible"
    :title="title"
    :width="width"
    :fullscreen="fullscreen"
    :top="top"
    :modal="modal"
    :modal-class="modalClass"
    :append-to-body="appendToBody"
    :lock-scroll="lockScroll"
    :close-on-click-modal="closeOnClickModal"
    :close-on-press-escape="closeOnPressEscape"
    :show-close="showClose"
    :before-close="handleBeforeClose"
    :center="centered"
    @open="handleOpen"
    @opened="handleOpened"
    @close="handleClose"
    @closed="handleClosed"
  >
    <slot></slot>

    <template #footer>
      <slot name="footer">
        <el-button @click="handleCancel">
          {{ cancelText }}
        </el-button>
        <el-button
          type="primary"
          :loading="submitting"
          @click="handleConfirm"
        >
          {{ confirmText }}
        </el-button>
      </slot>
    </template>
  </el-dialog>
</template>

<script>
import { computed } from 'vue'

export default {
  name: 'DataDialog',

  props: {
    modelValue: {
      type: Boolean,
      default: false
    },
    title: {
      type: String,
      default: '提示'
    },
    width: {
      type: String,
      default: '50%'
    },
    fullscreen: {
      type: Boolean,
      default: false
    },
    top: {
      type: String,
      default: '15vh'
    },
    modal: {
      type: Boolean,
      default: true
    },
    modalClass: {
      type: String,
      default: ''
    },
    appendToBody: {
      type: Boolean,
      default: true
    },
    lockScroll: {
      type: Boolean,
      default: true
    },
    closeOnClickModal: {
      type: Boolean,
      default: true
    },
    closeOnPressEscape: {
      type: Boolean,
      default: true
    },
    showClose: {
      type: Boolean,
      default: true
    },
    centered: {
      type: Boolean,
      default: false
    },
    submitting: {
      type: Boolean,
      default: false
    },
    cancelText: {
      type: String,
      default: '取消'
    },
    confirmText: {
      type: String,
      default: '确定'
    }
  },

  emits: [
    'update:modelValue',
    'open',
    'opened',
    'close',
    'closed',
    'confirm',
    'cancel',
    'before-close'
  ],

  setup(props, { emit }) {
    /**
     * Computed dialog visibility
     */
    const dialogVisible = computed({
      get: () => props.modelValue,
      set: (value) => emit('update:modelValue', value)
    })

    /**
     * Handle before close
     */
    const handleBeforeClose = (...args) => {
      emit('before-close', ...args)
    }

    /**
     * Handle open event
     */
    const handleOpen = (...args) => {
      emit('open', ...args)
    }

    /**
     * Handle opened event
     */
    const handleOpened = (...args) => {
      emit('opened', ...args)
    }

    /**
     * Handle close event
     */
    const handleClose = (...args) => {
      emit('close', ...args)
    }

    /**
     * Handle closed event
     */
    const handleClosed = (...args) => {
      emit('closed', ...args)
    }

    /**
     * Handle confirm button click
     */
    const handleConfirm = () => {
      emit('confirm')
    }

    /**
     * Handle cancel button click
     */
    const handleCancel = () => {
      emit('cancel')
      dialogVisible.value = false
    }

    return {
      dialogVisible,
      handleBeforeClose,
      handleOpen,
      handleOpened,
      handleClose,
      handleClosed,
      handleConfirm,
      handleCancel
    }
  }
}
</script>
