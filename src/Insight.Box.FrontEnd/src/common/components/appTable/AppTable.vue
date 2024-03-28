<template>

    <table class="table-auto table-bg">
        <thead>
        <tr>
            <th v-for="(header, index) in data.headers" :key="index"
                class="table-header-cell bg-secondaryColor"
            >{{ header }}
            </th>
        </tr>
        </thead>
        <tbody class="divide-y divide-accentSecondaryColor">
        <tr class="bg-secondaryContentColor odd:bg-opacity-5 even:bg-opacity-10"
            v-for="(row, index) in data.rows" :key="index">
            <td v-for="(cell, index) in row.data" :key="index" class="table-data-cell-small">
                {{ cell }}
            </td>
            <td v-show="row.actions.length > 0" class="table-data-cell-small flex gap-2">
                <app-button v-for="(action, index) in row.actions" :key="index" :icon="action.icon"
                            :type="action.buttonType" :layout="ButtonLayout.Square"
                            :size="ActionComponentSize.ExtraSmall"
                            @click="action.action">
                    <i :class="action.icon"/>
                </app-button>
            </td>
        </tr>
        </tbody>
    </table>

</template>

<script setup lang="ts">

import {computed, type PropType} from "vue";
import {TableSize} from "@/common/components/appTable/TableStyle";
import type {TableData} from "@/common/components/appTable/TableData";
import {ButtonLayout} from "@/common/components/appButton/ButtonLayout";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";
import AppButton from "@/common/components/appButton/AppButton.vue";

const props = defineProps({
    size: {
        type: Number as PropType<TableSize>,
        default: TableSize.Small
    },
    data: {
        type: Object as PropType<TableData>,
        required: true
    }
});

</script>