using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vovinam.Areas.Api.Common
{
    public static class Constants
    {
        public static class MESSAGE
        {
            public const string GET_LIST_SUCCESS = "Lấy danh sách thành công.";
            public const string LOGIN_SUCCESS = "Đăng nhập thành công.";
            public const string ACCOUNT_CLOCK = "Tài khoản đã bị khóa.";
            public const string LOGIN_FAIL = "Sai tên đăng nhập hoạc mật khẩu.";

            public const string UPDATE_SUCCESS = "Cập nhật thành công.";

            public const string NO_CONTENT = "Không tìm thấy dữ liệu.";
            public const string LOCK_SUCCESS = "Khóa thành công.";
            public const string UNLOCK_SUCCESS = "Mở khóa thành công.";

            // change password
            public const string CHANGE_SUCCESS = "Đổi mật khẩu thành công.";
            public const string OLD_PASS_FAIL = "Mật khẩu cũ không đúng.";

        }
    }
}