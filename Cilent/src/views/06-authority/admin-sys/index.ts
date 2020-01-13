import Vue from "vue";
import { SharedEnums } from "@/common/enums";
import { SharedData } from '@/common/shared-data';
import { Utils } from '@/common/utils';

export default Vue.extend({
    data() {
        return {
            adminSyses: null,
            loading: false,
            totalCount: 0,
            pageSize: 0,
            currentPage: 0,
        };
    },
    methods: {
        /**
         * 管理员系统
         */
        getAdminSys() {
            this.loading = true;
            let url: string = SharedData.ApiUrl + "Authority/GetAdminSys";
            let params = {
                adminID: this.$route.query.ID
            }
            Utils.HandleRequest.PostRequest(url, params).then((res: any) => {
                this.adminSyses = res.list;
                this.loading = false;
            });
        },
        /**
         * 转换数据状态
         * @param row 
         * @param column 
         */
        convertStatus(row: any, column: any) {
            return Utils.HandleEnums.ConvertValueToLabel(SharedData.DataStatusOptions, row.Status);
        },
        /**
         * 路由回退
         */
        goBack(){
            this.$router.go(-1);
        }
    },
    mounted() {
        this.getAdminSys();
    }
});