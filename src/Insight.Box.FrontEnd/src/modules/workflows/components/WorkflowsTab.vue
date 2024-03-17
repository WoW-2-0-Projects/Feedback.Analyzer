<template>

    <!-- Workflows tab content -->
    <div class="tab pt-10">

        <!-- Workflows search bar -->
        <workflows-search-bar :workflowsQuery="workflowsQuery" @addWorkflow="openWorkflowModal"/>

    </div>

</template>

<script setup lang="ts">

import {onBeforeMount, ref} from "vue";
import WorkflowsSearchBar from "@/modules/workflows/components/WorkflowsSearchBar.vue";
import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import {FeedbackAnalysisWorkflowFilter} from "@/modules/workflows/models/FeedbackAnalysisWorkflowFilter";
import {DocumentService} from "@/infrastructure/services/document/DocumentService";
import {FeedbackAnalysisWorkflow} from "@/modules/workflows/models/FeedbackAnalysisWorkflow";
import {Query} from "@/infrastructure/models/query/Query";
import {LayoutConstants} from "@/common/constants/LayoutConstants";

const insightBoxApiClient = new InsightBoxApiClient();
const documentService = new DocumentService();
const workflows = ref<Array<FeedbackAnalysisWorkflow>>([]);
const workflowsQuery = ref<Query<FeedbackAnalysisWorkflowFilter>>(new Query<FeedbackAnalysisWorkflowFilter>(new FeedbackAnalysisWorkflowFilter()));

onBeforeMount(async () => {
    // Set page title
    documentService.setTitle(LayoutConstants.Workflows);

    // Load workflows
    await loadWorkflowsAsync();
});

const loadWorkflowsAsync = async () => {
    const response = await insightBoxApiClient.workflows.getAsync(workflowsQuery.value);
    if(response.isSuccess) {
        workflows.value.push(...response.response);
    }
};

const openWorkflowModal = () => {};

</script>
