using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;
using ChangTing.Core.Models;
using ChangTing.Music.Models;

namespace ChangTing.Music.Repositories
{
    /// <summary>
    /// 操作歌曲类别表
    /// </summary>
   public class CategoryDataAccess
    {
        #region SelectAllCategoryWay
        /// <summary>
        /// 提取全部的类别信息
        /// </summary>
        /// <returns></returns>
        public IList<CategoryInfo> SelectAllCategoryWay()
        {
            Sql sql = Sql.Builder.Append("select CategoryId,NickName,Number,Frequency,Display,CreateDate from Music_CT_Category");
            return ConnectionPool.db.Fetch<Models.CategoryInfo>(sql);
        }
        #endregion


        #region SelectCategoryIdStorageWay
        /// <summary>
        /// 提取当前类别全部的歌曲信息
        /// </summary>
        /// <param name="Categoryid">歌手Id</param>
        /// <returns></returns>
        public IList<CategoryInfo> SelectSingerIdCategoryWay(int categoryid)
        {
            Sql sql = Sql.Builder.Append("select StorageId,RealName,Path,CategoryId,AlbumId,SingerId,Frequency,Display,CreateDate from Music_CT_Storage where CategoryId=@0", categoryid);
            return ConnectionPool.db.Fetch<Models.CategoryInfo>(sql);
        }
        #endregion


        #region UpdateCategoryWay
        /// <summary>
        /// 更改单条歌曲类别信息
        /// </summary>
        /// <param name="CategoryInfo">歌手信息</param>
        /// <returns></returns>
        public int UpdateCategoryWay(CategoryInfo Categoryinfo)
        {
            return ConnectionPool.db.Update("Music_CT_Category", "CategoryId", Categoryinfo);
        }
        #endregion


        #region InsertCategoryWay
        /// <summary>
        /// 添加歌曲类别
        /// </summary>
        /// <param name="SingerInfo">歌手信息</param>
        /// <returns></returns>
        public object InsertCategoryWay(CategoryInfo Categoryinfo)
        {
            return ConnectionPool.db.Insert("Music_CT_Category", "CategoryId", Categoryinfo);
        }
        #endregion


        #region SelectCategoryWay
        /// <summary>
        /// 获取id号相等单条类别
        /// </summary>
        /// <param name="categoryid">类别Id</param>
        /// <returns></returns>
        public CategoryInfo SelectCategoryWay(int categoryid)
        {
            Sql sql = Sql.Builder.Append("select CategoryId,NickName,Number,Frequency,Display,CreateDate from Music_CT_Category where CategoryId=@0", categoryid);
            return ConnectionPool.db.FirstOrDefault<Models.CategoryInfo>(sql);
        }
        #endregion


        #region DeleteCategoryWay
        /// <summary>
        /// 删除单条类别数据的
        /// </summary>
        /// <returns></returns>
        public int DeleteCategoryWay(int id)
        {
            Sql sql = Sql.Builder.Append("delete from Music_CT_Category where CategoryId=@0", id);
            return ConnectionPool.db.Execute(sql);
        }
        #endregion

        #region SelectNumberMaxCategoryWay
        /// <summary>
        /// 歌曲类别表获取类别排序的最大值
        /// </summary>
        /// <returns></returns>
        public int SelectNumberMaxCategoryWay()
        {
            Sql sql = Sql.Builder.Append("select * from Music_CT_Category");
            return ConnectionPool.db.Fetch<CategoryInfo>(sql).Count;
        }
        #endregion


        #region OutdateCategoryNumberWay
        /// <summary>
        /// 类别的排序大于指定值时  -1
        /// </summary>
        /// <returns></returns>
        public int OutdateCategoryNumberWay(int number)
        {
            Sql sql = Sql.Builder.Append("update Music_CT_Category set Number = Number-1 where Number >= @0", number);
            return ConnectionPool.db.Execute(sql);
        }
        #endregion

        #region UpdateCategoryNumberWay
        /// <summary>
        /// 类别的排序大于指定值时  +1
        /// </summary>
        /// <returns></returns>
        public int UpdateCategoryNumberWay(int number)
        {
            Sql sql = Sql.Builder.Append("update Music_CT_Category set Number = Number+1 where Number >= @0", number);
            return ConnectionPool.db.Execute(sql);
        }
        #endregion


    }
}