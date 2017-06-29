using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChangTing.Singer.Services;
using ChangTing.Core.Models;
using ChangTing.Admin.Models;
using ChangTing.Singer.Models;
using ChangTing.Singer.Repositories;

namespace ChangTing.Singer.Controllers
{
    [ValidateInput(false)]
    public partial class SingerController : Controller
    {

        #region 歌手信息列表相关

        #region 歌手信息列表直接进入显示所有信息
        /// <summary>
        /// Singer(歌手信息表)直接查看
        /// </summary>
        /// <returns></returns>
        public ActionResult SingerList_Admin()
        {
            SingerServiceLogic bll = new SingerServiceLogic();
            return View(bll.SelectAllSingerWay());
        }
        #endregion

        #region 歌手信息列表删除某条歌手数据

        #region 单条歌手删除
        /// <summary>
        /// 单个歌手删除josn接收
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteSinger(string id)
        {
            SingerServiceLogic bll = new SingerServiceLogic();
            int fhz = bll.DeleteSingerIdAlbumWay(int.Parse(id));//删除专辑并返回删除条数

            bool cg = bll.DeleteSingerWay(id);//删除当前歌手

            return Json(new { data1 = cg, data2 = fhz });
        }
        #endregion

        #region 多条歌手删除
        /// <summary>
        /// 批量删除歌手josn接收
        /// </summary>
        /// <param name="fxkint"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteSingerMulti(string[] fxkint)
        {
            SingerServiceLogic bll = new SingerServiceLogic();
            int cg = bll.DeleteSingerMultiWay(fxkint);
            return Json(cg);
        }
        #endregion

        #endregion

        #endregion


        #region 歌手信息添加相关

        #region 新建或修改进入界面时
        /// <summary>
        /// SingerListAdd(歌手信息添加（修改）)直接查看
        /// </summary>
        /// <returns></returns>
        [AdminLoginCheck]
        public ActionResult SingerListAdd_Admin(int singerid)
        {
            SingerInfo singerinfo = new SingerInfo();
            if (singerid > 0)
            {
                SingerServiceLogic bll = new SingerServiceLogic();
                singerinfo = bll.SelectSingerWay(singerid);
                ViewBag.editortext = singerinfo.Introduce;
                if (singerinfo.HeadPortrait == string.Empty || singerinfo.HeadPortrait == null)
                {
                    singerinfo.HeadPortrait = ModelInfo.imgmoren;
                }
                else
                {
                    ViewBag.imgfile = singerinfo.HeadPortrait;//不为空才记录头像
                }
            }
            else
            {
                singerinfo.HeadPortrait = ModelInfo.imgmoren;
            }
            return View(singerinfo);//新增返回空  修改返回内容
        }
        #endregion

        #region 新建或修改时提交操作
        /// <summary>
        /// 保存修改（添加内容） 提交
        /// </summary>
        /// <param name="singerid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SingerListAdd_Admin(SingerInfo singerinfo, FormCollection fc, HttpPostedFileBase file_upload)
        {

            ViewBag.editortext = fc["editor"];
            if (!ModelState.IsValid)
            {
                ViewBag.imgfile = singerinfo.HeadPortrait;
                return View(singerinfo);
            }

            //接收自己单独要上传的图片   
            string imgtext = string.Empty;
            if (file_upload != null)//用户选择了图片
            {
                Img im = new Img();
                imgtext = string.Empty;
                imgtext = im.img(file_upload);
                if (string.Empty.Equals(imgtext))
                {
                    ViewBag.cg = "img格式错误";
                    singerinfo.Introduce = null;
                    singerinfo.HeadPortrait = ModelInfo.imgmoren;
                    return View(singerinfo);
                }
                singerinfo.HeadPortrait = imgtext;//照片路径存储
            }
            else
            {
                if (fc["df"] == null || fc["df"] == string.Empty)
                {
                    singerinfo.HeadPortrait = ModelInfo.imgmoren;
                }
                else
                {
                    singerinfo.HeadPortrait = fc["df"];
                }
            }

            switch (fc["radio-xingbie"])//单选框改为可操作形式
            {
                case "保密":
                    singerinfo.Gender = 0; break;
                case "男":
                    singerinfo.Gender = 1; break;
                case "女":
                    singerinfo.Gender = 2; break;
            }
            
            singerinfo.Introduce = fc["editor"];//提取歌手自我介绍


            string fanhuizhi = null;//操作是否成功

            SingerServiceLogic bll = new SingerServiceLogic();
            if (singerinfo.SingerId > 0)//Id大于0执行修改操作
            {
                fanhuizhi = bll.UpdateSingerWay(singerinfo);
            }
            else//小于0 执行新建操作
            {
                fanhuizhi = bll.InsertSingerWay(singerinfo);
            }

            ViewBag.cg = fanhuizhi;
            ViewBag.imgfile = singerinfo.HeadPortrait;
            return View(singerinfo);
        }
        #endregion

        #endregion


        #region 歌手获取全部专辑
        /// <summary>
        /// 歌手获取全部专辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SelectSingerIdAlbum(int id)
        {
            AlbumAndSinger_Admin bll = new AlbumAndSinger_Admin();
            IList<AlbumInfo> albuminfolist = bll.SelectSingerIdAlbumWay(id);
            return Json(albuminfolist);
        }
        #endregion

    }
}