<template>

    <button ref="component" type="button" :class="componentStyles" :disabled="disabled"
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
import {computed, onMounted, type PropType, ref} from "vue";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import {ButtonRole} from "@/common/components/appButton/ButtonRole";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import {DocumentService} from "@/infrastructure/services/document/DocumentService";

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
    }
});

const documentService = new DocumentService();
const component = ref<HTMLButtonElement>();

onMounted(() => {
    // if (props.layout === ButtonLayout.Square)
    //     documentService.setEqualWidth(component.value);
});

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
                styles += ' theme-action-secondary';
                break;
            case ButtonType.Danger :
                styles += ' theme-action-danger';
                break;
            case ButtonType.Success :
                styles += ' theme-action-success';
                break;
        }

    //  Add button layout styles
    if (props.layout === ButtonLayout.Rectangle)
        styles += ' theme-action-border-round action-layout';
    else if (props.layout === ButtonLayout.Square)
        styles += ' theme-action-border-round aspect-square ';
    else if (props.layout === ButtonLayout.Circle)
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

    return styles;
});

const contentStyles = computed(() => {
    let styles = ' ';

    // Add button layout styles
    if (props.layout === ButtonLayout.Rectangle)
        styles += ' theme-action-padding justify-around ';
    else if (props.layout === ButtonLayout.Square || props.layout === ButtonLayout.SquareMini || props.layout === ButtonLayout.Circle)
        styles += ' justify-center ';

    if (props.icon)
        styles += ' gap-2';

    return styles;
});

</script>