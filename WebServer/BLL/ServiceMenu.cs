using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServiceMenu : SuperClass
    {
        //#region 获取管理员管理的系统
        ///// <summary>
        ///// 获取管理员管理的系统
        ///// </summary>
        ///// <param name="adminID"></param>
        ///// <returns></returns>
        //public List<object> GetAdminMenu(int adminID)
        //{
        //    List<object> list = new List<object>();
        //    try
        //    {
              
        //        //找到管理员绑定的系统id
        //        List<int> sysMenuID = dataContext.AdminMenu.Where(m => m.AdminID == adminID && m.Status == (int)Models.Enums.SharedStatus.Normal).Select(m => m.MenuID).ToList();
        //        //找到绑定的系统
        //        List<Models.Menu> menus = dataContext.Menu.Where(m => sysMenuID.Contains(m.ID) && m.Status == (int)Models.Enums.SharedStatus.Normal).ToList();
        //        //找到系统绑定的菜单
        //        List<Models.Menu> childMenus = new List<Models.Menu>();
        //        foreach (Models.Menu menu in menus)
        //        {
        //            //子菜单容器
        //            List<object> childList = new List<object>();
        //            childMenus = dataContext.Menu.Where(m => m.ParentID == menu.ID && m.Status == (int)Models.Enums.SharedStatus.Normal).ToList();
        //            foreach (Models.Menu item in childMenus)
        //            {
        //                childList.Add(new
        //                {
        //                    name = item.Name,
        //                    path=item.Path,
        //                    hidden=item.Hidden
        //                });
        //            }

        //            list.Add(new
        //            {
        //                name = menu.Name,
        //                path = menu.Path,
        //                hidden = menu.Hidden,
        //                icon = menu.Icon,
        //                children=childList
        //            });
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        new BLL.ServiceException().AddExceptionLog(ex);
        //    }
        //    return list;
        //}
        //#endregion
    }
}
