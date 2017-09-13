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
    public class ClubController : Controller
    {

        [AuthorizeAdmin(Permissions = new[] { Permission.Club_View })]
        public ActionResult Index(ClubSearchModel searchModel)
        {
            if (Request.HttpMethod == "GET")
            {
                return View(searchModel);
            }
            var pagedList = ClubService.Search(searchModel.Name, searchModel.PageSize, searchModel.PageIndex);
            pagedList.SearchModel = searchModel;
            return PartialView("_List", pagedList);
        }

        [AuthorizeAdmin(Permissions = new[] { Permission.Club_Create })]
        public ActionResult DetailClub(int? id)
        {
            var data = new Club();

            if (id.HasValue)
            {
                data = ClubService.Get(id.Value);

            }

            return PartialView("_DetailClub", data);
        }


        public ActionResult CreateClub(Club model)
        {
            if (model.Id == 0)
            {
                ClubService.CreateClub(model);
                return
                   Json(
                       new RedirectCommand { Code = ResultCode.Success, Message = "Đã tạo câu lạc bộ thành công!" },
                       JsonRequestBehavior.AllowGet);
            }

            var data = ClubService.Get(model.Id);
            TryUpdateModel(data);
            ClubService.UpdateClub(data);
            return
                    Json(
                        new RedirectCommand { Code = ResultCode.Success, Message = "Đã cập nhật câu lạc bộ thành công!" },
                        JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteClub(string clubId)
        {
            if (clubId.IsEmpty())
            {
                return Json(new RedirectCommand
                {
                    Code = ResultCode.Fail,
                    Message = "Không có câu lạc bộ nào để xóa!",
                }, JsonRequestBehavior.AllowGet);
            }

            ClubService.DeleteClub(clubId);
            return Json(new RedirectCommand
            {
                Code = ResultCode.Success,
                Message = "Đã xóa câu lạc bộ thành công!",
                Url = Url.Action("Index")
            }, JsonRequestBehavior.AllowGet);
        }


    }

}