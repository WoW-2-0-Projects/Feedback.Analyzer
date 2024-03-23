<template>

    <expanding-card-base :isExpanded="isResultsListOpen"
                         :expandingContentHeight="400"
                         class="w-full card card-bg card-round card-shadow text-secondaryContentColor"
    >
        <template v-slot:mainContent>

            <div class="flex shadow-xl items-center justify-start px-5 h-[70px] gap-5"
                 :class="isResultsListOpen ? 'card-top-round' : 'card-round'">

                <!-- Workflow result actions -->
                <div class="flex items-center gap-3">
                    <app-button :type="ButtonType.Secondary" :layout="ButtonLayout.Rectangle"
                                :size="ActionComponentSize.ExtraSmall" icon="fas fa-eye"
                    />
                    <app-button :type="ButtonType.Secondary" :layout="ButtonLayout.Rectangle"
                                :size="ActionComponentSize.ExtraSmall" icon="fas fa-clock-rotate-left"
                    />
                    <app-button class="w-fit" :type="ButtonType.Danger" :layout="ButtonLayout.Rectangle"
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
                        <h5 class="text-base font-bold whitespace-nowrap">
                            {{ timeFormatterService.formatHumanize(workflowResult.finishedTime) }}</h5>
                        <div></div>
                    </div>

                    <div class="h-full flex flex-col items-start justify-between text-tertiaryContentColor">
                        <h5 class="text-xs">{{ LayoutConstants.Status }}</h5>
                        <app-chip text="Completed" :type="ChipType.Success"/>
                        <div/>
                    </div>

                    <div class="h-full flex flex-col items-start justify-between pb-2 text-tertiaryContentColor">
                        <h5 class="text-xs"> {{ LayoutConstants.Feedbacks }}</h5>
                        <h5 class="text-xl font-bold">10</h5>
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

            <h5>test</h5>

        </template>

    </expanding-card-base>

</template>


<script setup lang="ts">

import {DividerType} from "@/common/components/dividers/DividerType";
import VerticalDivider from "@/common/components/dividers/VerticalDivider.vue";
import ExpandingCardBase from "@/common/components/expandingCardBase/ExpandingCardBase.vue";
import {type PropType, ref} from "vue";
import type {FeedbackAnalysisWorkflowResult} from "@/modules/workflows/models/FeedbackAnalysisWorkflowResult";
import DoughnutChart from "@/common/components/doughnutChart/DoughnutChart.vue";
import {DateTimeFormatterService} from "@/infrastructure/services/dateTime/DateTimeFormatterService";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import {ButtonType} from "@/common/components/appButton/ButtonType";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import AppButton from "@/common/components/appButton/AppButton.vue";
import AppChip from "@/common/components/appChip/AppChip.vue";
import {ChipType} from "@/common/components/appChip/ChipType";
import {Position} from "@/common/components/chartLabel/Position";

const timeFormatterService = new DateTimeFormatterService();

const isResultsListOpen = ref<boolean>(false);

const props = defineProps({
    workflowResult: {
        type: Object as PropType<FeedbackAnalysisWorkflowResult>,
        required: true
    }
});


</script>