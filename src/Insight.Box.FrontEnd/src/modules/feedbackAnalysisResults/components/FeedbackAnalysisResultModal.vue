<template>

    <ModalBase :isActive="props.isActive" @closeModal="onClose">

        <template v-slot:header>
            <h1 class="text-primaryContentColor text-xl">{{ LayoutConstants.FeedbackAnalysisResult }}</h1>
        </template>

        <!-- Workflow modal content -->
        <template v-slot:content>

            <div class="modal-content-padding modal-content-layout">

            </div>

        </template>

    </ModalBase>

</template>

<script setup lang="ts">

import {defineEmits, type PropType} from 'vue';
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import ModalBase from "@/common/components/modalBase/ModalBase.vue";
import {FeedbackAnalysisResult} from "@/modules/feedbackAnalysisResults/models/FeedbackAnalysisResult";

const props = defineProps({
    result: {
        type: Object as PropType<FeedbackAnalysisResult>,
        required: true
    },
});

const emit = defineEmits<{
    (e: 'closeModal'): void
}>();

// Base workflow drop down states
const onClose = () => {
    emit('closeModal');
}

</script>