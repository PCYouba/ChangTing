using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ChangTing.Admin.Models
{
     public class AdminInfo
    {
        /// <summary>
        /// 管理员ID
        /// </summary>
        /// 
        public int AdminId { get; set; } //int 非空 唯一约束，主键，自增长，聚焦 
        
        /// <summary>
        /// 管理员登录名
        /// </summary>
        public string LoginName { get; set; } //varchar(25) 非空 索引，唯一

        /// <summary>
        /// 管理员登陆密码
        /// </summary>
        public string Password { get; set; } //varchar(50) 非空 

        /// <summary>
        /// 管理员真实姓名
        /// </summary>
        public string RealName { get; set; } //nchar(15) 非空 

        /// <summary>
        /// 管理员Email
        /// </summary>
        public string Email { get; set; } //varchar(50) 空 

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; } //datetime 非空 默认值：getdate()   

    }
}
