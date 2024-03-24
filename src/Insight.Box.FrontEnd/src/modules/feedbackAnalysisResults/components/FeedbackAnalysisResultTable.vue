<template>

    <table class="table-auto table-bg">
        <thead>
        <tr>
            <th v-for="header in headers" class="table-header-cell">{{ header }}</th>
        </tr>
        </thead>
        <tbody class="divide-y-[0.5px] divide-accentSecondaryColor">
        <tr class="table-data-row" v-for="result in results" :key="result.id">
            <td class="table-data-cell">
                <app-button :type="ActionType.Secondary" :layout="ButtonLayout.Rectangle"
                            :size="ActionComponentSize.ExtraSmall" icon="fas fa-eye"
                            @click="emit('openFeedbackResult', result.id)"
                />
            </td>
            <td class="table-data-cell">{{ result.customerFeedback.userName }}</td>
            <td class="table-data-cell">
                <app-chip v-if="result.status == WorkflowStatus.Completed" icon="fas fa-check" :type="ActionType.Success"/>
                <app-chip v-if="result.status == WorkflowStatus.Failed" icon="fas fa-triangle-exclamation"
                          :type="ActionType.Danger"/>
            </td>
            <td class="table-data-cell">
                <app-chip :text="result.isRelevant
                            ? LayoutConstants.Relevant
                            : LayoutConstants.NonRelevant"
                          :type="result.isRelevant ? ActionType.Success : ActionType.Danger"/>
            </td>
            <td class="table-data-cell">
                <app-chip :text="OpinionType[result.opinion]"
                          :type="result.mapOpinionToActionType()"/>
            </td>
            <td class="table-data-cell">
                3
            </td>
            <td class="table-data-cell">
                <multi-chip :chips="result.languages.map(language => new
                    ChipData(language, ActionType.Secondary))" :displayLimit="2"/>
            </td>
        </tr>
        </tbody>
    </table>

</template>

<script setup lang="ts">

import {type PropType, watch} from "vue";
import {FeedbackAnalysisResult} from "@/modules/feedbackAnalysisResults/models/FeedbackAnalysisResult";
import AppChip from "@/common/components/appChip/AppChip.vue";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import MultiChip from "@/common/components/multiChip/MultiChip.vue";
import {ChipData} from "@/common/components/multiChip/ChipData";
import {ActionType} from "@/common/components/actions/ActionType";
import {OpinionType} from "@/modules/feedbackAnalysisResults/models/OpinionType";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import AppButton from "@/common/components/appButton/AppButton.vue";
import {WorkflowStatus} from "@/modules/workflows/models/WorkflowStatus";

const props = defineProps({
    results: {
        type: Object as PropType<Array<FeedbackAnalysisResult>>,
        required: true
    }
});

const emit = defineEmits<
    {
        (e: 'closeOthers', resultId: string): void
        (e: 'openFeedbackResult', feedbackResultId: string): void
    }>();

const headers = ['Actions', 'Username', 'Status', 'Relevance', 'Opinion', 'Impact Score', 'Languages'];

watch(() => props.results, () => {
    props.results?.forEach(result => result.map());
});

</script>