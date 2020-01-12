import Vue from 'vue'
import axios from 'axios';
import { SharedData } from './shared-data';

export namespace Utils {
    /**
     * ElementUI组件封装
     */
    export class ElementUI {
        /**
         * 消息提示
         * @param msg 
         * @param type 1=成功，2=警告，3=错误
         */
        static MessageTips(msg: string, type: number) {
            if (msg == null) {
                msg = "系统异常！";
            }
            switch (type) {
                case 1:
                    Vue.prototype.$message({
                        showClose: true,
                        message: msg,
                        type: 'success'
                    });
                    break;
                case 2:
                    Vue.prototype.$message({
                        showClose: true,
                        message: msg,
                        type: 'warning'
                    });
                    break;
                case 3:
                    Vue.prototype.$message({
                        showClose: true,
                        message: msg,
                        type: 'error'
                    });
                    break;
                default:
                    Vue.prototype.$message({
                        showClose: true,
                        message: msg
                    });
            }
        };
        static Validator(params: any) {
            Vue.prototype.$refs[params].validate((valid: string | number) => {
                if (valid) {

                } else {
                    return false;
                }
            })
        }
    }
    /**
     * 处理请求相关
     */
    export class HandleRequest {
        /**
         * 将参数对象转换为表单参数对象
         * @param obj 
         */
        static ConvertObjToForm(obj: any) {
            let formData = new FormData();
            for (let item in obj) {
                formData.append(item, obj[item]);
            }
            return formData;
        };
        /**
         * ajax请求
         * @param apiUrl api路径
         * @param params 请求参数对象
         */
        static PostRequest(apiUrl: string, params: any): any {
            let formParams = HandleRequest.ConvertObjToForm(params);
            //参数对象中添加管理员的token
            formParams.append('token', SharedData.OnlineAdmin.Token);
            return new Promise((resolve, reject) => {
                axios.post(apiUrl, formParams)
                    .then(res => {
                        if (res.data.error_code == 0) {
                            resolve(res.data.data);
                        } else {
                            ElementUI.MessageTips(res.data.error, 3);
                        }

                    })
                    .catch(err => {
                        reject(`front-end-error:${err.data}`)
                    })
            });
        }

    }
    /**
     * 处理系统枚举
     */
    export class HandleEnums {
        /**
         * 获取下拉列表value对应的label。
         * value对应系统枚举值
         * @param ary 
         * @param index 
         */
        static ConvertValueToLabel(ary: any, index: number): string {
            for (let item of ary) {
                if (item.value == index) {
                    return item.label;
                }
            }
        }
    }
    /**
     * 验证信息
     */
    export class ValidInfo {
        /**
         * 验证密码只能由数字和字母组成
         * @param param 
         */
        static ValidPassword(param: string) {
            let regex = /^[0-9a-zA-Z]*$/;
            return regex.test(param);
        }
    }
}