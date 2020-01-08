using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Models;

namespace BLL
{
    public class SuperClass
    {
        protected DepositoryDataContext dataContext = new DepositoryDataContext();
    }
}
