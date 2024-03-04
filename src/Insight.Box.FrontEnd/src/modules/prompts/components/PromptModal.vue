<template>

    <ModalBase :isActive="props.isActive" @closeModal="onCloseModal">

        <template v-slot:header>
            <h1 class="text-primaryContentColor text-xl">
                {{
                    isCreate
                        ? LayoutConstants.CreatePrompt :
                        LayoutConstants.EditPrompt
                }}
            </h1>
        </template>

        <!-- Prompt modal content -->
        <template v-slot:content>

            <div class="modal-content-padding modal-content-layout">

                <form class="flex flex-col gap-10" @submit.prevent="onSubmit">

                    <!-- Modal inputs -->
                    <form-input v-model="currentValue.prompt" :label="LayoutConstants.Prompt"
                                :placeholder="LayoutConstants.EnterPrompt"/>

                    <!-- Modal actions -->
                    <div class="flex gap-10">
                        <app-button :type="ButtonType.Secondary" class="w-full" :text="LayoutConstants.Cancel"
                                    @click="onCloseModal"/>
                        <app-button :type="ButtonType.Primary" class="w-full" :text="LayoutConstants.Submit"
                                    :role="ButtonRole.Submit" @click="onSubmit"/>
                    </div>

                </form>
            </div>

        </template>

    </ModalBase>

</template>

<script setup lang="ts">

import {defineEmits, type PropType, ref, watch} from 'vue';
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import AppButton from "@/common/components/appButton/AppButton.vue";
import {ButtonType} from "@/common/components/appButton/ButtonType";
import FormInput from "@/common/components/formInput/FormInput.vue";
import ModalBase from "@/common/components/modalBase/ModalBase.vue";
import {ButtonRole} from "@/common/components/appButton/ButtonRole";
import {AnalysisPrompt} from "@/modules/prompts/models/AnalysisPrompt";
import type {AnalysisPromptCategory} from "@/modules/prompts/models/AnalysisPromptCategory";

const props = defineProps({
    prompt: {
        type: Object as PropType<AnalysisPrompt>,
        default: new AnalysisPrompt()
    },
    promptCategory: {
        type: Object as PropType<AnalysisPromptCategory>,
        required: true
    },
    isActive: {
        type: Boolean,
        default: false
    },
    isCreate: {
        type: Boolean,
        default: true
    }
});

const emit = defineEmits<{
    (e: 'closeModal'): void
    (e: 'submit', prompt: AnalysisPrompt): void
}>();

const currentValue = ref<AnalysisPrompt>(props.prompt ?? new AnalysisPrompt());

const onCloseModal = () => {
    // Reset values
    if(props.isCreate)
        currentValue.value = new AnalysisPrompt();

    emit('closeModal');
}

const onSubmit = async () => {
    emit('submit', props.prompt);
    emit('closeModal');
}

watch(() => props.prompt, (newValue) => {
    currentValue.value = newValue;
});

</script>