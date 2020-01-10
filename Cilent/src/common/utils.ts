import Vue from 'vue'

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
        }

    }
    /**
     * 处理系统枚举
     */
    export class HandleEnums {
        static ConvertValueToLabel(ary: any, index: number): string {
            for (let item of ary) {
                if (item.value == index) {
                    return item.label;
                }
            }
        }
    }
}