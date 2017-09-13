using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vovinam.Common;
using Vovinam.Data;
using Vovinam.Models;
using Vovinam.Services;
using PagedList;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Vovinam.Controllers
{
    public class LevelUpController : Controller
    {

        public ActionResult Index(LevelUpSearchModel searchModel)
        {
            if (Request.HttpMethod == "GET")
            {
                var examination = ExaminationService.GetLastId();
                if (examination != null)
                    searchModel.ExaminationId = examination.Id;
                var user = UserService.GetUserInfo();
                if (user.CompanyId.HasValue)
                {
                    ViewBag.ExaminationId = new SelectList(ExaminationService.GetAll(user.CompanyId.GetValueOrDefault(0)), "Id", "Name", (examination != null) ? examination.Id : searchModel.ExaminationId);
                    ViewBag.LevelId = new SelectList(LevelService.GetByCompanyId(user.CompanyId.GetValueOrDefault(0)), "Id", "Name");
                }

                return View(searchModel);
            }
            var pagedList = LevelUpService.Search(searchModel.Name, searchModel.LevelId, searchModel.ExaminationId, searchModel.KetQua, searchModel.PageSize, searchModel.PageIndex);
            pagedList.SearchModel = searchModel;
            return PartialView("_List", pagedList);
        }

        public ActionResult GetResult(int ExaminationId, int CompanyId)
        {
            List<LevelUp> danhsach = new List<LevelUp>();
            List<LevelUpHistory> data = LevelUpHistoryService.Gets(2253);

            // THỦ KHOA
            // tự vệ nam
            var tuveNam = LevelUpService.GetThuKhoa(CompanyId, ExaminationId, 1, Gender.Male);
            if (tuveNam != null)
            {
                foreach (var item in tuveNam)
                {
                    item.KetQua = KetQua.ThuKhoa;
                    LevelUpService.setThuKhoa(item.Id);
                }
                danhsach.AddRange(tuveNam);
            }
            // tự vệ nữ
            var tuveNu = LevelUpService.GetThuKhoa(CompanyId, ExaminationId, 1, Gender.Female);
            if (tuveNu != null)
            {
                foreach (var item in tuveNu)
                {
                    item.KetQua = KetQua.ThuKhoa;
                    LevelUpService.setThuKhoa(item.Id);
                }

                danhsach.AddRange(tuveNu);
            }
            // lam đai nam
            var lamdaiNam = LevelUpService.GetThuKhoa(CompanyId, ExaminationId, 2, Gender.Male);
            if (lamdaiNam != null)
            {
                foreach (var item in lamdaiNam)
                {
                    item.KetQua = KetQua.ThuKhoa;
                    LevelUpService.setThuKhoa(item.Id);
                }

                danhsach.AddRange(lamdaiNam);
            }
            // lam đai nữ
            var lamdaiNu = LevelUpService.GetThuKhoa(CompanyId, ExaminationId, 2, Gender.Female);
            if (lamdaiNu != null)
            {
                foreach (var item in lamdaiNu)
                {
                    item.KetQua = KetQua.ThuKhoa;
                    LevelUpService.setThuKhoa(item.Id);
                }
                danhsach.AddRange(lamdaiNu);
            }


            // lam đai i nam
            var lamdaiINam = LevelUpService.GetThuKhoa(CompanyId, ExaminationId, 3, Gender.Male);
            if (lamdaiINam != null)
            {
                foreach (var item in lamdaiINam)
                {
                    item.KetQua = KetQua.ThuKhoa;
                    LevelUpService.setThuKhoa(item.Id);
                }
                danhsach.AddRange(lamdaiINam);
            }
            // lam đai i nữ
            var lamdaiINu = LevelUpService.GetThuKhoa(CompanyId, ExaminationId, 3, Gender.Female);
            if (lamdaiINu != null)
            {
                foreach (var item in lamdaiINu)
                {
                    item.KetQua = KetQua.ThuKhoa;
                    LevelUpService.setThuKhoa(item.Id);
                }
                danhsach.AddRange(lamdaiINu);
            }
            // lam đai ii nam
            var lamdaiIINam = LevelUpService.GetThuKhoa(CompanyId, ExaminationId, 4, Gender.Male);
            if (lamdaiIINam != null)
            {
                foreach (var item in lamdaiIINam)
                {
                    item.KetQua = KetQua.ThuKhoa;
                    LevelUpService.setThuKhoa(item.Id);
                }

                danhsach.AddRange(lamdaiIINam);
            }
            // lam đai i nữ
            var lamdaiIINu = LevelUpService.GetThuKhoa(CompanyId, ExaminationId, 4, Gender.Female);
            if (lamdaiIINu != null)
            {
                foreach (var item in lamdaiIINu)
                {
                    item.KetQua = KetQua.ThuKhoa;
                    LevelUpService.setThuKhoa(item.Id);
                }
                danhsach.AddRange(lamdaiIINu);
            }
            // Á KHOA

            // tự vệ nam
            var tuveAkhoaNam = LevelUpService.GetAKhoa(CompanyId, ExaminationId, 1, Gender.Male);
            if (tuveAkhoaNam != null)
            {
                foreach (var item in tuveAkhoaNam)
                {
                    item.KetQua = KetQua.AKhoa;
                    LevelUpService.setAKhoa(item.Id);
                }
                danhsach.AddRange(tuveAkhoaNam);
            }
            // tự vệ nữ
            var tuveAkhoaNu = LevelUpService.GetAKhoa(CompanyId, ExaminationId, 1, Gender.Female);
            if (tuveAkhoaNu != null)
            {
                foreach (var item in tuveAkhoaNu)
                {
                    item.KetQua = KetQua.AKhoa;
                    LevelUpService.setAKhoa(item.Id);
                }

                danhsach.AddRange(tuveAkhoaNu);
            }
            // lam đai nam
            var lamdaiAkhoaNam = LevelUpService.GetAKhoa(CompanyId, ExaminationId, 2, Gender.Male);
            if (lamdaiAkhoaNam != null)
            {
                foreach (var item in lamdaiAkhoaNam)
                {
                    item.KetQua = KetQua.AKhoa;
                    LevelUpService.setAKhoa(item.Id);
                }
                danhsach.AddRange(lamdaiAkhoaNam);
            }
            // lam đai nữ
            var lamdaiAkhoaNu = LevelUpService.GetAKhoa(CompanyId, ExaminationId, 2, Gender.Female);
            if (lamdaiAkhoaNu != null)
            {
                foreach (var item in lamdaiAkhoaNu)
                {
                    item.KetQua = KetQua.AKhoa;
                    LevelUpService.setAKhoa(item.Id);
                }
                danhsach.AddRange(lamdaiAkhoaNu);
            }
            // lam đai i nam
            var lamdaiIAkhoaNam = LevelUpService.GetAKhoa(CompanyId, ExaminationId, 3, Gender.Male);
            if (lamdaiIAkhoaNam != null)
            {
                foreach (var item in lamdaiIAkhoaNam)
                {
                    item.KetQua = KetQua.AKhoa;
                    LevelUpService.setAKhoa(item.Id);
                }
                danhsach.AddRange(lamdaiIAkhoaNam);
            }
            // lam đai i nữ
            var lamdaiIAkhoaNu = LevelUpService.GetAKhoa(CompanyId, ExaminationId, 3, Gender.Female);
            if (lamdaiIAkhoaNu != null)
            {
                foreach (var item in lamdaiIAkhoaNu)
                {
                    item.KetQua = KetQua.AKhoa;
                    LevelUpService.setAKhoa(item.Id);
                }
                danhsach.AddRange(lamdaiIAkhoaNu);
            }
            // lam đai ii nam
            var lamdaiIIAkhoaNam = LevelUpService.GetAKhoa(CompanyId, ExaminationId, 4, Gender.Male);
            if (lamdaiIIAkhoaNam != null)
            {
                foreach (var item in lamdaiIIAkhoaNam)
                {
                    item.KetQua = KetQua.AKhoa;
                    LevelUpService.setAKhoa(item.Id);
                }
                danhsach.AddRange(lamdaiIIAkhoaNam);
            }
            // lam đai ii nữ
            var lamdaiIIAkhoaNu = LevelUpService.GetAKhoa(CompanyId, ExaminationId, 4, Gender.Female);
            if (lamdaiIIAkhoaNu != null)
            {

                foreach (var item in lamdaiIIAkhoaNu)
                {
                    item.KetQua = KetQua.AKhoa;
                    LevelUpService.setAKhoa(item.Id);
                }
                danhsach.AddRange(lamdaiIIAkhoaNu);
            }

            danhsach.OrderBy(x => x.LevelId);
            return PartialView("_Result", danhsach);
        }


        [HttpPost]
        public ActionResult ShowHistory(int id)
        {
            List<LevelUpHistory> data = LevelUpHistoryService.Gets(id);
            return PartialView("_ShowHistory", data);
        }

        [HttpPost]
        public ActionResult Detail(int id)
        {
            LevelUp data = LevelUpService.Get(id);
            return PartialView("_Detail", data);
        }

        [HttpPost]
        public ActionResult Edit(LevelUp model)
        {
            var data = LevelUpService.Get(model.Id);
            var user = UserService.GetUserInfo();

            if (model.Quyen.Point != data.Quyen.Point)
            {
                LevelUpService.UpdateQuyen(data, model);
            }

            if (model.CoBan.Point != data.CoBan.Point)
            {
                LevelUpService.UpdateCoBan(data, model);
            }

            if (model.VoDao.Point != data.VoDao.Point)
            {
                LevelUpService.UpdateVoDao(data, model);
            }

            if (model.LevelId != 1)
            {
                if (model.DoiKhang.Point != data.DoiKhang.Point)
                {
                    LevelUpService.UpdateDoiKhang(data, model);
                }
            }

            if (model.LevelId == 1)
            {
                if (model.TheLuc.Point != data.TheLuc.Point)
                {
                    LevelUpService.UpdateTheLuc(data, model);
                }
            }
            if (model.LevelId == 4)
            {
                if (model.SongLuyen.Point != data.SongLuyen.Point)
                {
                    LevelUpService.UpdateSongLuyen(data, model);
                }
            }
            LevelUpService.setKetQua(model);
            return
                   Json(
                       new RedirectCommand
                       {
                           Code = ResultCode.Success,
                           Message = "Cập nhật điểm thành công!",
                           Url = Url.Action("Index")
                       },
                       JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileBase uploadfile, int? page)
        {
            var ExaminationName = String.Empty;
            if (ModelState.IsValid)
            {
                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    //ExcelDataReader works on binary excel file
                    Stream stream = uploadfile.InputStream;
                    //We need to written the Interface.
                    IExcelDataReader reader = null;
                    if (uploadfile.FileName.EndsWith(".xls"))
                    {
                        //reads the excel file with .xls extension
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (uploadfile.FileName.EndsWith(".xlsx"))
                    {
                        //reads excel file with .xlsx extension
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        return Json(new CommandResult
                        {
                            Code = ResultCode.Fail,
                            Message = "Chỉ được nhập file .xlsx và .xls"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    //treats the first row of excel file as Coluymn Names
                    reader.IsFirstRowAsColumnNames = true;
                    //Adding reader data to DataSet()
                    DataSet result = reader.AsDataSet();
                    DataTable data = result.Tables[0];
                    List<LevelUpViewModel> lists = new List<LevelUpViewModel>();
                    for (int i = 0; i < data.Rows.Count - 1; i++)
                    {
                        LevelUpViewModel student = new LevelUpViewModel();
                        student.Stt = Convert.ToInt32(data.Rows[i]["Stt"]);
                        student.Name = data.Rows[i]["Tên"].ToString();
                        student.BirthDay = Convert.ToInt32(data.Rows[i]["Năm sinh"]);
                        if (data.Rows[i]["Giới tính"].ToString().Equals("Nam"))
                            student.Gender = Gender.Male;
                        else
                            student.Gender = Gender.Female;
                        student.ClubName = data.Rows[i]["Câu lạc bộ"].ToString();
                        student.LevelName = data.Rows[i]["Cấp đai"].ToString();
                        if (!data.Rows[i].IsNull("Cân nặng"))
                            student.Weight = Convert.ToInt32(data.Rows[i]["Cân nặng"]);
                        student.ExaminationName = data.Rows[i]["Khóa thi"].ToString();
                        ExaminationName = data.Rows[i]["Khóa thi"].ToString();
                        lists.Add(student);
                    }
                    Examination exam = ExaminationService.GetByName(ExaminationName);
                    if (exam == null)
                    {
                        exam = new Examination();
                        exam.Name = ExaminationName;
                        ExaminationService.Create(exam);
                    }
                    //Sending result data to View
                    return View(lists);
                }
            }
            else
            {
                ModelState.AddModelError("File", "Please upload your file");
            }
            return View();
        }

        public ActionResult Import()
        {
            return View();
        }





        public ActionResult Save(List<LevelUpViewModel> data)
        {
            List<LevelUp> model = new List<LevelUp>();
            model = data.Select(x => new LevelUp
            {
                Stt = x.Stt,
                Name = x.Name,
                BirthDay = x.BirthDay,
                Gender = x.Gender,
                KetQua = KetQua.CapNhat,
                ClubId = ClubService.GetByName(x.ClubName).Id,
                LevelId = LevelService.GetByName(x.LevelName).Id,
                Weight = x.Weight,
                ExaminationId = ExaminationService.GetByName(x.ExaminationName).Id
            }).ToList();
            LevelUpService.Create(model);
            // tạo danh sách đối kháng
            var doikhangNam = GetCompete(model, Gender.Male);
            var doikhangNu = GetCompete(model, Gender.Female);

            if (doikhangNam != null)
                CompeteService.Create(doikhangNam);
            if (doikhangNu != null)
                CompeteService.Create(doikhangNu);

            return Json(new RedirectCommand
            {
                Code = ResultCode.Success,
                Message = "Đã tạo danh sách khoa thi thành công!",
                Url = Url.Action("Index")
            }, JsonRequestBehavior.AllowGet);
        }

        private List<Compete> GetCompete(List<LevelUp> model, Gender gender)
        {
            List<LevelUp> doikhang = model.Where(x => x.Gender == gender).Where(x => x.Weight != 0).OrderBy(x => x.Weight).ToList();

            List<Compete> capdau = new List<Compete>();

            for (int i = 0, length = doikhang.Count - 1; i < length; i++)
            {
                for (int j = 1, count = doikhang.Count; j < count; j++)
                {
                    if (doikhang[i].HasCompete)
                        break;
                    else if (doikhang[j].HasCompete)
                        continue;

                    var diff = doikhang[i].Weight - doikhang[j].Weight;
                    if (doikhang[i].ClubId != doikhang[j].ClubId && ((diff > 0 && diff <= 3) || (diff < 0 && diff >= -3)))
                    {
                        Compete compete = new Compete();
                        compete.LevelUpId1 = doikhang[i].Id;
                        compete.LevelUpId2 = doikhang[j].Id;
                        compete.Gender = gender;
                        compete.ExaminationId = doikhang[i].ExaminationId;

                        capdau.Add(compete);
                        doikhang[i].HasCompete = true;
                        doikhang[j].HasCompete = true;
                        break;
                    }
                }

            }

            doikhang = doikhang.Where(x => x.HasCompete == false).OrderBy(x => x.Weight).ToList();
            if (doikhang != null)
            {

                for (int i = 0, length = doikhang.Count - 1; i < length; i++)
                {
                    for (int j = 1, count = doikhang.Count; j < count; j++)
                    {

                        if (doikhang[i].HasCompete)
                            break;
                        else if (doikhang[j].HasCompete)
                            continue;

                        var diff = doikhang[i].Weight - doikhang[j].Weight;
                        if (((diff > 0 && diff <= 3) || (diff < 0 && diff >= -3)))
                        {
                            Compete compete = new Compete();
                            compete.LevelUpId1 = doikhang[i].Id;
                            compete.LevelUpId2 = doikhang[j].Id;
                            compete.Gender = gender;
                            compete.ExaminationId = doikhang[i].ExaminationId;

                            capdau.Add(compete);
                            doikhang[i].HasCompete = true;
                            doikhang[j].HasCompete = true;
                            break;
                        }
                    }
                }
            }

            doikhang = doikhang.Where(x => x.HasCompete == false).OrderBy(x => x.Weight).ToList();
            if (doikhang != null)
            {
                if (doikhang.Count % 2 != 0)
                {
                    Compete compete = new Compete();
                    compete.LevelUpId1 = doikhang[0].Id;
                    compete.LevelUpId2 = doikhang[0].Id;
                    compete.Gender = gender;
                    compete.ExaminationId = doikhang[0].ExaminationId;

                    capdau.Add(compete);
                    doikhang[0].HasCompete = true;
                    doikhang = doikhang.Where(x => x.HasCompete == false).OrderBy(x => x.Weight).ToList();
                }

                for (int i = 0, length = doikhang.Count - 1; i < length; i++)
                {
                    for (int j = 1, count = doikhang.Count; j < count; j++)
                    {

                        if (doikhang[i].HasCompete)
                            break;
                        else if (doikhang[j].HasCompete)
                            continue;
                        Compete compete = new Compete();
                        compete.LevelUpId1 = doikhang[i].Id;
                        compete.LevelUpId2 = doikhang[j].Id;
                        compete.Gender = gender;
                        compete.ExaminationId = doikhang[i].ExaminationId;

                        capdau.Add(compete);
                        doikhang[i].HasCompete = true;
                        doikhang[j].HasCompete = true;
                        break;
                    }
                }

            }
            return capdau;
        }

        public ActionResult ExportExcel(int ExaminationId)
        {

            var user = UserService.GetUserInfo();
            List<LevelUp> tuve = LevelUpService.Gets(user.CompanyId, ExaminationId, 1);
            List<LevelUp> lamdai = LevelUpService.Gets(user.CompanyId, ExaminationId, 2);
            List<LevelUp> lamdai1 = LevelUpService.Gets(user.CompanyId, ExaminationId, 3);
            List<LevelUp> lamdai2 = LevelUpService.Gets(user.CompanyId, ExaminationId, 4);

            string filePath = Server.MapPath(Url.Content("~/Content/Upload/Excel/danhsachthi.xlsx"));
            var existingFile = new FileInfo(filePath);
            using (var package = new ExcelPackage(existingFile))
            {
                var workBook = package.Workbook;
                ExcelWorksheet currentWorksheet = workBook.Worksheets.First();
                currentWorksheet.Cells["A:XFD"].Style.Font.Name = "Times New Roman";
                currentWorksheet.Cells["A:XFD"].Style.Font.Size = 12;

                currentWorksheet.Cells["D10:E10"].Merge = true;
                currentWorksheet.Cells["D10:E10"].Value = tuve.Count;
                currentWorksheet.Cells["D11:E11"].Merge = true;
                currentWorksheet.Cells["D11:E11"].Value = lamdai.Count;
                currentWorksheet.Cells["D12:E12"].Merge = true;
                currentWorksheet.Cells["D12:E12"].Value = lamdai1.Count;
                currentWorksheet.Cells["D13:E13"].Merge = true;
                currentWorksheet.Cells["D13:E13"].Value = lamdai2.Count;

                currentWorksheet.Cells["A17:F17"].Merge = true;
                currentWorksheet.Cells["A17:F17"].Style.Font.Bold = true;
                currentWorksheet.Cells["A17:F17"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                currentWorksheet.Cells["A17:F17"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                currentWorksheet.Cells["A17:F17"].Value = "THI LÊN NHẬP MÔN";
                int rowStarTuVe = 17;
                int rowEndTuVe = rowStarTuVe + 1;

                foreach (var item in tuve)
                {
                    rowStarTuVe++;
                    currentWorksheet.Cells[string.Format("J{0}:K{0}", rowStarTuVe)].Merge = true;
                    currentWorksheet.Cells[string.Format("M{0}:N{0}", rowStarTuVe)].Merge = true;
                    currentWorksheet.Cells[string.Format("A{0}", rowStarTuVe)].Value = item.Stt;
                    currentWorksheet.Cells[string.Format("B{0}", rowStarTuVe)].Value = Utility.GetFirtsName(item.Name);
                    currentWorksheet.Cells[string.Format("C{0}", rowStarTuVe)].Value = Utility.GetLastName(item.Name);
                    currentWorksheet.Cells[string.Format("D{0}", rowStarTuVe)].Value = item.Gender.GetString();
                    currentWorksheet.Cells[string.Format("E{0}", rowStarTuVe)].Value = item.BirthDay;
                    if (item.ClubId == 1 || item.ClubId == 2 || item.ClubId == 1004)
                        currentWorksheet.Cells[string.Format("F{0}", rowStarTuVe)].Value = "ĐH KHXH&NV";
                    else
                        currentWorksheet.Cells[string.Format("F{0}", rowStarTuVe)].Value = item.Club.Name;
                }
                if (tuve.Count != 0)
                {
                    currentWorksheet.Cells[string.Format("A{0}:N{1}", rowEndTuVe, rowStarTuVe)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:N{1}", rowEndTuVe, rowStarTuVe)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:N{1}", rowEndTuVe, rowStarTuVe)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:N{1}", rowEndTuVe, rowStarTuVe)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }
                // row lam dai 
                int rowStarLamDai = rowStarTuVe + 1;
                int rowEndLamDai = rowStarTuVe;

                currentWorksheet.Row(rowStarLamDai).Height = 30;

                currentWorksheet.Cells[string.Format("A{0}:F{0}", rowStarLamDai)].Merge = true;
                currentWorksheet.Cells[string.Format("A{0}:N{0}", rowStarLamDai)].Style.Font.Bold = true;
                currentWorksheet.Cells[string.Format("A{0}:N{0}", rowStarLamDai)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                currentWorksheet.Cells[string.Format("A{0}:N{0}", rowStarLamDai)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                currentWorksheet.Cells[string.Format("A{0}:F{0}", rowStarLamDai)].Value = "THI LÊN LAM ĐAI NHẤT";

                currentWorksheet.Cells[string.Format("G{0}", rowStarLamDai)].Value = "Kg";
                currentWorksheet.Cells[string.Format("H{0}", rowStarLamDai)].Value = "Cơ bản";
                currentWorksheet.Cells[string.Format("I{0}", rowStarLamDai)].Value = "Quyền";
                currentWorksheet.Cells[string.Format("J{0}", rowStarLamDai)].Value = "Đối kháng";
                currentWorksheet.Cells[string.Format("K{0}", rowStarLamDai)].Value = "Võ đạo";
                currentWorksheet.Cells[string.Format("L{0}", rowStarLamDai)].Value = "Tổng điểm";
                currentWorksheet.Cells[string.Format("M{0}:N{0}", rowStarLamDai)].Merge = true;
                currentWorksheet.Cells[string.Format("M{0}:N{0}", rowStarLamDai)].Value = "Kết quả";

                foreach (var item in lamdai)
                {
                    rowStarLamDai++;
                    currentWorksheet.Cells[string.Format("M{0}:N{0}", rowStarLamDai)].Merge = true;
                    currentWorksheet.Cells[string.Format("A{0}", rowStarLamDai)].Value = item.Stt;
                    currentWorksheet.Cells[string.Format("B{0}", rowStarLamDai)].Value = Utility.GetFirtsName(item.Name);
                    currentWorksheet.Cells[string.Format("C{0}", rowStarLamDai)].Value = Utility.GetLastName(item.Name);
                    currentWorksheet.Cells[string.Format("D{0}", rowStarLamDai)].Value = item.Gender.GetString();
                    currentWorksheet.Cells[string.Format("E{0}", rowStarLamDai)].Value = item.BirthDay;
                    if (item.ClubId == 1 || item.ClubId == 2 || item.ClubId == 1004)
                        currentWorksheet.Cells[string.Format("F{0}", rowStarLamDai)].Value = "ĐH KHXH&NV";
                    else
                        currentWorksheet.Cells[string.Format("F{0}", rowStarLamDai)].Value = item.Club.Name;
                    currentWorksheet.Cells[string.Format("G{0}", rowStarLamDai)].Value = item.Weight;
                }
                if (lamdai.Count != 0)
                {
                    currentWorksheet.Cells[string.Format("A{0}:N{1}", rowEndLamDai, rowStarLamDai)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:N{1}", rowEndLamDai, rowStarLamDai)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:N{1}", rowEndLamDai, rowStarLamDai)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:N{1}", rowEndLamDai, rowStarLamDai)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }
                // end lam dai
                // row lam dai nhất
                int rowStarLamDaiI = rowStarLamDai + 1;
                int rowEndLamDaiI = rowStarLamDaiI;

                currentWorksheet.Row(rowStarLamDaiI).Height = 30;

                currentWorksheet.Cells[string.Format("A{0}:F{0}", rowStarLamDaiI)].Merge = true;
                currentWorksheet.Cells[string.Format("A{0}:N{0}", rowStarLamDaiI)].Style.Font.Bold = true;
                currentWorksheet.Cells[string.Format("A{0}:N{0}", rowStarLamDaiI)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                currentWorksheet.Cells[string.Format("A{0}:F{0}", rowStarLamDaiI)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                currentWorksheet.Cells[string.Format("A{0}:F{0}", rowStarLamDaiI)].Value = "THI LÊN LAM ĐAI NHỊ";

                currentWorksheet.Cells[string.Format("G{0}", rowStarLamDaiI)].Value = "Kg";
                currentWorksheet.Cells[string.Format("H{0}", rowStarLamDaiI)].Value = "Cơ bản";
                currentWorksheet.Cells[string.Format("I{0}", rowStarLamDaiI)].Value = "Quyền";
                currentWorksheet.Cells[string.Format("J{0}", rowStarLamDaiI)].Value = "Đối kháng";
                currentWorksheet.Cells[string.Format("K{0}", rowStarLamDaiI)].Value = "Võ đạo";
                currentWorksheet.Cells[string.Format("L{0}", rowStarLamDaiI)].Value = "Song luyện";
                currentWorksheet.Cells[string.Format("M{0}", rowStarLamDaiI)].Value = "Tổng điểm";
                currentWorksheet.Cells[string.Format("N{0}", rowStarLamDaiI)].Value = "Kết quả";

                foreach (var item in lamdai1)
                {
                    rowStarLamDaiI++;
                    currentWorksheet.Cells[string.Format("A{0}", rowStarLamDaiI)].Value = item.Stt;
                    currentWorksheet.Cells[string.Format("B{0}", rowStarLamDaiI)].Value = Utility.GetFirtsName(item.Name);
                    currentWorksheet.Cells[string.Format("C{0}", rowStarLamDaiI)].Value = Utility.GetLastName(item.Name);
                    currentWorksheet.Cells[string.Format("D{0}", rowStarLamDaiI)].Value = item.Gender.GetString();
                    currentWorksheet.Cells[string.Format("E{0}", rowStarLamDaiI)].Value = item.BirthDay;
                    if (item.ClubId == 1 || item.ClubId == 2 || item.ClubId == 1004)
                        currentWorksheet.Cells[string.Format("F{0}", rowStarLamDaiI)].Value = "ĐH KHXH&NV";
                    else
                        currentWorksheet.Cells[string.Format("F{0}", rowStarLamDaiI)].Value = item.Club.Name;
                    currentWorksheet.Cells[string.Format("G{0}", rowStarLamDai)].Value = item.Weight;
                }
                if (lamdai1.Count != 0)
                {
                    currentWorksheet.Cells[string.Format("A{0}:N{1}", rowEndLamDaiI, rowStarLamDaiI)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:N{1}", rowEndLamDaiI, rowStarLamDaiI)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:N{1}", rowEndLamDaiI, rowStarLamDaiI)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:N{1}", rowEndLamDaiI, rowStarLamDaiI)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }
                // end lam dai nhất 

                // row lam dai nhị
                int rowStarLamDaiII = rowStarLamDaiI + 1;
                int rowEndLamDaiII = rowStarLamDaiI;

                currentWorksheet.Row(rowStarLamDaiII).Height = 30;

                currentWorksheet.Cells[string.Format("A{0}:F{0}", rowStarLamDaiII)].Merge = true;
                currentWorksheet.Cells[string.Format("A{0}:N{0}", rowStarLamDaiII)].Style.Font.Bold = true;
                currentWorksheet.Cells[string.Format("A{0}:N{0}", rowStarLamDaiII)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                currentWorksheet.Cells[string.Format("A{0}:N{0}", rowStarLamDaiII)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                currentWorksheet.Cells[string.Format("A{0}:F{0}", rowStarLamDaiII)].Value = "THI LÊN LAM ĐAI TAM";

                currentWorksheet.Cells[string.Format("G{0}", rowStarLamDaiII)].Value = "Kg";
                currentWorksheet.Cells[string.Format("H{0}", rowStarLamDaiII)].Value = "Cơ bản";
                currentWorksheet.Cells[string.Format("I{0}", rowStarLamDaiII)].Value = "Quyền";
                currentWorksheet.Cells[string.Format("J{0}", rowStarLamDaiII)].Value = "Đối kháng";
                currentWorksheet.Cells[string.Format("K{0}", rowStarLamDaiII)].Value = "Võ đạo";
                currentWorksheet.Cells[string.Format("L{0}", rowStarLamDaiII)].Value = "Song luyện";
                currentWorksheet.Cells[string.Format("M{0}", rowStarLamDaiII)].Value = "Tổng điểm";
                currentWorksheet.Cells[string.Format("N{0}", rowStarLamDaiII)].Value = "Kết quả";

                foreach (var item in lamdai2)
                {
                    rowStarLamDaiII++;

                    currentWorksheet.Cells[string.Format("A{0}", rowStarLamDaiII)].Value = item.Stt;
                    currentWorksheet.Cells[string.Format("B{0}", rowStarLamDaiII)].Value = Utility.GetFirtsName(item.Name);
                    currentWorksheet.Cells[string.Format("C{0}", rowStarLamDaiII)].Value = Utility.GetLastName(item.Name);
                    currentWorksheet.Cells[string.Format("D{0}", rowStarLamDaiII)].Value = item.Gender.GetString();
                    currentWorksheet.Cells[string.Format("E{0}", rowStarLamDaiII)].Value = item.BirthDay;
                    if (item.ClubId == 1 || item.ClubId == 2 || item.ClubId == 1004)
                        currentWorksheet.Cells[string.Format("F{0}", rowStarLamDaiII)].Value = "ĐH KHXH&NV";
                    else
                        currentWorksheet.Cells[string.Format("F{0}", rowStarLamDaiII)].Value = item.Club.Name;
                    currentWorksheet.Cells[string.Format("G{0}", rowStarLamDaiII)].Value = item.Weight;
                    rowStarTuVe++;
                }
                if (lamdai2.Count != 0)
                {
                    currentWorksheet.Cells[string.Format("A{0}:N{1}", rowEndLamDaiII, rowStarLamDaiII)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:N{1}", rowEndLamDaiII, rowStarLamDaiII)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:N{1}", rowEndLamDaiII, rowStarLamDaiII)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:N{1}", rowEndLamDaiII, rowStarLamDaiII)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }

                // end lam dai nhất

                currentWorksheet.Cells["A:XFD"].AutoFitColumns();
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;  filename=" + "DanhSachThi.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(package.GetAsByteArray());
                Response.End();
            }

            return null;

        }


        public ActionResult ExportResult(int ExaminationId)
        {

            var user = UserService.GetUserInfo();
            List<LevelUp> tuve = LevelUpService.Gets(user.CompanyId, ExaminationId, 1);
            List<LevelUp> lamdai = LevelUpService.Gets(user.CompanyId, ExaminationId, 2);
            List<LevelUp> lamdai1 = LevelUpService.Gets(user.CompanyId, ExaminationId, 3);
            List<LevelUp> lamdai2 = LevelUpService.Gets(user.CompanyId, ExaminationId, 4);

            string filePath = Server.MapPath(Url.Content("~/Content/Upload/Excel/ketquathi.xlsx"));
            var existingFile = new FileInfo(filePath);
            using (var package = new ExcelPackage(existingFile))
            {
                var workBook = package.Workbook;
                ExcelWorksheet currentWorksheet = workBook.Worksheets.First();
                currentWorksheet.Cells["A:XFD"].Style.Font.Name = "Times New Roman";
                currentWorksheet.Cells["A:XFD"].Style.Font.Size = 12;

                currentWorksheet.Cells["C10:D10"].Merge = true;
                currentWorksheet.Cells["C10:D10"].Value = tuve.Count;
                currentWorksheet.Cells["C11:D11"].Merge = true;
                currentWorksheet.Cells["C11:D11"].Value = lamdai.Count;
                currentWorksheet.Cells["C12:D12"].Merge = true;
                currentWorksheet.Cells["C12:D12"].Value = lamdai1.Count;
                currentWorksheet.Cells["C13:D13"].Merge = true;
                currentWorksheet.Cells["C13:D13"].Value = lamdai2.Count;

                currentWorksheet.Cells["B16:D16"].Merge = true;
                currentWorksheet.Cells["B16:D16"].Style.Font.Bold = true;
                currentWorksheet.Cells["B16:D16"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                currentWorksheet.Cells["B16:D16"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                currentWorksheet.Cells["E16:L16"].Merge = true;
                currentWorksheet.Cells[string.Format("A{0}:L{0}", 16)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                currentWorksheet.Cells[string.Format("A{0}:L{0}", 16)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                currentWorksheet.Cells[string.Format("A{0}:L{0}", 16)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                currentWorksheet.Cells[string.Format("A{0}:L{0}", 16)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                currentWorksheet.Cells["B16:D16"].Value = "THI LÊN NHẬP MÔN";
                int rowStarTuVe = 16;
                int rowEndTuVe = rowStarTuVe + 1;

                foreach (var item in tuve)
                {
                    rowStarTuVe++;
                    currentWorksheet.Cells[string.Format("K{0}:L{0}", rowStarTuVe)].Merge = true;
                    currentWorksheet.Cells[string.Format("A{0}", rowStarTuVe)].Value = item.Stt;
                    currentWorksheet.Cells[string.Format("B{0}", rowStarTuVe)].Value = item.Name;
                    if (item.Gender == Gender.Male)
                        currentWorksheet.Cells[string.Format("C{0}", rowStarTuVe)].Value = item.BirthDay;
                    else
                        currentWorksheet.Cells[string.Format("D{0}", rowStarTuVe)].Value = item.BirthDay;

                    if (item.ClubId == 1 || item.ClubId == 2 || item.ClubId == 1004)
                        currentWorksheet.Cells[string.Format("E{0}", rowStarTuVe)].Value = "ĐH KHXH&NV";
                    else
                        currentWorksheet.Cells[string.Format("E{0}", rowStarTuVe)].Value = item.Club.Name;

                    currentWorksheet.Cells[string.Format("F{0}", rowStarTuVe)].Value = item.CoBan.Point;
                    currentWorksheet.Cells[string.Format("G{0}", rowStarTuVe)].Value = item.VoDao.Point;
                    currentWorksheet.Cells[string.Format("H{0}", rowStarTuVe)].Value = item.Quyen.Point;
                    currentWorksheet.Cells[string.Format("I{0}", rowStarTuVe)].Value = item.TheLuc.Point;
                    currentWorksheet.Cells[string.Format("J{0}", rowStarTuVe)].Value = item.Total;
                    currentWorksheet.Cells[string.Format("K{0}:L{0}", rowStarTuVe)].Value = item.KetQua.GetString();


                }
                if (tuve.Count != 0)
                {
                    currentWorksheet.Cells[string.Format("A{0}:L{1}", rowEndTuVe, rowStarTuVe)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:L{1}", rowEndTuVe, rowStarTuVe)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:L{1}", rowEndTuVe, rowStarTuVe)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:L{1}", rowEndTuVe, rowStarTuVe)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }
                // row lam dai 
                int rowStarLamDai = rowStarTuVe + 1;
                int rowEndLamDai = rowStarTuVe;

                currentWorksheet.Row(rowStarLamDai).Height = 40;

                currentWorksheet.Cells[string.Format("B{0}:E{0}", rowStarLamDai)].Merge = true;
                currentWorksheet.Cells[string.Format("A{0}:L{0}", rowStarLamDai)].Style.Font.Bold = true;
                currentWorksheet.Cells[string.Format("A{0}:L{0}", rowStarLamDai)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                currentWorksheet.Cells[string.Format("A{0}:L{0}", rowStarLamDai)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                currentWorksheet.Cells[string.Format("B{0}:E{0}", rowStarLamDai)].Value = "THI LÊN LAM ĐAI NHẤT";

                currentWorksheet.Cells[string.Format("F{0}", rowStarLamDai)].Value = "Cơ bản";
                currentWorksheet.Cells[string.Format("G{0}", rowStarLamDai)].Value = "Quyền";
                currentWorksheet.Cells[string.Format("H{0}", rowStarLamDai)].Value = "Võ đạo";
                currentWorksheet.Cells[string.Format("I{0}", rowStarLamDai)].Value = "Đối kháng";
                currentWorksheet.Cells[string.Format("J{0}", rowStarLamDai)].Value = "Tổng điểm";
                currentWorksheet.Cells[string.Format("K{0}:L{0}", rowStarLamDai)].Merge = true;
                currentWorksheet.Cells[string.Format("K{0}:L{0}", rowStarLamDai)].Value = "Kết quả";

                foreach (var item in lamdai)
                {
                    rowStarLamDai++;
                    currentWorksheet.Cells[string.Format("K{0}:L{0}", rowStarLamDai)].Merge = true;
                    currentWorksheet.Cells[string.Format("A{0}", rowStarLamDai)].Value = item.Stt;
                    currentWorksheet.Cells[string.Format("B{0}", rowStarLamDai)].Value = item.Name;
                    if (item.Gender == Gender.Male)
                        currentWorksheet.Cells[string.Format("C{0}", rowStarLamDai)].Value = item.BirthDay;
                    else
                        currentWorksheet.Cells[string.Format("D{0}", rowStarLamDai)].Value = item.BirthDay;

                    if (item.ClubId == 1 || item.ClubId == 2 || item.ClubId == 1004)
                        currentWorksheet.Cells[string.Format("E{0}", rowStarLamDai)].Value = "ĐH KHXH&NV";
                    else
                        currentWorksheet.Cells[string.Format("E{0}", rowStarLamDai)].Value = item.Club.Name;
                    currentWorksheet.Cells[string.Format("F{0}", rowStarLamDai)].Value = item.CoBan.Point;
                    currentWorksheet.Cells[string.Format("G{0}", rowStarLamDai)].Value = item.Quyen.Point;
                    currentWorksheet.Cells[string.Format("H{0}", rowStarLamDai)].Value = item.VoDao.Point;
                    currentWorksheet.Cells[string.Format("I{0}", rowStarLamDai)].Value = item.DoiKhang.Point;
                    currentWorksheet.Cells[string.Format("J{0}", rowStarLamDai)].Value = item.Total;
                    currentWorksheet.Cells[string.Format("K{0}:L{0}", rowStarLamDai)].Value = item.KetQua.GetString();

                }
                if (lamdai.Count != 0)
                {
                    currentWorksheet.Cells[string.Format("A{0}:L{1}", rowEndLamDai, rowStarLamDai)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:L{1}", rowEndLamDai, rowStarLamDai)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:L{1}", rowEndLamDai, rowStarLamDai)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:L{1}", rowEndLamDai, rowStarLamDai)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }
                // end lam dai
                // row lam dai nhất
                int rowStarLamDaiI = rowStarLamDai + 1;
                int rowEndLamDaiI = rowStarLamDaiI;

                currentWorksheet.Row(rowStarLamDaiI).Height = 40;

                currentWorksheet.Cells[string.Format("B{0}:E{0}", rowStarLamDaiI)].Merge = true;
                currentWorksheet.Cells[string.Format("A{0}:L{0}", rowStarLamDaiI)].Style.Font.Bold = true;
                currentWorksheet.Cells[string.Format("A{0}:L{0}", rowStarLamDaiI)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                currentWorksheet.Cells[string.Format("A{0}:L{0}", rowStarLamDaiI)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                currentWorksheet.Cells[string.Format("B{0}:E{0}", rowStarLamDaiI)].Value = "THI LÊN LAM ĐAI NHỊ";

                currentWorksheet.Cells[string.Format("F{0}", rowStarLamDaiI)].Value = "Cơ bản";
                currentWorksheet.Cells[string.Format("G{0}", rowStarLamDaiI)].Value = "Quyền";
                currentWorksheet.Cells[string.Format("H{0}", rowStarLamDaiI)].Value = "Võ đạo";
                currentWorksheet.Cells[string.Format("I{0}", rowStarLamDaiI)].Value = "Đối kháng";
                currentWorksheet.Cells[string.Format("J{0}", rowStarLamDaiI)].Value = "Song luyện";
                currentWorksheet.Cells[string.Format("K{0}", rowStarLamDaiI)].Value = "Tổng điểm";
                currentWorksheet.Cells[string.Format("L{0}", rowStarLamDaiI)].Value = "Kết quả";

                foreach (var item in lamdai1)
                {
                    rowStarLamDaiI++;
                    currentWorksheet.Cells[string.Format("A{0}", rowStarLamDaiI)].Value = item.Stt;
                    currentWorksheet.Cells[string.Format("B{0}", rowStarLamDaiI)].Value = item.Name;
                    if (item.Gender == Gender.Male)
                        currentWorksheet.Cells[string.Format("C{0}", rowStarLamDaiI)].Value = item.BirthDay;
                    else
                        currentWorksheet.Cells[string.Format("D{0}", rowStarLamDaiI)].Value = item.BirthDay;

                    if (item.ClubId == 1 || item.ClubId == 2 || item.ClubId == 1004)
                        currentWorksheet.Cells[string.Format("E{0}", rowStarLamDaiI)].Value = "ĐH KHXH&NV";
                    else
                        currentWorksheet.Cells[string.Format("E{0}", rowStarLamDaiI)].Value = item.Club.Name;

                    currentWorksheet.Cells[string.Format("F{0}", rowStarLamDaiI)].Value = item.CoBan.Point;
                    currentWorksheet.Cells[string.Format("G{0}", rowStarLamDaiI)].Value = item.Quyen.Point;
                    currentWorksheet.Cells[string.Format("H{0}", rowStarLamDaiI)].Value = item.VoDao.Point;
                    currentWorksheet.Cells[string.Format("I{0}", rowStarLamDaiI)].Value = item.DoiKhang.Point;
                    currentWorksheet.Cells[string.Format("J{0}", rowStarLamDaiI)].Value = item.SongLuyen.Point;
                    currentWorksheet.Cells[string.Format("K{0}", rowStarLamDaiI)].Value = item.Total;
                    currentWorksheet.Cells[string.Format("L{0}", rowStarLamDaiI)].Value = item.KetQua.GetString();
                }
                if (lamdai1.Count != 0)
                {
                    currentWorksheet.Cells[string.Format("A{0}:L{1}", rowEndLamDaiI, rowStarLamDaiI)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:L{1}", rowEndLamDaiI, rowStarLamDaiI)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:L{1}", rowEndLamDaiI, rowStarLamDaiI)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:L{1}", rowEndLamDaiI, rowStarLamDaiI)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }
                // end lam dai nhất 

                // row lam dai nhị
                int rowStarLamDaiII = rowStarLamDaiI + 1;
                int rowEndLamDaiII = rowStarLamDaiI;

                currentWorksheet.Row(rowStarLamDaiII).Height = 40;

                currentWorksheet.Cells[string.Format("B{0}:E{0}", rowStarLamDaiII)].Merge = true;
                currentWorksheet.Cells[string.Format("A{0}:L{0}", rowStarLamDaiII)].Style.Font.Bold = true;
                currentWorksheet.Cells[string.Format("A{0}:L{0}", rowStarLamDaiII)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                currentWorksheet.Cells[string.Format("A{0}:L{0}", rowStarLamDaiII)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                currentWorksheet.Cells[string.Format("B{0}:E{0}", rowStarLamDaiII)].Value = "THI LÊN LAM ĐAI TAM";

                currentWorksheet.Cells[string.Format("F{0}", rowStarLamDaiII)].Value = "Cơ bản";
                currentWorksheet.Cells[string.Format("G{0}", rowStarLamDaiII)].Value = "Quyền";
                currentWorksheet.Cells[string.Format("H{0}", rowStarLamDaiII)].Value = "Võ đạo";
                currentWorksheet.Cells[string.Format("I{0}", rowStarLamDaiII)].Value = "Đối kháng";
                currentWorksheet.Cells[string.Format("J{0}", rowStarLamDaiII)].Value = "Song luyện";
                currentWorksheet.Cells[string.Format("K{0}", rowStarLamDaiII)].Value = "Tổng điểm";
                currentWorksheet.Cells[string.Format("L{0}", rowStarLamDaiII)].Value = "Kết quả";

                foreach (var item in lamdai2)
                {
                    rowStarLamDaiII++;

                    currentWorksheet.Cells[string.Format("A{0}", rowStarLamDaiII)].Value = item.Stt;
                    currentWorksheet.Cells[string.Format("B{0}", rowStarLamDaiII)].Value = item.Name;
                    if (item.Gender == Gender.Male)
                        currentWorksheet.Cells[string.Format("C{0}", rowStarLamDaiII)].Value = item.BirthDay;
                    else
                        currentWorksheet.Cells[string.Format("D{0}", rowStarLamDaiII)].Value = item.BirthDay;

                    if (item.ClubId == 1 || item.ClubId == 2 || item.ClubId == 1004)
                        currentWorksheet.Cells[string.Format("E{0}", rowStarLamDaiII)].Value = "ĐH KHXH&NV";
                    else
                        currentWorksheet.Cells[string.Format("E{0}", rowStarLamDaiII)].Value = item.Club.Name;

                    currentWorksheet.Cells[string.Format("F{0}", rowStarLamDaiII)].Value = item.CoBan.Point;
                    currentWorksheet.Cells[string.Format("G{0}", rowStarLamDaiII)].Value = item.Quyen.Point;
                    currentWorksheet.Cells[string.Format("H{0}", rowStarLamDaiII)].Value = item.VoDao.Point;
                    currentWorksheet.Cells[string.Format("I{0}", rowStarLamDaiII)].Value = item.DoiKhang.Point;
                    currentWorksheet.Cells[string.Format("J{0}", rowStarLamDaiII)].Value = item.SongLuyen.Point;
                    currentWorksheet.Cells[string.Format("K{0}", rowStarLamDaiII)].Value = item.Total;
                    currentWorksheet.Cells[string.Format("L{0}", rowStarLamDaiII)].Value = item.KetQua.GetString();
                }
                if (lamdai2.Count != 0)
                {
                    currentWorksheet.Cells[string.Format("A{0}:L{1}", rowEndLamDaiII, rowStarLamDaiII)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:L{1}", rowEndLamDaiII, rowStarLamDaiII)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:L{1}", rowEndLamDaiII, rowStarLamDaiII)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    currentWorksheet.Cells[string.Format("A{0}:L{1}", rowEndLamDaiII, rowStarLamDaiII)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }

                // end lam dai nhất

                currentWorksheet.Cells["A:XFD"].AutoFitColumns();
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;  filename=" + "DanhSachThi.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(package.GetAsByteArray());
                Response.End();
            }

            return null;

        }
    }


}