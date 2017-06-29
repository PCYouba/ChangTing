using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetaPoco;
using ChangTing.Singer.Models;
using ChangTing.Music.Models;
using ChangTing.Singer.Repositories;
using ChangTing.Index.ViewModel;



namespace ChangTing.Index.Controllers
{
    public partial class IndexController : Controller
    {
        Singer.Repositories.SingerDataAccess SingerDA = new SingerDataAccess();
        Singer.Repositories.AlbumDataAccess AlbumDA = new AlbumDataAccess();
       
        public ActionResult Singers()
        {
            ChangTing.Singer.Services.SingerServiceLogic dll = new Singer.Services.SingerServiceLogic();
            return View(dll.SelectTopSinger(6));
        }
        
        public ActionResult RuzhuSinger(int? page)
        {
            if (page==null)
            {
                page = 1;
            }
            ChangTing.Singer.Services.SingerServiceLogic dll = new Singer.Services.SingerServiceLogic();
            Page<SingerInfo> pSingerList = SingerDA.SingerfenyeList(Convert.ToInt32(page), 16);
            ViewBag.sinfo = dll.SelectAllSingerWay();
            return View(pSingerList);
        }
        
        public ActionResult SingerXQ(int? id)
        {
            if (id==0 || id ==null)
            {
                return Redirect("~/UI/index");
            }
            int ID = 0;
            try
            {
                ID = Convert.ToInt32(id);
            }
            catch (Exception)
            {
                return Redirect("~/UI/index");
            }
           
            SingerInfo sinInfo = new SingerInfo();
            ChangTing.Singer.Services.SingerServiceLogic dll = new Singer.Services.SingerServiceLogic();
            IList<StorageInfo>  musicInfo = new List<StorageInfo>();
            ChangTing.Music.Services.StorageServiceLogic_Admin musdll = new Music.Services.StorageServiceLogic_Admin();
            musicInfo = musdll.SelectSingermusic(ID);
            

           ViewBag.muscount = musdll.musicCount(ID);
            ViewBag.mus = musicInfo;
           
            sinInfo = dll.SelectSingerWay(ID);
            if (sinInfo==null)
            {
                sinInfo = new SingerInfo();
            }
           
           
            return View(sinInfo);
        }

      public ActionResult Xindieshangjia(int? page)
        {
            if(page==1)
            {
                page = 1;
            }
            ChangTing.Singer.Services.AlbumServiceLogic_Admin album = new Singer.Services.AlbumServiceLogic_Admin();
            Page<AlbumInfo> pAlbumList = AlbumDA.AlbumfenyeList(Convert.ToInt32(page), 16);
            ViewBag.album = album.SelectAllAlbumWay();
            return View(pAlbumList);
        }

    }
}
