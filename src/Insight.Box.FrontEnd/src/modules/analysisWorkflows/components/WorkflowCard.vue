<template>

    <expanding-card-base :isExpanded="isResultsListOpen"
                         :expandingContentHeight="400"
                         class="w-full card card-bg card-round card-shadow text-secondaryContentColor"
    >
        <template v-slot:mainContent>

            <div class="flex p-2 card-shadow">

                <!-- Prompt category details -->
                <div class="w-1/4 flex items-center">

                    <div class="flex flex-col flex-grow">
                        <h5 class="text-xl text-center">{{ workflow.name }}</h5>

                        <h5 class="text-sm"> {{ LayoutConstants.Results }} : {{ workflowResults.length }}</h5>

                        <div class="flex gap-5">

                            <app-button class="w-fit" :type="ButtonType.Success" :layout="ButtonLayout.Square"
                                        icon="fas fa-play"
                                        :size="ActionComponentSize.ExtraSmall" @click="onTriggerWorkflow"/>

                            <app-button class="w-fit" :type="ButtonType.Secondary" :layout="ButtonLayout.Square"
                                        icon="fas fa-clock-rotate-left" @click="onToggleResultsList"
                                        :size="ActionComponentSize.ExtraSmall"/>

                        </div>

                    </div>

                </div>

                <vertical-divider :type="DividerType.ContentLength"/>

                <!-- Prompt selection -->
                <div class="w-1/2 flex p-2 py-5 item-center">

                    <div class="w-full flex flex-col items-center justify-center">
                        <h5>Overall opinions</h5>
                        <doughnut-chart/>
                    </div>

                    <div class="w-full flex flex-col items-center justify-center">
                        <h5>Opinion points</h5>
                        <doughnut-chart/>
                    </div>

                    <div class="w-full flex flex-col items-center justify-center">
                        <h5>Actionable points</h5>
                        <doughnut-chart/>
                    </div>

                </div>

                <vertical-divider :type="DividerType.ContentLength"/>

                <!-- Prompt execution result -->
                <div class="w-1/4 flex-1 p-2 py-5 flex flex-col">

                    <h5>Key points</h5>

                </div>

            </div>

        </template>

        <template v-slot:expandingContent>

            <!-- Workflow execution results -->
            <div class="flex flex-col gap-2 p-2">
                <div v-for="result in workflowResults" :key="result.id">
                    <workflow-result-card :result="result"/>
                </div>
            </div>

        </template>

    </expanding-card-base>

</template>

<script setup lang="ts">

import {onBeforeMount, type PropType, ref} from "vue";
import {ButtonType} from "@/common/components/appButton/ButtonType";
import AppButton from "@/common/components/appButton/AppButton.vue";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import VerticalDivider from "@/common/components/dividers/VerticalDivider.vue";
import {DividerType} from "@/common/components/dividers/DividerType";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import {FeedbackAnalysisWorkflow} from "@/modules/prompts/models/FeedbackAnalysisWorkflow";
import ExpandingCardBase from "@/common/components/expandingCardBase/ExpandingCardBase.vue";
import {FeedbackAnalysisWorkflowResult} from "@/modules/analysisWorkflows/models/FeedbackAnalysisWorkflowResult";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import WorkflowResultCard from "@/modules/analysisWorkflows/components/WorkflowResultCard.vue";
import HorizontalDivider from "@/common/components/dividers/HorizontalDivider.vue";
import DoughnutChart from "@/common/components/doughnutChart/DoughnutChart.vue";

const isResultsListOpen = ref<boolean>(false);
const insightBoxApiClient = new InsightBoxApiClient();
const workflowResults = ref<Array<FeedbackAnalysisWorkflowResult>>([]);

const props = defineProps({
    workflow: {
        type: Object as PropType<FeedbackAnalysisWorkflow>,
        required: true
    }
});

onBeforeMount(async () => {
    await loadWorkflowResults();
});

const onTriggerWorkflow = async () => {
    const response = await insightBoxApiClient.workflows
        .executeWorkflowAsync(props.workflow?.id);
}

const onToggleResultsList = async () => {
    if (workflowResults.value.length === 0) {
        const result = await loadWorkflowResults();

        // Toggle only if results are available or if the list is already open
        if (result || isResultsListOpen.value)
            isResultsListOpen.value = !isResultsListOpen.value;
    } else
        isResultsListOpen.value = !isResultsListOpen.value;
}

const loadWorkflowResults = async () => {
    const response = await insightBoxApiClient.workflows.getResultsById(props.workflow?.id);

    if (response.response) {
        workflowResults.value = response.response;
        return true;
    }

    return false;
}

</script>