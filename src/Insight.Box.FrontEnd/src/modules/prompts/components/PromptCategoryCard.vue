<template>

    <div class="w-full h-[300px] flex card card-bg card-round card-shadow text-secondaryContentColor">

        <!-- Prompt category details -->
        <div class=" flex flex-col items-center justify-center w-full">
            <h5 class="text-xl">{{promptCategory.typeDisplayName}}</h5>

            <!-- Add prompt button -->
            <div class="mt-5 flex justify-center gap-5 w-full">

                <div class="flex flex-col h-fit items-center">
                    <h5 class="text-3xl font-bold"> {{promptCategory.promptsCount}}</h5>
                    <h5 class="text-sm "> {{LayoutConstants.Versions}}</h5>
                </div>

                <div class="flex flex-col h-fit items-center w-1/4 text-center">
                    <h5 class="text-3xl font-bold">0</h5>
                    <h5 class="text-sm whitespace-pre-wrap text-wrap"> {{LayoutConstants.SelectedVersion}}</h5>
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
                                    @click="emit('addPrompt', promptCategory.id, promptResultLoadFunction)"/>

                        <app-button  :type="ActionType.Primary" :layout="ButtonLayout.Rectangle" icon="fas fa-play"
                                     text="Run"
                                     :disabled="selectedTrainingWorkflow === null || promptCategory.selectedPromptId === null"
                                     :size="ActionComponentSize.ExtraSmall" @click="onTriggerWorkflow"/>

                    </div>
                </div>

            </div>

        </div>

        <!-- Prompt Section -->
        <div class="w-full p-5 pt-3 pr-2.5 flex flex-col items-center">
            <h5 class="text-center">Prompts</h5>
            <div class="mt-3 rounded-lg overflow-y-scroll no-scrollbar">
                <app-table class="w-full" :data="promptResultsTableData"/>
            </div>
        </div>

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
import {AnalysisPromptCategory} from "@/modules/prompts/models/AnalysisPromptCategory";
import {FeedbackAnalysisWorkflow} from "@/modules/workflows/models/FeedbackAnalysisWorkflow";
import {LayoutConstants} from "../../../common/constants/LayoutConstants";
import AppButton from "@/common/components/appButton/AppButton.vue";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import {AsyncFunction} from "@/infrastructure/models/notifications/Function";
import {DropDownValue} from "@/common/components/formDropDown/DropDownValue";
import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import FormDropDown from "@/common/components/formDropDown/FormDropDown.vue";
import {TableData} from "@/common/components/appTable/TableData";
import AppTable from "@/common/components/appTable/AppTable.vue";
import {ActionType} from "@/common/components/actions/ActionType";

const insightBoxApiClient = new InsightBoxApiClient();

const props = defineProps({
    promptCategory: {
        type: Object as PropType<AnalysisPromptCategory>,
        required: true
    },
    workflows: {
        type: Array as  PropType<Array<FeedbackAnalysisWorkflow>>,
        required: true
    }
})

const promptResultLoadFunction = ref<AsyncFunction>();
const selectedTrainingWorkflow = ref<DropDownValue<string, FeedbackAnalysisWorkflow> | null>(null);
const trainingWorkflowOptions = ref<Array<DropDownValue<string, FeedbackAnalysisWorkflow>>>();

watch(() => props.workflows, async () => {
    loadWorkflowOptions();
}, {deep: true});

const  emit = defineEmits<{
    (e: 'addPrompt', promptCategoryId: string, loadPromptResultCallback: (promptId: string) => Promise): void,
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

onBeforeMount(async () =>  {
    loadWorkflowOptions();
});

const  onTriggerWorkflow = async () => {
    if (!selectedTrainingWorkflow.value?.value?.id || !props.promptCategory?.selectedPromptId)
        return;

    // const executeSinglePromptCommand = new ExecuteSinglePromptCommand();
    const response = await insightBoxApiClient.workflows
        .executeSinglePromptAsync(selectedTrainingWorkflow.value?.value?.id!, props.promptCategory?.selectedPromptId!);
}

const loadWorkflowOptions = () => {
    trainingWorkflowOptions.value = props.workflows?.map(workflow => {
        return new DropDownValue(workflow.name, workflow)
    });
};
</script>