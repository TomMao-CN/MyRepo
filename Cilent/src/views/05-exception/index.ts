import Vue from 'vue'
import { SharedData } from '@/common/shared-data';
import { Utils } from '@/common/utils';

export default Vue.extend({
    data() {
        return {
            searchParams: {
                startTime: null,
                endTime: null,
                page: 1
            },
            lstExceptionLog: null,
            totalCount: 0,
            pageSize: 0,
            currentPage: 0,
            loading: false,
        }
    },
    methods: {
        /**
         * 获取异常日志列表
         */
        getExceptionLogList() {
            this.loading = true;
            let url: string = SharedData.ApiUrl + "ExceptionLog/GetExceptionLogList";
            let params = {
                token: sessionStorage.getItem('adminToken'),
                startTime: this.searchParams.startTime,
                endTime: this.searchParams.endTime,
                page: this.searchParams.page
            }
            let formParams = Utils.HandleRequest.ConvertObjToForm(params);
            this.axios.post(url, formParams).then((response: any) => {
                if (response.data.error_code == 0) {
                    this.lstExceptionLog = response.data.data.list;
                    this.totalCount = response.data.data.totalCount;
                    this.pageSize = response.data.data.pageSize;
                    this.searchParams.page = 1;
                    this.currentPage = response.data.data.page;
                    this.loading = false;
                } else {
                    Utils.ElementUI.MessageTips(response.data.error, 3);
                    console.log(response);
                }

            }).catch((error: any) => {
                alert('数据异常：' + error);
            });
        },

        /**
         * 处理分页
         * @param page 页码 
         */
        handleCurrentChange(page: any) {
            this.searchParams.page = page;
            this.getExceptionLogList();
        }
    },
    mounted() {
        this.getExceptionLogList();
    }
});