<template>
  <div class="app">
    <el-container>
      <!-- 根据isCollapse判断aside样式 -->
      <el-aside
        class="app-side app-side-left"
        :class="isCollapse?'app-side-collapsed':'app-side-expanded'"
      >
        <!-- logo -->
        <div class="app-side-logo">
          <img src="@/assets/logo1.png" width="60" height="60" />
        </div>
        <!-- 菜单 -->
        <el-menu
          class="el-menu-vertical-demo"
          style="border-right: solid 0px #e6e6e6;"
          :collapse="isCollapse"
          :collapse-transition="false"
          :router="true"
          background-color="#545c64"
          text-color="#fff"
          active-text-color="#ffd04b"
        >
          <template v-for="item in routes">
            <template v-if="item.hidden==false">
              <el-submenu :index="item.name" :key="item.name">
                <template slot="title">
                  <i :class="item.icon"></i>
                  <span slot="title">{{item.name}}</span>
                </template>
                <template v-for="_item in item.children">
                  <el-menu-item :index="_item.path" :key="_item.path">{{_item.name}}</el-menu-item>
                </template>
              </el-submenu>
            </template>
          </template>
        </el-menu>
      </el-aside>

      <el-container>
        <el-header class="app-header">
          <!-- 菜单缩放 -->
          <div style="width:60px;" @click="toggleMenu">
            <i v-if="!isCollapse" class="el-icon-caret-left"></i>
            <i v-if="isCollapse" class="el-icon-caret-right"></i>
          </div>
          <!-- 刷新 -->
          <i class="el-icon-refresh" @click="renovate"></i>
          <!-- 管理员信息 -->
          <div class="app-header-userinfo">
            <el-dropdown trigger="hover">
              <span class="el-dropdown-link">
                <strong style="color:black">{{adminName}}</strong>
              </span>
              <i class="el-icon-s-custom el-icon--right"></i>
              <el-dropdown-menu slot="dropdown">
                <el-dropdown-item>我的消息</el-dropdown-item>
                <el-dropdown-item>设置</el-dropdown-item>
                <el-dropdown-item @click.native="signOut">退出登录</el-dropdown-item>
              </el-dropdown-menu>
            </el-dropdown>
          </div>
        </el-header>
        <!-- 页面内容 -->
        <el-main class="app-body">
          <!-- 当前路由信息 -->
          <div>
            <strong
              style="font-size:12px"
            >{{`${this.$route.matched[0].name} / ${this.$route.name}`}}</strong>
          </div>
          <template>
            <router-view />
          </template>
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>
<style lang="scss">
</style>
<script src="./index.ts"></script>
