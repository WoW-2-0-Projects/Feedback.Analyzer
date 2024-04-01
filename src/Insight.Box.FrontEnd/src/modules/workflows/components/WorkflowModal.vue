<template>

    <ModalBase :isActive="props.isActive" @closeModal="onClose">

        <template v-slot:header>
            <h1 class="text-primaryContentColor text-xl">
                {{ isCreate ? LayoutConstants.CreateWorkflow : LayoutConstants.EditWorkflow }}
            </h1>
        </template>

        <!-- Workflow modal content -->
        <template v-slot:content>

            <div class="modal-content-padding modal-content-layout">

                <form class="flex flex-col gap-10" @submit.prevent="onSubmit">

                    <!-- Base workflow drop down -->
                    <form-drop-down v-if="isCreate" :label="LayoutConstants.BaseWorkflow" v-model="baseWorkflow"
                                    :values="baseWorkflowOptions" :placeholder="LayoutConstants.SelectBaseWorkflow"
                                    class="w-full"/>

                    <!-- Product drop down -->
                    <form-drop-down v-if="isCreate" class="w-full" :values="productOptions"
                                    :label="LayoutConstants.ProductToAnalyze" v-model="product"
                                    :placeholder="LayoutConstants.SelectBaseWorkflow"
                                    />

                    <!-- Modal inputs -->
                    <form-input v-model="workflow.name" :label="LayoutConstants.WorkflowName"
                                :placeholder="LayoutConstants.EnterWorkflowName"/>


                    <!-- Modal actions -->
                    <div class="flex gap-10">
                        <app-button :type="ActionType.Secondary" class="w-full" :text="LayoutConstants.Cancel"
                                    @click="onClose"/>
                        <app-button :type="ActionType.Primary" class="w-full" :text="LayoutConstants.Submit"
                                    :role="ButtonRole.Submit" @click="onSubmit"/>
                    </div>

                </form>
            </div>

        </template>

    </ModalBase>

</template>

<script setup lang="ts">

import {computed, type PropType, ref} from 'vue';
import {LayoutConstants} from "@/common/constants/LayoutConstants";
import AppButton from "@/common/components/appButton/AppButton.vue";
import FormInput from "@/common/components/formInput/FormInput.vue";
import ModalBase from "@/common/components/modalBase/ModalBase.vue";
import {ButtonRole} from "@/common/components/appButton/ButtonRole";
import type {FeedbackAnalysisWorkflow} from "@/modules/workflows/models/FeedbackAnalysisWorkflow";
import FormDropDown from "@/common/components/formDropDown/FormDropDown.vue";
import {DropDownValue} from "@/common/components/formDropDown/DropDownValue";
import type {Product} from "@/modules/products/models/Product";
import {ActionType} from "@/common/components/actions/ActionType";

const props = defineProps({
    workflow: {
        type: Object as PropType<FeedbackAnalysisWorkflow>,
        required: true
    },
    workflows: {
        type: Array as PropType<Array<FeedbackAnalysisWorkflow>>,
        required: true
    },
    products: {
        type: Array as PropType<Array<Product>>,
        required: true
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
    (e: 'submit', workflow: FeedbackAnalysisWorkflow, baseWorkflow: FeedbackAnalysisWorkflow | null): void
}>();

// Base workflow drop down states
const baseWorkflowOptions = computed(() => props.workflows?.map(workflow =>
    DropDownValue.create(workflow.name, workflow)));
const baseWorkflow = ref<DropDownValue<string, FeedbackAnalysisWorkflow> | null>(null);

// Product drop down states
const productOptions = computed(() => props.products?.map(product =>
    DropDownValue.create(product.name, product)));
const product = ref<DropDownValue<string, Product> | null>(null);

const onSubmit = async () => {




    props.workflow.productId = product!.value!.value!.id;
    emit('submit', props.workflow, props.isCreate ? baseWorkflow!.value!.value! : null);

    onClose();
}

const onClose = () => {
    baseWorkflow.value = null;
    product.value = null;
    emit('closeModal');
}

</script>