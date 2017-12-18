using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vovinam.Common;
using Vovinam.Models;
using Vovinam.Services;
using Vovinam.WebBackend.Common;

namespace Vovinam.Controllers
{
    public class ExaminationController : Controller
    {
        [AuthorizeAdmin(Permissions = new[] { Permission.QuanLyKhoaThi_All})]
        public ActionResult Index(ExaminationSearchModel searchModel)
        {
            if (Request.HttpMethod == "GET")
            {
                var user = UserService.GetUserInfo();
                if (user.CompanyId.HasValue)
                {
                    ViewBag.ExaminationId = new SelectList(ExaminationService.GetAll(user.CompanyId.GetValueOrDefault(0)), "Id", "Name", searchModel.ExaminationId);
                }
                return View(searchModel);
            }
            var pagedList = ExaminationService.Search(searchModel.ExaminationId, searchModel.PageSize, searchModel.PageIndex);
            pagedList.SearchModel = searchModel;
            return PartialView("_List", pagedList);
        }

        public ActionResult DeleteExamination(string ExaminationId)
        {
            if (ExaminationId.IsEmpty())
            {
                return Json(new RedirectCommand
                {
                    Code = ResultCode.Fail,
                    Message = "Không có khóa thi nào để xóa!",
                }, JsonRequestBehavior.AllowGet);
            }

            ExaminationService.Delete(ExaminationId);
            return Json(new RedirectCommand
            {
                Code = ResultCode.Success,
                Message = "Đã xóa khóa thi thành công!",
                Url = Url.Action("Index")
            }, JsonRequestBehavior.AllowGet);
        }
	}
}