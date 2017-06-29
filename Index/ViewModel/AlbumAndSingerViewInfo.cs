using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangTing.Index.ViewModel
{
     public class AlbumAndSingerViewInfo
    {
        /// <summary>
        /// 专辑Id
        /// </summary>
        public int AlbumId { get; set; } //Int 非空 唯一约束，主键，自增长，聚焦 

        /// <summary>
        /// 标题名称(专辑名)
        /// </summary>
        public string AlbumName { get; set; } //varchar(50) 非空

        /// <summary>
        /// 标题宣传图像
        /// </summary>
        public string Image { get; set; } //varchar(200) 空

        /// <summary>
        /// 专辑介绍
        /// </summary>
        public string Introduce { get; set; } //varchar(2000)   空 

        /// <summary>
        /// 歌手Id
        /// </summary>
        public int SingerId { get; set; } //Int 非空 外键，索引，Singer表主键
        
        /// <summary>
        /// 歌手姓名
        /// </summary>
        public string SingerName { get; set; } //varchar(50) 非空 

        /// <summary>
        /// 歌手自我介绍
        /// </summary>
        public string SingerIntroduce { get; set; } //varchar(72) 空
        
        /// <summary>
        /// 歌手年龄
        /// </summary>
        public string Age { get; set; } //varchar(12)  空 

        /// <summary>
        /// 发行时间
        /// </summary>
        public string Issue { get; set; } //varchar(20) 非空 

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }//datetime 非空 默认值:getdate()

    }
}
