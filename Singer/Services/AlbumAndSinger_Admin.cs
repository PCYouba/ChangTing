using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.Singer.Models;
using ChangTing.Singer.Repositories;
using ChangTing.Core.Models;

namespace ChangTing.Singer.Services
{
    /// <summary>
    /// 专辑与歌手的交接
    /// </summary>
    public class AlbumAndSinger_Admin
    {
        AlbumDataAccess dal;

        #region 获取所有歌手
        /// <summary>
        /// 获取所有歌手
        /// </summary>
        /// <returns></returns>
        public IList<SingerInfo> SelectAllSingerNameWay()
        {
            SingerServiceLogic dal = new SingerServiceLogic();
            IList<SingerInfo> singerinfolist = dal.SelectAllSingerWay();
            return paixv(singerinfolist);
        }
        #endregion

        #region 歌手id显示专辑
        /// <summary>
        /// 歌手id显示专辑
        /// </summary>
        /// <returns></returns>
        public IList<AlbumInfo> SelectSingerIdAlbumWay(int singerid)
        {
            dal = new AlbumDataAccess();
            return dal.SelectSingerIdAlbumWay(singerid);
        }
        #endregion


        #region paixv
        /// <summary>
        /// 姓名排序返回
        /// </summary>
        /// <returns></returns>
        public IList<SingerInfo> paixv(IList<SingerInfo> singerinfolist)
        {
            IList<SingerInfo> listinfo = new List<SingerInfo>();
            foreach (SingerInfo item in singerinfolist)
            {
                SingerInfo singerinfo = new SingerInfo();
                singerinfo.SingerId = item.SingerId;
                singerinfo.Name = item.Name;
                listinfo.Add(singerinfo);
            }

            listinfo = (from list in listinfo
                        orderby list.Name descending
                    select list).ToList();
            return listinfo;
        }

        #endregion
    }
}
