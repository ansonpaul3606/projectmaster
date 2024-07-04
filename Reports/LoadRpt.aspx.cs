using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using PerfectWebERP.Models;
using PerfectWebERP.ReportModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.Shared;
using System.Web.Mvc;
using System.Globalization;
using System.Text;
using PerfectWebERP.Filters;

namespace PerfectWebERP.Reports
{
    [CheckSessionTimeOut]
    public partial class LoadRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //LoadReport();
            LoadcrReport();
        }

        //public void LoadReport()
        //{


        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
        //    Rpparams reportparam = (Rpparams)Session["Rpparams"];
        //    try
        //    {

        //        //string Rptype = Session["Rptype"].ToString();
        //        //var Reportname = Session["Reportname"].ToString();
        //        //var Reportdata = Session["Reportdata"];
        //        /*var Fdate = reportparam.FromDate;*///Session["Fromdate"].ToString();
        //        /*var Tdate = reportparam.ToDate;*///Session["ToDate"].ToString();
        //        //string FromDate = reportparam.FromDate; //Fdate.ToString();
        //        //string Todate = reportparam.ToDate;// Tdate.ToString();
        //        //string User = Session["User"].ToString();
        //        // string FK_Employee = reportparam.FK_Employee; //Session["FK_Employee"].ToString();
        //        //int Status = reportparam.Status;//Session["Status"].ToString();
        //        string imlogo = Session["RpFieldptype"].ToString();
        //        ReportLoadModel reportload = new ReportLoadModel();

        //        byte TableCount = 1;
        //        //TableCount = reportparam.TableCount;
        //        DataSet ds = new DataSet();
        //        DataTable dt;
        //        do
        //        {
        //            dt = new DataTable();
        //            var outputData = reportload.GetReport(input: new ReportLoadModel.Reportproparam
        //            {


        //                FK_Company = _userLoginInfo.FK_Company,
        //                FK_Branch = _userLoginInfo.FK_Branch,
        //                FK_Employee = reportparam.FK_Employee,//FK_Employee,
        //                FromDate = Convert.ToDateTime(reportparam.FromDate),//Convert.ToDateTime(FromDate),
        //                ToDate = Convert.ToDateTime(reportparam.ToDate),//Convert.ToDateTime(reportparam.Todate),
        //                Status = Convert.ToInt64(reportparam.Status),//Convert.ToInt64(Status),
        //                TableCount = TableCount,

        //            }, companyKey: _userLoginInfo.CompanyKey);
        //            dt = outputData.Data;
        //            if (/*ds.Tables.*/dt == null)
        //            {
        //                Response.Redirect("~/Reports/Errorpage.aspx");
        //            }
        //            dt.TableName = reportparam.ReportSchema + (reportparam.TableCount == 0 ? "" : TableCount.ToString()).ToString();
        //            ds.Tables.Add(dt);
        //            TableCount += 1;
        //        }
        //        while (TableCount <= reportparam.TableCount);

        //        ds.WriteXmlSchema(Server.MapPath("~/Reports/Schema") + "//" + reportparam.ReportSchema + ".xsd");

        //        if (ds.Tables.Count == 0)
        //        {
        //            Response.Redirect("~/Reports/Errorpage.aspx");
        //        }

        //        ReportDocument report = new ReportDocument();

        //        report.Load(Path.Combine(Server.MapPath("~/Reports"), reportparam.Reportname));
        //        report.SetDataSource(ds);//Reportdata);
        //        report.DataDefinition.FormulaFields["From"].Text = "'" + reportparam.FromDate + "'";
        //        report.DataDefinition.FormulaFields["To"].Text = "'" + reportparam.ToDate + "'";
        //        report.DataDefinition.FormulaFields["Username"].Text = "'" + _userLoginInfo.UserName + "'";
        //        report.DataDefinition.FormulaFields["imlogo"].Text = "'" + imlogo + "'";

        //        // CrystalReportViewer1.ReportSource = report;
        //        //CrystalReportViewer1.DataBind();


        //        ExportCrystalReport(report);
        //    }
        //    catch
        //     (Exception ex)
        //    {
        //        throw;
        //    }

        //}
        public void LoadcrReport()
        {


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            Reportpassparams reportpassparam = (Reportpassparams)Session["Reportpassparams"];
            try
            {
                //string imlogo = Session["RpFieldptype"].ToString();
                ReportLoadModel reportload = new ReportLoadModel();

                byte TableCount = 1;
                ////TableCount = reportparam.TableCount;
                DataSet ds = new DataSet();
                DataTable dt;
                do
                {
                    dt = new DataTable();
                    if (reportpassparam.Reportsection == "Lead")
                    {
                        var outputData = reportload.ReportGet(input: new ReportLoadModel.Reportprocedureparams
                        {

                            ReportMode = reportpassparam.ReportMode,
                            FromDate = reportpassparam.FromDate == null ? null : reportpassparam.FromDate,
                            ToDate = reportpassparam.ToDate == null ? null : reportpassparam.ToDate,
                            FK_Product = reportpassparam.FK_Product,
                            FK_Branch = reportpassparam.FK_Branch,
                            FK_Employee = reportpassparam.FK_Employee,
                            FK_Priority = reportpassparam.FK_Priority,
                            FollowUpAction = reportpassparam.FollowUpAction,
                            FollowUpType = reportpassparam.FollowUType,
                            FK_Company = _userLoginInfo.FK_Company,
                            FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                            EntrBy = _userLoginInfo.EntrBy,
                            FK_Machine = _userLoginInfo.FK_Machine,
                            LeadNo = reportpassparam.LeadNo,
                            Criteria = reportpassparam.Criteria,
                            //Status = Convert.ToInt64(reportparam.Status),
                            Status = reportpassparam.Status,
                            FK_CollectedBy = reportpassparam.FK_CollectedBy,
                            FK_Area = reportpassparam.FK_Area,
                            Category = reportpassparam.Category,
                            TableCount = TableCount,

                        }, companyKey: _userLoginInfo.CompanyKey);
                        dt = outputData.Data;
                    }
                    if (reportpassparam.Reportsection == "Customer Service")
                    {
                        var outputData = reportload.ServiceReportGet(input: new ReportLoadModel.ServiceReportprocedureparams
                        {

                            ReportMode = reportpassparam.ReportMode,
                            FromDate = reportpassparam.FromDate == null ? null : reportpassparam.FromDate,
                            ToDate = reportpassparam.ToDate == null ? null : reportpassparam.ToDate,
                            FK_Product = reportpassparam.FK_Product,
                            FK_Branch = reportpassparam.FK_Branch,
                            FK_Employee = reportpassparam.FK_Employee,
                            FK_Priority = reportpassparam.FK_Priority,
                            //ComplaintProductType = reportpassparam.ComplaintProductType,
                            ComplaintType = reportpassparam.ComplaintType,
                            FK_Company = _userLoginInfo.FK_Company,
                            FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                            EntrBy = _userLoginInfo.EntrBy,
                            FK_Machine = _userLoginInfo.FK_Machine,
                            TicketNo = reportpassparam.TicketNo,
                            Criteria = reportpassparam.Criteria,
                            //Status = Convert.ToInt64(reportparam.Status),
                            Status = reportpassparam.Status,
                            FK_Area = reportpassparam.FK_Area,
                            FK_Category = reportpassparam.FK_Category,
                            FK_NextAction = reportpassparam.FK_NextAction,
                            DueDaysFrom = reportpassparam.DueDaysFrom,
                            DueDaysTo = reportpassparam.DueDaysTo,
                            DueCriteria = reportpassparam.DueCriteria,
                            ComplaintService = reportpassparam.ComplaintService,
                            //FK_Servicetype=reportpassparam.FK_Servicetype,
                            ReplacementType= reportpassparam.ReplacementType,
                            TableCount = TableCount,

                        }, companyKey: _userLoginInfo.CompanyKey);
                        dt = outputData.Data;
                    }
                    if (reportpassparam.Reportsection == "Project")
                    {
                        var outputData = reportload.ProjectReportGet(input: new ReportLoadModel.ProjectReportprocedureparams
                        {

                            ReportMode = reportpassparam.ReportMode,
                            FromDate = reportpassparam.FromDate == null ? null : reportpassparam.FromDate,
                            ToDate = reportpassparam.ToDate == null ? null : reportpassparam.ToDate,

                            FK_Company = _userLoginInfo.FK_Company,
                            FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                            EntrBy = _userLoginInfo.EntrBy,
                            FK_Machine = _userLoginInfo.FK_Machine,
                            LeadNo = reportpassparam.LeadNo,
                            Criteria = reportpassparam.Criteria,


                            FK_Area = reportpassparam.FK_Area,
                            Category = reportpassparam.Category,
                            TableCount = TableCount,

                        }, companyKey: _userLoginInfo.CompanyKey);
                        dt = outputData.Data;
                    }
                    if (reportpassparam.Reportsection == "SalesReport")
                    {
                        if (reportpassparam.ReportMode == 4)
                        {                          
                            reportpassparam.TableCount = 6;
                        }
                        var outputData = reportload.GetSalesReport(input: new ReportLoadModel.SalesReports
                        {
                     
                            ReportMode = reportpassparam.ReportMode,
                            FromDate = reportpassparam.FromDate == null ? null : reportpassparam.FromDate,
                            ToDate = reportpassparam.ToDate == null ? null : reportpassparam.ToDate,
                            BillNo= reportpassparam.BillNo,
                            ID_Master= reportpassparam.ID_Master,
                            FK_Company = _userLoginInfo.FK_Company,
                            FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                            EntrBy = _userLoginInfo.EntrBy,
                            FK_Machine = _userLoginInfo.FK_Machine,
                         
                            Criteria = reportpassparam.Criteria,
                            FK_Product= reportpassparam.FK_Product,
                            FK_BillType = reportpassparam.billtype,
                            FK_PaymentMethod = reportpassparam.pmtype,
                            FK_Department = reportpassparam.FK_Department,
                            FK_Branch = reportpassparam.FK_Branch,
                            FK_Category = reportpassparam.FK_Category,
                            TableCount = TableCount,
                            TranSMode= reportpassparam.TranSMode
                        }, companyKey: _userLoginInfo.CompanyKey);
                        dt = outputData.Data;
                    }
                    if (reportpassparam.Reportsection == "SalesOrderReport")
                    {
                        if (reportpassparam.ReportMode == 4)
                        {
                            reportpassparam.TableCount = 6;
                        }
                        var outputData = reportload.GetSalesReport(input: new ReportLoadModel.SalesReports
                        {

                            ReportMode = reportpassparam.ReportMode,
                            FromDate = reportpassparam.FromDate == null ? null : reportpassparam.FromDate,
                            ToDate = reportpassparam.ToDate == null ? null : reportpassparam.ToDate,
                            BillNo = "",
                            FK_Company = _userLoginInfo.FK_Company,
                            FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                            EntrBy = _userLoginInfo.EntrBy,
                            FK_Machine = _userLoginInfo.FK_Machine,

                            Criteria = reportpassparam.Criteria,
                            FK_Product = reportpassparam.FK_Product,
                            FK_BillType = reportpassparam.billtype,
                            FK_Department = reportpassparam.FK_Department,
                            FK_Branch = reportpassparam.FK_Branch,
                            FK_Category = reportpassparam.FK_Category,
                            TableCount = TableCount,
                            TranSMode = reportpassparam.TranSMode
                        }, companyKey: _userLoginInfo.CompanyKey);
                        dt = outputData.Data;
                    }
                    if (reportpassparam.Reportsection == "CurrentStock")
                    {

                        var outputData = reportload.GetStockReport(input: new ReportLoadModel.StockReports
                        {

                            ReportMode = reportpassparam.ReportMode,
                            FromDate = reportpassparam.FromDate == null ? null : reportpassparam.FromDate,
                            ToDate = reportpassparam.ToDate == null ? null : reportpassparam.ToDate,

                            FK_Company = _userLoginInfo.FK_Company,
                            FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                            EntrBy = _userLoginInfo.EntrBy,
                            FK_Machine = _userLoginInfo.FK_Machine,

                            Criteria = reportpassparam.Criteria,
                            FK_Product = reportpassparam.FK_Product,
                            FK_Employee = reportpassparam.FK_Employee,
                            FK_Department = reportpassparam.FK_Department,
                            FK_Branch = reportpassparam.FK_Branch,
                            FK_EmployeeTo = reportpassparam.FK_EmployeeTo,
                            FK_DepartmentTo = reportpassparam.FK_DepartmentTo,
                            FK_BranchTo = reportpassparam.FK_BranchTo,
                            FK_Category = reportpassparam.FK_Category,
                            FK_ProductOld = reportpassparam.FK_ProductOld,
                            FK_ProductNew = reportpassparam.FK_ProductNew,
                            TableCount = TableCount,

                        }, companyKey: _userLoginInfo.CompanyKey);
                        dt = outputData.Data;
                    }

                    if (reportpassparam.Reportsection == "PayRoll")
                    {
                        if (reportpassparam.ReportMode == 1)
                        {
                            reportpassparam.TableCount = 2;
                        }
                       
                        if (reportpassparam.ReportMode == 4)
                        {
                            reportpassparam.TableCount = 2;
                        }
                        var outputData = reportload.GetPayRollReport(input: new ReportLoadModel.PayRollReports
                        {
                            
                        ReportMode = reportpassparam.ReportMode,
                            FromDate = reportpassparam.FromDate == null ? null : reportpassparam.FromDate,
                            ToDate = reportpassparam.ToDate == null ? null : reportpassparam.ToDate,

                            FK_Company = _userLoginInfo.FK_Company,
                            FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                            EntrBy = _userLoginInfo.EntrBy,
                            FK_Machine = _userLoginInfo.FK_Machine,

                            Criteria = reportpassparam.Criteria,
                           
                            FK_Employee = reportpassparam.FK_Employee,
                            FK_Department = reportpassparam.FK_Department,
                            FK_Branch = reportpassparam.FK_Branch,
                          
                            TableCount = TableCount,

                        }, companyKey: _userLoginInfo.CompanyKey);
                        dt = outputData.Data;
                    }
                    if (reportpassparam.Reportsection == "PurchaseReport")
                    {
                        if (reportpassparam.ReportMode == 4)
                        {
                            reportpassparam.TableCount = 6;
                        }
                        var outputData = reportload.GetPurchaseReport(input: new ReportLoadModel.PurchaseReportProcedureparms
                        {

                            ReportMode = reportpassparam.ReportMode,
                            FromDate = reportpassparam.FromDate == null ? null : reportpassparam.FromDate,
                            ToDate = reportpassparam.ToDate == null ? null : reportpassparam.ToDate,
                            BillNo = reportpassparam.BillNo,
                            FK_Company = _userLoginInfo.FK_Company,
                            FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                            EntrBy = _userLoginInfo.EntrBy,
                            FK_Machine = _userLoginInfo.FK_Machine,
                            FK_Supplier= reportpassparam.FK_Supplier,

                            Criteria = reportpassparam.Criteria,
                            FK_Product = reportpassparam.FK_Product,
                            FK_BillType = reportpassparam.billtype,
                            FK_Department = reportpassparam.FK_Department,
                            FK_Branch = reportpassparam.FK_Branch,
                            FK_Category = reportpassparam.FK_Category,
                            TableCount = TableCount,
                            TranSMode = reportpassparam.TranSMode
                        }, companyKey: _userLoginInfo.CompanyKey);
                        dt = outputData.Data;
                    }
                    if (reportpassparam.Reportsection == "PurchaseOrderReport")
                    {
                        if (reportpassparam.ReportMode == 4)
                        {
                            reportpassparam.TableCount = 6;
                        }
                        var outputData = reportload.GetPurchaseReport(input: new ReportLoadModel.PurchaseReportProcedureparms
                        {

                            ReportMode = reportpassparam.ReportMode,
                            FromDate = reportpassparam.FromDate == null ? null : reportpassparam.FromDate,
                            ToDate = reportpassparam.ToDate == null ? null : reportpassparam.ToDate,
                            BillNo = reportpassparam.BillNo,
                            FK_Company = _userLoginInfo.FK_Company,
                            FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                            EntrBy = _userLoginInfo.EntrBy,
                            FK_Machine = _userLoginInfo.FK_Machine,
                            FK_Supplier = reportpassparam.FK_Supplier,

                            Criteria = reportpassparam.Criteria,
                            FK_Product = reportpassparam.FK_Product,
                            FK_BillType = reportpassparam.billtype,
                            FK_Department = reportpassparam.FK_Department,
                            FK_Branch = reportpassparam.FK_Branch,
                            FK_Category = reportpassparam.FK_Category,
                            TableCount = TableCount,
                            TranSMode = reportpassparam.TranSMode
                        }, companyKey: _userLoginInfo.CompanyKey);
                        dt = outputData.Data;
                    }
                    if (reportpassparam.Reportsection == "PurchaseReturnReport")
                    {
                        if (reportpassparam.ReportMode == 4)
                        {
                            reportpassparam.TableCount = 6;
                        }
                        var outputData = reportload.GetPurchaseReport(input: new ReportLoadModel.PurchaseReportProcedureparms
                        {

                            ReportMode = reportpassparam.ReportMode,
                            FromDate = reportpassparam.FromDate == null ? null : reportpassparam.FromDate,
                            ToDate = reportpassparam.ToDate == null ? null : reportpassparam.ToDate,
                            BillNo = reportpassparam.BillNo,
                            FK_Company = _userLoginInfo.FK_Company,
                            FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                            EntrBy = _userLoginInfo.EntrBy,
                            FK_Machine = _userLoginInfo.FK_Machine,
                            FK_Supplier = reportpassparam.FK_Supplier,

                            Criteria = reportpassparam.Criteria,
                            FK_Product = reportpassparam.FK_Product,
                            FK_BillType = reportpassparam.billtype,
                            FK_Department = reportpassparam.FK_Department,
                            FK_Branch = reportpassparam.FK_Branch,
                            FK_Category = reportpassparam.FK_Category,
                            TableCount = TableCount,
                            TranSMode = reportpassparam.TranSMode
                        }, companyKey: _userLoginInfo.CompanyKey);
                        dt = outputData.Data;
                    }
                    if (reportpassparam.Reportsection == "Invoice")
                    {
                        reportpassparam.TableCount = 5;
                        var outputData = reportload.GetInvoice(input: new ReportLoadModel.invReports
                        {                          
                            FK_SALES = reportpassparam.FK_SALES,
                            TableCount = TableCount,
                           
                        }, companyKey: _userLoginInfo.CompanyKey);
                        dt = outputData.Data;
                    }

                    if(reportpassparam.Reportsection == "ServiceInvoice")
                    {
                        reportpassparam.TableCount = 6;
                        var outputData = reportload.GetServiceInvoice(input: new ReportLoadModel.serviceReports
                        {
                            FK_ServiceBill = reportpassparam.FK_ServiceBill,
                            TableCount = TableCount,
                        }, companyKey: _userLoginInfo.CompanyKey);
                        dt = outputData.Data;

                    }

                    if (reportpassparam.Reportsection == "AMCWarrantyInvoice")
                    {
                        reportpassparam.TableCount = 4;
                        var outputData = reportload.GetAMCWarrantyInvoice(input: new ReportLoadModel.AMCWarrantyReports
                        {
                            FK_Master = reportpassparam.FK_Master,
                            Modes = reportpassparam.Modes,
                            TableCount = TableCount,

                        }, companyKey: _userLoginInfo.CompanyKey);
                        dt = outputData.Data;
                    }


                    if (reportpassparam.Reportsection == "GSTReport")
                    {

                        var outputData = reportload.GetGSTReport(input: new ReportLoadModel.GSTReports
                        {

                            ReportMode = reportpassparam.ReportMode,
                            FromDate = reportpassparam.FromDate == null ? null : reportpassparam.FromDate,
                            ToDate = reportpassparam.ToDate == null ? null : reportpassparam.ToDate,

                            FK_Company = _userLoginInfo.FK_Company,
                            FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                            EntrBy = _userLoginInfo.EntrBy,
                            FK_Machine = _userLoginInfo.FK_Machine,

                            FK_Department = reportpassparam.FK_Department,
                            FK_Branch = reportpassparam.FK_Branch,
                            FK_TaxType=reportpassparam.FK_TaxType,
                            FK_Customer= reportpassparam.FK_Customer,
                            FK_Supplier = reportpassparam.FK_Supplier,
                            Mode = reportpassparam.Mode,
                            TableCount = TableCount,

                        }, companyKey: _userLoginInfo.CompanyKey);
                        dt = outputData.Data;
                    }
                    if (reportpassparam.Reportsection == "AccountsReport")
                    {
                        reportpassparam.TableCount = 2;
                        var outputData = reportload.GetAccountsReport(input: new ReportLoadModel.AccountsReportProcedure
                        {
                            ReportMode = reportpassparam.ReportMode,
                            FK_Branch = reportpassparam.FK_Branch,
                            FromDate = reportpassparam.FromDate == null ? null : reportpassparam.FromDate,
                            ToDate = reportpassparam.ToDate == null ? null : reportpassparam.ToDate,
                            FK_AccountHead= reportpassparam.FK_AccountHead,
                            FK_AccountSubHead=reportpassparam.FK_AccountHeadSub,
                            FK_Company = _userLoginInfo.FK_Company,
                            FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                            EntrBy = _userLoginInfo.EntrBy,
                            FK_Machine = _userLoginInfo.FK_Machine,                            
                            TableCount = TableCount,

                        }, companyKey: _userLoginInfo.CompanyKey);
                        dt = outputData.Data;
                    }
                    //dt = outputData.Data;
                    //if (dt.Rows.Count > 0 || TableCount>1)
                    //{
                    dt.TableName = reportpassparam.ReportSchema + (reportpassparam.TableCount == 0 ? "" : TableCount.ToString()).ToString();
                        ds.Tables.Add(dt);
              
                    TableCount += 1;
                    //}
                    //else 
                    //{
                    //    Response.Redirect("~/Reports/Errorpage.aspx");
                    //    reportpassparam.TableCount = 0;

                    //}

                }
                while (TableCount <= reportpassparam.TableCount);

                ds.WriteXmlSchema(Server.MapPath("~/Reports/Schema") + "//" + reportpassparam.ReportSchema + ".xsd");

                //if (ds.Tables.Count == 0)
                //{
                //    Response.Redirect("~/Reports/Errorpage.aspx");
                //}

                ReportDocument report = new ReportDocument();
                if (reportpassparam.Reportsection == "AMCWarrantyInvoice")
                {


                    report.Load(Path.Combine(Server.MapPath("~/Reports"), reportpassparam.Reportname));
                    report.SetDataSource(ds);

                    report.DataDefinition.FormulaFields["User"].Text = "'" + _userLoginInfo.UserName + "'";

                    report.SetParameterValue("Reportname", reportpassparam.filename);


                }
                else if (reportpassparam.Reportsection == "Invoice")
                {
                   

                    report.Load(Path.Combine(Server.MapPath("~/Reports"), reportpassparam.Reportname));
                    report.SetDataSource(ds);
                    
                    report.DataDefinition.FormulaFields["User"].Text = "'" + _userLoginInfo.UserName + "'";

                    report.SetParameterValue("Reportname", reportpassparam.filename);

                   
                }
                else if(reportpassparam.ReportFrom == "LeadManagementGrid")
                {
                    report.Load(Path.Combine(Server.MapPath("~/Reports"), reportpassparam.Reportname));
                    report.SetDataSource(ds);

                    report.DataDefinition.FormulaFields["User"].Text = "'" + _userLoginInfo.UserName + "'";

                    report.SetParameterValue("Reportname", reportpassparam.filename);
                    report.SetParameterValue("Branchname", reportpassparam.Branchname);
                    report.SetParameterValue("duedayscustom", "No Dues");
                    report.SetParameterValue("Companyname", reportpassparam.CompName);
                }
                else if (reportpassparam.Reportsection == "ServiceInvoice")
                {


                    report.Load(Path.Combine(Server.MapPath("~/Reports"), reportpassparam.Reportname));
                    report.SetDataSource(ds);

                    report.DataDefinition.FormulaFields["User"].Text = "'" + _userLoginInfo.UserName + "'";

                    report.SetParameterValue("Reportname", reportpassparam.filename);


                }
                else if(reportpassparam.Reportsection == "Lead")
                {
                    DateTime dtf = DateTime.ParseExact(reportpassparam.FromDate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                    DateTime dtt = DateTime.ParseExact(reportpassparam.ToDate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                    string FromDate = dtf.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
                    string ToDate = dtt.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);

                    report.Load(Path.Combine(Server.MapPath("~/Reports"), reportpassparam.Reportname));
                    report.SetDataSource(ds);
                    report.DataDefinition.FormulaFields["From"].Text = "'" + FromDate + "'";
                    report.DataDefinition.FormulaFields["To"].Text = "'" + ToDate + "'";
                    report.DataDefinition.FormulaFields["User"].Text = "'" + _userLoginInfo.UserName + "'";

                    report.SetParameterValue("Companyname", reportpassparam.CompName);
                    report.SetParameterValue("Reportname", reportpassparam.filename);

                    report.SetParameterValue("Branchname", reportpassparam.Branchname);
                    report.SetParameterValue("CompanyCategory", _userLoginInfo.CompCategory);
                    if (reportpassparam.Filter != null)
                    {
                        report.SetParameterValue("Filter", reportpassparam.Filter);
                    }
                    else
                    {
                        report.SetParameterValue("Filter", string.Empty);
                    }
                    report.SetParameterValue("duedayscustom", "No Dues");
                }
                else
                {
                    DateTime dtf = DateTime.ParseExact(reportpassparam.FromDate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                    DateTime dtt = DateTime.ParseExact(reportpassparam.ToDate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                    string FromDate = dtf.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
                    string ToDate = dtt.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);

                    report.Load(Path.Combine(Server.MapPath("~/Reports"), reportpassparam.Reportname));
                    report.SetDataSource(ds);
                    report.DataDefinition.FormulaFields["From"].Text = "'" + FromDate + "'";
                    report.DataDefinition.FormulaFields["To"].Text = "'" + ToDate + "'";
                    report.DataDefinition.FormulaFields["User"].Text = "'" + _userLoginInfo.UserName + "'";

                    report.SetParameterValue("Companyname", reportpassparam.CompName);
                    report.SetParameterValue("Reportname", reportpassparam.filename);

                    report.SetParameterValue("Branchname", reportpassparam.Branchname);
                    if (reportpassparam.Filter != null)
                    {
                        report.SetParameterValue("Filter", reportpassparam.Filter);
                    }
                    else
                    {
                        report.SetParameterValue("Filter", string.Empty);
                    }
                    report.SetParameterValue("duedayscustom", "No Dues");
                    // report.SetParameterValue("Pm-Ledger2.Prouct", string.Empty);

                    // CrystalReportViewer1.ReportSource = report;
                    //  CrystalReportViewer1.DataBind();

                }
                if (reportpassparam.Reportsection == "PayRoll")
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (reportpassparam.ReportMode ==1)
                        {
                            if (ds.Tables.Count > 1)
                            {
                               
                                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
                                {
                                   

                                    ExportCrystalReport(report);
                                }
                            }
                        }
                        else
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                ExportCrystalReport(report);
                            }
                        }
                    }
                }
                else
                {
                    ExportCrystalReport(report);
                }

                
            }
            catch
             (Exception ex)
            {  
               throw;
            }
           

        }

        //private void ExportCrystalReport(ReportDocument reportDocument)
        //{
        //    Reportpassparams reportpassparam = (Reportpassparams)Session["Reportpassparams"];
        //    int Rptype = reportpassparam.Rptype;
        //    var Filename = reportpassparam.filename;
        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
        //    if (Rptype == 1)
        //    {
        //        reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, Filename);
        //        reportDocument.Close();
        //        reportDocument.Dispose();
        //    }
        //    if (Rptype == 2)
        //    {
        //        reportDocument.ExportToHttpResponse(ExportFormatType.Excel, Response, false, Filename);
        //        reportDocument.Close();
        //        reportDocument.Dispose();
        //    }
        //    if (Rptype == 3)
        //    {
        //        reportDocument.ExportToHttpResponse(ExportFormatType.Excel, Response, false, Filename);
        //        reportDocument.Close();
        //        reportDocument.Dispose();
        //    }
        //}


        private void ExportCrystalReport(ReportDocument reportDocument)
        {
            try
            {
                

                Reportpassparams reportpassparam = (Reportpassparams)Session["Reportpassparams"];
                int Rptype = reportpassparam.Rptype;
                var Filename = reportpassparam.filename;
                UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
                System.IO.Stream oStream = null;
                byte[] byteArray = null;

             
             
                if (Rptype == 1)
                {
                    oStream = reportDocument.ExportToStream(ExportFormatType.PortableDocFormat);
                    byteArray = new byte[oStream.Length];
                    oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.BinaryWrite(byteArray);
                    Response.Flush();
                    Response.Close();

                }
                if (Rptype == 2)
                {
                    reportDocument.ExportToHttpResponse(ExportFormatType.Excel, Response, false, Filename);
                    reportDocument.Close();
                    reportDocument.Dispose();

                }



            }
            catch (System.Threading.ThreadAbortException)
            {
                //ThreadException can happen for internale Response implementation
            }
            catch (Exception ex)
            {
              
                throw ex;
            }
        }

    }
}