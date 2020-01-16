module.exports = {
    // 基本路径
    publicPath: process.env.NODE_ENV === 'production'
        ? './'
        : '/',
    // 输出文件目录
    outputDir: 'dist',
    //新版的webpack-dev-server增加了安全验证，默认检查hostname，如果hostname不是配置内的，将中断访问。
    devServer: {
        disableHostCheck: true,
        port: 8080,
    }
}