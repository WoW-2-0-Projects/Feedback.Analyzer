<template>

    <div class="w-full h-[400px] flex card card-bg card-round card-shadow text-secondaryContentColor">

        <!-- Prompt category details -->
        <div class="flex flex-col items-center justify-center w-full">
            <h5 class="text-xl">{{ promptCategory.typeDisplayName }}</h5>

            <!-- Add prompt button -->
            <div class="mt-10 flex items-center gap-10">
                <div class="flex flex-col h-fit">
                    <h5 class="text-sm"> {{ LayoutConstants.Versions }} : {{ promptCategory.promptsCount }}</h5>
                    <h5 class="text-sm"> {{ LayoutConstants.SelectedVersion }} :
                        {{ promptCategory.selectedPromptVersion }}</h5>
                </div>

                <div class="flex gap-4">

                    <app-button :type="ButtonType.Primary" :layout="ButtonLayout.Square" icon="fas fa-plus"
                                :size="ActionComponentSize.ExtraSmall"
                                @click="emit('addPrompt', promptCategory.id, promptResultLoadFunction)"/>

                    <app-button :type="ButtonType.Success" :layout="ButtonLayout.Square" icon="fas fa-play"
                                :disabled="selectedTrainingWorkflow === null || promptCategory.selectedPromptId === null"
                                :size="ActionComponentSize.ExtraSmall" @click="onTriggerWorkflow"/>
                </div>

            </div>

            <!-- Training workflows -->
            <div class="mt-20">
                <h5 class="text-center">Training workflows</h5>

                <div class="flex gap-10 mt-5">
                    <form-drop-down label="Filter by" v-model="selectedTrainingWorkflow"
                                    :values="trainingWorkflowOptions"
                                    :size="ActionComponentSize.Small"/>
                </div>

            </div>

        </div>

        <!--        <vertical-divider :type="DividerType.ContentLength"/>-->

        <!-- Prompt selection -->
        <div class="w-full p-5 pt-3 pr-2.5 flex flex-col item-center">
            <h5 class="text-center">Prompts</h5>
            <div class="mt-3 rounded-lg overflow-y-scroll no-scrollbar">
                <app-table class="w-full" :data="promptResultsTableData"/>
            </div>
        </div>

        <!--        <vertical-divider :type="DividerType.ContentLength"/>-->

        <!-- Prompt execution result -->
        <div class="w-full p-5 pt-3 pl-2.5 flex flex-col">
            <h5 class="text-center">Execution histories</h5>
            <div class="mt-3 rounded-lg overflow-y-scroll no-scrollbar">
                <app-table class="w-full" :data="promptHistoriesTableData"/>
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
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import {TableRowData} from "@/common/components/appTable/TableRowData";
import {FeedbackAnalysisWorkflow} from "@/modules/prompts/models/FeedbackAnalysisWorkflow";
import FormDropDown from "@/common/components/formDropDown/FormDropDown.vue";
import {TableAction} from "@/common/components/appTable/TableAction";
import type {PromptsExecutionHistory} from "@/modules/prompts/models/PromptExecutionHistory";
import {DateTimeFormatterService} from "@/infrastructure/services/dateTime/DateTimeFormatterService";
import PromptExecutionHistoryModal from "@/modules/prompts/components/PromptExecutionHistoryModal.vue";
import {AsyncFunction} from "@/infrastructure/models/delegates/Function";
import {CategoryTrainingDataService} from "@/modules/prompts/services/CategoryTrainingDataService";

const insightBoxApiClient = new InsightBoxApiClient();
const dateTimeFormatterService = new DateTimeFormatterService();
const categoryTrainingDataService = new CategoryTrainingDataService();

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

watch(() => props.workflows, async () => {
    loadWorkflowOptions();
}, {deep: true});

const emit = defineEmits<{
    (e: 'addPrompt', promptCategoryId: string, loadPromptResultCallback: (promptId: string) => Promise): void
    (e: 'editPrompt', promptId: string, loadPromptResultCallback: AsyncFunction<string>),
    (e: 'openHistory', history: PromptsExecutionHistory): void,
    (e: 'loadCategory', categoryId: string): void,
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
        "Execution Time",
        "ED",
        "A",
        "Actions",
    ], []
));

onBeforeMount(async () => {
    promptResultLoadFunction.value = new AsyncFunction<string>(loadPromptResultAsync);
    loadWorkflowOptions();
    setSelectedTrainingWorkflow();
    await loadAllPromptResultsAsync();
    await loadPromptExecutionHistoryAsync();
});

