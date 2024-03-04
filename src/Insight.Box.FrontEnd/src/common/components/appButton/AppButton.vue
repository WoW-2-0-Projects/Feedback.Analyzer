<template>

    <button type="button" :class="componentStyles"
            class="text-md text-nowrap overflow-hidden
                   theme-action-content theme-action-shadow">
        <span class="h-full w-full inline-flex items-center theme-action-overlay theme-action-transition"
              :class="contentStyles">
            <i v-if="icon" :class="icon"></i>
            {{ text }}
        </span>
    </button>

</template>

<script setup lang="ts">

import {ButtonType} from "@/common/components/appButton/ButtonType";
import {computed, type PropType} from "vue";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import {ButtonRole} from "@/common/components/appButton/ButtonRole";

const props = defineProps({
    type: {
        type: Number as PropType<ButtonType>,
        default: ButtonType.Primary
    },
    role: {
        type: Number as PropType<ButtonRole>,
        default: ButtonRole.Button
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
    },
    layout: {
        type: Number as PropType<ButtonLayout>,
        default: ButtonLayout.Rectangle
    },
});

const componentStyles = computed(() => {
    let styles = ''

    switch (props.type) {
        case ButtonType.Primary:
            styles += 'theme-btn-bg-primary ';
            break;
        case ButtonType.Secondary:
            styles += 'theme-btn-bg-secondary ';
            break;
        case ButtonType.Danger :
            styles += 'theme-btn-bg-danger ';
            break;
    }

    if (props.layout === ButtonLayout.Rectangle)
        styles += 'theme-action-border-round theme-action-layout';
    else if (props.layout === ButtonLayout.Circle)
        styles += ' flex items-center justify-center theme-action-circle-layout';

    return styles;
});

const contentStyles = computed(() => {
    let styles = ' ';

    if (props.layout === ButtonLayout.Rectangle)
        styles += ' theme-action-padding justify-around ';
    else if (props.layout === ButtonLayout.Circle)
        styles += 'justify-center ';

    if (props.icon)
        styles += ' gap-2';

    return styles;
});

</script>