using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.Users.Models;
using ChangTing.Users.Repositories;
using ChangTing.Core.Models;
using ChangTing.Users.ViewModels;
using ChangTing.Users.Services;
using ChangTing.Singer.Services;
using ChangTing.Music.Services;

namespace ChangTing.Users.Services
{
    public class CollectionServiceLogic_Admin
    {
        CollectionDataAccess dal;
        #region SelectAllCollectionWay
        /// <summary>
        /// 获取所有用户收藏的信息
        /// </summary>
        /// <returns></returns>
        public IList<CollectionInfo> SelectAllCollectionWay()
        {
            dal = new CollectionDataAccess();
            return dal.SelectAllCollectionWay();
        }
        #endregion

        #region SelectUserIdCollectionWay

        /// <summary>
        /// 获取当前用户的收藏信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public IList<CollectionInfo> SelectUserIdCollectionWay(int userId)
        {
            dal = new CollectionDataAccess();
            return dal.SelectCollectionWay(userId);
        }

        public IList<CollectionViewInfo> CollectionViewInfoWay(IList<CollectionInfo> Collectioninfolist)
        {
            IList<CollectionViewInfo> CollectionViewinfolist = new List<CollectionViewInfo>();
            CollectionViewInfo CollectionViewinfo = new CollectionViewInfo();

            UsersServiceLogic_Admin userbll = new UsersServiceLogic_Admin();//用户的bll
            AlbumServiceLogic_Admin albumbll = new AlbumServiceLogic_Admin();//专辑的bll
            StorageServiceLogic_Admin storagebll = new StorageServiceLogic_Admin();//歌曲的bll

            foreach (CollectionInfo item in Collectioninfolist)
            {
                CollectionViewinfo = new CollectionViewInfo();
                CollectionViewinfo.CollectionId = item.CollectionId;//收藏ID
                CollectionViewinfo.UserId = item.UserId;//用户ID
                CollectionViewinfo.LoginName = userbll.SelectUserLoginNameWay(item.UserId);//登陆名
                if (item.StorageId !=0)
                {
                    CollectionViewinfo.StorageId = item.StorageId;//音乐ID
                    CollectionViewinfo.StorageName = storagebll.SelectStorageRealNameWay(item.StorageId);//音乐名获取
                    CollectionViewinfo.AlbumId = 0;//专辑ID
                    CollectionViewinfo.AlbumName = string.Empty;//专辑名获取
                }
                else if(item.AlbumId!=0)
                {
                    CollectionViewinfo.StorageId = 0;//音乐ID
                    CollectionViewinfo.StorageName = string.Empty;//音乐名获取
                    CollectionViewinfo.AlbumId = item.AlbumId;//专辑ID
                    CollectionViewinfo.AlbumName = albumbll.SelectAlbumNameWay(item.StorageId);//专辑名获取
                }
                CollectionViewinfo.CreateDate = item.CreateDate;//用户创建时间
                CollectionViewinfolist.Add(CollectionViewinfo);
            }
            return CollectionViewinfolist;
        }

        #endregion

        #region DeleteCollectionWay
        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteUserWay(string id)
        {
            int ID = 0;
            bool cg = int.TryParse(id, out ID);
            if (!cg)
            {
                return cg;
            }

            dal = new CollectionDataAccess();
            cg = dal.DeleteCollectionWay(ID) > 0;

            return cg;

        }
        #endregion

        #region DeleteSingerMultiWay
        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteCollectionMultiWay(string[] id)
        {
            dal = new CollectionDataAccess();
            int[] ID = new int[id.Length];
            int cg = 0;
            for (int i = 0; i < id.Length; i++)
            {
                ID[i] = int.Parse(id[i]);
                if (dal.DeleteCollectionWay(ID[i]) > 0)
                {
                    cg++;
                }
            }

            return cg;

        }
        #endregion

    }
}
