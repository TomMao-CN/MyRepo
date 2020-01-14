import Vue from "vue";
import tinymce from '@/components/tinymce.vue'

import { SharedEnums } from "@/common/enums";
import { SharedData } from '@/common/shared-data';
import { Utils } from '@/common/utils';

export default Vue.extend({
    components:{
        tinymce
    },
    data() {
        return {
            msg:'sadkfasj;dlf',
            uploadImgApi:SharedData.ApiUrl+'Shared/UploadImage'       
        };
    },
    methods: {
       
    },
    mounted() { 
       
    }
});