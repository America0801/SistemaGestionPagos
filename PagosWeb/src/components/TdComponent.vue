<template>
  <td class="p-1" v-bind="$attrs" :class="{ 'tdanulado': anulado }">
    <slot />
  </td>
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
.tdanulado {
  background-color: #fcd2cf;
}
</style>
