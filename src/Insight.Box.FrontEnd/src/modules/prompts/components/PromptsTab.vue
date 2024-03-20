<template>

    <div class="tab pt-10">

        <!-- Prompts search bar -->
        <prompts-search-bar :promptsQuery="promptsCategoryQuery"/>

    </div>

</template>

<script setup lang="ts">


import {onBeforeMount, ref} from "vue";
import {Query} from "@/infrastructure/models/query/Query";
import {PromptCategoryFilter} from "@/modules/prompts/models/PromptCategoryFilter";
import PromptsSearchBar from "@/modules/prompts/components/PromptsSearchBar.vue";
import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import {DocumentService} from "@/infrastructure/services/document/DocumentService";
import type {AnalysisPromptCategory} from "@/modules/prompts/models/AnalysisPromptCategory";
import {LayoutConstants} from "@/common/constants/LayoutConstants";

//Services
const insightBoxApiClient = new InsightBoxApiClient();
const documentService = new DocumentService


// States
const isLoading = ref<boolean>(false)

// Category card states
const promptsCategoryQuery = ref<Query>(new Query(new PromptCategoryFilter()));
const promptCategories = ref<Array<AnalysisPromptCategory>>([]);
const noPromptCategoriesFound = ref<boolean>(false);

onBeforeMount( async () => {
    // Set page title
    documentService.setTitle(LayoutConstants.Prompts);

    await loadPromptCategoriesAsync();
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
</script>