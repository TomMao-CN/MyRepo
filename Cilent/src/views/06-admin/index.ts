import Vue from 'vue'
import { SharedEnums } from '@/common/enums';
import { Utils } from '@/common/utils';
import { SharedData } from '@/common/shared-data';

export default Vue.extend({
    data() {
        return {
            searchParams: {
                name: '',
                type: null,
                status: null,
                page: 1
            },
            administrators: null,
            totalCount: 0,
            pageSize: 0,
            currentPage: 0,
            loading: false,
            typeOptions: SharedData.AdminTypeOptions,
            statusOptions: SharedData.RoleStatusOptions,

            dialogVisible: false,
        }
    },
    methods: {
        /**
         * 获取管理员
         */
        getAdmins() {
            this.loading = true;
            let url: string = SharedData.ApiUrl + "Administrator/GetAdmins";
            let params = {
                token: sessionStorage.getItem('adminToken'),
                name: this.searchParams.name,
                type: this.searchParams.type,
                status: this.searchParams.status,
                page: this.searchParams.page
            }
            let formParams = Utils.HandleRequest.ConvertObjToForm(params);
            this.axios.post(url, formParams).then((response: any) => {
                if (response.data.error_code == 0) {
                    this.administrators = response.data.data.list;
                    this.totalCount = response.data.data.totalCount;
                    this.pageSize = response.data.data.pageSize;
                    this.currentPage = response.data.data.page;
                    this.searchParams.page = 1;
                    this.loading = false;
                } else {
                    Utils.ElementUI.MessageTips(response.data.error, 3);
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
            this.getAdmins();
        },
        handleCancel() {
            this.dialogVisible = false;
        }
    },
    mounted() {
        this.getAdmins();
    }
});