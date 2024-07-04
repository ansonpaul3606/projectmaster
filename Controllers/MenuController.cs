
using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult MenuGroup()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            return View();
        }
        public ActionResult LoadMenuGroup()
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue"},
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            MenuGroupModel objMg = new MenuGroupModel();
            MenuGroupModel.MenuGroupNew objMgnew = new MenuGroupModel.MenuGroupNew();
            var sort = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
                companyKey: _userLoginInfo.CompanyKey,
                procedureName: "ProGetNextNo",
                parameter: new NextSortOrder
                {
                    TableName = "MenuGroup",
                    FieldName = "SortOrder",
                    Debug = 0
                });

            objMgnew.Sort = sort.Data[0].NextNo;

            var moduleList = objMg.GeMenuGroupModuleData(input: new MenuGroupModel.ModuleCriteria
            {
                Mode=69
            }, companyKey: _userLoginInfo.CompanyKey);

            objMgnew.ModuleList = moduleList.Data;

            return PartialView("_LoadMenuGroup", objMgnew);
        }
        [HttpPost]
        public ActionResult GetMenuGroupList(int pageSize, int pageIndex, string Name)
        {
            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ModelState.Remove("ReasonID");
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            MenuGroupModel MenuGroup = new MenuGroupModel();
            var MenuInfo = MenuGroup.GetMenuGroupData(companyKey: _userLoginInfo.CompanyKey, input: new MenuGroupModel.GetMenuGroup
            {
                FK_MenuGroup = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name
            });
            return Json(new { MenuInfo.Process, MenuInfo.Data, pageSize, pageIndex, totalrecord = (MenuInfo.Data is null) ? 0 : MenuInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetMenuGroupInfo(MenuGroupModel.MenuGroupID data)
        {
            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ModelState.Remove("ReasonID");
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            MenuGroupModel MenuGroup = new MenuGroupModel();
            var MenuInfo = MenuGroup.GetMenuGroupData(companyKey: _userLoginInfo.CompanyKey, input: new MenuGroupModel.GetMenuGroup
            {
                FK_MenuGroup = data.ID_MenuGroup,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine

            });
            return Json(MenuInfo, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewMenuGroup(MenuGroupModel.MenuGroupView data)
        {
            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            ModelState.Remove("MenuGroupID");


            MenuGroupModel MenuGroup = new MenuGroupModel();
            var datresponse = MenuGroup.UpdateMenuGroupData(input: new MenuGroupModel.UpdateMenuGroup
            {
                UserAction = 1,
                Debug = 0,
                ID_MenuGroup = 0,
                MnuGrpName = data.MnuGrpName,
                SubModule = data.SubModule,
                MnuGrpVisible = data.MnuGrpVisible,
                SortOrder = data.SortOrder,
                MnuGrpIcon = data.MnuGrpIcon,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_MenuGroup = 0
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateMenuGroup(MenuGroupModel.MenuGroupView data)
        {
            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            MenuGroupModel MenuGroup = new MenuGroupModel();
            var datresponse = MenuGroup.UpdateMenuGroupData(input: new MenuGroupModel.UpdateMenuGroup
            {
                UserAction = 2,
                ID_MenuGroup = data.ID_MenuGroup,
                MnuGrpName = data.MnuGrpName,
                SubModule = data.SubModule,
                MnuGrpVisible = data.MnuGrpVisible,
                SortOrder = data.SortOrder,
                MnuGrpIcon = data.MnuGrpIcon,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteMenuGroup(MenuGroupModel.DeleteMenuGroup data)
        {
            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            MenuGroupModel MenuGroup = new MenuGroupModel();

            var datresponse = MenuGroup.DeleteMenuGroupData(input: new MenuGroupModel.DeleteMenuGroup
            {
                FK_MenuGroup = data.FK_MenuGroup,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.FK_Reason,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            },
                companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMenuReasonList()
        {

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            ReasonModel reason = new ReasonModel();

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID
            {
                FK_Reason = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy
            });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }

        /*----------------------------------Menu List-------------------------------------*/
        public ActionResult MenuList()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            return View();
        }
        public ActionResult LoadMenuList()
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            MenuListModel objMl = new MenuListModel();
            MenuListModel.MenuListNew objMlnew = new MenuListModel.MenuListNew();
            var sort = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
                companyKey: _userLoginInfo.CompanyKey,
                procedureName: "ProGetNextNo",
                parameter: new NextSortOrder
                {
                    TableName = "MenuList",
                    FieldName = "SortOrder",
                    Debug = 0
                });
            objMlnew.Sort = sort.Data[0].NextNo;
           

            Assembly asm = Assembly.GetExecutingAssembly();
            var controllerList = asm.GetTypes()
               .Where(type => typeof(Controller).IsAssignableFrom(type));
            List<MenuListModel.ControllerList> objList = new List<MenuListModel.ControllerList>();
            foreach (var temp in controllerList)
            {
                MenuListModel.ControllerList obj = new MenuListModel.ControllerList();
                obj.Name = temp.Name.Replace("Controller", "");
                objList.Add(obj);
            }
            objMlnew.ControllerListData = objList.AsEnumerable();

            
            var transModeList = objMl.GeMenuListTransModeData(input: new MenuListModel.GenTransMode { Mode = 33 }, companyKey: _userLoginInfo.CompanyKey);
            objMlnew.TransMode = transModeList.Data.AsEnumerable();

            MenuGroupModel MenuGroup = new MenuGroupModel();
            var MenuGroupInfo = MenuGroup.GetMenuGroupData(companyKey: _userLoginInfo.CompanyKey, input: new MenuGroupModel.GetMenuGroup
            {
                FK_MenuGroup = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                Name = ""
            });
            objMlnew.MenuGroups = MenuGroupInfo.Data;
            return PartialView("_LoadMenuList", objMlnew);
        }
        [HttpPost]
        public ActionResult FillAction(MenuListModel.ControllerData obj)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            var controller = obj.ControllerName + "Controller";
            var action = asm.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type) && type.Name == controller)
                .SelectMany(type => type.GetMethods())               
                .Where(method => method.IsPublic && !method.IsDefined(typeof(NonActionAttribute)) && method.ReturnType == typeof(ActionResult));
            List<MenuListModel.ActionList> objList = new List<MenuListModel.ActionList>();

            foreach (var temp in action)
            {
                MenuListModel.ActionList objAction = new MenuListModel.ActionList();
                objAction.Name = temp.Name;
                objList.Add(objAction);
            }
            return Json(objList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult FillParentMenu(MenuListModel.GetMenuListByMenuGroup obj)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            MenuListModel objMl = new MenuListModel();
            var ParentMenuList = objMl.GetMenuListDataByMenuGroup(input: new MenuListModel.GetMenuListByMenuGroup
            {
                FK_MenuList = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                Name = "",
                FK_MenuGroup=obj.FK_MenuGroup
            }, companyKey: _userLoginInfo.CompanyKey);
          

            return Json(ParentMenuList.Data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult FillMenuWithoutParent(MenuListModel.GetMenuListByMenuGroup obj)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            MenuListModel objMl = new MenuListModel();
            var ParentMenuList = objMl.GetMenuListDataWithoutParentByMenuGroup(input: new MenuListModel.GetMenuListByMenuGroup
            {
                FK_MenuList = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                Name = "",
                FK_MenuGroup = obj.FK_MenuGroup
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(ParentMenuList.Data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewMenuList(MenuListModel.MenuListView data)
        {
            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            ModelState.Remove("MenuGroupID");


            MenuListModel objMl = new MenuListModel();
            var datresponse = objMl.UpdateMenuListData(input: new MenuListModel.UpdateMenuList
            {
                UserAction = 1,
                Debug = 0,
                ID_MenuList = 0,
                MnuLstName = data.MnuLstName,
                FK_MenuGroup = data.FK_MenuGroup,
                ControllerName = data.ControllerName,
                Url = data.Url,
                MnuParameter = data.MnuParameter,
                TransMode = data.TransMode,
                MnuIcon = data.MnuIcon,
                MnuImage = data.MnuImage,
                FK_SubMenu = data.FK_SubMenu,
                SortOrder = data.SortOrder,
                MnuLstVisible = data.MnuLstVisible,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_MenuList = 0
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetMenuListData(int pageSize, int pageIndex, string Name)
        {
            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ModelState.Remove("ReasonID");
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            MenuListModel objMl = new MenuListModel();
            var MenuInfo = objMl.GetMenuData(companyKey: _userLoginInfo.CompanyKey, input: new MenuListModel.GetMenuList
            {
                FK_MenuList = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name
            });
            return Json(new { MenuInfo.Process, MenuInfo.Data, pageSize, pageIndex, totalrecord = (MenuInfo.Data is null) ? 0 : MenuInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetMenuListInfo(MenuListModel.MenuListID data)
        {
            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ModelState.Remove("ReasonID");
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            MenuListModel MenuListData = new MenuListModel();
            var MenuInfo = MenuListData.GetMenuDetailsData(input: new MenuListModel.GetMenuDetails
            {
                FK_MenuList = data.ID_MenuList,
            }
            , companyKey: _userLoginInfo.CompanyKey);
            return Json(MenuInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateMenuList(MenuListModel.MenuListView data)
        {
            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

           

            MenuListModel objMl = new MenuListModel();
            var datresponse = objMl.UpdateMenuListData(input: new MenuListModel.UpdateMenuList
            {
                UserAction = 2,
                Debug = 0,
                ID_MenuList = data.ID_MenuList,
                MnuLstName = data.MnuLstName,
                FK_MenuGroup = data.FK_MenuGroup,
                ControllerName = data.ControllerName,
                Url = data.Url,
                MnuParameter = data.MnuParameter,
                TransMode = data.TransMode,
                MnuIcon = data.MnuIcon,
                MnuImage = data.MnuImage,
                FK_SubMenu = data.FK_SubMenu,
                SortOrder = data.SortOrder,
                MnuLstVisible = data.MnuLstVisible,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_MenuList = 0
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteMenu(MenuListModel.DeleteMenu data)
        {
            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            MenuListModel MenuGroup = new MenuListModel();

            var datresponse = MenuGroup.DeleteMenuData(input: new MenuListModel.DeleteMenu
            {
                FK_MenuList = data.FK_MenuList,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.FK_Reason,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            },
                companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
    }
}