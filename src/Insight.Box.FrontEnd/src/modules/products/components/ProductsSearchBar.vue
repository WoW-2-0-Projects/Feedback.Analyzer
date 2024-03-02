<template>

    <form class="w-full flex gap-10">

        <!-- Add product button -->
        <app-button :type="ButtonType.Primary" text="Add product" icon="fas fa-plus" @click="emit('addProduct')"/>

        <!-- Search products input -->
        <form-search-bar v-model="productsQuery.filter.searchKeyword" class="flex-grow"
                          label="Search" placeholder="Search products"
        />

        <!-- Filter product drop down -->
        <form-drop-down label="Filter by" v-model="selectedFilter" :values="filterDropDownValues"/>

        <!-- Sort product drop down -->
        <form-drop-down label="Sort by" v-model="selectedFilter" :values="filterDropDownValues"/>

    </form>

</template>

<script setup lang="ts">

import {type PropType, defineEmits, ref} from "vue";
import AppButton from "@/common/components/appButton/AppButton.vue";
import {ButtonType} from "@/common/components/appButton/ButtonType";
import FormDropDown from "@/common/components/formDropDown/FormDropDown.vue";
import FormSearchBar from "@/common/components/formSearchBar/FormSearchBar.vue";
import {DropDownValue} from "@/common/components/formDropDown/DropDownValue";
import type {ProductFilter} from "@/modules/products/models/ProductFilter";
import type {Query} from "@/infrastructure/models/query/Query";
import {Product} from "@/modules/products/models/Product";

const props = defineProps({
    productsQuery: {
        type: Object as PropType<Query<ProductFilter>>,
        required: true
    },
    isLoading: {
        type: Boolean,
        default: false
    }
});

const emit = defineEmits<{
    (e: 'addProduct'): void
}>();

const selectedFilter = ref<DropDownValue | null>(null);
const filterDropDownValues = ref<Array<DropDownValue>>([
    {key: 'All', value: 'All'},
    {key: 'Active', value: 'Active'},
    {key: 'Inactive', value: 'Inactive'}
]);

</script>