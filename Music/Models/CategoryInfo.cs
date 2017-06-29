using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ChangTing.Music.Models
{
    /// <summary>
    /// 音乐类别表
    /// </summary>
    public class CategoryInfo
    {
        /// <summary>
        /// 音乐类别Id
        /// </summary>
        public int CategoryId { get; set; }//Int 非空 唯一约束，主键，自增长，聚焦 

        /// <summary>
        /// 音乐类别名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "音乐类别名不能为空!")]
        [StringLength(25, ErrorMessage = "类别名称长度不能大于25!")]
        public string NickName { get; set; }//varchar(50) 非空
        
        /// <summary>
        /// 排序
        /// </summary>
        public int Number { get; set; }//Int 非空 

        /// <summary>
        /// 打开次数
        /// </summary>
        public int Frequency { get; set; } //bigint  非空 

        /// <summary>
        /// 显示状态
        /// </summary>
        public byte Display { get; set; }//tinyint 非空 默认值0(0正常,1置顶,2不显示)  

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; } //datetime 非空  默认值:getdate()   

    }
}
