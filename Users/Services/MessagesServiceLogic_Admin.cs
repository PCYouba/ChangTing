using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.Users.Repositories;
using ChangTing.Users.Models;
using ChangTing.Admin.Services;

namespace ChangTing.Users.Services
{
    /// <summary>
    /// 消息业务逻辑
    /// </summary>
    public class MessagesServiceLogic_Admin
    {

        MessagesDateAccess dal;
        #region SelectAllMessagesWay
        /// <summary>
        /// 获取所有的信息
        /// </summary>
        /// <returns></returns>
        public IList<MessagesInfo> SelectAllMessagesWay()
        {
            dal = new MessagesDateAccess();
            return dal.SelectAllMessagesWay();
        }
        #endregion


        #region SelectMessagesWay
        /// <summary>
        /// 获取id号相等单条信息
        /// </summary>
        /// <param name="Messagesid">歌手Id</param>
        /// <returns></returns>
        public MessagesInfo SelectMessagesWay(int Messagesid)
        {
            dal = new MessagesDateAccess();
            return dal.SelectMessagesWay(Messagesid);
        }
        #endregion

        #region SelectGradeLevelMessagesWay
        /// <summary>
        /// 提取当前等级状态的所有信息
        /// </summary>
        /// <param name="GradeLevel">等级</param>
        /// <returns></returns>
        public IList<MessagesInfo> SelectGradeLevelMessagesWay(int GradeLevel)
        {
            dal = new MessagesDateAccess();
            IList <MessagesInfo> Messagesinfo = dal.SelectSingerIdMessagesWay(GradeLevel);
            return Messagesinfo;

        }
        #endregion


        #region SelectMessagesNameWay
        /// <summary>
        /// 根据信息表单个内容  获取  创建者  收件者
        /// </summary>
        /// <param name="Messagesid">歌手Id</param>
        /// <returns></returns>
        public string[] SelectMessagesAllNameWay(MessagesInfo messagesinfo)
        {
            string Found, User;
            UsersServiceLogic_Admin userbll = new UsersServiceLogic_Admin();
            if (messagesinfo.GradeLevel == 2)
            {
                Found = userbll.SelectUserLoginNameWay(messagesinfo.FoundId);
                User = userbll.SelectUserLoginNameWay(messagesinfo.UserId);
                return new string[] { Found, User };
            }
            SelectAdmin adminbll = new SelectAdmin();
            Found = adminbll.SelectIdAdminNameWay(messagesinfo.FoundId);
            if (messagesinfo.GradeLevel == 0)
            {
                return new string[] { Found, "全部用户" };
            }
            else //messagesinfo.GradeLevel == 1
            {
                User = userbll.SelectUserLoginNameWay(messagesinfo.UserId);
                return new string[] { Found , User };
            }
        }
        #endregion


        #region UpdateSingerWay
        /// <summary>
        /// 修改id号相等单条信息
        /// </summary>
        /// <param name="Messagesinfo">信息</param>
        /// <returns></returns>
        public string UpdateMessagesWay(MessagesInfo Messagesinfo)
        {
            dal = new MessagesDateAccess();
            string fanhuizhi = null;
            bool x = dal.UpdateMessagesWay(Messagesinfo) > 0;
            if (x)
            {
                fanhuizhi = "已成功修改信息！";
            }
            else
            {
                fanhuizhi = "修改信息失败！请联系管理员";
            }
            return fanhuizhi;
        }
        #endregion


        #region InsertMessagesWay
        /// <summary>
        /// 新建信息
        /// </summary>
        /// <param name="singerid">歌手Id</param>
        /// <returns></returns>
        public string InsertMessagesWay(MessagesInfo Messagesinfo)
        {
           

            dal = new MessagesDateAccess();
            string fanhuizhi = null;

            Messagesinfo.CreateDate = DateTime.Now;

            bool x = dal.InsertMessagesWay(Messagesinfo) != null;
            if (x)
            {
                fanhuizhi = "信息发送成功！";
            }
            else
            {
                fanhuizhi = "信息发送失败！请联系管理员";
            }
            return fanhuizhi;
        }
        #endregion


        #region DeleteMessagesWay
        /// <summary>
        /// 根据Id删除单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteMessagesWay(string id)
        {
            int ID = 0;
            bool cg = int.TryParse(id, out ID);
            if (!cg)
            {
                return cg;
            }

            dal = new MessagesDateAccess();
            cg = dal.DeleteMessagesWay(ID) > 0;
            
            return cg;

        }
        #endregion


        #region DeleteMessagesMultiWay
        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteMessagesMultiWay(string[] id)
        {
           
            int[] ID = new int[id.Length];
            int cg = 0;
            for (int i = 0; i < id.Length; i++)
            {
                ID[i] = int.Parse(id[i]);
                if (dal.DeleteMessagesWay(ID[i]) > 0)
                {
                    cg++;
                }
            }

            return cg;

        }
        #endregion
    }
}
