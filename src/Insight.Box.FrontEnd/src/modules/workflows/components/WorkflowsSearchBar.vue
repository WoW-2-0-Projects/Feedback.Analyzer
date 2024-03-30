<template>

    <form class="w-full flex gap-10">

        <!-- Add workflow button -->
        <app-button :type="ActionType.Primary" :text="LayoutConstants.AddWorkflow" icon="fas fa-plus"
                    @click="emit('addWorkflow')"/>

        <!-- Search workflows input -->
        <form-search-bar v-model="workflowsQuery.filter.searchKeyword" class="flex-grow"
                         :label="LayoutConstants.Search" :placeholder="LayoutConstants.SearchWorkflows"
        />

    </form>

</template>

<script setup lang="ts">

import {type PropType} from "vue";
import FormSearchBar from "@/common/components/formSearchBar/FormSearchBar.vue";
import type {Query} from "@/infrastructure/models/query/Query";
import type {FeedbackAnalysisWorkflowFilter} from "@/modules/workflows/models/FeedbackAnalysisWorkflowFilter";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import AppButton from "@/common/components/appButton/AppButton.vue";
import {ActionType} from "@/common/components/actions/ActionType";

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