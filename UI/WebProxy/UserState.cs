using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using ChangTing.Users.Models;

namespace ChangTing.UI.WebProxy
{
    public class UserState
    {
        /// <summary>
        /// 存储实体类（主要用于将用户信息存储到服务器上）
        /// </summary>
        /// <param name="objMod"></param>
        /// <returns></returns>
        public static void SetUserState(UsersInfo objMod)
        {
            HttpContext.Current.Session["UsersInfo"] = objMod;
        }
        /// <summary>
        /// 获取存储的实体类
        /// </summary>
        /// <returns></returns>
        public static UsersInfo GetUserState()
        {
             ChangTing.Users.Models.UsersInfo objMod = (UsersInfo)HttpContext.Current.Session["UsersInfo"];
            return objMod;
        }
    }
}

