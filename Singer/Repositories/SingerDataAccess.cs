using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.Singer.Models;
using ChangTing.Core.Models;
using PetaPoco;

namespace ChangTing.Singer.Repositories
{
    /// <summary>
    /// 操作歌手表
    /// </summary>
    public class SingerDataAccess
    {
        #region SelectAllSingerWay
        /// <summary>
        /// 提取全部的歌手信息
        /// </summary>
        /// <returns></returns>
        public IList<SingerInfo> SelectAllSingerWay()
        {
            Sql sql = Sql.Builder.Append("select SingerId,Name,HeadPortrait,Introduce,Gender,Age,CreateDate from Music_CT_Singer");
            return ConnectionPool.db.Fetch<Models.SingerInfo>(sql);
        }
        #endregion
        
        #region
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="itemsPerPage">每页显示个数</param>
        /// <returns></returns>
        public Page<SingerInfo> SingerfenyeList(int page,int PageSize)
        {
            Sql sql = Sql.Builder
                .Select("*")
                .From("Music_CT_Singer")
                .Where("SingerId>=1")
                .OrderBy("SingerId desc");
            return ConnectionPool.db.Page<SingerInfo>(page, PageSize, sql);
        }
        #endregion
        #region SelectTopSinger
        /// <summary>
        /// 提取前
        /// </summary>
        /// <returns></returns>
        public IList<SingerInfo> SelectTopSinger(int top)
        {
            string sSql = string.Format("select top {0} * from Music_CT_Singer order by SingerId desc", top);
            Sql sql = Sql.Builder.Append(sSql);
            return ConnectionPool.db.Fetch<Models.SingerInfo>(sql);
        }
        #endregion
        
       

        #region SelectSingerWay
        /// <summary>
        /// 获取id号相等单条歌手信息
        /// </summary>
        /// <param name="singerid">歌手Id</param>
        /// <returns></returns>
        public SingerInfo SelectSingerWay(int singerid)
        {
            Sql sql = Sql.Builder.Append("select SingerId,Name,HeadPortrait,Introduce,Gender,Age,CreateDate from Music_CT_Singer where SingerId=@0", singerid);
            return ConnectionPool.db.FirstOrDefault<Models.SingerInfo>(sql);
        }
        #endregion

        
        #region UpdateSingerWay
        /// <summary>
        /// 更改单条歌手信息
        /// </summary>
        /// <param name="SingerInfo">歌手信息</param>
        /// <returns></returns>
        public int UpdateSingerWay(SingerInfo singerinfo)
        {
            return ConnectionPool.db.Update("Music_CT_Singer", "SingerId", singerinfo);
        }
        #endregion


        #region InsertSingerWay
        /// <summary>
        /// 更改单条歌手信息
        /// </summary>
        /// <param name="SingerInfo">歌手信息</param>
        /// <returns></returns>
        public object InsertSingerWay(SingerInfo singerinfo)
        {
            return ConnectionPool.db.Insert("Music_CT_Singer", "SingerId", singerinfo);
        }
        #endregion


        #region DeleteSingerWay
        /// <summary>
        /// 删除单条歌手数据的
        /// </summary>
        /// <returns></returns>
        public int DeleteSingerWay(int id)
        {
            Sql sql = Sql.Builder.Append("delete from Music_CT_Singer where SingerId=@0", id);
            return ConnectionPool.db.Execute(sql);
        }
        #endregion
        

        #region MaxLengthSingerIdSingerWay
        /// <summary>
        /// 获取歌手信息Id的最大值
        /// </summary>
        /// <returns></returns>
        public SingerInfo MaxLengthSingerIdSingerWay()
        {
            Sql sql = Sql.Builder.Append("select top 1 SingerId from Singer order by SingerId asc;");
            return ConnectionPool.db.FirstOrDefault<SingerInfo>(sql);
        }
        #endregion


       

    }
}
