using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static PerfectWebERP.Models.AreaModel;


namespace PerfectWebERP.Controllers
{

    [CheckSessionTimeOut]

    public class AreaController : Controller
    {
       
        [CheckSessionTimeOut]
      
        public ActionResult Index(string mtd)
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
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 
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
        public ActionResult LoadAreaForm(string mtd)
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

            AreaModel.AreaListModel area = new AreaModel.AreaListModel();

            DistrictModel obj = new DistrictModel();
            AreaModel objpaymode = new AreaModel();
            var Districtlist = obj.GetDistrictData(input: new DistrictModel.InputDistrictID { ID_District = 0, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy }, companyKey: _userLoginInfo.CompanyKey);
            area.DistrictList = Districtlist.Data;

            var AreaListdata = objpaymode.GeLeadStatusList(input: new AreaModel.ModeLead { Mode =50 }, companyKey: _userLoginInfo.CompanyKey);
            area.AreaList = AreaListdata.Data;


            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
               companyKey: _userLoginInfo.CompanyKey,
               procedureName: "ProGetNextNo",
               parameter: new NextSortOrder
               {
                   TableName = "Area",
                   FieldName = "SortOrder",
                   Debug = 0
               });

            area.SortOrder = a.Data[0].NextNo;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AreaForm", area);
        }
        public ActionResult GetDistrictList()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<CustomerCModel.Districtlist>(parameters: new APIParameters
            {
                TableName = "District",
                SelectFields = " DtName AS District,ID_District AS DistrictID",
                Criteria = "Cancelled =0 AND Passed=1  AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },

          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAreaList1()
        {
            #region ::  Check User Session to verifyLogin  ::
            Log logfile = new Log();
            APIGetRecordsDynamic<AreaModel.Area> data = new APIGetRecordsDynamic<AreaModel.Area>();
            try
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

                #endregion ::  Check User Session to verifyLogin  ::

                UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

                 data = Common.GetDataViaQuery<AreaModel.Area>(parameters: new APIParameters
                {
                    TableName = "Area A",
                    SelectFields = "A.AreaShortName,A.AreaName,ID_Area",
                     Criteria = "A.Cancelled=0 AND A.Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                    SortFields = "A.SortOrder",
                    GroupByFileds = ""
                },
              companyKey: _userLoginInfo.CompanyKey
             );
            }
            catch (Exception ex)
            {
                Output output = new Output();
                output.IsProcess = false;
                output.Message = new List<string> { ex.Message.ToString() };
                output.Status = "Exception Occured ";
                logfile.WriteLog(output.Message[0], output.IsProcess);
                return Json(new APIGetRecordsDynamic<AreaModel.Area> { Process = output, Data = data.Data }, JsonRequestBehavior.AllowGet);
                
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAreaList(int pageSize, int pageIndex, string Name)
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

            AreaModel area = new AreaModel();
            var data = area.GetAreaData(input: new AreaModel.GetArea
            {
                ID_Area = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                TransMode = "",
                Name = Name,

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult AddNewAreaDetails(AreaModel.Area newAreaDetails)
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

            Output _output = new Output();
            ModelState.Remove("ID_Area");
            ModelState.Remove("ID_District");
            ModelState.Remove("FK_District");
            ModelState.Remove("SortOrder");
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

            //Check model validation
            if (false)
            {

            }
            int userAction = 1;//update : 2 | Add : 1 
            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string userCode = _userLoginInfo.EntrBy;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            int backupId = 0;


            AreaModel.UpdateArea areas = new AreaModel.UpdateArea
            {
                UserAction = 1,//since insert set to 1 | update=2
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Company = OrgnCode,
                TransMode= newAreaDetails.TransMode,
                SortOrder = newAreaDetails.SortOrder,
                ID_Area = 0,
                AreaName = newAreaDetails.AreaName,
                AreaShortName = newAreaDetails.AreaShortName,
                DistrictID = newAreaDetails.DistrictID,
                EntrBy = entrBy,
                TypeID=newAreaDetails.TypeID,
            };

            Output datresponse = Common.UpdateTableData<AreaModel.UpdateArea>(companyKey: companyKey, procedureName: "proAreaUpdate", parameter: areas);



            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetArea()
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

            var data = Common.GetDataViaQuery<AreaModel.Area>(parameters: new APIParameters
            {
                TableName = "Area",
                SelectFields = "ID_Area AS AreaID,AreaName AS AreaName",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey
         );

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetAreaByID(int ID_Area)
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

            short ID_Areas = (short)ID_Area;

            var Stateinfo = Common.GetDataViaProcedure<AreaModel.Area, InputArea>(companyKey: _userLoginInfo.CompanyKey, procedureName: "proAreaSelect", parameter: new InputArea { ID_Area = ID_Area, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy,FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser });

            return Json(Stateinfo, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetAreaInfoByID(long ID_Area)
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
            short ID_Areas = (short)ID_Area;
            var stateInfo = Common.GetDataViaProcedure<AreaModel.Area, InputArea>(companyKey: _userLoginInfo.CompanyKey, procedureName: "proAreaSelect", parameter: new InputArea { ID_Area = ID_Area });
            return Json(stateInfo, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateArea(AreaModel.Area data)
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

            AreaModel area = new AreaModel();

            var datresponse = area.UpdateArearData(input: new AreaModel.UpdateArea
            {
                UserAction = 2,
                ID_Area= data.ID_Area,
                AreaShortName = data.AreaShortName,
                AreaName = data.AreaName,
                SortOrder = data.SortOrder,
                DistrictID = data.DistrictID,
                TransMode = data.TransMode,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine= _userLoginInfo.FK_Machine,
                EntrBy= _userLoginInfo.EntrBy,
                TypeID = data.TypeID,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAreaReasonList()
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
        public ActionResult DeleteArea(AreaModel.Area data)
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

            // if removing a node in model while validating do it above #region Model Validation and  not inside #region so its easly visible
            //<remove node in model validation here> 

            #region :: Model validation  ::
            //--- Model validation 
            //if (!ModelState.IsValid)
            //{

            //    // since no need to continue just return
            //    return Json(new
            //    {
            //        Process = new Output
            //        {
            //            IsProcess = false,
            //            Message = ModelState.Values.SelectMany(m => m.Errors)
            //                            .Select(e => e.ErrorMessage)
            //                            .ToList(),
            //            Status = "Validation failed",
            //        }
            //    }, JsonRequestBehavior.AllowGet);
            //}

            #endregion :: Model validation  ::

            AreaModel area = new AreaModel();

            var datresponse = area.DeleteAreaData(input: new AreaModel.DeleteAreaModel { ID_Area = data.ID_Area, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, FK_Company = _userLoginInfo.FK_Company, FK_Reason = data.ReasonID, }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteAreaInfo(int ID_Area)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            Output _output = new Output();
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



            AreaModel.DeleteAreaModel area = new AreaModel.DeleteAreaModel
            {
                ID_Area = ID_Area,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            };


            Output dataresponse = Common.UpdateTableData<AreaModel.DeleteAreaModel>(companyKey: _userLoginInfo.CompanyKey, procedureName: "proAreaDelete", parameter: area);




            return Json(new
            {
                Process = dataresponse,
            }, JsonRequestBehavior.AllowGet);
        }
    }
}