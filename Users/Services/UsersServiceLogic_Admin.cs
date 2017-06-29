using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.Users.Models;
using ChangTing.Users.Repositories;
using ChangTing.Core.Models;

namespace ChangTing.Users.Services
{
    /// <summary>
    /// 用户业务逻辑
    /// </summary>
    public class UsersServiceLogic_Admin
    {
        UsersDataAccess dal;
        #region SelectAllAlbumWay
        /// <summary>
        /// 获取所有用户的信息
        /// </summary>
        /// <returns></returns>
        public IList<UsersInfo> SelectAllUsersWay()
        {
            dal = new UsersDataAccess();
            return dal.SelectAllUsersWay();
        }
        #endregion

        #region SelectUserWay
        /// <summary>
        /// 获取id号相等单条用户信息
        /// </summary>
        /// <param name="userid">用户Id</param>
        /// <returns></returns>
        public UsersInfo SelectUserWay(int userid)
        {
            dal = new UsersDataAccess();
            return dal.SelectUserWay(userid);
        }
        #endregion

        #region SelectUserLoginNameWay
        /// <summary>
        /// 获取当前id的登陆名(账号)
        /// </summary>
        /// <param name="userid">用户Id</param>
        /// <returns></returns>
        public string SelectUserLoginNameWay(int userid)
        {
            dal = new UsersDataAccess();
            UsersInfo user = dal.SelectUserWay(userid);
            if (user == new UsersInfo() || user == null)
            {
                return "未获取到当前用户!";
            }
            else
            {
                return user.LoginName;
            }
        }
        #endregion

        #region SelectUserNickNameWay
        /// <summary>
        /// 获取当前id的用户名称(昵称)
        /// </summary>
        /// <param name="userid">用户Id</param>
        /// <returns></returns>
        public string SelectUserNickNameWay(int userid)
        {
            dal = new UsersDataAccess();
            UsersInfo user = dal.SelectUserWay(userid);
            if (user==new UsersInfo()||user==null)
            {
                return "未获取到当前用户!";
            }
            else
            {
                return user.NickName;
            }
        }
        #endregion

        #region UpdateAvailableOutUsersWay
        /// <summary>
        /// 通过Id修改用户为不可用
        /// </summary>
        /// <param name="userid">用户Id</param>
        /// <returns></returns>
        public int UpdateAvailableOutUsersWay(int userid)
        {
            dal = new UsersDataAccess();
 
            int fanhuizhi = 0;
            bool x = dal.UpdateUserAvailableWay(userid, 1) > 0;

            if (x)
            {
                fanhuizhi = userid;
            }
            else
            {
                fanhuizhi = 0;
            }
            return fanhuizhi;
        }
        #endregion

        #region UpdateAvailableUpUsersWay
        /// <summary>
        /// 通过Id修改用户为可用
        /// </summary>
        /// <param name="userid">用户Id</param>
        /// <returns></returns>
        public int UpdateAvailableUpUsersWay(int userid)
        {
            dal = new UsersDataAccess();
            
            int fanhuizhi = 0;
            bool x = dal.UpdateUserAvailableWay(userid, 0) > 0;

            if (x)
            {
                fanhuizhi = userid;
            }
            else
            {
                fanhuizhi = 0;
            }
            return fanhuizhi;
        }
        #endregion

        #region DeleteUserWay
        /// <summary>
        /// 删除单条用户数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object[] DeleteUserWay(string id)
        {
            
            int ID = 0;
            bool cg = int.TryParse(id, out ID);
            int sc = 0, pl = 0;
            if (!cg)
            {
                return new object[] { cg, sc, pl }; 
            }

            dal = new UsersDataAccess();
            
            CollectionDataAccess scdal = new CollectionDataAccess();
            sc = scdal.DeleteUserIdCollectionWay(ID);//先删除用户的收藏
            CommentDataAccess pldal = new CommentDataAccess();
            pl = pldal.DeleteUserIdCollectionWay(ID);//再删除评论
            string img = dal.SelectUserWay(ID).HeadPortrait;
            cg = dal.DeleteUserWay(ID) > 0;
            if (cg)
            {
                DeleteFile.DeleteFileWay(img);
            }
            return new object []{ cg, sc ,pl};

        }
        #endregion
        
        #region DeleteSingerMultiWay
        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteUsersMultiWay(string[] id)
        {
            dal = new UsersDataAccess();
            int[] ID = new int[id.Length];
            int cg = 0;
            int sc = 0, pl = 0;
            CollectionDataAccess coldal = new CollectionDataAccess();
            CommentDataAccess pldal = new CommentDataAccess();
            for (int i = 0; i < id.Length; i++)
            {
                sc += coldal.DeleteUserIdCollectionWay(ID[i]);//先删除用户的收藏
                pl += pldal.DeleteUserIdCollectionWay(ID[i]);//再删除评论
                string img = dal.SelectUserWay(int.Parse(id[i])).HeadPortrait;
                ID[i] = int.Parse(id[i]);
                if (dal.DeleteUserWay(ID[i]) > 0)
                {
                    cg++;
                    DeleteFile.DeleteFileWay(img);
                }
            }

            return cg;

        }
        #endregion

     
    }
}
