using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ChangTing.Music.Models
{
    /// <summary>
    /// 歌曲表
    /// </summary>
    public class StorageInfo
    {
        /// <summary>
        /// 音乐Id
        /// </summary>
        public int StorageId { get; set; } //Int 非空 唯一约束，主键，自增长，聚焦 

        /// <summary>
        /// 音乐名
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "音乐名不能为空!")]
        [StringLength(25, ErrorMessage = "音乐名称长度不能大于25!")]
        public string RealName { get; set; } //varchar(50) 非空

        /// <summary>
        /// 音乐地址
        /// </summary>
        public string Path { get; set; }//varchar(200) 非空 

        /// <summary>
        /// 音乐的时间
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "请输入音乐时间!")]
        public int Time { get; set; } //Int 非空 

        /// <summary>
        /// 音乐所属类别（Category表主键）
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "请选择所属类别!")]
        public int CategoryId { get; set; } //Int 非空 外键，索引

        /// <summary>
        /// 音乐所属专辑（Album表主键）
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "请选择专辑!")]
        public int AlbumId { get; set; }//Int 非空 外键，索引 

        /// <summary>
        /// 歌手Id（Singer表主键）
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "请选择歌手!")]
        public int SingerId { get; set; } //Int 非空 外键，索引

        /// <summary>
        /// 播放次数
        /// </summary>
        public int Frequency { get; set; } //Bigint  非空 

        /// <summary>
        /// 显示状态(0正常,1置顶,2不显示)
        /// </summary>
        public byte Display { get; set; } //tinyint 非空 默认值0(0正常,1置顶,2不显示)

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime ReleaseTime { get; set; } //datetime 非空 

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; } //datetime 非空  默认值:getdate()   


        /// <summary>
        /// 专辑名称
        /// </summary>
        public string Name { get; set; } //Int 非空 唯一约束，主键，自增长，聚焦 

    }
}
