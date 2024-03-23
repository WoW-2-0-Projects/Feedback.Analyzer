<template>

    <expanding-card-base :isExpanded="isResultsListOpen"
                         :expandingContentHeight="500"
                         class="w-full card card-bg card-round card-shadow text-secondaryContentColor"
    >
        <template v-slot:mainContent>

            <div class="flex shadow-xl items-center justify-start px-5 h-[70px] gap-5"
                 :class="isResultsListOpen ? 'card-top-round' : 'card-round'">

                <!-- Workflow result actions -->
                <div class="flex items-center gap-3">
                    <app-button :type="ActionType.Secondary" :layout="ButtonLayout.Rectangle"
                                :size="ActionComponentSize.ExtraSmall" icon="fas fa-eye"
                    />
                    <app-button :type="ActionType.Secondary" :layout="ButtonLayout.Rectangle"
                                :size="ActionComponentSize.ExtraSmall" icon="fas fa-clock-rotate-left"
                                @click="onOpenFeedbackAnalysisResults"
                    />
                    <app-button class="w-fit" :type="ActionType.Danger" :layout="ButtonLayout.Rectangle"
                                :size="ActionComponentSize.ExtraSmall" icon="fas fa-trash"
                                @click="emit('delete', workflow.id)"
                    />

                </div>

                <vertical-divider :type="DividerType.FullLength" class="h-4/5"/>

                <!-- Workflow analysis result details -->
                <div class="w-1/2 flex items-start justify-between gap-10 h-full py-2">

                    <div class="h-full flex flex-col items-start justify-between text-tertiaryContentColor">
                        <h5 class="text-xs">{{ LayoutConstants.StartedTime }}</h5>
                        <h5 class="text-base font-bold whitespace-nowrap">
                            {{ timeFormatterService.formatHumanize(workflowResult.startedTime) }}</h5>
                        <div/>
                    </div>

                    <div class="h-full flex flex-col items-start justify-between text-tertiaryContentColor">
                        <h5 class="text-xs">{{ LayoutConstants.FinishedTime }}</h5>
                        <h5 v-if="workflowResult.finishedTime" class="text-base font-bold whitespace-nowrap">
                            {{ timeFormatterService.formatHumanize(workflowResult.finishedTime) }}</h5>
                        <i v-else class="fas fa-infinity"></i>
                        <div></div>
                    </div>

                    <div class="h-full flex flex-col items-start justify-between text-tertiaryContentColor">
                        <h5 class="text-xs">{{ LayoutConstants.Status }}</h5>
                        <app-chip text="Completed" :type="ActionType.Success"/>
                        <div/>
                    </div>

                    <div class="h-full flex flex-col items-start justify-between pb-2 text-tertiaryContentColor">
                        <h5 class="text-xs"> {{ LayoutConstants.Processed }}</h5>
                        <h5 class="text-xl font-bold">10</h5>
                    </div>

                    <div class="h-full flex flex-col items-start justify-between pb-2 text-tertiaryContentColor">
                        <h5 class="text-xs"> {{ LayoutConstants.Failed }}</h5>
                        <h5 class="text-xl font-bold">1</h5>
                    </div>

                </div>

                <vertical-divider :type="DividerType.FullLength" class="h-4/5"/>

                <!-- Result diagrams -->
                <div class="flex item-center justify-center">
                    <doughnut-chart :height="80" :labelPosition="Position.Horizontal"
                                    :size="ActionComponentSize.ExtraSmall"/>
                    <doughnut-chart :height="80" :labelPosition="Position.Horizontal"
                                    :size="ActionComponentSize.ExtraSmall"/>
                    <doughnut-chart :height="80" :labelPosition="Position.Horizontal"
                                    :size="ActionComponentSize.ExtraSmall"/>
                </div>

            </div>

        </template>

        <template v-slot:expandingContent>

            <feedback-analysis-result-table :results="feedbackAnalysisResults" class="w-full h-fit"/>

        </template>

    </expanding-card-base>

</template>

<script setup lang="ts">

import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import {DividerType} from "@/common/components/dividers/DividerType";
import VerticalDivider from "@/common/components/dividers/VerticalDivider.vue";
import ExpandingCardBase from "@/common/components/expandingCardBase/ExpandingCardBase.vue";
import {onBeforeMount, type PropType, ref} from "vue";
import type {FeedbackAnalysisWorkflowResult} from "@/modules/workflows/models/FeedbackAnalysisWorkflowResult";
import DoughnutChart from "@/common/components/doughnutChart/DoughnutChart.vue";
import {DateTimeFormatterService} from "@/infrastructure/services/dateTime/DateTimeFormatterService";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import AppButton from "@/common/components/appButton/AppButton.vue";
import AppChip from "@/common/components/appChip/AppChip.vue";
import {Position} from "@/common/components/chartLabel/Position";
import {FeedbackAnalysisResult} from "@/modules/feedbackAnalysisResults/models/FeedbackAnalysisResult";
import {Query} from "@/infrastructure/models/query/Query";
import {FeedbackAnalysisResultFilter} from
        "@/modules/feedbackAnalysisResults/models/FeedbackAnalysisResultFilter";
import FeedbackAnalysisResultTable from "@/modules/feedbackAnalysisResults/components/FeedbackAnalysisResultTable.vue";
import {ActionType} from "@/common/components/actions/ActionType";
import {Organization} from "@/modules/organizations/models/Organization";
import type {NotificationSource} from "@/infrastructure/models/notifications/Action";

const timeFormatterService = new DateTimeFormatterService();
const insightBoxClient = new InsightBoxApiClient();

const props = defineProps({
    workflowResult: {
        type: Object as PropType<FeedbackAnalysisWorkflowResult>,
        required: true
    },
    closeSource: {
        type: Object as PropType<NotificationSource<string>>
    }
});

// Feedback analysis results table states
const isResultsListOpen = ref<boolean>(false);
const feedbackAnalysisResults = ref<Array<FeedbackAnalysisResult>>([]);
const feedbackAnalysisResultsQuery = ref<Query>(new Query(new FeedbackAnalysisResultFilter(props.workflowResult?.id)));

onBeforeMount(() => {
    props.closeSource?.addListener((resultId: string) => {
        console.log('closing', resultId);

        if(resultId != props.workflowResult.id)
            isResultsListOpen.value = false;
    });
})

const emit = defineEmits<
    {
        (e: 'closeOthers', resultId: string): void
    }>();

const onOpenFeedbackAnalysisResults = async () => {
    if (feedbackAnalysisResults.value.length == 0) {
        const response = await insightBoxClient.results.getAsync(feedbackAnalysisResultsQuery.value);
        if (response.response)
            feedbackAnalysisResults.value = response.response;
    }

    emit('closeOthers', props.workflowResult.id);
    isResultsListOpen.value = !isResultsListOpen.value;
}


</script>