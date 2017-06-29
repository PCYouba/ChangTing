using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;

namespace ChangTing.Core.Models
{
    public class DeleteFile
    {
        public static bool DeleteFileWay(string img)
        {
            try
            {
                File.Delete(HttpContext.Current.Server.MapPath(img));
                return true;
            }
            catch (Exception)
            {
                return false;
            } 
        }
    }
}
