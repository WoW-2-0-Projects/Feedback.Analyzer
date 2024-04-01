<template>

    <div
        class="flex theme-input-bg relative theme-action-transition
               theme-action-shadow theme-action-content theme-action-border-round"
        :class="wrapperStyles" @focusin="isFocused = true" @focusout="isFocused = false">

        <!-- Prefix value -->
        <span class="px-2.5 pb-2.5 pt-5 whitespace-nowrap" v-if="prefix">{{ prefix }}</span>

        <!-- Form input field -->
        <input :type="actualType" name="input" :value="value"
               @input="emit('update:modelValue', `${prefix}${($event?.target as HTMLInputElement)?.value}`)"
               @keydown="onInput" :disabled="disabled"
               class="w-full px-2.5 pb-2.5 pt-5 peer theme-input theme-input-text
                      theme-input-placeholder theme-action-transition text-base"
               :placeholder="placeholder"/>

        <label for="input" :class="labelStyles" class="absolute z-10 transform scale-75 origin-[0] start-2.5
                    peer-focus:scale-75 peer-focus:-translate-y-4 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto
                    peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0
                     theme-input-label theme-action-transition">
            {{ label }}
        </label>

        <!-- Show button for hidden fields -->
        <button type="button" v-if="type === FormInputType.Password" @click="onTogglePasswordVisibility"
                class="absolute top-4 right-4 text-sm theme-text-secondary">
            {{ actualType === FormInputType.Password ? 'Show' : 'Hide' }}
        </button>

    </div>

</template>

<script setup lang="ts">

import {computed, type PropType, ref, watch} from 'vue';
import {FormInputType} from "./FormInputType";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";

const props = defineProps({
    type: {
        type: String,
        default: FormInputType.Text
    },
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
    prefix: {
        type: String,
        default: ''
    },
    size: {
        type: Number as PropType<ActionComponentSize>,
        default: ActionComponentSize.Medium
    }
});

const wrapperStyles = computed(() => {
    let styles = '';

    if(isFocused.value) {
        styles += ' theme-input-border-focus';
    } else {
        styles += ' theme-input-border';
    }

// :class="focus ? 'theme-input-border-focus' : 'theme-input-border'"

    // Add button size styles
    switch (props.size) {
        case ActionComponentSize.Medium:
            styles += ' action-layout';
            break;
        case ActionComponentSize.Small:
            styles += ' action-small-layout';
            break;
        case ActionComponentSize.ExtraSmall:
            styles += ' action-extra-small-layout';
            break;
    }

    return styles;
});

const labelStyles = computed(() => {
    let styles = '';

    switch (props.size) {
        case ActionComponentSize.Medium:
            styles += ' text-md top-4 -translate-y-4 peer-focus:-translate-y-4';
            break;
        case ActionComponentSize.Small:
            styles += ' text-sm top-3 -translate-y-3 peer-focus:-translate-y-[14px]';
            break;
        case ActionComponentSize.ExtraSmall:
            styles += ' text-sm top-2 -translate-y-3 peer-focus:-translate-y-[10px]';
            break;
    }

    return styles;
});

const actualType = ref<string>(props.type == FormInputType.Number ? FormInputType.Text :
    props.type);
const isFocused = ref<boolean>(false);

const emit = defineEmits(['update:modelValue']);

// Watcher for prefix change
watch(() => props.prefix, (newValue, oldValue) => {
    if (newValue !== oldValue)
        emit('update:modelValue', props.modelValue?.replace(oldValue, newValue));
});

// Computed value without prefix
const value = computed(() => {
    return props.prefix ? props.modelValue?.replace(props.prefix, '') : props.modelValue;
});

/* region Input actions */

const onTogglePasswordVisibility = () => {
    actualType.value = actualType.value === FormInputType.Password ? FormInputType.Text : FormInputType.Password;
}

const onInput = (event: KeyboardEvent) => {
    const allowedKeys = ['Backspace', 'ArrowRight', 'ArrowUp', 'ArrowDown']
    const allowedShortcuts = ['a', 'c', 'x', 'v'];

    // Allow select, copy and paste shortcuts
    if (event.metaKey && allowedShortcuts.includes(event.key)) {
        return;
    }

    if (props.type === FormInputType.Number && !allowedKeys.includes(event.key) && (event.key.length === 1 &&
        isNaN(Number(event.key)))) {
        event.preventDefault();
    }
}

/* endregion */

</script>

