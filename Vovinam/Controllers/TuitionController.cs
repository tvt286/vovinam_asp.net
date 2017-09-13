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
    public class TuitionController : Controller
    {
        [AuthorizeAdmin(Permissions = new[] { Permission.TuitionKB_View, Permission.TuitionKA_View, Permission.TuitionNL_View})]
        public ActionResult Index(TuitionSearchModel searchModel, int id)
        {
            searchModel.ClubId = id;
            ViewBag.ClubId = id;

            if (Request.HttpMethod == "GET")
            {
                var user = UserService.GetUserInfo();
                ViewBag.LevelId = new SelectList(LevelService.GetByCompanyId(user.CompanyId.GetValueOrDefault(0)), "Id", "Name");
                return View(searchModel);
            }

            var pagedList = TuitionService.Search(searchModel.Name, searchModel.ClubId, searchModel.HasCollected, searchModel.UnCollected,
                searchModel.PageSize, searchModel.PageIndex);
            pagedList.SearchModel = searchModel;
            return PartialView("_List", pagedList);
        }

        [AuthorizeAdmin(Permissions = new[] { Permission.TuitionKB_Edit, Permission.TuitionKA_Edit, Permission.TuitionNL_Edit })]
        public ActionResult Detail(int? id, int clubId)
        {
            var user = UserService.GetUserInfo();
            ViewBag.User = user;
            var data = new Student
            {
                CompanyId = user.CompanyId.GetValueOrDefault(0),
                ClubId = clubId
            };
            if (id != 0)
            {
                data = StudentService.Get(id.Value);
            }

            return View(data);
        }

        public ActionResult Update(Student data)
        {

            var dataItem = StudentService.Get(data.Id);
            TryUpdateModel(dataItem);
            StudentService.Update(dataItem);

            return Json(new RedirectCommand
            {
                Code = ResultCode.Success,
                Message = "Đã cập nhật học phí thành công!",
                Url = Url.Action("Index", new { data.Name, id = data.ClubId })
            }, JsonRequestBehavior.AllowGet);
        }

    }
}