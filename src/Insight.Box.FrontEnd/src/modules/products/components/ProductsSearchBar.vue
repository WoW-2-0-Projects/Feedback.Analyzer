<template>

    <div class="w-full flex gap-10">

        <!-- Add product button -->
        <app-button :type="ButtonType.Primary" text="Add product" icon="fas fa-plus" @click="emit('addProduct')"/>

        <!-- Search products input -->
        <search-bar-input v-model="productFilter.searchKeyword" class="flex-grow"
                          label="Search" placeholder="Search products"/>

        <!-- Filter product drop down -->
        <form-drop-down label="Filter by" v-model="selectedFilter" :values="filterDropDownValues"/>

        <!-- Sort product drop down -->
        <form-drop-down label="Sort by" v-model="selectedFilter" :values="filterDropDownValues"/>

    </div>

</template>

<script setup lang="ts">

import AppButton from "@/common/components/appButton/AppButton.vue";
import {ButtonType} from "@/common/components/appButton/ButtonType";
import SearchBarInput from "@/common/components/formSearchBar/FormSearchBar.vue";
import {ref} from "vue";
import FormDropDown from "@/common/components/formDropDown/FormDropDown.vue";
import {DropDownValue} from "@/common/components/formDropDown/DropDownValue";
import {ProductFilter} from "@/modules/products/models/ProductFilter";

const productFilter = ref<ProductFilter>(new ProductFilter());


const emit = defineEmits<{
    (e: 'addProduct'): void
}>();

const selectedFilter = ref<DropDownValue<any, any> | null>(null);
const filterDropDownValues = ref<Array<DropDownValue<any, any>>>([
    {key: 'All', value: 'All'},
    {key: 'Active', value: 'Active'},
    {key: 'Inactive', value: 'Inactive'}
]);

</script>