using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vovinam.Areas.Api.Common;
using Vovinam.Models;
using Vovinam.Services;

namespace Vovinam.Areas.Api.Controllers
{
    public class NotificationController : ApiController
    {
        public IEnumerable<NotificationModel> GetAll()
        {
            var noti = NotificationService.GetAll();
            var results = new List<NotificationModel>();
            var count = noti.Count(x => x.Seen == false);

            return results = noti.Select(x => new NotificationModel
            {
                Id = x.Id,
                Content = x.Content,
                UserName = x.User.FullName,
                Image = x.User.Image,
                TimeCreate = x.TimeCreate,
                Seen = x.Seen.Value,
                Show = x.Show,
                CountNoti = count
            }).ToList();
        }

        [HttpGet]
        public object SetSeen()
        {
            var noti = NotificationService.GetAll(false);
            foreach (var item in noti)
            {
                NotificationService.Update(item.Id);
            }
            return Json(new Vovinam.Areas.Api.Common.JsonResult(ResultCode.NoContent, Constants.MESSAGE.NO_CONTENT));
        }
    }
}
