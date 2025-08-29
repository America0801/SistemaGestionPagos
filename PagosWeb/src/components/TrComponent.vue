<template>
  <tr
    v-bind="$attrs"
    :class="{ 'tdanulado': anulado }"
    class="hover:bg-gray-100 cursor-pointer"
  >
    <slot />
  </tr>
</template>

<script setup>
defineOptions({
  inheritAttrs: false
})

defineProps({
  anulado: {
    type: Boolean,
    default: false
  }
})
</script>

<style scoped>
.tdanulado td {
  background-color: #fbe3e1 !important;
  color: red !important;
}
</style>
