using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Ajax;

namespace Vovinam.WebBackend.Common
{
    public class AjaxCommonOption : AjaxOptions
    {
        public AjaxCommonOption()
        {
            OnBegin = "OnBeginAjax";
            OnComplete = "OnCompleteAjax";
            OnSuccess = "OnSuccessAjax";
            OnFailure = "OnFailure";
        }
    }
}