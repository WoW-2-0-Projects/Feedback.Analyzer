<template>

    <div class="flex items-center">
        <div class="bg-accentSecondaryColor w-[1px]" :class="componentStyles"/>
    </div>

</template>

<script setup lang="ts">

import {computed, type PropType} from "vue";
import {DividerType} from "@/common/components/dividers/DividerType";

const props = defineProps({
    type: {
        type: Number as PropType<DividerType>,
        default: DividerType.FullLength
    }
});

const componentStyles = computed(() => {
    let styles = "";

    switch (props.type) {
        case DividerType.FullLength:
            styles += "h-full";
            break;
        case DividerType.ContentLength:
            styles += "h-4/5";
            break;
    }

    return styles;
});

</script>