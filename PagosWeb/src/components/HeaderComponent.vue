<template v-bind="$attrs">
    <div class="container-fluid">
        <div class="card">
            <div class="card-header px-4">
                <router-link v-if="link" :to="{ name: link, params: paramsLink }"
                    class="btn badge badge-light text-dark mb-2 ps-0 text-xxs">
                    <i class="material-icons" style="font-size: 15px;">arrow_back_ios</i>
                    &nbsp;Atras</router-link>
                <div class="d-sm-flex ">
                    <div class="col-5">
                        <h5 class="mb-0" v-if="props.title">{{ props.title }} &nbsp;{{ props.numero }}</h5>
                        <h5 class="mb-0" v-else>{{ route.meta.title }}</h5>
                        <span :class="props.text" v-if="props.subtitle">
                            {{ props.subtitle }}
                            <span class="text-dark ms-2 font-weight-bold">
                                {{ props.nombre }}
                            </span>
                        </span>
                    </div>
                    <slot name="status">
                    </slot>
                    <div class="ms-auto my-auto mt-lg-0 mt-4">
                        <div class="ms-auto my-auto">
                            <slot name="buttonAction">
                            </slot>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card-body pt-0 px-4">
                <div class="row">
                    <div class="col-lg-12">
                        <slot name="body">
                        </slot>
                    </div>
                </div>
            </div>
            <div class="card-footer pt-0 px-4">
                <div class="row">
                    <div class="col-lg-12">
                        <slot name="footer">
                        </slot>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script setup>

import { useRoute } from 'vue-router';

const route = useRoute()

const props = defineProps({
    title: {
        type: String,
        default: ''
    },
    numero: {
        type: String,
        default: ''
    },
    link: {
        type: String,
        default: ''
    },
    paramsLink: {
        type: Object,
        default: {}
    },
    subtitle: {
        type: String,
    },
    nombre: {
        type: String,
    },
    text: {
        type: String,
        default: 'text-sm'
    },
})
</script>