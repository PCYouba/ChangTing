using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ChangTing.Singer.Models;
using ChangTing.Singer.Repositories;
using ChangTing.Users.Models;
using ChangTing.Users.Services;
using ChangTing.UI.WebProxy;
using ChangTing.Core.Models;
using ChangTing.UI.Models;
using ChangTing.UI.Services;
using ChangTing.Users.ViewModels;

namespace ChangTing.UI.Controllers
{
    [ValidateInput(false)]
    public partial class UIController : Controller
    {

        #region 个人收藏
        [LoginCheck]
        public ActionResult Collection()
        {
            string moren = "data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/PjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB3aWR0aD0iMTQwIiBoZWlnaHQ9IjE0MCIgdmlld0JveD0iMCAwIDE0MCAxNDAiIHByZXNlcnZlQXNwZWN0UmF0aW89Im5vbmUiPjwhLS0KU291cmNlIFVSTDogaG9sZGVyLmpzLzE0MHgxNDAKQ3JlYXRlZCB3aXRoIEhvbGRlci5qcyAyLjYuMC4KTGVhcm4gbW9yZSBhdCBodHRwOi8vaG9sZGVyanMuY29tCihjKSAyMDEyLTIwMTUgSXZhbiBNYWxvcGluc2t5IC0gaHR0cDovL2ltc2t5LmNvCi0tPjxkZWZzPjxzdHlsZSB0eXBlPSJ0ZXh0L2NzcyI+PCFbQ0RBVEFbI2hvbGRlcl8xNWM3YjA1MWI0NiB0ZXh0IHsgZmlsbDojQUFBQUFBO2ZvbnQtd2VpZ2h0OmJvbGQ7Zm9udC1mYW1pbHk6QXJpYWwsIEhlbHZldGljYSwgT3BlbiBTYW5zLCBzYW5zLXNlcmlmLCBtb25vc3BhY2U7Zm9udC1zaXplOjEwcHQgfSBdXT48L3N0eWxlPjwvZGVmcz48ZyBpZD0iaG9sZGVyXzE1YzdiMDUxYjQ2Ij48cmVjdCB3aWR0aD0iMTQwIiBoZWlnaHQ9IjE0MCIgZmlsbD0iI0VFRUVFRSIvPjxnPjx0ZXh0IHg9IjQzLjUiIHk9Ijc0LjgiPjE0MHgxNDA8L3RleHQ+PC9nPjwvZz48L3N2Zz4=";
            UsersInfo userinfo = UserState.GetUserState();
            if (userinfo.HeadPortrait==null)
            {
                userinfo.HeadPortrait = moren;
            }
            ViewBag.Region = userinfo.Region.Split(',');//省市

            ViewBag.User = userinfo;

            
            CommentInfo commentinfo = new CommentInfo();
            int ID = userinfo.UserId;
            
            CollectionService bll = new CollectionService();
            IList<CollectionViewInfo> uList = bll.SelectUserIdAndCollectionWay(ID);//获取当前ID的收藏
            return View(uList);
        }

        #endregion



    }
}
