using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ChangTing.Core.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Admin", action = "index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
             name: "Singer",
             url: "{controller}/{action}",
             defaults: new { controller = "Singer", action = "SingerList_Admin" },
             namespaces: new[] { "ChangTing.Singer.Controllers" }
            );
            routes.MapRoute(
             name: "SingerAdd",
             url: "{controller}/{action}/{singerid}",
             defaults: new { controller = "Singer", action = "SingerListAdd_Admin" },
             constraints: new { singerid = @"^/d+$" },
             namespaces: new[] { "ChangTing.Singer.Controllers" }
            );

            routes.MapRoute(
            name: "AlbumList",
            url: "{controller}/{action}/{singerid}",
            defaults: new { controller = "Singer", action = "AlbumList_Admin" },
            constraints: new { singerid = @"^/d+$" },
            namespaces: new[] { "ChangTing.Singer.Controllers" }
           );

            routes.MapRoute(
           name: "CollectionList",
           url: "{controller}/{action}/{singerid}",
           defaults: new { controller = "Users", action = "CollectionList_Admin" },
           constraints: new { singerid = @"^/d+$" },
           namespaces: new[] { "ChangTing.Users.Controllers" }
          );
            routes.MapRoute(
        name: "RuzhuSinger",
        url: "{controller}/{action}/{page}",
        defaults: new { controller = "Index", action = "RuzhuSinger", page=0 },
        constraints: new { singerid = @"^/d+$" },
        namespaces: new[] { "ChangTing.Index.Controllers" }
       );

        routes.MapRoute(
            name: "SingerXQ",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Index", action = "SingerXQ", id = 0 },
            constraints: new { id = @"^/d+$" },
            namespaces: new[] { "ChangTing.Index.Controllers" }
            );
            
        routes.MapRoute(
         name: "SingerIsAlbum",
         url: "{controller}/{action}/{singerid}",
         defaults: new { controller = "Index", action = "SingerIsAlbum", singerid = 0 },
         constraints: new { singerid = @"^/d+$" },
         namespaces: new[] { "ChangTing.Index.Controllers" }
        );

            
        }
    }
}
