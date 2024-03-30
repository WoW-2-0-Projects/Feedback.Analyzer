<template>

    <div ref="container" class="flex flex-wrap justify-center gap-1">
        <app-chip v-for="(chip, index) in props.chips.slice(0, chipsLimit)" :key="index" :text="chip.text"
                  :type="chip.type" :showBorder="chip.showBorder"/>
        <app-chip v-if="chipsLimit < chips.length" ref="counterChip" :text="counterChipText"
                  :type="ActionType.Secondary"/>
    </div>

</template>

<script setup lang="ts">

import {nextTick, onMounted, type PropType, ref, watch} from "vue";
import AppChip from "@/common/components/appChip/AppChip.vue";
import {ChipData} from "@/common/components/multiChip/ChipData";
import {ActionType} from "@/common/components/actions/ActionType";
import {DocumentService} from "@/infrastructure/services/document/DocumentService";

const documentService = new DocumentService();

const props = defineProps({
    chips: {
        type: Array<ChipData>,
        required: true
    },
    displayLimit: {
        type: Number,
        default: 50
    },
    displayRowLimit: {
        type: Number,
        default: 30
    }
});

const container = ref<HTMLDivElement>();
const counterChip = ref<HTMLSpanElement>();
const chipsLimit = ref<number>(10000);
const counterChipText = ref<string>('');

onMounted(() => {
    calculate();
    nextTick(() => calculate());
});

watch(() => props.chips, () => {
    calculate();
    nextTick(() => calculate());
});

const calculate = () => {
    if (!container.value || container.value?.children.length === 0) return;

    const containerWidth = documentService.getWidth(container.value);
    let totalWidth = 0; // Cumulative width of chips in the current row
    let rowCount = 1; // Start with one row
    let index: number;

    // Get child elements
    const children = Array.from(container.value?.children);
    // const counterChip = children[children.length - 1];
    for (index = 0; index < children.length - (counterChip.value ? 1 : 0); index++) {
        // Calculate sum of child width with 4px gap between them
        if (index >= props.displayLimit) break;
        const childWidth = documentService.getWidth(children[index] as HTMLElement) + 4;
        totalWidth += childWidth;

        // If the total width exceeds the container width, increment the row count
        if (totalWidth > containerWidth) {
            rowCount++;
            totalWidth = childWidth;

            // If the row count exceeds the display row limit, break the loop
            if (rowCount > props.displayRowLimit) {
                index--;
                counterChipText.value = `+ ${props.chips.length - index}`;

                // Ensure that the counter chip fits the last row otherwise remove one before chip
                if (counterChip.value && totalWidth + documentService.getWidth(counterChip.value) > containerWidth) {
                    counterChipText.value = `+ ${props.chips.length - index - 1}`;
                    index--;
                }

                break;
            }
        }
    }

    chipsLimit.value = index;
}

</script>