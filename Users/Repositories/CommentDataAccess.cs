using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.Users.Models;
using PetaPoco;
using ChangTing.Core.Models;

namespace ChangTing.Users.Repositories
{
    /// <summary>
    /// 评论表数据访问
    /// </summary>
     public class CommentDataAccess
    {
        #region SelectAllCommentWay
        /// <summary>
        /// 提取全部的评论信息
        /// </summary>
        /// <returns></returns>
        public IList<CommentInfo> SelectAllCommentWay()
        {
            Sql sql = Sql.Builder.Append("select CommentId,UserId,StorageId,C_Content,Praise,Tread,Display,Reply,CreateDate from Music_CT_Comment");
            return ConnectionPool.db.Fetch<CommentInfo>(sql);
        }
        #endregion

        #region SelectUserIdAndCollectionWay
        /// <summary>
        /// 根据用户id提取本评论全部信息
        /// </summary>
        /// <param name="userid">用户Id</param>
        /// <returns></returns>
        public IList<CommentInfo> SelectUserIdAndCollectionWay(int userid)
        {
            Sql sql = Sql.Builder.Append("select CommentId,UserId,StorageId,C_Content,Praise,Tread,Display,Reply,CreateDate from Music_CT_Comment where UserId=@0", userid);
            return ConnectionPool.db.Fetch<CommentInfo>(sql);
        }
        #endregion

        #region SelectUserIdAndCollectionWay
        /// <summary>
        /// 根据用户id提取本评论全部信息
        /// </summary>
        /// <param name="userid">用户Id</param>
        /// <returns></returns>
        //public IList<CommentInfo> SelectUserIdAndCollectionWay(int userid)
        //{
        //    Sql sql = Sql.Builder.Append("select CommentId,UserId,StorageId,C_Content,Praise,Tread,Display,Reply,CreateDate from Music_CT_Comment where UserId=@0", userid);
        //    return ConnectionPool.db.Fetch<CommentInfo>(sql);
        //}
        #endregion


        //SelectAlbumIdidCommentWay

        #region UpdateCommentWay
        /// <summary>
        /// 更改单条评论信息
        /// </summary>
        /// <param name="Commentinfo">评论信息</param>
        /// <returns></returns>
        public int UpdateCommentWay(CommentInfo Commentinfo)
        {
            return ConnectionPool.db.Update("Music_CT_Comment", "CommentId", Commentinfo);
        }
        #endregion

        #region UpdateCommentAvailableWay
        /// <summary>
        /// 更改单条评论的状态信息
        /// </summary>
        /// <param name="commentid">评论id</param>
        /// <param name="available">评论状态</param>
        /// <returns></returns>
        public int UpdateCommentDisplayWay(int commentid, int display)
        {
            Sql sql = Sql.Builder.Append("update Music_CT_Comment set Display=@0 where CommentId=@1", display, commentid);
            return ConnectionPool.db.Execute(sql);
        }
        #endregion

        #region InsertCommentWay
        /// <summary>
        /// 添加单条评论信息
        /// </summary>
        /// <param name="CommentInfo">评论信息</param>
        /// <returns></returns>
        public object InsertCommentWay(CommentInfo Commentinfo)
        {
            return ConnectionPool.db.Insert("Music_CT_Comment", "CommentId", Commentinfo);
        }
        #endregion

        #region DeleteCommentWay
        /// <summary>
        /// 删除单条评论数据的
        /// </summary>
        /// <returns></returns>
        public int DeleteCommentWay(int id)
        {
            Sql sql = Sql.Builder.Append("delete from Music_CT_Comment where CommentId=@0", id);
            return ConnectionPool.db.Execute(sql);
        }
        #endregion

        #region DeleteUserIdCollectionWay
        /// <summary>
        /// 根据用户ID删除所有评论数据的
        /// </summary>
        /// <returns></returns>
        public int DeleteUserIdCollectionWay(int id)
        {
            Sql sql = Sql.Builder.Append("delete from Music_CT_Comment where UserId=@0", id);
            return ConnectionPool.db.Execute(sql);
        }
        #endregion


        #region DeleteCommentIdAndReplyCommentWay
        /// <summary>
        /// 根据评论ID删除所有子评论数据的
        /// </summary>
        /// <returns></returns>
        public int DeleteCommentIdAndReplyCommentWay(int id)
        {
            Sql sql = Sql.Builder.Append("delete from Music_CT_Comment where Reply=@0", id);
            return ConnectionPool.db.Execute(sql);
        }
        #endregion

    }
}
