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

        #region 反馈信息列表相关

        #region 反馈信息列表直接进入显示所有信息
        /// <summary>
        /// FeedBack(反馈信息表)直接查看
        /// </summary>
        /// <returns></returns>
        public ActionResult FeedBackList_Admin()
        {
            FeedBackServiceLogic bll = new FeedBackServiceLogic();
            return View(bll.SelectAllFeedBackWay());
        }
        #endregion

        #region 管理员回复反馈信息信息
        /// <summary>
        /// FeedBack管理员回复反馈信息信息
        /// </summary>
        /// <returns></returns>
        public ActionResult FeedBackListReply_Admin()
        {

            return View();
        }
        #endregion


        #region 反馈信息列表删除某条反馈数据

        #region 单条反馈删除
        /// <summary>
        /// 单个反馈删除josn接收
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteFeedBack(string id)
        {
            FeedBackServiceLogic bll = new FeedBackServiceLogic();

            bool cg = bll.DeleteFeedBackWay(id);//删除当前反馈

            return Json(cg);
        }
        #endregion

        #region 多条反馈删除
        /// <summary>
        /// 批量删除反馈josn接收
        /// </summary>
        /// <param name="fxkint"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteFeedBackMulti(string[] fxkint)
        {
            FeedBackServiceLogic bll = new FeedBackServiceLogic();
            int cg = bll.DeleteFeedBackMultiWay(fxkint);
            return Json(cg);
        }
        #endregion

        #endregion
        

        #endregion


        #region 反馈信息添加相关

        #region 新建或修改进入界面时
        /// <summary>
        /// FeedBackListAdd(反馈信息添加（修改）)直接查看
        /// </summary>
        /// <returns></returns>
        [AdminLoginCheck]
        public ActionResult FeedBackListAdd_Admin(int FeedBackid)
        {
            FeedBackInfo FeedBackinfo = new FeedBackInfo();
            if (FeedBackid > 0)
            {
                FeedBackServiceLogic bll = new FeedBackServiceLogic();
                FeedBackinfo = bll.SelectFeedBackWay(FeedBackid);
               
            }
            return View(FeedBackinfo);//新增返回空  修改返回内容
        }
        #endregion

        #region 新建或修改时提交操作
        /// <summary>
        /// 保存修改（添加内容） 提交
        /// </summary>
        /// <param name="FeedBackid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FeedBackListAdd_Admin(FeedBackInfo FeedBackinfo, FormCollection fc)
        {

            ViewBag.editortext = fc["editor"];
            if (!ModelState.IsValid)
            {
               
                return View(FeedBackinfo);
            }

            //接收自己单独要上传的图片   
            string imgtext = string.Empty;
          
           

            FeedBackinfo.S_Content = fc["editor"];//提取反馈回复


            string fanhuizhi = null;//操作是否成功

            FeedBackServiceLogic bll = new FeedBackServiceLogic();
            if (FeedBackinfo.FeedBackId > 0)//Id大于0执行修改操作
            {
                fanhuizhi = bll.UpdateFeedBackWay(FeedBackinfo);
            }
            else//小于0 执行新建操作
            {
                fanhuizhi = bll.InsertFeedBackWay(FeedBackinfo);
            }

            ViewBag.cg = fanhuizhi;
            return View(FeedBackinfo);
        }
        #endregion



        #endregion



    }
}
