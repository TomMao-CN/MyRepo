import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '@/views/01-login/index.vue'
import Main from '@/views/02-main/index.vue'
import Home from '@/views/03-home/index.vue'
import ErrorPage from '@/views/error-page/index.vue'
import Admin from '@/views/04-admin/index.vue'
import Exception from '@/views/05-exception/index.vue'
import AuthorityAdmin from '@/views/06-authority/admin/index.vue'
import AuthoritySys from '@/views/06-authority/admin-sys/index.vue'
import Test from '@/views/07-test/index.vue'
import Blog from '@/views/08-blog/index.vue'
import Weather from '@/views/09-weather/index.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: '登录',
    component: Login
  },
  {
    path: '/main',
    name: '主页系统',
    display: false,
    icon: 'el-icon-s-home',
    component: Main,
    children: [
      { path: '/home', name: '首页', component: Home }
    ]
  },
  {
    path: '/main',
    name: '角色系统',
    display: true,
    icon: 'el-icon-user-solid',
    component: Main,
    children: [
      { path: '/admin', name: '管理员', display: true, component: Admin }
    ]
  },
  {
    path: '/main',
    name: '权限系统',
    display: true,
    icon: 'el-icon-lock',
    component: Main,
    children: [
      { path: '/authority-admin', name: '授权管理员', display: true, component: AuthorityAdmin },
      { path: '/authority-sys', name: '权限分配', display: false, component: AuthoritySys }
    ]
  },
  {
    path: '/main',
    name: '文档资讯',
    display: true,
    icon: 'el-icon-edit-outline',
    component: Main,
    children: [
      { path: '/blog', name: '博客', display: true, component: Blog }
    ]
  },
  {
    path: '/main',
    name: '天气系统',
    display: true,
    icon: 'el-icon-sunny',
    component: Main,
    children: [
      { path: '/weather', name: '天气预报', display: true, component: Weather }
    ]
  },
  {
    path: '/main',
    name: '测试系统',
    display: true,
    icon: 'el-icon-question',
    component: Main,
    children: [
      { path: '/test', name: '测试页', display: true, component: Test }
    ]
  },
  {
    path: '/main',
    name: '异常系统',
    display: true,
    icon: 'el-icon-info',
    component: Main,
    children: [
      { path: '/exception', name: ' 异常日志', display: true, component: Exception }
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
  // 打包环境下用hash模式，不然会直接跳到*
  mode: 'hash',
  base: process.env.BASE_URL,
  routes
})

export default router
