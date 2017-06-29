using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.Users.Models;
using ChangTing.Core.Models;
using PetaPoco;

namespace ChangTing.Users.Repositories
{
    /// <summary>
    /// 操作反馈表
    /// </summary>
    public class FeedBackDataAccess
    {
        #region SelectAllFeedBackWay
        /// <summary>
        /// 提取全部的反馈信息
        /// </summary>
        /// <returns></returns>
        public IList<FeedBackInfo> SelectAllFeedBackWay()
        {
            Sql sql = Sql.Builder.Append("select FeedBackId,S_Content,UserId,Phone,IsSolve,CreateDate from Music_CT_FeedBack");
            return ConnectionPool.db.Fetch<FeedBackInfo>(sql);
        }
        #endregion

        
        #region SelectFeedBackWay
        /// <summary>
        /// 获取id号相等单条反馈信息
        /// </summary>
        /// <param name="FeedBackid">反馈Id</param>
        /// <returns></returns>
        public FeedBackInfo SelectFeedBackWay(int FeedBackid)
        {
            Sql sql = Sql.Builder.Append("select FeedBackId,S_Content,UserId,Phone,IsSolve,CreateDate from Music_CT_FeedBack where FeedBackId=@0", FeedBackid);
            return ConnectionPool.db.FirstOrDefault<FeedBackInfo>(sql);
        }
        #endregion

        
        #region UpdateFeedBackWay
        /// <summary>
        /// 更改单条反馈信息
        /// </summary>
        /// <param name="FeedBackInfo">反馈信息</param>
        /// <returns></returns>
        public int UpdateFeedBackWay(FeedBackInfo FeedBackinfo)
        {
            return ConnectionPool.db.Update("Music_CT_FeedBack", "FeedBackId", FeedBackinfo);
        }
        #endregion


        #region InsertFeedBackWay
        /// <summary>
        /// 增加单条反馈信息
        /// </summary>
        /// <param name="FeedBackInfo">反馈信息</param>
        /// <returns></returns>
        public object InsertFeedBackWay(FeedBackInfo FeedBackinfo)
        {
            return ConnectionPool.db.Insert("Music_CT_FeedBack", "FeedBackId", FeedBackinfo);
        }
        #endregion


        #region DeleteFeedBackWay
        /// <summary>
        /// 删除单条反馈数据的
        /// </summary>
        /// <returns></returns>
        public int DeleteFeedBackWay(int id)
        {
            Sql sql = Sql.Builder.Append("delete from Music_CT_FeedBack where FeedBackId=@0", id);
            return ConnectionPool.db.Execute(sql);
        }
        #endregion
        
    }
}
