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
                avatar: null,
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
            imageUrl: '',
            //判断密码框是否显示
            display: true,
            //组件数据
            dialogVisible: false,
            loading: false,
            dialogTitle: '新增',
            //管理员头像上传url
            adminAvatarUpUrl: SharedData.ApiUrl + 'Shared/UploadImage'
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
                Avatar: this.modifyParams.avatar,
                Type: this.modifyParams.type,
                Status: this.modifyParams.status
            }
            let formParams = Utils.HandleRequest.ConvertObjToForm(params);
            this.axios.post(url, formParams).then((response: any) => {
                if (response.data.error_code == 0) {
                    this.loading = false;
                    this.dialogVisible = false;
                    this.getAdmins();
                    this.modifyParams = {
                        id: null,
                        name: '',
                        password: '',
                        type: null,
                        status: null
                    };
                    this.dialogTitle = '新增';
                    this.display = true;
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
            this.dialogTitle = '新增';
            this.display = true;
            this.modifyParams = {
                id: null,
                name: '',
                password: '',
                type: null,
                status: null
            }
        },
        /**
         * 表格编辑
         * @param index 
         * @param row 
         */
        handleEdit(index: number, row: any) {
            this.dialogTitle = '编辑';
            this.display = false;
            this.dialogVisible = true;
            this.modifyParams.id = row.ID;
            this.modifyParams.name = row.Name;
            this.modifyParams.avatar = row.Avatar;
            this.modifyParams.type = row.Type;
            this.modifyParams.status = row.Status;
        },
        /**
         * 表格删除
         * @param index 
         * @param row 
         */
        handleDelete(index: number, row: any) {
            this.loading = true;
            let url: string = SharedData.ApiUrl + "Administrator/DeleteAdmin";
            let params = {
                token: sessionStorage.getItem('adminToken'),
                adminID: row.ID
            }
            let formParams = Utils.HandleRequest.ConvertObjToForm(params);
            this.axios.post(url, formParams).then((response: any) => {
                if (response.data.error_code == 0) {
                    this.loading = false;
                    Utils.ElementUI.MessageTips("删除成功！", 2);
                    this.getAdmins();
                } else {
                    Utils.ElementUI.MessageTips(response.data.error, 3);
                }

            }).catch((error: any) => {
                alert('数据异常：' + error);
            });
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
         * 头像上传成功前的处理
         * @param file 
         */
        beforeAvatarUpload(file: any) {
            if (file.type != 'image/jpeg') {
                Utils.ElementUI.MessageTips('图片格式必须为jpg!', 3);
                return false;
            }
            //1k等于1024b
            if (file.size > 1024 * 100) {
                Utils.ElementUI.MessageTips('图片大小不得超过100k!', 3);
                return false;
            }
            return true;
        },
        /**
         * 头像上传成功后的处理
         * @param res 
         * @param file 
         */
        handleAvatarSuccess(res: any, file: any) {
            this.modifyParams.avatar = file.response.data;
        },
    },
    mounted() {
        this.getAdmins();
        let ss = Utils.HandleEnums.ConvertValueToLabel(SharedData.AdminTypeOptions, 2);

    }
});