using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChangTing.Music.Models;
using ChangTing.Core.Models;
using ChangTing.Admin.Models;
using ChangTing.Music.Services;
using ChangTing.Singer.Services;
using ChangTing.Singer.Models;

namespace ChangTing.Music.Controllers
{
    public partial class MusicController : Controller
    {
        #region 音乐信息列表相关

        #region 音乐信息列表直接进入显示所有信息
        /// <summary>
        /// Storage(音乐信息表)直接查看
        /// </summary>
        /// <returns></returns>
        [AdminLoginCheck]
        public ActionResult StorageList_Admin(int albumid)
        {
            StorageServiceLogic_Admin bll = new StorageServiceLogic_Admin();
            IList<StorageInfo> StorageInfolist = new List<StorageInfo>();
            if (albumid>0)
            {
                StorageInfolist = bll.SelectAlbumidAllStorageWay(albumid);
            }
            else
            {
                StorageInfolist = bll.SelectAllStorageWay();
            }
           

            SingerServiceLogic singerbll = new SingerServiceLogic();
            CategoryServiceLogic_Admin categorybll = new CategoryServiceLogic_Admin();
            AlbumServiceLogic_Admin albumbll = new AlbumServiceLogic_Admin();
            
            for (int i = 0; i < StorageInfolist.Count; i++)
            {
                string singername = "singername" + i;
                ViewData[singername] = singerbll.SingerIdAndName(StorageInfolist[i].SingerId);
                string albumname = "albumname" + i;
                ViewData[albumname] = albumbll.SelectAlbumNameWay(StorageInfolist[i].AlbumId);
                string categoryname = "categoryname" + i;
                ViewData[categoryname] = categorybll.CategoryIdAndName(StorageInfolist[i].CategoryId);
            }

            return View(StorageInfolist);
        }
        #endregion

        #region 歌手信息列表删除某条歌手数据

        #region 单条删除
        /// <summary>
        /// 单个删除josn接收
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteStorage(string id)
        {
            StorageServiceLogic_Admin bll = new StorageServiceLogic_Admin();
            object[] cg = bll.DeleteStorageWay(id);
            return Json(new { data1 = cg[0], data2 = cg[1] });
        }

        #endregion

        #region 多条删除
        /// <summary>
        /// 批量删除josn接收
        /// </summary>
        /// <param name="fxkint"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteStorageMulti(string[] fxkint)
        {
            StorageServiceLogic_Admin bll = new StorageServiceLogic_Admin();
            int cg = bll.DeleteStorageMultiWay(fxkint);
            return Json(cg);
        }
        #endregion

        #endregion

        #endregion

        #region 音乐信息增改界面

        #region 新建或修改进入界面时
        /// <summary>
        /// StorageListAdd(音乐信息添加（修改）)直接查看
        /// </summary>
        /// <returns></returns>
        [AdminLoginCheck]
        public ActionResult StorageListAdd_Admin(int storageid)
        {
            

            StorageServiceLogic_Admin bll = new StorageServiceLogic_Admin();
            StorageInfo StorageInfo = bll.SelectStorageWay(storageid);
            if (StorageInfo == null)
            {
                StorageInfo = new StorageInfo();
            }
            ViewBag.file = StorageInfo.Path;//存储当前  音乐的file
            StorageListAdd_ID(StorageInfo);


            return View(StorageInfo);//新增返回空  修改返回内容
        }
        #endregion

