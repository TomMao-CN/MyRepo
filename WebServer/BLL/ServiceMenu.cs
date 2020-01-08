using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServiceMenu : SuperClass
    {
        #region 获取管理员管理的系统
        /// <summary>
        /// 获取管理员管理的系统
        /// </summary>
        /// <param name="adminID"></param>
        /// <returns></returns>
        public List<object> GetAdminMenu(int adminID)
        {
            List<object> lstResult = new List<object>();
            try
            {
              
                //找到管理员绑定的系统id
                List<int> sysMenuID = dataContext.AdminMenu.Where(m => m.AdminID == adminID && m.Status == (int)Models.Enums.SharedStatus.Normal).Select(m => m.MenuID).ToList();
                //找到绑定的系统
                List<Models.Menu> lstMenu = dataContext.Menu.Where(m => sysMenuID.Contains(m.ID) && m.Status == (int)Models.Enums.SharedStatus.Normal).ToList();
                //找到系统绑定的菜单
                List<Models.Menu> lstChildMenu = new List<Models.Menu>();
                foreach (Models.Menu objMenu in lstMenu)
                {
                    //子菜单容器
                    List<object> lstChildResult = new List<object>();
                    lstChildMenu = dataContext.Menu.Where(m => m.ParentID == objMenu.ID && m.Status == (int)Models.Enums.SharedStatus.Normal).ToList();
                    foreach (Models.Menu objChildMenu in lstChildMenu)
                    {
                        lstChildResult.Add(new
                        {
                            name = objChildMenu.Name,
                            path=objChildMenu.Path,
                            hidden=objChildMenu.Hidden
                        });
                    }

                    lstResult.Add(new
                    {
                        name = objMenu.Name,
                        path = objMenu.Path,
                        hidden = objMenu.Hidden,
                        icon = objMenu.Icon,
                        children=lstChildResult
                    });
                }


            }
            catch (Exception ex)
            {
                new BLL.ServiceException().AddExceptionLog(ex);
            }
            return lstResult;
        }
        #endregion
    }
}
