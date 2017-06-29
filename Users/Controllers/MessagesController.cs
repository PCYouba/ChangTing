using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChangTing.Users.Services;
using ChangTing.Core.Models;
using ChangTing.Users.Models;
using ChangTing.Admin.Models;

namespace ChangTing.Users.Controllers
{
    public partial class UsersController : Controller
    {
        MessagesServiceLogic_Admin bll;
        #region 信息列表相关

        #region 信息列表直接进入显示所有信息
        /// <summary>
        /// Messages(信息表)直接查看
        /// </summary>
        /// <returns></returns>
        [AdminLoginCheck]
        public ActionResult MessagesList_Admin(int GradeLevel)
        {
            IList<MessagesInfo> MessagesInfolist = new List<MessagesInfo>();
            if (GradeLevel == -1)
            {
                bll = new MessagesServiceLogic_Admin();
                MessagesInfolist = bll.SelectAllMessagesWay();
                ViewBag.title3 = "全部消息";
            }
            else
            {
                bll = new MessagesServiceLogic_Admin();
                MessagesInfolist = bll.SelectGradeLevelMessagesWay(GradeLevel);
                switch (GradeLevel)
                {
                    case 0:
                        ViewBag.title3 = "公告列表";
                        break;
                    case 1:
                        ViewBag.title3 = "反馈列表";
                        break;
                    case 2:
                        ViewBag.title3 = "用户发信";
                        break;
                    default:
                        break;
                }
                
            }

            return View(MessagesInfolist);
        }
        #endregion

        #region 信息列表删除某条数据

        #region 单条删除
        /// <summary>
        /// 单个删除josn接收
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteMessages(string id)
        {
            bll = new MessagesServiceLogic_Admin();
            return Json(bll.DeleteMessagesWay(id));
        }
        #endregion  

        #region 多条删除
        /// <summary>
        /// 批量删除josn接收
        /// </summary>
        /// <param name="fxkint"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteMessagesMulti(string[] fxkint)
        {
            bll = new MessagesServiceLogic_Admin();
            int cg = bll.DeleteMessagesMultiWay(fxkint);
            return Json(cg);
        }
        #endregion

        #endregion

        #endregion

        #region 信息增改界面

        #region 新建或修改进入界面时
        /// <summary>
        /// MessagesListAdd(信息添加（修改）)直接查看
        /// </summary>
        /// <returns></returns>
        [AdminLoginCheck]
        public ActionResult MessagesListReply_Admin(int Messagesid)
        {
            UsersServiceLogic_Admin userbll = new UsersServiceLogic_Admin();
            IList<UsersInfo> UsersList = userbll.SelectAllUsersWay();
           

            MessagesInfo MessagesInfo = new MessagesInfo();

            bll = new MessagesServiceLogic_Admin();

            if (Messagesid>0)
            {
                MessagesInfo = bll.SelectMessagesWay(Messagesid);
                string[] names = bll.SelectMessagesAllNameWay(MessagesInfo);
                ViewBag.Found = new string[] { MessagesInfo.FoundId.ToString(), names[0] };//存储发件人信息
                ViewBag.User = new string[] { MessagesInfo.UserId.ToString(), names[1] };//存储收件人信息
            }
            else
            {
                ViewBag.Found = new string[] { MessagesInfo.FoundId.ToString(), ChangTing.Admin.WebProxy.AdminState.GetUserState().LoginName };//存储发件人信息
                UsersInfo alluser = new UsersInfo() { UserId=-1,NickName="全部用户"};
                UsersList.Insert(0, alluser);
            }
            
            ViewBag.UserName = UsersList;

            ViewBag.editortext = MessagesInfo.M_Content;
            
            return View(MessagesInfo);//新增返回空  修改返回内容
        }
        #endregion


        //#region 新建或修改时提交操作
        ///// <summary>
        ///// 保存修改（添加内容） 提交
        ///// </summary>
        ///// <param name="singerid"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult MessagesListAdd_Admin(MessagesInfo MessagesInfo, FormCollection fc, HttpPostedFileBase file_upload)
        //{
        //    ViewBag.editortext = fc["editor"];//文本框内容获取
        //    MessagesInfo.SingerId = Convert.ToInt32(fc["singerid"]);//id获取

        //    SingerInfo singerinfo = new SingerInfo();
        //    singerinfo.Name = fc["singername"];
        //    singerinfo.SingerId = MessagesInfo.SingerId;
        //    ViewBag.singer = singerinfo;//界面记录先做好

        //    MessagesAndSinger_Admin bll2 = new MessagesAndSinger_Admin();
        //    ViewBag.SingerName = bll2.SelectAllSingerNameWay();

        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.Image = MessagesInfo.Image;
        //        return View(MessagesInfo);
        //    }

        //    //接收自己单独要上传的图片   
        //    string imgtext = string.Empty;
        //    if (file_upload != null)//用户选择了图片
        //    {
        //        Img im = new Img();
        //        imgtext = string.Empty;
        //        imgtext = im.img(file_upload);
        //        if (string.Empty.Equals(imgtext))
        //        {
        //            ViewBag.cg = "img格式错误";
        //            MessagesInfo.Introduce = null;
        //            singerinfo.HeadPortrait = ModelInfo.imgmoren;
        //            return View(MessagesInfo);
        //        }
        //        MessagesInfo.Image = imgtext;//照片路径存储
        //    }
        //    else
        //    {
        //        if (fc["df"]==null || fc["df"]==string.Empty)
        //        {
        //            MessagesInfo.Image = ModelInfo.imgmoren;
        //        }
        //        else
        //        {
        //            MessagesInfo.Image = fc["df"];
        //        }
        //    }

        //    MessagesInfo.Introduce = fc["editor"];//提取歌手自我介绍

        //    string fanhuizhi = null;//操作是否成功

        //    bll = new MessagesServiceLogic_Admin();
        //    if (MessagesInfo.MessagesId > 0)//Id大于0执行修改操作
        //    {
        //        fanhuizhi = bll.UpdateMessagesWay(MessagesInfo);
        //    }
        //    else//小于0 执行新建操作
        //    {
        //        fanhuizhi = bll.InsertMessagesWay(MessagesInfo);
        //    }

        //    ViewBag.cg = fanhuizhi;
        //    ViewBag.Image = MessagesInfo.Image;
        //    return View(MessagesInfo);
        //}
        //#endregion

        #endregion
    }
}
