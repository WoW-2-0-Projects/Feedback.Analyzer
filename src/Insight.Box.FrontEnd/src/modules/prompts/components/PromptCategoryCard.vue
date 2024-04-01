<template>

    <div class="w-full h-[300px] flex card card-bg card-round card-shadow text-secondaryContentColor">

        <!-- Prompt category details -->
        <div class=" flex flex-col items-center justify-center w-full">
            <h5 class="text-xl">{{ promptCategory.typeDisplayName }}</h5>

            <!-- Add prompt button -->
            <div class="mt-5 flex justify-center gap-5 w-full">

                <div class="flex flex-col h-fit items-center">
                    <h5 class="text-3xl font-bold"> {{ promptCategory.promptsCount }}</h5>
                    <h5 class="text-sm "> {{ LayoutConstants.Versions }}</h5>
                </div>

                <div class="flex flex-col h-fit items-center w-1/4 text-center">
                    <h5 class="text-3xl font-bold">{{ promptCategory.selectedPromptVersion }}</h5>
                    <h5 class="text-sm whitespace-pre-wrap text-wrap"> {{ LayoutConstants.SelectedVersion }}</h5>
                </div>

            </div>

            <!-- Training workflows -->
            <div class="mt-5 items-center flex flex-col">
                <div class="flex gap-4 mt-5">
                    <form-drop-down label="Filter by"
                                    v-model="selectedTrainingWorkflow"
                                    :values="trainingWorkflowOptions"
                                    :size="ActionComponentSize.ExtraSmall"/>
                    <div class="flex gap-4">

                        <app-button :type="ActionType.Secondary" :layout="ButtonLayout.Square" icon="fas fa-plus"
                                    :size="ActionComponentSize.ExtraSmall"
                                    @click="emit('addPrompt', promptCategory.id, promptResultLoadFunction!)"/>

                        <app-button :type="ActionType.Primary" :layout="ButtonLayout.Rectangle" icon="fas fa-play"
                                    text="Run"
                                    :disabled="!selectedTrainingWorkflow || !promptCategory.selectedPromptId"
                                    :size="ActionComponentSize.ExtraSmall" @click="onTriggerWorkflow"/>

                    </div>
                </div>

            </div>

        </div>

        <!-- Prompt Section -->
        <div class="w-full p-5 pt-3 pr-2.5 flex flex-col items-center">
            <h5 class="text-center">Prompts</h5>
            <div class="mt-3  rounded-lg overflow-y-scroll no-scrollbar">
                <app-table class="w-full " :data="promptResultsTableData"/>
            </div>
        </div>

        <!-- Prompt execution result -->
        <div class="w-full p-5 pt-3 pl-2.5 flex flex-col">
            <h5 class="text-center">Execution histories</h5>
            <div class="mt-3  rounded-lg overflow-y-scroll no-scrollbar">
                <app-table class="w-full" :data="promptHistoriesTableData"/>
            </div>
        </div>

    </div>

</template>

<script setup lang="ts">

import {onBeforeMount, type PropType, ref, watch} from "vue";
import {AnalysisPromptCategory} from "@/modules/prompts/models/AnalysisPromptCategory";
import {FeedbackAnalysisWorkflow} from "@/modules/workflows/models/FeedbackAnalysisWorkflow";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import AppButton from "@/common/components/appButton/AppButton.vue";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import {DropDownValue} from "@/common/components/formDropDown/DropDownValue";
import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import FormDropDown from "@/common/components/formDropDown/FormDropDown.vue";
import {TableData} from "@/common/components/appTable/TableData";
import AppTable from "@/common/components/appTable/AppTable.vue";
import type {PromptsExecutionHistory} from "@/modules/prompts/models/PromptExecutionHistory";
import type {PromptExecutionResult} from "@/modules/prompts/models/PromptExecutionResult";
import {TableAction} from "@/common/components/appTable/TableAction";
import {TableRowData} from "@/common/components/appTable/TableRowData";
import {DateTimeFormatterService} from "@/infrastructure/services/dateTime/DateTimeFormatterService";
import {ActionType} from "@/common/components/actions/ActionType";
import {AsyncFunction} from "@/infrastructure/models/delegates/AsyncFunction";
import type {PromptExecutionResult} from "@/modules/prompts/models/PromptExecutionResult";

const insightBoxApiClient = new InsightBoxApiClient();
const dateTimeFormatterService = new DateTimeFormatterService();

const promptExecutionResults = ref<Array<PromptExecutionResult>>([]);
const promptExecutionHistories = ref<Array<PromptsExecutionHistory>>([]);


const props = defineProps({
    promptCategory: {
        type: Object as PropType<AnalysisPromptCategory>,
        required: true
    },
    workflows: {
        type: Array as PropType<Array<FeedbackAnalysisWorkflow>>,
        required: true
    }
});

