using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Vovinam.Areas.Api.Common;
using Vovinam.Areas.Api.Models;
using Vovinam.Common;
using Vovinam.Data;
using System.Data.Entity;

namespace Vovinam.Areas.Api.Services
{
    public class UserService
    {
        public static object Login(string username, string password)
        {

            var result = new JsonResult
            {
                message = Constants.MESSAGE.LOGIN_SUCCESS,
                erorr_code = Common.ResultCode.Success
            };

            var passMD5 = Encryptor.MD5Hash(password);

            var user = UserService.Get(username);

            if (user != null && (!string.IsNullOrWhiteSpace(user.Password) && user.Password == passMD5))
            {
                if (user.Status == UserStatus.InActive)
                {
                    result.message = Constants.MESSAGE.ACCOUNT_CLOCK;
                    result.erorr_code = Common.ResultCode.AccountLock;
                }
                else
                {
                    UserModel userModel = new UserModel();
                    userModel.id = user.Id;
                    userModel.full_name = user.FullName;
                    userModel.user_name = user.UserName;
                    userModel.email = user.Email;
                    userModel.phone = user.Phone;
                    userModel.image = user.Image;
                    userModel.is_admin_root = user.IsAdminRoot;
                    userModel.is_admin_company = user.IsAdminCompany;

                    if (user.User_Permission != null)
                    {
                        List<UserPermissionModel> permissions = new List<UserPermissionModel>();
                        foreach (var item in user.User_Permission)
                        {
                            UserPermissionModel model = new UserPermissionModel();
                            model.permission_id = item.PermissionId;
                            model.user_id = item.UserId;
                            permissions.Add(model);
                        }
                        userModel.user_permission = permissions;
                    }
                    if (user.Groups != null)
                    {
                        List<GroupModel> groups = new List<GroupModel>();
                        foreach (var item in user.Groups)
                        {
                            GroupModel model = new GroupModel();
                            model.group_id = item.GroupId;
                            model.name = item.Name;
                            groups.Add(model);
                        }
                        userModel.group = groups;
                    }
                    result.data = userModel;
                }
            }
            else
            {
                result.message = Constants.MESSAGE.LOGIN_FAIL;
                result.erorr_code = Common.ResultCode.LogInFail;
            }

            return result;

        }

        public static List<UserModel> getUsers(int companyId)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var users = context.Users.Where(x => x.CompanyId == companyId 
                    && x.IsDeleted == false && x.IsAdminCompany == false).ToList();
                List<UserModel> results = new List<UserModel>();
                foreach (var user in users)
                {
                    UserModel userModel = new UserModel();
                    userModel.id = user.Id;
                    userModel.full_name = user.FullName;
                    userModel.user_name = user.UserName;
                    userModel.email = user.Email;
                    userModel.phone = user.Phone;
                    userModel.image = user.Image;
                    userModel.is_admin_root = user.IsAdminRoot;
                    userModel.is_admin_company = user.IsAdminCompany;
                    results.Add(userModel);
                    
                }
                return results;
            }
        }

        public static User Get(string userName)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                return context.Users.Include(x => x.User_Permission).Include(x => x.Groups)
                    .FirstOrDefault(x => x.UserName == userName);
            }
        }

        public static string LockAccount(int user_id)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var user = context.Users.FirstOrDefault(x => x.Id == user_id);
                if (user == null)
                    return Constants.MESSAGE.NO_CONTENT;
                else
                {
                    var message = String.Empty;
                    if (user.Status == UserStatus.Active)
                    {
                        message = Constants.MESSAGE.LOCK_SUCCESS;
                        user.Status = UserStatus.InActive;
                    }
                    else
                    {
                        message = Constants.MESSAGE.UNLOCK_SUCCESS;
                        user.Status = UserStatus.Active;
                    }
                    user.DateUpdated = DateTime.Now;
                    context.SaveChanges();
                    return message;
                }
            }
        }

        public static object ChangePassword(string user_name, string old_pass, string new_pass)
        {
            using (var context = new vovinamEntities(IsolationLevel.ReadUncommitted))
            {
                var result = new JsonResult
                {
                    message = Constants.MESSAGE.CHANGE_SUCCESS,
                    erorr_code = Common.ResultCode.Success
                };

                var passMD5 = Encryptor.MD5Hash(old_pass);
                var passNewMD5 = Encryptor.MD5Hash(new_pass);

                var user = context.Users.Where(x => x.Password == passMD5).Where(x => x.UserName.Contains(user_name)).FirstOrDefault();
                if (user == null)
                {
                    result.erorr_code = Common.ResultCode.Fail;
                    result.message = Constants.MESSAGE.OLD_PASS_FAIL;
                }
                else
                {
                    user.Password = passNewMD5;
                    user.DateUpdated = DateTime.Now;
                    context.SaveChanges();
                }

                return result;
            }
        }
    }

}