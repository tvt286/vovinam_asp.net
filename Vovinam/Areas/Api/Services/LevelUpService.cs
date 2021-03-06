﻿using System;
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
    public class LevelUpService
    {
        public static List<ExaminationModel> getExamination(int companyId)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var examinations = context.Examinations.Where(x => x.CompanyId == companyId && x.IsDeleted == false).ToList();
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

                levelups.OrderBy(x => x.Stt).ToList();

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
                    model.quyen.point = item.Quyen.Point;

                    if (item.Quyen.User != null)
                        model.quyen.user_name = item.Quyen.User.FullName;

                    model.doi_khang = new DoiKhangModel();
                    model.doi_khang.id = item.DoiKhang.Id;
                    model.doi_khang.point = item.DoiKhang.Point;

                    if (item.DoiKhang.User != null)
                        model.doi_khang.user_name = item.DoiKhang.User.FullName;

                    model.song_luyen = new SongLuyenModel();
                    model.song_luyen.id = item.SongLuyen.Id;
                    model.song_luyen.point = item.SongLuyen.Point;

                    if (item.SongLuyen.User != null)
                        model.song_luyen.user_name = item.SongLuyen.User.FullName;

                    model.vo_dao = new VoDaoModel();
                    model.vo_dao.id = item.VoDao.Id;
                    model.vo_dao.point = item.VoDao.Point;

                    if (item.VoDao.User != null)
                        model.vo_dao.user_name = item.VoDao.User.FullName;

                    model.the_luc = new TheLucModel();
                    model.the_luc.id = item.TheLuc.Id;
                    model.the_luc.point = item.TheLuc.Point;
                    if (item.TheLuc.User != null)
                        model.the_luc.user_name = item.TheLuc.User.FullName;
                    results.Add(model);

                }             
                return results;
            }
        }

        public static List<ResultModel> GetResults(int CompanyId, int examinationId, int levelId)
        {
            List<ResultModel> results = new List<ResultModel>();
            ResultModel male = new ResultModel();
            male.gender = "Nam";
            male.students = GetResultsByGender(CompanyId, examinationId, levelId, Gender.Male);

            ResultModel female = new ResultModel();
            female.gender = "Nữ";
            female.students = GetResultsByGender(CompanyId, examinationId, levelId, Gender.Female);

            results.Add(male);
            results.Add(female);

            return results;

        }
        public static List<LevelUpModel> GetResultsByGender(int CompanyId, int examinationId, int levelId, Gender gender)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var levelups = context.LevelUps.Where(x => x.ExaminationId == examinationId)
                    .Where(x => x.LevelId == levelId && x.Gender == gender)
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

                
                levelups.OrderByDescending(x => x.KetQua).OrderByDescending(x => x.Total).ToList();
               

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
                    model.quyen.point = item.Quyen.Point;

                    if (item.Quyen.User != null)
                        model.quyen.user_name = item.Quyen.User.FullName;

                    model.doi_khang = new DoiKhangModel();
                    model.doi_khang.id = item.DoiKhang.Id;
                    model.doi_khang.point = item.DoiKhang.Point;

                    if (item.DoiKhang.User != null)
                        model.doi_khang.user_name = item.DoiKhang.User.FullName;

                    model.song_luyen = new SongLuyenModel();
                    model.song_luyen.id = item.SongLuyen.Id;
                    model.song_luyen.point = item.SongLuyen.Point;

                    if (item.SongLuyen.User != null)
                        model.song_luyen.user_name = item.SongLuyen.User.FullName;

                    model.vo_dao = new VoDaoModel();
                    model.vo_dao.id = item.VoDao.Id;
                    model.vo_dao.point = item.VoDao.Point;

                    if (item.VoDao.User != null)
                        model.vo_dao.user_name = item.VoDao.User.FullName;

                    model.the_luc = new TheLucModel();
                    model.the_luc.id = item.TheLuc.Id;
                    model.the_luc.point = item.TheLuc.Point;
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

        public static void UpdatePoint(int levelUpId, double point, int userId, int pointType)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = context.Users.FirstOrDefault(x => x.Id == userId);
                var levelUp = context.LevelUps.FirstOrDefault(x => x.Id == levelUpId);

                // luu lich sử
                LevelUpHistory history = new LevelUpHistory();
                history.UserName = user.FullName;
                history.LevelUpId = levelUpId;
                
                history.StudentName = levelUp.Name;
                history.PointNew = point;
                if(pointType == 1)
                {
                    if (levelUp.CoBan.Point != 0)
                    {
                        NotificationHubs.Add(user.Id, string.Format("Cơ bản: {0} từ {1} -> {2}", levelUp.Name, levelUp.CoBan.Point, point));
                    }

                    history.Name = "Cơ bản";
                    history.PointOld = levelUp.CoBan.Point;
                    levelUp.CoBan.Point = point;
                    levelUp.CoBan.UserId = user.Id;
                  
                }
                else if (pointType == 2)
                {
                    if (levelUp.VoDao.Point != 0)
                    {
                        NotificationHubs.Add(user.Id, string.Format("Võ đạo: {0} từ {1} -> {2}", levelUp.Name, levelUp.VoDao.Point, point));

                    }
                    history.Name = "Võ đạo";
                    history.PointOld = levelUp.VoDao.Point;
                    levelUp.VoDao.Point = point;
                    levelUp.VoDao.UserId = user.Id;
                   
                }
                else if (pointType == 3)
                {
                    if (levelUp.TheLuc.Point != 0)
                    {
                        NotificationHubs.Add(user.Id, string.Format("Thể lực: {0} từ {1} -> {2}", levelUp.Name, levelUp.TheLuc.Point, point));

                    }
                    history.Name = "Thể lực";
                    history.PointOld = levelUp.TheLuc.Point;
                    levelUp.TheLuc.Point = point;
                    levelUp.TheLuc.UserId = user.Id;
                   
                }
                else if (pointType == 4)
                {
                    if (levelUp.Quyen.Point != 0)
                    {
                        NotificationHubs.Add(user.Id, string.Format("Quyền: {0} từ {1} -> {2}", levelUp.Name, levelUp.Quyen.Point, point));

                    }
                    history.Name = "Quyền";
                    history.PointOld = levelUp.Quyen.Point;
                    levelUp.Quyen.Point = point;
                    levelUp.Quyen.UserId = user.Id;
                   
                }
                else if (pointType == 5)
                {
                    if (levelUp.DoiKhang.Point != 0)
                    {
                        NotificationHubs.Add(user.Id, string.Format("Đối kháng: {0} từ {1} -> {2}", levelUp.Name, levelUp.DoiKhang.Point, point));

                    }
                    history.Name = "Đối kháng";
                    history.PointOld = levelUp.DoiKhang.Point;
                    levelUp.DoiKhang.Point = point;
                    levelUp.DoiKhang.UserId = user.Id;
                   
                }
                else if (pointType == 6)
                {
                    if (levelUp.SongLuyen.Point != 0)
                    {
                        NotificationHubs.Add(user.Id, string.Format("Song luyện: {0} từ {1} -> {2}", levelUp.Name, levelUp.SongLuyen.Point, point));
                    }
                    history.Name = "Song luyện";
                    history.PointOld = levelUp.SongLuyen.Point;
                    levelUp.SongLuyen.Point = point;
                    levelUp.SongLuyen.UserId = user.Id;
                    
                }

                LevelUpHistoryService.Create(history);
                if (levelUp.LevelId == 1)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.TheLuc.Point + levelUp.VoDao.Point + levelUp.CoBan.Point;
                else if (levelUp.LevelId == 2 || levelUp.LevelId == 3)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.DoiKhang.Point + levelUp.VoDao.Point + levelUp.CoBan.Point;
                else if (levelUp.LevelId == 4)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.DoiKhang.Point + levelUp.VoDao.Point + levelUp.CoBan.Point + levelUp.SongLuyen.Point;
                // set ket qua
                SetKetQua(levelUp);
              
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