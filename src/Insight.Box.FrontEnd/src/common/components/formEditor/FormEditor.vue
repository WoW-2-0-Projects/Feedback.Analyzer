<template>

    <!-- Editor -->
    <div class="flex flex-col gap-10">

        <!-- Editor actions -->
        <div class="flex gap-5">

            <form-input :type="FormInputType.Text" :modelValue="searchKeyword"
                        :size="ActionComponentSize.ExtraSmall" class="flex-grow"
                        @update:modelValue="value => $emit('update:modelValue', value)"
                        :label="LayoutConstants.Search" :placeholder="LayoutConstants.SearchHere"/>

            <app-button class="w-fit" :type="isEditingDisabled ? ActionType.Secondary : ActionType.Danger"
                        :layout="ButtonLayout.Square" :icon="isEditingDisabled ? 'fas fa-lock' : 'fas fa-lock-open'"
                        @click="isEditingDisabled = !isEditingDisabled"
                        :size="ActionComponentSize.ExtraSmall"/>

            <app-button class="w-fit" :type="ActionType.Secondary"
                        :layout="ButtonLayout.Square" icon="fas fa-broom"
                        :size="ActionComponentSize.ExtraSmall" @click="onClear"/>

            <app-button class="w-fit" :type="ActionType.Secondary"
                        :layout="ButtonLayout.Square" icon="fas fa-rotate-left"
                        :size="ActionComponentSize.ExtraSmall" @click="onUndo"/>

            <app-button class="w-fit" :type="ActionType.Secondary"
                        :layout="ButtonLayout.Square" icon="fas fa-rotate-right"
                        :size="ActionComponentSize.ExtraSmall" @click="onRedo"/>

        </div>

        <!-- Editor content -->
        <form-text-area :focus="focus"
                        @focusIn="setDefaultValue"
                        :modelValue="modelValue" :label="label" :placeholder="placeholder"
                        @update:modelValue="onTextUpdate" :disabled="isEditingDisabled"
        />

    </div>


</template>

<script setup lang="ts">

import {type PropType, ref, watch} from "vue";
import AppButton from "@/common/components/appButton/AppButton.vue";
import FormInput from "@/common/components/formInput/FormInput.vue";
import FormTextArea from "@/common/components/formTextArea/FormTextArea.vue";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import {FormInputType} from "@/common/components/formInput/FormInputType";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import {TextContentSnapshotQueue} from "@/infrastructure/models/textContent/TextContentSnapshotQueue";
import  {ParameterizedAction} from "@/infrastructure/models/delegates/ParameterizedAction";
import {ActionType} from "@/common/components/actions/ActionType";
import type {Action} from "@/infrastructure/models/delegates/Action";

const textSnapshotService = ref<TextContentSnapshotQueue>(new TextContentSnapshotQueue());

const props = defineProps({
    modelValue: {
        type: String,
        required: true
    },
    label: {
        type: String,
        default: ''
    },
    placeholder: {
        type: String,
        default: ''
    },
    border: {
        type: Boolean,
        default: true
    },
    defaultValue: {
        type: String,
        default: 'Enter a text here...'
    },
    focus: {
        type: Object as PropType<Action>,
    },
    props: {
        type: Boolean,
        default: false
    },
    editingDisabled: {
        type: Boolean,
        default: false
    }
});

const isEditingDisabled = ref<boolean>(props.editingDisabled);
const searchKeyword = ref<string>('');

const emit = defineEmits(['update:modelValue']);

const onTextUpdate = (value: string) => {
    emit('update:modelValue', value);
    textSnapshotService.value.record(value);
}

const onClear = () => {
    emit('update:modelValue', props.defaultValue);
}

const onUndo = () => {
    emit('update:modelValue', textSnapshotService.value.undo());
}

const onRedo = () => {
    emit('update:modelValue', textSnapshotService.value.redo());
}

const setDefaultValue = () => {
    if (!props.modelValue || props.modelValue === '')
        emit('update:modelValue', props.defaultValue);
}

watch(() => props.editingDisabled, () => isEditingDisabled.value = props.editingDisabled);

</script>