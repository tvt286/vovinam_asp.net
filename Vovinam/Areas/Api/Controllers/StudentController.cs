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
    public class StudentController : ApiController
    {
        StudentService studentService;

        public StudentController()
        {
            studentService = new StudentService();
        }

      [HttpGet]
        public object GetStudents(int club_id, int company_id)
        {

            List<StudentModel> students = new List<StudentModel>();
            students = studentService.GetStudents(club_id, company_id);

            if (students.Count > 0)
            {
                return Json(new JsonResult(ResultCode.Success, Constants.MESSAGE.GET_LIST_SUCCESS, students));
            }
            else
            {
                return Json(new JsonResult(ResultCode.NoContent, Constants.MESSAGE.NO_CONTENT));
            }

        }
    }
}