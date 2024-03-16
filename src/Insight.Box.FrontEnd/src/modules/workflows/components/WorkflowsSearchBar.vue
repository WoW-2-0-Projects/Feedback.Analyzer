<template>

    <form class="w-full flex gap-10">

        <!-- Add workflow button -->
        <app-button :type="ButtonType.Primary" :text="LayoutConstants.AddWorkflow" icon="fas fa-plus"
                    @click="emit('addWorkflow')"/>

        <!-- Search workflows input -->
        <form-search-bar v-model="workflowsQuery.filter.searchKeyword" class="flex-grow"
                         :label="LayoutConstants.Search" :placeholder="LayoutConstants.SearchWorkflows"
        />

    </form>

</template>

<script setup lang="ts">

import {defineEmits, type PropType} from "vue";
import FormSearchBar from "@/common/components/formSearchBar/FormSearchBar.vue";
import type {Query} from "@/infrastructure/models/query/Query";
import type {FeedbackAnalysisWorkflowFilter} from "@/modules/workflows/models/FeedbackAnalysisWorkflowFilter";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import {ButtonType} from "@/common/components/appButton/ButtonType";
import AppButton from "@/common/components/appButton/AppButton.vue";

const props = defineProps({
    workflowsQuery: {
        type: Object as PropType<Query<FeedbackAnalysisWorkflowFilter>>,
        required: true
    },
    isLoading: {
        type: Boolean,
        default: false
    }
});

const emit = defineEmits<{
    (e: 'addWorkflow'): void
}>();

</script>