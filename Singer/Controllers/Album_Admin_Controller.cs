using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChangTing.Singer.Services;
using ChangTing.Core.Models;
using ChangTing.Admin.Models;
using ChangTing.Singer.Models;

namespace ChangTing.Singer.Controllers
{
    public partial class SingerController : Controller
    {

        #region 专辑信息列表相关

        #region 专辑信息列表直接进入显示所有信息
        /// <summary>
        /// Album(专辑信息表)直接查看
        /// </summary>
        /// <returns></returns>
        [AdminLoginCheck]
        public ActionResult AlbumList_Admin(int singerid)
        {
            IList<AlbumInfo> albuminfolist = new List<AlbumInfo>();
            if (singerid==0)
            {
                AlbumServiceLogic_Admin bll = new AlbumServiceLogic_Admin();
                albuminfolist = bll.SelectAllAlbumWay();
            }
            else
            {
                AlbumAndSinger_Admin bll = new AlbumAndSinger_Admin();
                albuminfolist = bll.SelectSingerIdAlbumWay(singerid);
            }
            SingerServiceLogic b = new SingerServiceLogic();

            for (int i=0; i< albuminfolist.Count;i++)
            {
                string name = "name" + i;
                ViewData[name] = b.SingerIdAndName(albuminfolist[i].SingerId);
            }

            return View(albuminfolist);
        }
        #endregion

        #region 专辑信息列表删除某条专辑数据

        #region 单条删除
        /// <summary>
        /// 单个删除josn接收
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteAlbum(string id)
        {
            AlbumServiceLogic_Admin bll = new AlbumServiceLogic_Admin();
            object[] xx = bll.DeleteAlbumWay(id);
            bool cg =(bool)xx[0];
            int yinyue = (int)xx[1];
            return Json(new {date1=cg,date2= yinyue });
        }
        #endregion  

        #region 多条删除
        /// <summary>
        /// 批量删除josn接收
        /// </summary>
        /// <param name="fxkint"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteAlbumMulti(string[] fxkint)
        {
            AlbumServiceLogic_Admin bll = new AlbumServiceLogic_Admin();
            int cg = bll.DeleteAlbumMultiWay(fxkint);
            return Json(cg);
        }
        #endregion

        #endregion

        #endregion

        #region 专辑信息增改界面

        #region 新建或修改进入界面时
        /// <summary>
        /// AlbumListAdd(专辑信息添加（修改）)直接查看
        /// </summary>
        /// <returns></returns>
        [AdminLoginCheck]
        public ActionResult AlbumListAdd_Admin(int albumid)
        {
            
            AlbumInfo AlbumInfo = new AlbumInfo();
            if (albumid > 0)
            {
                AlbumServiceLogic_Admin bll = new AlbumServiceLogic_Admin();
                AlbumInfo = bll.SelectAlbumWay(albumid);
                ViewBag.editortext = AlbumInfo.Introduce;
                if (AlbumInfo.Image == string.Empty || AlbumInfo.Image == null)
                {
                    AlbumInfo.Image = ModelInfo.imgmoren;
                }
                else
                {
                    ViewBag.Image = AlbumInfo.Image;//不为空才记录头像
                }
            }
            else
            {
                AlbumInfo.Image = ModelInfo.imgmoren;
            }
            
            if (AlbumInfo.SingerId!=0)
            {
                SingerServiceLogic b = new SingerServiceLogic();//歌手表用于获取姓名的

                SingerInfo singerinfo = new SingerInfo();
                singerinfo.Name = b.SingerIdAndName(AlbumInfo.SingerId);
                singerinfo.SingerId = AlbumInfo.SingerId;
                ViewBag.singer = singerinfo;
            }
            
            AlbumAndSinger_Admin bll2 = new AlbumAndSinger_Admin();
            ViewBag.SingerName = bll2.SelectAllSingerNameWay();
            //ViewBag.cg = "ri";
            return View(AlbumInfo);//新增返回空  修改返回内容
        }
        #endregion


        #region 新建或修改时提交操作
        /// <summary>
        /// 保存修改（添加内容） 提交
        /// </summary>
        /// <param name="singerid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AlbumListAdd_Admin(AlbumInfo albuminfo, FormCollection fc, HttpPostedFileBase file_upload)
        {
            ViewBag.editortext = fc["editor"];//文本框内容获取
            albuminfo.SingerId = Convert.ToInt32(fc["singerid"]);//歌手id获取

            SingerInfo singerinfo = new SingerInfo();
            singerinfo.Name = fc["singername"];
            singerinfo.SingerId = albuminfo.SingerId;
            ViewBag.singer = singerinfo;//界面记录先做好

            AlbumAndSinger_Admin bll2 = new AlbumAndSinger_Admin();
            ViewBag.SingerName = bll2.SelectAllSingerNameWay();

            if (!ModelState.IsValid)
            {
                ViewBag.Image = albuminfo.Image;
                return View(albuminfo);
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
                    albuminfo.Introduce = null;
                    singerinfo.HeadPortrait = ModelInfo.imgmoren;
                    return View(albuminfo);
                }
                albuminfo.Image = imgtext;//照片路径存储
            }
            else
            {
                if (fc["df"]==null || fc["df"]==string.Empty)
                {
                    albuminfo.Image = ModelInfo.imgmoren;
                }
                else
                {
                    albuminfo.Image = fc["df"];
                }
            }

            albuminfo.Introduce = fc["editor"];//提取歌手自我介绍
            
            string fanhuizhi = null;//操作是否成功

            AlbumServiceLogic_Admin bll = new AlbumServiceLogic_Admin();
            if (albuminfo.AlbumId > 0)//Id大于0执行修改操作
            {
                fanhuizhi = bll.UpdateAlbumWay(albuminfo);
            }
            else//小于0 执行新建操作
            {
                fanhuizhi = bll.InsertAlbumWay(albuminfo);
            }

            ViewBag.cg = fanhuizhi;
            ViewBag.Image = albuminfo.Image;
            return View(albuminfo);
        }
        #endregion

        #endregion
    }
}
