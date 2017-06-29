using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.Music.Models;
using ChangTing.Music.Repositories;
using System.Web.Mvc;

namespace ChangTing.Music.Services
{
    /// <summary>
    /// 歌曲类别业务逻辑
    /// </summary>
    public class CategoryServiceLogic_Admin
    {
        CategoryDataAccess dal;

        #region SelectAllCategoryWay
        /// <summary>
        /// 获取所有类别的信息
        /// </summary>
        /// <returns></returns>
        public IList<CategoryInfo> SelectAllCategoryWay()
        {
            dal = new CategoryDataAccess();
            return dal.SelectAllCategoryWay();
        }
        #endregion


        #region SelectCategoryWay
        /// <summary>
        /// 获取id号相等单条类别信息
        /// </summary>
        /// <param name="categoryid">类别Id</param>
        /// <returns></returns>
        public CategoryInfo SelectCategoryWay(int categoryid)
        {
            dal = new  CategoryDataAccess();
            return dal.SelectCategoryWay(categoryid);
        }
        #endregion


        #region UpdateCategoryWay
        /// <summary>
        /// 修改id号相等单条歌曲类别信息
        /// </summary>
        /// <param name="categoryinfo">歌曲类别信息</param>
        /// <returns></returns>
        public string UpdateCategoryWay(CategoryInfo categoryinfo)
        {
            dal = new  CategoryDataAccess();
            string fanhuizhi = null;


            bool x = dal.OutdateCategoryNumberWay(dal.SelectCategoryWay(categoryinfo.CategoryId).Number) >0;
            dal.UpdateCategoryNumberWay(categoryinfo.Number);
            x = dal.UpdateCategoryWay(categoryinfo) > 0;
            if (x)
            {
                fanhuizhi = "已成功修改歌曲类别信息！";
            }
            else
            {
                fanhuizhi = "修改信息失败！请联系管理员";
            }
            return fanhuizhi;
        }
        #endregion


        #region InsertCategoryWay
        /// <summary>
        /// 新建歌曲类别信息
        /// </summary>
        /// <param name="Categoryinfo">歌曲类别信息</param>
        /// <returns></returns>
        public string InsertCategoryWay(CategoryInfo Categoryinfo)
        {
            dal = new CategoryDataAccess();
            string fanhuizhi = null;

            Categoryinfo.CreateDate = DateTime.Now;


            dal.UpdateCategoryNumberWay(Categoryinfo.Number);

            bool x = dal.InsertCategoryWay(Categoryinfo) != null;
            if (x)
            {
                fanhuizhi = "歌曲类别信息新建成功！";
            }
            else
            {
                fanhuizhi = "歌曲类别信息新建失败！请联系管理员";
            }
            return fanhuizhi;
        }
        #endregion


        #region DeleteCategoryWay
        /// <summary>
        /// 删除单条类别数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回成功 删除歌曲个数</returns>
        public object[] DeleteCategoryWay(string id)
        {
            int ID = 0;
            if (!int.TryParse(id,out ID))
            {
                return new object[] {false,null };
            }
            StorageDataAccess Storagebll = new StorageDataAccess();
            int yinyuegs = Storagebll.DeleteCategoryIdStorageWay(ID);//删除歌曲返回操作个数


            //删除前获取类别的排序
            int nb = SelectCategoryNumberWay(ID);//获取排序

            dal = new CategoryDataAccess();
            dal.OutdateCategoryNumberWay(nb);//大于排序的排序-1
            bool cg = dal.DeleteCategoryWay(ID) > 0;//删除类别返回操作格式
            object[] fhz = { cg, yinyuegs };
            return fhz;
        }
        
        
        #endregion


        #region DeleteCategoryMultiWay
        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="id">需要删除的id</param>
        /// <returns></returns>
        public int DeleteCategoryMultiWay(string[] id)
        {

            StorageDataAccess Storagebll = new StorageDataAccess();
            int yinyuegs = 0;
            

            dal = new CategoryDataAccess();
            int cg = 0;
            for (int i = 0; i < id.Length; i++)
            {
                int ID = 0;
                int.TryParse(id[i],out ID);
                yinyuegs += Storagebll.DeleteCategoryIdStorageWay(ID);//删除歌曲返回操作个数

                int nb = SelectCategoryNumberWay(ID);//获取排序
                dal = new CategoryDataAccess();
                dal.UpdateCategoryNumberWay(nb);//大于排序的排序-1

                if (dal.DeleteCategoryWay(ID) > 0)//删除单条
                {
                    cg++;
                }
            }
            return cg;

        }
        #endregion


        #region CategoryIdAndName
        /// <summary>
        /// 通过歌曲类别获取类别姓名
        /// </summary>
        /// <param name="categoryid">歌曲类别ID</param>
        /// <returns></returns>
        public string CategoryIdAndName(int categoryid)
        {
            dal = new CategoryDataAccess();
            return dal.SelectCategoryWay(categoryid).NickName;
        }
        #endregion


        /// <summary>
        /// 获取当前类别的排序数
        /// </summary>
        /// <param name="ID">ID名</param>
        /// <returns></returns>
        public int SelectCategoryNumberWay(int ID)
        {
            dal = new CategoryDataAccess();
            return dal.SelectCategoryWay(ID).Number;
        }


        #region SelectNumberMaxCategoryWay
        /// <summary>
        /// 歌曲类别表获取类别排序的最大值
        /// </summary>
        /// <returns></returns>
        public int SelectNumberMaxCategoryWay()
        {
            dal = new CategoryDataAccess();
            return dal.SelectNumberMaxCategoryWay();
        }
        #endregion

        #region CategoryIdAndNickName
        /// <summary>
        /// 歌曲ID获取歌曲名
        /// </summary>
        /// <returns></returns>
        public string CategoryIdAndNickName(int categoryid)
        {
            dal = new CategoryDataAccess();
            return dal.SelectCategoryWay(categoryid).NickName;
        }
        #endregion
    }
}
