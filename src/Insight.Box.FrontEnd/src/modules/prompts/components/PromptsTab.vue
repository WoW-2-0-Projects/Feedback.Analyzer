<template>

    <!-- Prompts tab content -->
    <div class="tab pt-10">

        <!-- Prompts search bar -->
        <prompts-search-bar :promptsQuery="promptsQuery" @addPrompt="openPromptModal"/>

        <!-- Prompts gallery -->
        <infinite-scroll @onScroll="onScroll"
                         :contentChangeSource="promptsChangeSource"
                         class="grid w-full px-20 gap-x-5 gap-y-10 grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 theme-bg-primary">

            <!-- Prompts card -->

        </infinite-scroll>

        <!-- Prompts create / edit modal -->
        <prompt-modal :isActive="promptModalActive" @closeModal="closePromptModal"
                      @submit="createPromptAsync($event)" :isCreate="isCreate"
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
import type {FeedbackAnalysisPrompt} from "@/modules/prompts/models/AnalysisPrompt";
import {CreatePromptCommand} from "@/modules/prompts/models/CreatePromptCommand";

/* Services */
const insightBoxApiClient = new InsightBoxApiClient();
const documentService = new DocumentService();

/* States */
const isLoading = ref<boolean>(false);
const promptsQuery = ref<Query>(new Query(new PromptFilter()));
const prompts = ref<Array<FeedbackAnalysisPrompt>>([]);
const noPromptsFound = ref<boolean>(false);
const promptsChangeSource = ref<NotificationSource>(new NotificationSource());
const promptModalActive = ref<boolean>(false);

/* Search bar states */
const isSearchBarLoading = ref<boolean>(false);
const isCreate = ref<boolean>(true);

onBeforeMount(async () => {
    // Set page title
    documentService.setTitle(LayoutConstants.Prompts);

    // Load prompts
    await loadPromptsAsync();
});

/*
 * Loads prompts
 */
const loadPromptsAsync = async () => {
    isLoading.value = true;

    const response = await insightBoxApiClient.products.getAsync(promptsQuery.value);

    if (response.response) {
        prompts.value.push(...response.response);
    } else if (response.status == 404 || response.status == 204) {
        noPromptsFound.value = true;
    }

    isLoading.value = false;
};

/*
 * Handles prompt modal submit
 */
const onPromptModalSubmit = async(prompt: FeedbackAnalysisPrompt) => {
    if(isCreate.value) {
        await createPromptAsync(prompt);
    } else {
        await updatePromptAsync(prompt);
    }
}

/*
 * Creates a prompt
 */
const createPromptAsync = async (prompt: FeedbackAnalysisPrompt) => {
    isSearchBarLoading.value = true;

    prompt.organizationId = '60e6a4de-31e5-4f8b-8e6a-0a8f63f41527';
    const createPromptCommand = new CreatePromptCommand(prompt);
    const response = await
        insightBoxApiClient.products.createAsync(createPromptCommand);

    if (response.response) {
        prompts.value.push(response.response);
    }

    isSearchBarLoading.value = false;
};

/*
 * Updates prompt
 */
const updatePromptAsync = async (prompt: FeedbackAnalysisPrompt) => {
    isSearchBarLoading.value = true;

    isSearchBarLoading.value = false;
};

const openPromptModal = (prompt: FeedbackAnalysisPrompt | null) => {
    isCreate.value = prompt === null || prompt === undefined;
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