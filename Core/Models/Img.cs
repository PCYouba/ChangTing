using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ChangTing.Core.Common;
using System.Web;
using System.Data;

namespace ChangTing.Core.Models
{
     public class Img
    {
        public string img(HttpPostedFileBase file_upload)
        {
            string kuozhanming = Path.GetExtension(file_upload.FileName).ToLower();
            switch (kuozhanming)
            {
                case ".jpg":
                    break;
                case ".png":
                    break;
                default:
                    return string.Empty;
            }

            string RndFileName = string.Format("{0}{1}{2}", DateTime.Now.ToString("yyMMdd"), Path.GetRandomFileName().Replace(".", string.Empty), kuozhanming);
            string FullFileUrl = string.Format("{0}/{1}", ModelInfo.imgfile, RndFileName);//存放地址
            string SaveFilePathName = HttpContext.Current.Server.MapPath(FullFileUrl);
            if (!ImageOperation.IntelligentSaveImageFile(file_upload.InputStream, SaveFilePathName, 350, true, ModelInfo.WaterMark, new System.Drawing.Size(350, 350)))
            {
                file_upload.SaveAs(FullFileUrl);
            }

            return FullFileUrl;
        }
    }
}
