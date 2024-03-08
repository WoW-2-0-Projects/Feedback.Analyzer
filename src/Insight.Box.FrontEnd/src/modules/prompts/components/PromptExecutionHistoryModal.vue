<template>

    <ModalBase :isActive="props.isActive" @closeModal="onCloseModal">

        <template v-slot:header>
            <h1 class="text-primaryContentColor text-xl">
                {{ LayoutConstants.ExecutionHistory }}
            </h1>
        </template>

        <!-- Prompt modal content -->
        <template v-slot:content>
            <div class="modal-content-padding modal-content-layout" v-if="history">

                <form-text-area v-if="history.result" :modelValue="history.result"
                                :label="LayoutConstants.ExecutionResult"
                                :disabled="true"/>

                <form-text-area v-else :modelValue="history.result"
                                :label="LayoutConstants.ExecutionException"
                                :disabled="true"/>

            </div>
        </template>

    </ModalBase>

</template>

<script setup lang="ts">

import {defineEmits, type PropType, ref, watch} from 'vue';
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import AppButton from "@/common/components/appButton/AppButton.vue";
import {ButtonType} from "@/common/components/appButton/ButtonType";
import ModalBase from "@/common/components/modalBase/ModalBase.vue";
import {ButtonRole} from "@/common/components/appButton/ButtonRole";
import {AnalysisPrompt} from "@/modules/prompts/models/AnalysisPrompt";
import FormEditor from "@/common/components/formEditor/FormEditor.vue";
import type {Action} from "@/infrastructure/models/delegates/Action";
import type {PromptsExecutionHistory} from "@/modules/prompts/models/PromptExecutionHistory";
import FormTextArea from "@/common/components/formTextArea/FormTextArea.vue";

const props = defineProps({
    history: {
        type: Object as PropType<PromptsExecutionHistory> | null,
    },
    isActive: {
        type: Boolean,
        default: false
    }
});

const emit = defineEmits<{
    (e: 'closeModal'): void
}>();

const onCloseModal = () => {
    emit('closeModal');
}

</script>