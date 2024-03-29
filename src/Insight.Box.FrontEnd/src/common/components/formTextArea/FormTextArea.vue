<template>

    <div
        class="h-[400px] flex theme-input-bg relative rounded-md
         theme-action-transition theme-action-shadow theme-action-content
        theme-action-border-round"
        @focusin="onFocusIn"
        @focusout="onFocusOut"
        :class="focus ? 'theme-input-border-focus' : 'theme-input-border'"
    >
        <!-- Prefix value -->
<!--        <span class="px-2.5 pb-2.5 pt-5 whitespace-nowrap" v-if="prefix">{{ prefix }}</span>-->

        <!-- Form input field -->
        <textarea ref="textArea" name="input" :value="modelValue"
               @input="emit('update:modelValue', $event.target.value)"
               @keydown="onInput" :disabled="disabled"
               class="w-full px-2.5 pb-2.5 pt-5 no-scrollbar bg-transparent appearance-none focus:outline-none peer
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

import {computed, defineEmits, defineProps, onBeforeMount, onMounted, type PropType, ref, watch} from 'vue';
import {FormInputType} from "../formInput/FormInputType";
import {AnalysisPrompt} from "@/modules/prompts/models/AnalysisPrompt";
import type {Action} from "@/infrastructure/models/delegates/Action";

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
    disabled: {
        type: Boolean,
        default: false
    },
    border: {
        type: Boolean,
        default: true
    },
    focus: {
        type: Object as PropType<Action>,
    }
    // prefix: {
    //     type: String,
    //     default: ''
    // },
});

// const actualType = ref<FormInputType>(props.type === FormInputType.Number ? FormInputType.Text : props.type);
const textArea = ref<HTMLTextAreaElement>();
const focus = ref<boolean>(false);

onBeforeMount(() => {
    if(props.focus)
        props.focus.callback = () =>{
            textArea.value.focus();
        };
})

// const emit = defineEmits(['update:modelValue']);

const emit = defineEmits<{
    (e: 'focusIn'): void,
    (e: 'focusOut'): void,
    (e: 'update:modelValue', string: value): void
}>();

const onFocusIn = () => {
    focus.value = true;
    emit('focusIn');
}

const onFocusOut = () => {
    focus.value = false;
    emit('focusOut');
}

const onInput = (event: KeyboardEvent) => {
    const allowedKeys = ['Backspace', 'ArrowRight', 'ArrowUp', 'ArrowDown']
    const allowedShortcuts = ['a', 'c', 'x', 'v'];

    // Allow select, copy and paste shortcuts
    if (event.metaKey && allowedShortcuts.includes(event.key)) {
        return;
    }
}

/* endregion */

</script>

