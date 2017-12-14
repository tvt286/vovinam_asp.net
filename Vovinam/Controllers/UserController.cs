using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vovinam.Services;
using Vovinam.Data;
using Vovinam.Common;
using Vovinam.Common.Enums;
using Vovinam.WebBackend.Web;
using System.IO;
using Vovinam.WebBackend.Common;
using Vovinam.Models;

namespace Vovinam.Controllers
{
    public class UserController : Controller
    {

        public ActionResult Index(UserViewModel searchModel)
        {

            if (Request.HttpMethod == "GET")
            {
                var user = UserService.GetUserInfo();
                if (!(UserPermission.Has(Permission.User_View) || user.IsAdminRoot || user.IsAdminCompany))
                {
                    return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Index") });
                }

                if (user.CompanyId.HasValue)
                {
                    ViewBag.GroupId = new SelectList(GroupPermissionService.GetAll(user.CompanyId.GetValueOrDefault(0)), "GroupId", "Name");
                }

                return View(searchModel);
            }
            var pagedList = UserService.Search(searchModel.UserName, searchModel.FullName, searchModel.Phone, searchModel.Status, searchModel.GroupId, searchModel.PageSize, searchModel.PageIndex);
            pagedList.SearchModel = searchModel;
            return PartialView("_List", pagedList);
        }

        public ActionResult Detail(int? id)
        {
            var user = UserService.GetUserInfo();

            if (!(UserPermission.Has(Permission.User_Create) || user.IsAdminRoot || user.IsAdminCompany))
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Index") });
            }

            ViewBag.User = user;

            var model = new User
            {
                CompanyId = user.CompanyId,
                Groups = new List<Group>()
            };


            if (id != 0)
            {
                model = UserService.Get(id.Value, true, true);
            }

            if (user.IsAdminRoot)
            {
                ViewBag.CompanyId = new SelectList(CompanyService.GetAll(), "Id", "Name");
            }
            else
            {
                ViewBag.Groups = GroupPermissionService.GetAll(user.CompanyId.GetValueOrDefault(0));
            }


            return View(model);
        }

        public ActionResult Create(User model, HttpPostedFileBase image, int[] selectedGroup, string selectedPermissions, HttpPostedFileBase fileAttach)
        {

            if (model.Phone != null && !Utility.IsValidPhone(model.Phone))
            {
                return
                Json(
                    new RedirectCommand() { Code = ResultCode.Fail, Message = "Vui lòng kiểm tra lại số điện thoại!" },
                    JsonRequestBehavior.AllowGet);
            }

            int[] permissionIdList = string.IsNullOrWhiteSpace(selectedPermissions) ? new int[0]
                    : selectedPermissions.Split(',').Select(x => int.Parse(x)).ToArray();

            var user = UserService.GetUserInfo();

            string sourceFile = "";
            if (fileAttach != null)
            {
                if (!Directory.Exists(Server.MapPath(string.Format("~/content/Upload/Company{0}/User", model.CompanyId))))
                {
                    Directory.CreateDirectory(Server.MapPath(string.Format("~/content/Upload/Company{0}/User", model.CompanyId)));
                }

                var fileName = Utility.ConvertToUnsign(Path.GetFileNameWithoutExtension(fileAttach.FileName)).Replace("&", "").Replace("?", "").Replace(" ", "-") + "-" + Guid.NewGuid() + Path.GetExtension(fileAttach.FileName);
                sourceFile = string.Format("~/content/Upload/Company{0}/User/{1}", model.CompanyId, fileName);
                var pathFile = Server.MapPath(sourceFile);
                var checkFile = UploadHelper.CheckImageUpload(fileAttach);
                if (checkFile == UploadFileStatus.NotSupportExtension)
                {
                    return Json(new CommandResult
                    {
                        Code = ResultCode.Fail,
                        Message = "Chỉ được up file hình!"
                    }, JsonRequestBehavior.AllowGet);
                }

                if (checkFile == UploadFileStatus.OverLimited)
                {
                    return Json(new CommandResult
                    {
                        Code = ResultCode.Fail,
                        Message = "Chỉ được up file 5MB!"
                    }, JsonRequestBehavior.AllowGet);
                }

                fileAttach.SaveAs(pathFile);

            }

            var result = new RedirectCommand();
            if (model.Id == 0)
            {
                if (model.Password.IsEmpty())
                {
                    return Json(new CommandResult
                    {
                        Code = ResultCode.Fail,
                        Message = "Vui lòng nhập mật khẩu!"
                    }, JsonRequestBehavior.AllowGet);
                }

                model.Image = sourceFile;
                if (model.Password.NotEmpty())
                {
                    model.Password = Encryptor.MD5Hash(model.Password);
                }

                result = UserService.CreateUser(model, selectedGroup, permissionIdList);
            }
            else
            {
                AuthorizeService.ClearCache(HttpContext.User.Identity.Name);
                var data = UserService.Get(model.Id, true, true);
                var tempPass = data.Password;
                TryUpdateModel(data);

                if (sourceFile.NotEmpty())
                {
                    if (data.Image.NotEmpty())
                    {
                        try
                        {
                            System.IO.File.Delete(Server.MapPath(data.Image));
                        }
                        catch (Exception ex)
                        {
                        }

                    }
                    data.Image = sourceFile;
                }

                if (data.Password.IsEmpty())
                {
                    data.Password = tempPass;
                }
                else
                {
                    data.Password = Encryptor.MD5Hash(model.Password);
                }

                result = UserService.UpdateUser(data, selectedGroup, permissionIdList);

            }

            if (result.Code == ResultCode.Success)
            {
                result.Url = Url.Action("Index", new { model.UserName });
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteUser(string userId)
        {
            if (userId.IsEmpty())
            {
                return Json(new RedirectCommand
                {
                    Code = ResultCode.Fail,
                    Message = "Không có quản trị viên nào để xóa!",
                }, JsonRequestBehavior.AllowGet);
            }

            UserService.DeleteUser(userId);
            return Json(new RedirectCommand
            {
                Code = ResultCode.Success,
                Message = "Đã xóa quản trị viên thành công!",
                Url = Url.Action("Index")
            }, JsonRequestBehavior.AllowGet);
        }

    }
}