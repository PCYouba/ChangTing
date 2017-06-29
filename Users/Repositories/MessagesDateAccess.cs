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
    /// 操作信息表
    /// </summary>
     public class MessagesDateAccess
    {
        #region SelectAllMessagesWay
        /// <summary>
        /// 提取全部的信息
        /// </summary>
        /// <returns></returns>
        public IList<MessagesInfo> SelectAllMessagesWay()
        {
            Sql sql = Sql.Builder.Append("select MessageId,Title,M_Content,FoundId,UserId,GradeLevel,Display,CreateDate from Music_CT_Messages");
            return ConnectionPool.db.Fetch<MessagesInfo>(sql);
        }
        #endregion


        #region SelectGradeLevelMessagesWay
        /// <summary>
        /// 提取当前状态等级的所有信息
        /// </summary>
        /// <param name="GradeLevel">等级</param>
        /// <returns></returns>
        public IList<MessagesInfo> SelectSingerIdMessagesWay(int GradeLevel)
        {
            Sql sql = Sql.Builder.Append("select MessageId,Title,M_Content,FoundId,UserId,GradeLevel,Display,CreateDate from Music_CT_Messages where GradeLevel=@0", GradeLevel);
            return ConnectionPool.db.Fetch<Models.MessagesInfo>(sql);
        }
        #endregion


        #region UpdateMessagesWay
        /// <summary>
        /// 更改单条歌手信息
        /// </summary>
        /// <param name="MessagesInfo">歌手信息</param>
        /// <returns></returns>
        public int UpdateMessagesWay(MessagesInfo Messagesinfo)
        {
            return ConnectionPool.db.Update("Music_CT_Messages", "MessagesId", Messagesinfo);
        }
        #endregion


        #region InsertMessagesWay
        /// <summary>
        /// 更改单条歌手信息
        /// </summary>
        /// <param name="SingerInfo">歌手信息</param>
        /// <returns></returns>
        public object InsertMessagesWay(MessagesInfo Messagesinfo)
        {
            return ConnectionPool.db.Insert("Music_CT_Messages", "MessagesId", Messagesinfo);
        }
        #endregion


        #region SelectMessagesWay
        /// <summary>
        /// 获取id号相等单条信息
        /// </summary>
        /// <param name="Messagesid">Id</param>
        /// <returns></returns>
        public MessagesInfo SelectMessagesWay(int Messagesid)
        {
            Sql sql = Sql.Builder.Append("select MessageId,Title,M_Content,FoundId,UserId,GradeLevel,Display,CreateDate from Music_CT_Messages where MessageId=@0", Messagesid);
            return ConnectionPool.db.FirstOrDefault<MessagesInfo>(sql);
        }
        #endregion


        #region DeleteMessagesWay
        /// <summary>
        /// 删除单条数据的
        /// </summary>
        /// <returns></returns>
        public int DeleteMessagesWay(int id)
        {
            Sql sql = Sql.Builder.Append("delete from Music_CT_Messages where MessagesId=@0", id);
            return ConnectionPool.db.Execute(sql);
        }
        #endregion

    }
}
