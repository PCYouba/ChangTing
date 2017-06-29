using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.Music.Repositories;
using ChangTing.Music.Models;

namespace ChangTing.Music.Services
{
    /// <summary>
    /// 跳转音乐所需音乐类别信息
    /// </summary>
     public class CategoryAndStorage_Admin
    {

        CategoryDataAccess dal;

        #region 获取所有歌手
        /// <summary>
        /// 获取所有音乐类别
        /// </summary>
        /// <returns></returns>
        public IList<StorageInfo> SelectAllSingerNameWay()
        {
            StorageServiceLogic_Admin dal = new StorageServiceLogic_Admin();
            IList<StorageInfo> storageinfolist = dal.SelectAllStorageWay();
            return paixv(storageinfolist);
        }
        #endregion
        #region paixv
        /// <summary>
        /// 姓名排序返回
        /// </summary>
        /// <returns></returns>
        public IList<StorageInfo> paixv(IList<StorageInfo> storageinfolist)
        {
            IList<StorageInfo> listinfo = new List<StorageInfo>();
            foreach (StorageInfo item in storageinfolist)
            {
                StorageInfo storageinfo = new StorageInfo();
                storageinfo.CategoryId = item.CategoryId;
                storageinfo.RealName = item.RealName;
                listinfo.Add(storageinfo);
            }

            listinfo = (from list in listinfo
                        orderby list.RealName descending
                        select list).ToList();
            return listinfo;
        }



        #region 歌曲类别显示歌曲
        /// <summary>
        /// 歌曲类别显示歌曲
        /// </summary>
        /// <returns></returns>
        public IList<CategoryInfo> SelectSingerIdAlbumWay(int categoryid)
        {
            dal = new  CategoryDataAccess();
            return dal.SelectSingerIdCategoryWay(categoryid);
        }
        #endregion

        #endregion

    }
}
