using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetaPoco;
using ChangTing.Singer.Models;
using ChangTing.Music.Models;
using ChangTing.Singer.Services;
using ChangTing.Index.ViewModel;
using ChangTing.Music.Services;
using ChangTing.UI.WebProxy;
using ChangTing.Users.Services;

namespace ChangTing.Index.Controllers
{
    [ValidateInput(false)]
    public partial class IndexController : Controller
    {
        //[]
        public ActionResult SingerIsAlbum(int? AlbumId)
        {
         
            if (AlbumId == 0 || AlbumId == null)
            {
                return Redirect("~/UI/index");
            }
            int ID = 0;
            try
            {
                ID = Convert.ToInt32(AlbumId);
            }
            catch (Exception)
            {
                return Redirect("~/UI/index");
            }

            AlbumServiceLogic_Admin albbll = new AlbumServiceLogic_Admin();
            AlbumInfo albuminfo = albbll.SelectAlbumWay(ID);//获取当前专辑全部信息


            SingerServiceLogic sinbll = new SingerServiceLogic();
            SingerInfo singerinfo = sinbll.SelectSingerWay(albuminfo.SingerId);//获取当前歌手全部信息
            
            AlbumAndSingerViewInfo view = new AlbumAndSingerViewInfo();
            view.AlbumId = albuminfo.AlbumId;//专辑ID
            view.AlbumName = albuminfo.Name;//专辑名
            view.Image = albuminfo.Image;//专辑图片
            view.Introduce = albuminfo.Introduce;//专辑介绍
            view.Issue = albuminfo.Issue;//发行时间

            view.SingerId = singerinfo.SingerId;//歌手ID
            view.SingerName = singerinfo.Name;//歌手名
            view.Age = singerinfo.Age;//歌手年龄
            //其他的不需要

            StorageServiceLogic_Admin stobll = new StorageServiceLogic_Admin();
            IList<StorageInfo> StorageInfolist = stobll.SelectAlbumidAllStorageWay(albuminfo.AlbumId);//获取当前专辑的全部歌曲
            ViewBag.Storage = StorageInfolist;//歌曲


            ViewBag.User = UserState.GetUserState();//用户信息
            
            //评论
            CommentServiceLogic_Admin combll = new CommentServiceLogic_Admin();
            //combll.

            return View(view);
        }


    }
}
