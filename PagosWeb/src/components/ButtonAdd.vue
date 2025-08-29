<template>
    <ButtonClasic :type="type" :size="size" :color="color" :variant="variant" :icon="icon" :iconSize="iconSize"
        @click="$emit('click')">
        {{ label }}
    </ButtonClasic>
</template>

<script setup>
import ButtonClasic from "@/components/ButtonClasic.vue";

const props = defineProps({
    label: { type: String, default: "" },
    color: { type: String, default: "success" },
    variant: { type: String, default: "gradient" },
    icon: { type: String, default: "library_add" },
    iconSize: { type: String, default: "22" },
    type: { type: String, default: "button" },
    size: { type: String, default: "md" },
});
defineEmits(["click"]);
</script>