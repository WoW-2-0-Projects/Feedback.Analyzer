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
            />

        </infinite-scroll>

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

// Workflow states
const workflowQuery = ref<Query<AnalysisWorkflowFilter>>(new Query<AnalysisWorkflowFilter>(new
AnalysisWorkflowFilter(WorkflowType.Training)));
const  trainingWorkflows = ref<Array<FeedbackAnalysisWorkflow>>([])

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

const loadWorkflowsAsync = async () => {
    isLoading.value = true;

    const response = await insightBoxApiClient.workflows.getAsync(workflowQuery.value)

    if(response.response)
        trainingWorkflows.value.push(...response.response)

    isLoading.value = false;
}

const onScroll = async () => {

}
</script>