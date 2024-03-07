<template>

    <div class="w-full h-[100px] flex card card-bg card-round card-shadow text-secondaryContentColor">

        <!-- Prompt category details -->
        <div class="flex flex-col items-center justify-center w-full">
            <h5 class="text-xl">{{ workflow.name }}</h5>
        </div>

        <vertical-divider :type="DividerType.ContentLength"/>

        <!-- Prompt selection -->
        <div class="w-full p-2 py-5 flex flex-col item-center">

            <app-button :type="ButtonType.Success" :layout="ButtonLayout.Rectangle" icon="fas fa-play"
                        text="Trigger"
                        :disabled="!workflow.allPromptsSet"
                        :size="ActionComponentSize.Small" @click="onTriggerWorkflow"/>

        </div>

        <vertical-divider :type="DividerType.ContentLength"/>

        <!-- Prompt execution result -->
        <div class="w-full p-2 py-5 flex flex-col">

        </div>

    </div>

</template>

<script setup lang="ts">

import {type PropType} from "vue";
import {ButtonType} from "@/common/components/appButton/ButtonType";
import AppButton from "@/common/components/appButton/AppButton.vue";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import VerticalDivider from "@/common/components/divider/VerticalDivider.vue";
import {DividerType} from "@/common/components/divider/DividerType";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import AppTable from "@/common/components/appTable/AppTable.vue";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import {FeedbackAnalysisWorkflow} from "@/modules/prompts/models/FeedbackAnalysisWorkflow";
import FormDropDown from "@/common/components/formDropDown/FormDropDown.vue";

const insightBoxApiClient = new InsightBoxApiClient();

const props = defineProps({
    workflow: {
        type: Object as PropType<FeedbackAnalysisWorkflow>,
        required: true
    }
});

const onTriggerWorkflow = async () => {
    if (!selectedWorkflow.value?.value?.id || !props.promptCategory?.selectedPromptId)
        return;

    // const executeSinglePromptCommand = new ExecuteSinglePromptCommand();
    const response = await insightBoxApiClient.workflows
        .executeSinglePrompt(selectedWorkflow.value?.value?.id!, props.promptCategory?.selectedPromptId!);
}

</script>