        #region 新建或修改时提交操作
        /// <summary>
        /// 保存修改（添加内容） 提交
        /// </summary>
        /// <param name="singerid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult StorageListAdd_Admin(StorageInfo StorageInfo, FormCollection fc, HttpPostedFileBase file_music)
        {
            StorageInfo.StorageId = Convert.ToInt32(fc["storageid"]);            
            StorageInfo.SingerId = Convert.ToInt32(fc["singerid"]);
            StorageInfo.CategoryId = Convert.ToInt32(fc["categoryid"]);
            StorageInfo.AlbumId = Convert.ToInt32(fc["albumid"]);
            StorageInfo.Display = Convert.ToByte(fc["radio-zhuangtai"]);
            StorageListAdd_ID(StorageInfo);//界面显示内容保存
            string x = fc["file"];
            if (file_music == null && x ==null)
            {
                ViewBag.cg = "请上传歌曲文件";
                return View(StorageInfo);
            }

            ViewBag.file = fc["preview"];
            if (!ModelState.IsValid)
            {
                return View(StorageInfo);
            }


            filemusic musc = new filemusic();
            string imgtext = string.Empty;
            if (file_music!=null)
            {
                imgtext = musc.music(file_music, ViewBag.album.Name, ViewBag.singer.Name);
            }
            else if(file_music==null && x!=null)
            {
                imgtext = x;
                ViewBag.file = x;
            }
            else
            {
                imgtext = null;
            }

            if (imgtext == null)
            {
                ViewBag.cg = "请上传音乐文件!";
                return View(StorageInfo);
            }
            else if (imgtext == string.Empty)
            {
                ViewBag.cg = "请上传格式为mp3的音乐文件!";
                return View(StorageInfo);
            }
            //StorageInfo.Path = imgtext;//音乐路径存储
            StorageInfo.Path = imgtext;

            StorageServiceLogic_Admin bll = new StorageServiceLogic_Admin();
            if (StorageInfo.StorageId > 0)
            {
                ViewBag.cg = bll.UpdateStorageWay(StorageInfo);
            }
            else
            {
                ViewBag.cg = bll.InsertStorageWay(StorageInfo);
                StorageInfo.StorageId = 0;
            }
            return View(StorageInfo);
        }
        #endregion
        #region 派生
        /// <summary>
        /// 音乐 外键 的ID获取工作
        /// </summary>
        /// <param name="StorageInfo"></param>
        public void StorageListAdd_ID(StorageInfo StorageInfo)
        {
            if (StorageInfo.SingerId != 0)//歌手ID不为空（修改）获取歌手名
            {
                SingerServiceLogic singerbll = new SingerServiceLogic();//歌手表用于获取姓名的

                SingerInfo singerinfo = new SingerInfo();
                singerinfo.Name = singerbll.SingerIdAndName(StorageInfo.SingerId);
                singerinfo.SingerId = StorageInfo.SingerId;
                ViewBag.singer = singerinfo;//当前歌手名
            }
            AlbumAndSinger_Admin albumandsingerbll = new AlbumAndSinger_Admin();//专辑业务
            ViewBag.SingerName = albumandsingerbll.SelectAllSingerNameWay();//所有歌手


            CategoryServiceLogic_Admin categorybll = new CategoryServiceLogic_Admin();//歌曲
            if (StorageInfo.CategoryId != 0)//类别ID不为空（修改）获取类别名
            {
                CategoryInfo categoryinfo = new CategoryInfo();
                categoryinfo.NickName = categorybll.CategoryIdAndNickName(StorageInfo.CategoryId);
                categoryinfo.CategoryId = StorageInfo.CategoryId;
                ViewBag.category = categoryinfo;//当前类别名 ID
            }
            else
            {
                StorageInfo.Frequency = ModelInfo.Frequency;//默认播放次数
                StorageInfo.ReleaseTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);//创建时间为现在
            }
            ViewBag.CategoryName = categorybll.SelectAllCategoryWay();//所有类别信息

            AlbumServiceLogic_Admin albumbll = new AlbumServiceLogic_Admin();
            if (StorageInfo.AlbumId != 0)//专辑ID不为空（修改）获取专辑名
            {
                AlbumInfo albuminfo = new AlbumInfo();
                albuminfo.Name = albumbll.SelectAlbumNameWay(StorageInfo.AlbumId);
                albuminfo.AlbumId = StorageInfo.AlbumId;
                ViewBag.album = albuminfo;//当前专辑名 ID
            }
            ViewBag.AlbumName = albumbll.SelectAllAlbumWay();//所有专辑信息
        }
        #endregion
        #endregion


    }

}