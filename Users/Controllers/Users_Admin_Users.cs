using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChangTing.Users.Services;
using ChangTing.Core.Models;
using ChangTing.Admin.Models;
using ChangTing.Users.Models;

namespace ChangTing.Users.Controllers
{

    [ValidateInput(false)]
    public partial class UsersController : Controller
    {
        #region 用户信息列表相关

        #region 用户信息列表直接进入显示所有信息
        /// <summary>
        /// Users(用户信息表)直接查看
        /// </summary>
        /// <returns></returns>
        [AdminLoginCheck]
        public ActionResult UsersList_Admin()
        {
            IList<UsersInfo> Usersinfolist = new List<UsersInfo>();
            UsersServiceLogic_Admin bll = new UsersServiceLogic_Admin();
            Usersinfolist = bll.SelectAllUsersWay();
            return View(Usersinfolist);
        }
        #endregion

        #region 用户信息列表删除某条用户数据

        #region 单条用户删除
        /// <summary>
        /// 单个用户删除josn接收
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteUsers(string id)
        {
            UsersServiceLogic_Admin bll = new UsersServiceLogic_Admin();
            object [] cg = bll.DeleteUserWay(id);
            return Json(new { data1=cg[0],data2= cg[1], data3= cg[2]});
        }
        #endregion

        #region 多条用户删除
        /// <summary>
        /// 批量用户删除josn接收
        /// </summary>
        /// <param name="fxkint"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteUsersMulti(string[] fxkint)
        {
            UsersServiceLogic_Admin bll = new UsersServiceLogic_Admin();
            int cg = bll.DeleteUsersMultiWay(fxkint);
            return Json(cg);
        }
        #endregion

        #endregion

        #region 用户停用
        /// <summary>
        /// 单个停用josn接收
        /// </summary>
        /// <param name="id">停用ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateAvailableOutUsers(int id)
        {
            UsersServiceLogic_Admin bll = new UsersServiceLogic_Admin();
            int fhz = bll.UpdateAvailableOutUsersWay(id);
            return Json(fhz);
        }
        #endregion

        #region 用户启用
        /// <summary>
        /// 单个启用josn接收
        /// </summary>
        /// <param name="id">启用ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateAvailableUpUsers(int id)
        {
            UsersServiceLogic_Admin bll = new UsersServiceLogic_Admin();
            int fhz = bll.UpdateAvailableUpUsersWay(id);
            return Json(fhz);
        }
        #endregion

        #endregion
    }
}
