using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vovinam.Areas.Api.Common;
using Vovinam.Areas.Api.Models;
using Vovinam.Areas.Api.Services;

namespace Vovinam.Areas.Api.Controllers
{
    public class LevelUpController : ApiController
    {

        LevelUpService levelService;

        public LevelUpController()
        {
            levelService = new LevelUpService();
        }

        [HttpGet]
        public object GetLevelUps(int examination_id, int company_id, int level_Id)
        {

            List<LevelUpModel> levelUps = new List<LevelUpModel>();
            levelUps = LevelUpService.GetLevelUp(company_id, examination_id, level_Id);

            if (levelUps != null)
            {
                return Json(new JsonResult(ResultCode.Success, Constants.MESSAGE.GET_LIST_SUCCESS, levelUps));
            }
            else
            {
                return Json(new JsonResult(ResultCode.NoContent, Constants.MESSAGE.NO_CONTENT));
            }

        }

        [HttpGet]
        public object GetExamination(int company_id)
        {

            List<ExaminationModel> examinations = new List<ExaminationModel>();
            examinations = LevelUpService.getExamination(company_id);

            if (examinations != null)
            {
                return Json(new JsonResult(ResultCode.Success, Constants.MESSAGE.GET_LIST_SUCCESS, examinations));
            }
            else
            {
                return Json(new JsonResult(ResultCode.NoContent, Constants.MESSAGE.NO_CONTENT));
            }

        }

        [HttpPost]
        public object UpdateCoBan(int levelup_id, int user_id, double point)
        {
            LevelUpService.UpdateCoBan(levelup_id,point,user_id);
            return Json(new JsonResult(ResultCode.Success, Constants.MESSAGE.UPDATE_SUCCESS));

        }

        [HttpPost]
        public object UpdateQuyen(int levelup_id, int user_id, double point)
        {
            LevelUpService.UpdateQuyen(levelup_id, point, user_id);
            return Json(new JsonResult(ResultCode.Success, Constants.MESSAGE.UPDATE_SUCCESS));

        }

        [HttpPost]
        public object UpdateVoDao(int levelup_id, int user_id, double point)
        {
            LevelUpService.UpdateVoDao(levelup_id, point, user_id);
            return Json(new JsonResult(ResultCode.Success, Constants.MESSAGE.UPDATE_SUCCESS));

        }

        [HttpPost]
        public object UpdateSongLuyen(int levelup_id, int user_id, double point)
        {
            LevelUpService.UpdateSongLuyen(levelup_id, point, user_id);
            return Json(new JsonResult(ResultCode.Success, Constants.MESSAGE.UPDATE_SUCCESS));

        }

        [HttpPost]
        public object UpdateDoiKhang(int levelup_id, int user_id, double point)
        {
            LevelUpService.UpdateDoiKhang(levelup_id, point, user_id);
            return Json(new JsonResult(ResultCode.Success, Constants.MESSAGE.UPDATE_SUCCESS));

        }

        [HttpPost]
        public object UpdateTheLuc(int levelup_id, int user_id, double point)
        {
            LevelUpService.UpdateTheLuc(levelup_id, point, user_id);
            return Json(new JsonResult(ResultCode.Success, Constants.MESSAGE.UPDATE_SUCCESS));

        }
    }
}
