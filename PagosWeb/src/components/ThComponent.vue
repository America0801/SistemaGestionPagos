<template>
  <th class="font-weight-bolder" v-bind="$attrs">
    <slot />
  </th>
</template>

<script setup>
defineOptions({
  inheritAttrs: false
})
</script>
