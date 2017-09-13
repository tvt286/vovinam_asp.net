using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vovinam.Common;
using Vovinam.Data;
using Vovinam.Models;
using Vovinam.Services;
using Vovinam.WebBackend.Common;

namespace Vovinam.Controllers
{
    public class SchoolController : Controller
    {

        [AuthorizeAdmin(Permissions = new[] { Permission.StudentKB_View, Permission.StudentKA_View, Permission.StudentNL_View })]
        public ActionResult Index(SchoolSearchModel searchModel)
        {
            if (Request.HttpMethod == "GET")
            {
                return View(searchModel);
            }
            var pagedList = SchoolService.Search(searchModel.Name, searchModel.PageSize, searchModel.PageIndex);
            pagedList.SearchModel = searchModel;
            return PartialView("_List", pagedList);
        }

        [AuthorizeAdmin(Permissions = new[] { Permission.StudentKB_Create, Permission.StudentKA_Create, Permission.StudentNL_Create })]
        public ActionResult DetailSchool(int? id)
        {
            var data = new School();

            if (id.HasValue)
            {
                data = SchoolService.Get(id.Value);

            }

            return PartialView("_DetailSchool", data);
        }


        public ActionResult CreateSchool(School model)
        {
            if (model.Id == 0)
            {
                SchoolService.Create(model);
                return
                   Json(
                       new RedirectCommand { Code = ResultCode.Success, Message = "Đã tạo trường thành công!" },
                       JsonRequestBehavior.AllowGet);
            }

            var data = SchoolService.Get(model.Id);
            TryUpdateModel(data);
            SchoolService.Update(data);
            return
                    Json(
                        new RedirectCommand { Code = ResultCode.Success, Message = "Đã cập nhật trường thành công!" },
                        JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteSchool(string SchoolId)
        {
            if (SchoolId.IsEmpty())
            {
                return Json(new RedirectCommand
                {
                    Code = ResultCode.Fail,
                    Message = "Không có trường nào để xóa!",
                }, JsonRequestBehavior.AllowGet);
            }

            SchoolService.Delete(SchoolId);
            return Json(new RedirectCommand
            {
                Code = ResultCode.Success,
                Message = "Đã xóa trường thành công!",
                Url = Url.Action("Index")
            }, JsonRequestBehavior.AllowGet);
        }

	}
}