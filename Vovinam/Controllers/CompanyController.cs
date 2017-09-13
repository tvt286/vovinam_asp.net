using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vovinam.Data;
using Vovinam.Services;
using Vovinam.Models;
using Vovinam.WebBackend.Common;
using Vovinam.Common;

namespace Kootoro.WebBackend.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index(CompanySearchModel searchModel)
        {
            if (Request.HttpMethod == "GET")
            {
                var user = UserService.GetUserInfo();
                if (!user.IsAdminRoot)
                {
                    return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Index") });
                }
                return View(searchModel);
            }
            var pagedList = CompanyService.Search(searchModel.Name,searchModel.PageSize, searchModel.PageIndex);
            pagedList.SearchModel = searchModel;
            return PartialView("_List", pagedList);
        }
         
        public ActionResult DetailCompany(int? id)
        {
            var data = new Company();

            if (id.HasValue)
            {
                data = CompanyService.Get(id.Value);
                
            }
           
            return PartialView("_DetailCompany", data);
        }

        public ActionResult CreateCompany(Company model)
        {
            if (model.Id == 0)
            {
                CompanyService.CreateCompany(model);
                 return
                    Json(
                        new RedirectCommand { Code = ResultCode.Success, Message = "Đã tạo công ty thành công!"},
                        JsonRequestBehavior.AllowGet);
            }

            var data = CompanyService.Get(model.Id);
            TryUpdateModel(data);
            CompanyService.UpdateCompany(data);
            return
                    Json(
                        new RedirectCommand { Code = ResultCode.Success, Message = "Đã cập nhật công ty thành công!" },
                        JsonRequestBehavior.AllowGet);
        }
    }
}