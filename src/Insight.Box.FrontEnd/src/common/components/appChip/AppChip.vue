<template>

    <span class="w-fit px-3 py-1 rounded-full text-xs whitespace-nowrap bg-opacity-20" :class="styles">
        <i v-if="icon" :class="[icon, icon && text ? 'mr-1' : '']"/>
        {{ text }}
    </span>

</template>

<script setup lang="ts">

import {computed, type PropType} from "vue";
import {ActionType} from "@/common/components/actions/ActionType";

const props = defineProps({
    text: {
        type: String
    },
    icon: {
        type: String,
    },
    showBorder: {
        type: Boolean,
    },
    type: {
        type: Number as PropType<ActionType>,
        default: ActionType.Secondary
    }
});

const styles = computed(() => {
    let styles = '';

    switch (props.type) {
        case ActionType.Primary:
            styles += ' bg-bg-primaryColor text-bg-primaryColor';
            if(props.showBorder) styles += ' border-bg-primaryColor';
            break;
        case ActionType.Secondary:
            styles += ' bg-tertiaryContentColor text-tertiaryContentColor';
            if(props.showBorder) styles += ' border-tertiaryContentColor';
            break;
        case ActionType.Danger:
            styles += ' bg-dangerColor text-dangerColor';
            if(props.showBorder) styles += ' border-dangerColor';
            break;
        case ActionType.Success:
            styles += ' bg-successColor text-successColor';
            if(props.showBorder) styles += ' border-successColor';
            break;
        case ActionType.Processing:
            styles += ' bg-processingColor text-processingColor';
            if(props.showBorder) styles += ' border-processingColor';
            break;
    }

    if (props.showBorder) {
        styles += ' border';
    }

    return styles;
});

</script>