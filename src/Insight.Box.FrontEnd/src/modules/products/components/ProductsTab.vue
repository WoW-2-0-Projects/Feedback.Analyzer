<template>

    <!-- Products tab content -->
    <div class="tab pt-10">

        <!-- Products search bar -->
        <products-search-bar :productsQuery="productsQuery" @addProduct="openProductModal"/>

        <!-- Products gallery -->
        <infinite-scroll @onScroll="onScroll"
                         :contentChangeSource="productsChangeSource"
                         class="grid w-full px-20 gap-x-5 gap-y-10 grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 theme-bg-primary">

            <!-- Products card -->

        </infinite-scroll>

        <!-- Product create / edit modal -->
        <product-modal :isActive="productModalActive" @closeModal="closeProductModal"
                       @submit="createProductAsync($event)" :isCreate="isCreate"
        />

    </div>

</template>

<script setup lang="ts">

import {onBeforeMount, ref} from "vue";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import ProductsSearchBar from "@/modules/products/components/ProductsSearchBar.vue";
import InfiniteScroll from "@/common/components/infiniteScroll/InfiniteScroll.vue";
import ProductModal from "@/modules/products/components/ProductModal.vue";
import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import {DocumentService} from "@/infrastructure/services/document/DocumentService";
import {NotificationSource} from "@/infrastructure/models/notifications/Action";
import {Query} from "@/infrastructure/models/query/Query";
import {ProductFilter} from "@/modules/products/models/ProductFilter";
import type {Product} from "@/modules/products/models/Product";
import {Command} from "@/infrastructure/models/command/Command";
import {CreateProductCommand} from "@/modules/products/models/CreateProductCommand";

/* Services */
const insightBoxApiClient = new InsightBoxApiClient();
const documentService = new DocumentService();

/* States */
const isLoading = ref<boolean>(false);
const productsQuery = ref<Query>(new Query(new ProductFilter()));
const products = ref<Array<Product>>([]);
const noProductsFound = ref<boolean>(false);
const productsChangeSource = ref<NotificationSource>(new NotificationSource());
const productModalActive = ref<boolean>(false);

/* Search bar states */
const isSearchBarLoading = ref<boolean>(false);
const isCreate = ref<boolean>(true);

onBeforeMount(async () => {
    // Set page title
    documentService.setTitle(LayoutConstants.Products);

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
 * Handles product modal submit
 */
const onProductModalSubmit = async(product: Product) => {
    if(isCreate.value) {
        await createProductAsync(product);
    } else {
        await updateProductAsync(product);
    }
}

/*
 * Creates a product
 */
const createProductAsync = async (product: Product) => {
    isSearchBarLoading.value = true;

    product.organizationId = '60e6a4de-31e5-4f8b-8e6a-0a8f63f41527';
    const createProductCommand = new CreateProductCommand(product);
    const response = await
        insightBoxApiClient.products.createAsync(createProductCommand);

    if (response.response) {
        products.value.push(response.response);
    }

    isSearchBarLoading.value = false;
};

/*
 * Updates product
 */
const updateProductAsync = async (product: Product) => {
    isSearchBarLoading.value = true;

    isSearchBarLoading.value = false;
};

const openProductModal = (product: Product | null) => {
    isCreate.value = product === null || product === undefined;
    productModalActive.value = true;
};

const closeProductModal = () => {
    productModalActive.value = false;
};


/*
 * Scroll event handler
 */
const onScroll = async () => {
};


</script>