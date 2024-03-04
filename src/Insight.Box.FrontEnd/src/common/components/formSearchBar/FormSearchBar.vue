<template>

    <form-input :type="FormInputType.Text" :modelValue="modelValue"
                @update:modelValue="($event) => $emit('update:modelValue', $event)"
                :label="label" :placeholder="placeholder" :border="border"
    />

</template>

<script setup lang="ts">

import FormInput from "@/common/components/formInput/FormInput.vue";
import {defineEmits, defineProps} from "vue";
import {FormInputType} from "@/common/components/formInput/FormInputType";

const props = defineProps({
    modelValue: {
        type: String,
        required: true
    },
    label: {
        type: String,
        default: ''
    },
    placeholder: {
        type: String,
        default: ''
    },
    border: {
        type: Boolean,
        default: true
    },
    searchedValues: {
        type: Array,
        default: []
    }
});

const emit = defineEmits(['update:modelValue']);

</script>