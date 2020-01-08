<template>
  <div class="every-page">
    <div class="toolbar">
      <el-row>
        <el-col :span="5">
          <el-date-picker
            type="datetime"
            placeholder="选择开始时间"
            value-format="yyyy-MM-dd HH:mm"
            format="yyyy-MM-dd HH:mm"
            v-model="searchParams.startTime"
          ></el-date-picker>
        </el-col>
        <el-col :span="5">
          <el-date-picker
            type="datetime"
            placeholder="选择结束时间"
            value-format="yyyy-MM-dd HH:mm"
            format="yyyy-MM-dd HH:mm"
            v-model="searchParams.endTime"
          ></el-date-picker>
        </el-col>
        <el-col :span="8">
          <el-button type="primary" icon="el-icon-search" @click="getExceptionLogList">搜索</el-button>
        </el-col>
      </el-row>
    </div>
    <div class="content">
      <el-table :data="lstExceptionLog" v-loading="loading">
        <el-table-column prop="Source" label="数据源"></el-table-column>
        <el-table-column prop="Message" label="异常信息"></el-table-column>
        <el-table-column prop="Time" label="触发时间"></el-table-column>
        <el-table-column label="堆栈跟踪">
          <template slot-scope="scope">
            <el-popover trigger="click" placement="top">
              <p>{{ scope.row.StackTrace }}</p>
              <div slot="reference" class="name-wrapper">
                <el-tag size="medium">INFO</el-tag>
              </div>
            </el-popover>
          </template>
        </el-table-column>
      </el-table>
      <div class="change-page">
        <el-col :span="24">
          <el-pagination
            layout="prev,pager,next"
            @current-change="handleCurrentChange"
            :current-page.sync="currentPage"
            :page-size="pageSize"
            :total="totalCount"
          ></el-pagination>
        </el-col>
      </div>
    </div>
  </div>
</template>
<style lang="scss"></style>
<script src="./index.ts"></script>
