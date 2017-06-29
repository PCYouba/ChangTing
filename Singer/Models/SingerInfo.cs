using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ChangTing.Singer.Models
{
    /// <summary>
    /// 歌手信息表  
    /// </summary>
    public class SingerInfo
    {
        /// <summary>
        /// 歌手Id
        /// </summary>
        public int SingerId { get; set; }//Int     非空 唯一约束，主键，自增长，聚焦 

        /// <summary>
        /// 歌手姓名
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "歌手姓名不能为空!")]
        [StringLength(25, ErrorMessage = "名字长度不能大于25!")]
        public string Name { get; set; } //varchar(50) 非空 

        /// <summary>
        /// 歌手头像
        /// </summary>
        public string HeadPortrait { get; set; } //varchar(200) 空 

        /// <summary>
        /// 歌手自我介绍
        /// </summary>
        [StringLength(36, ErrorMessage = "自我介绍不得超过36位描述")]
        public string Introduce { get; set; } //varchar(72) 空
        

        /// <summary>
        /// 歌手性别
        /// </summary>
        public byte Gender { get; set; } //tinyint 非空  默认值：0 （0保密，1男，2女）

        /// <summary>
        /// 歌手年龄
        /// </summary>
        [StringLength(6, ErrorMessage = "年龄不得超过6位描述")]
        public string Age { get; set; } //varchar(12)  空 

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }  //datetime 非空  默认值:getdate()   

    }
}
