<template>

    <div class="w-full h-[400px] flex card card-bg card-round card-shadow text-secondaryContentColor">

        <!-- Prompt category details -->
        <div class="flex flex-col items-center justify-center w-full">
            <h5 class="text-xl">{{ promptCategory.typeDisplayName }}</h5>

            <!-- Add prompt button -->
            <div class="flex gap-2 items-center justify-center">
                <h5 class="mt-5 text-sm"> {{ LayoutConstants.Versions }} : {{ promptCategory.promptsCount }}</h5>

                <app-button :type="ButtonType.Primary" :layout="ButtonLayout.Rectangle" icon="fas fa-plus"
                            text="Add Prompt"
                            :size="ActionComponentSize.ExtraSmall" @click="emit('addPrompt', (promptCategory.id))"/>
            </div>

            <!-- Training workflows -->
            <div class="mt-20">
                <h5 class="text-center">Training workflows</h5>
                <form-drop-down label="Filter by" model-value="selectedFilter" values="filterDropDownValues"
                                :size="ActionComponentSize.Small"/>
            </div>

        </div>

        <vertical-divider :type="DividerType.ContentLength"/>

        <!-- Prompt selection -->
        <div class="w-full p-2 py-5 flex flex-col item-center">

            <h5 class="text-center">Prompts</h5>
            <div class="overflow-y-scroll no-scrollbar">
                <app-table :data="promptResultsTableData"/>
            </div>

        </div>

        <vertical-divider :type="DividerType.ContentLength"/>

        <!-- Prompt execution result -->
        <div class="w-full p-2 py-5 flex flex-col">

            <h5 class="text-center">Execution histories</h5>
            <div class="overflow-y-scroll no-scrollbar">
                <app-table :data="promptResultsTableData"/>
            </div>

        </div>

    </div>

</template>

<script setup lang="ts">

import {onBeforeMount, type PropType, ref} from "vue";
import type {AnalysisPromptCategory} from "@/modules/prompts/models/AnalysisPromptCategory";
import {ButtonType} from "@/common/components/appButton/ButtonType";
import AppButton from "@/common/components/appButton/AppButton.vue";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import VerticalDivider from "@/common/components/divider/VerticalDivider.vue";
import {DividerType} from "@/common/components/divider/DividerType";
import {DropDownValue} from "@/common/components/formDropDown/DropDownValue";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import {Query} from "@/infrastructure/models/query/Query";
import {PromptExecutionResultFilter} from "@/modules/prompts/models/PromptExecutionResultFilter";
import type {PromptExecutionResult} from "@/modules/prompts/models/PromptExecutionResult";
import AppTable from "@/common/components/appTable/AppTable.vue";
import {TableData} from "@/common/components/appTable/TableData";
import {LayoutConstants} from "../../../common/constants/LayoutConstants";
import {TableRowData} from "@/common/components/appTable/TableRowData";
import {TableAction} from "@/common/components/appTable/TableAction";
import FormDropDown from "@/common/components/formDropDown/FormDropDown.vue";

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
    (e: 'addPrompt', promptCategoryId: string): void
    (e: 'editPrompt', promptId: string): void
}>();

const promptResultsTableData = ref<TableData>(new TableData([
        "V",
        "AET",
        "AA",
        "EC",
        "Actions",
    ],[]
));

const promptHistoriesTableData = ref<TableData>(new TableData([
        "",
        "AET",
        "AA",
        "EC",
        "Actions",
    ],[]
));

onBeforeMount(async () => {
    await loadAllPromptVersionResults();
});

const loadAllPromptVersionResults = async () => {
    const response = await insightBoxApiClient.prompts.getPromptExecutionResults(props.promptCategory.id);

    if (response.response) {
        promptExecutionResults.value = response.response;
        promptResultsTableData.value.rows = promptExecutionResults.value.map(result => convertResultToTableRowData(result));
    }
};

const convertResultToTableRowData = (result: PromptExecutionResult) => {
    return new TableRowData([
            `${result.version}.${result.revision}`,
            result.averageExecutionTime,
            result.averageAccuracy,
            result.executionsCount,  // Comma added here
        ],
        [
            new TableAction(() => emit('editPrompt', result.promptId), ButtonType.Secondary, 'fas fa-edit')
        ]
    );
};

// const convertResultToTableRowData = (result: PromptExecutionResult) => {
//     return new TableRowData([
//         $`{result.version}.${result.revision}`,
//         result.averageExecutionTime,
//         result.averageAccuracy,
//         result.executionsCount
//         ],
//         [
//             new TableAction(() => emit('editPrompt', result.promptId))
//         ]
//     ]
// };

const selectedFilter = ref<DropDownValue | null>(null);
const filterDropDownValues = ref<Array<DropDownValue>>([
    {key: 'All', value: 'All'},
    {key: 'Active', value: 'Active'},
    {key: 'Inactive', value: 'Inactive'}
]);

</script>