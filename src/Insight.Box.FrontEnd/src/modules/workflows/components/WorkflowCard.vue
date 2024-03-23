<template>

    <expanding-card-base :isExpanded="isResultsListOpen"
                         :expandingContentHeight="400"
                         class="w-full card card-bg card-round card-shadow text-secondaryContentColor"
    >
        <template v-slot:mainContent>

            <div class="flex p-2 shadow-xl card-round">

                <!-- Prompt category details -->
                <div class="w-1/4 flex items-center py-5">

                    <div class="flex flex-col flex-grow items-center justify-between h-full">
                        <h5 class="text-2xl text-center">{{ workflow.name }}</h5>
                        <h5 class="text-base text-center text-tertiaryContentColor">{{ LayoutConstants.Product }} :
                            {{ workflow.productName }}</h5>

                        <div class="flex gap-5 items-center justify-center text-tertiaryContentColor cursor-cell">

                            <div class="flex flex-col justify-center items-center">
                                <h5 class="text-2xl font-bold">10</h5>
                                <h5 class="text-xs"> {{ LayoutConstants.Started }}</h5>
                            </div>

                            <div class="flex flex-col justify-center items-center">
                                <h5 class="text-2xl font-bold">7</h5>
                                <h5 class="text-xs"> {{ LayoutConstants.Completed }}</h5>
                            </div>

                            <div class="flex flex-col justify-center items-center">
                                <h5 class="text-2xl font-bold">97%</h5>
                                <h5 class="text-xs"> {{ LayoutConstants.Positive }}</h5>
                            </div>

                        </div>

                        <div class="flex gap-3">

                            <app-button class="w-fit" :type="ActionType.Primary" :layout="ButtonLayout.Rectangle"
                                        :size="ActionComponentSize.ExtraSmall" :text="runButtonText" icon="fas fa-play"
                            />
                            <app-button :type="ActionType.Secondary" :layout="ButtonLayout.Rectangle"
                                        :size="ActionComponentSize.ExtraSmall" icon="fas fa-eye"
                            />
                            <app-button class="w-fit" :type="ActionType.Secondary" :layout="ButtonLayout.Rectangle"
                                        :size="ActionComponentSize.ExtraSmall" icon="fas fa-edit"
                                        @click="emit('edit', workflow)"
                            />
                            <app-button class="w-fit" :type="ActionType.Secondary" :layout="ButtonLayout.Rectangle"
                                        :size="ActionComponentSize.ExtraSmall" icon="fas fa-clock-rotate-left"
                                        @click="onOpenWorkflowResultsList"
                            />
                            <app-button class="w-fit" :type="ActionType.Danger" :layout="ButtonLayout.Rectangle"
                                        :size="ActionComponentSize.ExtraSmall" icon="fas fa-trash"
                                        @click="emit('delete', workflow.id)"
                            />

                        </div>

                    </div>

                </div>

                <vertical-divider :type="DividerType.ContentLength"/>

                <!-- Prompt selection -->
                <div class="w-1/2 flex flex-col p-2 py-5 item-center justify-center">

                    <h5 class="text-center">Statistics</h5>

                    <div class="flex">

                        <div class="w-full flex flex-col items-center justify-center">
                            <doughnut-chart/>
                        </div>

                        <div class="w-full flex flex-col items-center justify-center">
                            <doughnut-chart/>
                        </div>

                        <div class="w-full flex flex-col items-center justify-center">
                            <doughnut-chart/>
                        </div>

                    </div>

                </div>

                <vertical-divider :type="DividerType.ContentLength"/>

                <!-- Prompt execution result -->
                <div class="w-1/4 flex-1 p-2 py-5 flex flex-col flex-grow items-center justify-between h-full">

                    <h5 class="text-center">Key points</h5>

                    <div class="mt-5 flex gap-2 flex-wrap">
                        <span class="bg-accentTertiaryColor px-3 py-1 rounded-full text-sm whitespace-nowrap">mouse
                            flat</span>
                        <span class="bg-accentTertiaryColor px-3 py-1 rounded-full text-sm whitespace-nowrap">mouse feels
                            flat</span>
                        <span class="bg-accentTertiaryColor px-3 py-1 rounded-full text-sm whitespace-nowrap">mouse
                            flat</span>
                        <span class="bg-accentTertiaryColor px-3 py-1 rounded-full text-sm whitespace-nowrap">mouse feels
                            flat</span>
                    </div>


                </div>

            </div>

        </template>

        <template v-slot:expandingContent>

            <!-- Workflow results history -->
            <div class="flex flex-col w-full gap-5 p-5">
                <workflow-result-card v-for="(result, index) in workflowResults" :workflowResult="result" :key="index"
                                      :closeSource="closeSource" @closeOthers="resultId =>
                                      closeSource.updateListeners(resultId)"
                />
            </div>

        </template>

    </expanding-card-base>

</template>

<script setup lang="ts">

import {defineEmits, type PropType, ref} from "vue";
import AppButton from "@/common/components/appButton/AppButton.vue";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import VerticalDivider from "@/common/components/dividers/VerticalDivider.vue";
import {DividerType} from "@/common/components/dividers/DividerType";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import ExpandingCardBase from "@/common/components/expandingCardBase/ExpandingCardBase.vue";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import type {FeedbackAnalysisWorkflow} from "@/modules/workflows/models/FeedbackAnalysisWorkflow";
import DoughnutChart from "@/common/components/doughnutChart/DoughnutChart.vue";
import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import {Query} from "@/infrastructure/models/query/Query";
import {FeedbackAnalysisWorkflowResultFilter} from "@/modules/workflows/models/FeedbackAnalysisWorkflowResultFilter";
import WorkflowResultCard from "@/modules/workflows/components/WorkflowResultCard.vue";
import {ActionType} from "@/common/components/actions/ActionType";
import {NotificationSource} from "@/infrastructure/models/notifications/Action";

const runButtonText = "Run";
const isResultsListOpen = ref<boolean>(false);

const insightBoxClient = new InsightBoxApiClient();

const props = defineProps({
    workflow: {
        type: Object as PropType<FeedbackAnalysisWorkflow>,
        required: true
    }
});

// Workflow results states
const workflowResultsQuery = ref<Query>(new Query(new FeedbackAnalysisWorkflowResultFilter(props.workflow?.id)));
const workflowResults = ref<FeedbackAnalysisWorkflow[]>([]);
const closeSource = ref<NotificationSource<string>>(new NotificationSource<string>());

const emit = defineEmits<{
    (e: 'update', workflow: FeedbackAnalysisWorkflow): void,
    (e: 'delete', workflowId: string): void,
}>();

const onOpenWorkflowResultsList = async () => {
    if (workflowResults.value.length == 0) {
        const response = await insightBoxClient.workflows.getResultsAsync(workflowResultsQuery.value);
        if (response.response)
            workflowResults.value = response.response;
    }

    isResultsListOpen.value = !isResultsListOpen.value;
}

</script>