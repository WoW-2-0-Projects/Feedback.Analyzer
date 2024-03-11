<template>

    <expanding-card-base :isExpanded="isExpanded"
                         :mainContentHeight="1000"
                         :expandingContentHeight="200"
                         class="w-full text-secondaryContentColor border border-accentSecondaryColor rounded-lg"
    >

        <template v-slot:mainContent>

            <div class="flex p-2">

                <!-- Prompt category details -->
                <div class="flex items-start justify-between gap-y-2 w-full">

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

            </div>

        </template>

        <template v-slot:expandingContent>
            <app-table class="w-full" :data="feedbackResultsTableData"/>
        </template>

    </expanding-card-base>

</template>

<script setup lang="ts">

import {FeedbackAnalysisWorkflowResult} from "@/modules/analysisWorkflows/models/FeedbackAnalysisWorkflowResult";
import {computed, type PropType, ref} from "vue";
import {ButtonType} from "@/common/components/appButton/ButtonType";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import AppButton from "@/common/components/appButton/AppButton.vue";
import ExpandingCardBase from "@/common/components/expandingCardBase/ExpandingCardBase.vue";
import {DateTimeFormatterService} from "@/infrastructure/services/dateTime/DateTimeFormatterService";
import {TableRowData} from "@/common/components/appTable/TableRowData";
import {TableAction} from "@/common/components/appTable/TableAction";
import type {FeedbackAnalysisResult} from "@/modules/analysisWorkflows/models/FeedbackAnalysisResult";
import {TableData} from "@/common/components/appTable/TableData";
import AppTable from "@/common/components/appTable/AppTable.vue";

const dateTimeFormatterService = new DateTimeFormatterService();


const props = defineProps({
    result: {
        type: Object as PropType<FeedbackAnalysisWorkflowResult>,
        required: true
    }
});

// Component states
const isExpanded = ref<boolean>(false);

// Feedback analysis result states
const feedbackResultsTableData = computed(() => {
    const tableData = new TableData([
        "Username",
        "Is Relevant",
    ], []);

    if (props.result.results.length > 0) {
        tableData.rows = props.result.results.map(result => mapFeedbackAnalysisResultToTableRowData(result));
    }

    return tableData;
});

const mapFeedbackAnalysisResultToTableRowData = (result: FeedbackAnalysisResult) => {
    console.log('test', result.feedbackRelevance);

    return new TableRowData([
            'John',
            // ${result.version}.${result.revision}`,
            result.feedbackRelevance.isRelevant,
            // result.averageAccuracy,
            // result.executionsCount
        ],
        [
            new TableAction(() => console.log(result.id), ButtonType.Secondary, 'fas fa-paperclip')
        ]
    );
};
</script>