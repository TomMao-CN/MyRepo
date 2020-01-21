<template>
  <div class="every-page">
    <div class="today-info" style="margin-left:10%">
      <el-tag type="success">{{city}}天气</el-tag>
    </div>
    <v-chart :options="polar" style="height:500px" />
    <div style="padding-left:10%">
      <h3>天气详情</h3>
      <el-table :data="weatherInfo">
        <el-table-column prop="date" label="日期"></el-table-column>
        <el-table-column prop="week" label="星期"></el-table-column>
        <el-table-column prop="sunrise" label="日出"></el-table-column>
        <el-table-column prop="sunset" label="日落"></el-table-column>
        <el-table-column prop="fx" label="风向"></el-table-column>
        <el-table-column prop="fl" label="风级"></el-table-column>
        <el-table-column prop="aqi" label="空气质量"></el-table-column>
        <el-table-column prop="type" label="类型"></el-table-column>
      </el-table>
    </div>
  </div>
</template>
<style lang="scss">
</style>
<script src="./index.ts"></script>
