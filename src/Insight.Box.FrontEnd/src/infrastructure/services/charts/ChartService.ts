/*
 * Provides the chart rendering functionality
 */
import ApexCharts from "apexcharts";

export class ChartService {

    /*
     * Renders a chart in the given container
     */
    public async render(chartContainer: HTMLDivElement, options: any): Promise {
        if (chartContainer && typeof ApexCharts !== 'undefined') {

            const chart = new ApexCharts(chartContainer, options);
            await chart.render();
        }
    }
}