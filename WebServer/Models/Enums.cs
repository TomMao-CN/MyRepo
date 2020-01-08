using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Enums
{
    #region 数据状态
    public enum SharedStatus
    {
        //正常
        Normal = 1,
        //审核中
        Verify = 2,
        //删除
        Delete = 3,
    }
    #endregion

    #region 角色状态
    public enum RoleStatus
    {
        //正常
        Normal = 1,
        //审核中
        Verify = 2,
        //删除
        Delete = 3,
    }
    #endregion

    #region 管理员类型
    public enum AdminType
    {
        //超级管理员
        SuperAdmin = 1,
        //次级管理员
        SecondaryAdmin = 2
    }
    #endregion

}
