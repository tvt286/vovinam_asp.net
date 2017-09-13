using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vovinam.Common
{
    public enum Permission
    {
        [Description("Xem Club")]
        Club_View = 1001,
        [Description("Tạo/Sửa Club")]
        Club_Create = 1002,
        [Description("Xóa Club")]
        Club_Delete = 1003,
       
        [Description("Xem User")]
        User_View = 2001,
        [Description("Tạo/Sửa user")]
        User_Create = 2002,
        [Description("Xóa User")]
        User_Delete = 2003,

        [Description("Xem danh sách vai trò")]
        GroupPermission_View = 2004,
        [Description("Tạo vai trò")]
        GroupPermission_Create = 2005,
        [Description("Xóa vai trò")]
        GroupPermission_Delete = 2006,

        [Description("Xem môn sinh khu A")]
        StudentKA_View = 3001,
        [Description("Tạo/Sửa môn sinh khu A")]
        StudentKA_Create = 3002,
        [Description("Xóa môn sinh khu A")]
        StudentKA_Delete = 3003,
        [Description("Xem học phí khu A")]
        TuitionKA_View = 3004,
        [Description("Sửa học phí khu A")]
        TuitionKA_Edit = 3005,

        [Description("Xem môn sinh khu B")]
        StudentKB_View = 4001,
        [Description("Tạo/Sửa môn sinh khu B")]
        StudentKB_Create = 4002,
        [Description("Xóa môn sinh khu B")]
        StudentKB_Delete = 4003,
        [Description("Xem học phí khu B")]
        TuitionKB_View = 4004,
        [Description("Sửa học phí khu B")]
        TuitionKB_Edit = 4005,

        [Description("Xem môn sinh Nông Lâm")]
        StudentNL_View = 5001,
        [Description("Tạo/Sửa môn sinh Nông Lâm")]
        StudentNL_Create = 5002,
        [Description("Xóa môn sinh Nông Lâm")]
        StudentNL_Delete = 5003,
        [Description("Xem học phí Nông Lâm")]
        TuitionNL_View = 5004,
        [Description("Sửa học phí Nông Lâm")]
        TuitionNL_Edit = 5005,

        [Description("Xem danh sách khóa thi")]
        ChamThi_View = 6001,
        [Description("Tạo danh sách khóa thi")]
        ChamThi_Import = 6002,
        [Description("Xuất kết quả khóa thi")]
        ChamThi_Export = 6003,

        [Description("Xem danh sách đối kháng")]
        ChamThi_ViewCompete = 6004,
        [Description("Xuất danh sách đối kháng")]
        ChamThi_ExportCompete = 6005,

        [Description("Cơ bản")]
        TuVe_CoBan = 7001,
        [Description("Quyền")]
        TuVe_Quyen = 7002,
        [Description("Võ đạo")]
        TuVe_VoDao = 7003,
        [Description("Thể lực")]
        TuVe_TheLuc = 7004,

        [Description("Cơ bản")]
        LamDai_CoBan = 8001,
        [Description("Quyền")]
        LamDai_Quyen = 8002,
        [Description("Võ đạo")]
        LamDai_VoDao = 8003,
        [Description("Đối kháng")]
        LamDai_DoiKhang = 8004,

        [Description("Cơ bản")]
        LamDaiI_CoBan = 9001,
        [Description("Quyền")]
        LamDaiI_Quyen = 9002,
        [Description("Võ đạo")]
        LamDaiI_VoDao = 9003,
        [Description("Đối kháng")]
        LamDaiI_DoiKhang = 9004,
        [Description("Song luyện")]
        LamDaiI_SongLuyen = 9005,

        [Description("Cơ bản")]
        LamDaiII_CoBan = 10001,
        [Description("Quyền")]
        LamDaiII_Quyen = 10002,
        [Description("Võ đạo")]
        LamDaiII_VoDao = 10003,
        [Description("Đối kháng")]
        LamDaiII_DoiKhang = 10004,
        [Description("Song luyện")]
        LamDaiII_SongLuyen = 10005,

        [Description("Xem Lịch sử")]
        History_View = 11001,

    }
}
