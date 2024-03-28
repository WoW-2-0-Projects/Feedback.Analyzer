<template>

    <div class="tab pt-10">

        <!-- Prompts search bar -->
        <prompts-search-bar :promptsQuery="promptsCategoryQuery"/>

        <!-- Prompts gallery-->
        <infinite-scroll @onScroll ="onScroll"
                         :contentchangesource ="promptChangeSource"
                         class="mt-10 flex flex-col gap-y-5">
            <!--Prompts card-->
            <prompt-category-card v-for="promptCategory in promptCategories" :key="promptCategory.id"
                                  :promptCategory="promptCategory" :workflows="trainingWorkflows"
                                  @addPrompt="(categoryId, loadFunc) => openPromptModal(null, categoryId, loadFunc)"
                                  @editPrompt="(promptId, loadFunc) => openPromptModal(promptId,null, loadFunc)"
                                  @loadCategory="categoryId => loadCategoryAsync(categoryId)"
                                  @openHistory="onOpenHistoryModal"
            />

        </infinite-scroll>

        <!-- Prompts create / edit modal -->
        <prompt-modal :isActive="promptModalActive" @closeModal="closePromptModal"
                      @submit="onPromptModalSubmit($event)" :isCreate="isCreate" :prompt="editingPrompt"
        />

        <!-- Prompt execution history modal -->
        <prompt-execution-history-modal :history="openedHistory"
                                        :isActive="historyModalActive"
                                        @closeModal="historyModalActive = false"
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
import InfiniteScroll from "@/common/components/infiniteScroll/InfiniteScroll.vue";
import {NotificationSource} from "@/infrastructure/models/notifications/Action";
import PromptCategoryCard from "@/modules/prompts/components/PromptCategoryCard.vue";
import {FeedbackAnalysisWorkflow} from "@/modules/prompts/models/FeedbackAnalysisWorkflow";
import {AnalysisWorkflowFilter} from "@/modules/prompts/models/AnalysisWorkflowFilter";
import {WorkflowType} from "@/modules/prompts/models/WorkflowType";
import  {AsyncFunction} from "@/infrastructure/models/notifications/Function";
import {AnalysisPrompt} from "@/modules/prompts/models/AnalysisPrompt";
import {CreatePromptCommand} from "@/modules/prompts/models/CreatePromptCommand";
import PromptModal from "@/modules/prompts/components/PromptModal.vue";
import type {PromptsExecutionHistory} from "@/modules/prompts/models/PromptExecutionHistory";
import PromptExecutionHistoryModal from "@/modules/prompts/components/PromptExecutionHistoryModal.vue";



//Services
const insightBoxApiClient = new InsightBoxApiClient();
const documentService = new DocumentService


// States
const isLoading = ref<boolean>(false)

// Category card states
const promptsCategoryQuery = ref<Query>(new Query(new PromptCategoryFilter()));
const promptCategories = ref<Array<AnalysisPromptCategory>>([]);
const noPromptCategoriesFound = ref<boolean>(false);

//Infinite scroll states
const  promptChangeSource = ref<NotificationSource<boolean>>(new NotificationSource<boolean>());

// Prompt modal states
const promptModalActive = ref<boolean>(false);

const isCreate = ref<boolean>(true);
const editingPrompt = ref<AnalysisPrompt>(new AnalysisPrompt());
const editingPromptLoadResultFunction = ref<AsyncFunction<string>>();

// Workflow states
const workflowQuery = ref<Query<AnalysisWorkflowFilter>>(new Query<AnalysisWorkflowFilter>(new
AnalysisWorkflowFilter(WorkflowType.Training)));
const  trainingWorkflows = ref<Array<FeedbackAnalysisWorkflow>>([])

// Prompt execution history modal
const historyModalActive = ref<boolean>(false);
const openedHistory = ref<PromptsExecutionHistory | null>(null);

// Search bar states
const isSearchBarLoading = ref<boolean>(false);

onBeforeMount( async () => {
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

    const  response = await insightBoxApiClient.prompts.getCategoriesAsync(promptsCategoryQuery.value);
    if(response.response) {
        promptCategories.value.push(...response.response);
    } else if( response.status == 404 || response.status == 204){
        noPromptCategoriesFound.value = true;
    }

    isLoading.value = false;
}

const loadCategoryAsync = async (categoryId: string) => {
    const response = await insightBoxApiClient.prompts.getCategoryByIdAsync(categoryId);

    if (response.response) {
        let categoryIndex = promptCategories.value.findIndex(c => c.id === categoryId);
        if (categoryIndex !== -1) {
            promptCategories.value[categoryIndex] = response.response;
        } else
            promptCategories.value.push(response.response);
    }
}

const loadWorkflowsAsync = async () => {
    isLoading.value = true;

    const response = await insightBoxApiClient.workflows.getAsync(workflowQuery.value)

    if(response.response)
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
        if (editingPromptLoadResultFunction?.value && editingPromptLoadResultFunction?.value?.callBack)
            editingPromptLoadResultFunction.value?.callBack(response.response.id);
    }

    isSearchBarLoading.value = false;
};

/*
 * Updates prompt
 */
const updatePromptAsync = async (prompt: AnalysisPrompt) => {
    isSearchBarLoading.value = true;

    const createPromptCommand = new CreatePromptCommand(prompt);

    const response = await
        insightBoxApiClient.prompts.updateAsync(createPromptCommand);

    if (response.response) {
        if (editingPromptLoadResultFunction?.value && editingPromptLoadResultFunction?.value?.callBack) {
            editingPromptLoadResultFunction.value?.callBack(response.response.id);
        }
    }

    isSearchBarLoading.value = false;
};

/**
 * open the modal
 */
const openPromptModal = async (
    promptId: string | null,
    promptCategoryId: string | null,
    loadPromptResultCallback: AsyncFunction<string>
) => {
    if(promptId){
        editingPromptLoadResultFunction.value = loadPromptResultCallback;
        const response = await insightBoxApiClient.prompts.getByIdAsync(promptId);

        if(response.isSuccess) {
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

const  onOpenHistoryModal = (history: PromptsExecutionHistory) => {
   openedHistory.value = history;
   historyModalActive.value = true;
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