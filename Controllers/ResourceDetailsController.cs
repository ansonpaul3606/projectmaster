using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.Business;
using PerfectWebERP.DataAccess;
using PerfectWebERP.Interface;
using PerfectWebERP.Models;
using PerfectWebERP.Services;
using System.Data;
using Newtonsoft.Json;
using PerfectWebERP.General;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls;
using PerfectWebERP.Filters;

namespace PerfectWebERP.Controllers
{
    public class ResourceDetailsController : Controller
    {
        // GET: ResourceDetails
        public ActionResult Index(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            CommonMethod objCmnMethod = new CommonMethod();
            //string mGrp = objCmnMethod.DecryptString(mgrp);
            //ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            ViewBag.PagedAccessRights = pageAccess;

            return View();
        }

        public ActionResult LoadResourceDetailsForm(string mtd)
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
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            ResourceDetailsModel.ResourceDetailsListModel ResourceDetailslist = new ResourceDetailsModel.ResourceDetailsListModel();

            APIParameters apiParameCate = new APIParameters
            {
                TableName = "[ProjectStages]",
                SelectFields = "[ID_ProjectStages] AS StageID,[PrStName] AS StageName",
                Criteria = "Passed=1 And Cancelled=0 AND TransMode='PRST' AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var Stages = Common.GetDataViaQuery<ResourceDetailsModel.StageList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameCate);
            ResourceDetailslist.StageList = Stages.Data;

            var resource = Common.GetDataViaQuery<ResourceDetailsModel.ProjectResourceList>(parameters: new APIParameters
            {
                TableName = "ProjectResources",
                SelectFields = "ID_ProjectResources AS FK_ProjectResources ,ProResourceName AS ProjectResourcesName",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_ProjectResources",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey

            );
            ResourceDetailslist.ResourceList = resource.Data;

            CommonMethod objCmnMethod = new CommonMethod();

            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddResourceDetails", ResourceDetailslist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddResourceType(ResourceDetailsModel.ResourceDetailsView data)
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


            ResourceDetailsModel model = new ResourceDetailsModel();
            var datresponse = model.UpdateResource(input: new ResourceDetailsModel.UpdateResourseType
            {
                UserAction = 1,
                TransMode = data.TransMode,
                ID_ProjectResourceMarking = data.ID_ProjectResourceMarking,
                PrmMarkingDate = data.PrmMarkingDate,
                FK_Project = data.FK_Project,
                FK_ProjectStages = data.FK_ProjectStages,             
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_ProjectResourceMarking = data.FK_ProjectResourceMarking,
                PrmTotalAmount=data.PrmTotalAmount,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                ResourceTypeDetails = data.ResourceTypeDetails is null ? "" : Common.xmlTostring(data.ResourceTypeDetails),
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateResourceType(ResourceDetailsModel.ResourceDetailsView data)
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


            ResourceDetailsModel model = new ResourceDetailsModel();
            var datresponse = model.UpdateResource(input: new ResourceDetailsModel.UpdateResourseType
            {
                UserAction = 2,
                TransMode = data.TransMode,
                ID_ProjectResourceMarking = data.ID_ProjectResourceMarking,
                PrmMarkingDate = data.PrmMarkingDate,
                FK_Project = data.FK_Project,
                FK_ProjectStages = data.FK_ProjectStages,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_ProjectResourceMarking = data.FK_ProjectResourceMarking,
                PrmTotalAmount = data.PrmTotalAmount,
                ResourceTypeDetails = data.ResourceTypeDetails is null ? "" : Common.xmlTostring(data.ResourceTypeDetails),
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult GetResourceTypeList(int pageSize, int pageIndex, string Name, string TransModes)
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
            ResourceDetailsModel resourcetype = new ResourceDetailsModel();

            var resourcetypeInfo = resourcetype.GetResourceTypeData(companyKey: _userLoginInfo.CompanyKey, input: new ResourceDetailsModel.ResourceTypeID

            {
                FK_ProjectResourceMarking = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                //Search= Name,
                TransMode = TransModes
            });

            //  return Json(EmployeetransferInfo, JsonRequestBehavior.AllowGet);
            return Json(new { resourcetypeInfo.Process, resourcetypeInfo.Data, pageSize, pageIndex, totalrecord = (resourcetypeInfo.Data is null) ? 0 : resourcetypeInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public ActionResult GetResourceRequestInfo(ResourceDetailsModel.ResourceDetailsView data)
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
            #region :: Model validation  ::
            //removing a node in model while validating
            //--- Model validation 
            if (!ModelState.IsValid)
            {

                // since no need to continue just return
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

            #endregion :: Model validation  ::


            ResourceDetailsModel objprd = new ResourceDetailsModel();

            var mptableInfo = objprd.GetResourceData(companyKey: _userLoginInfo.CompanyKey, input: new ResourceDetailsModel.ResourceTypeID
            {
                FK_ProjectResourceMarking = data.ID_ProjectResourceMarking,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Detailed = 0,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode = data.TransMode
            });
            var subtable = objprd.GetRenewalSelectDatanew(companyKey: _userLoginInfo.CompanyKey, input: new ResourceDetailsModel.ResourceTypeID
            {
                FK_ProjectResourceMarking = data.ID_ProjectResourceMarking,
                Detailed = 1,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode

            });

           

            return Json(new { mptableInfo, subtable }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteResource(ResourceDetailsModel.ResourceDetailsView data)
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

            ResourceDetailsModel Material = new ResourceDetailsModel();


            Output datresponse = Material.DeleteResourceTypeData(input: new ResourceDetailsModel.DeleteResourceType
            {
                FK_ProjectResourceMarking = data.ID_ProjectResourceMarking,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.ReasonID,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Debug = 0,
                TransMode = data.TransMode,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetResourceReasonList()
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

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }

    }
}