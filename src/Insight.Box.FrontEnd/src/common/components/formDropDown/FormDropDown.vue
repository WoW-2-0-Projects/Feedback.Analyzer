    <template>

    <div class="relative" @focusin="onFocusIn" @focusout="onFocusOut">

        <input type="text" name="input" v-model="searchValue"
               class="w-full rounded-md peer text-md theme-action-layout theme-action-padding theme-input-bg theme-action-style theme-input-placeholder
                 theme-action-transition theme-action-border-round theme-input-border theme-action-content"
               :placeholder="placeholder"/>

        <!-- Form input label -->
        <label for="input" class="absolute top-4 z-10 transform
                -translate-y-4 scale-75 origin-[0] start-2.5
                    peer-focus:scale-75 peer-focus:-translate-y-4 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto
                    peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0
                    text-sm theme-input-label theme-action-transition">
            {{ label }}
        </label>

        <!-- Expand / Collapse icon -->
        <svg :class="{ 'rotate-180': isOpen, 'transition-transform': true }"
             class="theme-action-content theme-focus-content theme-action-transition absolute top-1/2 right-4 z-10 text-sm theme-text-secondary transform -translate-y-1/2"
             xmlns="http://www.w3.org/2000/svg"
             viewBox="0 0 32 32"
             aria-hidden="true"
             role="presentation"
             focusable="false"
             style="display: block;
            fill: none;
            height: 16px;
            width: 16px;
            stroke: currentcolor;
            stroke-width: 4;
            overflow: visible;">
            <path fill="none" d="M28 12 16.7 23.3a1 1 0 0 1-1.4 0L4 12"></path>
        </svg>

        <!-- Drop down options -->
        <div v-show="isOpen"
             class="absolute mt-2 w-full focus:outline-none appearance-none rounded-md theme-modal-shadow  theme-bg-secondary theme-input-border-focus z-10 overflow-hidden theme-input">
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

import {onMounted, type PropType, ref, watch, watchEffect} from "vue";
import type {DropDownValue} from "@/common/components/forms/formDropDown/DropDownValue";

const props = defineProps({
    values: {
        type: Array as () => PropType<Array<DropDownValue>>,
        required: true,
        default: []
    },
    modelValue: {
        type: Object as () => DropDownValue | null
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

const emit = defineEmits(['update:modelValue']);

// Component states
const searchedOptions = ref(props.values);
const searchValue = ref('');
const isOpen = ref(false);

onMounted(() => {
    searchValue.value = props.modelValue?.key;
});

// Watcher for search value
watch(searchValue, (newValue) => {
    if (!newValue || newValue === '')
        searchedOptions.value = props.values;
    else
        searchOption(newValue);
});

// Filters the options based on the search value
const searchOption = (key: string) => {
    searchedOptions.value = props.values.filter((value) => {
        return value.key.toLowerCase().includes(key.toLowerCase());
    });
};

const onFocusIn = () => {
    searchedOptions.value = props.values;
    searchValue.value = '';
    isOpen.value = true;
};

const onFocusOut = () => {
    searchValue.value = props.modelValue?.key;
    isOpen.value = false;
};

const onSelected = (value: DropDownValue) => {
    emit('update:modelValue', value);
    isOpen.value = false;
}

</script>