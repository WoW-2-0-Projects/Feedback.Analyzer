<template>

    <div class="w-full h-[400px] flex card card-bg card-round card-shadow text-secondaryContentColor">

        <!-- Prompt category details -->
        <div class="flex flex-col items-center justify-center w-full">
            <h5 class="text-xl">{{ promptCategory.typeDisplayName }}</h5>

            <!-- Add prompt button -->
            <div class="mt-10 flex gap-10 items-center justify-center">
                <div class="flex flex-col h-fit">
                    <h5 class="text-sm"> {{ LayoutConstants.Versions }} : {{ promptCategory.promptsCount }}</h5>
                    <h5 class="text-sm"> {{ LayoutConstants.SelectedVersion }} :
                        {{ promptCategory.selectedPromptVersion }}</h5>
                </div>

                <app-button :type="ButtonType.Primary" :layout="ButtonLayout.Rectangle" icon="fas fa-plus"
                            text="Add Prompt"
                            :size="ActionComponentSize.ExtraSmall" @click="emit('addPrompt', (promptCategory.id))"/>
            </div>

            <!-- Training workflows -->
            <div class="mt-20">
                <h5 class="text-center">Training workflows</h5>

                <div class="flex gap-10 mt-5">
                    <form-drop-down label="Filter by" v-model="selectedWorkflow" :values="workflowDropDownValues"
                                    :size="ActionComponentSize.Small"/>

                    <app-button :type="ButtonType.Success" :layout="ButtonLayout.Rectangle" icon="fas fa-play"
                                text="Trigger"
                                :disabled="selectedWorkflow === null || promptCategory.selectedPromptId === null"
                                :size="ActionComponentSize.Small" @click="onTriggerWorkflow"/>
                </div>

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
import {DateTimeFormatterService} from "@/infrastructure/services/dateTime/DateTimeFormatterService";

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
    (e: 'editPrompt', promptId: string): void,
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
        "ET",
        "ED",
        "A",
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
        promptResultsTableData.value.rows = promptExecutionResults.value.map(result => {
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
            new TableAction(() => emit('editPrompt', result.promptId), ButtonType.Secondary, 'fas fa-edit'),
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

const convertHistoryToTableRowData = (result: PromptsExecutionHistory) => {
    return new TableRowData([
            dateTimeFormatterService.formatHumanize(result.executionTime),
            result.executionDurationInMilliseconds,
            result.result !== null
        ],
        [
            new TableAction(() => emit('editPrompt', result.promptId), ButtonType.Secondary, 'fas fa-edit')
        ]
    );
};

const onTriggerWorkflow = async () => {
    if (!selectedWorkflow.value?.value?.id || !props.promptCategory?.selectedPromptId)
        return;

    // const executeSinglePromptCommand = new ExecuteSinglePromptCommand();
    const response = await insightBoxApiClient.workflows
        .executeSinglePrompt(selectedWorkflow.value?.value?.id!, props.promptCategory?.selectedPromptId!);
}


const loadWorkflowOptions = () => {
    workflowDropDownValues.value = props.workflows?.map(workflow => {
        return new DropDownValue(workflow.name, workflow);
    });
};

const onPromptVersionSelected = async (promptId: string) => {
    const response = await insightBoxApiClient.prompts.updateSelectedPromptAsync(props.promptCategory?.id, promptId);

    if (response.isSuccess) {
        console.log('emit');
        // props.promptCategory.selectedPromptId = promptId;
        await loadPromptExecutionHistoryAsync();
        emit('loadCategory', props.promptCategory?.id);
    }
};

</script>