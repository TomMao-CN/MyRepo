import Vue from "vue";
import tinymce from '@/components/tinymce.vue'

import { SharedEnums } from "@/common/enums";
import { SharedData } from '@/common/shared-data';
import { Utils } from '@/common/utils';

export default Vue.extend({
    components: {
        //UEditor组件
        tinymce
    },
    data() {
        return {
            msg: 'lalala'
          
        };
    },
    methods: {

    },
    mounted() {

    }
});