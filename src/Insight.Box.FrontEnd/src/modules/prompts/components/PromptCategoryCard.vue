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
                <form-drop-down label="Filter by" v-model="selectedWorkflow" :values="workflowDropDownValues"
                                :size="ActionComponentSize.Small"/>
            </div>

            <app-button :type="ButtonType.Success" :layout="ButtonLayout.Rectangle" icon="fas fa-play"
                        text="Trigger" :disabled="selectedWorkflow === null || promptCategory.selectedPromptId === null"
                        :size="ActionComponentSize.ExtraSmall" @click="onTriggerWorkflow"/>

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
                <app-table :data="promptHistoriesTableData"/>
            </div>

        </div>

    </div>

</template>

<script setup lang="ts">

import {onBeforeMount, type PropType, ref, watch} from "vue";
import type {AnalysisPromptCategory} from "@/modules/prompts/models/AnalysisPromptCategory";
import {ButtonType} from "@/common/components/appButton/ButtonType";
import AppButton from "@/common/components/appButton/AppButton.vue";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import VerticalDivider from "@/common/components/divider/VerticalDivider.vue";
import {DividerType} from "@/common/components/divider/DividerType";
import {DropDownValue} from "@/common/components/formDropDown/DropDownValue";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import {PromptExecutionResult} from "@/modules/prompts/models/PromptExecutionResult";
import AppTable from "@/common/components/appTable/AppTable.vue";
import {TableData} from "@/common/components/appTable/TableData";
import {LayoutConstants} from "../../../common/constants/LayoutConstants";
import {TableRowData} from "@/common/components/appTable/TableRowData";
import {FeedbackExecutionWorkflow} from "@/modules/prompts/models/FeedbackExecutionWorkflow";
import FormDropDown from "@/common/components/formDropDown/FormDropDown.vue";
import {TableAction} from "@/common/components/appTable/TableAction";
import type {PromptsExecutionHistory} from "@/modules/prompts/models/PromptExecutionHistory";
import {Query} from "@/infrastructure/models/query/Query";
import {PromptExecutionResultFilter} from "@/modules/prompts/models/PromptExecutionResultFilter";
import {ExecuteSinglePromptCommand} from "@/modules/prompts/models/ExecuteSinglePromptCommand";

const insightBoxApiClient = new InsightBoxApiClient();

const promptExecutionResults = ref<Array<PromptExecutionResult>>([]);
const promptExecutionHistories = ref<Array<PromptsExecutionHistory>>([]);

const props = defineProps({
    promptCategory: {
        type: Object as PropType<AnalysisPromptCategory>,
        required: true
    },
    workflows: {
        type: Array as PropType<Array<FeedbackExecutionWorkflow>>,
        required: true
    }
});

const selectedWorkflow = ref<DropDownValue<string, FeedbackExecutionWorkflow> | null>(null);
const workflowDropDownValues = ref<Array<DropDownValue<string, FeedbackExecutionWorkflow>>>();

watch(() => props.workflows, async () => {
    loadWorkflowOptions();
}, {deep: true});

const emit = defineEmits<{
    (e: 'addPrompt', promptCategoryId: string): void
    (e: 'editPrompt', promptId: string): void
}>();

const promptResultsTableData = ref<TableData>(new TableData([
        "Version",
        "Avg Execution Time",
        "Average Accuracy",
        "Executions Count",
        "Actions",
    ], []
));

const promptHistoriesTableData = ref<TableData>(new TableData([
        "Execution Time",
        "Execution Duration",
        "Result",
        "Actions",
    ], []
));

onBeforeMount(async () => {
    loadWorkflowOptions();
    await loadAllPromptVersionResults();
    await loadPromptExecutionHistoryAsync();
});

const loadAllPromptVersionResults = async () => {
    const response = await insightBoxApiClient.prompts.getPromptExecutionResults(props.promptCategory.id);

    if (response.response) {
        promptExecutionResults.value = response.response;
        promptResultsTableData.value.rows = promptExecutionResults.value.map(result =>
        {
            return convertResultToTableRowData(result);
        })
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

const loadPromptExecutionHistoryAsync = async () => {
    if(!props.promptCategory?.selectedPromptId) return;

    const response = await insightBoxApiClient.executionHistories.getByPromptIdAsync(props.promptCategory?.selectedPromptId);

    if (response.response) {
        promptExecutionHistories.value = response.response;
        promptHistoriesTableData.value.rows = promptExecutionResults.value.map(result =>
        {
            return convertHistoryToTableRowData(result);
        })
    }
};

const convertHistoryToTableRowData = (result: PromptsExecutionHistory) => {
    return new TableRowData([
            result.executionTime,
            result.executionDurationInSeconds,
            result.result !== null
        ],
        [
            new TableAction(() => emit('editPrompt', result.promptId), ButtonType.Secondary, 'fas fa-edit')
        ]
    );
};

const onTriggerWorkflow = () => {
    const executeSinglePromptCommand = new ExecuteSinglePromptCommand();
    executeSinglePromptCommand.workflowId = selectedWorkflow.value?.value?.id!;
    executeSinglePromptCommand.promptId = props.promptCategory?.selectedPromptId!;

    console.log(executeSinglePromptCommand);
}


const loadWorkflowOptions = () => {
    workflowDropDownValues.value = props.workflows?.map(workflow => {
        return new DropDownValue(workflow.name, workflow);
    });
};

</script>