const promptResultLoadFunction = ref<AsyncFunction>();
const selectedTrainingWorkflow = ref<DropDownValue<string, FeedbackAnalysisWorkflow> | null>(null);
const trainingWorkflowOptions = ref<Array<DropDownValue<string, FeedbackAnalysisWorkflow>>>();
const promptExecutionResults = ref<Array<PromptExecutionResult>>([]);

watch(() => props.workflows, async () => {
    loadWorkflowOptions();
}, {deep: true});

const emit = defineEmits<{
    (e: 'addPrompt', promptCategoryId: string, loadPromptResultCallback: AsyncFunction): void,
    (e: 'editPrompt', promptId: string, loadPromptResultCallback: AsyncFunction<string>),
    (e: 'openHistory', history: PromptsExecutionHistory): void,
    (e: 'loadCategory', categoryId: string): void
}>();

const promptResultsTableData = ref<TableData>(new TableData([
        "V",
        "AED",
        "AA",
        "EC",
        "Actions",
    ], []
));

const promptHistoriesTableData = ref<TableData>(new TableData([
        "ET",
        "ED",
        "A",
        "Actions",
    ], []
));

onBeforeMount(async () => {
    loadWorkflowOptions();
    promptResultLoadFunction.value = new AsyncFunction(loadPromptResultsAsync);
    await loadAllPromptResultsAsync();
    await loadPromptExecutionHistoryAsync();
});

const loadAllPromptResultsAsync = async () => {
    const response = await insightBoxApiClient.prompts.getPromptResultsByCategoryIdAsync(props.promptCategory.id);

    if (response.response) {
        promptExecutionResults.value = response.response;
        promptResultsTableData.value.rows = promptExecutionResults.value.map(result => {
            return mapResultToTableRowData(result);
        });
    }
};

const loadPromptResultAsync = async (promptId: string) => {
    const response = await insightBoxApiClient.prompts.getPromptResultsByCategoryIdAsync(props.promptCategory.id);

    if (response.response) {
        promptExecutionResults.value = response.response;
        promptResultsTableData.value.rows = promptExecutionResults.value.map(result => mapResultToTableRowData(result));
    }
};

const mapResultToTableRowData = (result: PromptExecutionResult) => {
    return new TableRowData([
            `${result.version}.${result.revision}`,
            result.averageExecutionTime,
            result.averageAccuracy,
            result.executionsCount
        ],
        [
            new TableAction(
                () => emit('editPrompt', result.promptId, promptResultLoadFunction.value),
                ActionType.Secondary, 'fas fa-edit'),
            new TableAction(() => onPromptVersionSelected(result.promptId), ActionType.Secondary, 'fas fa-paperclip')
        ]
    );
};

const loadPromptExecutionHistoryAsync = async () => {
    if (!props.promptCategory?.selectedPromptId) return;

    const response = await insightBoxApiClient.executionHistories.getByPromptIdAsync(props.promptCategory?.selectedPromptId);

    if (response.response) {
        promptExecutionHistories.value = response.response;

        promptHistoriesTableData.value.rows = promptExecutionHistories.value.map(result => {
            return mapHistoryToTableRowData(result);
        });
    }
};

const mapHistoryToTableRowData = (history: PromptsExecutionHistory) => {

    return new TableRowData([
            dateTimeFormatterService.formatHumanize(history.executionTime),
            history.executionDurationInMilliseconds,
            history.result !== null
        ],

        [
            new TableAction(() => {
                    emit('openHistory', history)
                }
                , ActionType.Secondary, 'fas fa-circle-info')
        ]
    );
};

const onTriggerWorkflow = async () => {
    if (!selectedTrainingWorkflow.value?.value?.id || !props.promptCategory?.selectedPromptId)
        return;

    const response = await insightBoxApiClient.workflows
        .executeSinglePromptAsync(selectedTrainingWorkflow.value?.value?.id!, props.promptCategory?.selectedPromptId!);
}

const loadWorkflowOptions = () => {
    trainingWorkflowOptions.value = props.workflows?.map(workflow => {
        return new DropDownValue(workflow.name, workflow)
    });
};

const onPromptVersionSelected = async (promptId: string) => {
    const response = await insightBoxApiClient.prompts.updateSelectedPromptAsync(props.promptCategory?.id, promptId);

    if (response.isSuccess) {
        await loadPromptExecutionHistoryAsync();
        emit('loadCategory', props.promptCategory?.id);
    }
};


const loadPromptResultsAsync = async () => {
    const response = await insightBoxApiClient.prompts.getPromptResultsByCategoryIdAsync(props.promptCategory.id);

    if (response.response) {
        promptExecutionResults.value = response.response;
    }
}

</script>