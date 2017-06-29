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
     public class CommentServiceLogic_Admin
    {
        CommentDataAccess dal;
        #region SelectAllAlbumWay
        /// <summary>
        /// 获取所有评论的信息
        /// </summary>
        /// <returns></returns>
        public IList<CommentInfo> SelectAllCommentWay()
        {
            dal = new CommentDataAccess();
            return dal.SelectAllCommentWay();
        }
        #endregion

        #region SelectCommentidCommentWay
        /// <summary>
        /// 获取用户id号相等所有评论信息
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public IList<CommentInfo> SelectCommentidCommentWay(int userid)
        {
            dal = new CommentDataAccess();
            return dal.SelectUserIdAndCollectionWay(userid);
        }
        #endregion


        #region SelectCommentidCommentWay
        /// <summary>
        /// 获取本专辑所有评论信息
        /// </summary>
        /// <param name="userid">专辑ID</param>
        /// <returns></returns>
        public IList<CommentInfo> SelectAlbumIdidCommentWay(int albumid)
        {
            dal = new CommentDataAccess();
            return dal.SelectUserIdAndCollectionWay(albumid);
        }
        #endregion


        #region UpdateAvailableOutCommentWay
        /// <summary>
        /// 通过Id修改评论为不可用
        /// </summary>
        /// <param name="Commentid">评论Id</param>
        /// <returns></returns>
        public int UpdateAvailableOutCommentWay(int Commentid)
        {
            dal = new CommentDataAccess();

            int fanhuizhi = 0;
            bool x = dal.UpdateCommentDisplayWay(Commentid, 1) > 0;

            if (x)
            {
                fanhuizhi = Commentid;
            }
            else
            {
                fanhuizhi = 0;
            }
            return fanhuizhi;
        }
        #endregion

        #region UpdateAvailableUpCommentWay
        /// <summary>
        /// 通过Id修改评论为可用
        /// </summary>
        /// <param name="Commentid">评论Id</param>
        /// <returns></returns>
        public int UpdateAvailableUpCommentWay(int Commentid)
        {
            dal = new CommentDataAccess();

            int fanhuizhi = 0;
            bool x = dal.UpdateCommentDisplayWay(Commentid, 0) > 0;

            if (x)
            {
                fanhuizhi = Commentid;
            }
            else
            {
                fanhuizhi = 0;
            }
            return fanhuizhi;
        }
        #endregion

        #region DeleteCommentWay
        /// <summary>
        /// 删除单条评论数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object[] DeleteCommentWay(string id)
        {
            int ID = 0;
            int xj = 0;//下级
            bool cg = int.TryParse(id, out ID);
            if (!cg)
            {
                return new object[] { cg, xj };
            }

            dal = new CommentDataAccess();
            
            CommentDataAccess pldal = new CommentDataAccess();
            xj = pldal.DeleteCommentIdAndReplyCommentWay(ID);
            cg = dal.DeleteCommentWay(ID) > 0;

            return new object[] { cg, xj};
        }
        #endregion

        #region DeleteSingerMultiWay
        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteCommentMultiWay(string[] id)
        {
            dal = new CommentDataAccess();
            int[] ID = new int[id.Length];
            int cg = 0;
            int xj = 0;//下级
            CollectionDataAccess coldal = new CollectionDataAccess();
            CommentDataAccess pldal = new CommentDataAccess();
            for (int i = 0; i < id.Length; i++)
            {
                xj += pldal.DeleteCommentIdAndReplyCommentWay(ID[i]);//先删除子评论
                ID[i] = int.Parse(id[i]);
                if (dal.DeleteCommentWay(ID[i]) > 0)
                {
                    cg++;
                }
            }
            return cg;
        }
        #endregion
    }
}
