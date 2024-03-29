<template>

    <div class="w-fit flex items-center justify-center" :class="labelPosition === Position.Horizontal ? '' :
    'flex-col'">

        <div ref="chartContainer"/>

        <div v-if="showLabels" class="flex flex-col items-start">
            <chart-label text="Positive" color="#1C64F2" :size="size"/>
            <chart-label text="Negative" color="#16BDCA" :size="size"/>
            <chart-label text="Neutral" color="#FDBA8C" :size="size"/>
        </div>
    </div>

</template>

<script setup lang="ts">

import {onBeforeUnmount, onMounted, type PropType, ref} from "vue";
import {ChartService} from "@/infrastructure/services/charts/ChartService";
import ChartLabel from "@/common/components/chartLabel/ChartLabel.vue";
import {Position} from "@/common/components/chartLabel/Position";
import {ActionComponentSize} from "@/common/components/formInput/ActionComponentSize";

const chartContainer = ref<HTMLDivElement>();
const chartService = new ChartService();

const props = defineProps({
    size: {
        type: Number as PropType<ActionComponentSize>,
        default: ActionComponentSize.Small
    },
    height: {
        type: Number,
        default: 150
    },
    showLabels: {
        type: Boolean,
        default: true
    },
    labelPosition: {
        type: Number as PropType<Position>,
        default: Position.Vertical
    }

});

const getChartOptions = () => {
    return {
        series: [35.1, 23.5, 2.4, 5.4],
        colors: ["#1C64F2", "#16BDCA", "#FDBA8C", "#E74694"],
        chart: {
            height: props.height,
            width: props.height,
            type: "donut",
        },
        stroke: {
            colors: ["transparent"],
            lineCap: "",
        },
        plotOptions: {
            pie: {
                donut: {
                    labels: {
                        show: false,
                        name: {
                            show: false,
                            fontFamily: "Inter, sans-serif",
                            offsetY: 20,
                        },
                        total: {
                            showAlways: true,
                            show: false,
                            label: "Unique visitors",
                            fontFamily: "Inter, sans-serif",
                            formatter: function (w) {
                                const sum = w.globals.seriesTotals.reduce((a, b) => {
                                    return a + b
                                }, 0)
                                return '$' + sum + 'k'
                            },
                        },
                        value: {
                            show: false,
                            fontFamily: "Inter, sans-serif",
                            offsetY: -20,
                            formatter: function (value) {
                                return value + "k"
                            },
                        },
                    },
                    size: "80%",
                },
            },
        },
        grid: {
            padding: {
                top: -2,
            },
        },
        labels: ["Direct", "Sponsor", "Affiliate", "Email marketing"],
        dataLabels: {
            enabled: false,
        },
        legend: {
            show: false,
            position: "bottom",
            fontFamily: "Inter, sans-serif",
        }
    }
}

onMounted(async () => {
    await chartService.render(chartContainer.value, getChartOptions());
});

onBeforeUnmount(async () => {
    await chartService.remove(chartContainer.value, getChartOptions());
});

</script>