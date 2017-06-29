using ChangTing.Music.Models;
using ChangTing.Music.Services;
using ChangTing.UI.Viewmodels;
using ChangTing.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChangTing.Singer.Models;
using ChangTing.Singer.Repositories;
using ChangTing.Users.Models;
using ChangTing.Users.Services;
using ChangTing.UI.WebProxy;
using ChangTing.Core.Models;
using ChangTing.UI.Models;

namespace ChangTing.UI.Controllers
{
    public partial class UIController : Controller
    {
        // GET: UI


        UIService bll = new UIService();
        UsersInfo uInfo = new UsersInfo();



        #region 用户登录
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View(new UsersInfo());
        }

        [HttpPost]
        public ActionResult Login(FormCollection FC)
        {
           
            string UserNmae = FC["Login-Name"];
            string password = FC["Login-Password"];
            if (string.IsNullOrEmpty(UserNmae)|| string.IsNullOrEmpty(password) )
            {
                return Content("<script>alert('登录名或密码不能为空');window.location.href='../UI/Login';</script>");
            }

            uInfo = bll.UserLogin(UserNmae, password);

                if (uInfo != null)
                {
                    UserState.SetUserState(uInfo);
                    uInfo = UserState.GetUserState();
                    ViewBag.name = uInfo.NickName;
                    return Redirect("index");
                }
            else
            {
                return Content("<script>alert('登陆失败，请输入正确的用户名和密码');window.location.href='../UI/Login';</script>");
            }
        }









        #endregion



        #region 用户注册
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View(new UsersInfo());
        }

        [HttpPost]
        public ActionResult Register(FormCollection FC, UsersInfo uInfo, HttpPostedFileBase file_upload)
        {
            //确认密码
            string uPass = FC["Pass"];
            #region 省市地区
            string province = FC["province"];
            string city = FC["city"];
            uInfo.Region =  province+","+city;
            #endregion
            DateTime newdate =new DateTime();
            
            if (!DateTime.TryParse(FC["birthday"], out newdate))
            {
                uInfo.Birthday =  DateTime.Now;
            }
            else
            {
                uInfo.Birthday = newdate;
            }
            if (!ModelState.IsValid)
            {
                return View(uInfo);
            }

            if (uPass != uInfo.PassWord)//判断登录密码和交易密码是否一致
            {
                ViewBag.hintpass = "请输入一致的密码";
                return View(uInfo);
            }
            if (string.IsNullOrEmpty(uPass))
            {
                ViewBag.hintpass = "确认密码不能为空";
                return View(uInfo);
            }
            uInfo.Available = 0;

            string imgtext = string.Empty;
            if (file_upload != null)//用户选择了图片
            {
                Img im = new Img();
                imgtext = string.Empty;
                imgtext = im.img(file_upload);
                if (string.Empty.Equals(imgtext))
                {
                    ViewBag.cg = "图片格式错误";
                    uInfo.HeadPortrait = null;
                    uInfo.HeadPortrait = ModelInfo.imgmoren;
                    return View(uInfo);
                }
                uInfo.HeadPortrait = imgtext;//照片路径存储
            }
            uInfo.CreateDate = DateTime.Now;
            
            bll.Register(uInfo);
            var hint = ("<script>alert('注册成功！');document.location.href = '../UI//Login';</script>");
            return Content(hint);
        }
        #endregion

        #region 个人设置
        [LoginCheck]
        public ActionResult PersonalSetting()
        {
            UsersInfo uInfo = new UsersInfo();
            int ID = UserState.GetUserState().UserId;
           
            IList<UsersInfo> uList = new List<UsersInfo>();
            uList = bll.SelectUIUser(ID);


            #region 获取用户昵称头像
            uInfo = UserState.GetUserState();
            ViewBag.User = uInfo;
            #endregion

            return View(uList);
        }
      
        #endregion


        #region 首页代码
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult index(FormCollection FC)
        {
            IList<NewestInfo> NewestInfolist = new List<NewestInfo>();
            NewestInfo NI = new NewestInfo();

            #region 获取用户昵称头像
            uInfo = UserState.GetUserState();
            ViewBag.User = uInfo;
            #endregion

            #region 获取热门歌曲
            IList<UIInfo> UIInfolist = new List<UIInfo>();
            UIInfolist = bll.SelectUIRecommend(6);
            ViewBag.Recommend = UIInfolist;
            #endregion

            #region  获取最新专辑前6条信息
            //先获取歌手id
            IList<AlbumInfo> albinfo = new List<AlbumInfo>();
          
            ChangTing.Singer.Repositories.SingerDataAccess sigdal = new SingerDataAccess();

            //通过歌曲id获取最新专辑前6条信息
            albinfo = bll.SelectNewestAlbumWay();
            for (int i = 0; i < albinfo.Count; i++)
            {
                NI = new NewestInfo();
                NI.AlbumId = albinfo[i].AlbumId;
                NI.SingerName = sigdal.SelectSingerWay(albinfo[i].SingerId).Name;
                NI.AlbumName = albinfo[i].Name;
                NI.Image = albinfo[i].Image;
                NewestInfolist.Add(NI);

            }

            ViewBag.Newest = NewestInfolist;
            #endregion

            #region 获取最新入驻歌手
            IList<SingerInfo> NewSinglist = new List<SingerInfo>();
            NewSinglist = bll.SelectSinger(6);
            ViewBag.NewestSinger = NewSinglist;

            #endregion
            
            return View();

          
        }
        #endregion


        #region 排行榜

        public ActionResult RankingList()
        {

            #region 获取用户昵称头像
            uInfo = UserState.GetUserState();
            ViewBag.User = uInfo;
            #endregion

            IList<UIInfo> UIInfolist = new List<UIInfo>();

            //获取热门歌曲
            UIInfolist = bll.SelectUIRecommend(3);
            ViewBag.Recommend = UIInfolist;

            //获取最新入驻歌手
            IList<SingerInfo> NewSinglist = new List<SingerInfo>();
            NewSinglist = bll.SelectSinger(3);
            ViewBag.NewestSinger = NewSinglist;



            return View();
        }
        #endregion

        public ActionResult FenYe(int id)
        {

            #region 获取用户昵称头像
            uInfo = UserState.GetUserState();
            ViewBag.User = uInfo;
            #endregion

            if (id==1016)
            {
                ViewBag.leibie = "欧美";
            }
            if (id == 1017)
            {
                ViewBag.leibie = "日语";
            }
            if (id == 2019)
            {
                ViewBag.leibie = "华语";
            }
            if (id == 2020)
            {
                ViewBag.leibie = "粤语";
            }
            if (id == 2021)
            {
                ViewBag.leibie = "韩语";
            }
            IList<UIInfo> UIInfolist = new List<UIInfo>();
            //获取热门歌曲
            UIInfolist = bll.SelectUIMusic(id,1);
            ViewBag.UIMusic = UIInfolist;
            
            
            return View();
        }







        public ActionResult UI()
        {
            return View();
        }


        #region
        // GET: UI/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UI/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UI/Create
        [HttpPost]




        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UI/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UI/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UI/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UI/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

    }
}
