import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '@/views/01-login/index.vue'
import Main from '@/views/02-main/index.vue'
import ErrorPage from '@/views/error-page/index.vue'
import Admin from '@/views/04-admin/index.vue'
import Exception from '@/views/05-exception/index.vue'
import Article from '@/views/06-article/index.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: '登录',
    component: Login
  },
  {
    path: '/main',
    name: '角色系统',
    hidden: false,
    icon: 'el-icon-user-solid',
    component: Main,
    children: [
      { path: '/admin', name: '管理员', component: Admin }
    ]
  },
  {
    path: '/main',
    name: '信息系统',
    hidden: false,
    icon: 'el-icon-document',
    component: Main,
    children: [
      { path: '/article', name: '资讯', component: Article }
    ]
  },
  {
    path: '/main',
    name: '异常系统',
    hidden: false,
    icon: 'el-icon-info',
    component: Main,
    children: [
      { path: '/exception', name: ' 异常日志', component: Exception }
    ]
  },
  {
    //*是路由未找到时的跳转，须放到路由配置的最后面
    path: '*',
    name: '404页',
    component: ErrorPage
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
