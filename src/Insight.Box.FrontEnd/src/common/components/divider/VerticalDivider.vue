<template>

    <div class=" flex items-center">
        <div class="bg-accentSecondaryColor w-[1.5px] h-4/5" :class="componentStyles"/>
    </div>

</template>

<script setup lang="ts">

import {computed, type PropType} from "vue";
import {DividerType} from "@/common/components/divider/DividerType";

const props = defineProps({
    type: {
        type: Number as PropType<DividerType>,
        default: DividerType.FullLength
    }
});

const componentStyles = computed(() => {

    let initialStyles = "";

    switch (props.type) {
        case DividerType.FullLength:
            initialStyles = "h-full";
            break;
        case DividerType.ContentLength:
            initialStyles = "h-4/5";
            break;
    }

})

</script>