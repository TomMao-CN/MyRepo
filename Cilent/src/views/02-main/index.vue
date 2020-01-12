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
          <template v-for="item in adminMenus">
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
          <div class="app-header-tools">
            <!-- 主页 -->
            <i class="el-icon-s-home" style="padding-right:20px;" @click="goHome"></i>
            <!-- 刷新 -->
            <i class="el-icon-refresh" style="padding-right:20px;" @click="renovate"></i>
          </div>
          <!-- 管理员信息 -->
          <div class="app-header-userinfo">
            <el-dropdown trigger="hover">
              <span class="el-dropdown-link">
                <strong style="color:black">{{adminName}}</strong>
              </span>
              <!-- <i class="el-icon-s-custom el-icon--right"></i> -->
              <el-dropdown-menu slot="dropdown">
                <!-- 下拉列表的点击事件得加native -->
                <el-dropdown-item @click.native="dialogVisible=true">修改信息</el-dropdown-item>
                <el-dropdown-item
                  @click.native="dialogVisible=true;display=false;dialogTitle='更换密码'"
                >更换密码</el-dropdown-item>
                <el-dropdown-item @click.native="signOut">退出登录</el-dropdown-item>
              </el-dropdown-menu>
              <!-- 管理员头像 -->
              <el-avatar :size="40" :src="adminAvatar"></el-avatar>
            </el-dropdown>
          </div>
          <!-- 修改信息弹出框 -->
          <div class="dialog">
            <el-dialog :title="dialogTitle" :visible.sync="dialogVisible">
              <!-- ref属性不加会出现找不到validate函数的错误 -->
              <!-- 验证参数必须是model指定的参数 -->
              <el-form
                label-width="90px"
                :model="modifyParams"
                :rules="modifyRules"
                ref="modifyParams"
              >
                <!-- 修改信息 -->
                <el-form-item label="姓名" prop="name" v-if="display">
                  <el-input type="text" v-model="modifyParams.name"></el-input>
                </el-form-item>
                <el-form-item label="头像" prop="avatar" v-if="display">
                  <el-upload
                    class="avatar-uploader"
                    :action="adminAvatarUpUrl"
                    :show-file-list="false"
                    :on-success="handleAvatarSuccess"
                    :before-upload="beforeAvatarUpload"
                  >
                    <img v-if="modifyParams.avatar" :src="modifyParams.avatar" class="avatar" />
                    <i v-else class="el-icon-plus avatar-uploader-icon"></i>
                  </el-upload>
                </el-form-item>
                <!-- 跟换密码 -->
                <el-form-item label="原密码" prop="oldPassword" v-if="!display">
                  <el-input type="password" v-model="modifyParams.oldPassword"></el-input>
                </el-form-item>
                <el-form-item label="密码" prop="password" v-if="!display">
                  <el-input type="password" v-model="modifyParams.password"></el-input>
                </el-form-item>
                <el-form-item>
                  <el-button type="primary" :loading="loading" @click="modifyInfo">确定</el-button>
                  <el-button @click="handleCancel">取消</el-button>
                </el-form-item>
              </el-form>
            </el-dialog>
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
