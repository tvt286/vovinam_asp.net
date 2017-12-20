using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Vovinam.Services;
using Vovinam.Data;
using System.Web.Helpers;
using Newtonsoft.Json;
using Vovinam.Models;

namespace Vovinam.Hubs
{
    public class NotificationHubs : Hub
    {
        //public void GetAll()
        //{
        //    Clients.All.getAll(NotificationService.GetAll());
        //}

        public static void Add(int userId, string content)
        {
            var noti = new Notification
            {
                UserId = userId,
                TimeCreate = DateTime.Now,
                Content = content,
                Seen = false
            };
            NotificationService.Add(noti);
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHubs>();
            context.Clients.All.displayNotification();
        }
    }
}