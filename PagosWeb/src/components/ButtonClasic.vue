<template>
    <button class="btn mb-0" :class="getClasses(variant, color, size, fullWidth, active)">
        <i v-if="icon" class="material-icons" :style="`font-size:${iconSize}px;`">
            {{ icon }}
        </i>
        <span v-if="icon">&nbsp;</span>
        {{ label }}
    </button>
</template>

<script>
export default {
    name: "ButtonClasic",
    props: {
        label: { type: String, default: "" },   // 👈 aquí la agregas
        color: { type: String, default: "success" },
        size: { type: String, default: "md" },
        variant: { type: String, default: "fill" },
        fullWidth: { type: Boolean, default: false },
        active: { type: Boolean, default: false },
        icon: { type: String, default: "" },
        iconSize: { type: String, default: "16" },
    },
    methods: {
        getClasses(variant, color, size, fullWidth, active) {
            let colorValue, sizeValue = "", fullWidthValue = "", activeValue = "";

            if (variant === "gradient") {
                colorValue = `btn-${color} bg-gradient`;
            } else if (variant === "outline") {
                colorValue = `btn-outline-${color}`;
            } else {
                colorValue = `btn-${color}`;
            }

            if (size === "sm") sizeValue = "btn-sm";
            else if (size === "lg") sizeValue = "btn-lg";
            if (fullWidth) fullWidthValue = "w-100";
            if (active) activeValue = "active";

            return `${colorValue} ${sizeValue} ${fullWidthValue} ${activeValue}`.trim();
        },
    },
};
</script>