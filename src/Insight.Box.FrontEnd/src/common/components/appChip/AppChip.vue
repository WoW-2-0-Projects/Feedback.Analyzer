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
        case ActionType.Success:
            styles += ' bg-successColor text-successColor border-successColor';
            break;
        case ActionType.Danger:
            styles += ' bg-dangerColor text-dangerColor';
            break;
        case ActionType.Secondary:
            styles += ' bg-tertiaryContentColor text-tertiaryContentColor';
            break;
    }

    if (props.showBorder) {
        styles += ' border';
    }

    return styles;
});

</script>