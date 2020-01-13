<template>
  <div class="every-page">
    <div class="toolbar">
      <el-button type="primary" icon="el-icon-d-arrow-left" @click="goBack">返回</el-button>
      <el-button type="primary" icon="el-icon-plus" @click="handleAdd">新增</el-button>
    </div>
    <div class="content">
      <el-table :data="adminSyses" v-loading="loading">
        <el-table-column prop="AdminName" label="管理员"></el-table-column>
        <el-table-column prop="SysName" label="系统"></el-table-column>
        <el-table-column prop="AuthorityTime" label="授权时间"></el-table-column>
        <el-table-column prop="Status" label="数据状态" :formatter="convertStatus"></el-table-column>
        <el-table-column label="操作">
          <template slot-scope="scope">
            <el-button
              type="danger"
              icon="el-icon-delete"
              circle
              @click="handleDelete(scope.$index, scope.row)"
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
    <div class="dialog">
      <el-dialog title="绑定系统" :visible.sync="dialogVisible">
        <el-form label-width="100px" :model="modifyParams" :rules="modifyRules" ref="modifyParams">
          <el-form-item label="姓名">
            <el-input type="text" :disabled="true" v-model="modifyParams.adminName"></el-input>
          </el-form-item>
          <el-form-item label="系统">
            <el-select v-model="modifyParams.sysID" placeholder="系统">
              <el-option
                v-for="item in sysOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="数据状态">
            <el-select v-model="modifyParams.status" placeholder="数据状态">
              <el-option
                v-for="item in statusOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" :loading="loading" @click="addAdminSys">确定</el-button>
            <el-button @click="handleCancel">取消</el-button>
          </el-form-item>
        </el-form>
      </el-dialog>
    </div>
  </div>
</template>
<style lang="scss">
</style>
<script src="./index.ts"></script>