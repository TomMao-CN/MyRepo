import Vue from "vue";
import { SharedEnums } from "@/common/enums";
import { SharedData } from '@/common/shared-data';
import { Utils } from '@/common/utils';

export default Vue.extend({
    data() {
        return {};
    },
    methods: {
        /**
         * ajax请求模板
         */
        ajaxModel() {
            this.loading = true;
            let url: string = SharedData.ApiUrl + "ExceptionLog/GetExceptionLogList";
            let params = {
                token: sessionStorage.getItem('adminToken')
            }
            let formParams = Utils.HandleRequest.ConvertObjToForm(params);
            this.axios.post(url, formParams).then((response: any) => {
                if (response.data.error_code == 0) {
                    this.loading = false;
                } else {
                    Utils.ElementUI.MessageTips(response.data.error, 3);
                }

            }).catch((error: any) => {
                alert('数据异常：' + error);
            });
        },
    },
    mounted() { }
});