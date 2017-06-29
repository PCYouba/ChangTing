using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangTing.UI.Viewmodels
{
   public class NewestInfo
    {
        /// <summary>
        /// 专辑ID
        /// </summary>
        public int AlbumId { get; set; }
        /// <summary>
        /// 专辑图片
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 专辑名
        /// </summary>
        public string AlbumName { get; set; }
        /// <summary>
        /// 专歌手名
        /// </summary>
        public string SingerName { get; set; }
    }
}
