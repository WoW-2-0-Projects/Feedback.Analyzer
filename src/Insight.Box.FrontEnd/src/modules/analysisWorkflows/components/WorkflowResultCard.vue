<template>

    <expanding-card-base :isExpanded="isExpanded"
                         :mainContentHeight="1000"
                         :expandingContentHeight="200"
                         class="w-full text-secondaryContentColor border border-accentSecondaryColor rounded-lg"
    >

        <template v-slot:mainContent>

            <div class="flex p-2">

                <!-- Prompt category details -->
                <div class="flex flex-col items-start gap-y-2 w-full">

                    <div class="flex flex-col">
                        <h5 class="text-sm text-tertiaryColor"> {{ LayoutConstants.StartedTime }}
                            : {{ dateTimeFormatterService.formatHumanize(result.startedTime) }}</h5>
                        <h5 class="text-sm text-tertiaryColor"> {{ LayoutConstants.FinishedTime }}
                            : {{ dateTimeFormatterService.formatHumanize(result.finishedTime) }}</h5>
                    </div>

                    <app-button class="w-fit" :type="ButtonType.Secondary" :layout="ButtonLayout.Square"
                                icon="fas fa-circle-info" @click="isExpanded = !isExpanded"
                                :size="ActionComponentSize.ExtraSmall"/>

                </div>

                <vertical-divider :type="DividerType.ContentLength"/>

                <!-- Prompt selection -->
                <div class="w-full p-2 py-5 flex flex-col item-center">

                </div>

                <vertical-divider :type="DividerType.ContentLength"/>

                <!-- Prompt execution result -->
                <div class="w-full p-2 py-5 flex flex-col">

                </div>

            </div>

        </template>

        <template v-slot:expandingContent>

            <!-- Workflow execution results -->

            <!--            <app-table>-->

            <!--            </app-table>-->


            <!--            <div v-for="test in 10">hi</div>-->

        </template>


    </expanding-card-base>

</template>

<script setup lang="ts">

import {FeedbackAnalysisWorkflowResult} from "@/modules/analysisWorkflows/models/FeedbackAnalysisWorkflowResult";
import {type PropType, ref} from "vue";
import {ButtonType} from "@/common/components/appButton/ButtonType";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import {DividerType} from "@/common/components/dividers/DividerType";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import AppButton from "@/common/components/appButton/AppButton.vue";
import VerticalDivider from "@/common/components/dividers/VerticalDivider.vue";
import ExpandingCardBase from "@/common/components/expandingCardBase/ExpandingCardBase.vue";
import {DateTimeFormatterService} from "@/infrastructure/services/dateTime/DateTimeFormatterService";

const dateTimeFormatterService = new DateTimeFormatterService();

const isExpanded = ref<boolean>(false);

const props = defineProps({
    result: {
        type: Object as PropType<FeedbackAnalysisWorkflowResult>,
        required: true
    }
});

</script>