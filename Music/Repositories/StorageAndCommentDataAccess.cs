using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.Singer.Models;
using PetaPoco;
using ChangTing.Core.Models;

namespace ChangTing.Music.Repositories
{
    /// <summary>
    /// 音乐数据操作用户评论的数据访问
    /// </summary>
     public class StorageAndCommentDataAccess
    {
        #region DeleteStorageIdCommentWay
        /// <summary>
        /// 删除与音乐ID相关的评论
        /// </summary>
        /// <returns></returns>
        public int DeleteStorageIdCommentWay(int id)
        {
            Sql sql = Sql.Builder.Append("delete from Music_CT_Comment where StorageId=@0", id);
            return ConnectionPool.db.Execute(sql);
        }
        #endregion
    }
}
