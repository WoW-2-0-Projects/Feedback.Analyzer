<template>

    <div class="w-full h-[200px] flex card card-bg card-round card-shadow text-secondaryContentColor">

        <!-- Prompt category details -->
        <div class="flex flex-col items-center justify-center w-full">
            <h5 class="text-xl">{{ promptCategory.typeDisplayName }}</h5>

            <!-- Add prompt button -->
            <div class="flex gap-2">
                <h5 class="mt-5 text-sm"> {{ LayoutConstants.Versions }} : {{ promptCategory.promptsCount }}</h5>

                <app-button :type="ButtonType.Primary" :layout="ButtonLayout.Square" icon="fas fa-plus"
                    :size="ActionComponentSize.Mini" @click="emit('addPrompt', (promptCategory.id))" />
            </div>
        </div>

        <vertical-divider :type="DividerType.ContentLength" />

        <!-- Prompt selection -->
        <div class="w-full p-5 flex flex-col">


            <div class="overflow-y-scroll">
                <table class="table-auto">
                    <thead>
                        <tr>
                            <th class="p-2">Version</th>
                            <th class="p-2">Avg Exc time</th>
                            <th class="p-2">Avg Acc</th>
                            <th class="p-2">Exc count</th>
                            <th class="p-2">Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="executionResult in promptExecutionResults" :key="executionResult.promptId">
                            <td class="p-2">{{ executionResult.version }}.{{ executionResult.revision }}</td>
                            <td class="p-2">{{ executionResult.averageExecutionTime }}</td>
                            <td>{{ executionResult.averageAccuracy }}%</td>
                            <td>{{ executionResult.executionsCount }}%</td>
                            <td>
                                <app-button :type="ButtonType.Danger" :layout="ButtonLayout.Square" icon="fas fa-minus"
                                    :size="ActionComponentSize.Mini"
                                    @click="emit('editPrompt', executionResult.promptId)" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>


        </div>

        <vertical-divider :type="DividerType.ContentLength" />

        <!-- Prompt execution result -->
        <div class="w-full">


        </div>

    </div>

</template>

<script setup lang="ts">

import { onBeforeMount, type PropType, ref } from "vue";
import type { AnalysisPromptCategory } from "@/modules/prompts/models/AnalysisPromptCategory";
import { ButtonType } from "@/common/components/appButton/ButtonType";
import AppButton from "@/common/components/appButton/AppButton.vue";
import { ButtonLayout } from "@/common/components/appButton/ButtonLayout";
import VerticalDivider from "@/common/components/divider/VerticalDivider.vue";
import { DividerType } from "@/common/components/divider/DividerType";
import { DropDownValue } from "@/common/components/formDropDown/DropDownValue";
import { ActionComponentSize } from "@/common/components/formInput/ActionComponentSize";
import { InsightBoxApiClient } from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import { LayoutConstants } from "../../../common/constants/LayoutConstants";
import { Query } from "@/infrastructure/models/query/Query";
import { PromptExecutionResultFilter } from "@/modules/prompts/models/PromptExecutionResultFilter";
import type { PromptExecutionResult } from "@/modules/prompts/models/PromptExecutionResult";

const insightBoxApiClient = new InsightBoxApiClient();

const promptExecutionResultQuery = ref<Query>(new Query(new PromptExecutionResultFilter()));
const promptExecutionResults = ref<Array<PromptExecutionResult>>([]);

const props = defineProps({
    promptCategory: {
        type: Object as PropType<AnalysisPromptCategory>,
        required: true
    }
});

const emit = defineEmits<{
    (e: 'addPrompt', promptCategoryId: string): void,
    (e: 'editPrompt', promptId: string): void,
}>();

onBeforeMount(async () => {
    console.log('test');

    await loadAllPromptVersionResults();
});

const loadAllPromptVersionResults = async () => {
    const response = await insightBoxApiClient.prompts.getPromptExecutionResults(props.promptCategory.id);

    if (response.response) {
        promptExecutionResults.value = response.response;
    }
};

const selectedFilter = ref<DropDownValue | null>(null);
const filterDropDownValues = ref<Array<DropDownValue>>([
    { key: 'All', value: 'All' },
    { key: 'Active', value: 'Active' },
    { key: 'Inactive', value: 'Inactive' }
]);

</script>