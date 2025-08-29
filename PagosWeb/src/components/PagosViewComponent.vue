<template>
    
  <RouterLink :to="{name: 'prueba'}">
    <button>
     ir a prueba
    </button>
  </RouterLink>
    Hola mundo
</template>


<script setup>
import { usePagosComposable } from '@/composables/usePagosComposable'

const {
    cargarPagos
} = usePagosComposable();

cargarPagos();
</script>

