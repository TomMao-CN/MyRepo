import Vue from 'vue'
import { SharedEnums } from '@/common/enums';
import { Utils } from '@/common/utils';
import { SharedData } from '@/common/shared-data';

export default Vue.extend({
    data() {
        //验证用户名
        let validName = (rule: any, value: any, callback: any) => {
            if (!value) {
                return callback(new Error('用户名不能为空！'));
            } else {
                if (value.length < 2 || value.length > 8) {
                    return callback(new Error('用户名不得小于2位或者大于8位'));
                } else {
                    return callback();
                }
            }
        };
        //验证密码
        let validPassword = (rule: any, value: any, callback: any) => {
            if (!value) {
                return callback(new Error('密码不能为空！'));
            } else {
                if (!Utils.ValidInfo.ValidPassword(value)) {
                    return callback(new Error('密码只能由字母和数字组成！'));
                }
                else {
                    return callback();
                }
            }
        };
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
            modifyRules: {
                name: [{ required: true, validator: validName, trigger: 'blur' }],
                avatar: [{ required: true, message: '头像不能为空！', trigger: 'blur' }],
                password: [{ required: true, validator: validPassword, trigger: 'blur' }],
                type: [{ required: true, message: '管理员类型不能为空！', trigger: 'blur' }],
                status: [{ required: true, message: '角色状态不能为空！', trigger: 'blur' }],
            },
            administrators: null,
            totalCount: 0,
            pageSize: 0,
            currentPage: 0,
            typeOptions: SharedData.AdminTypeOptions,
            statusOptions: SharedData.RoleStatusOptions,
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
                name: this.searchParams.name,
                type: this.searchParams.type,
                status: this.searchParams.status,
                page: this.searchParams.page
            }
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
         * 新增or编辑管理员
         */
        modifyAdmin() {
            this.$refs.modifyParams.validate((valid: string | number) => {
                if (valid) {
                    this.loading = true;
                    let url: string = SharedData.ApiUrl + "Administrator/ModifyAdmin";
                    let params = {
                        ID: this.modifyParams.id,
                        Name: this.modifyParams.name,
                        Password: this.modifyParams.password,
                        Avatar: this.modifyParams.avatar,
                        Type: this.modifyParams.type,
                        Status: this.modifyParams.status
                    }
                    Utils.HandleRequest.PostRequest(url, params).then((res: any) => {
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
                    });
                }
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
            this.$refs.modifyParams.resetFields();
            this.modifyParams = {
                id: null,
                name: '',
                password: '',
                type: null,
                status: null,
                avatar: null
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
                adminID: row.ID
            };
            Utils.HandleRequest.PostRequest(url, params).then((res: any) => {
                this.loading = false;
                Utils.ElementUI.MessageTips("删除成功！", 2);
                this.getAdmins();
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
    }
});