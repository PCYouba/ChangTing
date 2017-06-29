using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;

namespace ChangTing.Core.Models
{
    /// <summary>
    /// 上传音乐
    /// </summary>
     public class filemusic
    {
        public string music(HttpPostedFileBase file_music,string zj,string gs)
        {
            if (file_music==null)
            {
                return null;
            }
            string kuozhanming = Path.GetExtension(file_music.FileName).ToLower();
            switch (kuozhanming)
            {
                case ".mp3":
                    break;
                default:
                    return string.Empty;
            }
            string RndFileName = string.Format("{0}{1}{2}", DateTime.Now.ToString("yyMMdd"), Path.GetRandomFileName().Replace(".", string.Empty), kuozhanming);
            string FullFileUrl = string.Format("{0}/{1}", ModelInfo.musicfile, RndFileName);//存放地址
            string SaveFilePathName = HttpContext.Current.Server.MapPath(FullFileUrl);
            file_music.SaveAs(SaveFilePathName);

            return RndFileName;
        }
    }
}
