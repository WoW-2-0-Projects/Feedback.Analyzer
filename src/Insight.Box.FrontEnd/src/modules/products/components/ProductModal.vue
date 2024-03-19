<template>

    <ModalBase :isActive="props.isActive" @closeModal="emit('closeModal')">

        <template v-slot:header>
            <h1 class="text-primaryContentColor text-xl">
                {{
                    isCreate ? LayoutConstants.CreateProduct :
                        LayoutConstants.EditProduct + ' ' + product.name
                }}
            </h1>
        </template>

        <!-- Product modal content -->
        <template v-slot:content>

            <div class="modal-content-padding modal-content-layout">

                <form class="flex flex-col gap-10" @submit.prevent="onSubmit">

                    <!-- Modal inputs -->
                    <form-input v-model="product.name" :label="LayoutConstants.ProductName"
                                :placeholder="LayoutConstants.EnterProductName"/>

                    <form-input v-model="product.description" :label="LayoutConstants.ProductDescription"
                                :placeholder="LayoutConstants.EnterProductDescription"/>

                    <!-- Modal actions -->
                    <div class="flex gap-10">
                        <app-button :type="ButtonType.Secondary" class="w-full" :text="LayoutConstants.Cancel"
                                    @click="emit('closeModal')"/>
                        <app-button :type="ButtonType.Primary" class="w-full" :text="LayoutConstants.Submit"
                                    :role="ButtonRole.Submit" @click="onSubmit"/>
                    </div>

                </form>
            </div>

        </template>

    </ModalBase>

</template>

<script setup lang="ts">

import {defineEmits, type PropType} from 'vue';
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import AppButton from "@/common/components/appButton/AppButton.vue";
import {ButtonType} from "@/common/components/appButton/ButtonType";
import FormInput from "@/common/components/formInput/FormInput.vue";
import ModalBase from "@/common/components/modalBase/ModalBase.vue";
import {Product} from "@/modules/products/models/Product";
import {ButtonRole} from "@/common/components/appButton/ButtonRole";

const props = defineProps({
    product: {
        type: Object as PropType<Product>,
        default: new Product()
    },
    isActive: {
        type: Boolean,
        default: false
    },
    isCreate: {
        type: Boolean,
        default: true
    }
});

const emit = defineEmits<{
    (e: 'closeModal'): void
    (e: 'submit', product: Product): void
}>();

const onSubmit = async () => {
    emit('submit', props.product);
    emit('closeModal');
}

</script>