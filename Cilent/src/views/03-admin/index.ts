import Vue from 'vue'
import { SharedEnums } from '@/common/enums';
import { Utils } from '@/common/utils';

export default Vue.extend({
    data() {
        return {

        }
    },
    methods: {

    },
    mounted() {
        let type: any = 1;
        let params = {
            name: "admin",
            password: "123456"
        }

        let formData = Utils.HandleRequest.ConvertObjToForm(params);

        for (let item of formData) {
            console.log(item);
        }
    }
});