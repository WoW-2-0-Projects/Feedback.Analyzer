<template>

    <ModalBase :isActive ="props.isActive" @closeModal="onCloseModal">
        <template v-slot:header>
            <h1 class="text-primaryContentColor text-xl">
                {{ LayoutConstants.ExecutionHistory }}
            </h1>
        </template>

        <!-- Prompt modal content -->
        <template>
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

import ModalBase from "@/common/components/modalBase/ModalBase.vue";
import type {PropType} from "vue";
import type {PromptsExecutionHistory} from "@/modules/prompts/models/PromptExecutionHistory";
import {LayoutConstants} from "../../../common/constants/LayoutConstants";
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
    (e: 'closeModal'): void;
}>();

const onCloseModal = () => {
    emit('closeModal');
}
</script>