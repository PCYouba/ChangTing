using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.Core.Models;
using PetaPoco;

namespace ChangTing.Admin.Repositories
{
    public class updatePassworddb
    {
        /// <summary>
        /// 更改密码用的数据访问
        /// </summary>
        public int updatePassworddbWay(int adminid,string password)
        {
            Sql sql = Sql.Builder.Append("update Music_CT_Admin set Password=@0 where AdminID=@1", password, adminid);
            return ConnectionPool.db.Execute(sql);
        }
    }
}
