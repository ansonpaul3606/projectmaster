/*----------------------------------------------------------------------
Created By	: Kavya K
Created On	: 19/12/2022
Purpose		: Job Card Creation
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
    public class JobCardCreationController : Controller
    {
        // GET: JobCardCreation
        public ActionResult JobCard(string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            //ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            return View();
        }
        

        public ActionResult LoadFormJobCard(string TransMode,string TransDate)
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

            JobCardCreationModel.PeriodTypeList TypeList = new JobCardCreationModel.PeriodTypeList();
            JobCardCreationModel objPeriodType = new JobCardCreationModel();
            var PeriodType = objPeriodType.GetPeriodTypeList(input: new JobCardCreationModel.PeriodTypeMode { Mode = 55 }, companyKey: _userLoginInfo.CompanyKey);
            TypeList.PeriodType = PeriodType.Data;

            JobCardCreationModel objJobcardNo = new JobCardCreationModel();
            var JobCardNo = objJobcardNo.GetJobCardNo(input: new JobCardCreationModel.inputJobCardNo
            {
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser,
                Submodule = TransMode,
                FK_Type = 0,
                TransDate = Convert.ToDateTime(TransDate==""?DateTime.Now.ToString():TransDate),

            },companyKey: _userLoginInfo.CompanyKey);


            TypeList.JobCardNo = JobCardNo.Data[0].AccountNo;

            return PartialView("_AddJobCardCreations", TypeList);
        }



        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewJobCard(JobCardCreationModel.JobCardView data)
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

            ModelState.Remove("JobCardID");
           
            if (data.JobCardType == 1)
            {
                if (data.PeriodType != 0)
                {
                    ModelState["TargetDate"].Errors.Clear(); data.TargetDate = null;
                }
                else
                {
                    if(data.TargetDate==null)
                    {
                        ModelState.AddModelError("TargetDate", "The TargetDate field is required.");
                    }
                    ModelState["TypeEndDate"].Errors.Clear(); data.TypeEndDate = null;
                }
            }
            else
            {

                ModelState["TypeEndDate"].Errors.Clear(); 
                ModelState["JobCardNo"].Errors.Clear(); data.TypeEndDate = null;
            }
            if (!ModelState.IsValid)
            {
                List<string> errorList = new List<string>();

                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                    .Select(e => e.ErrorMessage == "The TargetDate field is required." ? "Select Target Date" : e.ErrorMessage).
                     ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                if(data.TargetDate!=null)
                {
                    if(data.StartDate>data.TargetDate)
                    {
                        List<string> Errormsg=new List<string>();

                        Errormsg.Insert(0, "Target Date should be greater than Start Date");
                        return Json(new
                        {
                            Process = new Output
                            {
                                IsProcess = false,
                                Message = Errormsg,
                                Status = "Validation failed",
                            }
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                JobCardCreationModel JobCard = new JobCardCreationModel();

                var datresponse = JobCard.UpdateJobCardData(input: new JobCardCreationModel.UpdateJobCard
                {
                    UserAction = 1,
                    StartDate = data.StartDate,
                    TargetDate = data.TargetDate,
                    JobCardType = data.JobCardType,
                    Priority = data.Priority,
                    JobCardNo = data.JobCardNo,
                    FK_Master = data.FK_Master,
                    TransMode = data.TransMode,
                    JobCardDetails = data.JobCardDetails is null ? "" : Common.xmlTostring(data.JobCardDetails),
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    ID_JobCard = 0,
                    FK_JobCard = 0,
                    Debug = 0,
                    PeriodType = data.PeriodType,
                    Period = data.Period,
                    NoofCount = data.NoofCount,
                    Date = data.TypeEndDate,
                    LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                }, companyKey: _userLoginInfo.CompanyKey);
                return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateJobCard(JobCardCreationModel.JobCardView data)
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
            ModelState.Remove("JobCardID");
           
            if (data.JobCardType == 1)
            {
                if (data.PeriodType != 0)
                {
                    ModelState["TargetDate"].Errors.Clear(); data.TargetDate = null;
                }
                else
                {

                    ModelState["TypeEndDate"].Errors.Clear(); data.TypeEndDate = null;
                }
            }
            else
            {

                ModelState["TypeEndDate"].Errors.Clear();
                ModelState["JobCardNo"].Errors.Clear(); data.TypeEndDate = null;
            }
            if (!ModelState.IsValid)
            {
                List<string> errorList = new List<string>();

                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                    .Select(e => e.ErrorMessage == "The TargetDate field is required." ? "Select Target Date" : e.ErrorMessage).
                     ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);

            }
            if (data.TargetDate == null)
            {
               
                    List<string> Errormsg = new List<string>();

                    Errormsg.Insert(0, "Please Enter Target Date");
                    return Json(new
                    {
                        Process = new Output
                        {
                            IsProcess = false,
                            Message = Errormsg,
                            Status = "Validation failed",
                        }
                    }, JsonRequestBehavior.AllowGet);
                 
            }
            JobCardCreationModel JobCard = new JobCardCreationModel();
            var datresponse = JobCard.UpdateJobCardData(input: new JobCardCreationModel.UpdateJobCard
            {
                UserAction = 2,
                StartDate = data.StartDate,
                TargetDate = data.TargetDate,
                JobCardType = data.JobCardType,
                Priority = data.Priority,
                JobCardNo = data.JobCardNo,
                FK_Master = data.FK_Master,
                TransMode = data.TransMode,
                JobCardDetails = data.JobCardDetails is null ? "" : Common.xmlTostring(data.JobCardDetails),
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_JobCard = data.ID_JobCard,
                FK_JobCard = data.FK_JobCard,
                Debug = 0,
                PeriodType = data.PeriodType,
                Period = data.Period,
                NoofCount = data.NoofCount,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                Date = data.TypeEndDate,
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetJobCardList(int pageSize, int pageIndex, string Name, string TransMode)
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
            JobCardCreationModel JobCard = new JobCardCreationModel();

            var JobCInfo = JobCard.GetJobCardData(companyKey: _userLoginInfo.CompanyKey, input: new JobCardCreationModel.GetJobCard
            {
                GroupID = 0,
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

        [HttpPost]
        public ActionResult GetJobCardInfo(JobCardCreationModel.JobCardView data)
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
            JobCardCreationModel objJobCard = new JobCardCreationModel();
            var JobCardInfo = objJobCard.GetJobCardSelectDetails(companyKey: _userLoginInfo.CompanyKey, input: new JobCardCreationModel.GetJobCard
            {
                GroupID = data.GroupID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = data.PageIndex,
                PageSize = data.PageSize,
                Name = data.Name,
                TransMode = data.TransMode,
                Detailed = 0,
                

            });

            var JobCardItem = objJobCard.GetJobCardItemDetailsSelect(companyKey: _userLoginInfo.CompanyKey, input: new JobCardCreationModel.GetJobCard
            {
                GroupID = data.GroupID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = data.PageIndex,
                PageSize = data.PageSize,
                Name = data.Name,
                TransMode = data.TransMode,
                Detailed = 1
            });

            return Json(new { JobCardInfo, JobCardItem }, JsonRequestBehavior.AllowGet);
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
        public ActionResult DeleteJobCard(JobCardCreationModel.DeleteJobCard data)
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

            JobCardCreationModel JobCard = new JobCardCreationModel();

            var datresponse = JobCard.DeleteJobCardData(input: new JobCardCreationModel.DeleteJobCard
            {
                GroupID = data.GroupID,
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