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
using PerfectWebERP.Filters;


namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class ComplaintCheckListController : Controller
    {
        // GET: ComplaintCheckList
        public ActionResult ComplaintCheckListIndex(string mtd)
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
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return View();
        }

        public ActionResult Complaintform(string mtd)
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

            ComplaintCheckListModel.ComplaintCheckList output = new ComplaintCheckListModel.ComplaintCheckList();

            var ComplainttList = Common.GetDataViaQuery<ComplaintCheckListModel.ComplaintList>(parameters: new APIParameters
            {
                TableName = "ComplaintList C",
                SelectFields = "C.ID_ComplaintList as ComplaintListID,C.CompntName as Complaint",
                Criteria = "C.Cancelled=0",
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey
            );

            output.ComplaintLists = ComplainttList.Data;
            var Category = Common.GetDataViaQuery<ComplaintCheckListModel.CategoryList>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS CategoryID ,CatName AS Category",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey

             );
            output.CategoryList = Category.Data;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddComplaintCheckList", output);
        }


        public ActionResult GetProductlist()
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


            var data = Common.GetDataViaQuery<ComplaintCheckListModel.Productlist>(parameters: new APIParameters
            {
                TableName = "Product p",
                SelectFields = "p.ID_Product as ProductID,p.ProdName as ProductName",
                Criteria = "p.Cancelled=0 and P.passed =1  AND P.FK_Company=" + _userLoginInfo.FK_Company,
              
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            return Json(data, JsonRequestBehavior.AllowGet);



        }

        [HttpPost]
        public ActionResult AddComplaintCheckList(ComplaintCheckListModel.ComplaintCheckListView data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ModelState.Remove("ComplaintCheckingDetails");
            ModelState.Remove("Product");
            ModelState.Remove("Product");
            ModelState.Remove("FK_Product");
            ModelState.Remove("Complaint");
            ModelState.Remove("");
            ModelState.Remove("");

            #region :: Model validation  ::
            //--- Model validation 
            if (!ModelState.IsValid)
            {
                List<string> errorList = new List<string>();

                //errorList = ModelState.Values.SelectMany(m => m.Errors)
                //                        .Select(e => e.ErrorMessage)
                //                        .ToList();

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

            ComplaintCheckListModel Comp = new ComplaintCheckListModel();

            var datresponse = Comp.UpdateComplaintCheckListData(input: new ComplaintCheckListModel.UpdateComplaintCheckList
            {
                UserAction = 1,
                Debug = 0,
                TransMode = data.TransMode,
                ID_ComplaintCheckList = data.ID_ComplaintCheckList,
                ChklstCheckingDetails=data.ComplaintCheckingDetails,
                FK_Product=data.FK_Product,
        
                FK_Complaint=data.FK_Complaint,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                BackupId = data.ID_ComplaintCheckList,
                FK_ComplaintCheckList=data.ID_ComplaintCheckList,
                FK_Category = data.CategoryID,

                ComplaintCheckListDetails = data.comlist is null ? "" : Common.xmlTostring(data.comlist),
               



            },

            companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult UpdatComplaintCheckList(ComplaintCheckListModel.ComplaintCheckListView data)
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


            //ModelState.Remove("comlist");
            //ModelState.Remove("ComplaintCheckingDetails");
            //ModelState.Remove("FK_Product");
            //ModelState.Remove("FK_Complaint");
            ModelState.Remove("ComplaintCheckingDetails");
            ModelState.Remove("Product");
            ModelState.Remove("Complaint");
            ModelState.Remove("");
            ModelState.Remove("");

            #region :: Model validation  ::
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

            ComplaintCheckListModel Comp = new ComplaintCheckListModel();

            var datresponse = Comp.UpdateComplaintCheckListData(input: new ComplaintCheckListModel.UpdateComplaintCheckList
            {


                UserAction = 2,
                Debug = 0,
                TransMode = data.TransMode,
                ID_ComplaintCheckList = data.ID_ComplaintCheckList,
                ChklstCheckingDetails = data.ComplaintCheckingDetails,
                FK_Product = data.FK_Product,

                FK_Complaint = data.FK_Complaint,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                BackupId = data.ID_ComplaintCheckList,
                FK_ComplaintCheckList = data.ID_ComplaintCheckList,
                FK_Category = data.CategoryID,

                ComplaintCheckListDetails = data.comlist is null ? "" : Common.xmlTostring(data.comlist),


            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

      
        public ActionResult GetComplaintCheckList(int pageSize, int pageIndex, string Name,string Transmode)
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

            ComplaintCheckListModel complaintlist = new ComplaintCheckListModel();

            var data = complaintlist.GetComplaintCheckListData(companyKey: _userLoginInfo.CompanyKey, input: new ComplaintCheckListModel.ComplaintCheckListID
            {
                FK_ComplaintCheckList = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                Name=Name,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                TransMode = Transmode,
                EntrBy=_userLoginInfo.EntrBy
            });

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetComplaintInfo(ComplaintCheckListModel.ComplaintCheckListView data)
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
            ModelState.Remove("ComplaintCheckingDetails");
            ModelState.Remove("FK_Product");
            ModelState.Remove("FK_Complaint");
            ModelState.Remove("comlist");




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



            ComplaintCheckListModel complaint = new ComplaintCheckListModel();

            var complInfo = complaint.GetComplaintCheckListData(companyKey: _userLoginInfo.CompanyKey, input: new ComplaintCheckListModel.ComplaintCheckListID
            {
                FK_ComplaintCheckList = data.ID_ComplaintCheckList,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            var Complaintdetails = complaint.GetComplaintDetails(companyKey: _userLoginInfo.CompanyKey, input: new ComplaintCheckListModel.ComplaintDetails
            {
                FK_Company =_userLoginInfo.FK_Company,
                FK_ComplaintCheckList=data.ID_ComplaintCheckList,

            });

            if (Complaintdetails.Process.IsProcess)
            {
                complInfo.Data[0].comlist = Complaintdetails.Data;
            }
            //if (!(complInfo.Data is  null))
            //{
            //    complInfo.Data[0].comlist = Complaintdetails.Data;
            //}

            return Json(complInfo, JsonRequestBehavior.AllowGet);


        }



        public ActionResult DeleteComplaint(ComplaintCheckListModel.DeleteView data)
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

            ComplaintCheckListModel mdl = new ComplaintCheckListModel();



            Output datresponse = mdl.DeleteComplaintCheckListData(input: new ComplaintCheckListModel.DeleteComplaintCheckList
            {
                FK_ComplaintCheckList = data.ID_ComplaintCheckList,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.ReasonID,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Debug = 0,
                TransMode = "",
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetComplaintCheckDeleteReasonList()
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

            ReasonModel reason = new ReasonModel();

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID
            { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy ,FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,FK_Machine=_userLoginInfo.FK_Machine,PageIndex=0,PageSize=0,TransMode=""});


            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };


            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }
    }
}