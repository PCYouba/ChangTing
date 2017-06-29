using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.Admin.Repositories;

namespace ChangTing.Admin.Services
{
    /// <summary>
    /// 管理员查询
    /// </summary>
     public class SelectAdmin
    {
        /// <summary>
        /// 通过管理员ID查询管理员登录名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string SelectIdAdminNameWay(int id)
        {
            idAndAlldb dal = new idAndAlldb();
            return dal.idAndAlldbWay(id).LoginName;
        }
        

    }
}
