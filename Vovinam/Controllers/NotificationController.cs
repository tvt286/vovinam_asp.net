using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vovinam.Common;
using Vovinam.Services;

namespace Vovinam.Controllers
{
    public class NotificationController : Controller
    {
        [HttpGet]
        public ActionResult SetShow()
        {
            NotificationService.UpdateShow();
            return Json(new RedirectCommand
            {
                Code = ResultCode.Success,
                Message = "Cập nhật thành công"
            }, JsonRequestBehavior.AllowGet);
        }
	}
}