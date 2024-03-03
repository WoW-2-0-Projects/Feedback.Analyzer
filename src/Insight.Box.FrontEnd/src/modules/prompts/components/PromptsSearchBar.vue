<template>

    <form class="w-full flex gap-10">

        <!-- Add Prompt button -->
        <app-button :type="ButtonType.Primary" text="Add prompt" icon="fas fa-plus" @click="emit('addPrompt')"/>

        <!-- Search Prompts input -->
        <form-search-bar v-model="promptsQuery.filter.searchKeyword" class="flex-grow"
                         label="Search" placeholder="Search prompts"
        />

        <!-- Filter prompt drop down -->
        <form-drop-down label="Filter by" v-model="selectedFilter" :values="filterDropDownValues"/>

        <!-- Sort prompt drop down -->
        <form-drop-down label="Sort by" v-model="selectedFilter" :values="filterDropDownValues"/>

    </form>

</template>

<script setup lang="ts">

import {type PropType, defineEmits, ref} from "vue";
import AppButton from "@/common/components/appButton/AppButton.vue";
import {ButtonType} from "@/common/components/appButton/ButtonType";
import FormDropDown from "@/common/components/formDropDown/FormDropDown.vue";
import {DropDownValue} from "@/common/components/formDropDown/DropDownValue";
import FormSearchBar from "@/common/components/formSearchBar/FormSearchBar.vue";
import type {Query} from "@/infrastructure/models/query/Query";
import type {PromptFilter} from "@/modules/prompts/models/PromptFilter";

const props = defineProps({
    promptsQuery: {
        type: Object as PropType<Query<PromptFilter>>,
        required: true
    },
    isLoading: {
        type: Boolean,
        default: false
    }
});

const emit = defineEmits<{
    (e: 'addPrompt'): void
}>();

const selectedFilter = ref<DropDownValue | null>(null);
const filterDropDownValues = ref<Array<DropDownValue>>([
    {key: 'All', value: 'All'},
    {key: 'Active', value: 'Active'},
    {key: 'Inactive', value: 'Inactive'}
]);

</script>