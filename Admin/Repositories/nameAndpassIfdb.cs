using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;
using ChangTing.Core.Models;
using ChangTing.Admin.Models;

namespace ChangTing.Admin.Repositories
{
    /// <summary>
    /// 用户名密码是否匹配
    /// </summary>
    public class nameAndpassIfdb
    {
        /// <summary>
        /// 用户名密码是否匹配
        /// </summary>
        /// <param name="AdminInfo">用户的数据</param>
        /// <returns>匹配返回这个用户的数据，不匹配返回null</returns>
        public AdminInfo nameAndpassIfdbWay(string loginname,string password) {
           Sql sql = Sql.Builder.Append("select AdminId,LoginName,Password,RealName,CreateDate from Music_CT_Admin where LoginName = @0 and Password = @1", loginname, password);
           return  ConnectionPool.db.FirstOrDefault<AdminInfo>(sql);
        }
        
    }
}
