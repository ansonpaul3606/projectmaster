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
    public class JobCardAssignController : Controller
    {
        // GET: JobCardAssign
        public ActionResult JobCardAssign(string mgrp)
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
        public ActionResult LoadJobCardAssign(string ID_ProjectStages)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            var IsAdmin = _userLoginInfo.UsrrlAdmin == true ? 1 : 0;
            ID_ProjectStages= ID_ProjectStages??"0";
            JobCardAssignModel.JobCardAssignView Assign = new JobCardAssignModel.JobCardAssignView();

            var StageList = Common.GetDataViaQuery<JobCardAssignModel.Stage>(parameters: new APIParameters
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

            var TeamList = Common.GetDataViaQuery<JobCardAssignModel.Team>(parameters: new APIParameters
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


            Assign.StageList = StageList.Data;
            Assign.TeamList = TeamList.Data;
            //Assign.JobCardDetails = JobCardDetails.Data;
            return PartialView("_AddJobCardAssign", Assign);
        }
        public ActionResult LoadProjectTeam(long ID_ProjectStages)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            var IsAdmin = _userLoginInfo.UsrrlAdmin == true ? 1 : 0;
            JobCardAssignModel.JobCardAssignView Assign = new JobCardAssignModel.JobCardAssignView();



            var TeamList = Common.GetDataViaQuery<JobCardAssignModel.Team>(parameters: new APIParameters
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
        public ActionResult GetJobCards(JobCardAssignModel.JobCardAssignView data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
           
            var JobCardDetails = new JobCardAssignModel().GetJobCardData(companyKey: _userLoginInfo.CompanyKey, input: new JobCardAssignModel.GetJobCards
            {
                TransMode = "",
                FK_Product = data.FK_Product,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                PageIndex = data.PageIndex+1,
                PageSize = data.PageSize,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                Filterid = data.FilterId,
                TargetFrom = data.TargetFromDate,
                TargetTo = data.TargetToDate
            });
            return Json(new { JobCardDetails.Process, JobCardDetails.Data, data.PageSize, data.PageIndex, totalrecord = (JobCardDetails.Data is null) ? 0 : JobCardDetails.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

        
        public ActionResult GetJobCardAssignList(int pageSize, int pageIndex, string Name, string TransMode)
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
            JobCardAssignModel JobCard = new JobCardAssignModel();

            var JobCInfo = JobCard.GetJobCardAssignData(companyKey: _userLoginInfo.CompanyKey, input: new JobCardAssignModel.GetJobCardAssign
            { FK_JobCardAssign = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = TransMode,
                Detailed = 0
            });

            return Json(new { JobCInfo.Process, JobCInfo.Data, pageSize, pageIndex, totalrecord = (JobCInfo.Data is null) ? 0 : JobCInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult JobCardAssignDetails(long ID,long ProdID,bool ReturnJob)
        {
            //CommonMethod objCmnMethod = new CommonMethod();
            //string mGrp = objCmnMethod.Decrypt(mgrp);
            //ViewBag.TransMode = mGrp;
            //ViewBag.TicketNo = ID;
            //ViewBag.mgrp = mgrp;
            //ViewBag.Service = ServiceID;


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            JobCardAssignModel.JobCardAssignDetailsView Assign = new JobCardAssignModel.JobCardAssignDetailsView();



            var JobCardDetails = new JobCardAssignModel().GetJobCardEmpData(companyKey: _userLoginInfo.CompanyKey, input: new JobCardAssignModel.GetJobCardEmployees
            {
                FK_Product = ProdID,
                FK_JobCard = ID,
                 EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser= _userLoginInfo.FK_BranchCodeUser,
                Filterid = 0,
               ReturnJob= ReturnJob
            });
           
            if (JobCardDetails.Data != null)
            {
                if (JobCardDetails.Data.Count > 0)
                {
                    if (JobCardDetails.Data[0].ErrCode == 0)
                    {
                        Assign.JobCardNo = JobCardDetails.Data[0].JobCardNo;
                        Assign.JobCardDetails = JobCardDetails.Data[0].JobCardDetails;
                        Assign.ProductName = JobCardDetails.Data[0].ProductName;
                        Assign.StartDate = JobCardDetails.Data[0].StartDate;
                        Assign.JcaStartDate = JobCardDetails.Data[0].JcaStartDate;
                        Assign.JcaTargetDate = JobCardDetails.Data[0].JcaTargetDate;
                        Assign.JcaStartTime = JobCardDetails.Data[0].JcaStartTime==""?DateTime.Now.ToString("HH:mm"): Convert.ToDateTime(JobCardDetails.Data[0].JcaStartTime).ToString("HH:mm"); 
                        Assign.TargetDate = JobCardDetails.Data[0].TargetDate;
                        Assign.FK_Product = JobCardDetails.Data[0].FK_Product;
                        Assign.ID_JobCard = JobCardDetails.Data[0].ID_JobCard;
                        Assign.FK_ProjectStages = JobCardDetails.Data[0].FK_ProjectStages;
                        Assign.Stage = JobCardDetails.Data[0].Stage;
                        Assign.Quantity = JobCardDetails.Data[0].Quantity;
                        Assign.ErrMsg = JobCardDetails.Data[0].ErrMsg??"";
                        Assign.ErrCode = JobCardDetails.Data[0].ErrCode;
                        Assign.ReturnJob = ReturnJob;

                        var JobCardEmpDetails = new JobCardAssignModel().GetJobCardEmpDtlsData(companyKey: _userLoginInfo.CompanyKey, input: new JobCardAssignModel.GetJobCardEmployees
                        {
                            FK_Product = Assign.FK_Product,
                            FK_JobCard = Assign.ID_JobCard,
                            EntrBy = _userLoginInfo.EntrBy,
                            FK_Company = _userLoginInfo.FK_Company,
                            FK_Machine = _userLoginInfo.FK_Machine,
                            FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                            Filterid = 1,
                            FK_ProjectStages = Assign.FK_ProjectStages,
                            ReturnJob=ReturnJob
                        });
                        if (JobCardEmpDetails.Data != null)
                        {
                            
                                Assign.EmployeeAssign = JobCardEmpDetails.Data;
                            
                        }
                    }
                    else
                    {
                        Assign.ErrMsg = JobCardDetails.Data[0].ErrMsg;
                        Assign.ErrCode = JobCardDetails.Data[0].ErrCode;
                    }


                }
                 
            }

            return PartialView("_JobCardAssignEmp", Assign);



        }
       
        public ActionResult JobCardAssignDetailsEdit(long ID, string TransMode)
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
            JobCardAssignModel.JobCardAssignDetailsView Assign = new JobCardAssignModel.JobCardAssignDetailsView();
            var JobCardDetails = new JobCardAssignModel().GetJobCardAssignData(companyKey: _userLoginInfo.CompanyKey, input: new JobCardAssignModel.GetJobCardAssign
            {
                FK_JobCardAssign = ID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = 0,
                PageSize =0,
                Name = "",
                TransMode = TransMode,
                Detailed = 0
            });
            Assign.JobCardNo = JobCardDetails.Data[0].JobCardNo;
            Assign.JobCardDetails = JobCardDetails.Data[0].JobCardDetails;
            Assign.ProductName = JobCardDetails.Data[0].ProductName;
            Assign.StartDate = JobCardDetails.Data[0].StartDate;
            Assign.JcaStartDate = JobCardDetails.Data[0].JcaStartDate;
            Assign.JcaStartTime = JobCardDetails.Data[0].JcaStartTime == "" ? DateTime.Now.ToString("HH:mm tt") : Convert.ToDateTime(JobCardDetails.Data[0].JcaStartTime).ToString("HH:mm tt");
            Assign.TargetDate = JobCardDetails.Data[0].TargetDate;
            Assign.FK_Product = JobCardDetails.Data[0].FK_Product;
            Assign.ID_JobCard = JobCardDetails.Data[0].ID_JobCard;
            Assign.JcaAssignDate = JobCardDetails.Data[0].JcaAssignDate;
            Assign.FK_ProjectStages = JobCardDetails.Data[0].FK_ProjectStages;
            Assign.Stage = JobCardDetails.Data[0].Stage;
            Assign.Quantity = JobCardDetails.Data[0].Quantity;
            Assign.ErrMsg = JobCardDetails.Data[0].ErrMsg ?? "";
            Assign.ErrCode = JobCardDetails.Data[0].ErrCode;
            Assign.ID_JobCardAssign = JobCardDetails.Data[0].ID_JobCardAssign;

            




            var JobCardAssignItem = new JobCardAssignModel().GetJobCardAssignEmpData(companyKey: _userLoginInfo.CompanyKey, input: new JobCardAssignModel.GetJobCardAssign
            {
                FK_JobCardAssign = ID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = 0,
                PageSize = 0,
                Name = "",
                TransMode = TransMode,
                Detailed = 1
            });
            if (JobCardAssignItem.Data != null)
            {

                Assign.EmployeeAssign = JobCardAssignItem.Data;

            }
            return PartialView("_JobCardAssignEmp", Assign);

        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult SaveJobCardAssign(JobCardAssignModel.JobCardAssignDetailsView data)
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
           

            JobCardAssignModel JobCard = new JobCardAssignModel();
            var datresponse = JobCard.UpdateJobCardData(input: new JobCardAssignModel.UpdateJobCardAssign
            {
                ID_JobCardAssign = data.ID_JobCardAssign ,
                UserAction = data.ID_JobCardAssign == 0?1:2,
                JobCardAssignDetails = data.EmployeeAssign is null ? "" : Common.xmlTostring(data.EmployeeAssign),
                FK_Product = data.FK_Product,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_JobCard = data.ID_JobCard,
                JcaStartTime=Convert.ToDateTime(data.JcaStartTime).ToString("HH:mm tt"),
                JcaStartDate = data.JcaStartDate,
                JCStartDate =data.JCStartDate,
                Debug = 0,
                FK_ProjectStages = data.FK_ProjectStages,
                TransMode=data.TransMode,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                ReturnJob =data.ReturnJob
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetJobCardReasonList()
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
        public ActionResult DeleteJobCardAssign(JobCardAssignModel.DeleteJobCardAssign data)
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

            JobCardAssignModel JobCard = new JobCardAssignModel();

            var datresponse = JobCard.DeleteJobCardData(input: new JobCardAssignModel.DeleteJobCardAssign
            {
                FK_JobCardAssign = data.FK_JobCardAssign,
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
    }
    
}