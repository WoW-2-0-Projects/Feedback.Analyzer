<template>


    <div class="flex justify-center">
        <hr class="h-px border-0 bg-accentSecondaryColor" :class="componentStyles">
<!--        <div class="bg-accentSecondaryColor h-[1.5px] w-4/5"/>-->
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
            styles += "w-full";
            break;
        case DividerType.BorderLength:
            styles += "w-[95%]";
            break;
        case DividerType.ContentLength:
            styles += "w-4/5";
            break;
        default: styles += "w-full";
    }

    return styles;
});

</script>