using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class CostCenterController : Controller
    {
        // GET: CostCenter

        #region[Index]
        public ActionResult Index(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            return View();
        }
        #endregion

        #region[LoadCostCenter]
        public ActionResult LoadCostCenter(string mtd)
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

            CostCenterModel costCenter = new CostCenterModel();
            CostCenterModel.CostCenterView view = new CostCenterModel.CostCenterView();

            CommonMethod commonMethod = new CommonMethod();
            ViewBag.PageTitle = commonMethod.DecryptString(mtd);

            return PartialView("_AddCostcenter", view);
        }
        #endregion

        #region[Addcostcenter]
        public ActionResult Addcostcenter(CostCenterModel.CostCenterView cost)
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

            CostCenterModel model = new CostCenterModel();

            var data = model.Updatecostcenterdetails(new CostCenterModel.CostcenterUpdate
            {
                 FK_AssignedTo = cost.FK_AssignedTo,
                 costDetails = cost.costDetails is null ? "" : Common.xmlTostring(cost.costDetails),
                 FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                 FK_Company = _userLoginInfo.FK_Company,
                 FK_Machine = _userLoginInfo.FK_Machine,
                 UserAction = 1,
                 EntrBy = _userLoginInfo.EntrBy,
                 Debug = 0,
                 FK_CostCenter = 0


            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[Updatecostcenter]
        public ActionResult Updatecostcenter(CostCenterModel.CostCenterView costdata)
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

            CostCenterModel model = new CostCenterModel();

            var datares = model.Updatecostcenterdetails(new CostCenterModel.CostcenterUpdate
            {
                FK_AssignedTo = costdata.FK_AssignedTo,
                costDetails = costdata.costDetails is null ? "" : Common.xmlTostring(costdata.costDetails),
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                UserAction = 2,
                EntrBy = _userLoginInfo.EntrBy,
                Debug = 0,
                FK_CostCenter = costdata.FK_CostCenter
             
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datares }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetCostcenterdetailsList]
        public ActionResult GetCostcenterdetailsList(int pageSize, int pageIndex, string Name)
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

           CostCenterModel costview = new CostCenterModel();

            var datares = costview.Getcostcenterdetails(input: new CostCenterModel.CostcenterInput
            {
               
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Name = Name,
                FK_CostCenter = 0,
               

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { datares.Process, datares.Data, pageIndex, pageSize, totalrecord = (datares.Data is null) ? 0 : datares.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetcostcenterReasonList]
        public ActionResult GetcostcenterReasonList()
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
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                TransMode = ""
            });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[Deletecostcenter]
        public ActionResult Deletecostcenter(CostCenterModel.DeleteInput data)
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

            CostCenterModel model = new CostCenterModel();

            var result = model.DeleteCostcenterdata(input: new CostCenterModel.DeleteCostcenter
            {
               
                FK_CostCenter = data.FK_CostCenter,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Reason = data.ReasonID,
                Debug = 0,

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = result }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetCostcenterInfoByID]
        public ActionResult GetCostcenterInfoByID(CostCenterModel.CostCenterOutput cost)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ModelState.Remove("ReasonID");
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

            CostCenterModel model = new CostCenterModel();

            var data = model.Getcostcentersubdetails(new CostCenterModel.CostcenterInput
            {
              
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = 0,
                PageSize = 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_CostCenter = cost.ID_CostCenter,
                Name = ""
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { data }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}