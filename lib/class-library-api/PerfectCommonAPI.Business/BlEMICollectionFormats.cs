using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfectWebERPAPI.Business;
using PerfectWebERPAPI.Interface;
using System.Data;
using System;

namespace PerfectWebERPAPI.Business
{
    public class BlEMICollectionFormats
    {
        public static EMICollectionReportCount ConvertEMICollectionReportCount(DataTable  dt)
        {
            EMICollectionReportCount log = new EMICollectionReportCount();        
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["Field"]) == "ToDoList")
                        log.ToDoList = Convert.ToInt32(dt.Rows[i]["Value"]);
                    if (Convert.ToString(dt.Rows[i]["Field"]) == "OverDue")
                        log.OverDue = Convert.ToInt32(dt.Rows[i]["Value"]);
                    if (Convert.ToString(dt.Rows[i]["Field"]) == "Upcoming")
                        log.Upcoming = Convert.ToInt32(dt.Rows[i]["Value"]);
                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        
       
        public static EMICollectionReport ConvertEMICollectionReport(DataTable dt)
        {
            EMICollectionReport log = new EMICollectionReport();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.EMICollectionReportList = ConvertEMICollectionReportList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }

        public static List<EMICollectionReportList> ConvertEMICollectionReportList(DataTable dt)
        {
            List<EMICollectionReportList> lst = new List<EMICollectionReportList>();
            foreach (DataRow dr in dt.Rows)
            {
                EMICollectionReportList obj = new EMICollectionReportList();

                obj.ID_CustomerWiseEMI = Convert.ToString(dr["ID_CustomerWiseEMI"].ToString());
                obj.EMINo = Convert.ToString(dr["EMINo"].ToString());
                obj.Customer = Convert.ToString(dr["Customer"].ToString());
                obj.Mobile = Convert.ToString(dr["Mobile"].ToString());
                obj.Product = Convert.ToString(dr["Product"].ToString());
                obj.FinancePlan = Convert.ToString(dr["FinancePlan"].ToString());
                obj.LastTransaction = Convert.ToString(dr["LastTransaction"].ToString());
                obj.DueAmount = Convert.ToString(dr["DueAmount"].ToString());
                obj.FineAmount = Convert.ToString(dr["FineAmount"].ToString());
                obj.Balance = Convert.ToString(dr["Balance"].ToString());
                obj.DueDate = Convert.ToString(dr["DueDate"].ToString());
                obj.NextEMIDate = Convert.ToString(dr["NextEMIDate"].ToString());
                obj.InstNo = Convert.ToString(dr["InstNo"].ToString());
                obj.FK_Area = Convert.ToString(dr["FK_Area"].ToString());
                obj.Area = Convert.ToString(dr["Area"].ToString());
                lst.Add(obj);
            }
            return lst;
        }
      
        public static List<CustomerInformationList> ConvertCustomerInformationList(DataTable dt)
        {
            List<CustomerInformationList> lst = new List<CustomerInformationList>();
            foreach (DataRow dr in dt.Rows)
            {
                CustomerInformationList obj = new CustomerInformationList();

                obj.ID_CustomerWiseEMI = Convert.ToString(dr["ID_CustomerWiseEMI"].ToString());
                obj.ID_Customer = Convert.ToString(dr["ID_Customer"].ToString());            
                obj.CusName = Convert.ToString(dr["CusName"].ToString());
                obj.Mobile = Convert.ToString(dr["Mobile"].ToString());
                obj.Address = Convert.ToString(dr["CusAddress1"].ToString());
        
                lst.Add(obj);
            }
            return lst;
        }
        public static EMIAccountDetails ConvertEMIAccountDetails(DataSet dts)
        {
            EMIAccountDetails log = new EMIAccountDetails();
            DataTable dt = dts.Tables[0];
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.CustomerInformationList = ConvertCustomerInformationList(dts.Tables[0]);
                log.EMIAccountDetailsList = ConvertEMIAccountDetailsList(dts.Tables[1]);
            
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }

        public static List<EMIAccountDetailsList> ConvertEMIAccountDetailsList(DataTable dt)
        {
            List<EMIAccountDetailsList> lst = new List<EMIAccountDetailsList>();
            foreach (DataRow dr in dt.Rows)
            {
                EMIAccountDetailsList obj = new EMIAccountDetailsList();
                obj.FK_CustomerWiseEMI = Convert.ToString(dr["FK_CustomerWiseEMI"].ToString());
                obj.BillDate = Convert.ToString(dr["BillDate"].ToString());
                obj.EMINo = Convert.ToString(dr["EMINo"].ToString());
                obj.Product = Convert.ToString(dr["Product"].ToString());
                obj.Product = Convert.ToString(dr["Product"].ToString());
                obj.Amount = Convert.ToString(dr["Amount"].ToString());
                obj.Fine = Convert.ToString(dr["Fine"].ToString());
                obj.Balance = Convert.ToString(dr["Balance"].ToString());
                obj.FK_FinancePlanType = Convert.ToString(dr["FK_FinancePlanType"].ToString());
                obj.Balance = Convert.ToString(dr["Balance"].ToString());
                lst.Add(obj);
            }
            return lst;
        }
        public static UpdateEMICollection ConvertUpdateEMICollection(DataTable dt)
        {
            UpdateEMICollection log = new UpdateEMICollection();           
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.Message = "Update Successfully";
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static FinancePlanTypeDetails ConvertFinancePlanTypeDetails(DataTable dt)
        {
            FinancePlanTypeDetails log = new FinancePlanTypeDetails();          
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.FinancePlanTypeDetailsList = ConvertFinancePlanTypeDetailsList(dt);
               

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }

        public static List<FinancePlanTypeDetailsList> ConvertFinancePlanTypeDetailsList(DataTable dt)
        {
            List<FinancePlanTypeDetailsList> lst = new List<FinancePlanTypeDetailsList>();
            foreach (DataRow dr in dt.Rows)
            {
                FinancePlanTypeDetailsList obj = new FinancePlanTypeDetailsList();
                obj.ID_FinancePlanType = Convert.ToString(dr["ID_FinancePlanType"].ToString());
                obj.FinanceName = Convert.ToString(dr["FinanceName"].ToString());               
                lst.Add(obj);
            }
            return lst;
        }
    }
}
