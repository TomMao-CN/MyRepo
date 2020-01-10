<template>
  <div class="login-container">
    <el-form class="login-form" :model="formParams" :rules="formRules" ref="formParams">
      <h3 style=" text-align:center">管理系统登录</h3>
      <el-form-item prop="name">
        <el-input type="text" placeholder="用户名" v-model="formParams.name">
          <!-- 字体图标 -->
          <template slot="prepend">
            <span class="fa fa-user fa-lg" style="width: 13px"></span>
          </template>
        </el-input>
      </el-form-item>
      <el-form-item prop="password">
        <el-input :type="pwdType" placeholder="密码" v-model="formParams.password">
          <!-- 字体图标 -->
          <template slot="prepend">
            <span class="fa fa-lock fa-lg" style="width: 13px"></span>
          </template>
          <!-- 字体图标 -->
          <template slot="suffix">
            <span class="password-eye" @click="showPassword" :class="eyeType"></span>
          </template>
        </el-input>
      </el-form-item>
      <el-form-item style="width:100%;">
        <el-button type="primary" style="width:100%" :loading="loading" @click="handleSubmit">登录</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>
<style lang="scss">
.login-container {
  width: 100%;
  height: 100%;
  background-color: rgb(41, 175, 115);
}
.login-form {
  -webkit-border-radius: 5px;
  border-radius: 5px;

  //登录框水平垂直居中的方法
  position: absolute;
  left: 50%;
  top: 50%;
  margin-left: -12%;
  margin-top: -10%;

  width: 24%;
  padding: 35px 35px 15px;
  background: #fff;
  border: 1px solid #eaeaea;
  box-shadow: 0 0 25px #cac6c6;
  .password-eye {
    position: absolute;
    right: 10px;
    top: 13px;
    cursor: pointer;
  }
}
</style>
<script src="./index.ts"></script>
