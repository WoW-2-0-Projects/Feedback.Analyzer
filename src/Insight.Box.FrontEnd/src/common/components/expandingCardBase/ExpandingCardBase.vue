<template>

    <div class="w-full flex flex-col card card-bg card-round card-shadow text-secondaryContentColor">

        <!-- Main content -->
        <div class="flex card-shadow" :style="{ maxHeight: mainContentHeight > 0 ? `${mainContentHeight}px` : 'none' }">
            <slot name="mainContent"/>
        </div>

        <!-- Expanding content -->
        <div class="w-full overflow-hidden"
             :style="`--expand-height: ${props.expandingContentHeight}px`"
             :class="{'animate-fadeInExpand': isExpanded, 'animate-fadeOutCollapse': !isExpanded
                      ,'hidden': !firstExpanded
             }">
            <slot name="expandingContent"/>
        </div>

    </div>

</template>

<script setup lang="ts">

import {ref, watch} from "vue";

const props = defineProps({
    isExpanded: {
        type: Boolean,
        default: false
    },
    mainContentHeight: {
        type: Number,
        default: 0
    },
    expandingContentHeight: {
        type: Number,
        default: 0
    },
});

const firstExpanded = ref<boolean>(props.isExpanded);
const emit = defineEmits(['closeModal']);
watch(() => props.isExpanded, () => {
    if (props.isExpanded) firstExpanded.value = true;
});

</script>