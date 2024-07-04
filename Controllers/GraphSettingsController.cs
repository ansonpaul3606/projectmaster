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
using System.Reflection;
using System.Text;
using System.Net;
using System.Configuration;
using System.Net.Security;
using System.Net.Http.Headers;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]

    public class GraphSettingsController : Controller
    {
        Random randomColor = new Random();
        //private IDALCustomer _customer = new DALCustomer();

        // GET: Customer


        public ActionResult GraphSettings()
        {
            GraphSettingsModel objHome = new GraphSettingsModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Companyname = _userLoginInfo.Company;
            GraphSettingsModel.DashboardDetails obj = new GraphSettingsModel.DashboardDetails();
            var objDashboardInfo = objHome.GetDashboard(companyKey: _userLoginInfo.CompanyKey, input: new GraphSettingsModel.GetDashboardInData
            {
                FK_Employee = _userLoginInfo.FK_Employee,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Department = _userLoginInfo.FK_Department,
                EntrBy = _userLoginInfo.EntrBy,
                AsOnDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyy"))
            });
            GraphSettingsModel.DashboardData objData = new GraphSettingsModel.DashboardData();
            if (objDashboardInfo.Data != null)
            {
                List<GraphSettingsModel.DashboardOutData> TileData = objDashboardInfo.Data.Where(m => m.ChartType == 1).ToList();
                List<GraphSettingsModel.DashboardOutData> ChartData = objDashboardInfo.Data.Where(m => m.ChartType == 2).ToList();
                List<GraphSettingsModel.ChartListData> chartList = new List<GraphSettingsModel.ChartListData>();
                StringBuilder sbModal = new StringBuilder();
                // ViewBag.DashboardTile = CreateTile(TileData);
                string value = objDashboardInfo.Data[0].ToString();
                ViewBag.DashboardChart = CreateChart(ChartData, ref chartList, ref sbModal);
                ViewBag.DashboardChartModal = sbModal.ToString();
                if (chartList != null)
                {
                    obj.ChartData = chartList;
                }
            }
            else
            {
                ViewBag.DashboardWarning = @"Dashboard settings are missing!";
            }
            return View(obj);
        }
        [HttpPost]
        public ActionResult GetGraphData(GraphSettingsModel.GetDashboardInData input)
        {
            GraphSettingsModel objHome = new GraphSettingsModel();
            List<GraphSettingsModel.ChartListData> Data = new List<GraphSettingsModel.ChartListData>();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Companyname = _userLoginInfo.Company;
            GraphSettingsModel.DashboardDetails obj = new GraphSettingsModel.DashboardDetails();
            var objDashboardInfo = objHome.GetDashboard(companyKey: _userLoginInfo.CompanyKey, input: new GraphSettingsModel.GetDashboardInData
            {
                FK_Employee = _userLoginInfo.FK_Employee,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Department = _userLoginInfo.FK_Department,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Module= input.FK_Module,
                FK_SubModule = input.FK_SubModule,
                FromDate = input.FromDate,
                ToDate = input.ToDate,
                FK_GraphType = input.FK_GraphType,
                DashMode=1,
                ChartType=2,
                AsOnDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyy"))
            });
            GraphSettingsModel.DashboardData objData = new GraphSettingsModel.DashboardData();
            if (objDashboardInfo.Data != null)
            {
                List<GraphSettingsModel.DashboardOutData> TileData = objDashboardInfo.Data.Where(m => m.ChartType == 1).ToList();
                List<GraphSettingsModel.DashboardOutData> ChartData = objDashboardInfo.Data.Where(m => m.ChartType == 2).ToList();
                List<GraphSettingsModel.ChartListData> chartList = new List<GraphSettingsModel.ChartListData>();
                StringBuilder sbModal = new StringBuilder();
                // ViewBag.DashboardTile = CreateTile(TileData);
                ViewBag.DashboardChart = CreateChart(ChartData, ref chartList, ref sbModal);
                ViewBag.DashboardChartModal = sbModal.ToString();
               
                if (chartList != null)
                {
                    obj.ChartData = chartList;
                }
                Data = obj.ChartData;
                

            }
            // return View(obj);
            return Json(new { Data, ViewBag.DashboardChart, ViewBag.DashboardChartModal} ,JsonRequestBehavior.AllowGet);
        }

        

        public ActionResult LoadGraphSettingsForm()
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
            GraphSettingsModel.Graphlist statusobj = new GraphSettingsModel.Graphlist();
            var branch = Common.GetDataViaQuery<GraphSettingsModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch ",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            statusobj.BranchList = branch.Data;






            var compname = Common.GetDataViaQuery<GraphSettingsModel.Graphlist>(parameters: new APIParameters
            {
                TableName = "Company",
                SelectFields = "CompName",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 ",
                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey);

            statusobj.CompName = compname.Data[0].CompName;

            GraphSettingsModel objAuth = new GraphSettingsModel();
            var ModuleList = objAuth.GetModuleList(input: new GraphSettingsModel.ModeLead { Mode = 115 }, companyKey: _userLoginInfo.CompanyKey);

            statusobj.ModuleList = ModuleList.Data;

            //var statusmodeList = objAuth.GeLeadStatusList(input: new AuthorizationReportModel.ModeLead { Mode = 11 }, companyKey: _userLoginInfo.CompanyKey);

            // statusobj.StatusList = statusmodeList.Data;


            return PartialView("_AddGraphSettings", statusobj);

        }
       
        private string CreateTile(List<GraphSettingsModel.DashboardOutData> objData)
        {
            StringBuilder sb = new StringBuilder();
            Type type = typeof(GraphSettingsModel.CustomColorClass);
            Array Colorvalues = type.GetEnumValues();
            Random random = new Random();
            for (int i = 0; i < objData.Count; i++)
            {
                int index = random.Next(Colorvalues.Length);
                GraphSettingsModel.CustomColorClass cValue = (GraphSettingsModel.CustomColorClass)Colorvalues.GetValue(index);

                if (!string.IsNullOrEmpty(objData[i].Datafile))
                {
                    DataTable CountData = JsonConvert.DeserializeObject<DataTable>(objData[i].Datafile);
                    switch (objData[i].ChartList)
                    {

                        case 1:
                        case 3:
                        case 4:
                        case 6:
                            sb.Append(@"<div class='col-xl-3 col-xxl-3 col-sm-12'>
                                    <div class='card' title='" + objData[i].Remarks + @"'>
                                        <div class='social-graph-wrapper " + cValue + @"'>
                                            <div class='media align-items-center'>
                                                <div class='media-body text-left'>
                                                    <p class='fs-18 text-white mb-2'>" + objData[i].ChartName + @"</p>
                                                </div>
                                                <div class='media-body text-right'>
                                                    <span class='fs-18 text-white mb-2 font-w600'>" + CountData.Rows[0][0].ToString() + @"</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='row'>
                                            <div class='col-4 border-right'>
                                                <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                    <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][1].ToString() + @"</span></h4>
                                                    <p class='m-0' style='color:#f96922'>" + CountData.Columns[1].Caption.ToString() + @"</p>
                                                </div>
                                            </div>
                                            <div class='col-4 border-right'>
                                                <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                    <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][2].ToString() + @"</span></h4>
                                                    <p class='m-0' style='color:#f5a732'>" + CountData.Columns[2].Caption.ToString() + @"</p>
                                                </div>
                                            </div>
                                            <div class='col-4'>
                                                <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                    <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][3].ToString() + @"</span></h4>
                                                    <p class='m-0' style='color:#1ca4fc'>" + CountData.Columns[3].Caption.ToString() + @"</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>");
                            break;
                        case 11:
                        case 12:
                        case 2:
                            sb.Append(@"<div class='col-xl-3 col-xxl-3 col-sm-12'>
                                            <div class='card' title='" + objData[i].Remarks + @"'>
                                                <div class='card-body' style='margin-top:4%;'>
                                                        <div class='media'>
                                                        <svg class='mr-3' width='80' height='80' viewBox='0 0 80 80' fill='none'>
                                                            <path d='M0 11.6364C0 5.20978 5.20978 0 11.6364 0H68.3636C74.7902 0 80 5.20978 80 11.6364V68.3636C80 74.7902 74.7902 80 68.3636 80H11.6364C5.20978 80 0 74.7902 0 68.3636V11.6364Z' fill='#D3D3D3'></path>
                                                            <path d='M0 11.6364C0 5.20978 5.20978 0 11.6364 0H68.3636C74.7902 0 80 5.20978 80 11.6364V68.3636C80 74.7902 74.7902 80 68.3636 80H11.6364C5.20978 80 0 74.7902 0 68.3636V11.6364Z' fill='#40C7CF'></path>
                                                            <path d='M20.6216 20.6219C23.142 18.1015 26.1342 16.1022 29.4273 14.7381C32.7205 13.374 36.25 12.672 39.8145 12.672C43.3789 12.672 46.9085 13.374 50.2016 14.7381C53.4947 16.1022 56.4869 18.1015 59.0074 20.6219C61.5278 23.1424 63.5271 26.1346 64.8912 29.4277C66.2552 32.7208 66.9573 36.2504 66.9573 39.8148C66.9573 43.3793 66.2552 46.9088 64.8912 50.202C63.5271 53.4951 61.5278 56.4873 59.0074 59.0077L49.4109 49.4113C50.6711 48.1511 51.6708 46.6549 52.3528 45.0084C53.0348 43.3618 53.3859 41.5971 53.3859 39.8148C53.3859 38.0326 53.0348 36.2678 52.3528 34.6213C51.6708 32.9747 50.6711 31.4786 49.4109 30.2184C48.1507 28.9582 46.6546 27.9585 45.008 27.2765C43.3615 26.5944 41.5967 26.2434 39.8145 26.2434C38.0322 26.2434 36.2675 26.5944 34.6209 27.2765C32.9743 27.9585 31.4782 28.9582 30.218 30.2184L20.6216 20.6219Z' fill='#8FD7FF'></path>
                                                            <path d='M20.6215 59.0077C15.5312 53.9174 12.6715 47.0135 12.6715 39.8148C12.6715 32.6161 15.5312 25.7122 20.6215 20.6219C25.7118 15.5316 32.6157 12.6719 39.8144 12.6719C47.0131 12.6719 53.917 15.5316 59.0073 20.6219L49.4108 30.2183C46.8657 27.6732 43.4138 26.2434 39.8144 26.2434C36.215 26.2434 32.7631 27.6732 30.2179 30.2183C27.6728 32.7635 26.243 36.2154 26.243 39.8148C26.243 43.4141 27.6728 46.8661 30.2179 49.4112L20.6215 59.0077Z' fill='white'></path>
                                                        </svg>
                                                        <div class='media-body'>
                                                            <h6 class='fs-16 text-black font-w600'>" + objData[i].ChartName + @"</h6>
                                                            <span class='fs-16 text-primary font-w600'>" + CountData.Rows[0][0].ToString() + @"</span>
                                                        </div>
                                                    </div>                       
                                                </div>

                                            </div>
                                        </div>");
                            break;
                        case 5:
                        case 7:
                            sb.Append(@"<div class='col-xl-3 col-xxl-3 col-sm-12'>
                                    <div class='card' title='" + objData[i].Remarks + @"'>
                                        <div class='social-graph-wrapper " + cValue + @"'>
                                            <div class='media align-items-center'>
                                                <div class='media-body text-left'>
                                                    <p class='fs-18 text-white mb-2'>" + objData[i].ChartName + @"</p>
                                                </div>
                                                <div class='media-body text-right'>
                                                    <span class='fs-18 text-white mb-2 font-w600'>" + CountData.Rows[0][0].ToString() + @"</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='row'>
                                            <div class='col-6 border-right'>
                                                <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                    <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][1].ToString() + @"</span></h4>
                                                    <p class='m-0' style='color:#f96922'>" + CountData.Columns[1].Caption.ToString() + @"</p>
                                                </div>
                                            </div>
                                            <div class='col-6'>
                                                <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                    <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][2].ToString() + @"</span></h4>
                                                    <p class='m-0' style='color:#f5a732'>" + CountData.Columns[2].Caption.ToString() + @"</p>
                                                </div>
                                            </div>                                           
                                        </div>
                                    </div>
                                </div>");
                            break;
                        case 9:
                            sb.Append(@"<div class='col-xl-3 col-xxl-3 col-sm-12'>
                                                        <div class='card'  title='" + objData[i].Remarks + @"'>
                                                            <div class='social-graph-wrapper " + cValue + @"'>
                                                                <div class='media align-items-center'>
                                                                    <div class='media-body text-left'>
                                                                        <p class='fs-18 text-white mb-2'>" + objData[i].ChartName + @"</p>
                                                                    </div>
                                                                    <div class='media-body text-right' style='display:none;'>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class='row'>
                                                                
                                                                <div class='col-4 border-right'>
                                                                    <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                                        <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][0].ToString() + @"</span></h4>
                                                                        <p class='m-0' style='color:#f5a732'>" + CountData.Columns[0].Caption.ToString() + @"</p>
                                                                    </div>
                                                                </div>
                                                                <div class='col-4'>
                                                                    <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                                        <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][1].ToString() + @"</span></h4>
                                                                        <p class='m-0' style='color:#1ca4fc'>" + CountData.Columns[1].Caption.ToString() + @"</p>
                                                                    </div>
                                                                </div>
                                                                <div class='col-4'>
                                                                    <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                                        <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][2].ToString() + @"</span></h4>
                                                                        <p class='m-0' style='color:#ff0000'>" + CountData.Columns[2].Caption.ToString() + @"</p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            
                                                        </div>
                                                    </div>");

                            break;
                        case 10:
                            sb.Append(@"<div class='col-xl-4 col-xxl-3 col-sm-12'>
                                                        <div class='card'  title='" + objData[i].Remarks + @"'>
                                                            <div class='social-graph-wrapper " + cValue + @"'>
                                                                <div class='media align-items-center'>
                                                                    <div class='media-body text-left'>
                                                                        <p class='fs-18 text-white mb-2'>" + objData[i].ChartName + @"</p>
                                                                    </div>
                                                                    <div class='media-body text-right' style='display:none;'>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class='row'>
                                                                <div class='col-3 border-right'>
                                                                    <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                                        <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][0].ToString() + @"</span></h4>
                                                                        <p class='m-0' style='color:#f96922'>" + CountData.Columns[0].Caption.ToString() + @"</p>
                                                                    </div>
                                                                </div>
                                                                <div class='col-3 border-right'>
                                                                    <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                                        <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][1].ToString() + @"</span></h4>
                                                                        <p class='m-0' style='color:#f5a732'>" + CountData.Columns[1].Caption.ToString() + @"</p>
                                                                    </div>
                                                                </div>
                                                                <div class='col-3'>
                                                                    <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                                        <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][2].ToString() + @"</span></h4>
                                                                        <p class='m-0' style='color:#1ca4fc'>" + CountData.Columns[2].Caption.ToString() + @"</p>
                                                                    </div>
                                                                </div>
                                                                <div class='col-3'>
                                                                    <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                                        <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][3].ToString() + @"</span></h4>
                                                                        <p class='m-0' style='color:#ff0000'>" + CountData.Columns[3].Caption.ToString() + @"</p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                             
                                                        </div>
                                                    </div>");

                            break;

                    }
                }
            }
            return sb.ToString();
        }
        private string CreateChart(List<GraphSettingsModel.DashboardOutData> objData, ref List<GraphSettingsModel.ChartListData> chartList, ref StringBuilder sbModal)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < objData.Count; i++)
            {
                if (!string.IsNullOrEmpty(objData[i].Datafile))
                {
                    string PanelChartID = "PanelChart" + objData[i].ChartList;
                    string ChartID = "canChart" + objData[i].ChartList;
                    string ModalChartID = "ModCanChart" + objData[i].ChartList;
                    long GraphType =  objData[i].GraphType;
                    sbModal.Append(@"<div class='modal fade' id='modal" + PanelChartID + @"' data-backdrop='static' data-keyboard='false' tabindex='1000'>
                                        <div class='modal-dialog modal-dialog-centered modal-lg' role='dialog'>
                                            <div class='modal-content'>
                                                <div class='modal-header border-0'>
                                                    <h5 class='modal-title' style='text-transform: inherit;'>" + objData[i].ChartName + @"</h5>
                                                    <button type='button' class='close' data-dismiss='modal' aria-label='Close'>
                                                        <span aria-hidden='true'>&times;</span>
                                                    </button>
                                                </div>
                                                <div class='modal-body' perfect-class='formGroup' style='max-height: calc(100vh - 200px); overflow-y: auto;padding: 1rem;'>
                                                    <div class='row'>");

                    DataTable ChartData = JsonConvert.DeserializeObject<DataTable>(objData[i].Datafile);
                    switch (objData[i].ChartList)
                    {

                        case 1:
                            sb.Append(@"<div class='col-xl-2 col-xxl-2 col-sm-12'></div><div class='col-xl-6 col-xxl-6 col-sm-12' id='" + PanelChartID + @"'>
                                        <div class='card flex-lg-column flex-md-row w-100'>
                                            <div class='card-header border-0 pb-0 flex-wrap' style='background-color:#e1e7e7;'>
                                                <div class='card-header-title cht' modalid='modal" + PanelChartID + @"'>
                                                    <p class='fs-18 mb-2'>" + objData[i].ChartName + @"</p>                                                    
                                                </div>     
                                                <ul class='nav'>
                                                    <button type='button' class='close chartClose' data-dismiss='alert' title='Close' style='margin: -4px -19px 10px 0px;' chartID='" + PanelChartID + @"'><span class='float-right'><i class='fa fa-remove'></i></span></button>
                                                </ul>
                                            </div>
                                            <div class='card-body card-body text-sm-left'>
                                                <div class='row'>
                                                    <div class='col-sm-12 col-12'>
                                                        <div class='row'>");
                            foreach (DataRow row in ChartData.Rows)
                            {
                                var percentage = ((Convert.ToDecimal(row[1]) * 100) / Convert.ToDecimal(row[2]));
                                var chartdata = @"<div class='col-md-6' style='margin-top: 15px;'>
                                                                                                                            <div class='widget-content'>
                                                                                                                                <div class='widget-content-outer'>
                                                                                                                                    <div class='widget-content-wrapper'>
                                                                                                                                        <div class='widget-content-right'>
                                                                                                                                            <div class='text-muted opacity-6'>" + row[0].ToString() + "  (" + row[1].ToString() + "/" + row[2].ToString() + @")</div>
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                    <div class='widget-progress-wrapper mt-1'>
                                                                                                                                        <div class='progress-bar-sm progress-bar-animated-alt progress' style='height: 18px;'>
                                                                                                                                            <div class='progress-bar' role='progressbar'  aria-valuemin='0' aria-valuemax='100' style='width:" + percentage + "%;background-color:" + String.Format("#{0:X6}", randomColor.Next(0x1000000)) + @";'></div>
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                        </div>";
                                sb.Append(chartdata);
                                sbModal.Append(chartdata);
                            }
                            sb.Append(@"</div>                                                        
                                                    </div>
                                                </div>
                                            </div>  
                                            <div class='card-footer' style='padding: 0.1rem 0.5rem 0.1rem;'><span style='font-size:x-small;color: gray;'>" + objData[i].Remarks + @"</span></div>                                                 
                                        </div>
                                    </div>");
                            break;
                        case 4:
                            sb.Append(@"<div class='col-xl-8 col-xxl-8 col-sm-12' id='" + PanelChartID + @"'>
                                        <div class='card flex-lg-column flex-md-row w-100'>
                                            <div class='card-header border-0 pb-0 flex-wrap' style='background-color:#e1e7e7;'>
                                                <div class='card-header-title cht' modalid='modal" + PanelChartID + @"'>
                                                    <p class='fs-18 mb-2'>" + objData[i].ChartName + @"</p>
                                                </div>    
                                                 <ul class='nav'>
                                                    <button type='button' class='close chartClose' data-dismiss='alert' title='Close' style='margin: -4px -19px 10px 0px;' chartID='" + PanelChartID + @"'><span class='float-right'><i class='fa fa-remove'></i></span></button>
                                                </ul>
                                            </div>
                                            <div class='card-body card-body text-sm-left'>
                                                <div class='row'>
                                                <div class='col-sm-12 col-12'>
                                                    <div id='" + ChartID + @"' style='width: 100%; height: 250px; background-color: #FFFFFF;'></div>
                                                </div>
                                            </div>
                                            </div>
                                             <div class='card-footer' style='padding: 0.1rem 0.5rem 0.1rem;'><span style='font-size:x-small;color: gray;'>" + objData[i].Remarks + @"</span></div> 
                                        </div>
                                    </div>");
                            sbModal.Append(@"<div class='col-sm-12 col-12'>
                                                    <div id='" + ModalChartID + @"' style='width: 100%; height: 500px; background-color: #FFFFFF;'></div>
                                                </div>");
                            CreateChartData(ChartData, objData[i].ChartList, ChartID, ModalChartID, objData[i].XAxis, objData[i].YAxis, ref chartList, objData[i].GraphType);
                            break;
                        case 2:
                        case 3:
                        case 8:
                        case 10:
                        case 11:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 28:
                        case 26:
                        case 29:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 12:
                        case 15:
                        case 16:
                        case 14:
                        case 6:
                            sb.Append(@"<div class='col-xl-8 col-xxl-8 col-sm-12' id='" + PanelChartID + @"'>
                                        <div class='card flex-lg-column flex-md-row w-100'>
                                            <div class='card-header border-0 pb-0 flex-wrap' style='background-color:#e1e7e7;'>
                                                <div class='card-header-title cht' modalid='modal" + PanelChartID + @"'>
                                                    <p class='fs-18 mb-2'>" + objData[i].ChartName + @"</p>
                                                </div>   
                                                 <ul class='nav'>
                                                    <button type='button' class='close chartClose' data-dismiss='alert' title='Close' style='margin: -4px -19px 10px 0px;' chartID='" + PanelChartID + @"'><span class='float-right'><i class='fa fa-remove'></i></span></button>
                                                </ul>
                                            </div>
                                            <div class='card-body card-body text-sm-left'>
                                                <div class='row'>
                                                    <div class='col-sm-12 col-12'>
                                                         <canvas id='" + ChartID + @"' style='width:100%;'></canvas>     
                                                    </div>
                                                </div>
                                            </div>
                                             <div class='card-footer' style='padding: 0.1rem 0.5rem 0.1rem;'><span style='font-size:x-small;color: gray;'>" + objData[i].Remarks + @"</span></div> 
                                        </div>
                                    </div>");
                            sbModal.Append(@"<div class='col-sm-12 col-12'>
                                                         <canvas id='" + ModalChartID + @"' style='width:100%;'></canvas>   
                                                    </div>");
                            CreateChartData(ChartData, objData[i].ChartList, ChartID, ModalChartID, objData[i].XAxis, objData[i].YAxis, ref chartList, GraphType);
                            break;


                        case 5:
                       
                       
                            sb.Append(@"<div class='col-xl-8 col-xxl-8 col-sm-12' id='" + PanelChartID + @"'>
                                        <div class='card flex-lg-column flex-md-row w-100'>
                                            <div class='card-header border-0 pb-0 flex-wrap' style='background-color:#e1e7e7;'>
                                                <div class='card-header-title cht' modalid='modal" + PanelChartID + @"'>
                                                    <p class='fs-18 mb-2'>" + objData[i].ChartName + @"</p>
                                                </div>   
                                                 <ul class='nav'>
                                                    <button type='button' class='close chartClose' data-dismiss='alert' title='Close' style='margin: -4px -19px 10px 0px;' chartID='" + PanelChartID + @"'><span class='float-right'><i class='fa fa-remove'></i></span></button>
                                                </ul>
                                            </div>
                                            <div class='card-body card-body text-sm-left'>
                                                <div class='row'>
                                                    <div class='col-sm-12 col-12'>
                                                         <canvas id='" + ChartID + @"' style='width:100%;'></canvas>     
                                                    </div>
                                                </div>
                                            </div>
                                             <div class='card-footer' style='padding: 0.1rem 0.5rem 0.1rem;'><span style='font-size:x-small;color: gray;'>" + objData[i].Remarks + @"</span></div> 
                                        </div>
                                    </div>");
                            sbModal.Append(@"<div class='col-sm-12 col-12'>
                                                         <canvas id='" + ModalChartID + @"' style='width:100%;'></canvas>   
                                                    </div>");
                            CreateChartData(ChartData, objData[i].ChartList, ChartID, ModalChartID, objData[i].XAxis, objData[i].YAxis, ref chartList, objData[i].GraphType);
                            break;
                      
                    

                    }
                    sbModal.Append(@"</div><br/><div class='text-center'><span style='font-size:x-small;color: gray;'>" + objData[i].Remarks + @"</span></div></div></div></div></div>");
                }
            }

            return sb.ToString();
        }
        
        private void CreateChartData(DataTable dt, long chartID, string elementID, string ChartModalID, string XAxis, string YAxis, ref List<GraphSettingsModel.ChartListData> chartList,long GraphType)
        {
            bool showYinHundreds = false;
            bool showY3inHundreds = false;
            bool showXinHundreds = false;
            long DivXAmnt = 0;
            long DivYAmnt = 0;
            long DivY2Amnt = 0;
            long DivY3Amnt = 0;
            long bindvalue = 0;
            bool showY2inHundreds = false;
            decimal sum = 0;
            decimal value=0;
            foreach (DataRow row in dt.Rows)
            {


                sum +=Convert.ToDecimal(row[1]);

            }

            foreach (DataRow row in dt.Rows)
            {
                GraphSettingsModel.ChartListData obj = new GraphSettingsModel.ChartListData();
                obj.ChartID = chartID;
                obj.ChartName = elementID;



                obj.xValues = row[0].ToString();
                obj.yValues = row[1].ToString();
                obj.yColor = String.Format("#{0:X6}", randomColor.Next(0x1000000));
                Double num;
                var isnumber = Double.TryParse(row[1].ToString(), out num);
                if (GraphType == 1 || GraphType==4)
                {
                    if (chartID != 9)
                    {

                        if (!string.IsNullOrEmpty(row[1].ToString()) && isnumber)
                        {
                            var yVal = Math.Abs(Convert.ToDouble(row[1]));
                            if (yVal > 100000 && (DivYAmnt >= 100000 || DivYAmnt == 0))
                            {
                                obj.ShowYInhndreds = true; showYinHundreds = true; DivYAmnt = 10000;
                            }
                            else if (yVal > 10000 && (DivYAmnt >= 10000 || DivYAmnt == 0))
                            {
                                obj.ShowYInhndreds = true; showYinHundreds = true; DivYAmnt = 1000;
                            }
                            else if (yVal > 1000 && (DivYAmnt >= 1000 || DivYAmnt == 0))
                            {
                                obj.ShowYInhndreds = true; showYinHundreds = true; DivYAmnt = 100;
                            }
                        }
                    }
                    isnumber = Double.TryParse(row[0].ToString(), out num);

                    if (!string.IsNullOrEmpty(row[0].ToString()) && isnumber)
                    {
                        var XVal = Math.Abs(Convert.ToDouble(row[0]));

                        if (XVal > 100000 && (DivXAmnt >= 100000 || DivXAmnt == 0))
                        {
                            obj.ShowXInhndreds = true; showXinHundreds = true; DivXAmnt = 10000;
                        }
                        else if (XVal > 10000 && (DivXAmnt >= 10000 || DivXAmnt == 0))
                        {
                            obj.ShowXInhndreds = true; showXinHundreds = true; DivXAmnt = 1000;
                        }
                        else if (XVal > 1000 && (DivXAmnt >= 1000 || DivXAmnt == 0))
                        {
                            obj.ShowXInhndreds = true; showXinHundreds = true; DivXAmnt = 100;
                        }
                    }
                    // if (chartID != 2)
                    //{
                }
                else if(GraphType == 2 || GraphType == 4)
                {
                    isnumber = Double.TryParse(row[1].ToString(), out num);
                    if (!string.IsNullOrEmpty(row[1].ToString()) && isnumber)
                    {
                        var yVal = Math.Abs(Convert.ToDouble(row[1]));
                        if (yVal > 100000 && (DivY2Amnt >= 100000 || DivY2Amnt == 0))
                        {
                            obj.ShowY2Inhndreds = true; showY2inHundreds = true; DivY2Amnt = 10000;
                        }
                        else if (yVal > 10000 && (DivY2Amnt >= 10000 || DivY2Amnt == 0))
                        {
                            obj.ShowY2Inhndreds = true; showY2inHundreds = true; DivY2Amnt = 1000;
                        }
                        else if (yVal > 1000 && (DivY2Amnt >= 1000 || DivY2Amnt == 0))
                        {
                            obj.ShowY2Inhndreds = true; showY2inHundreds = true; DivY2Amnt = 100;
                        }
                    }
                    // decimal yvalue= (row[1].ToString())/ (sum)
                    obj.ySecondValues = Convert.ToString(Math.Round((Convert.ToDecimal(row[1].ToString()) / sum) * 100, 2));
                    obj.ySecondColor = String.Format("#{0:X6}", randomColor.Next(0x1000000));
                }
                   
                    //if (dt.Columns.Count > 3)
                    //{
                    //    isnumber = Double.TryParse(row[3].ToString(), out num);

                    //    if (!string.IsNullOrEmpty(row[3].ToString()) && isnumber)
                    //    {
                    //        var yVal = Math.Abs(Convert.ToDouble(row[3]));
                    //        if (yVal > 100000 && (DivY3Amnt >= 100000 || DivY3Amnt == 0))
                    //        {
                    //            obj.ShowY3Inhndreds = true; showY3inHundreds = true; DivY3Amnt = 10000;
                    //        }
                    //        else if (yVal > 10000 && (DivY3Amnt >= 10000 || DivY3Amnt == 0))
                    //        {
                    //            obj.ShowY3Inhndreds = true; showY3inHundreds = true; DivY3Amnt = 1000;
                    //        }
                    //        else if (yVal > 1000 && (DivY3Amnt >= 1000 || DivY3Amnt == 0))
                    //        {
                    //            obj.ShowY3Inhndreds = true; showY3inHundreds = true; DivY3Amnt = 100;
                    //        }
                    //    }
                    //    obj.yThirdValues = row[3].ToString();
                    //    obj.yThirdColor = String.Format("#{0:X6}", randomColor.Next(0x1000000));
                    //}
              //  }
                if (showYinHundreds)
                {
                    switch (DivYAmnt)
                    {
                        case 100: obj.YAxis = YAxis + " (In Hundreds)"; break;
                        case 1000: obj.YAxis = YAxis + " (In Thousands)"; break;
                        case 10000: obj.YAxis = YAxis + " (In Ten Thousands)"; break;
                        case 0: obj.YAxis = YAxis; break;

                    }

                }
                else if (showY2inHundreds)
                {
                    switch (DivY2Amnt)
                    {
                        case 100: obj.YAxis = YAxis + " (In Hundreds)"; break;
                        case 1000: obj.YAxis = YAxis + " (In Thousands)"; break;
                        case 10000: obj.YAxis = YAxis + " (In Ten Thousands)"; break;
                        case 0: obj.YAxis = YAxis; break;
                    }

                }
                else if (showY3inHundreds)
                {
                    switch (DivY3Amnt)
                    {
                        case 100: obj.YAxis = YAxis + " (In Hundreds)"; break;
                        case 1000: obj.YAxis = YAxis + " (In Thousands)"; break;
                        case 10000: obj.YAxis = YAxis + " (In Ten Thousands)"; break;
                        case 0: obj.YAxis = YAxis; break;
                    }

                }
                else
                {
                    obj.YAxis = YAxis;
                }
                if (showXinHundreds)
                {
                    switch (DivXAmnt)
                    {
                        case 100: obj.XAxis = XAxis + " (In Hundreds)"; break;
                        case 1000: obj.XAxis = XAxis + " (In Thousands)"; break;
                        case 10000: obj.XAxis = XAxis + " (In Ten Thousands)"; break;
                        case 0: obj.XAxis = XAxis; break;
                    }

                }
                else
                {
                    obj.XAxis = XAxis;
                }
                obj.ChartModalName = ChartModalID;
                chartList.Add(obj);
            }
            foreach (var item in chartList.Where(w => w.ChartID == chartID))
            {
                item.ShowYInhndreds = showYinHundreds;
                item.ShowY2Inhndreds = showY2inHundreds;
                item.ShowXInhndreds = showXinHundreds;
                item.DevideXAmnt = DivXAmnt;
                item.DevideYAmnt = DivYAmnt;
                item.DevideY2Amnt = DivY2Amnt;
                item.DevideY3Amnt = DivY3Amnt;
            }
        }

        public ActionResult GetSubModule(GraphSettingsModel.customerview cr)
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
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            #endregion ::  Check User Session to verifyLogin  ::
            GraphSettingsModel.customerview Acctview = new GraphSettingsModel.customerview();
            GraphSettingsModel objact = new GraphSettingsModel();
            var SubModuleInfo = objact.GetSubModule(input: new GraphSettingsModel.SubModule { Mode = 116, Group = cr.ID_Mode }, companyKey: _userLoginInfo.CompanyKey);
            return Json(SubModuleInfo, JsonRequestBehavior.AllowGet);


        }
        public ActionResult GetGraphType(GraphSettingsModel.customerview cr)
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
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            #endregion ::  Check User Session to verifyLogin  ::
            GraphSettingsModel.customerview Acctview = new GraphSettingsModel.customerview();
            GraphSettingsModel objact = new GraphSettingsModel();
            var SubModuleInfo = objact.GetGraphType(input: new GraphSettingsModel.SubModule { Mode = 132, Group = cr.ID_SubModule }, companyKey: _userLoginInfo.CompanyKey);
            return Json(SubModuleInfo, JsonRequestBehavior.AllowGet);


        }
    }

}







