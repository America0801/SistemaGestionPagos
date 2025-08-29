import PagosViewComponent from "@/components/PagosViewComponent.vue";
import Prueba from "@/components/Prueba.vue";
import BasuraListView from "@/views/BasuraListView.vue";
import { createRouter, createWebHistory } from "vue-router";

const routes = [
    {
        path: "/pagos",
        name: "pagos",
        component: PagosViewComponent
    },
    {
        path: "/prueba",
        name: "prueba",
        component: Prueba
    },
    {
        path: "/basura",
        name: "basura-lista",
        component: BasuraListView
    },
]

const router = createRouter({
    history: createWebHistory(),
    routes,
})

export default router