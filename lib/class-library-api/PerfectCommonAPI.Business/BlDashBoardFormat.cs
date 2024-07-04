using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfectWebERPAPI.Business;
using PerfectWebERPAPI.Interface;
using System.Data;
using System;
using Newtonsoft.Json;

namespace PerfectWebERPAPI.Business
{
    public class BlDashBoardFormat
    {
        public static TileLeadDashBoardDetails ConvertTileLeadDashBoardDetails(DataTable dt, string DashMode, string DashType)
        {
            string Flag = "N";
            TileLeadDashBoardDetails log = new TileLeadDashBoardDetails();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {

                        log.ChartName = Convert.ToString(dt.Rows[i]["ChartName"]);
                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonData);
                            DataTable dataTable = new DataTable();
                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }
                            if (dataTable.Rows.Count > 0)
                            {
                                Flag = "Y";
                                log.LeadTileData = convertLeadTileData(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }

                    }

                }
                if (Flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Lead DashBoard Details";

                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Lead DashBoard Details";
            }
            return log;
        }
        public static List<LeadTileData> convertLeadTileData(DataTable dt)
        {
            List<LeadTileData> lst = new List<LeadTileData>();
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    lst.Add(new LeadTileData
                    {
                        Label = column.ColumnName,
                        Value = Convert.ToString(dr[column])
                    });
                }
            }
            return lst;
        }

        public static EmployeeWiseTaegetInPercentage ConvertEmployeeWiseTaegetInPercentage(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            EmployeeWiseTaegetInPercentage log = new EmployeeWiseTaegetInPercentage();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.EmployeeWiseTaegetDetails = convertEmployeeWiseTaegetDetails(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }


                    }

                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Employee Wise Target Details";

                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Employee Wise Target Details";
            }
            return log;
        }
        public static List<EmployeeWiseTaegetDetails> convertEmployeeWiseTaegetDetails(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new EmployeeWiseTaegetDetails()
                    {

                        EmpFName = Convert.ToString(dr["EmpFName"]),
                        ActualPercentage = Convert.ToString(dr["ActualPercentage"]),
                        ActualAmount = Convert.ToString(dr["ActualAmount"]),
                        TargetAmount = Convert.ToString(dr["TargetAmount"])

                    }).ToList();
        }
        public static LeadActivites ConvertLeadActivites(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            LeadActivites log = new LeadActivites();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.LeadActivitesList = convertLeadActivitesList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }


                    }

                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Lead Activites Details";

                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Lead Activites Details";
            }
            return log;
        }
        public static List<LeadActivitesList> convertLeadActivitesList(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new LeadActivitesList()
                    {

                        ActionName = Convert.ToString(dr["ActionName"]),
                        Closed = Convert.ToString(dr["Closed"]),
                        Total = Convert.ToString(dr["Total"])

                    }).ToList();
        }
        public static Leadstagewiseforcast ConvertLeadstagewiseforcast(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            Leadstagewiseforcast log = new Leadstagewiseforcast();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }
                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.LeadstagewiseforcastList = convertLeadstagewiseforcastList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }
                    }

                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Lead stage wise forcast Details";

                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Lead stage wise forcast Details";
            }
            return log;
        }
        public static List<LeadstagewiseforcastList> convertLeadstagewiseforcastList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new LeadstagewiseforcastList()
                    {

                        StatusName = Convert.ToString(dr["StatusName"]),
                        Amount = Convert.ToString(dr["Amount"]),
                        Percentage = Convert.ToString(dr["Percentage"])

                    }).ToList();
        }
        public static CRMTileDashBoardDetails ConvertCRMTileDashBoardDetails(DataTable dt, string DashMode, string DashType)
        {
            string Flag = "N";
            CRMTileDashBoardDetails log = new CRMTileDashBoardDetails();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        log.ChartName = Convert.ToString(dt.Rows[i]["ChartName"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonData);
                            DataTable dataTable = new DataTable();
                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }
                            if (dataTable.Rows.Count > 0)
                            {
                                Flag = "Y";
                                log.CRMTileDashBoardDetailsList = convertCRMTileDashBoardDetailsList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }



                    }

                }
                if (Flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Service Tile Details";
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Service Tile Details";
            }
            return log;
        }
        public static List<CRMTileDashBoardDetailsList> convertCRMTileDashBoardDetailsList(DataTable dt)
        {
            List<CRMTileDashBoardDetailsList> lst = new List<CRMTileDashBoardDetailsList>();
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    lst.Add(new CRMTileDashBoardDetailsList
                    {
                        Label = column.ColumnName,
                        Value = Convert.ToString(dr[column])
                    });
                }
            }
            return lst;
        }
        public static CRMStagewiseDetails ConvertCRMStagewiseDetails(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            CRMStagewiseDetails log = new CRMStagewiseDetails();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.CRMStagewiseDetailsList = convertCRMStagewiseDetailsList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }


                    }

                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Service Stage wise Details";

                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Service Stage wise Details";
            }
            return log;
        }
        public static List<CRMStagewiseDetailsList> convertCRMStagewiseDetailsList(DataTable dt)
        {


            return (from DataRow dr in dt.Rows
                    select new CRMStagewiseDetailsList()
                    {

                        StatusName = Convert.ToString(dr["StatusName"]),
                        StatusCount = Convert.ToString(dr["StatusCount"])

                    }).ToList();
        }
        public static CRMservicewise ConvertCRMservicewise(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            CRMservicewise log = new CRMservicewise();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }
                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.CRMservicewiseList = convertCRMservicewiseList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }


                    }

                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Service DashBoard Details";

                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Service DashBoard  Details";
            }
            return log;
        }
        public static List<CRMservicewiseList> convertCRMservicewiseList(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new CRMservicewiseList()
                    {

                        ServiceName = Convert.ToString(dr["ServiceName"].ToString()),
                        ServiceCount = Convert.ToString(dr["ServiceCount"].ToString())


                    }).ToList();
        }
        public static CRMcomplaintwise ConvertCRMcomplaintwise(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            CRMcomplaintwise log = new CRMcomplaintwise();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }
                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.CRMcomplaintwiseList = convertCRMcomplaintwiseList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }

                    }

                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Service Complaint wise Details";

                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Service Complaint wise Details";
            }
            return log;
        }
        public static List<CRMcomplaintwiseList> convertCRMcomplaintwiseList(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new CRMcomplaintwiseList()
                    {

                        ComplaintName = Convert.ToString(dr["ComplaintName"].ToString()),
                        ComplaintCount = Convert.ToString(dr["ComplaintCount"].ToString())


                    }).ToList();
        }
        public static ProjectTileDashBoardDetails ConvertProjectTileDashBoardDetails(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            ProjectTileDashBoardDetails log = new ProjectTileDashBoardDetails();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.ChartName = Convert.ToString(dt.Rows[i]["ChartName"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonData);
                            DataTable dataTable = new DataTable();
                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }
                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.ProjectTileDashBoardDetailsList = convertProjectTileDashBoardDetailsList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }


                    }
                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Project Tile DashBoard Details";

                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Project Tile DashBoard Details";
            }
            return log;
        }
        public static List<ProjectTileDashBoardDetailsList> convertProjectTileDashBoardDetailsList(DataTable dt)
        {
            List<ProjectTileDashBoardDetailsList> lst = new List<ProjectTileDashBoardDetailsList>();
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    lst.Add(new ProjectTileDashBoardDetailsList
                    {
                        Label = column.ColumnName,
                        Value = Convert.ToString(dr[column])
                    });
                }
            }
            return lst;
        }
        public static InventoryMonthlySaleGraph ConvertInventoryMonthlySaleGraph(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            InventoryMonthlySaleGraph log = new InventoryMonthlySaleGraph();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.InventoryMonthlySaleGraphList = convertInventoryMonthlySaleGraphList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }


                    }

                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Inventory Monthly Sale Graph Details";

                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Inventory Monthly Sale Graph Details";
            }
            return log;
        }
        public static List<InventoryMonthlySaleGraphList> convertInventoryMonthlySaleGraphList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new InventoryMonthlySaleGraphList()
                    {

                        Month = Convert.ToString(dr["Month"].ToString()),
                        Amount = Convert.ToString(dr["Amount"].ToString())


                    }).ToList();
        }
        public static InventorySupplierWisePurchase ConvertInventorySupplierWisePurchase(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            InventorySupplierWisePurchase log = new InventorySupplierWisePurchase();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.InventorySupplierWisePurchaseList = convertInventorySupplierWisePurchaseList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }


                    }

                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Inventory Supplier Wise Purchase Details";

                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Inventory Supplier Wise Purchase Details";
            }
            return log;
        }
        public static List<InventorySupplierWisePurchaseList> convertInventorySupplierWisePurchaseList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new InventorySupplierWisePurchaseList()
                    {

                        SuppName = Convert.ToString(dr["SuppName"].ToString()),
                        Amount = Convert.ToString(dr["Amount"].ToString())


                    }).ToList();

        }
        public static InventoryProductReorderLevel ConvertInventoryProductReorderLevel(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            InventoryProductReorderLevel log = new InventoryProductReorderLevel();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.InventoryProductReorderLevelList = convertInventoryProductReorderLevelList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }


                    }

                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Inventory Product Reorder Level Details";

                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Inventory Product Reorder Level Details";
            }
            return log;
        }
        public static List<InventoryProductReorderLevelList> convertInventoryProductReorderLevelList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new InventoryProductReorderLevelList()
                    {

                        ProdName = Convert.ToString(dr["ProdName"].ToString()),
                        ReorderLevel = Convert.ToString(dr["ReorderLevel"].ToString()),
                        CurrentQuantity = Convert.ToString(dr["CurrentQuantity"].ToString())


                    }).ToList();

        }
        public static InventoryTopSupplierList ConvertInventoryTopSupplierList(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            InventoryTopSupplierList log = new InventoryTopSupplierList();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.InventoryTopSupplierListDetails = convertInventoryTopSupplierListDetails(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }


                    }

                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Inventory Top Supplier List Details";

                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Inventory Top Supplier List Details";
            }
            return log;
        }
        public static List<InventoryTopSupplierListDetails> convertInventoryTopSupplierListDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new InventoryTopSupplierListDetails()
                    {

                        SuppName = Convert.ToString(dr["SuppName"].ToString()),
                        Amount = Convert.ToString(dr["Amount"].ToString())
                    }).ToList();

        }
        public static InventoryTopSellingItem ConvertInventoryTopSellingItem(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            InventoryTopSellingItem log = new InventoryTopSellingItem();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }
                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.InventoryTopSellingItemList = convertInventoryTopSellingItemList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }


                    }

                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Inventory Top Selling Item Details";

                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Inventory Top Selling Item Details";
            }
            return log;
        }
        public static List<InventoryTopSellingItemList> convertInventoryTopSellingItemList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new InventoryTopSellingItemList()
                    {

                        Product = Convert.ToString(dr["Product"].ToString()),
                        Count = Convert.ToString(dr["Count"].ToString())
                    }).ToList();

        }
        public static InventoryStockListCategory ConvertInventoryStockListCategory(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            InventoryStockListCategory log = new InventoryStockListCategory();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }
                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.InventoryStockListCategoryDetails = convertInventoryStockListCategoryDetails(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }


                    }

                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Inventory Stock List Category Details";
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Inventory Stock List Category Details";
            }
            return log;
        }
        public static List<InventoryStockListCategoryDetails> convertInventoryStockListCategoryDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new InventoryStockListCategoryDetails()
                    {

                        CatName = Convert.ToString(dr["CatName"].ToString()),
                        Count = Convert.ToString(dr["Count"].ToString())
                    }).ToList();

        }
        public static Top10ProductsinLead ConvertTop10ProductsinLead(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            Top10ProductsinLead log = new Top10ProductsinLead();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }
                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.Top10ProductsinLeadlist = convertTop10ProductsinLeadlist(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }



                    }
                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Top 10 Products in Lead Details";
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Top 10 Products in Lead Details";
            }
            return log;
        }
        public static List<Top10ProductsinLeadlist> convertTop10ProductsinLeadlist(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new Top10ProductsinLeadlist()
                    {

                        Productname = Convert.ToString(dr["Productname"].ToString()),
                        TotalCount = Convert.ToString(dr["TotalCount"].ToString())
                    }).ToList();

        }
        public static LeadSource ConvertLeadSource(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            LeadSource log = new LeadSource();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            if (jsonData != "")
                            {
                                var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                                DataTable dataTable = new DataTable();

                                if (dataList.Count > 0)
                                {
                                    foreach (var key in dataList[0].Keys)
                                    {
                                        dataTable.Columns.Add(key, typeof(object));
                                    }

                                    // Populate the DataTable with data
                                    foreach (var data in dataList)
                                    {
                                        DataRow row = dataTable.NewRow();
                                        foreach (var key in data.Keys)
                                        {
                                            row[key] = data[key];
                                        }
                                        dataTable.Rows.Add(row);
                                    }
                                }
                                if (dataTable.Rows.Count > 0)
                                {
                                    flag = "Y";
                                    log.ResponseCode = "0";
                                    log.ResponseMessage = "Transaction Verified";

                                    log.LeadSourceList = convertLeadSourceList(dataTable);
                                }
                            }
                        }

                    }

                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Lead Source Details";
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Lead Source Details";
            }
            return log;
        }
        public static List<LeadSourceList> convertLeadSourceList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new LeadSourceList()
                    {

                        LeadFrom = Convert.ToString(dr["LeadFrom"].ToString()),
                        TotalCount = Convert.ToString(dr["TotalCount"].ToString()),
                        TotalPercentage = Convert.ToString(dr["TotalPercentage"].ToString())
                    }).ToList();

        }
        public static ExpenseVSGain ConvertExpenseVSGain(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            ExpenseVSGain log = new ExpenseVSGain();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }
                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.ExpenseVSGainList = convertExpenseVSGainList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }
                    }
                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Expense VS Gain List";
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Expense VS Gain List";
            }
            return log;
        }
        public static List<ExpenseVSGainList> convertExpenseVSGainList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ExpenseVSGainList()
                    {

                        MediaName = Convert.ToString(dr["MediaName"].ToString()),
                        MediaAmount = Convert.ToString(dr["MediaAmount"].ToString()),
                        LeadAmount = Convert.ToString(dr["LeadAmount"].ToString())
                    }).ToList();

        }
        public static EmployeeWiseConversionTime ConvertEmployeeWiseConversionTime(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            EmployeeWiseConversionTime log = new EmployeeWiseConversionTime();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {


                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.EmployeeWiseConversionTimeList = convertEmployeeWiseConversionTimeList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }


                    }
                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Employee Wise Conversion Time List";
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Employee Wise Conversion Time List";
            }
            return log;
        }
        public static List<EmployeeWiseConversionTimeList> convertEmployeeWiseConversionTimeList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new EmployeeWiseConversionTimeList()
                    {

                        EmployeeName = Convert.ToString(dr["EmployeeName"].ToString()),
                        Conversion = Convert.ToString(dr["Conversion"].ToString())
                    }).ToList();

        }
        public static SalesComparison ConvertSalesComparison(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            SalesComparison log = new SalesComparison();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonData);
                            DataTable dataTable = new DataTable();
                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }
                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.SalesComparisonData = convertSalesComparisonData(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }
                    }
                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Sales Comparison Details";
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Sales Comparison Details";
            }
            return log;
        }
        public static List<SalesComparisonData> convertSalesComparisonData(DataTable dt)
        {
            List<SalesComparisonData> lst = new List<SalesComparisonData>();
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    lst.Add(new SalesComparisonData
                    {
                        Label = column.ColumnName,
                        Value = Convert.ToString(dr[column])
                    });
                }
            }
            return lst;
        }
        public static AccountTileData ConvertAccountTileData(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            AccountTileData log = new AccountTileData();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        log.ChartName = Convert.ToString(dt.Rows[i]["ChartName"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonData);
                            DataTable dataTable = new DataTable();
                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }
                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.AccountTileDataList = convertAccountTileDataList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }


                    }
                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Account Dash Board  Details";

                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Account Dash Board  Details";
            }
            return log;
        }
        public static List<AccountTileDataList> convertAccountTileDataList(DataTable dt)
        {
            List<AccountTileDataList> lst = new List<AccountTileDataList>();
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    lst.Add(new AccountTileDataList
                    {
                        Label = column.ColumnName,
                        Value = Convert.ToString(dr[column])
                    });
                }
            }
            return lst;
        }

        public static CashBalance ConvertCashBalance(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            CashBalance log = new CashBalance();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.CashBalanceList = convertCashBalanceList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }


                    }
                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Cash Balance Details";
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Cash Balance Details";
            }
            return log;
        }
        public static List<CashBalanceList> convertCashBalanceList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new CashBalanceList()
                    {

                        BranchName = Convert.ToString(dr["BranchName"].ToString()),
                        CashBalance = Convert.ToString(dr["CashBalance"].ToString())
                    }).ToList();

        }
        public static BankBalance ConvertBankBalance(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            BankBalance log = new BankBalance();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {
                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.BankBalanceList = convertBankBalanceList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }

                    }
                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Bank Balance Details";
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Bank Balance Details";
            }
            return log;
        }
        public static List<BankBalanceList> convertBankBalanceList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new BankBalanceList()
                    {

                        BankAccount = Convert.ToString(dr["BankAccount"].ToString()),
                        Balance = Convert.ToString(dr["Balance"].ToString())
                    }).ToList();

        }
        public static ExpenseChart ConvertExpenseChart(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            ExpenseChart log = new ExpenseChart();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }
                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.ExpenseChartList = convertExpenseChartList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }

                    }
                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Expense Details";
                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Expense Details";
            }
            return log;
        }
        public static List<ExpenseChartList> convertExpenseChartList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ExpenseChartList()
                    {

                        Branch = Convert.ToString(dr["Branch"].ToString()),
                        ExpenseAmount = Convert.ToString(dr["ExpenseAmount"].ToString())
                    }).ToList();

        }
        public static SupplierOutstanding ConvertSupplierOutstanding(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            SupplierOutstanding log = new SupplierOutstanding();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }
                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.SupplierOutstandingList = convertSupplierOutstandingList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }

                    }
                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Supplier Outstanding Details";
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Supplier Outstanding Details";
            }
            return log;
        }
        public static List<SupplierOutstandingList> convertSupplierOutstandingList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new SupplierOutstandingList()
                    {

                        Supplier = Convert.ToString(dr["Supplier"].ToString()),
                        Amount = Convert.ToString(dr["Amount"].ToString())
                    }).ToList();

        }
        public static ProjectDelayedStatus ConvertProjectDelayedStatus(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            ProjectDelayedStatus log = new ProjectDelayedStatus();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.ProjectDelayedStatusList = convertProjectDelayedStatusList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";
                            }
                        }

                    }
                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Project Delayed Status Details";
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Project Delayed Status Details";
            }
            return log;
        }
        public static List<ProjectDelayedStatusList> convertProjectDelayedStatusList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ProjectDelayedStatusList()
                    {

                        Project = Convert.ToString(dr["Project"].ToString()),
                        ActualPeriod = Convert.ToString(dr["ActualPeriod"].ToString()),
                        CurrentPeriod = Convert.ToString(dr["CurrentPeriod"].ToString())
                    }).ToList();

        }
        public static StockValueData ConvertStockValueData(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            StockValueData log = new StockValueData();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        //  log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        //log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        log.ChartName = Convert.ToString(dt.Rows[i]["ChartName"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.StockValue = Convert.ToString(dataTable.Rows[0].Field<double>("StockValue"));
                            }
                        }
                    }
                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the stock value Details";
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the stock value Details";
            }
            return log;
        }
        public static Leadstagecountwisefrorecast ConvertLeadstagecountwisefrorecast(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            Leadstagecountwisefrorecast log = new Leadstagecountwisefrorecast();
            if (dt.Rows.Count > 0)
            {
                //log.ResponseCode = "0";
                //log.ResponseMessage = "Transaction Verified";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.LeadstagecountwisefrorecastData = convertLeadstagecountwisefrorecastData(dataTable);
                            }
                        }

                    }
                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Lead stage count wise frorecast Details";
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Lead stage count wise frorecast Details";
            }
            return log;
        }
        public static List<LeadstagecountwisefrorecastData> convertLeadstagecountwisefrorecastData(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new LeadstagecountwisefrorecastData()
                    {

                        StageName = Convert.ToString(dr["StageName"].ToString()),
                        TotalCount = Convert.ToString(dr["TotalCount"].ToString())
                    }).ToList();

        }
        public static DashBoardNameDetails ConvertDashBoardNameDetails(DataTable dt, string FK_Module)
        {
            string flag = "N";
            DashBoardNameDetails log = new DashBoardNameDetails();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["MenuList"]) == FK_Module && Convert.ToString(dt.Rows[i]["ChartType"]) == "2")
                    {
                        //if (Convert.ToString(dt.Rows[0].Field<Int32>("MenuList")) == FK_Module && Convert.ToString(dt.Rows[0].Field<Int32>("ChartType")) == "2")
                        //{
                        log.ResponseCode = "0";
                        log.ResponseMessage = "Transaction Verified";
                        flag = "Y";

                    }

                }
                flag = "Y";
                log.DashBoardNameDetailsList = convertDashBoardNameDetailsList(dt, FK_Module);


                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the DashBoard Name Details";
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                }

            }

            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the DashBoard Name Details";
            }
            return log;
        }
        public static List<DashBoardNameDetailsList> convertDashBoardNameDetailsList(DataTable dt, string FK_Module)
        {

            List<DashBoardNameDetailsList> objList = new List<DashBoardNameDetailsList>();
            foreach (DataRow dr in dt.Rows)
            {
                DashBoardNameDetailsList obj = new DashBoardNameDetailsList();
                if (Convert.ToString(dr["MenuList"].ToString()) == FK_Module && Convert.ToString(dr["ChartType"].ToString()) == "2")
                {
                    obj.ModuleId = Convert.ToString(dr["MenuList"].ToString());
                    obj.DashBoardName = Convert.ToString(dr["ChartName"].ToString());
                    obj.DashMode = Convert.ToString(dr["ChartList"].ToString());
                    obj.DashType = Convert.ToString(dr["ChartType"].ToString());
                    objList.Add(obj);
                }

            }
            return objList;
        }
        public static ProjectExpenseAnalysis ConvertProjectExpenseAnalysis(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            ProjectExpenseAnalysis log = new ProjectExpenseAnalysis();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }
                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.ProjectExpenseAnalysisList = convertProjectExpenseAnalysisList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }

                    }

                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Records in the Project Expense Analysis Details";
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Project Expense Analysis Details";
            }
            return log;
        }
        public static List<ProjectExpenseAnalysisList> convertProjectExpenseAnalysisList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ProjectExpenseAnalysisList()
                    {

                        Project = Convert.ToString(dr["Project"].ToString()),
                        ProjectAmount = Convert.ToString(dr["ProjectAmount"].ToString()),
                        Expense = Convert.ToString(dr["Expense"].ToString())
                    }).ToList();

        }
        public static CostofMaterialUsageAllocatedandUsed ConvertCostofMaterialUsageAllocatedandUsed(DataTable dt, string DashMode, string DashType)
        {
            string flag = "N";
            CostofMaterialUsageAllocatedandUsed log = new CostofMaterialUsageAllocatedandUsed();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }
                            if (dataTable.Rows.Count > 0)
                            {
                                flag = "Y";
                                log.PCostofMaterialUsageAllocatedandUsedList = convertPCostofMaterialUsageAllocatedandUsedList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }

                    }

                }
                if (flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Data Found";
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }

        public static List<PCostofMaterialUsageAllocatedandUsedList> convertPCostofMaterialUsageAllocatedandUsedList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new PCostofMaterialUsageAllocatedandUsedList()
                    {

                        Project = Convert.ToString(dr["Project"].ToString()),
                        Allocated = Convert.ToString(dr["Allocated"].ToString()),
                        Usage = Convert.ToString(dr["Usage"].ToString())
                    }).ToList();

        }
        public static UpcomingStageDueDates ConvertUpcomingStageDueDates(DataTable dt, string DashMode, string DashType)
        {
            string Flag = "N";
            UpcomingStageDueDates log = new UpcomingStageDueDates();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }
                            if (dataTable.Rows.Count > 0)
                            {
                                Flag = "Y";
                                log.PUpcomingStageDueDatesList = convertPUpcomingStageDueDatesList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }



                    }

                }
                if (Flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Data Found";

                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<PUpcomingStageDueDatesList> convertPUpcomingStageDueDatesList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new PUpcomingStageDueDatesList()
                    {

                        Project = Convert.ToString(dr["Project"].ToString()),
                        Stages = Convert.ToString(dr["Stages"].ToString()),
                        DueDate = Convert.ToString(dr["DueDate"].ToString())
                    }).ToList();

        }
        public static TotalStagewiseDue ConvertTotalStagewiseDue(DataTable dt, string DashMode, string DashType)
        {
            string Flag = "N";

            TotalStagewiseDue log = new TotalStagewiseDue();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }
                            if (dataTable.Rows.Count > 0)
                            {

                                Flag = "Y";
                                log.TotalStagewiseDueList = convertTotalStagewiseDueList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }
                    }

                }
                if (Flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Data Found";

                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<TotalStagewiseDueList> convertTotalStagewiseDueList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new TotalStagewiseDueList()
                    {

                        Stages = Convert.ToString(dr["Stages"].ToString()),
                        TotalCount = Convert.ToString(dr["TotalCount"].ToString()),
                        TotalPercentage = Convert.ToString(dr["TotalPercentage"].ToString())
                    }).ToList();

        }
        public static Top10Projects ConvertTop10Projects(DataTable dt, string DashMode, string DashType)
        {
            string Flag = "N";
            Top10Projects log = new Top10Projects();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }
                            if (dataTable.Rows.Count > 0)
                            {
                                Flag = "Y";
                                log.Top10ProjectsList = convertTop10ProjectsList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }

                    }

                }
                if (Flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Data Found";

                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                }


            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<Top10ProjectsList> convertTop10ProjectsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new Top10ProjectsList()
                    {

                        Project = Convert.ToString(dr["Project"].ToString()),
                        Amount = Convert.ToString(dr["Amount"].ToString())

                    }).ToList();

        }
        public static CRMCountofWarrantyPaidandAMC ConvertCRMCountofWarrantyPaidandAMC(DataTable dt, string DashMode, string DashType)
        {
            string Flag = "N";
            CRMCountofWarrantyPaidandAMC log = new CRMCountofWarrantyPaidandAMC();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                Flag = "Y";
                                log.CRMCountofWarrantyPaidandAMCList = convertCRMCountofWarrantyPaidandAMCList(dataTable);


                            }
                        }
                        //else
                        //{
                        //    log.ResponseCode = "-2";
                        //    log.ResponseMessage = "No Data Found";
                        //}

                    }

                }
                if (Flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Data Found";

                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<CRMCountofWarrantyPaidandAMCList> convertCRMCountofWarrantyPaidandAMCList(DataTable dt)
        {


            return (from DataRow dr in dt.Rows
                    select new CRMCountofWarrantyPaidandAMCList()
                    {

                        ItemName = Convert.ToString(dr["ItemName"]),
                        TotalCount = Convert.ToString(dr["TotalCount"])

                    }).ToList();
        }
        public static CRMTop10Products ConvertCRMTop10Products(DataTable dt, string DashMode, string DashType)
        {
            string Flag = "N";
            CRMTop10Products log = new CRMTop10Products();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                Flag = "Y";
                                log.CRMTop10ProductsList = convertCRMTop10ProductsList(dataTable);


                            }
                        }

                    }

                }
                if (Flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Data Found";

                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<CRMTop10ProductsList> convertCRMTop10ProductsList(DataTable dt)
        {


            return (from DataRow dr in dt.Rows
                    select new CRMTop10ProductsList()
                    {

                        ProductName = Convert.ToString(dr["ProductName"]),
                        Count = Convert.ToString(dr["Count"])

                    }).ToList();
        }
        public static CRMChannelWise ConvertCRMChannelWise(DataTable dt, string DashMode, string DashType)
        {
            string Flag = "N";
            CRMChannelWise log = new CRMChannelWise();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                Flag = "Y";
                                log.CRMChannelWiseList = convertCRMChannelWiseList(dataTable);


                            }
                        }

                    }

                }
                if (Flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Data Found";

                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<CRMChannelWiseList> convertCRMChannelWiseList(DataTable dt)
        {


            return (from DataRow dr in dt.Rows
                    select new CRMChannelWiseList()
                    {
                        ChannelName = Convert.ToString(dr["ChannelName"]),
                        ChannelCount = Convert.ToString(dr["ChannelCount"])

                    }).ToList();
        }
        public static CRMSLAViolationStatus ConvertCRMSLAViolationStatus(DataTable dt, string DashMode, string DashType)
        {
            string Flag = "N";
            CRMSLAViolationStatus log = new CRMSLAViolationStatus();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                Flag = "Y";
                                log.CRMSLAViolationStatusList = ConvertCRMSLAViolationStatusList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }

                    }

                }
                if (Flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Data Found";

                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<CRMSLAViolationStatusList> ConvertCRMSLAViolationStatusList(DataTable dt)
        {


            return (from DataRow dr in dt.Rows
                    select new CRMSLAViolationStatusList()
                    {
                        StatusName = Convert.ToString(dr["StatusName"]),
                        Count = Convert.ToString(dr["Count"])

                    }).ToList();
        }
        public static ProductionTileDashBoard ConvertProductionTileDashBoard(DataTable dt, string DashMode, string DashType)
        {
            string Flag = "N";
            ProductionTileDashBoard log = new ProductionTileDashBoard();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {

                        log.ChartName = Convert.ToString(dt.Rows[i]["ChartName"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonData);
                            DataTable dataTable = new DataTable();
                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }
                            if (dataTable.Rows.Count > 0)
                            {
                                Flag = "Y";
                                log.ProductionTileDashBoardList = convertProductionTileDashBoardList(dataTable);
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }

                    }

                }
                if (Flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Data Found";

                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ProductionTileDashBoardList> convertProductionTileDashBoardList(DataTable dt)
        {
            List<ProductionTileDashBoardList> lst = new List<ProductionTileDashBoardList>();
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    lst.Add(new ProductionTileDashBoardList
                    {
                        Label = column.ColumnName,
                        Value = Convert.ToString(dr[column])
                    });
                }
            }
            return lst;
        }
        public static ProductionUpcomingStock ConvertProductionUpcomingStock(DataTable dt, string DashMode, string DashType)
        {
            string Flag = "N";
            ProductionUpcomingStock log = new ProductionUpcomingStock();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                Flag = "Y";
                                log.ProductionUpcomingStockList = ConvertProductionUpcomingStockList(dataTable);


                            }
                        }

                    }

                }
                if (Flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Data Found";

                }
                else
                {

                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ProductionUpcomingStockList> ConvertProductionUpcomingStockList(DataTable dt)
        {


            return (from DataRow dr in dt.Rows
                    select new ProductionUpcomingStockList()
                    {
                        Product = Convert.ToString(dr["Product"]),
                        Quantity = Convert.ToString(dr["Quantity"])

                    }).ToList();
        }

        public static ProductionMaterialShortage ConvertProductionMaterialShortage(DataTable dt, string DashMode, string DashType)
        {
            string Flag = "N";
            ProductionMaterialShortage log = new ProductionMaterialShortage();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                Flag = "Y";
                                log.ProductionMaterialShortageList = ConvertProductionMaterialShortageList(dataTable);


                            }
                        }

                    }

                }
                if (Flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Data Found";

                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ProductionMaterialShortageList> ConvertProductionMaterialShortageList(DataTable dt)
        {


            return (from DataRow dr in dt.Rows
                    select new ProductionMaterialShortageList()
                    {
                        Product = Convert.ToString(dr["Product"]),
                        ActualQuantity = Convert.ToString(dr["ActualQuantity"]),
                        ShortageQuantity = Convert.ToString(dr["ShortageQuantity"])

                    }).ToList();
        }
        public static ProductionCompletedProducts ConvertProductionCompletedProducts(DataTable dt, string DashMode, string DashType)
        {
            string Flag = "N";
            ProductionCompletedProducts log = new ProductionCompletedProducts();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                Flag = "Y";
                                log.ProductionCompletedProductsList = ConvertProductionCompletedProductsList(dataTable);


                            }
                        }

                    }

                }
                if (Flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Data Found";

                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ProductionCompletedProductsList> ConvertProductionCompletedProductsList(DataTable dt)
        {


            return (from DataRow dr in dt.Rows
                    select new ProductionCompletedProductsList()
                    {
                        Product = Convert.ToString(dr["Product"]),
                        Quantity = Convert.ToString(dr["Quantity"])

                    }).ToList();
        }
        public static VehicleDetails ConvertVehicleDetails(DataTable dt, string DashMode, string DashType)
        {
            string Flag = "N";
            VehicleDetails log = new VehicleDetails();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {


                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonData);
                            DataTable dataTable = new DataTable();

                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }

                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];
                                    }
                                    dataTable.Rows.Add(row);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                Flag = "Y";
                                log.VehicleDetailsList = ConvertVehicleDetailsList(dataTable);


                            }
                        }

                    }

                }
                if (Flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Data Found";

                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<VehicleDetailsList> ConvertVehicleDetailsList(DataTable dt)
        {


            return (from DataRow dr in dt.Rows
                    select new VehicleDetailsList()
                    {
                        Vehicle = Convert.ToString(dr["Vehicle"]),
                        Pickup = Convert.ToString(dr["Pickup"]),
                        Delivery = Convert.ToString(dr["Delivery"]),

                    }).ToList();
        }
        public static OrderTrackingTileDashBoard ConvertOrderTrackingTileDashBoard(DataTable dt, string DashMode, string DashType)
        {
            string Flag = "N";
            OrderTrackingTileDashBoard log = new OrderTrackingTileDashBoard();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["ChartList"]) == DashMode && Convert.ToString(dt.Rows[i]["ChartType"]) == DashType)
                    {

                        log.ChartName = Convert.ToString(dt.Rows[i]["ChartName"]);
                        log.XAxis = Convert.ToString(dt.Rows[i]["XAxis"]);
                        log.YAxis = Convert.ToString(dt.Rows[i]["YAxis"]);
                        log.Reamrk = Convert.ToString(dt.Rows[i]["Remarks"]);
                        long ChartType = Convert.ToInt64(dt.Rows[i]["ChartType"]);
                        string jsonData = Convert.ToString(dt.Rows[i]["Datafile"]);
                        if (jsonData != "")
                        {
                            var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonData);
                            DataTable dataTable = new DataTable();
                            List<PickUpTileData> DataListNew = new List<PickUpTileData>();
                            if (dataList.Count > 0)
                            {
                                foreach (var key in dataList[0].Keys)
                                {
                                    dataTable.Columns.Add(key, typeof(object));
                                }
                               
                                // Populate the DataTable with data
                                foreach (var data in dataList)
                                {
                                    DataRow row = dataTable.NewRow();
                                    foreach (var key in data.Keys)
                                    {
                                        row[key] = data[key];

                                        DataListNew.Add(new PickUpTileData { Label = key, Value = data[key] });
                                    }
                                    dataTable.Rows.Add(row);
                                }


                            }
                            if (dataTable.Rows.Count > 0)
                            {
                                Flag = "Y";
                                log.OrderTrackingTileDashBoardList = ConvertOrderTrackingTileDashBoardList(dataTable);
                                log.PickUpTileData = ConvertOrderTrackingTileDashBoardList2(dataTable);
                              
                               
                                log.ResponseCode = "0";
                                log.ResponseMessage = "Transaction Verified";

                            }
                        }

                    }

                }
                if (Flag == "N")
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Data Found";

                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }


        public static List<OrderTrackingTileDashBoardList> ConvertOrderTrackingTileDashBoardList(DataTable dt)
        {


            return (from DataRow dr in dt.Rows
                    select new OrderTrackingTileDashBoardList()
                    {
                        Pending = Convert.ToString(dr["Pending"]),
                        Completed = Convert.ToString(dr["Completed"]),
                        Processing = Convert.ToString(dr["Processing"]),

                    }).ToList();
        }
        public static List<PickUpTileData> ConvertOrderTrackingTileDashBoardList2(DataTable dt)
        {

            List<PickUpTileData> dataOut = new List<PickUpTileData>();

           var test=(from DataRow dr in dt.Rows
                    select new OrderTrackingTileDashBoardList()
                    {
                        Pending = Convert.ToString(dr["Pending"]),
                        Completed = Convert.ToString(dr["Completed"]),
                        Processing = Convert.ToString(dr["Processing"]),

                    }).ToList();
            dataOut.Add(new PickUpTileData { Label = "Pending", Value = test.FirstOrDefault().Pending });
            dataOut.Add(new PickUpTileData { Label = "Completed", Value = test.FirstOrDefault().Completed });
            dataOut.Add(new PickUpTileData { Label = "Processing", Value = test.FirstOrDefault().Processing });

            return dataOut;

        }


    }
}
