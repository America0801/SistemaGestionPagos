<template>
  <div class="table-responsive">
    <table class="table align-items-center mb-0 table-hover table-bordered table-sm text-sm">
      <thead class="thead-light">
        <slot name="header" />
      </thead>
      <tbody>
        <slot />
      </tbody>
    </table>
  </div>
</template>

<script setup>
defineOptions({
  inheritAttrs: false
})
</script>
