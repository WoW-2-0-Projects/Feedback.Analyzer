<template>

    <button ref="component" type="button" :class="componentStyles" :disabled="disabled"
            class="text-md text-nowrap overflow-hidden theme-action-content theme-action-shadow">
        <span class="h-full w-full inline-flex items-center theme-action-overlay theme-action-transition"
              :class="contentStyles">
            <i v-if="icon" :class="icon"></i>
            <span class="theme-action-transition" :class="hideText ? 'w-0 opacity-0' : 'w-fit opacity-100'">
                {{ text }}
            </span>
        </span>
    </button>

</template>

<script setup lang="ts">

import {ButtonType} from "@/common/components/appButton/ButtonType";
import {computed, type PropType, ref} from "vue";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import {ButtonRole} from "@/common/components/appButton/ButtonRole";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";

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
    size: {
        type: Number as PropType<ActionComponentSize>,
        default: ActionComponentSize.Medium
    },
    hideText: {
        type: Boolean,
        default: false
    }
});

const component = ref<HTMLButtonElement>();
const actualLayout = ref<ButtonLayout>(props.layout);

const componentStyles = computed(() => {
    let styles = ''

    // Add button type styles
    if (props.disabled) {
        styles += ' theme-action-disabled cursor-not-allowed';
    } else
        switch (props.type) {
            case ButtonType.Primary:
                styles += ' theme-action-primary';
                break;
            case ButtonType.Secondary:
                styles += ' bg-accentTertiaryColor';
                break;
            case ButtonType.Danger :
                styles += ' theme-action-danger';
                break;
            case ButtonType.Success :
                styles += ' theme-action-success';
                break;
        }

    //  Add button layout styles
    if (actualLayout.value === ButtonLayout.Rectangle)
        styles += ' theme-action-border-round action-layout';
    else if (actualLayout.value === ButtonLayout.Square)
        styles += ' theme-action-border-round aspect-square ';
    else if (actualLayout.value === ButtonLayout.Circle)
        styles += ' flex items-center justify-center action-circle-layout';

    // Add button size styles
    switch (props.size) {
        case ActionComponentSize.Medium:
            styles += ' action-layout';
            break;
        case ActionComponentSize.Small:
            styles += ' action-small-layout';
            break;
        case ActionComponentSize.ExtraSmall:
            styles += ' action-extra-small-layout';
            break;
    }

    if((!(props.text && props.icon) || props.hideText) && !styles.includes('aspect-square')) {
        styles += ' aspect-square';
    }

    return styles;
});

const contentStyles = computed(() => {
    let styles = ' ';

    // Add button layout styles
    if (actualLayout.value === ButtonLayout.Rectangle)
        styles += ' theme-action-padding';

    if (props.icon && props.text && !props.hideText)
        styles += ' justify-around gap-2';
    else
        styles += ' justify-center';

    return styles;
});

</script>