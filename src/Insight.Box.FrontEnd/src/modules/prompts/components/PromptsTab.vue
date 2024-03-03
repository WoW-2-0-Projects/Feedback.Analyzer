<template>

    <!-- Products tab content -->
    <div class="tab pt-10">

        <!-- Products search bar -->
        <prompts-search-bar :productsQuery="productsQuery"/>

        <!-- Products gallery -->
        <infinite-scroll @onScroll="onScroll"
                         :contentChangeSource="productsChangeSource"
                         class="grid w-full px-20 gap-x-5 gap-y-10 grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 theme-bg-primary">

            <!-- Products card -->

        </infinite-scroll>

    </div>

</template>

<script setup lang="ts">

import {onBeforeMount, onMounted, ref} from "vue";
import ProductsSearchBar from "@/modules/products/components/ProductsSearchBar.vue";
import InfiniteScroll from "@/common/components/infiniteScroll/InfiniteScroll.vue";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import {NotificationSource} from "@/infrastructure/models/notifications/Action";
import {Query} from "@/infrastructure/models/query/Query";
import {ProductFilter} from "@/modules/products/models/ProductFilter";
import type {Product} from "@/modules/products/models/Product";
import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import {DocumentService} from "@/infrastructure/services/document/DocumentService";
import PromptsSearchBar from "@/modules/prompts/components/PromptsSearchBar.vue";

/* Services */
const insightBoxApiClient = new InsightBoxApiClient();
const documentService = new DocumentService();

/* States */
const isLoading = ref<boolean>(false);
const productsQuery = ref<Query>(new Query(new ProductFilter()));
const products = ref<Array<Product>>([]);
const noProductsFound = ref<boolean>(false);
const productsChangeSource = ref<NotificationSource>(new NotificationSource());

onBeforeMount(async () => {
    // Set page title
    documentService.setTitle(LayoutConstants.Organizations);

    // Load products
    await loadProductsAsync();
});

/*
 * Loads products
 */
const loadProductsAsync = async () => {
    isLoading.value = true;

    const response = await insightBoxApiClient.products.getAsync(productsQuery.value);

    if (response.response) {
        products.value.push(...response.response);
    } else if (response.status == 404 || response.status == 204) {
        noProductsFound.value = true;
    }

    isLoading.value = false;
};

/*
 * Scroll event handler
 */
const onScroll = async () => {
};


</script>