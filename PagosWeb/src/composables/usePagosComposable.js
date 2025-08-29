import { usePagosStore } from "@/stores/usePagosStore";
import { storeToRefs } from "pinia";

export const usePagosComposable = () => {
    const pagos = usePagosStore()

    const {
        pago,
        basuras,
        basura
    } = storeToRefs(pagos)

    const cargarPagos = async() => await pagos.cargarPagos();
    const cargarDatosBasura = async() => await pagos.cargarDatosBasura();
    const crearBasura = async() => await pagos.crearBasura();

    return {
        pago, 
        basuras,
        basura,
        cargarPagos,
        cargarDatosBasura,
        crearBasura
    }
}