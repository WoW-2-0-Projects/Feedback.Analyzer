<template>

    <table class="table-auto">
        <thead>
        <tr>
            <th v-for="(header, index) in data.headers" :key="index" :class="headerCellStyles">{{ header }}</th>
        </tr>
        </thead>
        <tbody>
        <tr class="hover:bg-gray-500" v-for="(row, index) in data.rows"
            :key="index">
            <td v-for="(cell, index) in row.data" :key="index" :class="rowCellStyles">{{ cell }}</td>
            <td v-show="row.actions.length > 0" :class="rowCellStyles" class="flex gap-2">
                <app-button v-for="(action, index) in row.actions" :key="index" :icon="action.icon"
                            :type="action.buttonType" :layout="ButtonLayout.Square" :size="ActionComponentSize.ExtraSmall"
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
import {ButtonType} from "@/common/components/appButton/ButtonType";
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

const headerCellStyles = computed(() => {
    const styles = 'px-6 py-2';

    return styles;
});


const rowCellStyles = computed(() => {
    const styles = 'px-6';

    return styles;
});


</script>