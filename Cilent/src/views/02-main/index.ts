import Vue from 'vue'
import { SharedEnums } from '@/common/enums';

export default Vue.extend({
    data() {
        return {
            loading: false,
            isCollapse: false,
            adminName: null,
            routes: null
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
         * 退出登录
         */
        signOut() {
            this.$confirm('确认退出?', '提示', {})
                .then(() => {
                    sessionStorage.removeItem('adminName');
                    sessionStorage.removeItem('adminToken');
                    sessionStorage.removeItem('adminMenus');
                    this.$router.push('/');
                })
                .catch(() => { });
        }

    },
    mounted() {
        this.adminName = sessionStorage.getItem('adminName');
        //管理员类型为超级管理员，则读取本地路由    
        if (sessionStorage.getItem('adminType') == SharedEnums.AdminType.SuperAdmin as unknown as string) {
            this.routes = this.$router.options.routes;
        } else {
            //否则，读取数据库路由
            this.routes = JSON.parse(sessionStorage.getItem('adminMenus'));
        }

    }
});