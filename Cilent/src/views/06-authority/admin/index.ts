import Vue from "vue";
import { SharedEnums } from "@/common/enums";
import { SharedData } from '@/common/shared-data';
import { Utils } from '@/common/utils';

export default Vue.extend({
    data() {
        return {
            searchParams: {
                name: '',
                page: 1,
            },
            administrators: null,
            totalCount: 0,
            pageSize: 0,
            currentPage: 0,
            loading: false
        };
    },
    methods: {
        /**
         * 获取管理员
         */
        getAdmins() {
            this.loading = true;
            let url: string = SharedData.ApiUrl + "Administrator/GetAdmins";
            let params = {
                name: this.searchParams.name,
                type: SharedEnums.AdminType.SecondaryAdmin,
                status: SharedEnums.RoleStatus.Normal,
                page: this.searchParams.page
            };
            Utils.HandleRequest.PostRequest(url, params).then((res: any) => {
                this.loading = false;
                this.administrators = res.list;
                this.totalCount = res.totalCount;
                this.pageSize = res.pageSize;
                this.currentPage = res.page;
                this.searchParams.page = 1;
            });
        },
        /**
       * 处理分页
       * @param page 页码 
       */
        handleCurrentChange(page: any) {
            this.searchParams.page = page;
            this.getAdmins();
        },
        /**
       * 格式化角色状态
       * @param row 
       * @param column 
       */
        convertStatus(row: any, column: any) {
            return Utils.HandleEnums.ConvertValueToLabel(SharedData.RoleStatusOptions, row.Status);
        },
        /**
         * 格式化管理员类型
         * @param row 
         * @param column 
         */
        convetRoleType(row: any, column: any) {
            return Utils.HandleEnums.ConvertValueToLabel(SharedData.AdminTypeOptions, row.Type);
        },
        /**
         * 跳转页面
         */
        jumpPage(index: number, row: any) {
            this.$router.push({
                name: '权限分配',
                query: row
            });
        }
    },
    mounted() {
        this.getAdmins();
    }
});