using Newtonsoft.Json;
using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class UserMenuAccessController : Controller
    {
        
        public ActionResult Index()
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
       
        public ActionResult LoadMenuAccess(int? AccessID)
        {
            //  int[] data = new int[10];
            int[] SelectedList=null;
           
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
           
            UserMenuAccessModel.UserMenuAccessNew objUmNew = new UserMenuAccessModel.UserMenuAccessNew();
            UserRoleModel userRole = new UserRoleModel();
            var userRoleData = userRole.GetUserRoleData(input: new UserRoleModel.GetUserRole
            {
                FK_UserRole = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                Name = "",
                TransMode = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            objUmNew.Role = userRoleData.Data;


            MenuGroupModel menuGroup = new MenuGroupModel();
            var mnuGrp = menuGroup.GetMenuGroupData(companyKey: _userLoginInfo.CompanyKey, input: new MenuGroupModel.GetMenuGroup
            {
                FK_MenuGroup = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine

            });

            MenuListModel menuList = new MenuListModel();
            
            var MenuInfo = menuList.GetMenuDataForMenuAccess(companyKey: _userLoginInfo.CompanyKey, input: new MenuListModel.GetMenuListForAccess
            {
                FK_MenuList = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });
            var MenuInfoData = MenuInfo.Data;


            if(AccessID!=null)
            {
                UserMenuAccessModel objMl = new UserMenuAccessModel();
                var MenuSelInfo = objMl.GetUserGroupMenuAccessData(companyKey: _userLoginInfo.CompanyKey, input: new UserMenuAccessModel.GetUserGroupMenuAccess
                {
                    FK_UserGroupMenuAccess = (long)AccessID,
                    FK_Company = _userLoginInfo.FK_Company,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine
                });
                string UsrGrpMnuLst = MenuSelInfo.Data[0].UsrGrpMnuLst;
                SelectedList = UsrGrpMnuLst.Split(',').Select(Int32.Parse).ToArray();
             
            }


            bool itemSelected = false;
            List<UserMenuAccessModel.TreeViewNode> treeMenuList = new List<UserMenuAccessModel.TreeViewNode>();
            foreach (var grp in mnuGrp.Data)
            {
                treeMenuList.Add(new UserMenuAccessModel.TreeViewNode {
                    id = "M"+grp.ID_MenuGroup,
                    text = grp.MnuGrpName,
                    parent = "#",
                    state = new UserMenuAccessModel.TreeViewNodeState() { selected = false, opened = false, checkbox_disabled = true }
                });
                foreach (var subSecond in MenuInfoData.Where(m => m.FK_MenuGroup == grp.ID_MenuGroup && m.FK_SubMenu == 0))
                {
                    itemSelected = false;
                    if(SelectedList !=null)
                    {
                        itemSelected = SelectedList.Contains(Convert.ToInt32(subSecond.ID_MenuList));
                    }
                   
                    treeMenuList.Add(new UserMenuAccessModel.TreeViewNode
                    {
                        id = subSecond.ID_MenuList.ToString(),
                        text = subSecond.MnuLstName,
                        parent = "M" + grp.ID_MenuGroup,
                        state = new UserMenuAccessModel.TreeViewNodeState() { selected = itemSelected, opened = false, checkbox_disabled = true }
                    });
                    foreach (var subThird in MenuInfoData.Where(m => m.FK_SubMenu == subSecond.ID_MenuList))
                    {
                        itemSelected = false;
                        if (SelectedList != null)
                        {
                            itemSelected = SelectedList.Contains(Convert.ToInt32(subThird.ID_MenuList));
                        }                            
                        treeMenuList.Add(new UserMenuAccessModel.TreeViewNode
                        {
                            id = subThird.ID_MenuList.ToString(),
                            text = subThird.MnuLstName,
                            parent = subThird.FK_SubMenu.ToString(),
                            state = new UserMenuAccessModel.TreeViewNodeState() { selected = itemSelected, opened = false, checkbox_disabled = true }
                        });
                    }
                }
            }
           
            ViewBag.JsonTreeData = (new JavaScriptSerializer()).Serialize(treeMenuList);
            return PartialView("_LoadMenuAccess", objUmNew);           
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddMenuAccess(UserMenuAccessModel.UserMenuAccessUpdate data)
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
            UserMenuAccessModel objAccess = new UserMenuAccessModel();
            var datresponse = objAccess.UpdateUserGroupMenuAccessData(input: new UserMenuAccessModel.UpdateUserGroupMenuAccess
            {
                UserAction = 1,
                Debug = 0,
                ID_UserGroupMenuAccess = 0,
                UsrGrpMnuLst = GetSelectedList(data.SelectedNodes),
                FK_UserGroup = data.FK_UserGroup,              
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_UserGroupMenuAccess = 0
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetMenuAccessListData(int pageSize, int pageIndex, string Name)
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
            UserMenuAccessModel objMl = new UserMenuAccessModel();
            var MenuInfo = objMl.GetUserGroupMenuAccessData(companyKey: _userLoginInfo.CompanyKey, input: new UserMenuAccessModel.GetUserGroupMenuAccess
            {
                FK_UserGroupMenuAccess = 0,
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
        public ActionResult GetMenuAccessInfo(UserMenuAccessModel.UserGroupMenuAccessID data)
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
            UserMenuAccessModel objMl = new UserMenuAccessModel();
            var MenuInfo = objMl.GetUserGroupMenuAccessData(companyKey: _userLoginInfo.CompanyKey, input: new UserMenuAccessModel.GetUserGroupMenuAccess
            {
                FK_UserGroupMenuAccess = data.ID_UserGroupMenuAccess,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });
            return Json(MenuInfo, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateMenuAccess(UserMenuAccessModel.UserMenuAccessUpdate data)
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
            UserMenuAccessModel objAccess = new UserMenuAccessModel();
            var datresponse = objAccess.UpdateUserGroupMenuAccessData(input: new UserMenuAccessModel.UpdateUserGroupMenuAccess
            {
                UserAction = 2,
                Debug = 0,
                ID_UserGroupMenuAccess = data.ID_UserGroupMenuAccess,
                UsrGrpMnuLst = GetSelectedList(data.SelectedNodes),
                FK_UserGroup = data.FK_UserGroup,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_UserGroupMenuAccess = 0
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        private string GetSelectedList(List<UserMenuAccessModel.SelectedNode> SelectedNodes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var select in SelectedNodes.Where(s => !s.ID.Contains("M")))           
            {
                sb.Append(select.ID + ",");
            }
            if(sb.Length>0)
            {
                sb.Remove(sb.Length - 1, 1);
            }          
            return sb.ToString();
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteMMenuAccess(UserMenuAccessModel.DeleteUserGroupMenuAccess data)
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

            UserMenuAccessModel MenuAccess = new UserMenuAccessModel();
            var datresponse = MenuAccess.DeleteUserGroupMenuAccessData(input: new UserMenuAccessModel.DeleteUserGroupMenuAccess
            {
                FK_UserGroupMenuAccess = data.FK_UserGroupMenuAccess,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.FK_Reason,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            },
                companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMenuAccessReasonList()
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
    }
}