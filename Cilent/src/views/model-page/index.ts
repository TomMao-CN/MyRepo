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
               
            }
            Utils.HandleRequest.PostRequest(url, params).then((res: any) => {

            });
        },
    },
    mounted() { }
});