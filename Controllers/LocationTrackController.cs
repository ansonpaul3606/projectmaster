using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PerfectWebERP.Controllers
{
    public class LocationTrackController : Controller
    {
        // GET: LocationTrack
        public ActionResult Index()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            //test code start

            //test code end

            return View();
        }
        public ActionResult LoadLocationTrack()
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
            ViewBag.FK_UserRole = _userLoginInfo.FK_UserRole;
            ViewBag.IsAdmin = _userLoginInfo.UsrrlAdmin;
            ViewBag.IsManger = _userLoginInfo.UsrrlManager;
            LocationTrackModel objMg = new LocationTrackModel();
            LocationTrackModel.LocationTrackerView locationView = new LocationTrackModel.LocationTrackerView();
            string markers = "[";
            markers += "{";
            markers += string.Format("'title': '{0}',", "Test Tiltle");
            markers += string.Format("'lat': '{0}',", "75.8341724");
            markers += string.Format("'lng': '{0}',", "11.2476357");
            markers += string.Format("'description': '{0}'", "Test Description");
            markers += "}";
            markers += "];";

            ViewBag.Markers = markers;

            var branch = Common.GetDataViaQuery<LocationTrackModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS BranchName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);

            locationView.BranchList = branch.Data;

            var departmentlist = Common.GetDataViaQuery<LocationTrackModel.Department>(parameters: new APIParameters
            {

                TableName = "Department",
                SelectFields = "ID_Department as DepartmentID,DeptName as DepartmentName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
         companyKey: _userLoginInfo.CompanyKey
         );

            locationView.DepartmentList = departmentlist.Data;

            //DesignationList

            var designationList = Common.GetDataViaQuery<LocationTrackModel.DesignationList>(parameters: new APIParameters
            {
                TableName = "Designation D",
                SelectFields = "D.ID_Designation AS DesignationID,D.DesName AS[Designation]",
                Criteria = "D.Cancelled=0 AND D.Passed=1 AND D.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey

           );
            locationView.DesignationList = designationList.Data;

            locationView.MapKey = ConfigurationManager.AppSettings["googleMapKey"];

            return PartialView("_LoadLocationTrack", locationView);
        }
        [HttpPost]
        public ActionResult Getgetlocationtrackerdetails(LocationTrackModel.InputDto inputdata)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            LocationTrackModel objfld = new LocationTrackModel();

            //var data = objfld.GetLocationTrackerDetailsData(input: new LocationTrackModel.InputDto
            //{
            //    ReportMode = Data.ID_Report,
            //    FromDate = Data.FromDate,
            //    ToDate = Data.ToDate,
            //    FK_Branch = Data.BranchID,
            //    FK_Department = Data.DepartmentID,
            //    FK_Employee = Data.EmployeeID,
            //    FK_AllowanceType = (Data.FK_AllowanceType.HasValue) ? Data.FK_AllowanceType.Value : 0,

            //    FK_Company = _userLoginInfo.FK_Company,
            //    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            //    EntrBy = _userLoginInfo.EntrBy,
            //    FK_Machine = _userLoginInfo.FK_Machine
            //},

            //companyKey: _userLoginInfo.CompanyKey);

            var data = objfld.GetLocationTrackerDetailsData(input: new LocationTrackModel.InputDto
            {
                FK_Company = _userLoginInfo.FK_Company,
                Date = inputdata.Date,
                FK_Department = inputdata.FK_Department,
                FK_Employee = inputdata.FK_Employee,
                FK_Designation = inputdata.FK_Designation,
                FK_Branch = inputdata.FK_Branch,
                EntrBy= _userLoginInfo.EntrBy
                
            }, companyKey: _userLoginInfo.CompanyKey);



            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetgetlocationRoutedetails(LocationTrackModel.inputRoute inputdata)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            LocationTrackModel objfld = new LocationTrackModel();


            var data = objfld.GetLocationTrackerRouteDetails(input: new LocationTrackModel.inputRoute
            {
                FK_Company = _userLoginInfo.FK_Company,
                Date = inputdata.Date,
                FK_Employee = inputdata.FK_Employee==0? _userLoginInfo.FK_Employee: inputdata.FK_Employee

            }, companyKey: _userLoginInfo.CompanyKey);



            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}