<template>
    <Teleport to="body">
        <div class="modal fade" ref="modalEl" tabindex="-1" :aria-labelledby="labelId" aria-hidden="true">
            <div class="modal-dialog" :class="dialogClasses">
                <div class="modal-content modern-modal-content">
                    <!-- Header -->
                    <div v-if="!hideHeader" class="modal-header border-0">
                        <h5 class="modal-title" :id="labelId">
                            <slot name="title">{{ title }}</slot>
                        </h5>

                        <button v-if="closeButton" type="button" class="btn-close" data-bs-dismiss="modal"
                            aria-label="Close"></button>
                    </div>

                    <!-- Body -->
                    <div class="modal-body" :class="bodyClass">
                        <slot />
                    </div>

                    <!-- Footer -->
                    <div v-if="!hideFooter" class="modal-footer border-0">
                        <slot name="footer">
                            <!-- Footer por defecto si no proves #footer -->
                            <button class="btn btn-outline-secondary" @click="onCancel">Cancelar</button>
                            <button class="btn btn-primary" @click="onConfirm">Confirmar</button>
                        </slot>
                    </div>
                </div>
            </div>
        </div>
    </Teleport>
</template>

<script setup>
import { onMounted, onBeforeUnmount, ref, watch, computed } from 'vue'
// Opción A: importar solo el módulo Modal
// import Modal from 'bootstrap/js/dist/modal'
// Opción B: si ya importaste el bundle en main, puedes traerlo así:
import { Modal } from 'bootstrap'

const props = defineProps({
    modelValue: { type: Boolean, default: false }, // v-model
    title: { type: String, default: '' },

    // Apariencia
    size: { type: String, default: 'md' }, // 'sm' | 'md' | 'lg' | 'xl'
    centered: { type: Boolean, default: true },
    scrollable: { type: Boolean, default: false },
    fullscreen: { type: [Boolean, String], default: false }, // true | 'sm-down' | 'md-down' ...

    // Comportamiento
    backdrop: { type: [Boolean, String], default: true }, // true | false | 'static'
    keyboard: { type: Boolean, default: true },
    closeButton: { type: Boolean, default: true },

    // Estructura
    hideHeader: { type: Boolean, default: false },
    hideFooter: { type: Boolean, default: false },

    // Clases extra
    dialogClass: { type: String, default: '' },
    bodyClass: { type: String, default: '' },

    // Accesibilidad
    labelId: { type: String, default: 'base-modal-label' }
})

const emit = defineEmits([
    'update:modelValue',
    'show', 'shown', 'hide', 'hidden',
    'confirm', 'cancel'
])

const modalEl = ref(null)
let instance = null

const dialogClasses = computed(() => {
    const classes = []
    if (props.centered) classes.push('modal-dialog-centered')
    if (props.scrollable) classes.push('modal-dialog-scrollable')

    // tamaños
    if (props.size === 'sm') classes.push('modal-sm')
    if (props.size === 'lg') classes.push('modal-lg')
    if (props.size === 'xl') classes.push('modal-xl')
    // 'md' no agrega clase (Bootstrap usa el tamaño por defecto)

    // fullscreen
    if (props.fullscreen === true) classes.push('modal-fullscreen')
    else if (typeof props.fullscreen === 'string' && props.fullscreen.endsWith('-down')) {
        classes.push(`modal-fullscreen-${props.fullscreen}`)
    }

    if (props.dialogClass) classes.push(props.dialogClass)
    return classes.join(' ')
})

onMounted(() => {
    instance = new Modal(modalEl.value, {
        backdrop: props.backdrop,
        keyboard: props.keyboard,
        focus: true
    })

    // Enlazar eventos de Bootstrap a eventos Vue
    modalEl.value.addEventListener('show.bs.modal', () => emit('show'))
    modalEl.value.addEventListener('shown.bs.modal', () => emit('shown'))
    modalEl.value.addEventListener('hide.bs.modal', () => emit('hide'))
    modalEl.value.addEventListener('hidden.bs.modal', () => {
        emit('hidden')
        emit('update:modelValue', false) // mantener v-model sincronizado
    })

    if (props.modelValue) instance.show()
})

onBeforeUnmount(() => {
    try {
        instance?.hide()
        instance?.dispose()
    } finally {
        instance = null
    }
})

// Escuchar cambios del v-model
watch(() => props.modelValue, (val) => {
    if (!instance) return
    val ? instance.show() : instance.hide()
})

function onCancel() {
    emit('cancel')
    instance?.hide()
}

function onConfirm() {
    emit('confirm')
    // Tú decides si cerrar aquí o esperar a que el padre confirme guardado:
    // instance?.hide()
}
</script>

<style scoped>
/* Toque moderno (suave, limpio) */
.modern-modal-content {
    border: none;
    border-radius: 1rem;
    /* esquinas suaves */
    box-shadow: 0 12px 34px rgba(0, 0, 0, 0.16);
}

/* Opcional: más “aire” por dentro */
.modal-header,
.modal-footer {
    padding-top: 1rem;
    padding-bottom: 1rem;
}

.modal-body {
    padding-top: 0.25rem;
    padding-bottom: 0.75rem;
}
</style>
