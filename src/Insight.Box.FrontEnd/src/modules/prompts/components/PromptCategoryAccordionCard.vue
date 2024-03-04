<template>

    <div class="w-full h-[200px] flex card card-bg card-round card-shadow text-secondaryContentColor">

        <!-- Prompt category details -->
        <div class="flex flex-col items-center justify-center w-full">
            <h5 class="text-xl">{{ promptCategory.typeDisplayName }}</h5>
            <h5 class="mt-5 text-sm"> {{ LayoutConstants.Versions }} : {{ promptCategory.promptsCount }}</h5>
        </div>

        <vertical-divider :type="DividerType.ContentLength"/>

        <!-- Prompt selection -->
        <div class="w-full p-5 flex flex-col">

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

            <div>
                <table class="table-auto">
                    <thead>
                    <tr>
                        <th class="p-2">Version</th>
                        <th class="p-2">Exc time</th>
                        <th class="p-2">Accuracy</th>
                    </tr>
                    </thead>
                    <tbody>
                    <tr>
                        <td class="p-2">2.1</td>
                        <td class="p-2">10s</td>
                        <td >68%</td>
                    </tr>
                    </tbody>
                </table>
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

const loadAllPromptVersionResults = (() => {
    console.log();
});

const selectedFilter = ref<DropDownValue | null>(null);
const filterDropDownValues = ref<Array<DropDownValue>>([
    {key: 'All', value: 'All'},
    {key: 'Active', value: 'Active'},
    {key: 'Inactive', value: 'Inactive'}
]);

</script>