import Vue from "vue";
import { SharedEnums } from "@/common/enums";
import { SharedData } from '@/common/shared-data';
import { Utils } from '@/common/utils';

export default Vue.extend({
    data() {
        return {
            modifyParams: {
                adminID: this.$route.query.ID,
                adminName: this.$route.query.Name,
                sysID: null,
                status: null
            },
            modifyRules: {

            },
            statusOptions: SharedData.DataStatusOptions,
            sysOptions: null,
            dialogVisible: false,
            adminSyses: null,
            loading: false,
            totalCount: 0,
            pageSize: 0,
            currentPage: 0,
            page: 1
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
                adminID: this.$route.query.ID,
                page: this.page
            }
            Utils.HandleRequest.PostRequest(url, params).then((res: any) => {
                this.adminSyses = res.list;
                this.totalCount = res.totalCount;
                this.pageSize = res.pageSize;
                this.currentPage = res.page;
                this.page = 1;
                this.loading = false;
            });
        },
        /**
         * 添加管理系统
         */
        addAdminSys() {
            this.loading = true;
            let url: string = SharedData.ApiUrl + "Authority/AddAdminSys";
            let params = {
                AdminID: this.modifyParams.adminID,
                SysID: this.modifyParams.sysID,
                Status: this.modifyParams.status
            }
            Utils.HandleRequest.PostRequest(url, params).then((res: any) => {
                Utils.ElementUI.MessageTips("添加成功！", 1);
                this.loading = false;
                this.getAdminSys();
                this.dialogVisible = false;
                this.modifyParams = {
                    adminID: this.$route.query.ID,
                    sysID: null,
                    status: null
                };
            });
        },
        handleDelete(index: number, row: any) {
            this.loading = true;
            let url: string = SharedData.ApiUrl + "Authority/DeleteAdminSys";
            let params = {
                adminSysID: row.ID
            };
            Utils.HandleRequest.PostRequest(url, params).then((res: any) => {
                this.loading = false;
                Utils.ElementUI.MessageTips("删除成功！", 2);
                this.getAdminSys();
            });
        },
        /**
         * 弹出框取消按钮
         */
        handleCancel() {
            this.dialogVisible = false;
            this.modifyParams = {
                adminID: this.$route.query.ID,
                sysID: null,
                status: null
            }
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
        goBack() {
            this.$router.go(-1);
        },
        /**
         * 处理分页
         * @param page 
         */
        handleCurrentChange(page: any) {
            this.page = page;
            this.getAdminSys();
        },
        /**
         * 处理添加按钮
         */
        handleAdd() {
            this.dialogVisible = true;
            this.getAllSys();
        },
        /**
         * 获取所有系统
         */
        getAllSys() {
            let url: string = SharedData.ApiUrl + "Authority/GetAllSys";
            let params = {

            }
            Utils.HandleRequest.PostRequest(url, params).then((res: any) => {
                let temp = [];
                for (let item of res.list) {
                    temp.push(
                        {
                            value: item.ID,
                            label: item.Name
                        }
                    );
                };
                this.sysOptions = temp;
            });
        }
    },
    mounted() {
        this.getAdminSys();
    }
});