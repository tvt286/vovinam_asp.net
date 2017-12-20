using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Vovinam.Areas.Api.Common;
using Vovinam.Areas.Api.Models;
using Vovinam.Areas.Api.Services;
using Vovinam.Data;
using Vovinam.Hubs;

namespace Vovinam.Areas.Api.Controllers
{
    public class CompeteController : ApiController
    {
        [HttpGet]
        public object GetCompetes(int examination_id, int company_id, Gender gender)
        {

            List<CompeteModel> competes = new List<CompeteModel>();
            competes = CompeteService.GetCompetes(company_id, examination_id, gender);

            if (competes.Count > 0)
            {
                return Json(new JsonResult(ResultCode.Success, Constants.MESSAGE.GET_LIST_SUCCESS, competes));
            }
            else
            {
                return Json(new JsonResult(ResultCode.NoContent, Constants.MESSAGE.NO_CONTENT));
            }

        }

        [HttpPost]
        public object UpdatePoint(int levelup_id_1, int levelup_id_2, int user_id, double point_1, double point_2)
        {
            CompeteService.UpdatePoint(levelup_id_1, levelup_id_2, point_1, point_2, user_id);
            return Json(new JsonResult(ResultCode.Success, Constants.MESSAGE.UPDATE_SUCCESS));

        }
    }
}
