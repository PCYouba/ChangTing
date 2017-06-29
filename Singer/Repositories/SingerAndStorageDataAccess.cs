using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.Singer.Models;
using PetaPoco;
using ChangTing.Core.Models;

namespace ChangTing.Singer.Repositories
{
   /// <summary>
   /// 歌手表与音乐存储表结合的数据访问
   /// </summary>
     public class SingerAndStorageDataAccess
    {
        #region DeleteSingerIdStorageWay
        /// <summary>
        /// 删除与歌手ID相等的所有音乐
        /// </summary>
        /// <returns></returns>
        public int DeleteSingerIdStorageWay(int id)
        {
            Sql sql = Sql.Builder.Append("delete from Music_CT_Storage where StorageId=@0", id);
            return ConnectionPool.db.Execute(sql);
        }
        #endregion
    }
}
