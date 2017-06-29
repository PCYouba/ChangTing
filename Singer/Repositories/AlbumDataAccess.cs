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
    /// 操作专辑表
    /// </summary>
    public class AlbumDataAccess
    {
        #region SelectAllAlbumWay
        /// <summary>
        /// 提取全部的专辑信息
        /// </summary>
        /// <returns></returns>
        public IList<AlbumInfo> SelectAllAlbumWay()
        {
            Sql sql = Sql.Builder.Append("select AlbumId,Name,Image,Introduce,SingerId,Issue,CreateDate from Music_CT_Album");
            return ConnectionPool.db.Fetch<Models.AlbumInfo>(sql);
        }
        #endregion

        #region
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="itemsPerPage">每页显示个数</param>
        /// <returns></returns>
        public Page<AlbumInfo> AlbumfenyeList(int page, int PageSize)
        {
            Sql sql = Sql.Builder
                .Select("*")
                .From("Music_CT_Album")
                .Where("AlbumId>=1")
                .OrderBy("AlbumId desc");
            return ConnectionPool.db.Page<AlbumInfo>(page, PageSize, sql);
        }
        #endregion

        #region SelectSingerIdAlbumWay
        /// <summary>
        /// 提取当前歌手全部的专辑信息
        /// </summary>
        /// <param name="albumid">歌手Id</param>
        /// <returns></returns>
        public IList<AlbumInfo> SelectSingerIdAlbumWay(int singerid)
        {
            Sql sql = Sql.Builder.Append("select AlbumId,Name,Image,Introduce,SingerId,Issue,CreateDate from Music_CT_Album where SingerId=@0", singerid);
            return ConnectionPool.db.Fetch<Models.AlbumInfo>(sql);
        }
        #endregion


        #region UpdateAlbumWay
        /// <summary>
        /// 更改单条歌手信息
        /// </summary>
        /// <param name="AlbumInfo">歌手信息</param>
        /// <returns></returns>
        public int UpdateAlbumWay(AlbumInfo albuminfo)
        {
            return ConnectionPool.db.Update("Music_CT_Album", "AlbumId", albuminfo);
        }
        #endregion


        #region InsertAlbumWay
        /// <summary>
        /// 更改单条歌手信息
        /// </summary>
        /// <param name="SingerInfo">歌手信息</param>
        /// <returns></returns>
        public object InsertAlbumWay(AlbumInfo albuminfo)
        {
            return ConnectionPool.db.Insert("Music_CT_Album", "AlbumId", albuminfo);
        }
        #endregion


        #region SelectAlbumWay
        /// <summary>
        /// 获取id号相等单条专辑信息
        /// </summary>
        /// <param name="albumid">专辑Id</param>
        /// <returns></returns>
        public AlbumInfo SelectAlbumWay(int albumid)
        {
            Sql sql = Sql.Builder.Append("select AlbumId,Name,Image,Introduce,SingerId,Issue,CreateDate from Music_CT_Album where AlbumId=@0", albumid);
            return ConnectionPool.db.FirstOrDefault<AlbumInfo>(sql);
        }
        #endregion


        #region DeleteAlbumWay
        /// <summary>
        /// 删除单条专辑数据的
        /// </summary>
        /// <returns></returns>
        public int DeleteAlbumWay(int id)
        {
            Sql sql = Sql.Builder.Append("delete from Music_CT_Album where AlbumId=@0", id);
            return ConnectionPool.db.Execute(sql);
        }
        #endregion
    }
}
