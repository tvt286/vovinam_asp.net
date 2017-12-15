using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vovinam.Data;
using Vovinam.Common;
using System.Data.Entity;

namespace Vovinam.Services
{
    public class LevelUpService
    {
        public static List<LevelUp> Gets(int? CompanyId, int ExaminationId, int LevelId)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                return context.LevelUps.Where(x => x.CompanyId == CompanyId)
                    .Where(x => x.ExaminationId == ExaminationId)
                    .Where(x => x.LevelId == LevelId)
                    .Include(x => x.Club)
                    .Include(x => x.CoBan)
                     .Include(x => x.Level)

                    .Include(x => x.Quyen)
                    .Include(x => x.SongLuyen)
                    .Include(x => x.TheLuc)
                    .Include(x => x.DoiKhang)
                    .Include(x => x.VoDao)
                    .OrderBy(x => x.Stt)
                    .ToList();
            }
        }

        public static LevelUp Get(int Id)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                return context.LevelUps.Where(x => x.Id == Id)                 
                    .Include(x => x.Club)
                    .Include(x => x.CoBan)
                     .Include(x => x.Level)

                    .Include(x => x.Quyen)
                    .Include(x => x.SongLuyen)
                    .Include(x => x.TheLuc)
                    .Include(x => x.DoiKhang)
                    .Include(x => x.VoDao)
                    .OrderBy(x => x.Stt)
                    .FirstOrDefault();
            }
        }
        public static void UpdateLevelUp(LevelUp data)
        {
            using (var context = new vovinamEntities())
            {
                context.LevelUps.Attach(data);
                context.Entry(data).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

  
        public static void UpdateQuyen(LevelUp levelUpOld, LevelUp levelUpNew)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = UserService.GetUserInfo();

                // luu lich sử
                LevelUpHistory history = new LevelUpHistory();
                history.UserName = user.FullName;
                history.LevelUpId = levelUpOld.Id;
                history.Name = "Quyền";
                history.StudentName = levelUpOld.Name;
                history.PointOld = levelUpOld.Quyen.Point;
                history.PointNew = levelUpNew.Quyen.Point;
                LevelUpHistoryService.Create(history);

                var quyen = context.Quyens.FirstOrDefault(x => x.Id == levelUpOld.Quyen.Id);
                quyen.Point = levelUpNew.Quyen.Point;
                quyen.UserId = user.Id;
                context.SaveChanges();
            }
        }

        public static void UpdateCoBan(LevelUp levelUpOld, LevelUp levelUpNew)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = UserService.GetUserInfo();

                // luu lich sử
                LevelUpHistory history = new LevelUpHistory();
                history.UserName = user.FullName;
                history.LevelUpId = levelUpOld.Id;
                history.Name = "Cơ bản";
                history.StudentName = levelUpOld.Name;
                history.PointOld = levelUpOld.CoBan.Point;
                history.PointNew = levelUpNew.CoBan.Point;
                LevelUpHistoryService.Create(history);

                var coban = context.CoBans.FirstOrDefault(x => x.Id == levelUpOld.CoBan.Id);
                coban.Point = levelUpNew.CoBan.Point;
                coban.UserId = user.Id;
                context.SaveChanges();
            }
        }

        public static void UpdateDoiKhang(LevelUp levelUpOld, LevelUp levelUpNew)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = UserService.GetUserInfo();

                // luu lich sử
                LevelUpHistory history = new LevelUpHistory();
                history.UserName = user.FullName;
                history.LevelUpId = levelUpOld.Id;
                history.Name = "Đối kháng";
                history.StudentName = levelUpOld.Name;
                history.PointOld = levelUpOld.DoiKhang.Point;
                history.PointNew = levelUpNew.DoiKhang.Point;
                LevelUpHistoryService.Create(history);
                var doikhang = context.DoiKhangs.FirstOrDefault(x => x.Id == levelUpOld.DoiKhang.Id);
                doikhang.Point = levelUpNew.DoiKhang.Point;
                doikhang.UserId = user.Id;

                context.SaveChanges();
            }
        }

        public static void UpdateTheLuc(LevelUp levelUpOld, LevelUp levelUpNew)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = UserService.GetUserInfo();
                LevelUpHistory history = new LevelUpHistory();
                history.UserName = user.FullName;
                history.LevelUpId = levelUpOld.Id;
                history.Name = "Thể lực";
                history.StudentName = levelUpOld.Name;
                history.PointOld = levelUpOld.TheLuc.Point;
                history.PointNew = levelUpNew.TheLuc.Point;
                LevelUpHistoryService.Create(history);
                var theluc = context.TheLucs.FirstOrDefault(x => x.Id == levelUpOld.TheLuc.Id);
                theluc.Point = levelUpNew.TheLuc.Point;
                theluc.UserId = user.Id;

                context.SaveChanges();
               
            }
        }

        public static void UpdateSongLuyen(LevelUp levelUpOld, LevelUp levelUpNew)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = UserService.GetUserInfo();
                LevelUpHistory history = new LevelUpHistory();
                history.UserName = user.FullName;
                history.LevelUpId = levelUpOld.Id;
                history.Name = "Song luyện";
                history.StudentName = levelUpOld.Name;
                history.PointOld = levelUpOld.SongLuyen.Point;
                history.PointNew = levelUpNew.SongLuyen.Point;
                LevelUpHistoryService.Create(history);
                var songluyen = context.SongLuyens.FirstOrDefault(x => x.Id == levelUpOld.SongLuyen.Id);
                songluyen.Point = levelUpNew.SongLuyen.Point;
                songluyen.UserId = user.Id;
                
                context.SaveChanges();

            }
        }


        public static void UpdateVoDao(LevelUp levelUpOld, LevelUp levelUpNew)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = UserService.GetUserInfo();

                // luu lich sử
                LevelUpHistory history = new LevelUpHistory();
                history.UserName = user.FullName;
                history.LevelUpId = levelUpOld.Id;
                history.Name = "Võ đạo";
                history.StudentName = levelUpOld.Name;
                history.PointOld = levelUpOld.VoDao.Point;
                history.PointNew = levelUpNew.VoDao.Point;
                LevelUpHistoryService.Create(history);
                var vodao = context.VoDaos.FirstOrDefault(x => x.Id == levelUpOld.VoDao.Id);
                vodao.Point = levelUpNew.VoDao.Point;
                vodao.UserId = user.Id;

                context.SaveChanges();
            }
        }


        public static void setKetQua(LevelUp data)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = UserService.GetUserInfo();
                var levelUp = context.LevelUps.FirstOrDefault(x => x.Id == data.Id);
                levelUp.Quyen.UserId = user.Id;
                if (levelUp.LevelId == 1)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.TheLuc.Point + levelUp.VoDao.Point + levelUp.CoBan.Point;
                else if (levelUp.LevelId == 2 || levelUp.LevelId == 3)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.DoiKhang.Point + levelUp.VoDao.Point + levelUp.CoBan.Point;
                else if (levelUp.LevelId == 4)
                    levelUp.Total = levelUp.Quyen.Point + levelUp.DoiKhang.Point + levelUp.VoDao.Point + levelUp.CoBan.Point + levelUp.SongLuyen.Point;
                LevelUpService.SetKetQua(levelUp);
                context.SaveChanges();
            }
        }

        public static PagedSearchList<LevelUp> Search(string Name, int? LevelId, int? ExaminationId, KetQua? ketqua,
          int pageSize, int pageIndex)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = UserService.GetUserInfo();
                IQueryable<LevelUp> query = context.LevelUps.AsNoTracking();

                if (user.IsAdminRoot == false)
                {
                    query = query.Where(x => x.CompanyId == user.CompanyId);
                }

                if (Name.NotEmpty())
                {
                    query = query.Where(x => x.Name.Contains(Name));
                }

                if (LevelId.HasValue)
                {
                    query = query.Where(x => x.LevelId == LevelId);
                }

                if (ExaminationId.HasValue)
                {
                    query = query.Where(x => x.ExaminationId == ExaminationId);
                }


                if (ketqua.HasValue)
                {
                    query = query.Where(x => x.KetQua == ketqua.Value);
                }


                query = query.Include(x => x.VoDao)
                    .Include(x => x.Quyen)
                    .Include(x => x.SongLuyen)
                    .Include(x => x.CoBan)
                    .Include(x => x.Club)
                    .Include(x => x.DoiKhang)
                    .Include(x => x.Level)
                    .Include(x => x.TheLuc)
                    .OrderBy(x => x.Id)
                    .OrderBy(x => x.LevelId);
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                return new PagedSearchList<LevelUp>(query, pageIndex, pageSize);
            }
        }


        public static List<LevelUp> GetThuKhoa(int? CompanyId, int ExaminationId, int levelId, Gender gender)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var maxPoint = 0.0;
                var list = new List<LevelUp>();

                if (levelId == 1)
                {
                    list = Gets(CompanyId, ExaminationId, levelId)
                       .Where(x => x.CoBan.Point != 0)
                       .Where(x => x.Quyen.Point != 0)
                       .Where(x => x.VoDao.Point != 0)
                       .Where(x => x.TheLuc.Point != 0)
                       .Where(x => x.Gender == gender).ToList();

                }
                else if (levelId == 3 || levelId == 2)
                {
                    list = Gets(CompanyId, ExaminationId, levelId)
                        .Where(x => x.CoBan.Point != 0)
                        .Where(x => x.Quyen.Point != 0)
                        .Where(x => x.VoDao.Point != 0)
                        .Where(x => x.DoiKhang.Point != 0).Where(x => x.Gender == gender).ToList();

                }
                else
                {
                    list = Gets(CompanyId, ExaminationId, levelId)
                        .Where(x => x.CoBan.Point != 0)
                        .Where(x => x.Quyen.Point != 0)
                        .Where(x => x.VoDao.Point != 0)
                        .Where(x => x.SongLuyen.Point != 0)
                        .Where(x => x.DoiKhang.Point != 0).Where(x => x.Gender == gender).ToList();
                }

                if (list.Count > 0)
                    maxPoint = list.Max(x => x.Total);
                if (maxPoint == 0)
                    return null;

                var danhsach = Gets(CompanyId, ExaminationId, levelId).Where(x => x.Gender == gender).Where(x => x.Total == maxPoint).ToList();
                foreach (var item in danhsach)
                {
                    SetKetQua(item);
                }
                if (danhsach.Count > 1)
                {
                    var maxCoBan = danhsach.Max(x => x.CoBan.Point);
                    List<LevelUp> thukhoaCoBan = new List<LevelUp>();
                    foreach (var item in danhsach)
                    {
                        if (item.CoBan.Point == maxCoBan)
                        {
                            thukhoaCoBan.Add(item);
                        }
                        
                    }

                    if (thukhoaCoBan.Count > 1)
                    {
                        var maxVoDao = thukhoaCoBan.Max(x => x.VoDao.Point);
                        List<LevelUp> thukhoaVoDao = new List<LevelUp>();
                        foreach (var item in thukhoaCoBan)
                        {
                            if (item.VoDao.Point == maxVoDao)
                            {
                                thukhoaVoDao.Add(item);
                            }
                        }

                        if (thukhoaVoDao.Count > 1)
                        {
                            var maxQuyen = thukhoaVoDao.Max(x => x.Quyen.Point);
                            List<LevelUp> thukhoaQuyen = new List<LevelUp>();
                            foreach (var item in thukhoaVoDao)
                            {
                                if (item.Quyen.Point == maxQuyen)
                                {
                                    thukhoaQuyen.Add(item);
                                }
                            }
                            return thukhoaQuyen;
                        }
                        return thukhoaVoDao;
                    }
                    return thukhoaCoBan;
                }
                return danhsach;
            }
        }

        public static void SetKetQua(LevelUp levelUp)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var item = context.LevelUps.FirstOrDefault(x => x.Id == levelUp.Id);
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
                context.SaveChanges();
            }
        }

        public static List<LevelUp> GetAKhoa(int? CompanyId, int ExaminationId, int levelId, Gender gender, List<LevelUp> ThuKhoa)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var listIdThuKhoa = new List<int>();

                if(ThuKhoa != null)
                    listIdThuKhoa = ThuKhoa.Select(r => r.Id).ToList();

                var maxPoint = 0.0;
                var list = new List<LevelUp>();
                if (levelId == 1)
                {
                    list = Gets(CompanyId, ExaminationId, levelId)
                       .Where(x => x.CoBan.Point != 0)
                       .Where(x => x.Quyen.Point != 0)
                       .Where(x => x.VoDao.Point != 0)
                       .Where(x => x.TheLuc.Point != 0)
                       .Where(x => !listIdThuKhoa.Contains(x.Id))
                       .Where(x => x.Gender == gender).ToList();

                }
                else if (levelId == 3 || levelId == 2)
                {
                    list = Gets(CompanyId, ExaminationId, levelId)
                        .Where(x => x.CoBan.Point != 0)
                        .Where(x => x.Quyen.Point != 0)
                        .Where(x => x.VoDao.Point != 0)
                        .Where(x => !listIdThuKhoa.Contains(x.Id))
                        .Where(x => x.DoiKhang.Point != 0).Where(x => x.Gender == gender).ToList();

                }
                else
                {
                    list = Gets(CompanyId, ExaminationId, levelId)
                        .Where(x => x.CoBan.Point != 0)
                        .Where(x => x.Quyen.Point != 0)
                        .Where(x => x.VoDao.Point != 0)
                        .Where(x => x.SongLuyen.Point != 0)
                        .Where(x => !listIdThuKhoa.Contains(x.Id))
                        .Where(x => x.DoiKhang.Point != 0).Where(x => x.Gender == gender).ToList();
                }

                if (list.Count > 0)
                    maxPoint = list.Max(x => x.Total);
                if (maxPoint == 0)
                    return null;
                var danhsach = Gets(CompanyId, ExaminationId, levelId).Where(x => !listIdThuKhoa.Contains(x.Id))
                    .Where(x => x.Gender == gender).Where(x => x.Total == maxPoint).ToList();

                if (danhsach.Count > 1)
                {
                    var maxCoBan = danhsach.Max(x => x.CoBan.Point);
                    List<LevelUp> akhoaCoBan = new List<LevelUp>();
                    foreach (var item in danhsach)
                    {
                        if (item.CoBan.Point == maxCoBan)
                        {
                            akhoaCoBan.Add(item);
                        }
                    }

                    if (akhoaCoBan.Count > 1)
                    {
                        var maxVoDao = akhoaCoBan.Max(x => x.VoDao.Point);
                        List<LevelUp> akhoaVoDao = new List<LevelUp>();
                        foreach (var item in akhoaCoBan)
                        {
                            if (item.VoDao.Point == maxVoDao)
                            {
                                akhoaVoDao.Add(item);
                            }
                        }

                        if (akhoaVoDao.Count > 1)
                        {
                            var maxQuyen = akhoaVoDao.Max(x => x.Quyen.Point);
                            List<LevelUp> akhoaQuyen = new List<LevelUp>();
                            foreach (var item in akhoaVoDao)
                            {
                                if (item.Quyen.Point == maxQuyen)
                                {
                                    akhoaQuyen.Add(item);
                                }
                            }
                            return akhoaQuyen;
                        }
                        return akhoaVoDao;
                    }
                    return akhoaCoBan;
                }
                return danhsach;
            }
        }


        public static void setThuKhoa(int Id)
        {
            using (var context = new vovinamEntities())
            {
                var thukhoa = context.LevelUps.FirstOrDefault(x => x.Id == Id);
                thukhoa.KetQua = KetQua.ThuKhoa;
                context.SaveChanges();
            }
        }



        public static void setAKhoa(int Id)
        {
            using (var context = new vovinamEntities())
            {
                var thukhoa = context.LevelUps.FirstOrDefault(x => x.Id == Id);
                thukhoa.KetQua = KetQua.AKhoa;
                context.SaveChanges();
            }
        }
        public static void Create(List<LevelUp> data)
        {
            using (var context = new vovinamEntities())
            {
                var user = UserService.GetUserInfo();

                foreach (var item in data)
                {
                    var coban = new CoBan();
                    context.CoBans.Add(coban);
                    var quyen = new Quyen();
                    context.Quyens.Add(quyen);
                    var songluyen = new SongLuyen();
                    context.SongLuyens.Add(songluyen);
                    var theluc = new TheLuc();
                    context.TheLucs.Add(theluc);
                    var vodao = new VoDao();
                    context.VoDaos.Add(vodao);
                    var doikhang = new DoiKhang();
                    context.DoiKhangs.Add(doikhang);
                    context.SaveChanges();

                    item.CobanId = coban.Id;
                    item.QuyenId = quyen.Id;
                    item.DoiKhangId = doikhang.Id;
                    item.VodaoId = vodao.Id;
                    item.TheLucId = theluc.Id;
                    item.SongLuyenId = songluyen.Id;
                    item.CompanyId = user.CompanyId;
                    context.LevelUps.Add(item);

                    context.SaveChanges();

                }

            }
        }


    }
}
