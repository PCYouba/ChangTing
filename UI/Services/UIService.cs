
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.UI.Viewmodels;
using ChangTing.Singer.Models;
using ChangTing.Singer.Repositories;
using ChangTing.Users.Models;
using ChangTing.Admin.Models;

namespace ChangTing.UI.Services
{
    public class UIService
    {

        ChangTing.UI.Repositories.UIDataAccess dal = new ChangTing.UI.Repositories.UIDataAccess();
       


        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public UsersInfo UserLogin(string uName, string uPass)
        {
            return dal.UserLogin(uName, uPass);
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        public object Register(UsersInfo uInfo)
        {
            return dal.Register(uInfo);
        }

        /// <summary>
        /// 根据用户id查找用户信息
        /// </summary>
        /// <returns></returns>
        public IList<UsersInfo> SelectUIUser(int ID)
        {
            return dal.SelectUIUser(ID);
        }




        #region SelectNewestAlbumWay
        /// <summary>
        /// 获取热门专辑的信息
        /// </summary>
        /// <returns></returns>
        public IList<AlbumInfo> SelectNewestAlbumWay()
        {
            return dal.SelectNewestAlbumWay();
        }
        #endregion


        #region SelectUIRecommend
        /// <summary>
        /// 获取热门音乐的信息
        /// </summary>
        /// <returns></returns>
        public IList<UIInfo> SelectUIRecommend(int num)
        {
            return dal.SelectUIRecommend(num);
        }
        #endregion

        #region SelectUINewest
        /// <summary>
        /// 获取最新专辑的信息
        /// </summary>
        /// <returns></returns>
        public IList<UIInfo> SelectUINewest()
        {


            
            return dal.SelectUINewest();
        }
        #endregion

        


        #region SelectUINewest
        /// <summary>
        /// 获取最新歌手信息
        /// </summary>
        /// <returns></returns>
        public IList<SingerInfo> SelectSinger(int num)
        {
            return dal.SelectSinger(num);
        }
        #endregion






        #region SelectUIMusic
        /// <summary>
        /// 各种类别(华语1/粤语2/欧美3/日语4/韩语5)的歌曲
        /// </summary>
        /// <returns></returns>
        public IList<UIInfo> SelectUIMusic(int CategoryId, int page)
        {
            int MIN = (page - 1) * 12+1;
            int MAX = page * 12;
            int PageSize = MAX - MIN;
            
            return dal.SelectUIMusic(CategoryId, PageSize);
        }
        #endregion

        
    }
}
