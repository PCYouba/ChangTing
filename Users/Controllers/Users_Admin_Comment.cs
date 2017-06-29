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
    public partial class UsersController : Controller
    {
        #region 评论评论信息列表相关

        #region 评论评论信息列表显示评论的评论信息
        /// <summary>
        /// Comment(评论评论信息表)评论Id查看
        /// </summary>
        /// <param name="userid">评论Id</param>
        /// <returns></returns>
        [AdminLoginCheck]
        public ActionResult CommentList_Admin(int userid)
        {
            IList<CommentInfo> Commentinfolist = new List<CommentInfo>();
            CommentServiceLogic_Admin bll = new CommentServiceLogic_Admin();
            if (userid>0)
            {
                Commentinfolist = bll.SelectCommentidCommentWay(userid);//当前ID的评论
            }
            else
            {
                Commentinfolist = bll.SelectAllCommentWay();//获取所有评论
            }
            return View(Commentinfolist);
        }
        #endregion

        #region 评论评论信息列表删除某条数据

        #region 单条评论删除
        /// <summary>
        /// 单个评论删除josn接收
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteComment(string id)
        {
            CommentServiceLogic_Admin bll = new CommentServiceLogic_Admin();
            object[] cg = bll.DeleteCommentWay(id);
            return Json(new {data1 = cg[0],data2 = cg[1]});
        }
        #endregion

        #region 多条评论删除
        /// <summary>
        /// 批量评论删除josn接收
        /// </summary>
        /// <param name="fxkint"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteCommentMulti(string[] fxkint)
        {
            CommentServiceLogic_Admin bll = new CommentServiceLogic_Admin();
            int cg = bll.DeleteCommentMultiWay(fxkint);
            return Json(cg);
        }
        #endregion

        #endregion

        #region 评论停用
        /// <summary>
        /// 单个停用josn接收
        /// </summary>
        /// <param name="id">停用ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateDisplayOut(int id)
        {
            UsersServiceLogic_Admin bll = new UsersServiceLogic_Admin();
            int fhz = bll.UpdateAvailableOutUsersWay(id);
            return Json(fhz);
        }
        #endregion

        #region 评论启用
        /// <summary>
        /// 单个启用josn接收
        /// </summary>
        /// <param name="id">启用ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateDisplayUp(int id)
        {
            UsersServiceLogic_Admin bll = new UsersServiceLogic_Admin();
            int fhz = bll.UpdateAvailableUpUsersWay(id);
            return Json(fhz);
        }
        #endregion

        #endregion
    }
}
