import { Utils } from '@/common/utils.ts'
import Vue from 'vue'
import { SharedData } from '@/common/shared-data'

export default Vue.extend({
    data() {
        return {
            loading: false,
            pwdType: 'password',
            eyeType: 'fa fa-eye-slash fa-lg',
            formParams: {
                name: "admin",
                password: "123456"
            },
            formRules: {
                name: [{ required: true, message: '请输入用户名！', trigger: 'blur' }],
                password: [{ required: true, message: '请输入密码！', trigger: 'blur' }]
            }
        }
    },
    methods: {
        /**
         * 登录提交
         */
        handleSubmit() {
            //valid不能为any类型,否则表单验证会失效。
            this.$refs.formParams.validate((valid: string | number) => {
                if (valid) {
                    this.loading = true;
                    this.axios.post(SharedData.ApiUrl + "Shared/LoginSystem", {
                        name: this.formParams.name,
                        password: this.formParams.password
                    }).then((res: any) => {
                        if (res.data.error_code == 0) {
                            //将管理员信息保存在sessionStorage
                            sessionStorage.setItem('adminName', res.data.data.Name);
                            sessionStorage.setItem('adminToken', res.data.data.Token);
                            sessionStorage.setItem('adminType', res.data.data.Type);
                            sessionStorage.setItem('adminAvatar', res.data.data.Avatar);
                            sessionStorage.setItem('adminMenus', JSON.stringify(res.data.data.Menus));
                            this.loading = false;
                            //路由跳转
                            this.$router.push({ path: '/exception' });
                            Utils.ElementUI.MessageTips("登录成功！", 1)
                        }
                        else {
                            Utils.ElementUI.MessageTips(res.data.error, 3);
                            this.loading=false;
                        }
                    }).catch((error: string) => {
                        alert('前端异常：' + error);
                    });
                } else {
                    Utils.ElementUI.MessageTips("错误的提交！", 3);
                    return false;
                }
            });
        },
        /**
         * 密码输入框眼睛点击事件
         */
        showPassword() {
            //绑定input输入框的type
            if (this.pwdType === 'password') {
                this.pwdType = ''
                this.eyeType = 'fa fa-eye fa-lg'
            } else {
                this.pwdType = 'password'
                this.eyeType = 'fa fa-eye-slash fa-lg'
            }
        }

    },
    mounted() {

    }
});