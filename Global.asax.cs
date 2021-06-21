using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public static Controller Controller { get => _controller; set => _controller = value; }

        private void Application_Start(object sender, EventArgs e)
        {
            // Code, der beim Anwendungsstart ausgeführt wird
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

           // DatabaseCreator.GenerateDatabase();

            Controller = new Controller();
        }
    }
}