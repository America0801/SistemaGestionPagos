import { defineStore } from 'pinia';
import Api from '@/helpers/Api';
// import { toast } from 'vue3-toastify/index';

export const usePagosStore = defineStore("pagos", {
    state: () => {
        return {
            pago: {},
            basuras: [],
            basura: {}
        }
    },

    actions: {
        async cargarPagos() {
            await Api().get("PagosApi/")
        },

        async cargarDatosBasura() {
            const res = await Api().get('PagosApi/basura')
            this.basuras = res.data;
        },

        async crearBasura() {
            await Api().post('PagosApi/',
                this.basura
            ).then(() => {
                // toast.success("REGISTRO GUARDADO")
            });
            this.cargarDatosBasura();
        }
    }

})