const loadAllPromptResultsAsync = async () => {
    const response = await insightBoxApiClient.prompts.getPromptResultsByCategoryIdAsync(props.promptCategory.id);

    if (response.response) {
        promptExecutionResults.value = response.response;
        promptResultsTableData.value.rows = promptExecutionResults.value.map(result => {
            return convertResultToTableRowData(result);
        });
    }
};

const loadPromptResultAsync = async (promptId: string) => {
    const response = await insightBoxApiClient.prompts.getPromptResultsByCategoryIdAsync(props.promptCategory.id);

    if (response.response) {
        promptExecutionResults.value = response.response;
        promptResultsTableData.value.rows = promptExecutionResults.value.map(result => {
            return convertResultToTableRowData(result);
        });
    }
};

const convertResultToTableRowData = (result: PromptExecutionResult) => {
    return new TableRowData([
            `${result.version}.${result.revision}`,
            result.averageExecutionTime,
            result.averageAccuracy,
            result.executionsCount
        ],
        [
            new TableAction(
                () => emit('editPrompt', result.promptId, promptResultLoadFunction.value),
                ButtonType.Secondary, 'fas fa-edit'),
            new TableAction(() => onPromptVersionSelected(result.promptId), ButtonType.Secondary, 'fas fa-paperclip')
        ]
    );
};

const loadPromptExecutionHistoryAsync = async () => {
    if (!props.promptCategory?.selectedPromptId) return;

    const response = await insightBoxApiClient.executionHistories.getByPromptIdAsync(props.promptCategory?.selectedPromptId);

    if (response.response) {
        promptExecutionHistories.value = response.response;

        promptHistoriesTableData.value.rows = promptExecutionHistories.value.map(result => {
            return convertHistoryToTableRowData(result);
        });
    }
};

const convertHistoryToTableRowData = (history: PromptsExecutionHistory) => {
    return new TableRowData([
            dateTimeFormatterService.formatHumanize(history.executionTime),
            history.executionDurationInMilliseconds,
            history.result !== null
        ],
        [
            new TableAction(() => emit('openHistory', history), ButtonType.Secondary, 'fas fa-circle-info')
        ]
    );
};

const onTriggerWorkflow = async () => {
    if (!selectedTrainingWorkflow.value?.value?.id || !props.promptCategory?.selectedPromptId)
        return;

    // const executeSinglePromptCommand = new ExecuteSinglePromptCommand();
    const response = await insightBoxApiClient.workflows
        .executeSinglePromptAsync(selectedTrainingWorkflow.value?.value?.id!, props.promptCategory?.selectedPromptId!);
}

const loadWorkflowOptions = () => {
    trainingWorkflowOptions.value = props.workflows?.map(workflow => {
        return new DropDownValue(workflow.name, workflow);
    });
};

const onPromptVersionSelected = async (promptId: string) => {
    const response = await insightBoxApiClient.prompts.updateSelectedPromptAsync(props.promptCategory?.id, promptId);

    if (response.isSuccess) {
        await loadPromptExecutionHistoryAsync();
        emit('loadCategory', props.promptCategory?.id);
    }
};

watch(() => selectedTrainingWorkflow.value, () => {
    // if (selectedTrainingWorkflow.value) {
    //     categoryTrainingDataService.setTrainingData(props.promptCategory?.id, selectedTrainingWorkflow.value.value.id);
    // }
});

const setSelectedTrainingWorkflow = () => {
    // const setTrainingWorkflow = categoryTrainingDataService.getTrainingData(props.promptCategory?.id);
    //
    // if(setTrainingWorkflow) {
    //
    //     // Find that selected one from workflow from database
    //     const test = props.workflows?.findIndex(workflow => workflow.id === setTrainingWorkflow.workflowId);
    //
    //     const foundWorkflow = props.workflows?.find(workflow => workflow.id === setTrainingWorkflow.workflowId);
    //
    //     // If that workflow still exists, put that to drop down value
    //     if(foundWorkflow) {
    //         selectedTrainingWorkflow.value = new DropDownValue(foundWorkflow.name, foundWorkflow);
    //     // If not, remove from storage
    //     } else {
    //         categoryTrainingDataService.removeTrainingData(props.promptCategory?.id);
    //     }
    // }

}

</script>