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
                var result = new List<Notification>();
                if(includeSeen)
                    result = context.Notifications.Include(x => x.User).Where(x => x.UserId != user.Id).OrderByDescending(x => x.Id).ToList();
                else
                    result = context.Notifications.Include(x => x.User).Where(x => x.UserId != user.Id && x.Seen == false).OrderByDescending(x => x.Id).ToList();
               
                return result;
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

        public static void UpdateShow()
        {
            using (var context = new vovinamEntities())
            {
                var user = UserService.GetUserInfo();
                var noti = context.Notifications.Where(x => x.UserId != user.Id && x.Show == false).ToList();
                foreach (var item in noti)
                {
                    item.Show = true;
                }
                context.SaveChanges();
            }
        }
    }
}
