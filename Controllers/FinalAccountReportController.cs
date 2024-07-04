using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace PerfectWebERP.Controllers
{
    public class FinalAccountReportController : Controller
    {
        // GET: FinalAccountReport

        public ActionResult Index(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.mtd = mtd;
            return View();
        }

        public ActionResult FinalAccountreportForm(string mtd)
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

            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;

            int userrole = 0;
            if (_userLoginInfo.UsrrlAdmin == true)
            {
                userrole = 1;
            }
            else
            {
                userrole = 0;
            }

            AccountGeneralReportModel.AccountGeneralReportView statusobj = new AccountGeneralReportModel.AccountGeneralReportView();
            var branch = Common.GetDataViaQuery<AccountGeneralReportModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch B LEFT JOIN  Users U ON U.FK_Branch = B.ID_Branch  AND U.ID_Users=" + _userLoginInfo.ID_Users,
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch",
                Criteria = "B.cancelled=0 AND B.Passed =1 AND B.FK_Company=" + _userLoginInfo.FK_Company + "AND  B.ID_Branch = CASE WHEN " + userrole + "= 1 THEN B.ID_Branch ELSE U.FK_Branch END  ",
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);

            statusobj.BranchList = branch.Data;

            var Transtypelist = Common.GetDataViaQuery<AccountGeneralReportModel.TransactionType>(parameters: new APIParameters
            {
                TableName = "TransType",
                SelectFields = "ID_TransType AS TransTypeID,TransType AS TransType",
                Criteria = "",
                SortFields = "ID_TransType",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);

            statusobj.TransactionTypeList = Transtypelist.Data;

            ViewBag.UsrrlAdmin = _userLoginInfo.UsrrlAdmin;

           


            FinalAccountReportModel PrReport = new FinalAccountReportModel();
            FinalAccountReportModel.FinalAccountReportview PrReportView = new FinalAccountReportModel.FinalAccountReportview();

            var AccountGroupType = PrReport.FillReportType(input: new FinalAccountReportModel.ReportTypeMode { Mode = 73 }, companyKey: _userLoginInfo.CompanyKey);
            PrReportView.ReportType = AccountGroupType.Data;

            var branch1 = Common.GetDataViaQuery<FinalAccountReportModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey);
            CommonMethod objCmnMethod = new CommonMethod();
            PrReportView.BranchList = branch1.Data;
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            return PartialView("_AddFinalAccountReport", PrReportView);
        }

       
        

        [HttpPost]
        public ActionResult GetFinalAccountReportgridViewList(FinalAccountReportModel.FinalAccountReportDto objData)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            FinalAccountReportModel objfld = new FinalAccountReportModel();

            DataTable dt = new DataTable();

            var data = objfld.GetFinalAccountReportView(input: new FinalAccountReportModel.FinalAccountReportDto
            {
               ReportName=objData.ReportName,
               AsonDate=objData.AsonDate,
               BranchID=objData.BranchID,
               FK_Company = _userLoginInfo.FK_Company,
                
            },
            companyKey: _userLoginInfo.CompanyKey);

            if (data.Data != null)
            {
                var item = "";

                decimal expdebit = 0;
                decimal expcredit = 0;
                decimal incdebit = 0;
                decimal inccredit = 0;
                var Total = new FinalAccountReportModel.Toatal();
                if (objData.ReportName == 3)
                {
                    List<FinalAccountReportModel.AccountGridview> resultAsset = data.Data.Where(a => a.Mode == "A").ToList();
                    List<FinalAccountReportModel.AccountGridview> resultLiability = data.Data.Where(a => a.Mode == "L").ToList();

                    DataTable faTable = new DataTable("FinalAccount");
                    DataColumn dtColumn;
                    DataRow faDataRow;

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "ParticularA";
                    faTable.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "AmountA1";
                    faTable.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "AmountA2";
                    faTable.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "ParticularB";
                    faTable.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "AmountB1";
                    faTable.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "AmountB2";
                    faTable.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "ColumnType1";
                    faTable.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "ColumnType2";
                    faTable.Columns.Add(dtColumn);

                    for (int i = 0; i < resultAsset.Count() || i < resultLiability.Count(); i++)
                    {
                        faDataRow = faTable.NewRow();
                        if (i < resultAsset.Count())
                        {
                            faDataRow["ParticularA"] = resultAsset[i].FaPlName;
                            if (Convert.ToDouble(resultAsset[i].Amount) > 0)
                            {
                                faDataRow["AmountA1"] = resultAsset[i].Amount;
                                faDataRow["AmountA2"] = 0;
                                //faDataRow["AmountA2"] = resultAsset[i].Amount;
                                //faDataRow["AmountA1"] = 0;
                            }
                            else
                            {
                                if (resultAsset[i].LevelID==1 &&resultAsset[i].FaPlName!= "Group Total")
                                {
                                    faDataRow["AmountA1"] = "";
                                    faDataRow["AmountA2"] ="";
                                }
                                else
                                {
                                    faDataRow["AmountA1"] = 0;
                                    faDataRow["AmountA2"] = Convert.ToDouble(resultAsset[i].Amount) * -1;
                                }

                               
                                //faDataRow["AmountA2"] = 0;
                                //faDataRow["AmountA1"] = Convert.ToDouble(resultAsset[i].Amount) * -1;
                            }
                            //faDataRow["ColumnType"] = resultAsset[i].LevelID ;
                            faDataRow["ColumnType1"] = resultAsset[i].LevelID;
                            if (resultAsset[i].FaPlName.ToUpper().Trim() == "GROUP TOTAL")
                            {
                                
                                if (Convert.ToDouble(resultAsset[i].Amount) > 0)
                                {
                                    //credit
                                    expcredit += resultAsset[i].Amount;
                                }
                                else
                                {
                                    //debit
                                    expdebit += resultAsset[i].Amount;
                                }
                            }
                        }
                        else
                        {
                            faDataRow["ParticularA"] = "";
                            faDataRow["AmountA1"] = "" ;
                            faDataRow["AmountA2"] = "";
                        }
                        if (i < resultLiability.Count())
                        {
                            faDataRow["ParticularB"] = resultLiability[i].FaPlName;
                            if (Convert.ToDouble(resultLiability[i].Amount) > 0)
                            {
                                faDataRow["AmountB1"] = resultLiability[i].Amount;
                                faDataRow["AmountB2"] = 0;
                            }
                            else
                            {
                                if(resultLiability[i].LevelID==1 &&resultLiability[i].FaPlName!= "Group Total")
                                {
                                    faDataRow["AmountB1"] = "";
                                    faDataRow["AmountB2"] = "";
                                }
                                else
                                {
                                    faDataRow["AmountB1"] = 0;
                                    faDataRow["AmountB2"] = Convert.ToDouble(resultLiability[i].Amount) * -1;
                                }
                              
                            }
                            faDataRow["ColumnType2"] = resultLiability[i].LevelID;

                            if (resultLiability[i].FaPlName.ToUpper().Trim() == "GROUP TOTAL")
                            {
                                if (Convert.ToDouble(resultLiability[i].Amount) > 0)
                                {
                                    //credit
                                    inccredit += resultLiability[i].Amount;
                                }
                                else
                                {
                                    //debit
                                    incdebit += resultLiability[i].Amount;
                                }
                            }

                        }
                        else
                        {
                            faDataRow["ParticularB"] = "";
                            faDataRow["AmountB1"] = ""; 
                            faDataRow["AmountB2"] = ""; 
                        }

                        faTable.Rows.Add(faDataRow);
                    }
                    // DataTable dt = faTable;
                    dt = faTable;

                     item = JsonConvert.SerializeObject(dt);
                    //return Json(new { data, item1 }, JsonRequestBehavior.AllowGet);
                }
                else{
                    List<FinalAccountReportModel.AccountGridview> resultExpense = data.Data.Where(a => a.Mode == "E").ToList();
                    List<FinalAccountReportModel.AccountGridview> resultIncome = data.Data.Where(a => a.Mode == "I").ToList();

                    DataTable faTable = new DataTable("FinalAccount");
                    DataColumn dtColumn;
                    DataRow faDataRow;

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "ParticularA";
                    faTable.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "AmountA1";
                    faTable.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "AmountA2";
                    faTable.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "ParticularB";
                    faTable.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "AmountB1";
                    faTable.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "AmountB2";
                    faTable.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "ColumnType1";
                    faTable.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "ColumnType2";
                    faTable.Columns.Add(dtColumn);

                    // string spaceValue = "&nbsp;&nbsp;&nbsp;";


                    for (int i = 0; i < resultExpense.Count() || i < resultIncome.Count(); i++)
                    {
                        faDataRow = faTable.NewRow();
                        if (i < resultExpense.Count())
                        {
                            //Expense
                            faDataRow["ParticularA"] = resultExpense[i].FaPlName;
                            if (Convert.ToDouble(resultExpense[i].Amount) > 0)
                            {
                                //if (resultExpense[i].LevelID == 1 && resultExpense[i].FaPlName == "Group Total")  //new line of code
                                //{
                                //    //faDataRow["AmountA1"] = resultExpense[i].Amount;
                                //    //faDataRow["AmountA2"] = 0;
                                //    faDataRow["AmountA1"] = "";
                                //    faDataRow["AmountA2"] = "";
                                //}
                                //else
                                //{
                                    faDataRow["AmountA1"] = resultExpense[i].Amount;
                                    faDataRow["AmountA2"] = 0;
                               // }
                                
                            }
                            else
                            {

                                if (resultExpense[i].LevelID == 1 && resultExpense[i].FaPlName != "Group Total")  //new line of code
                                {
                                    //faDataRow["AmountA1"] = resultExpense[i].Amount;
                                    //faDataRow["AmountA2"] = 0;
                                    faDataRow["AmountA1"] = "";
                                    faDataRow["AmountA2"] = "";
                                }
                                else
                                {
                                    faDataRow["AmountA1"] = 0;
                                    faDataRow["AmountA2"] = Convert.ToDouble(resultExpense[i].Amount) * -1;
                                }

                                
                            }
                            faDataRow["ColumnType1"] = resultExpense[i].LevelID;
                            if (resultExpense[i].FaPlName.ToUpper().Trim() == "GROUP TOTAL")
                            {
                                if (Convert.ToDouble(resultExpense[i].Amount) > 0)
                                {
                                    //credit
                                    expcredit += resultExpense[i].Amount;
                                }
                                else
                                {
                                    //debit
                                    expdebit += resultExpense[i].Amount;
                                }
                            }
                        }
                        else
                        {
                            faDataRow["ParticularA"] = "";
                            faDataRow["AmountA1"] = ""; 
                            faDataRow["AmountA2"] = ""; 
                        }
                        if (i < resultIncome.Count())
                        {
                            faDataRow["ParticularB"] = resultIncome[i].FaPlName;
                            if (Convert.ToDouble(resultIncome[i].Amount) > 0)
                            {
                                faDataRow["AmountB1"] = resultIncome[i].Amount;
                                faDataRow["AmountB2"] = 0;
                            }
                            else
                            {
                                if(resultIncome[i].LevelID==1 &&resultIncome[i].FaPlName!= "Group Total")
                                {
                                    faDataRow["AmountB1"] = "";
                                    faDataRow["AmountB2"] = "";
                                }
                                else
                                {
                                    faDataRow["AmountB1"] = 0;
                                    faDataRow["AmountB2"] = Convert.ToDouble(resultIncome[i].Amount) * -1;
                                }
                                
                            }
                            faDataRow["ColumnType2"] = resultIncome[i].LevelID;

                            if (resultIncome[i].FaPlName.ToUpper().Trim() == "GROUP TOTAL")
                            {
                                if (Convert.ToDouble(resultIncome[i].Amount) > 0)
                                {
                                    //credit
                                    inccredit += resultIncome[i].Amount;
                                }
                                else
                                {
                                    //debit
                                    incdebit += resultIncome[i].Amount;
                                }
                            }
                        }
                        else
                        {
                            faDataRow["ParticularB"] = "";
                            faDataRow["AmountB1"] = ""; 
                            faDataRow["AmountB2"] = "";
                        }

                        faTable.Rows.Add(faDataRow);
                    }
                    // DataTable dt = faTable;
                    dt = faTable;

                    item = JsonConvert.SerializeObject(dt);

                    

                    // return Json(dt, JsonRequestBehavior.AllowGet);
                }

                Total.TotalA = expcredit - expcredit;
                Total.TotalB = inccredit- incdebit;
                var tt = JsonConvert.SerializeObject(Total);
                //12
                //return Json(data, JsonRequestBehavior.AllowGet);

                return Json(new { data, item , tt }, JsonRequestBehavior.AllowGet);

            }

            return null;
        }


}
}