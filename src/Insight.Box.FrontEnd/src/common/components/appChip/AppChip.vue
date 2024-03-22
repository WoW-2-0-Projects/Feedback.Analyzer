<template>

      <span class="px-3 py-1 rounded-full text-xs whitespace-nowrap bg-opacity-20" :class="styles">{{text
          }}</span>

</template>

<script setup lang="ts">

import {computed, type PropType} from "vue";
import {ChipType} from "@/common/components/appChip/ChipType";

const props = defineProps({
    text: {
        type: String,
        required: true
    },
    showBorder: {
        type: Boolean,
    },
    type: {
        type: Number as PropType<ChipType>,
        default: ChipType.Info
    }
});

const styles = computed(() => {
    let styles = '';

    switch(props.type) {
        case ChipType.Success: styles += ' bg-successColor text-successColor border-successColor'; break;
        case ChipType.Error: styles += ' bg-dangerColor text-dangerColor'; break;
        case ChipType.Info: styles += ' bg-accentTertiaryColor text-accentTertiaryColor'; break;
        case ChipType.Warning: styles += ' bg-warningColor text-warningColor'; break;
    }

    if(props.showBorder) {
        styles += ' border';
    }

    return styles;
});

</script>