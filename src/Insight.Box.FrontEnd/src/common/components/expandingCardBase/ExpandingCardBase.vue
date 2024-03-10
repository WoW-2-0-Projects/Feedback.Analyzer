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

import {onBeforeMount, onBeforeUnmount, ref, watch} from "vue";
import {TimerService} from "@/infrastructure/services/timer/TimerService";

const timerService = new TimerService();

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

const emit = defineEmits(['closeModal']);

const timer = ref<number | null>(null);
const expandingContentStyles = ref<string>('');

onBeforeMount(() => setStyles(true));
watch(() => props.isExpanded, () => setStyles());
onBeforeUnmount(() => timer.value = timerService.clearTimer(timer.value));

const setStyles = (beforeMount: false) => {
    if (props.isExpanded) {
        expandingContentStyles.value = 'h-0 opacity-0';
        timer.value = timerService.clearTimer(timer.value);
        timer.value = timerService.setTimer(() => expandingContentStyles.value = `opacity-0 ${getExpandingContentHeight()}`, 100);
        timer.value = timerService.setTimer(() => expandingContentStyles.value += 'opacity-100 ', 400);
    } else {
        expandingContentStyles.value = `opacity-0 ${getExpandingContentHeight()}`;
        timer.value = timerService.clearTimer(timer.value);
        timer.value = timerService.setTimer(() => expandingContentStyles.value = 'opacity-0 h-0', 200);

        if (beforeMount) {
            expandingContentStyles.value = 'hidden';
        } else {
            timer.value = timerService.setTimer(() => expandingContentStyles.value = 'hidden', 2000);
        }
    }
};

const getExpandingContentHeight = () => expandingContentStyles.value =
    props.expandingContentHeight > 0
        ? `h-[${props.expandingContentHeight}px] `
        : 'h-fit ';

</script>