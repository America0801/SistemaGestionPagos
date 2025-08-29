import { createApp, Transition } from 'vue'
import App from './App.vue'
import router from './router/'
import { createPinia } from 'pinia'
import 'bootstrap'
import 'bootstrap/dist/css/bootstrap.min.css'
import Vue3Toastify, { toast } from 'vue3-toastify'
import 'vue3-toastify/dist/index.css';
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/js/bootstrap.bundle.min.js";
import "bootstrap-icons/font/bootstrap-icons.css";

const pinia = createPinia()

const appInstance = createApp(App)

appInstance.use(Vue3Toastify, {
    transition: toast.TRANSITIONS.SLIDE,
    position: toast.POSITION.BOTTOM_RIGHT,
    theme: toast.THEME.COLORED,
    pauseOnFocusLoss: false,
    multiple: true,
})

appInstance.use(router)
appInstance.use(pinia)
appInstance.mount('#app')
