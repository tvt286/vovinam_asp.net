using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vovinam.Common;
using Vovinam.Common.Enums;
using Vovinam.Data;
using Vovinam.Models;
using Vovinam.Services;
using Vovinam.WebBackend.Common;
using Vovinam.WebBackend.Web;

namespace Vovinam.Controllers
{
    public class StudentController : Controller
    {
        [AuthorizeAdmin(Permissions = new[] { Permission.StudentKB_View, Permission.StudentKA_View, Permission.StudentNL_View })]
        public ActionResult Index(StudentModel searchModel, int id)
        {
            searchModel.ClubId = id;
            ViewBag.ClubId = id;
            if (Request.HttpMethod == "GET")
            {
                var user = UserService.GetUserInfo();
                ViewBag.LevelId = new SelectList(LevelService.GetByCompanyId(user.CompanyId.GetValueOrDefault(0)), "Id", "Name");
                return View(searchModel);
            }

            var pagedList = StudentService.Search(searchModel.Name, searchModel.LevelId, searchModel.ClubId,
                searchModel.PageSize, searchModel.PageIndex);
            pagedList.SearchModel = searchModel;
            return PartialView("_List", pagedList);
        }

        [AuthorizeAdmin(Permissions = new[] { Permission.StudentKB_Create, Permission.StudentKA_Create, Permission.StudentNL_Create })]
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
            ViewBag.SchoolId = new SelectList(SchoolService.GetByCompanyId(user.CompanyId.GetValueOrDefault(0)), "Id", "Name",data.SchoolId);
            ViewBag.LevelId = new SelectList(LevelService.GetByCompanyId(user.CompanyId.GetValueOrDefault(0)), "Id", "Name", data.LevelId);

            return View(data);
        }

        public ActionResult Create(Student data, HttpPostedFileBase fileAttach)
        {

            if (data.Phone != null && !Utility.IsValidPhone(data.Phone))
            {
                return
                Json(
                    new RedirectCommand() { Code = ResultCode.Fail, Message = "Vui lòng kiểm tra lại số điện thoại!" },
                    JsonRequestBehavior.AllowGet);
            }

            string sourceFile = "";
            if (fileAttach != null)
            {
                if (
                    !Directory.Exists(
                        Server.MapPath(string.Format("~/content/Upload/Company{0}/Student", data.CompanyId))))
                {
                    Directory.CreateDirectory(
                        Server.MapPath(string.Format("~/content/Upload/Company{0}/Student", data.CompanyId)));
                }

                var fileName =
                    Utility.ConvertToUnsign(Path.GetFileNameWithoutExtension(fileAttach.FileName))
                        .Replace("&", "")
                        .Replace("?", "")
                        .Replace(" ", "-") + "-" + Guid.NewGuid() + Path.GetExtension(fileAttach.FileName);
                sourceFile = string.Format("~/content/Upload/Company{0}/Student/{1}", data.CompanyId, fileName);
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
                //fileAttach.SaveAs(pathFile);
                Bitmap bitmap = (Bitmap)Bitmap.FromStream(fileAttach.InputStream, true);
                ImageUtils.RewriteImageFix(bitmap, 500, 500, pathFile);
            }

            if (data.Id == 0)
            {
                data.Image = sourceFile;
                StudentService.Create(data);
                return Json(new RedirectCommand
                {
                    Code = ResultCode.Success,
                    Message = "Đã tạo học viên thành công!",
                    Url = Url.Action("Index", new { id = data.ClubId })
                }, JsonRequestBehavior.AllowGet);
            }

            var dataItem = StudentService.Get(data.Id);

            if (sourceFile.NotEmpty())
            {
                if (dataItem.Image.NotEmpty())
                {                   
                    try
                    {
                        System.IO.File.Delete(Server.MapPath(dataItem.Image));
                    }
                    catch (Exception ex)
                    {
                    }

                }
                dataItem.Image = sourceFile;
            }
           
            TryUpdateModel(dataItem);
            StudentService.Update(dataItem);

            return Json(new RedirectCommand
            {
                Code = ResultCode.Success,
                Message = "Đã cập nhật học viên thành công!",
                Url = Url.Action("Index", new { data.Name, id = data.ClubId })
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteStudent(string studentId, int clubId)
        {
            if (studentId.IsEmpty())
            {
                return Json(new RedirectCommand
                {
                    Code = ResultCode.Fail,
                    Message = "Không có học viên nào để xóa!",
                }, JsonRequestBehavior.AllowGet);
            }

            StudentService.DeleteStudent(studentId);
            return Json(new RedirectCommand
            {
                Code = ResultCode.Success,
                Message = "Đã cập xóa học viên thành công!",
                Url = Url.Action("Index", new { id = clubId })
            }, JsonRequestBehavior.AllowGet);
        }
	}
}