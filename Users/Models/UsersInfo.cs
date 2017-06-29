using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangTing.Users.Models
{
    /// <summary>
    /// 用户表
    /// </summary>
     public class UsersInfo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }//[int] 唯一约束，主键，自增长，聚焦 NOT NULL,

        /// <summary>
        /// 用户账号(登陆用)
        /// </summary>
        public string LoginName { get; set; } //[varchar](25) NOT NULL,

        /// <summary>
        /// 用户密码
        /// </summary>
        public string PassWord { get; set; } //[varchar](25) NOT NULL,

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; } //[nvarchar](15) NOT NULL,

        /// <summary>
        /// 头像 存储地址 href直接存放
        /// </summary>
        public string HeadPortrait { get; set; } //[varchar](200) NULL,

        /// <summary>
        /// 用户自我介绍 Max180字
        /// </summary>
        public string Introduce { get; set; }//[varchar](360) NULL,

        /// <summary>
        /// 用户性别 默认值：0 （0保密，1男，2女）	
        /// </summary>
        public byte Gender { get; set; }// [tinyint] NOT NULL 默认0

        /// <summary>
        /// 用户生日
        /// </summary>
	    public DateTime Birthday { get; set; } //  [datetime]  NULL,

        /// <summary>
        /// 用户所在地区 “省，市”+”，分隔符”+”市，区”	存储方式
        /// </summary>
        public string Region { get; set; } //[nvarchar](30) NULL,

        /// <summary>
        /// 当前用户是否可用   默认：0（0可用，1不可用）	
        /// </summary>
        public byte Available { get; set; }// [tinyint]  NOT NULL 

        /// <summary>
        /// 创建时间 默认值：getdate()
        /// </summary>
        public DateTime CreateDate { get; set; }// [datetime]  NOT NULL
    }
}
