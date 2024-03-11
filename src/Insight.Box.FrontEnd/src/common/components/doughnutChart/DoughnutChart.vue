<template>

    <div class="flex flex-col items-center justify-center">

        <div ref="chartContainer" class="py-6"></div>

        <div class="flex flex-col items-start">
            <chart-label text="Positive" color="#1C64F2"/>
            <chart-label text="Negative" color="#16BDCA"/>
            <chart-label text="Neutral" color="#FDBA8C"/>
        </div>
    </div>

</template>

<script setup lang="ts">

import {onMounted, ref} from "vue";
import {ChartService} from "@/infrastructure/services/charts/ChartService";
import ChartLabel from "@/common/components/chartLabel/ChartLabel.vue";

const chartContainer = ref<HTMLDivElement>();
const chartService = new ChartService();

const getChartOptions = () => {
    return {
        series: [35.1, 23.5, 2.4],
        colors: ["#1C64F2", "#16BDCA", "#FDBA8C"],
        chart: {
            height: 150,
            width: "100%",
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
                        show: true,
                        name: {
                            show: true,
                            fontFamily: "Poppins, sans-serif",
                            offsetY: 20,
                        },
                        value: {
                            show: true,
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
        labels: ["Positive", "Negative", "Neutral"],
        dataLabels: {
            enabled: false,
        },
        legend: {
            position: "bottom",
            fontFamily: "Poppins, sans-serif",
            show: false
        },
        yaxis: {
            labels: {
                formatter: function (value) {
                    return value + "k"
                },
            },
        },
        xaxis: {
            labels: {
                formatter: function (value) {
                    return value + "k"
                },
            },
            axisTicks: {
                show: false,
            },
            axisBorder: {
                show: false,
            },
        },
    }
}

onMounted(async () => {
    await chartService.render(chartContainer.value, getChartOptions());
});

</script>