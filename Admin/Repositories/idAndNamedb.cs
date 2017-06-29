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
    /// 通过管理员ID获取管理员信息
    /// </summary>
     public class idAndAlldb
    {
        /// <summary>
        /// 通过管理员ID获取管理员信息
        /// </summary>
        /// <param name="id">管理员iD</param>
        /// <returns>匹配返回这个用户的数据，不匹配返回null</returns>
        public AdminInfo idAndAlldbWay(int id)
        {
            Sql sql = Sql.Builder.Append("select AdminId,LoginName,Password,RealName,CreateDate from Music_CT_Admin where AdminId = @1", id);
            return ConnectionPool.db.FirstOrDefault<AdminInfo>(sql);
        }
    }
}
