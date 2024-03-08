<template>

    <!-- Editor -->
    <div class="flex flex-col gap-10">

        <!-- Editor actions -->
        <div class="flex gap-5">

            <form-input :type="FormInputType.Text" :modelValue="searchKeyword"
                        :size="ActionComponentSize.ExtraSmall"
                        class="flex-grow"
                        @update:modelValue="($event) => $emit('update:modelValue', $event)"
                        :label="LayoutConstants.Search" :placeholder="LayoutConstants.SearchHere"/>

            <app-button class="w-fit" :type="ButtonType.Secondary" :layout="ButtonLayout.Square"
                        :icon="isEditingDisabled ? 'fas fa-lock' : 'fas fa-lock-open'"
                        @click="isEditingDisabled = !isEditingDisabled"
                        :size="ActionComponentSize.ExtraSmall"/>

            <app-button class="w-fit" :type="ButtonType.Secondary"
                        :layout="ButtonLayout.Square" icon="fas fa-broom"
                        :size="ActionComponentSize.ExtraSmall" @click="onClear"/>

            <app-button class="w-fit" :type="ButtonType.Secondary"
                        :layout="ButtonLayout.Square" icon="fas fa-rotate-left"
                        :size="ActionComponentSize.ExtraSmall" @click="onUndo"/>

            <app-button class="w-fit" :type="ButtonType.Secondary"
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


import FormTextArea from "@/common/components/formTextArea/FormTextArea.vue";
import {defineEmits, defineProps, nextTick, onBeforeMount, onMounted, type PropType, ref} from "vue";
import {ButtonType} from "@/common/components/appButton/ButtonType";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import AppButton from "@/common/components/appButton/AppButton.vue";
import {TextContentSnapshotQueue} from "@/infrastructure/models/textContent/TextContentSnapshotQueue";
import {FormInputType} from "@/common/components/formInput/FormInputType";
import FormInput from "@/common/components/formInput/FormInput.vue";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import {DocumentService} from "@/infrastructure/services/document/DocumentService";
import {TimerService} from "@/infrastructure/services/timer/TimerService";
import type {Action} from "@/infrastructure/models/delegates/Action";

const timerService = new TimerService();
const searchKeyword = ref<string>('');
const textSnapshotService = ref<TextContentSnapshotQueue>(new TextContentSnapshotQueue());
const isEditingDisabled = ref<boolean>(false);
const timerId = ref<number>();

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
    }
});

onBeforeMount(() => {
    // setTimeout(() => setDefaultValue(), 1000);

    // if (!props.modelValue || props.modelValue === '')
    //     timerId.value = timerService.setTimer(() => setDefaultValue(), 1000);
});

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

</script>