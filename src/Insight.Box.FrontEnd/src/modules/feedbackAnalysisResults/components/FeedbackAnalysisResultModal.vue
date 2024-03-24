<template>

    <ModalBase :isActive="props.isActive" @closeModal="onClose">

        <template v-slot:header>

            <h1 class="text-primaryContentColor text-xl">{{ LayoutConstants.FeedbackAnalysisResult }}</h1>

        </template>

        <!-- Workflow modal content -->
        <template v-slot:content>

            <div class="modal-content-padding modal-content-layout">

                <!-- Overview -->
                <div class="flex items-start justify-between text-secondaryContentColor">

                    <div class="flex flex-col gap-3 justify-center items-center">
                        <h5 class="text-sm"> {{ LayoutConstants.Relevance }}</h5>
                        <app-chip :text="result.isRelevant
                                      ? LayoutConstants.Relevant
                                      : LayoutConstants.NonRelevant"
                                  :type="result.isRelevant
                                      ? ActionType.Success
                                      : ActionType.Danger"/>
                    </div>

                    <div class="flex flex-col gap-3 justify-center items-center">
                        <h5 class="text-sm"> {{ LayoutConstants.Status }}</h5>
                        <app-chip :text="WorkflowStatus[result.status]"
                                  :type="result.mapStatusToActionType()"/>
                    </div>

                    <div class="flex flex-col gap-3 justify-center items-center">
                        <h5 class="text-sm"> {{ LayoutConstants.Opinion }}</h5>
                        <app-chip :text="OpinionType[result.opinion]"
                                  :type="result.mapOpinionToActionType()"/>
                    </div>

                    <div class="flex flex-col gap-2 justify-center items-center text-tertiaryContentColor">
                        <h5 class="text-sm"> {{ LayoutConstants.AnalysisTime }}</h5>
                        <h5 class="text-2xl">
                            {{
                                result.modelExecutionDurationInMilliseconds < 10000 ?
                                    result.modelExecutionDurationInMilliseconds + 'ms' :
                                    (result.modelExecutionDurationInMilliseconds / 1000).toFixed(1) + 's'
                            }}
                        </h5>
                    </div>

                </div>

                <div class="mt-5 h-full flex flex-col">

                    <div class="flex items-center justify-start gap-2">
                        <avatar-circle :fullName="result.customerFeedback.userName"/>
                        <h5 class="text-base text-secondaryContentColor">{{result.customerFeedback.userName}}</h5>
                    </div>

                    <div class="bg-black bg-opacity-15 p-4 ml-10 rounded-tr-xl rounded-bl-xl rounded-br-xl">
                        <h5 class="text-sm text-primaryContentColor line-clamp-6 text-justify">
                            {{result.customerFeedback.comment}}
                        </h5>
                    </div>
                </div>

                <horizontal-divider class="my-5"/>

                <!-- Customer Opinions -->
                <div class="flex flex-col gap-5">

                    <!-- Questions -->
                    <div v-if="result.questions?.length > 0"
                         class="flex flex-col items-center justify-center gap-2">
                        <h5 class="text-sm text-secondaryContentColor text-center">{{LayoutConstants.Questions}}</h5>
                        <div class="flex gap-2 justify-center flex-wrap lowercase">
                            <multi-chip :chips="result.questions.map(question =>
                                new ChipData(question, ActionType.Secondary))" :displayRowLimit="3"/>
                        </div>
                    </div>

                    <!-- Negative Opinions -->
                    <div v-if="result.negativeOpinionPoints?.length > 0"
                         class="flex flex-col items-center justify-center gap-2">
                        <h5 class="text-sm text-secondaryContentColor text-center">{{LayoutConstants.Complaints}}</h5>
                        <div class="flex gap-2 justify-center flex-wrap lowercase">
                            <multi-chip :chips="result.negativeOpinionPoints.map(opinion =>
                                new ChipData(opinion, ActionType.Danger))" :displayRowLimit="3"/>
                        </div>
                    </div>

                    <!-- Ideas -->
                    <div v-if="result.ideas?.length > 0"
                         class="flex flex-col items-center justify-center gap-2">
                        <h5 class="text-sm text-secondaryContentColor text-center">{{LayoutConstants.Ideas}}</h5>
                        <div class="flex gap-2 justify-center flex-wrap lowercase">
                            <multi-chip :chips="result.ideas.map(idea =>
                                new ChipData(idea, ActionType.Processing))" :displayRowLimit="3"/>
                        </div>
                    </div>

                    <!-- Positive Opinions -->
                    <div v-if="result.positiveOpinionPoints?.length > 0"
                         class="flex flex-col items-center justify-center gap-2">
                        <h5 class="text-sm text-secondaryContentColor text-center">{{LayoutConstants.Compliments}}</h5>
                        <div class="flex gap-2 justify-center flex-wrap lowercase">
                            <multi-chip :chips="result.positiveOpinionPoints.map(opinion =>
                                new ChipData(opinion, ActionType.Success))" :displayRowLimit="3"/>
                        </div>
                    </div>

                </div>

            </div>

        </template>

    </ModalBase>

</template>

<script setup lang="ts">

import {defineEmits, type PropType, watch} from 'vue';
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import ModalBase from "@/common/components/modalBase/ModalBase.vue";
import {FeedbackAnalysisResult} from "@/modules/feedbackAnalysisResults/models/FeedbackAnalysisResult";
import HorizontalDivider from "@/common/components/dividers/HorizontalDivider.vue";
import AppChip from "@/common/components/appChip/AppChip.vue";
import {ActionType} from "@/common/components/actions/ActionType";
import AvatarCircle from "@/common/components/avatarCircle/AvatarCircle.vue";
import {OpinionType} from "@/modules/feedbackAnalysisResults/models/OpinionType";
import {WorkflowStatus} from "@/modules/workflows/models/WorkflowStatus";
import {ChipData} from "@/common/components/multiChip/ChipData";
import MultiChip from "@/common/components/multiChip/MultiChip.vue";

const props = defineProps({
    isActive: {
        type: Boolean,
        required: true
    },
    result: {
        type: Object as PropType<FeedbackAnalysisResult>,
        required: true
    },
});

const emit = defineEmits<{
    (e: 'closeModal'): void
}>();

const onClose = () => emit('closeModal');

</script>