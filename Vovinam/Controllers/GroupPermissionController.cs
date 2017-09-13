using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Vovinam.Common;
using Vovinam.Data;
using Vovinam.Services;
using Vovinam.WebBackend.Common;
using Vovinam.Models;


namespace Vovinam.Controllers
{
    public class GroupPermissionController : Controller
    {
        private vovinamEntities db = new vovinamEntities();

        [AuthorizeAdmin(Permissions = new[] { Permission.GroupPermission_View })]
        public ActionResult Index(GroupPermissionSearchModel searchModel)
        {
            if (Request.HttpMethod == "GET")
            {
                return View(searchModel);
            }
            var pagedList = GroupPermissionService.Search(searchModel.Name, searchModel.PageSize, searchModel.PageIndex);
            pagedList.SearchModel = searchModel;
            return PartialView("_List", pagedList);
        }

        [AuthorizeAdmin(Permissions = new[] { Permission.GroupPermission_Create })]
        public ActionResult Create()
        {
            var user = UserService.GetUserInfo();
            ViewBag.User = user;

            var data = new Group
            {
                CompanyId = user.CompanyId
            };

            if (user.IsAdminRoot)
            {
                ViewBag.CompanyId = new SelectList(CompanyService.GetAll(), "Id", "Name");
            }
            ViewBag.GroupId = UserService.GetGroups().Where(x => x.CompanyId == user.CompanyId).Select(x => new SelectListItem() { Text = x.Name, Value = x.GroupId.ToString() });
            return View(data);
        }

        // POST: /Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupId,Name,Description,DateCreated")] Group group, int[] selectedGroupPermission, int[] groupId)
        {
            var user = UserService.GetUserInfo();

            if (ModelState.IsValid)
            {
                group.DateCreated = DateTime.Now;
                group.CompanyId = user.CompanyId;
                db.Groups.Add(group);
                db.SaveChanges();

                var permissionId = new HashSet<int>();
                if (groupId != null)
                {
                    foreach (var item in groupId)
                    {
                        var permission = db.Group_Permission.Where(x => x.GroupId == item).ToList();
                        if (permission.Count > 0)
                        {
                            foreach (var groupPermission in permission)
                            {
                                permissionId.Add(groupPermission.PermissionId);
                            }
                        }
                    }
                }


                foreach (var i in permissionId)
                {
                    db.Group_Permission.Add(new Group_Permission()
                    {
                        GroupId = group.GroupId,
                        PermissionId = i,
                    });
                }
                db.SaveChanges();
                TempData["Message"] = "Thêm mới thành công .";
                // lưu lịch sử
                //var operation = HistoryOperation.Group_Create + "Name: " + group.Name;
                //HistoryService.Create(operation);
                return View("Index");
            }

            return View("Index");
        }

        [AuthorizeAdmin(Permission = Permission.GroupPermission_Create)]
        public ActionResult Edit(int? id)
        {
            var user = UserService.GetUserInfo();
            ViewBag.User = user;
            Group model = new Group()
            {
                CompanyId = user.CompanyId,
                Group_Permission = new List<Group_Permission>()
            };

            if (id != 0)
            {
                model = db.Groups.Include(x => x.Group_Permission).First(x => x.GroupId == id.Value);
            }


            if (user.IsAdminRoot)
            {
                ViewBag.CompanyId = new SelectList(CompanyService.GetAll(), "Id", "Name", model.CompanyId);
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupId,Name,Description,CompanyId", Exclude = "CreateDate")] Group group, string selectedPermissions)
        {
            var user = UserService.GetUserInfo();
            ViewBag.User = user;
            if (user.IsAdminRoot)
            {
                ViewBag.CompanyId = new SelectList(CompanyService.GetAll(), "Id", "Name", group.CompanyId);
            }

            int[] selectedGroupPermission = string.IsNullOrWhiteSpace(selectedPermissions) ? new int[0] : selectedPermissions.Split(',').Select(int.Parse).ToArray();
            ModelState.Remove("CreateDate");
            if (ModelState.IsValid)
            {
                if (group.GroupId == 0)
                {
                    group.DateCreated = DateTime.Now;
                    group.CompanyId = user.CompanyId;
                    db.Groups.Add(group);
                    db.SaveChanges();


                    foreach (var i in selectedGroupPermission)
                    {
                        db.Group_Permission.Add(new Group_Permission()
                        {
                            GroupId = group.GroupId,
                            PermissionId = i,
                        });
                    }
                    db.SaveChanges();
                    TempData["Message"] = "Thêm mới thành công .";

                    return View("Index");
                }
                var data = GroupPermissionService.Get(group.GroupId);
                ////lưu lịch sử
                //var operation = HistoryOperation.Group_Update + group.Name;
                //if (!group.Name.Equals(data.Name))
                //    operation += " - Tên nhóm: " + data.Name + " -> " + group.Name;
                //if (group.Description.NotEmpty() && data.Description.NotEmpty())
                //{
                //    if (!group.Description.Equals(data.Description))
                //        operation += " - Chú thích: " + data.Description + " -> " + group.Description;
                //}
                //else if (!group.Description.NotEmpty() && data.Description.NotEmpty())
                //    operation += " - Xóa Chú thích: " + data.Description;
                //else if (group.Description.NotEmpty() && !data.Description.NotEmpty())
                //    operation += " - Thêm Chú thích: " + group.Description;

                db.Groups.Attach(group);
                db.Entry(group).State = EntityState.Modified;
                db.Entry(group).Property(x => x.DateCreated).IsModified = false;

                Group _group = db.Groups.Include(x => x.Group_Permission).FirstOrDefault(x => x.GroupId == group.GroupId);
                if (_group == null)
                {
                    return HttpNotFound();
                }
                var deleted = _group.Group_Permission.Where(x => selectedGroupPermission.Contains(x.PermissionId) == false);
                //if (deleted.Count() > 0)
                //    operation += " - Xóa quyền trong nhóm: " + group.Name;
                foreach (var groupPermission in deleted.ToList())
                {
                    // lưu lịch sử
                    var des = EnumHelper.GetDescription((Permission)groupPermission.PermissionId);
                    //operation += " + " + des;
                    //db.Group_Permission.Remove(groupPermission);
                    ViewBag.message = "Cập nhật quyền thành công.";
                }
                var addNew = selectedGroupPermission.Where(x => _group.Group_Permission.All(y => y.PermissionId != x));
                //if (addNew.Count() > 0)
                //    operation += " - Thêm quyền trong nhóm: " + group.Name;
                foreach (var permissionId in addNew)
                {
                    // lưu lịch sử
                    var des = EnumHelper.GetDescription((Permission)permissionId);
                    //operation += " + " + des;
                    var permission = new Group_Permission();
                    permission.GroupId = group.GroupId;
                    permission.PermissionId = permissionId;
                    db.Group_Permission.Add(permission);
                    ViewBag.message = "Cập nhật quyền thành công.";
                }
                //HistoryService.Create(operation);
                db.SaveChanges();
                return View(_group);
            }
            return View(group);
        }

        public ActionResult DeleteGroupPermission(string groupId)
        {
            if (groupId.IsEmpty())
            {
                return Json(new RedirectCommand
                {
                    Code = ResultCode.Fail,
                    Message = "Không có nhóm nào để xóa!",
                }, JsonRequestBehavior.AllowGet);
            }

            GroupPermissionService.DeleteGroup(groupId);
            return Json(new RedirectCommand
            {
                Code = ResultCode.Success,
                Message = "Đã xóa nhóm vai trò thành công!",
                Url = Url.Action("Index")
            }, JsonRequestBehavior.AllowGet);
        }
    }
}