<template>
  <div class="every-page">
    <div class="toolbar">
      <el-button type="primary" icon="el-icon-d-arrow-left" @click="goBack">返回</el-button>
      <el-button type="primary" icon="el-icon-plus">新增</el-button>
    </div>
    <div class="content">
      <el-table :data="adminSyses" v-loading="loading">
        <el-table-column prop="AdminName" label="管理员"></el-table-column>
        <el-table-column prop="SysName" label="系统"></el-table-column>
        <el-table-column prop="AuthorityTime" label="授权时间"></el-table-column>
        <el-table-column prop="Status" label="数据状态" :formatter="convertStatus"></el-table-column>
      </el-table>
    </div>
  </div>
</template>
<style lang="scss">
</style>
<script src="./index.ts"></script>