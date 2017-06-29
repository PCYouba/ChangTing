using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.Users.Models;
using PetaPoco;
using ChangTing.Core.Models;

namespace ChangTing.Users.Repositories
{
    /// <summary>
    /// 收藏表数据访问
    /// </summary>
     public class CollectionDataAccess
    {

        #region SelectAllCollectionWay
        /// <summary>
        /// 提取全部的用户收藏信息
        /// </summary>
        /// <returns></returns>
        public IList<CollectionInfo> SelectAllCollectionWay()
        {
            Sql sql = Sql.Builder.Append("select CollectionId,UserId,StorageId,AlbumId,CreateDate from Music_CT_Collection");
            return ConnectionPool.db.Fetch<CollectionInfo>(sql);
        }
        #endregion


        #region SelectCollectionWay
        /// <summary>
        /// 根据用户id提取全部收藏信息
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public IList<CollectionInfo> SelectCollectionWay(int UserId)
        {
            Sql sql = Sql.Builder.Append("select CollectionId,UserId,StorageId,AlbumId,CreateDate from Music_CT_Collection where UserId=@0", UserId);
            return ConnectionPool.db.Fetch<CollectionInfo>(sql);
        }
        #endregion


        #region UpdateCollectionWay
        /// <summary>
        /// 更改单条用户收藏信息
        /// </summary>
        /// <param name="Collectioninfo">用户信息</param>
        /// <returns></returns>
        public int UpdateCollectionWay(CollectionInfo Collectioninfo)
        {
            return ConnectionPool.db.Update("Music_CT_Collection", "CollectionId", Collectioninfo);
        }
        #endregion
        

        #region InsertCollectionWay
        /// <summary>
        /// 添加单条用户收藏信息
        /// </summary>
        /// <param name="CollectionInfo">用户收藏信息</param>
        /// <returns></returns>
        public object InsertCollectionWay(CollectionInfo Collectioninfo)
        {
            return ConnectionPool.db.Insert("Music_CT_Collection", "CollectionId", Collectioninfo);
        }
        #endregion


        #region DeleteCollectionWay
        /// <summary>
        /// 删除单条用户收藏数据的
        /// </summary>
        /// <returns></returns>
        public int DeleteCollectionWay(int Collectionid)
        {
            Sql sql = Sql.Builder.Append("delete from Music_CT_Collection where CollectionId=@0", Collectionid);
            return ConnectionPool.db.Execute(sql);
        }
        #endregion



        #region DeleteUserIdCollectionWay
        /// <summary>
        /// 删除某用户Id的全部收藏数据的
        /// </summary>
        /// <returns></returns>
        public int DeleteUserIdCollectionWay(int UserId)
        {
            Sql sql = Sql.Builder.Append("delete from Music_CT_Collection where UserId=@0", UserId);
            return ConnectionPool.db.Execute(sql);
        }
        #endregion

    }
}