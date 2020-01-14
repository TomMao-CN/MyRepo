import { SharedEnums } from '@/common/enums';
export namespace SharedData {
    //请求接口域名
    //花生壳域名
    // export const ApiUrl = "http://2d8665909o.wicp.vip/";
    //本机开发域名
    export const ApiUrl = "http://tommao.com/";
    //当前登陆的管理员
    export let OnlineAdmin = JSON.parse(sessionStorage.getItem('admin'));
    //下拉框管理员类型
    export let AdminTypeOptions = [
        {
            value: SharedEnums.AdminType.SuperAdmin,
            label: '超级管理员'
        },
        {
            value: SharedEnums.AdminType.SecondaryAdmin,
            label: '次级管理员'
        }
    ];
    //下拉框角色状态
    export let RoleStatusOptions = [
        {
            value: SharedEnums.RoleStatus.Normal,
            label: '正常'
        },
        {
            value: SharedEnums.RoleStatus.Verify,
            label: '审核中'
        }
    ];
    //下拉框角色状态
    export let DataStatusOptions = [
        {
            value: SharedEnums.SharedStatus.Normal,
            label: '正常'
        },
        {
            value: SharedEnums.SharedStatus.Verify,
            label: '审核中'
        }
    ];

}
