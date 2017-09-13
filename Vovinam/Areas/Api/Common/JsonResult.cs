using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vovinam.Areas.Api.Common
{
    public class JsonResult
    {
        public JsonResult(ResultCode Code, string Message, object Data = null)
        {
            erorr_code = Code;
            message = Message;
            data = Data;
        }

        public JsonResult()
        {
            erorr_code = ResultCode.Success;
            message = string.Empty;
        }

        public ResultCode erorr_code { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }

    public enum ResultCode
    {
        Success = 200,
        AccountLock = 201,
        LogInFail = 202,
        NoContent = 203,
        Fail = 204,

    }

   
}