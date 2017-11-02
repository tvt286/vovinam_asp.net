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
    public class LevelUpService
    {
        public static List<ExaminationModel> getExamination(int companyId)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var examinations = context.Examinations.Where(x => x.CompanyId == companyId).ToList();
                List<ExaminationModel> results = new List<ExaminationModel>();
                foreach (var item in examinations)
                {
                    ExaminationModel model = new ExaminationModel();
                    model.id = item.Id;
                    model.name = item.Name;
                    results.Add(model);
                }
                return results;
            }
        }

        public static List<LevelUpModel> GetLevelUp(int CompanyId, int examinationId, int levelId)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var levelups = context.LevelUps.Where(x => x.ExaminationId == examinationId)
					.Where(x => x.LevelId == levelId)
                    .Where(x => x.CompanyId == CompanyId)
                    .Include(x => x.Club)
                     .Include(x => x.Level)
                     .Include(x => x.CoBan)
                     .Include(x => x.Quyen)
                     .Include(x => x.VoDao)
                     .Include(x => x.SongLuyen)
                     .Include(x => x.TheLuc)
                     .Include(x => x.DoiKhang)
                     .Include(x => x.CoBan.User)
                     .Include(x => x.Quyen.User)
                     .Include(x => x.VoDao.User)
                     .Include(x => x.SongLuyen.User)
                     .Include(x => x.TheLuc.User)
                     .Include(x => x.DoiKhang.User)
                    .ToList();

                List<LevelUpModel> results = new List<LevelUpModel>();

                foreach (var item in levelups)
                {
                    LevelUpModel model = new LevelUpModel();
                    model.id = item.Id;
                    model.name = item.Name;
                    model.stt = item.Stt;
                    model.birthday = item.BirthDay;
                    model.gender = item.Gender.ToString();
                    model.weight = item.Weight;
                    model.total = item.Total;
                    model.ketqua = item.KetQua.ToString();

                    model.club = new ClubModel();
                    model.club.id = item.Club.Id;
                    model.club.name = item.Club.Name;

                    model.level = new LevelModel();
                    model.level.id = item.Level.Id;
                    model.level.name = item.Level.Name;

                    model.co_ban = new CoBanModel();
                    model.co_ban.point = item.CoBan.Point;
                    if (item.CoBan.User != null)
                        model.co_ban.user_name = item.CoBan.User.FullName;

                    model.quyen = new QuyenModel();
                    model.quyen.id = item.Quyen.Id;
                    if (item.Quyen.User != null)
                        model.quyen.user_name = item.Quyen.User.FullName;

                    model.doi_khang = new DoiKhangModel();
                    model.doi_khang.id = item.DoiKhang.Id;
                    if (item.DoiKhang.User != null)
                        model.doi_khang.user_name = item.DoiKhang.User.FullName;

                    model.song_luyen = new SongLuyenModel();
                    model.song_luyen.id = item.SongLuyen.Id;
                    if (item.SongLuyen.User != null)
                        model.song_luyen.user_name = item.SongLuyen.User.FullName;

                    model.vo_dao = new VoDaoModel();
                    model.vo_dao.id = item.VoDao.Id;
                    if (item.VoDao.User != null)
                        model.vo_dao.user_name = item.VoDao.User.FullName;

                    model.the_luc = new TheLucModel();
                    model.the_luc.id = item.TheLuc.Id;
                    if (item.TheLuc.User != null)
                        model.the_luc.user_name = item.TheLuc.User.FullName;
                    results.Add(model);

                }
                return results;
            }
        }

        public static void UpdateQuyen(int levelUpId, double point, int userId)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = context.Users.FirstOrDefault(x => x.Id == userId);
                var levelUp = context.LevelUps.FirstOrDefault(x => x.Id == levelUpId);

                // luu lich sử
                LevelUpHistory history = new LevelUpHistory();
                history.UserName = user.FullName;
                history.LevelUpId = levelUpId;
                history.Name = "Quyền";
                history.StudentName = levelUp.Name;
                history.PointOld = levelUp.Quyen.Point;
                history.PointNew = point;
                LevelUpHistoryService.Create(history);
                levelUp.Quyen.Point = point;
                if (levelUp.LevelId == 1)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.TheLuc.Point + levelUp.VoDao.Point + levelUp.CoBan.Point;
                else if (levelUp.LevelId == 2 || levelUp.LevelId == 3)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.DoiKhang.Point + levelUp.VoDao.Point + levelUp.CoBan.Point;
                else if (levelUp.LevelId == 4)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.DoiKhang.Point + levelUp.VoDao.Point + levelUp.CoBan.Point + levelUp.SongLuyen.Point;
                // set ket qua
                SetKetQua(levelUp);
              
                levelUp.Quyen.UserId = user.Id;
                context.SaveChanges();
            }
        }

        public static void UpdateCoBan(int levelUpId, double point, int userId)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = context.Users.FirstOrDefault(x => x.Id == userId);
                var levelUp = context.LevelUps.FirstOrDefault(x => x.Id == levelUpId);

                // luu lich sử
                LevelUpHistory history = new LevelUpHistory();
                history.UserName = user.FullName;
                history.LevelUpId = levelUpId;
                history.Name = "Cơ bản";
                history.StudentName = levelUp.Name;
                history.PointOld = levelUp.CoBan.Point;
                history.PointNew = point;
                LevelUpHistoryService.Create(history);
                levelUp.CoBan.Point = point;
                if (levelUp.LevelId == 1)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.TheLuc.Point + levelUp.VoDao.Point + levelUp.CoBan.Point;
                else if (levelUp.LevelId == 2 || levelUp.LevelId == 3)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.DoiKhang.Point + levelUp.VoDao.Point + levelUp.CoBan.Point;
                else if (levelUp.LevelId == 4)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.DoiKhang.Point + levelUp.VoDao.Point + levelUp.CoBan.Point + levelUp.SongLuyen.Point;
                // set ket qua
                SetKetQua(levelUp);
              
                levelUp.CoBan.UserId = user.Id;
                context.SaveChanges();
            }
        }


        public static void UpdateVoDao(int levelUpId, double point, int userId)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = context.Users.FirstOrDefault(x => x.Id == userId);
                var levelUp = context.LevelUps.FirstOrDefault(x => x.Id == levelUpId);

                // luu lich sử
                LevelUpHistory history = new LevelUpHistory();
                history.UserName = user.FullName;
                history.LevelUpId = levelUpId;
                history.Name = "Võ đạo";
                history.StudentName = levelUp.Name;
                history.PointOld = levelUp.VoDao.Point;
                history.PointNew = point;
                LevelUpHistoryService.Create(history);
                levelUp.VoDao.Point = point;
                if (levelUp.LevelId == 1)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.TheLuc.Point + levelUp.VoDao.Point + levelUp.CoBan.Point;
                else if (levelUp.LevelId == 2 || levelUp.LevelId == 3)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.DoiKhang.Point + levelUp.VoDao.Point + levelUp.CoBan.Point;
                else if (levelUp.LevelId == 4)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.DoiKhang.Point + levelUp.VoDao.Point + levelUp.CoBan.Point + levelUp.SongLuyen.Point;
                // set ket qua
                SetKetQua(levelUp);
         
                levelUp.VoDao.UserId = user.Id;
                context.SaveChanges();
            }
        }

        public static void UpdateSongLuyen(int levelUpId, double point, int userId)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = context.Users.FirstOrDefault(x => x.Id == userId);
                var levelUp = context.LevelUps.FirstOrDefault(x => x.Id == levelUpId);

                // luu lich sử
                LevelUpHistory history = new LevelUpHistory();
                history.UserName = user.FullName;
                history.LevelUpId = levelUpId;
                history.Name = "Song luyện";
                history.StudentName = levelUp.Name;
                history.PointOld = levelUp.SongLuyen.Point;
                history.PointNew = point;
                LevelUpHistoryService.Create(history);
                levelUp.SongLuyen.Point = point;
                if (levelUp.LevelId == 1)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.TheLuc.Point + levelUp.VoDao.Point + levelUp.CoBan.Point;
                else if (levelUp.LevelId == 2 || levelUp.LevelId == 3)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.DoiKhang.Point + levelUp.VoDao.Point + levelUp.CoBan.Point;
                else if (levelUp.LevelId == 4)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.DoiKhang.Point + levelUp.VoDao.Point + levelUp.CoBan.Point + levelUp.SongLuyen.Point;
                // set ket qua
                SetKetQua(levelUp);              
                levelUp.SongLuyen.UserId = user.Id;
                context.SaveChanges();
            }
        }


        public static void UpdateTheLuc(int levelUpId, double point, int userId)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = context.Users.FirstOrDefault(x => x.Id == userId);
                var levelUp = context.LevelUps.FirstOrDefault(x => x.Id == levelUpId);

                // luu lich sử
                LevelUpHistory history = new LevelUpHistory();
                history.UserName = user.FullName;
                history.LevelUpId = levelUpId;
                history.Name = "Thể lực";
                history.StudentName = levelUp.Name;
                history.PointOld = levelUp.TheLuc.Point;
                history.PointNew = point;
                LevelUpHistoryService.Create(history);
                levelUp.TheLuc.Point = point;
                if (levelUp.LevelId == 1)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.TheLuc.Point + levelUp.VoDao.Point + levelUp.CoBan.Point;
                else if (levelUp.LevelId == 2 || levelUp.LevelId == 3)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.DoiKhang.Point + levelUp.VoDao.Point + levelUp.CoBan.Point;
                else if (levelUp.LevelId == 4)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.DoiKhang.Point + levelUp.VoDao.Point + levelUp.CoBan.Point + levelUp.SongLuyen.Point;
                // set ket qua
                SetKetQua(levelUp);             
                levelUp.TheLuc.UserId = user.Id;
                context.SaveChanges();
            }
        }

        public static void UpdateDoiKhang(int levelUpId, double point, int userId)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = context.Users.FirstOrDefault(x => x.Id == userId);
                var levelUp = context.LevelUps.FirstOrDefault(x => x.Id == levelUpId);

                // luu lich sử
                LevelUpHistory history = new LevelUpHistory();
                history.UserName = user.FullName;
                history.LevelUpId = levelUpId;
                history.Name = "Đối kháng";
                history.StudentName = levelUp.Name;
                history.PointOld = levelUp.DoiKhang.Point;
                history.PointNew = point;
                LevelUpHistoryService.Create(history);
                levelUp.DoiKhang.Point = point;
                if (levelUp.LevelId == 1)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.TheLuc.Point + levelUp.VoDao.Point + levelUp.CoBan.Point;
                else if (levelUp.LevelId == 2 || levelUp.LevelId == 3)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.DoiKhang.Point + levelUp.VoDao.Point + levelUp.CoBan.Point;
                else if (levelUp.LevelId == 4)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.DoiKhang.Point + levelUp.VoDao.Point + levelUp.CoBan.Point + levelUp.SongLuyen.Point;
                // set ket qua
                SetKetQua(levelUp);
                levelUp.DoiKhang.UserId = user.Id;
                context.SaveChanges();
            }
        }

        public static void SetKetQua(LevelUp item)
        {

            if (item.LevelId == 1 && item.Quyen.Point != 0.0 && item.TheLuc.Point != 0.0 && item.CoBan.Point != 0.0 && item.VoDao.Point != 0.0)
            {
                if (item.CoBan.Point == 0.0 || item.TheLuc.Point == 0.0 || item.VoDao.Point == 0.0 || item.Quyen.Point == 0.0)
                    item.KetQua = KetQua.CapNhat;
                else if (item.CoBan.Point <= 4 || item.TheLuc.Point <= 4 || item.VoDao.Point <= 4 || item.Quyen.Point <= 4)
                    item.KetQua = KetQua.Rot;
                else if ((item.CoBan.Point <= 4.5 && item.TheLuc.Point <= 4.5)
               || (item.CoBan.Point <= 4.5 && item.VoDao.Point <= 4.5)
               || (item.CoBan.Point <= 4.5 && item.Quyen.Point <= 4.5)
               || (item.TheLuc.Point <= 4.5 && item.VoDao.Point <= 4.5)
               || (item.TheLuc.Point <= 4.5 && item.Quyen.Point <= 4.5)
               || (item.VoDao.Point <= 4.5 && item.Quyen.Point <= 4.5))
                    item.KetQua = KetQua.Rot;
                else if (item.CoBan.Point <= 4.5 || item.TheLuc.Point <= 4.5 || item.VoDao.Point <= 4.5 || item.Quyen.Point <= 4.5)
                    item.KetQua = KetQua.Vot;
                else
                    item.KetQua = KetQua.Dau;
            }
            else if ((item.LevelId == 2 || item.LevelId == 3) && item.Quyen.Point != 0.0 && item.DoiKhang.Point != 0.0 && item.CoBan.Point != 0.0 && item.VoDao.Point != 0.0)
            {
                if (item.CoBan.Point == 0.0 || item.DoiKhang.Point == 0.0 || item.VoDao.Point == 0.0 || item.Quyen.Point == 0.0)
                    item.KetQua = KetQua.CapNhat;
                else if (item.CoBan.Point <= 4 || item.DoiKhang.Point <= 4 || item.VoDao.Point <= 4 || item.Quyen.Point <= 4)
                    item.KetQua = KetQua.Rot;
                else if ((item.CoBan.Point <= 4.5 && item.DoiKhang.Point <= 4.5)
               || (item.CoBan.Point <= 4.5 && item.VoDao.Point <= 4.5)
               || (item.CoBan.Point <= 4.5 && item.Quyen.Point <= 4.5)
               || (item.DoiKhang.Point <= 4.5 && item.VoDao.Point <= 4.5)
               || (item.DoiKhang.Point <= 4.5 && item.Quyen.Point <= 4.5)
               || (item.VoDao.Point <= 4.5 && item.Quyen.Point <= 4.5))
                    item.KetQua = KetQua.Rot;
                else if (item.CoBan.Point <= 4.5 || item.DoiKhang.Point <= 4.5 || item.VoDao.Point <= 4.5 || item.Quyen.Point <= 4.5)
                    item.KetQua = KetQua.Vot;
                else
                    item.KetQua = KetQua.Dau;

            }
            else if ((item.LevelId == 4) && item.Quyen.Point != 0.0 && item.DoiKhang.Point != 0.0 && item.CoBan.Point != 0.0 && item.VoDao.Point != 0.0 && item.SongLuyen.Point != 0.0)
            {
                if (item.CoBan.Point == 0.0 || item.DoiKhang.Point == 0.0 || item.VoDao.Point == 0.0 || item.Quyen.Point == 0.0 || item.SongLuyen.Point == 0.0)
                    item.KetQua = KetQua.CapNhat;
                else if (item.CoBan.Point <= 4 || item.DoiKhang.Point <= 4 || item.VoDao.Point <= 4 || item.Quyen.Point <= 4 || item.SongLuyen.Point <= 4)
                    item.KetQua = KetQua.Rot;
                else if ((item.CoBan.Point <= 4.5 && item.DoiKhang.Point <= 4.5)
               || (item.CoBan.Point <= 4.5 && item.VoDao.Point <= 4.5)
               || (item.CoBan.Point <= 4.5 && item.Quyen.Point <= 4.5)
               || (item.CoBan.Point <= 4.5 && item.SongLuyen.Point <= 4.5)
               || (item.DoiKhang.Point <= 4.5 && item.VoDao.Point <= 4.5)
               || (item.DoiKhang.Point <= 4.5 && item.Quyen.Point <= 4.5)
               || (item.DoiKhang.Point <= 4.5 && item.SongLuyen.Point <= 4.5)
               || (item.VoDao.Point <= 4.5 && item.Quyen.Point <= 4.5)
               || (item.VoDao.Point <= 4.5 && item.SongLuyen.Point <= 4.5)
               || (item.Quyen.Point <= 4.5 && item.SongLuyen.Point <= 4.5))
                    item.KetQua = KetQua.Rot;
                else if (item.CoBan.Point <= 4.5 || item.DoiKhang.Point <= 4.5 || item.VoDao.Point <= 4.5 || item.Quyen.Point <= 4.5 || item.SongLuyen.Point <= 4.5)
                    item.KetQua = KetQua.Vot;
                else
                    item.KetQua = KetQua.Dau;
            }
        }

    }
}