using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vovinam.Data;
using System.Data.Entity;

namespace Vovinam.Services
{
    public class NotificationService
    {
        public static List<Notification> GetAll(bool includeSeen = true)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = UserService.GetUserInfo();
                if(includeSeen)
                    return context.Notifications.Include(x => x.User).Where(x => x.UserId != user.Id).OrderByDescending(x => x.Id).ToList();
                else
                    return context.Notifications.Include(x => x.User).Where(x => x.UserId != user.Id && x.Seen == false).OrderByDescending(x => x.Id).ToList();

            }
        }

        public static void Add(Notification data)
        {
            using (var context = new vovinamEntities())
            {
                context.Notifications.Add(data);
                context.SaveChanges();
            }
        }

        public static void Update(int id)
        {
            using (var context = new vovinamEntities())
            {
                var noti = context.Notifications.FirstOrDefault(x => x.Id == id);
                noti.Seen = true;
                context.SaveChanges();
            }
        }

    }
}
