<template>

    <div class="w-full flex gap-10 ">

        <!-- Add organization button -->
        <app-button :type="ButtonType.Primary" text="Add organization" icon="fas fa-plus"/>

        <!-- Search organization input -->
        <search-bar-input v-model="organizationFilter.searchKeyword" class="flex-grow"
                          label="Search" placeholder="Search organizations"
        />

        <!-- Filter organization drop down -->
        <form-drop-down label="Filter by" model-value="selectedFilter" values="filterDropDownValues"/>

        <!-- Sort organization drop down -->
        <form-drop-down label="Sort by" model-value="selectedFilter" values="filterDropDownValues"/>

    </div>

</template>
<script setup lang="ts">
import AppButton from "@/common/components/appButton/AppButton.vue";
import {ButtonType} from "@/common/components/appButton/ButtonType";
import SearchBarInput from "@/common/components/formSearchBar/FormSearchBar.vue";
import {OrganizationFilter} from "@/modules/organizations/models/OrganizationFilter";
import {ref} from "vue";
import FormDropDown from "@/common/components/formDropDown/FormDropDown.vue";
import {DropDownValue} from "@/common/components/formDropDown/DropDownValue";

const organizationFilter = ref<OrganizationFilter>(new OrganizationFilter());

const selectedFilter = ref<DropDownValue | null>(null);
const filterDropDownValues = ref<Array<DropDownValue>>([
    {key: 'All', value: 'All'},
    {key: 'Active', value: 'Active'},
    {key: 'Inactive', value: 'Inactive'}
]);

</script>