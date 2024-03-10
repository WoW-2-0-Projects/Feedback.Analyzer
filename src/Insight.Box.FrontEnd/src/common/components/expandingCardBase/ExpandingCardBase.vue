<template>

    <div class="w-full flex flex-col card card-bg card-round card-shadow text-secondaryContentColor">

        <div class="flex card-shadow" :class="mainContentHeight > 0 ? `h-[${mainContentHeight}px]` : 'h-fit'">
            <slot name="mainContent"/>
        </div>

        <div class="w-full theme-action-transition"
             :class="expandingContentStyles"
        >
            <slot name="expandingContent"/>
        </div>

    </div>

</template>

<script setup lang="ts">

import {computed, onBeforeMount, ref, watch} from "vue";
import {TimerService} from "@/infrastructure/services/timer/TimerService";
import {DocumentService} from "@/infrastructure/services/document/DocumentService";
import CloseButton from "@/common/components/buttons/CloseButton.vue";
import {DividerType} from "@/common/components/divider/DividerType";
import {ButtonType} from "@/common/components/appButton/ButtonType";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import ExpandButton from "@/common/components/buttons/ExpandButton.vue";
import AppButton from "@/common/components/appButton/AppButton.vue";
import VerticalDivider from "@/common/components/divider/VerticalDivider.vue";

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

const timerService = new TimerService();
const isActiveInternal = ref<boolean>(props.isActive);
const timer = ref<number | null>(null);
const expandingContentStyles = ref<string>('');

onBeforeMount(() => setStyles(true));
watch(() => props.isExpanded, () => {
    console.log('test', props.isExpanded);
    setStyles();
});

const setStyles = (beforeMount: false) => {
    if (props.isExpanded) {
        expandingContentStyles.value = 'h-0 ';
        timer.value = timerService.clearTimer(timer.value);
        timer.value = timerService.setTimer(() => {
            expandingContentStyles.value = props.expandingContentHeight > 0 ? `h-[${props.expandingContentHeight}px]` :
                'h-fit';
        }, 100);
    } else {
        expandingContentStyles.value = 'h-0 ';
        timer.value = timerService.clearTimer(timer.value);

        if(beforeMount) {
            expandingContentStyles.value = 'h-0 hidden';
        } else {
            timer.value = timerService.setTimer(() => expandingContentStyles.value += 'hidden', 2000);
        }

    }
};

// const expandingContentStyles = computed(() => {
//     let styles = '';
//
//     if (props.expandingContentHeight > 0) {
//         styles += `h-[${props.expandingContentHeight}px]`;
//     } else {
//         styles += 'h-0 hidden';
//     }
// })

const emit = defineEmits(['closeModal']);


</script>