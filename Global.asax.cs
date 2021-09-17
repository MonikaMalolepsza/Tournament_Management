using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Tournament_Management.ControllerNS;
using Tournament_Management.DBHelper;

namespace Tournament_Management
{
    public class Global : HttpApplication
    {
        //add the list of controllers instead of one
        private static Controller _controller;

        private static UserController _uController;

        public static Controller Controller { get => _controller; set => _controller = value; }
        public static UserController UserController { get => _uController; set => _uController = value; }

        private void Application_Start(object sender, EventArgs e)
        {
            // Code, der beim Anwendungsstart ausgeführt wird
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DatabaseCreator.GenerateDatabase();

            RouteTable.Routes.MapHttpRoute(
                name: "WebApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = System.Web.Http.RouteParameter.Optional }
            );
            Controller = new Controller();
            UserController = new UserController();
        }
    }
}