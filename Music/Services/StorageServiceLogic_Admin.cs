using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.Music.Models;
using ChangTing.Music.Repositories;

namespace ChangTing.Music.Services
{
    /// <summary>
    /// 歌曲业务逻辑层
    /// </summary>
    public class StorageServiceLogic_Admin
    {

        StorageDataAccess dal;

        #region SelectAllStorageWay
        /// <summary>
        /// 获取所有音乐的信息
        /// </summary>
        /// <returns></returns>
        public IList<StorageInfo> SelectAllStorageWay()
        {
            dal = new StorageDataAccess();
            return dal.SelectAllStorageWay();
        }
        #endregion

        #region SelectAlbumidAllStorageWay
        /// <summary>
        /// 获取当前专辑所有音乐的信息
        /// </summary>
        /// <returns></returns>
        public IList<StorageInfo> SelectAlbumidAllStorageWay(int albumid)
        {
            dal = new StorageDataAccess();
            return dal.SelectAlbumidAllStorageWay(albumid);
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
            dal = new StorageDataAccess();
            return dal.SelectSingermusic(SingerId);
        }
        #endregion
        #region SelectStorageWay
        /// <summary>
        /// 获取id号相等单条音乐信息
        /// </summary>
        /// <param name="Storageid">音乐Id</param>
        /// <returns></returns>
        public StorageInfo SelectStorageWay(int Storageid)
        {
            dal = new StorageDataAccess();
            return dal.SelectStorageWay(Storageid);
        }
        #endregion


        #region SelectStorageRealNameWay
        /// <summary>
        /// 获取id号相等单条音乐名
        /// </summary>
        /// <param name="Storageid">音乐Id</param>
        /// <returns></returns>
        public string SelectStorageRealNameWay(int Storageid)
        {
            dal = new StorageDataAccess();
            StorageInfo storageinfo = dal.SelectStorageWay(Storageid);
            if (storageinfo != null && storageinfo != new StorageInfo())
            {
                return storageinfo.RealName;
            }
            return "当前音乐不存在!";
        }
        #endregion


        //#region SelectStorageRealNameWay
        ///// <summary>
        ///// 获取id号相等单条音乐的播放个数
        ///// </summary>
        ///// <param name="Storageid">音乐Id</param>
        ///// <returns></returns>
        //public int SelectStorageFrequencyWay(int Storageid)
        //{
        //    dal = new StorageDataAccess();
        //    StorageInfo storageinfo = dal.SelectStorageWay(Storageid);
        //    if (storageinfo != null && storageinfo != new StorageInfo())
        //    {
        //        return storageinfo.Frequency;
        //    }
        //    return 0;
        //}
        //#endregion

        #region SelectStorageRealNameWay
        /// <summary>
        /// 当前ID号的音乐播放量+1
        /// </summary>
        /// <param name="Storageid">音乐Id</param>
        /// <returns></returns>
        public int SelectStorageFrequencyWay(int Storageid)
        {
            dal = new StorageDataAccess();
            return dal.UpdateStorageFrequencyWay(Storageid);
        }
        #endregion


        #region UpdateStorageWay
        /// <summary>
        /// 修改id号相等单条歌曲信息
        /// </summary>
        /// <param name="Storageid">歌曲Id</param>
        /// <returns></returns>
        public string UpdateStorageWay(StorageInfo Storageinfo)
        {
            dal = new StorageDataAccess();
            string fanhuizhi = null;
            bool x = dal.UpdateStorageWay(Storageinfo) > 0;
            if (x)
            {
                fanhuizhi = "已成功修改歌曲信息！";
            }
            else
            {
                fanhuizhi = "修改信息失败！请联系管理员";
            }
            return fanhuizhi;
        }
        #endregion


        #region InsertStorageWay
        /// <summary>
        /// 新建歌曲信息
        /// </summary>
        /// <param name="Storageid">歌曲Id</param>
        /// <returns></returns>
        public string InsertStorageWay(StorageInfo Storageinfo)
        {
            dal = new StorageDataAccess();
            string fanhuizhi = null;

            Storageinfo.CreateDate = DateTime.Now;

            bool x = dal.InsertStorageWay(Storageinfo) != null;
            if (x)
            {
                fanhuizhi = "歌曲信息新建成功！";
            }
            else
            {
                fanhuizhi = "歌曲信息新建失败！请联系管理员";
            }
            return fanhuizhi;
        }
        #endregion


        #region DeleteStorageWay
        /// <summary>
        /// 删除单条音乐数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object[] DeleteStorageWay(string id)
        {
            int ID = 0, pl = 0;
            bool cg = int.TryParse(id, out ID);
            StorageAndCommentDataAccess comdal = new StorageAndCommentDataAccess();
            pl = comdal.DeleteStorageIdCommentWay(ID);//先删除评论
            StorageServiceLogic_Admin bll = new StorageServiceLogic_Admin();
            dal = new StorageDataAccess();
            cg = dal.DeleteStorageWay(ID) > 0;//删除音乐
            return new object[] { cg, pl };
        }


        #endregion


        #region DeleteStorageMultiWay
        /// <summary>
        /// 删除多条音乐数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteStorageMultiWay(string[] id)
        {
            dal = new StorageDataAccess();
            StorageAndCommentDataAccess comdal = new StorageAndCommentDataAccess();
            int cg = 0, pl = 0, ID = 0;
            for (int i = 0; i < id.Length; i++)
            {
                ID = int.Parse(id[i]);
                pl += comdal.DeleteStorageIdCommentWay(ID);//先删除评论
                if (dal.DeleteStorageWay(ID) > 0)//再删除音乐
                {
                    cg++;
                }
            }
            return cg;

        }
        #endregion
        #region
        /// <summary>
        /// 查询歌曲总数
        /// </summary>
        /// <returns></returns>
        public long musicCount(int singerid)
        {
            dal = new StorageDataAccess();
            return dal.musicCount(singerid);
        }
        #endregion
    }
}