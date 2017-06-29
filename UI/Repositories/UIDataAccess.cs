using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.UI.Viewmodels;

using ChangTing.Singer.Repositories;
using ChangTing.Singer.Models;
using ChangTing.Core.Models;
using PetaPoco;
using ChangTing.Users.Models;
using ChangTing.Admin.Models;
using System.Data.SqlClient;
using System.Data;

namespace ChangTing.UI.Repositories
{
    public class UIDataAccess
    {
        #region UserLogin
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public UsersInfo UserLogin(string uName, string uPass)
        {
          
            Sql sql = Sql.Builder.Append("select UserId,LoginName,PassWord,NickName,HeadPortrait,Introduce,Gender,Birthday,Region,Available from Music_CT_Users where LoginName=@0 and PassWord=@1", uName, uPass);
            return ConnectionPool.db.FirstOrDefault<UsersInfo>(sql);
        }
        #endregion

        #region UserLogin
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public object Register(UsersInfo uInfo)
        {
            return ConnectionPool.db.Insert("Music_CT_Users", "UserId", uInfo);
        }
        #endregion

        #region SelectUIUser
        /// <summary>
        /// 根据用户id查找用户信息
        /// </summary>
        /// <returns></returns>
        public IList<UsersInfo> SelectUIUser(int id)
        {
            Sql sql = Sql.Builder.Append("select UserId,LoginName,PassWord,NickName,HeadPortrait,Introduce,Gender,Birthday,Region,Available from Music_CT_Users where UserID=@0", id);
            return ConnectionPool.db.Fetch<UsersInfo>(sql);
        }
        #endregion

        #region SelectAllAlbumWay
        /// <summary>
        /// 提取前6的专辑信息
        /// </summary>
        /// <returns></returns>
        public IList<AlbumInfo> SelectNewestAlbumWay()
        {
            Sql sql = Sql.Builder.Append("select top 6 AlbumId,Name,Image,Introduce,SingerId,Issue,CreateDate from Music_CT_Album order by CreateDate");
            return ConnectionPool.db.Fetch<AlbumInfo>(sql);
        }
        #endregion













        #region SelectAllUIWay
        /// <summary>
        /// 提取歌手名、专辑名
        /// </summary>
        /// <returns></returns>
        public IList<UIInfo> SelectUIRecommend(int num)
        {
            string ss = string.Format("select top {0} Music_CT_Storage.RealName,Music_CT_Album.Image,Music_CT_Album.Name,Music_CT_Storage.Path from Music_CT_Storage join Music_CT_Album on (Music_CT_Album.AlbumID = Music_CT_Storage.AlbumID) order by Music_CT_Storage.Frequency", num);
            Sql sql = Sql.Builder.Append(ss);
            return ConnectionPool.db.Fetch<UIInfo>(sql);
        }
        #endregion

        #region SelectUINewest
        /// <summary>
        /// 提取专辑名、专辑图片、歌手名
        /// </summary>
        /// <returns></returns>
        public IList<UIInfo> SelectUINewest()
        {
            Sql sql = Sql.Builder.Append("select top  6 Music_CT_Album.Name,Music_CT_Album.Image,Music_CT_Singer.Name  from Music_CT_Singer join Music_CT_Album on (Music_CT_Album.SingerId = Music_CT_Album.SingerId) order by Music_CT_Album.CreateDate ");
         
            return ConnectionPool.db.Fetch<UIInfo>(sql);

        }
        #endregion

        #region SelectAllUIWay
        /// <summary>
        /// 提取歌手名 歌手头像
        /// </summary>
        /// <returns></returns>
        public IList<SingerInfo> SelectSinger(int num)
        {
            string ss = string.Format("select top {0} Name,HeadPortrait from Music_CT_Singer order by CreateDate", num);
            Sql sql = Sql.Builder.Append(ss);
            return ConnectionPool.db.Fetch<SingerInfo>(sql);
        }
        #endregion



        #region SelectAllUIWay
        /// <summary>
        /// 各种类别(华语1/粤语2/欧美3/日语4/韩语5)的歌曲
        /// </summary>
        /// <returns></returns>
        public IList<UIInfo> SelectUIMusic(int CategoryId, int PageSize)
        {
           string sss =  string.Format("select top {0} Music_CT_Storage.RealName, Music_CT_Album.Image from Music_CT_Storage join Music_CT_Album on (Music_CT_Album.AlbumId = Music_CT_Storage.AlbumId and Music_CT_Storage.CategoryId =@0)", PageSize);
            Sql sql = Sql.Builder.Append(sss, CategoryId);
            return ConnectionPool.db.Fetch<UIInfo>(sql);
        }
        #endregion
        
    }
}
