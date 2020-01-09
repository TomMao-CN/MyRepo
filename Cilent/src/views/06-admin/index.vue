<template>
  <div class="every-page">
    <!-- 工具栏 -->
    <div class="toolbar">
      <el-row>
        <el-col :span="4">
          <el-input placeholder="请输入名字" v-model="searchParams.name"></el-input>
        </el-col>
        <el-col :span="4">
          <el-select v-model="searchParams.type" placeholder="管理员类型">
            <el-option
              v-for="item in typeOptions"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-col>
        <el-col :span="4">
          <el-select v-model="searchParams.status" placeholder="角色状态">
            <el-option
              v-for="item in statusOptions"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-col>
        <el-col :span="8">
          <el-button type="primary" icon="el-icon-search" @click="getAdmins">搜索</el-button>
          <el-button type="primary" icon="el-icon-plus" @click="dialogVisible=true">新增</el-button>
        </el-col>
      </el-row>
    </div>
    <!-- 内容 -->
    <div class="content">
      <el-table :data="administrators" v-loading="loading">
        <el-table-column prop="Avatar" label="头像"></el-table-column>
        <el-table-column prop="Name" label="姓名"></el-table-column>
        <el-table-column prop="Type" label="管理员类型"></el-table-column>
        <el-table-column prop="Status" label="角色状态"></el-table-column>
        <el-table-column label="操作">
          <template slot-scope="scope">
            <el-button
              type="primary"
              icon="el-icon-edit"
              circle
              @click="handleEdit(scope.$index, scope.row)"
            ></el-button>
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
    <!-- 弹出框 -->
    <div class="dialog">
      <el-dialog :title="dialogTitle" :visible.sync="dialogVisible">
        <el-form label-width="90px">
          <el-form-item label="姓名">
            <el-input type="text" v-model="modifyParams.name"></el-input>
          </el-form-item>
          <el-form-item label="密码">
            <el-input type="password" v-model="modifyParams.password"></el-input>
          </el-form-item>
          <el-form-item label="管理员类型">
            <el-select v-model="modifyParams.type" placeholder="管理员类型">
              <el-option
                v-for="item in typeOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="角色状态">
            <el-select v-model="modifyParams.status" placeholder="角色状态">
              <el-option
                v-for="item in statusOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" :loading="loading" @click="modifyAdmin">创建</el-button>
            <el-button @click="handleCancel">取消</el-button>
          </el-form-item>
        </el-form>
      </el-dialog>
    </div>
  </div>
</template>
<style lang="scss"></style>
<script src="./index.ts"></script>
