
<template>

    <form class="w-full flex gap-10">

        <!-- Search Prompts input -->
        <form-search-bar v-model="promptsQuery.filter.searchKeyword" class="flex-grow"
                label="Search" placeholder="Search prompts"
        />

        <!-- Filter prompt drop down -->
        <form-drop-down label="Filter by" v-model="selectedFilter" :values="filterDropDownValues"/>

        <!-- Search prompt drop down -->
        <form-drop-down label="Sort by" v-model="selectedFilter" :values="filterDropDownValues"/>
    </form>

</template>


<script setup lang="ts">

import FormSearchBar from "@/common/components/formSearchBar/FormSearchBar.vue";
import {type PropType, ref} from "vue";
import type {Query} from "@/infrastructure/models/query/Query";
import type {PromptFilter} from "@/modules/prompts/models/PromptFilter";
import FormDropDown from "@/common/components/formDropDown/FormDropDown.vue";
import type {DropDownValue} from "@/common/components/formDropDown/DropDownValue";

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

const selectedFilter = ref<DropDownValue | null>(null);
const filterDropDownValues = ref<Array<DropDownValue | null>>([
    {key: 'All', value: 'All'},
    {key: 'Active', value: 'Active'},
    {key: 'Inactive', value: 'Inactive'}
]);

</script>
