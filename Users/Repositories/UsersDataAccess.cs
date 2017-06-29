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
    /// 操作用户表
    /// </summary>
    public class UsersDataAccess
    {
        #region SelectAllUsersWay
        /// <summary>
        /// 提取全部的用户信息
        /// </summary>
        /// <returns></returns>
        public IList<UsersInfo> SelectAllUsersWay()
        {
            Sql sql = Sql.Builder.Append("select UserId,NickName,HeadPortrait,Introduce,Gender,Birthday,Region,Available,CreateDate from Music_CT_Users");
            return ConnectionPool.db.Fetch<UsersInfo>(sql);
        }
        #endregion
        
        #region SelectUserWay
        /// <summary>
        /// 根据用户id提取本用户全部信息
        /// </summary>
        /// <param name="userid">用户Id</param>
        /// <returns></returns>
        public UsersInfo SelectUserWay(int userid)
        {
            Sql sql = Sql.Builder.Append("select UserId,LoginName,PassWord,NickName,HeadPortrait,Introduce,Gender,Birthday,Region,Available,CreateDate from Music_CT_Users where UserId=@0", userid);
            return ConnectionPool.db.First<UsersInfo>(sql);
        }
        #endregion
        
        #region UpdateUserWay
        /// <summary>
        /// 更改单条用户信息
        /// </summary>
        /// <param name="usersinfo">用户信息</param>
        /// <returns></returns>
        public int UpdateUserWay(UsersInfo usersinfo)
        {
            return ConnectionPool.db.Update("Music_CT_Users", "UserId", usersinfo);
        }
        #endregion

        #region UpdateUserAvailableWay
        /// <summary>
        /// 更改单条用户的状态信息
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="available">用户状态</param>
        /// <returns></returns>
        public int UpdateUserAvailableWay(int userid,int available)
        {
            Sql sql = Sql.Builder.Append("update Music_CT_Users set Available=@0 where UserId=@1", available, userid);
            return ConnectionPool.db.Execute(sql);
        }
        #endregion
        
        #region InsertUserWay
        /// <summary>
        /// 添加单条用户信息
        /// </summary>
        /// <param name="UsersInfo">用户信息</param>
        /// <returns></returns>
        public object InsertUserWay(UsersInfo usersinfo)
        {
            return ConnectionPool.db.Insert("Music_CT_Users", "UserId", usersinfo);
        }
        #endregion
        
        #region DeleteUserWay
        /// <summary>
        /// 删除单条用户数据的
        /// </summary>
        /// <returns></returns>
        public int DeleteUserWay(int id)
        {
            Sql sql = Sql.Builder.Append("delete from Music_CT_Users where UserId=@0", id);
            return ConnectionPool.db.Execute(sql);
        }
        #endregion
        
    }
}
