<template>

    <div class="w-full h-[200px] flex card card-bg card-round card-shadow">

        <!-- Prompt category details -->
        <div class="flex flex-col items-center justify-center w-full text-secondaryContentColor">
            <h5 class="text-xl">{{ promptCategory.typeDisplayName }}</h5>
            <h5 class="mt-5 text-sm"> {{ LayoutConstants.Versions }} : {{ promptCategory.promptsCount }}</h5>
        </div>

        <vertical-divider :type="DividerType.ContentLength"/>

        <!-- Prompt selection -->
        <div class="w-full p-5">

            <div class="flex gap-2">

                <!-- Filter product drop down -->
                <form-drop-down label="Filter by" v-model="selectedFilter" :values="filterDropDownValues"
                                :size="ActionComponentSize.Mini"
                />

                <!-- Sort product drop down -->
                <form-drop-down label="Sort by" v-model="selectedFilter" :values="filterDropDownValues"
                                :size="ActionComponentSize.Mini"
                />

                <!-- Add prompt button -->
                <app-button :type="ButtonType.Primary" :layout="ButtonLayout.Square" icon="fas fa-plus"
                            :size="ActionComponentSize.Mini" @click="emit('addPrompt', promptCategory)"/>

            </div>


        </div>

        <vertical-divider :type="DividerType.ContentLength"/>

        <div class="w-full">hi</div>

    </div>

</template>

<script setup lang="ts">

import {onBeforeMount, type PropType, ref} from "vue";
import type {AnalysisPromptCategory} from "@/modules/prompts/models/AnalysisPromptCategory";
import {ButtonType} from "@/common/components/appButton/ButtonType";
import AppButton from "@/common/components/appButton/AppButton.vue";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import VerticalDivider from "@/common/components/divider/VerticalDivider.vue";
import {DividerType} from "@/common/components/divider/DividerType";
import FormDropDown from "@/common/components/formDropDown/FormDropDown.vue";
import {DropDownValue} from "@/common/components/formDropDown/DropDownValue";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import {LayoutConstants} from "../../../common/constants/LayoutConstants";

const insightBoxApiClient = new InsightBoxApiClient();

const props = defineProps({
    promptCategory: {
        type: Object as PropType<AnalysisPromptCategory>,
        required: true
    }
});

const emit = defineEmits<{
    (e: 'addPrompt', promptCategory: AnalysisPromptCategory): void
}>();

onBeforeMount(() => {

});

const selectedFilter = ref<DropDownValue | null>(null);
const filterDropDownValues = ref<Array<DropDownValue>>([
    {key: 'All', value: 'All'},
    {key: 'Active', value: 'Active'},
    {key: 'Inactive', value: 'Inactive'}
]);

</script>