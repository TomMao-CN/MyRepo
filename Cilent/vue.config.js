module.exports = {
    //新版的webpack-dev-server增加了安全验证，默认检查hostname，如果hostname不是配置内的，将中断访问。
    devServer: {
        disableHostCheck: true,
    }
}