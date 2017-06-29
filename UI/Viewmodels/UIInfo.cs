using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangTing.UI.Viewmodels
{
    /// <summary>
    /// 
    /// </summary>
     public class UIInfo
    {
        /// <summary>
        /// 专辑图片
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 歌曲名称
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 歌手头像
        /// </summary>
        public string HeadPortrait { get; set; }
        /// <summary>
        /// 歌曲的地址
        /// </summary>
        public string Path { get; set; }
        public int CurrentPage { get; set; }

    }
}
