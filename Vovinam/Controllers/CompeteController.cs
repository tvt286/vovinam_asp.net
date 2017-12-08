using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vovinam.Common;
using Vovinam.Data;
using Vovinam.Models;
using Vovinam.Services;

namespace Vovinam.Controllers
{
    public class CompeteController : Controller
    {

        public ActionResult Index(CompeteSearchModel searchModel, int id)
        {
            ViewBag.Gender = id;
            if (!searchModel.ExaminationId.HasValue)
                searchModel.ExaminationId = ExaminationService.GetLastId().Id;
            if (Request.HttpMethod == "GET")
            {
                var user = UserService.GetUserInfo();
                if (user.CompanyId.HasValue)
                {
                    ViewBag.ExaminationId = new SelectList(ExaminationService.GetAll(user.CompanyId.GetValueOrDefault(0)), "Id", "Name", ExaminationService.GetLastId().Id);
                }

                return View(searchModel);
            }

            var pagedList = CompeteService.Search(searchModel.ExaminationId, id, searchModel.PageSize, searchModel.PageIndex);
            pagedList.SearchModel = searchModel;
            return PartialView("_List", pagedList);
        }

        public ActionResult ExportExcel(int ExaminationId, int Gender)
        {
            var user = UserService.GetUserInfo();
            List<Compete> doikhang = CompeteService.Gets(user.CompanyId, ExaminationId, Gender);

            string filePath = "";
            if (Gender == 1)
                filePath = Server.MapPath(Url.Content("~/Content/Upload/Excel/doikhangnam.xlsx"));
            else
                filePath = Server.MapPath(Url.Content("~/Content/Upload/Excel/doikhangnu.xlsx"));

            var existingFile = new FileInfo(filePath);
            using (var package = new ExcelPackage(existingFile))
            {
                var workBook = package.Workbook;
                ExcelWorksheet currentWorksheet = workBook.Worksheets.First();
                currentWorksheet.Cells["A:XFD"].Style.Font.Name = "Times New Roman";
                currentWorksheet.Cells["A:XFD"].Style.Font.Size = 12;

                int rowStarMale = 4;
                int rowEndMale = rowStarMale;
                int Code = 1;
                foreach (var item in doikhang)
                {

                    // thí sinh 1;
                    currentWorksheet.Cells[string.Format("A{0}", rowStarMale)].Value = item.LevelUp.Stt;
                    currentWorksheet.Cells[string.Format("B{0}", rowStarMale)].Value = item.LevelUp.Name;
                    currentWorksheet.Cells[string.Format("C{0}", rowStarMale)].Value = item.LevelUp.Club.Name;
                    currentWorksheet.Cells[string.Format("D{0}", rowStarMale)].Value = item.LevelUp.Level.Name;
                    currentWorksheet.Cells[string.Format("E{0}", rowStarMale)].Value = item.LevelUp.Weight;
                    currentWorksheet.Cells[string.Format("F{0}", rowStarMale)].Value = "Đỏ";

                    // thí sinh 2
                    currentWorksheet.Cells[string.Format("A{0}", rowStarMale + 1)].Value = item.LevelUp1.Stt;
                    currentWorksheet.Cells[string.Format("B{0}", rowStarMale + 1)].Value = item.LevelUp1.Name;
                    currentWorksheet.Cells[string.Format("C{0}", rowStarMale + 1)].Value = item.LevelUp1.Club.Name;
                    currentWorksheet.Cells[string.Format("D{0}", rowStarMale + 1)].Value = item.LevelUp1.Level.Name;
                    currentWorksheet.Cells[string.Format("E{0}", rowStarMale + 1)].Value = item.LevelUp1.Weight;
                    currentWorksheet.Cells[string.Format("G{0}", rowStarMale + 1)].Value = "Xanh";

                    currentWorksheet.Cells[string.Format("H{0}:H{1}", rowStarMale, rowStarMale + 1)].Merge = true;
                    currentWorksheet.Cells[string.Format("H{0}:H{1}", rowStarMale, rowStarMale + 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    currentWorksheet.Cells[string.Format("H{0}:H{1}", rowStarMale, rowStarMale + 1)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    currentWorksheet.Cells[string.Format("H{0}:H{1}", rowStarMale, rowStarMale + 1)].Value = Code;

                    currentWorksheet.Cells[string.Format("I{0}:I{1}", rowStarMale, rowStarMale + 1)].Merge = true;
                    currentWorksheet.Cells[string.Format("I{0}:I{1}", rowStarMale, rowStarMale + 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    currentWorksheet.Cells[string.Format("I{0}:I{1}", rowStarMale, rowStarMale + 1)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    currentWorksheet.Cells[string.Format("I{0}:I{1}", rowStarMale, rowStarMale + 1)].Value = Code;
                    Code++;
                    rowStarMale = rowStarMale + 2;
                }

                if (doikhang.Count != 0)
                {
                    currentWorksheet.Cells[string.Format("A{0}:I{1}", rowEndMale, rowStarMale - 1)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:I{1}", rowEndMale, rowStarMale - 1)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:I{1}", rowEndMale, rowStarMale - 1)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:I{1}", rowEndMale, rowStarMale - 1)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }

                currentWorksheet.Cells["A:I"].AutoFitColumns();
                Response.Clear();

                if (Gender == 1)
                    Response.AddHeader("content-disposition", "attachment;  filename=" + "DoiKhangNam.xlsx");
                else
                    Response.AddHeader("content-disposition", "attachment;  filename=" + "DoiKhangNu.xlsx");

                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(package.GetAsByteArray());
                Response.End();
            }
            return null;
        }

        [HttpPost]
        public ActionResult Detail(int id)
        {
            Compete data = CompeteService.Get(id);
            return PartialView("_Detail", data);
        }

        [HttpPost]
        public ActionResult Edit(Compete model)
        {
            var data = CompeteService.Get(model.Id);
            var user = UserService.GetUserInfo();

            if (model.LevelUp.DoiKhang.Point != data.LevelUp.DoiKhang.Point)
            {
                var levelup = LevelUpService.Get(model.LevelUpId1);
                LevelUpService.UpdateDoiKhang(levelup, model.LevelUp);
            }

            if (model.LevelUp1.DoiKhang.Point != data.LevelUp1.DoiKhang.Point)
            {
                var levelup = LevelUpService.Get(model.LevelUpId2);
                LevelUpService.UpdateDoiKhang(levelup, model.LevelUp1);
            }
            var levelup1 = LevelUpService.Get(model.LevelUpId1);
            var levelup2 = LevelUpService.Get(model.LevelUpId2);

            LevelUpService.setKetQua(levelup1);
            LevelUpService.setKetQua(levelup2);
            return
                   Json(
                       new RedirectCommand
                       {
                           Code = ResultCode.Success,
                           Message = "Cập nhật điểm thành công!",
                           Url = Url.Action("Index", new { Id = (levelup1.Gender == Gender.Male) ? 1 : 2 })
                       },
                       JsonRequestBehavior.AllowGet);
        }
    }
}