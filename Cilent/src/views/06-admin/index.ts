import Vue from 'vue'
import { SharedEnums } from '@/common/enums';
import { Utils } from '@/common/utils';
import { SharedData } from '@/common/shared-data';

export default Vue.extend({
    data() {
        return {
            //
            searchParams: {
                name: '',
                type: null,
                status: null,
                page: 1
            },
            modifyParams: {
                id: null,
                name: '',
                password: '',
                type: null,
                status: null
            },
            administrators: null,
            totalCount: 0,
            pageSize: 0,
            currentPage: 0,
            typeOptions: SharedData.AdminTypeOptions,
            statusOptions: SharedData.RoleStatusOptions,
            //组件数据
            dialogVisible: false,
            loading: false,
            dialogTitle: '新增'
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
         * 新增or编辑管理员
         */
        modifyAdmin() {
            this.loading = true;
            let url: string = SharedData.ApiUrl + "Administrator/ModifyAdmin";
            let params = {
                token: sessionStorage.getItem('adminToken'),
                ID: this.modifyParams.id,
                Name: this.modifyParams.name,
                Password: this.modifyParams.password,
                Avatar: "sssss",
                Type: this.modifyParams.type,
                Status: this.modifyParams.status
            }
            let formParams = Utils.HandleRequest.ConvertObjToForm(params);
            this.axios.post(url, formParams).then((response: any) => {
                if (response.data.error_code == 0) {
                    this.loading = false;
                    this.dialogVisible = false;
                    this.getAdmins();
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
        /**
         * 弹出框取消
         */
        handleCancel() {
            this.dialogVisible = false;
            this.dialogTitle = '新增'
        },
        /**
         * 表格编辑
         * @param index 
         * @param row 
         */
        handleEdit(index: number, row: any) {
            this.dialogTitle = '编辑';
            this.dialogVisible = true;
            this.modifyParams.id = row.ID;
            this.modifyParams.name = row.Name;
            this.modifyParams.password;
            this.modifyParams.type = row.Type;
            this.modifyParams.status = row.Status;
        },
        /**
         * 表格删除
         * @param index 
         * @param row 
         */
        handleDelete(index: number, row: any) {

        }
    },
    mounted() {
        this.getAdmins();
        // this.modifyAdmin();
    }
});