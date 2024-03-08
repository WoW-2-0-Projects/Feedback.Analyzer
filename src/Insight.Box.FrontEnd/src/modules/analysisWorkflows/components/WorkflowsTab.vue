<template>

    <!-- Workflows tab content -->
    <div class="tab pt-10">

        <!-- Workflows search bar -->
        <workflows-search-bar/>

        <!-- Workflows gallery -->
        <infinite-scroll @onScroll="onScroll"
                         :contentChangeSource="workflowsChangeSource"
                         class="grid w-full px-20 gap-x-5 gap-y-10 grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 theme-bg-primary">

            <!-- Listing card -->
        </infinite-scroll>

        <!-- Workflows gallery -->
        <div class="mt-4  flex flex-wrap justify-center gap-5">
            <workflow-card v-for="workflow in workflows" :workflow="workflow" :key="workflow.id" />
        </div>
    </div>

</template>

<script setup lang="ts">

import {onBeforeMount, ref} from "vue";
import {DocumentService} from "@/infrastructure/services/document/DocumentService";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import InfiniteScroll from "@/common/components/infiniteScroll/InfiniteScroll.vue";
import {NotificationSource} from "@/infrastructure/models/notifications/Action";
import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import {Query} from "@/infrastructure/models/query/Query";
import WorkflowsSearchBar from "@/modules/analysisWorkflows/components/WorkflowsSearchBar.vue";
import {AnalysisWorkflowFilter} from "@/modules/prompts/models/AnalysisWorkflowFilter";
import type {Organization} from "@/modules/organizations/models/Organization";
import WorkflowCard from "@/modules/analysisWorkflows/components/WorkflowCard.vue";
import {WorkflowType} from "@/modules/prompts/models/WorkflowType";

const insightBoxApiClient = new InsightBoxApiClient();
const documentService = new DocumentService();
const workflows = ref<Array<Organization>>([]);
const workflowsQuery = ref<Query<AnalysisWorkflowFilter>>(new Query<AnalysisWorkflowFilter>(new
AnalysisWorkflowFilter()));

onBeforeMount(async () => {
    // Set page title
    workflowsQuery.value.filter.type = WorkflowType.Training;
    documentService.setTitle(LayoutConstants.Workflows);
    await loadWorkflowsAsync();
});

const loadWorkflowsAsync = async () => {
    const response = await insightBoxApiClient.workflows.getAsync(workflowsQuery.value);
    if(response.isSuccess) {
        workflows.value.push(...response.response);
    }
};

const workflowsChangeSource = ref<NotificationSource>(new NotificationSource());

const onScroll = async () => {
};

</script>
