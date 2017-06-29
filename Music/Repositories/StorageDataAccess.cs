using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;
using ChangTing.Core.Models;
using ChangTing.Music.Models;

namespace ChangTing.Music.Repositories
{
    /// <summary>
    /// 操作歌曲表
    /// </summary>
   public class StorageDataAccess
    {
        #region SelectAllStorageWay
        /// <summary>
        /// 提取全部的类别信息
        /// </summary>
        /// <returns></returns>
        public IList<StorageInfo> SelectAllStorageWay()
        {
            Sql sql = Sql.Builder.Append("select StorageId,RealName,Path,Time,CategoryId,AlbumId,SingerId,Frequency,Display,ReleaseTime,CreateDate from Music_CT_Storage");
            return ConnectionPool.db.Fetch<StorageInfo>(sql);
        }
        #endregion

        #region SelectAlbumidAllStorageWay
        /// <summary>
        /// 获取当前专辑所有音乐的信息
        /// </summary>
        /// <returns></returns>
        public IList<StorageInfo> SelectAlbumidAllStorageWay(int albumid)
        {
            Sql sql = Sql.Builder.Append("select StorageId,RealName,Path,Time,CategoryId,AlbumId,SingerId,Frequency,Display,ReleaseTime,CreateDate from Music_CT_Storage where Albumid=@0", albumid);
            return ConnectionPool.db.Fetch<StorageInfo>(sql);
        }
        #endregion

        #region
        /// <summary>
        /// 获取该歌手的所有歌曲信息及专辑名
        /// </summary>
        /// <param name="SingerId"></param>
        /// <returns></returns>
        public IList<StorageInfo> SelectSingermusic(int SingerId)
        {
            Sql sql = Sql.Builder.Append("select Music_CT_Storage.StorageId, Music_CT_Storage.RealName,Music_CT_Storage.Path,Music_CT_Storage.CategoryId,Music_CT_Storage.AlbumId, Music_CT_Storage.SingerId, Music_CT_Storage.Frequency, Music_CT_Storage.Display,Music_CT_Album.Name from Music_CT_Storage  join Music_CT_Album on Music_CT_Storage.AlbumId = Music_CT_Album.AlbumId  and  Music_CT_Storage.SingerId=@0", SingerId);
            return ConnectionPool.db.Fetch<StorageInfo>(sql);
        }
        #endregion
        #region UpdateStorageWay
        /// <summary>
        /// 更改单条歌曲信息
        /// </summary>
        /// <param name="StorageInfo">歌曲信息</param>
        /// <returns></returns>
        public int UpdateStorageWay(StorageInfo Storageinfo)
        {
            return ConnectionPool.db.Update("Music_CT_Storage", "StorageId", Storageinfo);
        }
        #endregion


        #region UpdateStorageWay
        /// <summary>
        /// 当前ID号的音乐播放量+1
        /// </summary>
        /// <param name="StorageId">歌曲ID</param>
        /// <returns></returns>
        public int UpdateStorageFrequencyWay(int StorageId)
        {
            Sql sql = Sql.Builder.Append("update set Frequency=Frequency+1 from Music_CT_Storage where StorageId=@0", StorageId);
            return ConnectionPool.db.Execute(sql);
        }
        #endregion

        #region InsertStorageWay
        /// <summary>
        /// 添加单条歌曲信息
        /// </summary>
        /// <param name="SingerInfo">歌手信息</param>
        /// <returns></returns>
        public object InsertStorageWay(StorageInfo Storageinfo)
        {
            return ConnectionPool.db.Insert("Music_CT_Storage", "StorageId", Storageinfo);
        }
        #endregion


        #region SelectStorageWay
        /// <summary>
        /// 获取id号相等单条歌曲
        /// </summary>
        /// <param name="Storageid">音乐Id</param>
        /// <returns></returns>
        public StorageInfo SelectStorageWay(int Storageid)
        {
            Sql sql = Sql.Builder.Append("select StorageId,RealName,Path,Time,CategoryId,AlbumId,SingerId,Frequency,Display,ReleaseTime,CreateDate from Music_CT_Storage where StorageId=@0", Storageid);
            return ConnectionPool.db.FirstOrDefault<Models.StorageInfo>(sql);
        }
        #endregion


        #region DeleteStorageWay
        /// <summary>
        /// 根据音乐ID删除单条音乐数据的
        /// </summary>
        /// <returns></returns>
        public int DeleteStorageWay(int id)
        {
            Sql sql = Sql.Builder.Append("delete from Music_CT_Storage where StorageId=@0", id);
            return ConnectionPool.db.Execute(sql);
        }
        #endregion


        #region DeleteCategoryIdStorageWay
        /// <summary>
        /// 根据音乐类别ID删除音乐
        /// </summary>
        /// <returns></returns>
        public int DeleteCategoryIdStorageWay(int id)
        {
            Sql sql = Sql.Builder.Append("delete from Music_CT_Storage where CategoryId=@0", id);
            return ConnectionPool.db.Execute(sql);
        }
        #endregion
        #region
        /// <summary>
        /// 查询歌曲总数
        /// </summary>
        /// <returns></returns>
        public long musicCount(int singerid)
        {
            Sql sql = Sql.Builder.Append("select Count(1) from  Music_CT_Storage where SingerId=@0", singerid);
            return ConnectionPool.db.ExecuteScalar<long>(sql);
        }
        #endregion
    }
}