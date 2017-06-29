using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangTing.Users.ViewModels
{
    /// <summary>
    /// 收藏表
    /// </summary>
    public class CollectionViewInfo
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
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 音乐Id(音乐存储表主键)
        /// </summary>
        public int StorageId { get; set; } //Int 非空 外键，索引，音乐存储表主键

        /// <summary>
        /// 音乐地址
        /// </summary>
        public string Path { get; set; } //varchar(200) 非空 
        /// <summary>
        /// 播放次数
        /// </summary>
        public int Frequency { get; set; } //只用于 存储歌曲的播放次数 存储专辑时为-1

        /// <summary>
        /// 音乐姓名(音乐存储表主键)
        /// </summary>
        public string StorageName { get; set; } //Int 非空 外键，索引，音乐存储表主键

        /// <summary>
        /// 专辑Id(专辑表主键)
        /// </summary>
        public int AlbumId { get; set; } //Int 非空 外键，索引，专辑表主键 

        /// <summary>
        /// 专辑名(专辑表主键)
        /// </summary>
        public string AlbumName { get; set; } //Int 非空 外键，索引，专辑表主键 

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; } //datetime    非空 默认值：getdate()   
    }
}
