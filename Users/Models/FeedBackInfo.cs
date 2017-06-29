using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangTing.Users.Models
{
     public class FeedBackInfo
    {
        /// <summary>
        /// 反馈Id
        /// </summary>
        public int FeedBackId { get; set; } //Int     非空 唯一约束，主键，自增长，聚焦 
        
        /// <summary>
        /// 反馈内容
        /// </summary>
        public string S_Content { get; set; } //varchar(360)    非空 

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; } //Int 非空 外键，索引，Users表主键 
       
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; } //varchar(20) 非空 

        /// <summary>
        /// 是否解决 默认值0(0未解决，1解决)
        /// </summary>
        public byte IsSolve { get; set; } //tinyint 非空   

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; } //datetime 非空  默认值:getdate()   
    }
}
