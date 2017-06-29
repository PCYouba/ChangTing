using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangTing.Users.Models
{
    /// <summary>
    /// 信息表
    /// </summary>
     public class MessagesInfo
    {
        /// <summary>
        /// 信息Id
        /// </summary>
        public int MessageId { get; set; } //Int     非空 唯一约束，主键，自增长，聚焦 

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } //varchar(50) 非空 

        /// <summary>
        /// 内容
        /// </summary>
        public string M_Content { get; set; }// nvarchar(360)   空 

        /// <summary>
        /// 创建者
        /// </summary>
        public int FoundId { get; set; }//int 空       


        /// <summary>
        /// 收件者
        /// </summary>
        public int UserId { get; set; }//int 非空  UserID
         
        /// <summary>
        /// 等级 默认值0(0系统通告,1系统消息,2普通消息)  
        /// </summary>
        public byte GradeLevel { get; set; } //tinyint 非空  默认值0(0系统通告,1系统消息,2普通消息)   

        /// <summary>
        /// 状态  默认值0 (0新消息未查看  1查看过)
        /// </summary>
        public byte Display { get; set; } //tinyint 空   默认值0 (0新消息未查看  1已查看)  状态 

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; } //datetime  非空 默认值:getdate()   
    }
}
