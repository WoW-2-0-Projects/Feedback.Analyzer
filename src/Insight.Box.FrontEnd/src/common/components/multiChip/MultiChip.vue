<template>

    <div class="flex gap-1">
        <app-chip v-for="(chip, index) in props.chips.slice(0, props.displayLimit)" :key="index" :text="chip.text"
                  :type="chip.type"
                  :showBorder="chip.showBorder"/>
        <app-chip v-if="props.chips.length > props.displayLimit"
                  :text="'+ ' + (props.chips.length - props.displayLimit)"
                  :type="ActionType.Secondary"/>
    </div>

</template>

<script setup lang="ts">

import {type PropType} from "vue";
import {ChipData} from "@/common/components/multiChip/ChipData";
import AppChip from "@/common/components/appChip/AppChip.vue";
import {ActionType} from "@/common/components/actions/ActionType";

const props = defineProps({
    chips: {
        type: Array as PropType<ChipData>,
        required: true
    },
    displayLimit: {
        type: Number,
        default: 2
    }
});

</script>