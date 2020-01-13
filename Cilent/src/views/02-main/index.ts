import Vue from 'vue'
import md5 from 'js-md5'
import { SharedEnums } from '@/common/enums';
import { SharedData } from '@/common/shared-data';
import { Utils } from '@/common/utils';


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
        //验证原密码
        let validOldPassword = (rule: any, value: any, callback: any) => {
            if (!value) {
                return callback(new Error("原密码不能为空！"));
            } else {
                if (md5(value + SharedData.OnlineAdmin.Token) != SharedData.OnlineAdmin.Password) {
                    return callback(new Error("原密码错误！"));
                } else {
                    return callback();
                }

            };

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
            modifyParams: {
                id: SharedData.OnlineAdmin.ID,
                name: SharedData.OnlineAdmin.Name,
                avatar: SharedData.OnlineAdmin.Avatar,
                password: "",
                oldPassword: ""
            },
            modifyRules: {
                name: [{ required: true, validator: validName, trigger: 'blur' }],
                avatar: [{ required: true, message: '请上传头像！', trigger: 'blur' }],
                oldPassword: [{ required: true, validator: validOldPassword, trigger: 'blur' }],
                password: [{ required: true, validator: validPassword, trigger: 'blur' }],
            },
            isCollapse: false,
            adminName: null,
            adminAvatar: SharedData.OnlineAdmin.Avatar,
            adminMenus: null,

            //判断密码框是否显示
            display: true,
            //组件数据
            dialogVisible: false,
            loading: false,
            dialogTitle: '修改信息',
            //管理员头像上传url
            adminAvatarUpUrl: SharedData.ApiUrl + 'Shared/UploadImage'
        }
    },
    methods: {
        /**
         * 菜单缩放
         */
        toggleMenu() {
            this.isCollapse = !this.isCollapse;
        },
        /**
         * 修改当前管理员信息
         */
        modifyInfo() {
            this.$refs.modifyParams.validate((valid: string | number) => {
                if (valid) {
                    this.loading = true;
                    let url: string = SharedData.ApiUrl + "Administrator/ModifyAdmin";
                    let params = {
                        ID: this.modifyParams.id,
                        Name: this.modifyParams.name,
                        Avatar: this.modifyParams.avatar,
                        Password: this.modifyParams.password,
                        Type: SharedData.OnlineAdmin.Type,
                        Status: SharedData.OnlineAdmin.Status
                    };
                    Utils.HandleRequest.PostRequest(url, params).then((res: any) => {
                        this.dialogVisible = false;
                        this.$router.push('/');
                    });
                    this.loading = false;
                }
            });
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
        /**
         * 弹出框取消按钮
         */
        handleCancel() {
            this.display = true;
            this.dialogVisible = false;
            this.$refs.modifyParams.resetFields();
        },
        /**
         * 退出登录
         */
        signOut() {
            this.$confirm('确认退出?', '提示', {})
                .then(() => {
                    sessionStorage.removeItem('admin');
                    this.$router.push('/');
                })
                .catch(() => { });
        },
        renovate() {
            //浏览器刷新
            location.reload();
        },
        //去主页
        goHome() {
            if (this.$route.path != '/home') {
                this.$router.push('/home');
            }
        }

    },
    mounted() {
        this.adminName = SharedData.OnlineAdmin.Name;
        //管理员类型为超级管理员，则读取本地路由
        if (SharedData.OnlineAdmin.Type == SharedEnums.AdminType.SuperAdmin) {
            this.adminMenus = this.$router.options.routes;
        } else {
            //否则，读取数据库路由
            this.adminMenus = SharedData.OnlineAdmin.Menus;
        }

    }
});