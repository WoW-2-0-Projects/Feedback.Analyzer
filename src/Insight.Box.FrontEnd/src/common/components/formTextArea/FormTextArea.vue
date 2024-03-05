<template>

    <div
        class="h-[400px] flex theme-input-bg relative rounded-md
         theme-action-transition theme-action-shadow theme-action-content
        theme-action-border-round"
        @focusin="focus = true"
        @focusout="focus = false"
        :class="focus ? 'theme-input-border-focus' : 'theme-input-border'"
    >
        <!-- Prefix value -->
<!--        <span class="px-2.5 pb-2.5 pt-5 whitespace-nowrap" v-if="prefix">{{ prefix }}</span>-->

        <!-- Form input field -->
        <textarea name="input" :value="modelValue"
               @input="emit('update:modelValue', $event.target.value)"
               @keydown="onInput"
               class="w-full px-2.5 pb-2.5 pt-5 bg-transparent appearance-none focus:outline-none peer
                   theme-input-text theme-input-placeholder theme-action-transition text-base"
               :placeholder="placeholder"/>

        <!--         Form input label-->
        <label for="input" class="absolute top-4 transform -translate-y-4 scale-75 origin-[0] start-2.5
               theme-input-label theme-action-transition text-sm
               peer-focus:scale-75 peer-focus:-translate-y-4 rtl:peer-focus:translate-x-1/4
               rtl:peer-focus:left-auto peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0">
            {{ label }}
        </label>

        <!-- Show button for hidden fields -->
<!--        <button type="button" v-if="type === FormInputType.Password" @click="onTogglePasswordVisibility"-->
<!--                class="absolute top-4 right-4 text-sm theme-text-secondary">-->
<!--            {{ actualType === FormInputType.Password ? 'Show' : 'Hide' }}-->
<!--        </button>-->

    </div>

</template>

<script setup lang="ts">

import {computed, defineEmits, defineProps, ref, watch} from 'vue';
import {FormInputType} from "../formInput/FormInputType";

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
    // prefix: {
    //     type: String,
    //     default: ''
    // },
});

// const actualType = ref<FormInputType>(props.type === FormInputType.Number ? FormInputType.Text : props.type);
const focus = ref<boolean>(false);

const emit = defineEmits(['update:modelValue']);

// Watcher for prefix change
// watch(() => props.prefix, (newValue, oldValue) => {
//     if (newValue !== oldValue)
//         emit('update:modelValue', props.modelValue?.replace(oldValue, newValue));
// });

// Computed value without prefix
// const value = computed(() => {
//     return props.prefix ? props.modelValue?.replace(props.prefix, '') : props.modelValue;
// });

/* region Input actions */

// const onTogglePasswordVisibility = () => {
//     actualType.value = actualType.value === FormInputType.Password ? FormInputType.Text : FormInputType.Password;
// }

const onInput = (event: KeyboardEvent) => {
    const allowedKeys = ['Backspace', 'ArrowRight', 'ArrowUp', 'ArrowDown']
    const allowedShortcuts = ['a', 'c', 'x', 'v'];

    // Allow select, copy and paste shortcuts
    if (event.metaKey && allowedShortcuts.includes(event.key)) {
        return;
    }

    // if (props.type === FormInputType.Number && !allowedKeys.includes(event.key) && (event.key.length === 1 &&
    //     isNaN(Number(event.key)))) {
    //     event.preventDefault();
    // }
}

/* endregion */

</script>

