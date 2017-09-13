using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vovinam.Common.Enums
{
    public enum UploadFileStatus : int
    {
        Exception = -1,
        NotFile = 0,
        Success = 1,
        NotSupportExtension = 2,
        OverLimited = 3
    }
}
