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
        <el-table-column label="头像">
          <!-- slot-scope是属于Vue，叫做插槽。可获取每行的属性值-->
          <template slot-scope="scope">
            <el-avatar style="object-fit:fill;" :size="50" :src="scope.row.Avatar"></el-avatar>
          </template>
        </el-table-column>
        <el-table-column prop="Name" label="姓名"></el-table-column>
        <el-table-column prop="Type" label="管理员类型" :formatter="convetRoleType"></el-table-column>
        <el-table-column prop="Status" label="角色状态" :formatter="convertStatus"></el-table-column>
        <el-table-column prop="LoginTime" label="登录时间"></el-table-column>
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
          <el-form-item label="姓名" v-if="display">
            <el-input type="text" v-model="modifyParams.name"></el-input>
          </el-form-item>
          <el-form-item label="头像" v-if="display">
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
          <el-form-item label="密码" v-if="display">
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
            <el-button type="primary" :loading="loading" @click="modifyAdmin">确定</el-button>
            <el-button @click="handleCancel">取消</el-button>
          </el-form-item>
        </el-form>
      </el-dialog>
    </div>
  </div>
</template>
<style lang="scss">
.avatar-uploader .el-upload {
  border: 1px dashed #d9d9d9;
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
}
.avatar-uploader .el-upload:hover {
  border-color: #409eff;
}
.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 150px;
  height: 150px;
  line-height: 150px;
  text-align: center;
}
.avatar {
  width: 150px;
  height: 150px;
  display: block;
}
</style>
<script src="./index.ts"></script>
