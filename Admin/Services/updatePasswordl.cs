using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.Admin.Repositories;

namespace ChangTing.Admin.Services
{
    public class updatePasswordl
    {
        /// <summary>
        /// 更改密码用的业务逻辑
        /// </summary>
        public bool updatePasswordWayl(int adminid, string password)
        {
            updatePassworddb dal = new updatePassworddb();
            return dal.updatePassworddbWay(adminid,password)>0;
        }

        

    }
}
