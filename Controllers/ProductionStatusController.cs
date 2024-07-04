/*----------------------------------------------------------------------
Created By	: Kavya K
Created On	: 05/01/2023
Purpose		: Job Card Assign
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
-------------------------------------------------------------------------*/
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
    public class ProductionStatusController : Controller
    {
        // GET: ProductionStatus
        public ActionResult ProductionStatus(string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            pageAccess = _userLoginInfo.PageAccessRights;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);

            ViewBag.TransMode = mGrp;
            ViewBag.PagedAccessRights = pageAccess;
            return View();
        }
        public ActionResult LoadProductionStatus(string ID_ProjectStages)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            var IsAdmin = _userLoginInfo.UsrrlAdmin == true ? 1 : 0;
            ProductionStatusModel.ProductionStatusView Assign = new ProductionStatusModel.ProductionStatusView();
            Assign.UserCode = _userLoginInfo.EntrBy;

            var StageList = Common.GetDataViaQuery<ProductionStatusModel.Stage>(parameters: new APIParameters
            {
                //TableName = "ProjectStages PS JOIN users U ON PS.FK_Company=U.FK_Company  JOIN UserRole UR ON UR.ID_UserRole=U.FK_UserRole",
                //SelectFields = "ID_ProjectStages ,PrStName",
                //Criteria = "PS.Cancelled=0  AND PS.Passed=1 AND PS.TransMode='PDST' AND U.UserCode ='" +_userLoginInfo.EntrBy + "' AND PS.FK_Company=" + _userLoginInfo.FK_Company + " AND PS.FK_BranchCodeUser=" + _userLoginInfo.FK_BranchCodeUser, 
                //GroupByFileds = "",
                //SortFields = ""
                TableName = "ProductionStage PS JOIN ProjectStages PS1 ON PS.FK_Stage = PS1.ID_ProjectStages" +
                " JOIN ProjectTeam PT ON PT.ID_ProjectTeam = PS.FK_Team" +

                " JOIN ProjectAttendanceDetails PA ON PA.FK_Master = PT.ID_ProjectTeam AND PT.TransMode = PA.TransMode" +

                " JOIN Employee E ON E.ID_Employee = PA.FK_Employee" +

                " LEFT JOIN Users U ON U.FK_Employee = E.ID_Employee AND U.Cancelled = 0" +

                " LEFT JOIN UserRole AS UR ON UR.ID_UserRole = U.FK_UserRole",
                SelectFields = "PS1.ID_ProjectStages, PS1.PrStName",
                Criteria = "PS.Cancelled = 0 AND PS.Passed = 1 AND PS.FK_Company = " + _userLoginInfo.FK_Company + " AND U.UserCode = CASE WHEN " + IsAdmin + " = 1 THEN U.UserCode ELSE '" + _userLoginInfo.EntrBy + "' END",
                GroupByFileds = "PS1.ID_ProjectStages, PS1.PrStName",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey
          );

            var TeamList = Common.GetDataViaQuery<ProductionStatusModel.Team>(parameters: new APIParameters
            {
                TableName = "ProductionStage PS JOIN ProjectStages PS1 ON PS.FK_Stage = PS1.ID_ProjectStages" +

" JOIN ProjectTeam PT ON PT.ID_ProjectTeam = PS.FK_Team" +

" JOIN ProjectAttendanceDetails PA ON PA.FK_Master = PT.ID_ProjectTeam AND PT.TransMode = PA.TransMode" +

" JOIN Employee E ON E.ID_Employee = PA.FK_Employee" +

" LEFT JOIN Users U ON U.FK_Employee = E.ID_Employee AND U.Cancelled = 0" +

" LEFT JOIN UserRole AS UR ON UR.ID_UserRole = U.FK_UserRole",
                SelectFields = " PT .ID_ProjectTeam, PT .ProjTeamName ",
                Criteria = " PS.Cancelled = 0 AND PS.Passed = 1 AND PS.FK_Company = " + _userLoginInfo.FK_Company + " AND U.UserCode = CASE WHEN " + IsAdmin + " = 1 THEN U.UserCode ELSE '" + _userLoginInfo.EntrBy + "' END  AND PS1.ID_ProjectStages=" + ID_ProjectStages,
                GroupByFileds = " PT .ID_ProjectTeam, PT .ProjTeamName",
                SortFields = ""
                //TableName = "ProjectTeam PT JOIN users U ON PT.FK_Company=U.FK_Company  JOIN UserRole UR ON UR.ID_UserRole=U.FK_UserRole",
                //SelectFields = "ID_ProjectTeam,ProjTeamName",
                //Criteria = "PT.Cancelled=0  AND PT.Passed=1 AND PT.TransMode='PDTC' AND U.UserCode ='" + _userLoginInfo.EntrBy + "' AND PT.FK_Company=" + _userLoginInfo.FK_Company + " AND PT.FK_BranchCodeUser=" + _userLoginInfo.FK_BranchCodeUser,
                //GroupByFileds = "",
                //SortFields = ""
            },
                      companyKey: _userLoginInfo.CompanyKey
                     );


            Assign.FK_Employee = _userLoginInfo.FK_Employee;
            var EmpName = Common.GetDataViaQuery<LeadGenerateModel.EmployeeInfo>(parameters: new APIParameters
            {


                TableName = "Employee",
                SelectFields = "ID_Employee,EmpFName",
                Criteria = "ID_Employee=" + _userLoginInfo.FK_Employee + "  AND Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Employee",
                GroupByFileds = ""


            },
      companyKey: _userLoginInfo.CompanyKey

         );
            Assign.EmployeeInfoList = EmpName.Data;






            Assign.StageList = StageList.Data;
            Assign.TeamList = TeamList.Data;

            //Assign.JobCardDetails = JobCardDetails.Data;
            return PartialView("_AddProductionStatus", Assign);
        }
        public ActionResult LoadProjectTeam(long ID_ProjectStages)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            var IsAdmin = _userLoginInfo.UsrrlAdmin == true ? 1 : 0;
            ProductionStatusModel.ProductionStatusView Assign = new ProductionStatusModel.ProductionStatusView();



            var TeamList = Common.GetDataViaQuery<ProductionStatusModel.Team>(parameters: new APIParameters
            {
                TableName = "FROM ProductionStage PS JOIN ProjectStages PS1 ON PS.FK_Stage = PS1.ID_ProjectStages" +

" JOIN ProjectTeam PT ON PT.ID_ProjectTeam = PS.FK_Team" +

" JOIN ProjectAttendanceDetails PA ON PA.FK_Master = PT.ID_ProjectTeam AND PT.TransMode = PA.TransMode" +

" JOIN Employee E ON E.ID_Employee = PA.FK_Employee" +

" LEFT JOIN Users U ON U.FK_Employee = E.ID_Employee AND U.Cancelled = 0" +

" LEFT JOIN UserRole AS UR ON UR.ID_UserRole = U.FK_UserRole",
                SelectFields = " PT .ID_ProjectTeam, PT .ProjTeamName ",
                Criteria = " PS.Cancelled = 0 AND PS.Passed = 1 AND PS.FK_Company = " + _userLoginInfo.FK_Company + " AND U.UserCode = CASE WHEN " + IsAdmin + " = 1 THEN U.UserCode ELSE '" + _userLoginInfo.EntrBy + "' END AND PS1.ID_ProjectStages=" + ID_ProjectStages,
                GroupByFileds = " PT .ID_ProjectTeam, PT .ProjTeamName",
                SortFields = ""

            },
                      companyKey: _userLoginInfo.CompanyKey
                     );




            return Json(new { TeamList = TeamList.Data }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetJobCards(ProductionStatusModel.ProductionStatusView data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            var JobCardDetails = new ProductionStatusModel().GetJobCardData(companyKey: _userLoginInfo.CompanyKey, input: new ProductionStatusModel.GetJobCards
            {
                TransMode = "",
                FK_Product = data.FK_Product,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                PageIndex = data.PageIndex + 1,
                PageSize = data.PageSize,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                Filterid = data.FilterId,
                TargetFrom = data.TargetFromDate,
                TargetTo = data.TargetToDate,
                FK_Employee = data.FK_Employee
            });
            return Json(new { JobCardDetails.Process, JobCardDetails.Data, data.PageSize, data.PageIndex, totalrecord = (JobCardDetails.Data is null) ? 0 : JobCardDetails.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetProductionStatusList(int pageSize, int pageIndex, string Name, string TransMode)
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
            ProductionStatusModel JobCard = new ProductionStatusModel();

            var JobCInfo = JobCard.GetProductionStatusData(companyKey: _userLoginInfo.CompanyKey, input: new ProductionStatusModel.GetProductionStatus
            {
                FK_JobCardFollowUp = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                //TransMode = TransMode,
                //Detailed = 0
            });

            return Json(new { JobCInfo.Process, JobCInfo.Data, pageSize, pageIndex, totalrecord = (JobCInfo.Data is null) ? 0 : JobCInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetProductionStatusMarkingDetails(long ID, long FK_Employee, long FK_ProjectStages, long FK_Product)
        {
            //CommonMethod objCmnMethod = new CommonMethod();
            //string mGrp = objCmnMethod.Decrypt(mgrp);
            //ViewBag.TransMode = mGrp;
            //ViewBag.TicketNo = ID;
            //ViewBag.mgrp = mgrp;
            //ViewBag.Service = ServiceID;


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ProductionStatusModel.ProductionStatusDetailsView Assign = new ProductionStatusModel.ProductionStatusDetailsView();



            var FollowUpDetailsList = new ProductionStatusModel().GetJobCardStatusMarkingDetailsData(companyKey: _userLoginInfo.CompanyKey, input: new ProductionStatusModel.GetJobCardAssignEmployees
            {

                FK_JobCardAssign = ID,
                FK_Employee = FK_Employee,
                FK_ProjectStages = Assign.FK_ProjectStages,
                FK_Product = Assign.FK_Product,
                Mode = 1
            }).Data;
            return Json(new { FollowUpDetailsList }, JsonRequestBehavior.AllowGet);



        }
        public ActionResult FillNewInputMaterial(long FK_Stock, long FK_Product)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ProductionStatusModel.ProductionStatusDetailsView Assign = new ProductionStatusModel.ProductionStatusDetailsView();
            var StockDetails = new ProductionStatusModel().GetJobCardStatusMarkingDetailsData(companyKey: _userLoginInfo.CompanyKey, input: new ProductionStatusModel.GetJobCardAssignEmployees
            {
                FK_Stock = FK_Stock,
                FK_Product = FK_Product,
                Mode = 2
            }).Data;

            return Json(new { StockDetails }, JsonRequestBehavior.AllowGet);


        }

        public ActionResult ProductionStatusDetails(long ID, long FK_Employee,long ID_JobCardFollowUp,long View)
        {
            //CommonMethod objCmnMethod = new CommonMethod();
            //string mGrp = objCmnMethod.Decrypt(mgrp);
            //ViewBag.TransMode = mGrp;
            //ViewBag.TicketNo = ID;
            //ViewBag.mgrp = mgrp;
            //ViewBag.Service = ServiceID;


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ProductionStatusModel.ProductionStatusDetailsView Assign = new ProductionStatusModel.ProductionStatusDetailsView();

            var JobCardDetails = new APIGetRecordsDynamic<ProductionStatusModel.ProductionStatusDetailsView>();
            if (ID_JobCardFollowUp > 0)
            {
                JobCardDetails = new ProductionStatusModel().GetProductionStatusData(companyKey: _userLoginInfo.CompanyKey, input: new ProductionStatusModel.GetProductionStatus
                {
                    
                    FK_JobCardFollowUp = ID_JobCardFollowUp,
                    FK_Company = _userLoginInfo.FK_Company,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    PageIndex = 0,
                    PageSize = 0,
                    Name = "",
                    //TransMode = TransMode,
                    //Detailed = 0
                });
            }
            else
            {
                JobCardDetails = new ProductionStatusModel().GetJobCardStatusMarkingData(companyKey: _userLoginInfo.CompanyKey, input: new ProductionStatusModel.GetJobCardAssignEmployees
                {

                    FK_JobCardAssign = ID,
                    FK_Employee = FK_Employee,
                    ID_JobCardFollowUp = ID_JobCardFollowUp

                });
            }

            if (JobCardDetails.Data != null)
            {
                if (JobCardDetails.Data.Count > 0)
                {
                    if (JobCardDetails.Data[0].ErrCode == 0)
                    {
                        Assign.JobCardNo = JobCardDetails.Data[0].JobCardNo;
                        Assign.JobCardDetails = JobCardDetails.Data[0].JobCardDetails;
                        Assign.ProductName = JobCardDetails.Data[0].ProductName;
                        Assign.JcaStartDate = JobCardDetails.Data[0].JcaStartDate;
                        Assign.JcaTargetDate = JobCardDetails.Data[0].JcaTargetDate;
                        Assign.ID_JobCardFollowUp = JobCardDetails.Data[0].ID_JobCardFollowUp;
                        Assign.FK_Product = JobCardDetails.Data[0].FK_Product;
                        Assign.FK_JobCardAssign = JobCardDetails.Data[0].FK_JobCardAssign;
                        Assign.FK_ProjectStages = JobCardDetails.Data[0].FK_ProjectStages;
                        Assign.Stage = JobCardDetails.Data[0].Stage;
                        Assign.Quantity = JobCardDetails.Data[0].Quantity;
                        Assign.ErrMsg = JobCardDetails.Data[0].ErrMsg ?? "";
                        Assign.ErrCode = JobCardDetails.Data[0].ErrCode;
                        Assign.FK_Employee = JobCardDetails.Data[0].FK_Employee;
                        Assign.JcfCompletedQty = JobCardDetails.Data[0].JcfCompletedQty;
                        Assign.JcfDamagedQty = JobCardDetails.Data[0].JcfDamagedQty;
                        Assign.JcfReturnedQty = JobCardDetails.Data[0].JcfReturnedQty;
                        Assign.FK_ReturnProjectStage = JobCardDetails.Data[0].FK_ReturnProjectStage;
                        Assign.ReturnProjectStage = JobCardDetails.Data[0].ReturnProjectStage;
                        Assign.ReturnStage= JobCardDetails.Data[0].ReturnStage;

                    }
                }

            }
            var FollowUpDetailsList = new ProductionStatusModel().GetJobCardStatusMarkingDetailsData(companyKey: _userLoginInfo.CompanyKey, input: new ProductionStatusModel.GetJobCardAssignEmployees
            {

                FK_JobCardAssign = ID,
                FK_Employee = FK_Employee,
                FK_ProjectStages = Assign.FK_ProjectStages,
                FK_Product = Assign.FK_Product,
                ID_JobCardFollowUp= ID_JobCardFollowUp,
                Mode = 1,
                View= View
            }).Data;
            return Json(new { Assign, FollowUpDetailsList }, JsonRequestBehavior.AllowGet);
            //return PartialView("_ProductionStatusMarkingDetls", Assign);



        }
        //public ActionResult LoadProductionStatusCmpleteMarking(long ID, long FK_Employee, long ID_JobCardFollowUp)
        //{
        //    //CommonMethod objCmnMethod = new CommonMethod();
        //    //string mGrp = objCmnMethod.Decrypt(mgrp);
        //    //ViewBag.TransMode = mGrp;
        //    //ViewBag.TicketNo = ID;
        //    //ViewBag.mgrp = mgrp;
        //    //ViewBag.Service = ServiceID;


        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
        //    ProductionStatusModel.ProductionStatusDetailsView Assign = new ProductionStatusModel.ProductionStatusDetailsView();

        //    var JobCardDetails = new APIGetRecordsDynamic<ProductionStatusModel.ProductionStatusDetailsView>();
        //    if (ID_JobCardFollowUp > 0)
        //    {
        //        JobCardDetails = new ProductionStatusModel().GetProductionStatusData(companyKey: _userLoginInfo.CompanyKey, input: new ProductionStatusModel.GetProductionStatus
        //        {
        //            FK_JobCardFollowUp = ID,
        //            FK_Company = _userLoginInfo.FK_Company,
        //            EntrBy = _userLoginInfo.EntrBy,
        //            FK_Machine = _userLoginInfo.FK_Machine,
        //            FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
        //            PageIndex = 0,
        //            PageSize = 0,
        //            Name = "",
        //            //TransMode = TransMode,
        //            //Detailed = 0
        //        });
        //    }
        //    else
        //    {
        //          JobCardDetails = new ProductionStatusModel().GetJobCardStatusMarkingData(companyKey: _userLoginInfo.CompanyKey, input: new ProductionStatusModel.GetJobCardAssignEmployees
        //        {

        //            FK_JobCardAssign = ID,
        //            FK_Employee = FK_Employee,
        //            ID_JobCardFollowUp = ID_JobCardFollowUp

        //        });
        //    }

        //    if (JobCardDetails.Data != null)
        //    {
        //        if (JobCardDetails.Data.Count > 0)
        //        {
        //            if (JobCardDetails.Data[0].ErrCode == 0)
        //            {
        //                Assign.JobCardNo = JobCardDetails.Data[0].JobCardNo;
        //                Assign.JobCardDetails = JobCardDetails.Data[0].JobCardDetails;
        //                Assign.ProductName = JobCardDetails.Data[0].ProductName;
        //                Assign.JcaStartDate = JobCardDetails.Data[0].JcaStartDate;
        //                Assign.JcaTargetDate = JobCardDetails.Data[0].JcaTargetDate;
        //                Assign.JcaStartTime = JobCardDetails.Data[0].JcaStartTime == "" ? DateTime.Now.ToString("HH:mm") : Convert.ToDateTime(JobCardDetails.Data[0].JcaStartTime).ToString("HH:mm");
        //                Assign.FK_Product = JobCardDetails.Data[0].FK_Product;
        //                Assign.FK_JobCardAssign = JobCardDetails.Data[0].FK_JobCardAssign;
        //                Assign.FK_ProjectStages = JobCardDetails.Data[0].FK_ProjectStages;
        //                Assign.Stage = JobCardDetails.Data[0].Stage;
        //                Assign.Quantity = JobCardDetails.Data[0].Quantity;
        //                Assign.ErrMsg = JobCardDetails.Data[0].ErrMsg ?? "";
        //                Assign.ErrCode = JobCardDetails.Data[0].ErrCode;
        //                Assign.FK_Employee = JobCardDetails.Data[0].FK_Employee;
        //                Assign.JcfCompletedQty= JobCardDetails.Data[0].JcfCompletedQty;
        //                Assign.JcfDamagedQty= JobCardDetails.Data[0].JcfDamagedQty;
        //                Assign.JcfReturnedQty= JobCardDetails.Data[0].JcfReturnedQty;
        //                Assign.FK_ReturnProjectStage= JobCardDetails.Data[0].FK_ReturnProjectStage;
        //                Assign.Stage= JobCardDetails.Data[0].Stage;


        //            }
        //        }

        //    }

        //    return PartialView("_ProductionStatusCmpltMarkingDetls", Assign);



        //}
        public ActionResult LoadProductionStatusDetailsMarking()
        {
            ProductionStatusModel.ProductionStatusDetailsView Assign = new ProductionStatusModel.ProductionStatusDetailsView();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            Assign.FK_Branch = _userLoginInfo.FK_Branch;
            Assign.FK_Department = _userLoginInfo.FK_Department;

            return PartialView("_ProductionStatusMarkingDetls", Assign);

        }
        public ActionResult ProductionStatusDetailsEdit(long ID, string TransMode)
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
            ProductionStatusModel.ProductionStatusDetailsView Assign = new ProductionStatusModel.ProductionStatusDetailsView();


            var JobCardDetails = new ProductionStatusModel().GetProductionStatusData(companyKey: _userLoginInfo.CompanyKey, input: new ProductionStatusModel.GetProductionStatus
            {
                FK_JobCardFollowUp = ID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = 0,
                PageSize = 0,
                Name = "",
                //TransMode = TransMode,
                //Detailed = 0
            });
            if (JobCardDetails.Data != null)
            {
                if (JobCardDetails.Data.Count > 0)
                {
                    Assign.ID_JobCardFollowUp = JobCardDetails.Data[0].ID_JobCardFollowUp;
                    Assign.JobCardNo = JobCardDetails.Data[0].JobCardNo;
                    Assign.JobCardDetails = JobCardDetails.Data[0].JobCardDetails;
                    Assign.ProductName = JobCardDetails.Data[0].ProductName;
                    Assign.JcaStartDate = JobCardDetails.Data[0].JcaStartDate;
                    Assign.FK_Product = JobCardDetails.Data[0].FK_Product;
                    Assign.FK_JobCardAssign = JobCardDetails.Data[0].FK_JobCardAssign;
                    Assign.FK_ProjectStages = JobCardDetails.Data[0].FK_ProjectStages;
                    Assign.Stage = JobCardDetails.Data[0].Stage;
                    Assign.Quantity = JobCardDetails.Data[0].Quantity;
                    Assign.ErrMsg = JobCardDetails.Data[0].ErrMsg ?? "";
                    Assign.ErrCode = JobCardDetails.Data[0].ErrCode;
                    Assign.FK_Employee = JobCardDetails.Data[0].FK_Employee;
                    Assign.JcfCompletedQty = JobCardDetails.Data[0].JcfCompletedQty;
                    Assign.JcfDamagedQty = JobCardDetails.Data[0].JcfDamagedQty;
                    Assign.JcfReturnedQty = JobCardDetails.Data[0].JcfReturnedQty;
                    Assign.Stage = JobCardDetails.Data[0].Stage;
                }
            }




            return PartialView("_ProductionStatusMarkingDetls", Assign);

        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult SaveProductionStatus(ProductionStatusModel.ProductionStatusDetailsView data)
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
                List<string> errorList = new List<string>();

                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                    .Select(e => e.ErrorMessage).
                     ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);

            }


            ProductionStatusModel JobCard = new ProductionStatusModel();
            var datresponse = JobCard.UpdateJobCardData(input: new ProductionStatusModel.UpdateProductionStatus
            {
                UserAction = data.ID_JobCardFollowUp == 0 ? 1 : 2,
                Debug = 0,
                TransMode = data.TransMode,
                ID_JobCardFollowUp = data.ID_JobCardFollowUp,
                FK_JobCardAssign = data.FK_JobCardAssign,
                FK_Employee = data.FK_Employee,
                JcfCompletedQty = data.JcfCompletedQty,
                JcfDamagedQty = data.JcfDamagedQty,
                JcfReturnedQty = data.JcfReturnedQty,
                FK_ProjectStages = data.FK_ProjectStages,
                FK_ReturnProjectStage = data.FK_ReturnProjectStage,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine, 
                JobCardFollowUpProductDetailList = data.JobCardFollowUpProductDetailList is null ? "" : Common.xmlTostring(data.JobCardFollowUpProductDetailList),

            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProductionStatusReasonList()
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

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteProductionStatus(ProductionStatusModel.DeleteProductionStatus data)
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

            ProductionStatusModel JobCard = new ProductionStatusModel();

            var datresponse = JobCard.DeleteJobCardData(input: new ProductionStatusModel.DeleteProductionStatus
            {
                ID_JobCardFollowUp = data.ID_JobCardFollowUp,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.FK_Reason,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = data.TransMode,
                Debug = 0,
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetJobCardFollowUpList(int pageSize, int pageIndex, string Name, string TransMode)
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
            ProductionStatusModel JobCard = new ProductionStatusModel();

            var JobCInfo = JobCard.GetProductionStatusData(companyKey: _userLoginInfo.CompanyKey, input: new ProductionStatusModel.GetProductionStatus
            {
                FK_JobCardFollowUp = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                //TransMode = TransMode,
                //Detailed = 0
            });

            return Json(new { JobCInfo.Process, JobCInfo.Data, pageSize, pageIndex, totalrecord = (JobCInfo.Data is null) ? 0 : JobCInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

    }

}