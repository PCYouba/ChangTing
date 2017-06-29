using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChangTing.Admin.Models;
using ChangTing.Core.Models;
using ChangTing.Admin.WebProxy;
using System.Text.RegularExpressions;
using ChangTing.Admin.Services;

namespace ChangTing.Admin.Controllers
{
    [ValidateInput(false)]
    public partial class AdminController : Controller
    {

        #region login
        /// <summary>
        /// 登录直接访问
        /// </summary>
        /// <returns></returns>
        public ActionResult login()
        {
            return View(new AdminInfo());
        }

        /// <summary>
        /// 登录时通过login界面传入账号密码验证码检测登录
        /// </summary>
        /// <param name="admininfo"></param>
        /// <param name="IMGTXT">验证码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult login(AdminInfo admininfo, string IMGTXT)
        {
            if (!ModelState.IsValid)
            {
                return View(admininfo);
            }

            string rndcodetxt = Session["rndcode"].ToString();

            if (IMGTXT == "")
            {
                ViewBag.Miss = "请输入验证码!";
                return View(admininfo);
            }
            else if (!rndcodetxt.Equals(IMGTXT.ToLower()))
            {
                ViewBag.Miss = "验证码错误!请确认后输入";
                return View(admininfo);
            }
            Services.login service = new Services.login();
            string ifdb = service.IfDblogin(admininfo);

            if (ifdb.Equals("账号或密码错误!"))
            {
                ViewBag.Miss = ifdb;
                return View(admininfo);
            }
            else
            {
                return Redirect("index");
            }
        }
        #endregion


        #region index

        /// <summary>
        /// 界面直接访问防止未登录的访问
        /// </summary>
        /// <returns></returns>
        [AdminLoginCheck]
        public ActionResult index()
        {
            AdminInfo admininfo = AdminState.GetUserState();
            ViewBag.ss = admininfo.LoginName;
            return View(admininfo);
        }
       
        #endregion


        #region welcome

        /// <summary>
        /// 欢迎界面
        /// </summary>
        /// <returns></returns>
        public ActionResult welcome()
        {
            return View();
        }
        #endregion


        #region 安全退出
        public ActionResult ifdb()
        {
            AdminState.SetUserState(null);
            return Redirect("Login");
        }

        #endregion

       
        #region password
        /// <summary>
        /// 密码界面直接进入
        /// </summary>
        /// <returns></returns>
        [AdminLoginCheck]
        public ActionResult password()
        {
            ViewBag.NewPassword = "";
            AdminInfo admininfo =  AdminState.GetUserState();
            ViewBag.LoginName = admininfo.LoginName;
            return View();
        }
        /// <summary>
        /// 密码界面提交
        /// </summary>
        /// <param name="frmc"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult password(FormCollection frmc)
        {
            AdminInfo admininfo = AdminState.GetUserState();
            int AdminId = admininfo.AdminId;//id
            ViewBag.LoginName = admininfo.LoginName;//xm
            string newpassword = frmc["newpassword"];//mm1
            string doublepassword = frmc["doublepassword"];//mm2
            bool QR = true;

            
            
            if (!(newpassword.Length <= 16 && newpassword.Length >= 3))
            {
                ViewBag.NewPassword = "密码长度应该在3-16位内!";
                return View();
            }
            else if(Regex.IsMatch("^[0-9a-zA-Z]*$", newpassword))
            {
                ViewBag.NewPassword = "密码只能输入英文和数字!";
                QR = false;
            }

            if (doublepassword.Length == 0)
            {
                ViewBag.DoublePassword = "请再次输入密码!";
                QR = false;
            }
            else if (doublepassword != newpassword)
            {
                ViewBag.DoublePassword  = "两次输入的密码不相同!";
                QR = false;
            }
            if (newpassword.Equals(admininfo.Password))
            {
                ViewBag.CaoZuoTiShi = "密码未改变!";
            }
            else if (QR)
            {
                updatePasswordl bll = new updatePasswordl();
                bool xgdal = bll.updatePasswordWayl(AdminId,newpassword);
                if (xgdal)
                {
                    ViewBag.CaoZuoTiShi = "密码修改成功!";
                    admininfo.Password = newpassword;
                    AdminState.SetUserState(admininfo);
                }
                else
                {
                    ViewBag.CaoZuoTiShi = "密码修改失败!";
                }
            }
            else
            {
                ViewBag.CaoZuoTiShi = "密码修改失败!";
            }
            return View();
        }
        #endregion


        #region 验证码
        public ActionResult YanZhengMa()
        {
            return File(CheckCode.ProcessRequest(), "image/gif");
        }
        #endregion

    }
}
