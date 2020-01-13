<template>
  <div class="every-page">
    <div class="toolbar">
      <el-row>
        <el-col :span="4">
          <el-input placeholder="请输入名字" v-model="searchParams.name"></el-input>
        </el-col>
        <el-col :span="8">
          <el-button type="primary" icon="el-icon-search" @click="getAdmins">搜索</el-button>
        </el-col>
      </el-row>
    </div>
    <div class="content">
      <el-table :data="administrators" v-loading="loading">
        <el-table-column label="头像">
          <!-- slot-scope是属于Vue，叫做插槽。可获取每行的属性值-->
          <template slot-scope="scope">
            <el-avatar style="object-fit:fill;" :size="50" :src="scope.row.Avatar"></el-avatar>
          </template>
        </el-table-column>
        <el-table-column prop="Name" label="姓名"></el-table-column>
        <el-table-column prop="Type" label="管理员类型" :formatter="convetRoleType"></el-table-column>
        <el-table-column prop="Status" label="角色状态" :formatter="convertStatus"></el-table-column>
        <el-table-column label="操作">
          <template slot-scope="scope">
            <el-button
              type="primary"
              icon="el-icon-unlock"
              circle
              @click="jumpPage(scope.$index, scope.row)"
            ></el-button>
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
<style lang="scss">
</style>
<script src="./index.ts"></script>
