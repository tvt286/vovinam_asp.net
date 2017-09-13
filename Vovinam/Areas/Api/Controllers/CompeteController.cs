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

namespace Vovinam.Areas.Api.Controllers
{
    public class CompeteController : ApiController
    {
        [HttpGet]
        public object GetCompetes(int club_id, int company_id)
        {

            List<CompeteModel> competes = new List<CompeteModel>();
            competes = CompeteService.GetCompetes(club_id, company_id);

            if (competes.Count > 0)
            {
                return Json(new JsonResult(ResultCode.Success, Constants.MESSAGE.GET_LIST_SUCCESS, competes));
            }
            else
            {
                return Json(new JsonResult(ResultCode.NoContent, Constants.MESSAGE.NO_CONTENT));
            }

        }
    }
}
