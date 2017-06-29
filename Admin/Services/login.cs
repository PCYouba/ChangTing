using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.Admin.Models;
using ChangTing.Admin.Repositories;
using ChangTing.Admin.WebProxy;

namespace ChangTing.Admin.Services
{
    public class login
    {

        /// <summary>
        /// 账号密码判断登录
        /// </summary>
        /// <param name="admininfo"></param>
        /// <returns>账号密码检测成功后返回“成功”，失败后返回“账号或密码错误!”</returns>
        public string IfDblogin(AdminInfo admininfo)
        {
            AdminInfo AdminInfojs = new AdminInfo();
            nameAndpassIfdb Repos = new nameAndpassIfdb();
            AdminInfojs = Repos.nameAndpassIfdbWay(admininfo.LoginName, admininfo.Password);

            if (AdminInfojs == null)
            {
                return "账号或密码错误!";
            }
            else
            {
                AdminState.SetUserState(AdminInfojs);//成功则存储到session
                return "成功";
            }

        }

    }
}
