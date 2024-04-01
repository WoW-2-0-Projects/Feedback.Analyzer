<template>

    <div @mouseenter="isHover = true" @mouseleave="isHover = false"
         class="relative bg-[#5d6285] flex flex-col items-center justify-around pt-3 rounded-3xl w-[290px] h-[192px] shadow-lg">

        <!-- organization image -->
        <img class="rounded-full w-24 h-24 bg-[#878aa3]"
             src="https://www.logolynx.com/images/logolynx/s_58/585472b8c0bbdc5171ac72fa9478131e.jpeg">
        <h3 class="text-white">{{ product.name }}</h3>
        <!--        <div class="w-full pl-3 text-white" >
                    <span>{{ /*product.productsCount */}} Products</span>
                </div>-->

        <div :class="isHover ? '' : 'hidden'" class="absolute top-4 right-4 flex flex-col gap-2">

            <app-button :type="ActionType.Danger" :layout="ButtonLayout.Rectangle"
                        icon="fas fa-minus" :size="ActionComponentSize.ExtraSmall"
                        @click="emit('delete', product.id)"/>

            <app-button :type="ActionType.Secondary" :layout="ButtonLayout.Rectangle"
                        icon="fas fa-edit" :size="ActionComponentSize.ExtraSmall"
                        @click="emit('edit', product)"/>

        </div>

    </div>

</template>

<script setup lang="ts">
import AppButton from "@/common/components/appButton/AppButton.vue";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import {type PropType, ref} from "vue";
import type {Product} from "@/modules/products/models/Product";
import {ActionType} from "@/common/components/actions/ActionType";

const isHover = ref<boolean>(false);

const props = defineProps({
    product: {
        type: Object as PropType<Product>,
        required: true
    }
});

const emit = defineEmits<
    {
        (e: 'edit', product: Product): void,
        (e: 'delete', productId:string): void
    }>();

</script>