using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using ChangTing.Admin.Models;

namespace ChangTing.Admin.WebProxy
{
    public class AdminState
    {
        /// <summary>
        /// 存储实体类（主要用于将用户信息存储到服务器上）
        /// </summary>
        /// <param name="objMod"></param>
        /// <returns></returns>
        public static void SetUserState(AdminInfo objMod)
        {
            HttpContext.Current.Session["AdminInfo"] = objMod;
        }
        /// <summary>
        /// 获取存储的实体类
        /// </summary>
        /// <returns></returns>
        public static AdminInfo GetUserState()
        {
             ChangTing.Admin.Models.AdminInfo objMod = (AdminInfo)HttpContext.Current.Session["AdminInfo"];
            return objMod;
        }
    }
}

