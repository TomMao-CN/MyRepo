# client

## Project setup
```
npm install
```

### Compiles and hot-reloads for development
```
npm run serve
```

### Compiles and minifies for production
```
npm run build
```

### Customize configuration
See [Configuration Reference](https://cli.vuejs.org/config/).

### 安装的组件

#### vue-echarts
```
通过cnpm install后，项目任然会报错
错误信息为缺少vue-echart相关组件
这时要运行终端
cnpm install echarts vue-echarts
```

#### font-awesome
```
font-awesome字体图标库
安装方式
npm install --save font-awesome
组件引入
import 'font-awesome/scss/font-awesome.scss'
```
#### axios
```
axios异步Ajax请求
安装方式
npm install --save axios vue-axios
组件引入
import axios from 'axios'
import VueAxios from 'vue-axios'
Vue.use(VueAxios, axios)
```

#### md5
```
安装方式
cnpm install --save js-md5
使用
import md5 from 'js-md5'

```
#### tinymce 富文本编辑器
```
安装方式
npm install tinymce -S
npm install @tinymce/tinymce-vue -S
参考网站
https://www.cnblogs.com/zhongchao666/p/11142537.html
```
