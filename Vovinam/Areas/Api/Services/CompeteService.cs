using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Vovinam.Areas.Api.Models;
using Vovinam.Data;
using Vovinam.Hubs;

namespace Vovinam.Areas.Api.Services
{
    public class CompeteService
    {
        public static List<CompeteModel> GetCompetes(int CompanyId, int examinationId, Gender gender)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var competes = context.Competes.Where(x => x.ExaminationId == examinationId)
                    .Where(x => x.CompanyId == CompanyId && x.Gender == gender)
                    .Include(x => x.LevelUp)
                     .Include(x => x.LevelUp1)
                    .Include(x => x.LevelUp.DoiKhang)
                    .Include(x => x.LevelUp.DoiKhang.User)
                     .Include(x => x.LevelUp1.DoiKhang)
                     .Include(x => x.LevelUp1.DoiKhang.User)

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
                    model.levelup_1.doi_khang.point = item.LevelUp.DoiKhang.Point;

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
                    model.levelup_2.doi_khang.point = item.LevelUp1.DoiKhang.Point;

                    if (item.LevelUp1.DoiKhang.User != null)
                        model.levelup_2.doi_khang.user_name = item.LevelUp1.DoiKhang.User.FullName;

                    results.Add(model);

                }
                return results;
            }
        }


        public static void UpdatePoint(int levelUpId1, int levelUpId2, double point1, double point2, int userId)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = context.Users.FirstOrDefault(x => x.Id == userId);
                var levelUp1 = context.LevelUps.FirstOrDefault(x => x.Id == levelUpId1);
                var levelUp2 = context.LevelUps.FirstOrDefault(x => x.Id == levelUpId2);

                if(levelUp1.DoiKhang.Point != 0)
                {
                    NotificationHubs.Add(user.Id, string.Format("Đối kháng: {0} từ {1} -> {2}", levelUp1.Name, levelUp1.DoiKhang.Point, point1));

                }
                if (levelUp2.DoiKhang.Point != 0)
                {
                    NotificationHubs.Add(user.Id, string.Format("Đối kháng: {0} từ {1} -> {2}", levelUp2.Name, levelUp2.DoiKhang.Point, point2));

                }
                // luu lich sử 1
                LevelUpHistory history = new LevelUpHistory();
                history.UserName = user.FullName;
                history.LevelUpId = levelUpId1;

                history.StudentName = levelUp1.Name;
                history.PointNew = point1;
                                      
                history.Name = "Đối kháng";
                history.PointOld = levelUp1.DoiKhang.Point;
                levelUp1.DoiKhang.Point = point1;
                levelUp1.DoiKhang.UserId = user.Id;

                LevelUpHistoryService.Create(history);
                if (levelUp1.LevelId == 1)
                    levelUp1.Total = levelUp1.Quyen.Point + levelUp1.TheLuc.Point + levelUp1.VoDao.Point + levelUp1.CoBan.Point;
                else if (levelUp1.LevelId == 2 || levelUp1.LevelId == 3)
                    levelUp1.Total = levelUp1.Quyen.Point + levelUp1.DoiKhang.Point + levelUp1.VoDao.Point + levelUp1.CoBan.Point;
                else if (levelUp1.LevelId == 4)
                    levelUp1.Total = levelUp1.Quyen.Point + levelUp1.DoiKhang.Point + levelUp1.VoDao.Point + levelUp1.CoBan.Point + levelUp1.SongLuyen.Point;
                // set ket qua
                LevelUpService.SetKetQua(levelUp1);

                // luu lich sử 2
                LevelUpHistory history1 = new LevelUpHistory();
                history1.UserName = user.FullName;
                history1.LevelUpId = levelUpId2;

                history1.StudentName = levelUp2.Name;
                history1.PointNew = point2;

                history1.Name = "Đối kháng";
                history1.PointOld = levelUp2.DoiKhang.Point;
                levelUp2.DoiKhang.Point = point2;
                levelUp2.DoiKhang.UserId = user.Id;

                LevelUpHistoryService.Create(history1);
                if (levelUp2.LevelId == 1)
                    levelUp2.Total = levelUp2.Quyen.Point + levelUp2.TheLuc.Point + levelUp2.VoDao.Point + levelUp2.CoBan.Point;
                else if (levelUp2.LevelId == 2 || levelUp2.LevelId == 3)
                    levelUp2.Total = levelUp2.Quyen.Point + levelUp2.DoiKhang.Point + levelUp2.VoDao.Point + levelUp2.CoBan.Point;
                else if (levelUp2.LevelId == 4)
                    levelUp2.Total = levelUp2.Quyen.Point + levelUp2.DoiKhang.Point + levelUp2.VoDao.Point + levelUp2.CoBan.Point + levelUp2.SongLuyen.Point;
                // set ket qua
                LevelUpService.SetKetQua(levelUp2);

                context.SaveChanges();
            }
        }
    }
}