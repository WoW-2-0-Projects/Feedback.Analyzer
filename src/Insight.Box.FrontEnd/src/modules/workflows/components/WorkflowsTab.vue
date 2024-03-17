<template>

    <!-- Workflows tab content -->
    <div class="tab pt-10">

        <!-- Workflows search bar -->
        <workflows-search-bar :workflowsQuery="workflowsQuery" @addWorkflow="openWorkflowModal"/>

        <!-- Workflow modal -->
        <workflow-modal :isActive="isWorkflowModalActive" :isCreate="isWorkflowCreate"
                        :workflow="workflow" :workflows="workflows" :products="products"
                        @closeModal="isWorkflowModalActive = false" @submit="onWorkflowModalSubmit"/>

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
import WorkflowModal from "@/modules/workflows/components/WorkflowModal.vue";
import {CreateFeedbackAnalysisWorkflowCommand} from "@/modules/workflows/models/CreateFeedbackAnalysisWorkflowCommand";
import type {Product} from "@/modules/products/models/Product";
import {ProductFilter} from "@/modules/products/models/ProductFilter";
import {UpdateFeedbackAnalysisWorkflowCommand} from "@/modules/workflows/models/UpdateFeedbackAnalysisWorkflowCommand";

const insightBoxApiClient = new InsightBoxApiClient();
const documentService = new DocumentService();
const workflowsQuery = ref<Query<FeedbackAnalysisWorkflowFilter>>(new Query<FeedbackAnalysisWorkflowFilter>(new FeedbackAnalysisWorkflowFilter()));
const workflows = ref<Array<FeedbackAnalysisWorkflow>>([]);
const productsQuery = ref<Query<Product>>(new Query<ProductFilter>(new ProductFilter()));

const products = ref<Array<Product>>([]);

// Searchbar states
const isSearchBarLoading = ref<boolean>(false);

// Workflow modal states
const isWorkflowModalActive = ref<boolean>(false);
const isWorkflowCreate = ref<boolean>(true);
const workflow = ref<FeedbackAnalysisWorkflow>(new FeedbackAnalysisWorkflow());

onBeforeMount(async () => {
    // Set page title
    documentService.setTitle(LayoutConstants.Workflows);

    // Load workflows
    await loadWorkflowsAsync();
    await loadProductsAsync();
});

const loadWorkflowsAsync = async () => {
    const response = await insightBoxApiClient.workflows.getAsync(workflowsQuery.value);
    if(response.isSuccess) {
        workflows.value = response.response;
    }
};

const loadProductsAsync = async () => {
    const response = await insightBoxApiClient.products.getAsync(productsQuery.value);
    if(response.isSuccess) {
        products.value = response.response;
    }
};

const openWorkflowModal = (workflowToEdit: FeedbackAnalysisWorkflow | null) => {
    if(workflowToEdit) {
        workflow.value = workflowToEdit;
        isWorkflowCreate.value = false;
        isWorkflowModalActive.value = true;
    } else {
        workflow.value = new FeedbackAnalysisWorkflow();
        isWorkflowCreate.value = true;
        isWorkflowModalActive.value = true;
    }
};

const onWorkflowModalSubmit = async (workflow: FeedbackAnalysisWorkflow, baseWorkflow: FeedbackAnalysisWorkflow) => {
    if (isWorkflowCreate.value) {
        await createWorkflowAsync(workflow, baseWorkflow);
    } else {
        await updateWorkflowAsync(workflow);
    }
}

/*
 * Creates a prompt
 */
const createWorkflowAsync = async (workflow: FeedbackAnalysisWorkflow, baseWorkflow: FeedbackAnalysisWorkflow) => {
    isSearchBarLoading.value = true;

    const createWorkflowCommand = new CreateFeedbackAnalysisWorkflowCommand(workflow, baseWorkflow.id);
    const response = await
        insightBoxApiClient.workflows.createAsync(createWorkflowCommand);

    if (response.response) {
        workflows.value.push(response.response);
    }

    isSearchBarLoading.value = false;
};

/*
 * Updates prompt
 */
const updateWorkflowAsync = async (workflow: FeedbackAnalysisWorkflow) => {
    isSearchBarLoading.value = true;

    // prompt.organizationId = '60e6a4de-31e5-4f8b-8e6a-0a8f63f41527';
    const updateWorkflowCommand = new UpdateFeedbackAnalysisWorkflowCommand(workflow);

    const response = await
        insightBoxApiClient.workflows.updateAsync(updateWorkflowCommand);

    if (response.response) {
        const workflowIndex = workflows.value.findIndex(w => w.id === response.response.id);
        if (workflowIndex !== -1) {
            workflows.value[workflowIndex] = response.response;
        } else
            workflows.value.push(response.response);
    }

    isSearchBarLoading.value = false;
};


</script>
