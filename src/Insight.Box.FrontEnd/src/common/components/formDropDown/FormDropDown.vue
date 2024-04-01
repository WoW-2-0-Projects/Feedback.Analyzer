<template>

    <div class="relative h-fit w-fit theme-action-border-round theme-input-bg theme-action-transition
                theme-action-shadow theme-action-content"
         :class="wrapperStyles"
         @focusin="onFocusIn" @focusout="onFocusOut">

        <input type="text" name="input" v-model="searchValue" autocomplete="off"
               :class="size === ActionComponentSize.Medium ? 'action-layout text-md' : 'action-small-layout text-sm'"
               class="mx-3 w-full peer theme-input theme-input-placeholder theme-action-transition
                      theme-action-content"
               :placeholder="placeholder"/>

        <!-- Form input label -->
        <label for="input" :class="labelStyles" class="absolute transform scale-75 origin-[0] start-2.5
                    peer-focus:scale-75 peer-focus:-translate-y-4 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto
                    peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0
                     theme-input-label theme-action-transition">
            {{ label }}
        </label>

        <!-- Expand / Collapse icon -->
        <expand-icon :isOpen="isOpen" class="absolute top-1/2 -translate-y-1/2 right-4"/>

        <!-- Drop down options -->
        <div class="mt-2 w-full z-50 focus:outline-none bg-[#333740] appearance-none theme-action-border-round text-secondaryContentColor theme-modal-shadow theme-action-secondary theme-input-border overflow-hidden p-2" v-show="isOpen">
            <ul>
                <li v-for="(value, index) in searchedOptions" :key="index" @mousedown="onSelected(value)"
                    class="px-4 py-4 cursor-pointer theme-input-hover"
                    :class="value.key === props.modelValue?.key ? 'text-opacity-100' : 'text-opacity-40'"
                >
                    {{ value.key }}
                </li>
                <li v-if="searchedOptions.length == 0" class="px-4 py-4">
                    No results found
                </li>
            </ul>
        </div>
    </div>

</template>

<script setup lang="ts">

import {computed, onMounted, type PropType, ref, watch} from "vue";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import type {DropDownValue} from "@/common/components/formDropDown/DropDownValue";
import ExpandIcon from "@/common/components/icons/ExpandIcon.vue";

const props = defineProps({
    values: {
        type: Array as PropType<Array<DropDownValue<string, any>>>,
        required: true,
        default: []
    },
    size: {
        type: Number as PropType<ActionComponentSize>,
        default: ActionComponentSize.Medium
    },
    modelValue: {
        type: Object as PropType<DropDownValue<string, any> | null>,
        required: true
    },
    label: {
        type: String,
        default: ''
    },
    placeholder: {
        type: String,
        default: ''
    }
});

const isFocused = ref<boolean>(false);

const wrapperStyles = computed(() => {
    let styles = '';

    if(isFocused.value) {
        styles += ' theme-input-border-focus';
    } else {
        styles += ' theme-input-border';
    }

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
    }

    return styles;
});

const emit = defineEmits(['update:modelValue']);

// Component states
const searchedOptions = ref<Array<DropDownValue<string, any>>>(props.values);
const searchValue = ref<string>('');
const isOpen = ref<boolean>(false);

onMounted(() => {
    searchValue.value = props.modelValue?.key ?? '';
});

// Watcher for search value
watch(() => [searchValue, props.values], () => searchOption());
watch(() => props.modelValue, () => searchValue.value = props.modelValue?.key ?? '');

// Filters the options based on the search value
const searchOption = () => {
    if (!searchValue.value || searchValue.value === '') {
        searchedOptions.value = props.values;
        return;
    }

    searchedOptions.value = props.values.filter((option: DropDownValue<string, any>) => {
        return option.key.toLowerCase().includes(searchValue.value.toLowerCase());
    });
};

const onFocusIn = () => {
    searchedOptions.value = props.values;
    searchValue.value = '';
    isFocused.value = true;
    isOpen.value = true;
};

const onFocusOut = () => {
    searchValue.value = props.modelValue?.key ?? '';
    isFocused.value = false;
    isOpen.value = false;
};

const onSelected = (value: DropDownValue<string, any>) => {
    emit('update:modelValue', value);
    isOpen.value = false;
}

</script>