using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.Core.Models;
using PetaPoco;

namespace ChangTing.Singer.Repositories
{
    public  class AlbumAndStorageDateAccess
    {
        #region DeleteSingerIdStorageWay
        /// <summary>
        /// 删除与歌手ID相等的所有音乐
        /// </summary>
        /// <returns></returns>
        public int DeleteAlbumIdStorageWay(int id)
        {
            Sql sql = Sql.Builder.Append("delete from Music_CT_Storage where AlbumId=@0", id);
            return ConnectionPool.db.Execute(sql);
        }
        #endregion
    }
}
