<template>
    <ModalComponent v-model="isOpen" title="Crear registro de basura" :backdrop="'static'" :centered="true"
        @shown="focusFirst">
        <div class="row g-3">
            <div class="col-sm-6">
                <label class="form-label">Fecha</label>
                <input class="form-control" v-model="basura.fecha" type="date" />
            </div>

            <div class="col-sm-6">
                <label class="form-label">Monto (Q)</label>
                <input ref="montoInput" v-model.number="basura.monto" type="number" min="0" step="0.01"
                    class="form-control" />
            </div>

            <div class="col-12">
                <label class="form-label">Beneficiario</label>
                <input v-model.trim="basura.beneficiario" type="text" class="form-control" />
            </div>

            <div class="col-12">
                <label class="form-label">Documento (PDF o imagen)</label>
                <input @change="onFileChange" type="file" class="form-control" accept=".pdf,image/*" />
                <small class="text-muted" v-if="basura.documento">{{ basura.documento.name }}</small>
            </div>
        </div>

        <template #footer>
            <button class="btn btn-light" @click="close">Cancelar</button>
            <button class="btn btn-primary" :disabled="!isValid" @click="guardar">Guardar</button>
        </template>
    </ModalComponent>
</template>

<script setup>
import { ref, reactive, watch, computed, nextTick } from 'vue'
import ModalComponent from '@/components/ModalComponent.vue'
import { usePagosComposable } from '@/composables/usePagosComposable'

const props = defineProps({
    modelValue: { type: Boolean, default: false }
})
const emit = defineEmits(['update:modelValue', 'saved'])

const isOpen = ref(props.modelValue)
watch(() => props.modelValue, v => (isOpen.value = v))
watch(isOpen, v => emit('update:modelValue', v))

const montoInput = ref(null)

// ✅ objeto basura
const basura = reactive({
    fecha: new Date().toISOString().slice(0, 10), // YYYY-MM-DD
    monto: null,
    beneficiario: '',
    documento: null
})

function reset() {
    basura.fecha = new Date().toISOString().slice(0, 10)
    basura.monto = null
    basura.beneficiario = ''
    basura.documento = null
}

function close() {
    isOpen.value = false
}

function onFileChange(e) {
    basura.documento = e.target.files?.[0] ?? null
}

const isValid = computed(() =>
    basura.monto !== null && basura.monto > 0 && basura.beneficiario.trim().length > 0
)

const { crearBasura, cargarDatosBasura } = usePagosComposable()

async function guardar() {
    try {
        const fd = new FormData()
        fd.append('fecha', basura.fecha)
        fd.append('monto', String(basura.monto ?? 0))
        fd.append('beneficiario', basura.beneficiario)
        if (basura.documento) fd.append('documento', basura.documento)

        // 👇 aquí llamamos directo al composable
        await crearBasura(fd)

        // refrescamos la tabla de basuras
        await cargarDatosBasura()

        emit('saved', { ...basura })
        close()
        reset()
    } catch (err) {
        console.error('Error creando basura:', err)
    }
}

function focusFirst() {
    nextTick(() => montoInput.value?.focus())
}
</script>