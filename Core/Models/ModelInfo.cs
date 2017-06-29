using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;
using System.Configuration;

namespace ChangTing.Core.Models
{
    public static class ModelInfo
    {
        /// <summary>
        /// 链接串建立
        /// </summary>
        public static readonly Database db = new Database("ConnString");

        #region 
        /// <summary>
        /// img默认显示
        /// </summary>
        public static readonly string imgmoren = "../../Content/Hui/img/photo.png";

        /// <summary>
        /// img存放地址
        /// </summary>
        public static readonly string imgfile = ConfigurationManager.AppSettings["imgfile"];

        /// <summary>
        /// music存放地址
        /// </summary>
        public static readonly string musicfile = ConfigurationManager.AppSettings["musicfile"];
        
        /// <summary>
        /// 水印获取
        /// </summary>
        public static readonly string WaterMark = ConfigurationManager.AppSettings["WaterMark"];

        /// <summary>
        /// 默认播放次数
        /// </summary>
        public static readonly int Frequency = int.Parse(ConfigurationManager.AppSettings["Frequency"]);
        #endregion
    }
}
