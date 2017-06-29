using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChangTing.Users.Services;
using ChangTing.Core.Models;
using ChangTing.Admin.Models;
using ChangTing.Users.Models;
using ChangTing.Users.ViewModels;

namespace ChangTing.Users.Controllers
{
     public partial class UsersController : Controller
    {
        #region 用户收藏信息列表相关

        #region 用户收藏信息列表显示用户的收藏信息
        /// <summary>
        /// Collection(用户收藏信息表)用户Id查看
        /// </summary>
        /// <param name="userid">用户Id</param>
        /// <returns></returns>
        [AdminLoginCheck]
        public ActionResult CollectionList_Admin(int userid)
        {
            IList<CollectionInfo> Collectioninfolist = new List<CollectionInfo>();
            CollectionServiceLogic_Admin bll = new CollectionServiceLogic_Admin();
            Collectioninfolist = bll.SelectAllCollectionWay();//获取所有收藏

            IList<CollectionViewInfo> CollectionViewinfolist = new List<CollectionViewInfo>();
            CollectionViewinfolist = bll.CollectionViewInfoWay(Collectioninfolist);//获取界面所需的modile
            return View(CollectionViewinfolist);
        }
        #endregion

        #region 用户收藏信息列表删除某条数据

        #region 单条收藏删除
        /// <summary>
        /// 单个收藏删除josn接收
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteCollection(string id)
        {
            CollectionServiceLogic_Admin bll = new CollectionServiceLogic_Admin();
            bool cg = bll.DeleteUserWay(id);
            return Json(cg);
        }
        #endregion

        #region 多条收藏删除
        /// <summary>
        /// 批量收藏删除josn接收
        /// </summary>
        /// <param name="fxkint"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteCollectionMulti(string[] fxkint)
        {
            CollectionServiceLogic_Admin bll = new CollectionServiceLogic_Admin();
            int cg = bll.DeleteCollectionMultiWay(fxkint);
            return Json(cg);
        }
        #endregion

        #endregion
        
        #endregion
    }
}
