import Vue from "vue";
import { SharedEnums } from "@/common/enums";
import { SharedData } from '@/common/shared-data';
import { Utils } from '@/common/utils';
import ECharts from 'vue-echarts'
import 'echarts/lib/chart/line'
import 'echarts/lib/component/polar'


export default Vue.extend({
    components: {
        'v-chart': ECharts
    },
    data() {
        let data = []

        for (let i = 0; i <= 360; i++) {
            let t = i / 180 * Math.PI
            let r = Math.sin(2 * t) * Math.cos(2 * t)
            data.push([r, i])
        }
        return {
            polar: {
                legend: {
                    data: ['line']
                },
                polar: {
                    center: ['50%', '54%']
                },
                tooltip: {
                    trigger: 'axis',
                    axisPointer: {
                        type: 'cross'
                    }
                },
                angleAxis: {
                    type: 'value',
                    startAngle: 0
                },
                radiusAxis: {
                    min: 0
                },
                series: [
                    {
                        coordinateSystem: 'polar',
                        name: 'line',
                        type: 'line',
                        showSymbol: false,
                        data: data
                    }
                ],
                animationDuration: 2000
            }

        };
    },
    methods: {

    },
    mounted() { }
});