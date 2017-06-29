using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.Users.Models;
using ChangTing.Users.Repositories;
using ChangTing.Core.Models;

namespace ChangTing.Users.Services
{ 
    /// <summary>
    /// 反馈表操作的业务逻辑
    /// </summary>
     public class FeedBackServiceLogic
    {
        FeedBackDataAccess dal;

        #region SelectAllFeedBackWay
        /// <summary>
        /// 获取所有反馈的信息
        /// </summary>
        /// <returns></returns>
        public IList<FeedBackInfo> SelectAllFeedBackWay()
        {
            dal = new FeedBackDataAccess();
            return dal.SelectAllFeedBackWay();
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
            dal = new FeedBackDataAccess();
            return dal.SelectFeedBackWay(FeedBackid);
        }
        #endregion


        #region UpdateFeedBackWay
        /// <summary>
        /// 修改id号相等单条反馈信息
        /// </summary>
        /// <param name="FeedBackid">反馈Id</param>
        /// <returns></returns>
        public string UpdateFeedBackWay(FeedBackInfo FeedBackinfo)
        {
            dal = new FeedBackDataAccess();
            string fanhuizhi = null;
            bool x = dal.UpdateFeedBackWay(FeedBackinfo) > 0;
            if (x)
            {
                fanhuizhi = "已成功修改反馈信息！";
            }
            else
            {
                fanhuizhi = "修改信息失败！请联系管理员";
            }
            return fanhuizhi;
        }
        #endregion


        #region InsertFeedBackWay
        /// <summary>
        /// 新建反馈信息
        /// </summary>
        /// <param name="FeedBackinfo">反馈信息</param>
        /// <returns></returns>
        public string InsertFeedBackWay(FeedBackInfo FeedBackinfo)
        {
            

            dal = new FeedBackDataAccess();
            string fanhuizhi = null;
            
            FeedBackinfo.CreateDate = DateTime.Now;

            bool x = dal.InsertFeedBackWay(FeedBackinfo) !=null;
            if (x)
            {
                fanhuizhi = "反馈信息新建成功！";
            }
            else
            {
                fanhuizhi = "反馈信息新建失败！请联系管理员";
            }
            return fanhuizhi;
        }
        #endregion


        


        #region DeleteFeedBackWay
        /// <summary>
        /// 删除单条反馈数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteFeedBackWay(string id)
        {
            int ID = 0;
            bool cg = int.TryParse(id, out ID);
            if (!cg)
            {
                return cg;
            }

            dal = new FeedBackDataAccess();
            cg = dal.DeleteFeedBackWay(ID) > 0;
            
            return cg;

        }
        #endregion


        #region DeleteFeedBackMultiWay
        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteFeedBackMultiWay(string[] id)
        {
            dal = new FeedBackDataAccess();
            int cg = 0;
            for (int i = 0; i < id.Length; i++)
            {
                FeedBackServiceLogic bll = new FeedBackServiceLogic();
                if (dal.DeleteFeedBackWay(int.Parse(id[i])) > 0) {
                    cg++;
                }
            }

            return cg;

        }
        #endregion
        
    }
}
