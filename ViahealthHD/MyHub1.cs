using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ViahealthHD.Controllers.API;

namespace ViahealthHD
{
    public class MyHub1 : Hub
    {
        public static void newdatatoupdate()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MyHub1>();

            context.Clients.All.updatedClients();
            
        }

    }
}