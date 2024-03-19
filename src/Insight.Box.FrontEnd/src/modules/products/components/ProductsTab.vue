<template>

    <!-- Products tab content -->
    <div class="tab pt-10">

        <!-- Products search bar -->
        <products-search-bar :productsQuery="productsQuery"
                             @addProduct="openProductModal(null)"/>

        <div class="mt-4  flex flex-wrap justify-center gap-5">
            <product-card v-for="product in products"
                               @edit="openProductModal" @delete="onDeleteProductAsync"
                               :product="product" :key="product.id"></product-card>
        </div>

        <!-- Product create / edit modal -->
        <product-modal :isActive="productModalActive" @closeModal="productModalActive = false"
                       @submit="onProductModalSubmit" :isCreate="isCreate"
                       :product="modalProduct"
        />

        <confirmation-modal text="Are you sure you want to delete it?" :modalOptions="deleteConfirmationDialog"/>

    </div>

</template>

<script setup lang="ts">

import {onBeforeMount, ref} from "vue";
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import ProductsSearchBar from "@/modules/products/components/ProductsSearchBar.vue";
import ProductModal from "@/modules/products/components/ProductModal.vue";
import {InsightBoxApiClient} from "@/infrastructure/apiClients/insightBoxClient/brokers/InsightBoxApiClient";
import {DocumentService} from "@/infrastructure/services/document/DocumentService";
import {NotificationSource} from "@/infrastructure/models/notifications/Action";
import {Query} from "@/infrastructure/models/query/Query";
import {ProductFilter} from "@/modules/products/models/ProductFilter";
import {CreateProductCommand} from "@/modules/products/models/CreateProductCommand";
import ProductCard from "@/modules/products/components/ProductCard.vue";
import {UpdateProductCommand} from "@/modules/products/models/UpdateProductCommand";
import {Product} from "@/modules/products/models/Product";
import {useConfirmDialog} from "@vueuse/core";
import ConfirmationModal from "@/common/components/confirmationModal/ConfirmationModal.vue";


/* Services */
const insightBoxApiClient = new InsightBoxApiClient();
const documentService = new DocumentService();

/* States */
const isLoading = ref<boolean>(false);
const productsQuery = ref<Query<any>>(new Query(new ProductFilter()));
const products = ref<Array<Product>>([]);
const noProductsFound = ref<boolean>(false);

/* Product modal states  */
const productModalActive = ref<boolean>(false);
const productsChangeSource = ref<NotificationSource<any>>(new NotificationSource());
const modalProduct = ref<Product>(new Product());
const deleteConfirmationDialog = ref(useConfirmDialog());

/* Search bar states */
const isSearchBarLoading = ref<boolean>(false);
const isCreate = ref<boolean>(true);

onBeforeMount(async () => {
    // Set page title
    documentService.setTitle(LayoutConstants.Products);

    // Load products
    await loadProductsAsync();
});

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

const openProductModal = async (product: Product | null) => {
    if (product == null){
        modalProduct.value = new Product();
        isCreate.value = true;
    }else{
        modalProduct.value = product;
        isCreate.value = false;
    }

    productModalActive.value = true;
};

const onProductModalSubmit = async(product: Product) => {
    if(isCreate.value) {
        await createProductAsync(product);
    } else {
        await updateProductAsync(product);
    }
}

const createProductAsync = async (product: Product) => {
    isSearchBarLoading.value = true;

    product.organizationId = 'c2fe1019-1180-4f3e-b477-413a9b33bbd1';
    const createProductCommand = new CreateProductCommand(product);
    const response = await insightBoxApiClient.products.createAsync(createProductCommand);

    if (response.response) {
        products.value.push(response.response);
    }

    isSearchBarLoading.value = false;
};

const updateProductAsync = async (product: Product) => {
    isSearchBarLoading.value = true;

    const updateProductCommand = new UpdateProductCommand(product)

    const response = await insightBoxApiClient.products.updateAsync(updateProductCommand);

    if (response.response) {
        const productId = products.value.findIndex(product => product.id == response.response?.id);
        if (productId > -1)
        {
            products.value[productId] = response.response;
        }
    }

    isSearchBarLoading.value = false;
};

const onDeleteProductAsync = async (productId: string) => {
    const data = (await deleteConfirmationDialog.value.reveal()).data;

    if (data){
        isSearchBarLoading.value = true;

        const response = await
            insightBoxApiClient.products.deleteAsync(productId);

        if (response.isSuccess) {
            const resultId: number = products.value.findIndex(product => product.id == productId);

            if (resultId > -1)
                products.value.splice(resultId, 1);
        }
    }

    isSearchBarLoading.value = true;
}

</script>