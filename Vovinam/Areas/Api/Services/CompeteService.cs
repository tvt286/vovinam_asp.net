using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Vovinam.Areas.Api.Models;
using Vovinam.Data;

namespace Vovinam.Areas.Api.Services
{
    public class CompeteService
    {
        public static List<CompeteModel> GetCompetes(int CompanyId, int examinationId)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var competes = context.Competes.Where(x => x.ExaminationId == examinationId)
                    .Where(x => x.CompanyId == CompanyId)
                    .Include(x => x.LevelUp)
                     .Include(x => x.LevelUp1)
                    .Include(x => x.LevelUp.DoiKhang)
                     .Include(x => x.LevelUp1.DoiKhang)
                    .ToList();

                List<CompeteModel> results = new List<CompeteModel>();

                foreach (var item in competes)
                {

                    CompeteModel model = new CompeteModel();
                    model.levelup_1 = new LevelUpModel();
                    model.levelup_1.id = item.LevelUp.Id;
                    model.levelup_1.name = item.LevelUp.Name;
                    model.levelup_1.stt = item.LevelUp.Stt;
                    model.levelup_1.weight = item.LevelUp.Weight;

                    model.levelup_1.club = new ClubModel();
                    model.levelup_1.club.id = item.LevelUp.Club.Id;
                    model.levelup_1.club.name = item.LevelUp.Club.Name;

                    model.levelup_1.level = new LevelModel();
                    model.levelup_1.level.id = item.LevelUp.Level.Id;
                    model.levelup_1.level.name = item.LevelUp.Level.Name;

                    model.levelup_1.doi_khang = new DoiKhangModel();
                    model.levelup_1.doi_khang.id = item.LevelUp.DoiKhang.Id;
                    if (item.LevelUp.DoiKhang.User != null)
                        model.levelup_1.doi_khang.user_name = item.LevelUp.DoiKhang.User.FullName;

                    model.levelup_2 = new LevelUpModel();
                    model.levelup_2.id = item.LevelUp1.Id;
                    model.levelup_2.name = item.LevelUp1.Name;
                    model.levelup_2.stt = item.LevelUp1.Stt;
                    model.levelup_2.weight = item.LevelUp1.Weight;

                    model.levelup_2.club = new ClubModel();
                    model.levelup_2.club.id = item.LevelUp1.Club.Id;
                    model.levelup_2.club.name = item.LevelUp1.Club.Name;

                    model.levelup_2.level = new LevelModel();
                    model.levelup_2.level.id = item.LevelUp.Level.Id;
                    model.levelup_2.level.name = item.LevelUp1.Level.Name;

                    model.levelup_2.doi_khang = new DoiKhangModel();
                    model.levelup_2.doi_khang.id = item.LevelUp1.DoiKhang.Id;
                    if (item.LevelUp1.DoiKhang.User != null)
                        model.levelup_2.doi_khang.user_name = item.LevelUp1.DoiKhang.User.FullName;

                    results.Add(model);

                }
                return results;
            }
        }
    }
}