using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ChangTing.Singer.Models
{
    /// <summary>
    /// 专辑表
    /// </summary>
    public class AlbumInfo
    {
        /// <summary>
        /// 专辑Id
        /// </summary>
        public int AlbumId { get; set; } //Int 非空 唯一约束，主键，自增长，聚焦 

        /// <summary>
        /// 标题名称(专辑名)
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "专辑名不能为空!")]
        [StringLength(25, ErrorMessage = "名字长度不能大于25!")]
        public string Name { get; set; } //varchar(50) 非空

        /// <summary>
        /// 标题宣传图像
        /// </summary>
        public string Image { get; set; } //varchar(200) 空
        /// <summary>
        /// 专辑介绍
        /// </summary>
        [StringLength(1000, ErrorMessage = "专辑介绍不得超过1000位描述!")]
        public string Introduce { get; set; } //varchar(2000)   空 

        /// <summary>
        /// 歌手Id
        /// </summary>
        [Range(1,int.MaxValue, ErrorMessage = "请正确选择存在的歌手!")]
        public int SingerId { get;set; } //Int 非空 外键，索引，Singer表主键

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
