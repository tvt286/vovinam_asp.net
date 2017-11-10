using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vovinam.Areas.Api.Common;
using Vovinam.Areas.Api.Models;
using Vovinam.Areas.Api.Services;
using Vovinam.Data;

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
        public object GetResults(int examination_id, int company_id, int level_Id)
        {

            List<ResultModel> results = new List<ResultModel>();
            results = LevelUpService.GetResults(company_id, examination_id, level_Id);

            if (results != null)
            {
                return Json(new JsonResult(ResultCode.Success, Constants.MESSAGE.GET_LIST_SUCCESS, results));
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
        public object UpdatePoint(int levelup_id, int user_id, double point, int point_type)
        {
            LevelUpService.UpdatePoint(levelup_id, point, user_id, point_type);
            return Json(new JsonResult(ResultCode.Success, Constants.MESSAGE.UPDATE_SUCCESS));

        }

     
    }
}
