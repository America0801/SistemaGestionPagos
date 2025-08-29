<template>
    <HeaderComponent title="Registro Basuras">
        <template v-slot:buttonAction>
            <div class="input-group input-group-outline d-flex pb-3">
                <ButtonClasic label="Crear Registro" @click="open = true" />
            </div>
        </template>
        <template v-slot:body>
            <TableComponent>
                <tr>
                    <ThComponent> Fecha </ThComponent>
                    <ThComponent> Monto </ThComponent>
                    <ThComponent> Beneficiario </ThComponent>
                    <ThComponent> Documento </ThComponent>
                    <ThComponent> </ThComponent>
                </tr>
                <TrComponent v-if="basuras" v-for="detail in basuras" :key="detail.id" data-bs-toggle="tooltip"
                    data-bs-placement="bottom">

                    <TdComponent>
                        {{ detail.fecha }}
                    </TdComponent>

                    <TdComponent>
                        {{ detail.monto }}
                    </TdComponent>

                    <TdComponent>
                        {{ detail.beneficiario }}
                    </TdComponent>

                    <TdComponent>
                        {{ detail.documento }}
                    </TdComponent>

                </TrComponent>
            </TableComponent>
        </template>
    </HeaderComponent>
    <ModalPagosComponent v-model="open" @saved="refresh" />
</template>

<script setup>
import { ref } from "vue";
import HeaderComponent from "@/components/HeaderComponent.vue";
import TdComponent from "@/components/TdComponent.vue";
import TableComponent from "@/components/TableComponent.vue";
import ThComponent from "@/components/ThComponent.vue";
import TrComponent from "@/components/TrComponent.vue";
import ButtonClasic from "@/components/ButtonClasic.vue";
import { usePagosComposable } from '@/composables/usePagosComposable'
import ModalPagosComponent from "@/components/ModalPagosComponent.vue"

const open = ref(false)

const {
    basuras,
    cargarDatosBasura,
} = usePagosComposable();

const refresh = async () => {
    await cargarDatosBasura();
};

refresh();

</script>