using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServiceAuthority : SuperClass
    {

        #region 获取管理员系统
        public List<object> GetAdminMenu(int adminID)
        {
            List<object> list = new List<object>();
            try
            {
                List<int> sysIDs = dataContext.AdminSys.Where(m => m.AdminID == adminID && m.Status == (int)Models.Enums.SharedStatus.Normal).Select(m => m.SysID).ToList();   
                List<Models.AuthoritySys> authoritySyses = dataContext.AuthoritySys.Where(m => sysIDs.Contains(m.ID) && m.Status == (int)Models.Enums.SharedStatus.Normal).ToList();
                List<Models.SysMenus> sysMenuses = new List<Models.SysMenus>();
                foreach(Models.AuthoritySys item in authoritySyses)
                {
                    List<object> _list = new List<object>();
                    sysMenuses = dataContext.SysMenus.Where(m => m.SysID == item.ID && m.Status == (int)Models.Enums.SharedStatus.Normal).ToList();
                    foreach(Models.SysMenus _item in sysMenuses)
                    {
                        _list.Add(new {
                            name=_item.Name,
                            path=_item.Path,
                            display=_item.Display
                        });
                    }
                    list.Add(new {
                        name=item.Name,
                        path=item.Path,
                        icon=item.Icon,
                        display=item.Display,
                        children=_list
                    });
                }
            }
            catch(Exception ex)
            {
                new BLL.ServiceException().AddExceptionLog(ex);
            }
            return list;
        }
        #endregion
    }
}
