<template>

    <div class="tab pt-10">

        <!-- Prompts search bar -->
        <prompts-search-bar :promptsQuery="promptsCategoryQuery"/>

        <!-- Prompts gallery-->

        <!--Prompts card-->
        <prompt-category-card v-for="promptCategory in promptCategories" :key="promptCategory.id"
                              :promptCategory="promptCategory" :workflows="trainingWorkflows"
                              @addPrompt="(categoryId, loadFunc) => openPromptModal(null, categoryId, loadFunc)"
                              @editPrompt="(promptId, loadFunc) => openPromptModal(promptId,null, loadFunc)"
        />

        <!-- Prompts create / edit modal -->
        <prompt-modal :isActive="promptModalActive" @closeModal="closePromptModal"
                      @submit="onPromptModalSubmit($event)" :isCreate="isCreate" :prompt="editingPrompt"
        />

    </div>

</template>

<script setup lang="ts">

import {onBeforeMount, ref} from "vue";
import {Query} from "@/infrastructure/models/query/Query";
import {PromptCategoryFilter} from "@/modules/prompts/models/PromptCategoryFilter";
import PromptsSearchBar from "@/modules/prompts/components/PromptsSearchBar.vue";
import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import {DocumentService} from "@/infrastructure/services/document/DocumentService";
import {AnalysisPromptCategory} from "@/modules/prompts/models/AnalysisPromptCategory";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import PromptCategoryCard from "@/modules/prompts/components/PromptCategoryCard.vue";
import {AnalysisPrompt} from "@/modules/prompts/models/AnalysisPrompt";
import {CreatePromptCommand} from "@/modules/prompts/models/CreatePromptCommand";
import PromptModal from "@/modules/prompts/components/PromptModal.vue";
import {FeedbackAnalysisWorkflowFilter} from "@/modules/workflows/models/FeedbackAnalysisWorkflowFilter";
import type {FeedbackAnalysisWorkflow} from "@/modules/workflows/models/FeedbackAnalysisWorkflow";
import type {AsyncFunction} from "@/infrastructure/models/delegates/AsyncFunction";
import {WorkflowType} from "@/modules/workflows/models/WorkflowType";

//Services
const insightBoxApiClient = new InsightBoxApiClient();
const documentService = new DocumentService

// States
const isLoading = ref<boolean>(false)

// Category card states
const promptsCategoryQuery = ref<Query<PromptCategoryFilter>>(new Query<PromptCategoryFilter>(new
PromptCategoryFilter()));
const promptCategories = ref<Array<AnalysisPromptCategory>>([]);
const noPromptCategoriesFound = ref<boolean>(false);

// Prompt modal states
const promptModalActive = ref<boolean>(false);

const isCreate = ref<boolean>(true);
const editingPrompt = ref<AnalysisPrompt>(new AnalysisPrompt());
const editingPromptLoadResultFunction = ref<AsyncFunction>();

// Workflow states
const workflowQuery = ref<Query<FeedbackAnalysisWorkflowFilter>>(new Query<FeedbackAnalysisWorkflowFilter>(new
FeedbackAnalysisWorkflowFilter(WorkflowType.Training)));
const trainingWorkflows = ref<Array<FeedbackAnalysisWorkflow>>([])

// Search bar states
const isSearchBarLoading = ref<boolean>(false);

onBeforeMount(async () => {
    // Set page title
    documentService.setTitle(LayoutConstants.Prompts);

    await loadPromptCategoriesAsync();
    await loadWorkflowsAsync();
});

/*
 * Loads prompt categories
 */
const loadPromptCategoriesAsync = async () => {
    isLoading.value = true;

    const response = await insightBoxApiClient.prompts.getCategoriesAsync(promptsCategoryQuery.value);
    if (response.response) {
        promptCategories.value.push(...response.response);
    } else if (response.status == 404 || response.status == 204) {
        noPromptCategoriesFound.value = true;
    }

    isLoading.value = false;
}

const loadWorkflowsAsync = async () => {
    isLoading.value = true;

    const response = await insightBoxApiClient.workflows.getAsync(workflowQuery.value)

    if (response.response)
        trainingWorkflows.value.push(...response.response)

    isLoading.value = false;
}

/*
 * Handles prompt modal submit
 */
const onPromptModalSubmit = async (prompt: AnalysisPrompt) => {
    if (isCreate.value) {
        await createPromptAsync(prompt);
    } else {
        await updatePromptAsync(prompt);
    }
}

/*
 * Creates a prompt
 */
const createPromptAsync = async (prompt: AnalysisPrompt) => {
    isSearchBarLoading.value = true;

    const createPromptCommand = new CreatePromptCommand(prompt);

    const response = await
        insightBoxApiClient.prompts.createAsync(createPromptCommand);

    if (response.response) {
        if (editingPromptLoadResultFunction?.value && editingPromptLoadResultFunction?.value?.callback)
            editingPromptLoadResultFunction.value?.callback();
    }

    isSearchBarLoading.value = false;
};

/*
 * Updates prompt
 */
const updatePromptAsync = async (prompt: AnalysisPrompt) => {
    isSearchBarLoading.value = true;

    // prompt.organizationId = '60e6a4de-31e5-4f8b-8e6a-0a8f63f41527';
    const createPromptCommand = new CreatePromptCommand(prompt);

    const response = await
        insightBoxApiClient.prompts.updateAsync(createPromptCommand);

    if (response.response) {
        if (editingPromptLoadResultFunction?.value && editingPromptLoadResultFunction?.value?.callback) {
            editingPromptLoadResultFunction.value?.callback();
        }
    }

    isSearchBarLoading.value = false;
};

/**
 * open the modal
 * @param promptId
 * @param promptCategoryId
 * @param loadPromptResultCallback
 */
const openPromptModal = async (
    promptId: string | null,
    promptCategoryId: string | null,
    loadPromptResultCallback: AsyncFunction
) => {
    if (promptId) {
        editingPromptLoadResultFunction.value = loadPromptResultCallback;
        const response = await insightBoxApiClient.prompts.getByIdAsync(promptId);

        if (response.isSuccess) {
            editingPrompt.value = response.response!;
            isCreate.value = false;
            promptModalActive.value = true;
        }
    } else {
        editingPromptLoadResultFunction.value = loadPromptResultCallback;
        editingPrompt.value = new AnalysisPrompt();
        editingPrompt.value.categoryId = promptCategoryId!;
        isCreate.value = true;
        promptModalActive.value = true;

    }
}

/**
 * close the modal
 */
const closePromptModal = () => {
    promptModalActive.value = false;
}

const onScroll = async () => {

}
</script>