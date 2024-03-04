<template>

    <!-- Prompts tab content -->
    <div class="tab pt-10">

        <!-- Prompts search bar -->
        <prompts-search-bar :promptsQuery="promptsCategoryQuery"/>

        <!-- Prompts gallery -->
        <infinite-scroll @onScroll="onScroll"
                         :contentChangeSource="promptsChangeSource"
                         class="mt-10 flex flex-col gap-y-5">

            <!-- Prompts card -->
            <prompt-category-accordion-card v-for="promptCategory in promptCategories" :key="promptCategory.id"
                                            :promptCategory="promptCategory"
                                            @addPrompt="categoryId => openPromptModal(null, categoryId)"
                                            @editPrompt="promptId=> openPromptModal(promptId, null)"
            />

        </infinite-scroll>

        <!-- Prompts create / edit modal -->
        <prompt-modal :isActive="promptModalActive" @closeModal="closePromptModal"
                      @submit="onPromptModalSubmit($event)" :isCreate="isCreate" :prompt="editingPrompt"
        />

    </div>

</template>

<script setup lang="ts">

import {onBeforeMount, ref} from "vue";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import InfiniteScroll from "@/common/components/infiniteScroll/InfiniteScroll.vue";
import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import {DocumentService} from "@/infrastructure/services/document/DocumentService";
import {NotificationSource} from "@/infrastructure/models/notifications/Action";
import {Query} from "@/infrastructure/models/query/Query";
import PromptModal from "@/modules/prompts/components/PromptModal.vue";
import PromptsSearchBar from "@/modules/prompts/components/PromptsSearchBar.vue";
import {PromptFilter} from "@/modules/prompts/models/PromptFilter";
import {AnalysisPrompt} from "@/modules/prompts/models/AnalysisPrompt";
import {CreatePromptCommand} from "@/modules/prompts/models/CreatePromptCommand";
import PromptCategoryAccordionCard from "@/modules/prompts/components/PromptCategoryAccordionCard.vue";
import {PromptCategoryFilter} from "@/modules/prompts/models/PromptCategoryFilter";
import type {AnalysisPromptCategory} from "@/modules/prompts/models/AnalysisPromptCategory";

/* Services */
const insightBoxApiClient = new InsightBoxApiClient();
const documentService = new DocumentService();

/* States */
const isLoading = ref<boolean>(false);
const promptsQuery = ref<Query>(new Query(new PromptFilter()));
const promptsCategoryQuery = ref<Query>(new Query(new PromptCategoryFilter()));
const prompts = ref<Array<AnalysisPrompt>>([]);
const promptCategories = ref<Array<AnalysisPromptCategory>>([]);
const noPromptCategoriesFound = ref<boolean>(false);

/* Infinite scroll states */
const promptsChangeSource = ref<NotificationSource>(new NotificationSource());

/* Prompt modal states */
const promptModalActive = ref<boolean>(false);
const isCreate = ref<boolean>(true);
const editingPrompt = ref<AnalysisPrompt>(new AnalysisPrompt());

/* Search bar states */
const isSearchBarLoading = ref<boolean>(false);

onBeforeMount(async () => {
    // Set page title
    documentService.setTitle(LayoutConstants.Prompts);

    // Load prompt categories
    await loadPromptCategoriesAsync();
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
};

/*
 * Loads prompts
 */
const loadPromptsAsync = async (categoryId: string) => {
    isLoading.value = true;

    const response = await insightBoxApiClient.prompts.getAsync(promptsQuery.value);

    if (response.response) {
        promptCategories.value.find(category => category.id === categoryId)?.prompts.push(...response.response);
    }

    isLoading.value = false;
};

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
        prompts.value.push(response.response);
    }

    isSearchBarLoading.value = false;
};

/*
 * Updates prompt
 */
const updatePromptAsync = async (prompt: AnalysisPrompt) => {
    isSearchBarLoading.value = true;

    prompt.organizationId = '60e6a4de-31e5-4f8b-8e6a-0a8f63f41527';
    const createPromptCommand = new CreatePromptCommand(prompt);

    const response = await
        insightBoxApiClient.prompts.updateAsync(createPromptCommand);

    if (response.response) {
        prompts.value.push(response.response);
    }

    isSearchBarLoading.value = false;
};

const openPromptModal = async(promptId: string | null, promptCategoryId: string | null) => {

    if(promptId) {
        const response = await insightBoxApiClient.prompts.getByIdAsync(promptId!);

        if (response.isSuccess){
            editingPrompt.value = response.response!;
        }

        isCreate.value = false;
    }
    else {
        editingPrompt.value = new AnalysisPrompt();
        editingPrompt.value.categoryId = promptCategoryId!;
        isCreate.value = true;
    }

    promptModalActive.value = true;
};

const closePromptModal = () => {
    promptModalActive.value = false;
};


/*
 * Scroll event handler
 */
const onScroll = async () => {
};


</script>