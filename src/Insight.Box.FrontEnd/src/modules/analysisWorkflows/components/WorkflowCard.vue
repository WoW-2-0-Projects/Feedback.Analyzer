<template>

    <expanding-card-base :isExpanded="isOpen"
                         :mainContentHeight="100"
                         :expandingContentHeight="300"
    >
        <template v-slot:mainContent>

            <!-- Prompt category details -->
            <div class="flex flex-col items-center justify-center w-full">

                <div class="flex">
                    <expand-button :isExpanded="isOpen" @click="isOpen = !isOpen"/>
                    <h5 class="text-xl">{{ workflow.name }}</h5>
                </div>

            </div>

            <vertical-divider :type="DividerType.ContentLength"/>

            <!-- Prompt selection -->
            <div class="w-full p-2 py-5 flex flex-col item-center">

                <app-button :type="ButtonType.Success" :layout="ButtonLayout.Rectangle" icon="fas fa-play"
                            text="Trigger"
                            :size="ActionComponentSize.Small" @click="onTriggerWorkflow"/>

            </div>

            <vertical-divider :type="DividerType.ContentLength"/>

            <!-- Prompt execution result -->
            <div class="w-full p-2 py-5 flex flex-col">

            </div>

        </template>

        <template v-slot:expandingContent>

            <div v-for="test in 10">hi</div>

        </template>

    </expanding-card-base>

<!--    <div class="w-full flex flex-col card card-bg card-round card-shadow text-secondaryContentColor">-->

<!--        <div class="h-[100px] flex card-shadow">-->

<!--        </div>-->

<!--        <div class="w-full transition-all ease-in-out duration-1000" :class="isOpen ? 'h-[300px]' :'h-0 hidden'">-->


<!--        </div>-->

<!--    </div>-->

</template>

<script setup lang="ts">

import {type PropType, ref} from "vue";
import {ButtonType} from "@/common/components/appButton/ButtonType";
import AppButton from "@/common/components/appButton/AppButton.vue";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import VerticalDivider from "@/common/components/divider/VerticalDivider.vue";
import {DividerType} from "@/common/components/divider/DividerType";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import {FeedbackAnalysisWorkflow} from "@/modules/prompts/models/FeedbackAnalysisWorkflow";
import ExpandIcon from "@/common/components/icons/ExpandIcon.vue";
import {ExpandIconStyle} from "@/common/components/icons/ExpandIconStyle";
import ExpandButton from "@/common/components/buttons/ExpandButton.vue";
import ExpandingCardBase from "@/common/components/expandingCardBase/ExpandingCardBase.vue";

const isOpen = ref<boolean>(false);
const insightBoxApiClient = new InsightBoxApiClient();

const props = defineProps({
    workflow: {
        type: Object as PropType<FeedbackAnalysisWorkflow>,
        required: true
    }
});

const onTriggerWorkflow = async () => {
    const response = await insightBoxApiClient.workflows
        .executeWorkflowAsync(props.workflow?.id);
}

</script>