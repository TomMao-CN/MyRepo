import Vue from "vue";
import ECharts from 'vue-echarts'
import 'echarts/lib/chart/line'
import 'echarts/lib/component/polar'
import 'echarts/lib/component/title'
import 'echarts/lib/chart/bar'
import 'echarts/lib/component/tooltip'
import 'echarts/lib/component/grid'
import 'echarts/lib/component/legend'

import { SharedEnums } from "@/common/enums";
import { SharedData } from '@/common/shared-data';
import { Utils } from '@/common/utils';

export default Vue.extend({
    components: {
        'v-chart': ECharts
    },
    data() {
        return {
            weatherInfo: null,
            city: null,
            polar: {
                title: {
                    text: '温度预测',
                    left: '10%',
                    top: 20
                },
                grid: {
                    left: '10%'
                },

                legend: {
                    data: ['最低温', '最高温'],
                    top: 20
                },
                tooltip: {
                    trigger: 'axis',
                    axisPointer: {
                        type: 'cross'
                    }
                },
                xAxis: {
                    type: 'category',
                    data: []
                },
                yAxis: {
                    type: 'value'
                },
                series: [
                    {
                        name: '最低温',
                        data: [],
                        type: 'line'
                    },
                    {
                        name: '最高温',
                        data: [],
                        type: 'line'
                    }
                ]
            }
        };
    },
    methods: {
        getWeather() {
            let url: string = SharedData.ApiUrl + "Weather/GetWeather";
            let params = {

            }
            Utils.HandleRequest.PostRequest(url, params).then((res: any) => {
                this.polar.xAxis.data = res.map.date;
                this.polar.series[0].data = res.map.low;
                this.polar.series[1].data = res.map.hight;
                this.weatherInfo = res.table;
                this.city = res.city;
            });
        }
    },
    mounted() {
        this.$notify({
            title: '提示',
            message: '由于获取天气的接口来自第三方，所以经常会因为获取不到数据而出现异常！',
            duration: 0
        });
        this.getWeather();
    }
});