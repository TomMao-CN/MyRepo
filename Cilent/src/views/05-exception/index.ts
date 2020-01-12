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
            exceptionLogs: null,
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
                startTime: this.searchParams.startTime,
                endTime: this.searchParams.endTime,
                page: this.searchParams.page
            }
            Utils.HandleRequest.PostRequest(url, params).then((res: any) => {
                this.exceptionLogs = res.list;
                this.totalCount = res.totalCount;
                this.pageSize = res.pageSize;
                this.currentPage = res.page;
                this.searchParams.page = 1;
                this.loading = false;
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