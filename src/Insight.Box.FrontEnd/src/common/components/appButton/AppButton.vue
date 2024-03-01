<template>

    <button type="button"
            class="theme-input-border-round text-md
            theme-action theme-action-padding theme-action-overlay theme-btn-bg-primary
            theme-action-content theme-action-shadow theme-action-transition">
        <i v-if="icon" :class="icon"></i>
        {{ text }}
    </button>

</template>

<script setup lang="ts">

import {ButtonType} from "@/common/components/appButton/ButtonType";
import {computed, type PropType} from "vue";

const props = defineProps({
    type: {
        type: Number as PropType<ButtonType>,
        required: true,
        default: ButtonType.Primary
    },
    text: {
        type: String,
        default: '',
    },
    disabled: {
        type: Boolean,
        default: false
    },
    icon: {
        type: String,
        default: ''
    }
});

// onMounted(() => {
//     component.value?.classList.remove()
// });

const componentClass = computed(() => {
    switch (props.type) {
        case ButtonType.Primary:
            return 'bg-accentPrimaryColor';
        case ButtonType.Secondary:
            return 'bg-accentSecondaryColor';
        case ButtonType.Tertiary:
            return 'bg-gray-200';
    }

    if (props.disabled) {
        return 'bg-gray-400';
    }

    return classes.join(' ');
});

</script>