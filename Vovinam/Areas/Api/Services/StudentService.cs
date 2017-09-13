using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vovinam.Data;
using System.Data.Entity;
using Vovinam.Areas.Api.Models;

namespace Vovinam.Areas.Api.Services
{
    public class StudentService
    {
        public List<StudentModel> GetStudents(int CompanyId, int clubId)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var students = context.Students.Where(x => x.ClubId == clubId)
                    .Where(x => x.CompanyId == CompanyId)
                    .Where(x => x.Deleted == false)
                    .Include(x => x.Club)
                     .Include(x => x.School)
                     .Include(x => x.Level)
                    .ToList();

                List<StudentModel> results = new List<StudentModel>();

                foreach (var item in students)
                {
                    StudentModel model = new StudentModel();
                    model.id = item.Id;
                    model.name = item.Name;
                    model.hometown = item.Hometown;
                    model.phone = item.Phone;
                    if (item.Gender == Gender.Female)
                        model.gender = "Nữ";
                    else
                        model.gender = "Nam";
                    model.date_in = item.DateIn;
                    model.image = item.Image;
                    model.birthday = item.Birthday;
                    model.tuition1 = item.Tuition1;
                    model.tuition2 = item.Tuition2;
                    model.tuition3 = item.Tuition3;
                    model.tuition4 = item.Tuition4;
                    model.tuition5 = item.Tuition5;
                    model.tuition6 = item.Tuition6;
                    model.tuition9 = item.Tuition9;
                    model.tuition10 = item.Tuition10;
                    model.tuition11 = item.Tuition11;
                    model.tuition12 = item.Tuition12;

                    model.club = new StudentClubModel();
                    model.club.id = item.Club.Id;
                    model.club.Name = item.Club.Name;

                    model.school = new StudentSchoolModel();
                    model.school.id = item.School.Id;
                    model.school.Name = item.School.Name;

                    model.level = new StudentLevelModel();
                    model.level.id = item.Level.Id;
                    model.level.Name = item.Level.Name;


                    results.Add(model);

                }

                return results;
            }
        }
    }
}