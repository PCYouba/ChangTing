using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangTing.Users.Models
{
    /// <summary>
    /// 收藏表
    /// </summary>
    public class CollectionInfo
    {
        /// <summary>
        /// 音乐收藏Id
        /// </summary>
        public int CollectionId { get; set; } // Int 非空 唯一约束，主键，自增长，聚焦 
        /// <summary>
        /// 用户Id(Users表主键)
        /// </summary>
        public int UserId { get; set; } //Int 非空 外键，索引，Users表主键 

        /// <summary>
        /// 播放次数
        /// </summary>
        public long Frequency { get; set; }//bigint	非空	默认0	

        /// <summary>
        /// 音乐Id(音乐存储表主键)
        /// </summary>
        public int StorageId { get; set; } //Int 非空 外键，索引，音乐存储表主键 
        /// <summary>
        /// 专辑Id(专辑表主键)
        /// </summary>
        public int AlbumId { get; set; } //Int 非空 外键，索引，专辑表主键 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; } //datetime    非空 默认值：getdate()   
    }
}
