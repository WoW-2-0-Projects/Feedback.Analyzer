<template>

    <form class="w-full flex gap-10">

        <!-- Search Prompts input -->
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

</script>