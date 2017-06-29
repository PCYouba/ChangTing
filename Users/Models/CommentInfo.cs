using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangTing.Users.Models
{
     public class CommentInfo
    {
        /// <summary>
        /// 用户评论Id
        /// </summary>
        public int CommentId { get; set; } //Int 非空 唯一约束，主键，自增长，聚焦 

        /// <summary>
        /// 楼层号
        /// </summary>
        public int Floor { get; set; } //Int 非空 

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; } //Int 非空 外键，索引，Users表主键 

        /// <summary>
        /// 音乐Id
        /// </summary>
        public int StorageId { get; set; }// Int 非空 外键，索引，Storage存储表主键 

        /// <summary>
        /// 评论内容
        /// </summary>
        public string C_Content { get; set; }// varchar(360)    非空

        /// <summary>
        /// 点赞个数
        /// </summary>
        public int Praise { get; set; }// Int 非空 默认值0    

        /// <summary>
        /// 点踩个数
        /// </summary>
        public int Tread { get; set; }//Int 非空  默认值0 

        /// <summary>
        /// 显示状态 默认值0(0正常,1置顶,2不显示)  
        /// </summary>
        public int Display { get; set; } //tinyint 非空 默认值0(0正常,1置顶,2不显示)

        /// <summary>
        /// 回复ID 默认值0(0不是回复, 其他:评论Id) 
        /// </summary>
        public int Reply { get; set; }  // Int 非空  默认值0(0不是回复, 其他:评论Id) 

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }  //datetime 非空  默认值getdate()
    }
}
