using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChangTing.Music.Models;
using ChangTing.Core.Models;
using ChangTing.Admin.Models;
using ChangTing.Music.Services;

namespace ChangTing.Music.Controllers
{
    public partial class MusicController : Controller
    {

        #region 类别信息列表相关

        #region 类别信息列表直接进入显示所有信息
        /// <summary>
        /// Category(类别信息表)直接查看
        /// </summary>
        /// <returns></returns>
        [AdminLoginCheck]
        public ActionResult CategoryList_Admin()
        {
            CategoryServiceLogic_Admin bll = new CategoryServiceLogic_Admin();
            IList<CategoryInfo> CategoryInfolist = new List<CategoryInfo>();
            
            CategoryInfolist = bll.SelectAllCategoryWay();
            
            return View(CategoryInfolist);
        }
        #endregion

        #region 歌曲类别信息列表删除某条类别数据

        #region 单条类别删除
        /// <summary>
        /// 单个类别删除josn接收
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteCategory(string id)
        {
            CategoryServiceLogic_Admin bll = new CategoryServiceLogic_Admin();
            object [] cg = bll.DeleteCategoryWay(id);
            return Json(new {data1= cg[0],data2 = cg[1] });
        }

        #endregion

        #region 多条类别删除
        /// <summary>
        /// 批量类别删除josn接收
        /// </summary>
        /// <param name="fxkint"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteCategoryMulti(string[] fxkint)
        {
            CategoryServiceLogic_Admin bll = new CategoryServiceLogic_Admin();
            int cg = bll.DeleteCategoryMultiWay(fxkint);
            return Json(cg);
        }
        #endregion

        #endregion

        #endregion


        #region 类别信息增改界面

        #region 新建或修改进入界面时
        /// <summary>
        /// CategoryListAdd(类别信息添加（修改）)直接查看
        /// </summary>
        /// <returns></returns>
        [AdminLoginCheck]
        public ActionResult CategoryListAdd_Admin(int categoryid)
        {

            CategoryInfo CategoryInfo = new CategoryInfo();
            CategoryServiceLogic_Admin bll = new CategoryServiceLogic_Admin();
            int maxNumber = 0;
            maxNumber = bll.SelectNumberMaxCategoryWay();//MAX序号
            if (categoryid>0)//修改的时候
            {
                CategoryInfo = bll.SelectCategoryWay(categoryid);
            }
            else//新建的时候
            {
                maxNumber++;//新建的时候序号+1
                CategoryInfo.Number = maxNumber;//默认序号为最大
                CategoryInfo.Frequency = ModelInfo.Frequency;//默认播放次数
            }
            ViewBag.NumberMax = maxNumber;//获取最大序号
            
            return View(CategoryInfo);//新增返回空  修改返回内容
        }
        #endregion

        #region 新建或修改时提交操作
        /// <summary>
        /// 保存修改（添加内容）提交
        /// </summary>
        /// <param name="singerid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CategoryListAdd_Admin(CategoryInfo CategoryInfo, FormCollection fc)
        {
            CategoryInfo.Display = Convert.ToByte(fc["radio-zhuangtai"]);//id获取

            CategoryServiceLogic_Admin bll = new CategoryServiceLogic_Admin();
            if (!ModelState.IsValid)
            {
                return View(CategoryInfo);
            }

            string fanhuizhi = null;//操作是否成功

            if (CategoryInfo.CategoryId > 0)//Id大于0执行修改操作
            {
                fanhuizhi = bll.UpdateCategoryWay(CategoryInfo);
            }
            else//小于0 执行新建操作
            {
                fanhuizhi = bll.InsertCategoryWay(CategoryInfo);
            }
            ViewBag.NumberMax = bll.SelectNumberMaxCategoryWay();//MAX序号
            ViewBag.cg = fanhuizhi;//存储返回值
            return View(CategoryInfo);
        }
        #endregion

        #endregion

    }
}
