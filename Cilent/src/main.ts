import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'

import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
import 'font-awesome/scss/font-awesome.scss'
import '@/styles/global.scss'
import axios from 'axios'
import VueAxios from 'vue-axios'
import ECharts from 'vue-echarts'



Vue.config.productionTip = false
Vue.use(ElementUI)
Vue.use(VueAxios, axios)
Vue.component('v-chart', ECharts)

//全局导航守卫，当用户为登录时，会跳转到登录页
router.beforeEach((to, from, next) => {
  let admin = sessionStorage.getItem('admin');
  if (!admin && to.path !== '/') {
    next({
      path: '/'
    })
  } else {
    next();
  }
})

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')

