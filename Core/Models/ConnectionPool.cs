using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace ChangTing.Core.Models
{
    /// <summary>
    /// PetaPoco调用类
    /// </summary>
    public static class ConnectionPool
    {
       public static Database db = new Database("ConnString");
    }
}
