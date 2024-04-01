<template>

    <i class="fas fa-angle-down theme-action-transition theme-action-content theme-focus-content"
       :class="componentStyles"/>

</template>

<script setup lang="ts">

import {computed, type PropType} from "vue";
import {ExpandIconStyle} from "@/common/components/icons/ExpandIconStyle";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";

const props = defineProps({
    isOpen: {
        type: Boolean,
        required: true
    },
    initialStyle: {
        type: Number as PropType<ExpandIconStyle>,
        default: ExpandIconStyle.Top
    }
});

const componentStyles = computed(() => {
    let styles = "";

    if (!props.isOpen)
        switch (props.initialStyle) {
            case ExpandIconStyle.Top:
                styles = "-rotate-180";
                break;
            case ExpandIconStyle.Right:
                styles = "-rotate-90";
                break;
        }

    return styles;
});

</script>