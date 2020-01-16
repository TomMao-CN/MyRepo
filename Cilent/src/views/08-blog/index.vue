<template>
  <div class="every-page">
    <div class="toolbar">
      <el-row>
        <el-col :span="4">
          <el-input placeholder="请输入标题/作者" v-model="searchParams.keyword"></el-input>
        </el-col>
        <el-col :span="4"></el-col>
        <el-col :span="4">
          <el-select placeholder="数据状态" v-model="searchParams.status">
            <el-option
              v-for="item in statusOptions"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-col>
        <el-col :span="8">
          <el-button type="primary" icon="el-icon-search" @click="getBlogs">搜索</el-button>
          <el-button type="primary" icon="el-icon-plus" @click="dialogVisible=true">新增</el-button>
          <el-button type="primary" icon="el-icon-caret-right" @click="exportBlogs">导出</el-button>
        </el-col>
      </el-row>
    </div>
    <div class="content">
      <el-table :data="blogs" v-loading="loading">
        <el-table-column prop="Title" label="标题"></el-table-column>
        <el-table-column prop="Cover" label="封面">
          <template slot-scope="scope">
            <el-image style="width: 100px; height: 100px" :src="scope.row.Cover" fit="fit"></el-image>
          </template>
        </el-table-column>
        <el-table-column prop="Author" label="作者"></el-table-column>
        <el-table-column prop="CreateTime" label="创建时间"></el-table-column>
        <el-table-column prop="Status" label="状态" :formatter="convertStatus"></el-table-column>
        <el-table-column label="内容">
          <template slot-scope="scope">
            <el-button
              type="success"
              icon="el-icon-document"
              circle
              @click="showContent(scope.$index, scope.row)"
            ></el-button>
          </template>
        </el-table-column>
        <el-table-column label="操作">
          <template slot-scope="scope">
            <el-button
              type="primary"
              icon="el-icon-edit"
              circle
              :loading="loading"
              @click="handleEdit(scope.$index, scope.row)"
            ></el-button>
            <el-button
              type="danger"
              icon="el-icon-delete"
              circle
              :loading="loading"
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
      <!-- tinymce在弹出框中会出现层级问题，需要修改css中的z-index -->
      <!-- destroy-on-close="true"消除tinymce再弹出问题 -->
      <el-dialog :title="dialogTitle" :visible.sync="dialogVisible" :destroy-on-close="true">
        <el-form label-width="100px" :model="modifyParams" :rules="modifyRules" ref="modifyParams">
          <el-form-item label="标题" prop="title">
            <el-input type="text" v-model="modifyParams.title"></el-input>
          </el-form-item>
          <el-form-item label="作者" prop="author">
            <el-input type="text" v-model="modifyParams.author"></el-input>
          </el-form-item>
          <el-form-item label="封面" prop="cover">
            <el-upload
              class="avatar-uploader"
              :action="imageUploadUrl"
              :show-file-list="false"
              :on-success="handleAvatarSuccess"
              :before-upload="beforeAvatarUpload"
            >
              <img v-if="modifyParams.cover" :src="modifyParams.cover" class="avatar" />
              <i v-else class="el-icon-plus avatar-uploader-icon"></i>
            </el-upload>
          </el-form-item>
          <el-form-item label="数据状态" prop="status">
            <el-select v-model="modifyParams.status" placeholder="数据状态">
              <el-option
                v-for="item in statusOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="内容" prop="content">
            <tinymce ref="editor" v-model="modifyParams.content" :disabled="false" />
            <!-- <vue-ueditor-wrap v-model="modifyParams.content" :config="ueConfig"></vue-ueditor-wrap> -->
          </el-form-item>
          <div></div>
          <el-form-item>
            <el-button type="primary" :loading="loading" @click="modifyBlog">确定</el-button>
            <el-button @click="handleCancel">取消</el-button>
          </el-form-item>
        </el-form>
      </el-dialog>
      <el-dialog title="内容" :visible.sync="dialogContent">
        <div v-html="blogContent"></div>
      </el-dialog>
    </div>
  </div>
</template>
<style lang="scss">
</style>
<script src="./index.ts"></script>
