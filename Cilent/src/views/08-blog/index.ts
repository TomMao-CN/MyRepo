import Vue from "vue";
import tinymce from '@/components/tinymce.vue'

import { SharedEnums } from "@/common/enums";
import { SharedData } from '@/common/shared-data';
import { Utils } from '@/common/utils';

export default Vue.extend({
    components: {
        tinymce
    },
    data() {
        return {
            searchParams: {
                keyword: '',
                status: null,
                page: 1
            },
            modifyParams: {
                id: null,
                title: null,
                cover: null,
                author: null,
                content: '',
                status: null
            },
            modifyRules: {
                title: [{ required: true, message: '标题不能为空！', trigger: 'blur' }],
                cover: [{ required: true, message: '封面不能为空！', trigger: 'blur' }],
                author: [{ required: true, message: '作者不能为空！', trigger: 'blur' }],
                content: [{ required: true, message: '内容不能为空！', trigger: 'blur' }],
                status: [{ required: true, message: '状态不能为空！', trigger: 'blur' }],
            },
            loading: false,
            totalCount: 0,
            pageSize: 0,
            currentPage: 0,
            //table的初始值得是数组
            blogs: [],
            blogContent: '',
            
            statusOptions: SharedData.DataStatusOptions,

            dialogVisible: false,
            dialogContent: false,
            dialogTitle: '新增',
            imageUploadUrl: SharedData.ApiUrl + "Shared/UploadImage",

        };
    },
    methods: {
        /**
         * 获取博客
         */
        getBlogs() {
            this.loading = true;
            let url: string = SharedData.ApiUrl + "Blog/GetBlogs";
            let params = {
                keyword: this.searchParams.keyword,
                status: this.searchParams.status,
                page: this.searchParams.page
            }
            Utils.HandleRequest.PostRequest(url, params).then((res: any) => {
                this.blogs = res.list;
                this.loading = false;
                this.totalCount = res.totalCount;
                this.pageSize = res.pageSize;
                this.currentPage = res.page;
                this.searchParams.page = 1;
            });
        },
        /**
         * 更改博客
         */
        modifyBlog() {
            this.$refs.modifyParams.validate((valid: string | number) => {
                if (valid) {
                    this.loading = true;
                    let url: string = SharedData.ApiUrl + "Blog/ModifyBlog";
                    let params = {
                        ID: this.modifyParams.id,
                        Title: this.modifyParams.title,
                        Cover: this.modifyParams.cover,
                        Author: this.modifyParams.author,
                        Content: this.modifyParams.content,
                        Status: this.modifyParams.status
                    }
                    Utils.HandleRequest.PostRequest(url, params).then((res: any) => {
                        this.loading = false;
                        this.dialogVisible = false;
                        this.getBlogs();
                        this.modifyParams = {
                            title: '',
                            cover: '',
                            author: '',
                            content: '',
                            status: null
                        };

                        Utils.ElementUI.MessageTips("执行成功！", 1);
                    });
                }
            });
        },
        /**
         * 导出博客
         */
        exportBlogs() {
            let url: string = SharedData.ApiUrl + "Blog/ExportBlogs";
            let params = {
                keyword: this.searchParams.keyword,
                status: this.searchParams.status,
            }
            Utils.HandleRequest.PostRequest(url, params).then((res: any) => {
                console.log(res);
                window.location.href=res.url;
            });
        },
        /**
         * 编辑
         * @param index 
         * @param row 
         */
        handleEdit(index: number, row: any) {
            this.dialogVisible = true;
            this.dialogTitle = "编辑";
            this.modifyParams.id = row.ID;
            this.modifyParams.title = row.Title;
            this.modifyParams.cover = row.Cover;
            this.modifyParams.author = row.Author;
            this.modifyParams.content = row.Content;
            this.modifyParams.status = row.Status;
        },
        /**
         * 删除
         * @param index 
         * @param row 
         */
        handleDelete(index: number, row: any) {
            this.loading = true;
            let url: string = SharedData.ApiUrl + "Blog/DeleteBlog";
            let params = {
                blogID: row.ID
            }
            Utils.HandleRequest.PostRequest(url, params).then((res: any) => {
                this.loading = false;
                Utils.ElementUI.MessageTips("删除成功！", 2);
                this.getBlogs();
            });
        },
        /**
         * 展示内容
         * @param index 
         * @param row 
         */
        showContent(index: number, row: any) {
            this.dialogContent = true;
            this.blogContent = row.Content;
        },
        /**
        * 处理分页
        * @param page 页码 
        */
        handleCurrentChange(page: any) {
            this.searchParams.page = page;
            this.getBlogs();
        },
        /**
       * 格式化角色状态
       * @param row 
       * @param column 
       */
        convertStatus(row: any, column: any) {
            return Utils.HandleEnums.ConvertValueToLabel(SharedData.DataStatusOptions, row.Status);
        },
        /**
       * 头像上传成功前的处理
       * @param file 
       */
        beforeAvatarUpload(file: any) {
            if (file.type != 'image/jpeg') {
                Utils.ElementUI.MessageTips('图片格式必须为jpg!', 3);
                return false;
            }
            //1k等于1024b
            if (file.size > 1024 * 100) {
                Utils.ElementUI.MessageTips('图片大小不得超过100k!', 3);
                return false;
            }
            return true;
        },
        /**
         * 头像上传成功后的处理
         * @param res 
         * @param file 
         */
        handleAvatarSuccess(res: any, file: any) {
            this.modifyParams.cover = file.response.data;
        },
        /**
         * 取消
         */
        handleCancel() {
            this.dialogVisible = false;
            this.modifyParams = {
                title: '',
                cover: '',
                author: '',
                content: '',
                status: null
            };
            this.dialogTitle = '新增';
            this.$refs.modifyParams.resetFields();
        }
    },
    mounted() {
        this.getBlogs();
    }
});