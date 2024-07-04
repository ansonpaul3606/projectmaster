using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PerfectWebERPAPI.Interface;

namespace PerfectWebERPAPI.DataAccess
{
    public class DalPickUpDelivery
    {
        public DataTable DalCommonValidate(IPickUpDelivery pickUpDelivery)
        {

            Int32 ReqMode = 0, SubMode = 0;
            Int16 Status = 0;
            Int64 FK_Company = 0, FK_BranchCodeUser = 0, FK_Employee=0, FK_Area=0,FK_Product=0, ID_ProductDelivery=0;          
            if (pickUpDelivery.ReqMode != "")
                ReqMode = Convert.ToInt32(pickUpDelivery.ReqMode);
            if (pickUpDelivery.SubMode != "")
                SubMode = Convert.ToInt32(pickUpDelivery.SubMode);       
            if (pickUpDelivery.FK_Company != "")
                FK_Company = Convert.ToInt32(pickUpDelivery.FK_Company);            
            if (pickUpDelivery.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt32(pickUpDelivery.FK_BranchCodeUser);
            if (pickUpDelivery.FK_Employee != "")
                FK_Employee = Convert.ToInt64(pickUpDelivery.FK_Employee);
            if (pickUpDelivery.FK_Area != "")
                FK_Area = Convert.ToInt64(pickUpDelivery.FK_Area);
            if (pickUpDelivery.FK_Product != "")
                FK_Product = Convert.ToInt64(pickUpDelivery.FK_Product);
            if (pickUpDelivery.Status != "")
                Status = Convert.ToInt16(pickUpDelivery.Status);
            if (pickUpDelivery.ID_ProductDelivery != "")
                ID_ProductDelivery = Convert.ToInt64(pickUpDelivery.ID_ProductDelivery);
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + pickUpDelivery.BankKey);
            string sqlCommand = "ProAPIProdSuitValidate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int32, ReqMode);
            db.AddInParameter(dbCommand, "@SubMode", DbType.Int32, SubMode);          
            db.AddInParameter(dbCommand, "@FK_Company", DbType.String, pickUpDelivery.FK_Company);   
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, pickUpDelivery.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@FK_Area", DbType.Int64, FK_Area);
            db.AddInParameter(dbCommand, "@FK_Product", DbType.Int64, FK_Product);
            db.AddInParameter(dbCommand, "@Name", DbType.String, pickUpDelivery.CusName);
            db.AddInParameter(dbCommand, "@MobileNumber", DbType.String, pickUpDelivery.CusMobile);
            db.AddInParameter(dbCommand, "@TicketNo", DbType.String, pickUpDelivery.TicketNo);
            db.AddInParameter(dbCommand, "@DeliveryStatus", DbType.Int16, Status);
            db.AddInParameter(dbCommand, "@ID_ProductDelivery", DbType.Int64, ID_ProductDelivery);       
            db.AddInParameter(dbCommand, "@ToDate", DbType.String, pickUpDelivery.ToDate);
            db.AddInParameter(dbCommand, "@FromDate", DbType.String, pickUpDelivery.FromDate); 
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, pickUpDelivery.TransMode);
            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalProductDeliveryFollowUpUpdate(IPickUpDelivery pickUpDelivery)
        {

            Int32 ReqMode = 0, SubMode = 0;
            Int16 Status = 0, Action=0;
            Int64 FK_Company = 0, FK_BranchCodeUser = 0, FK_Employee = 0, FK_Area = 0, FK_Product = 0, ID_ProductDelivery = 0, ID_ProductDeliveryFollowUp=0, FK_ProductDeliveryAssign=0,
                FK_Reason=0, ID_NextAction=0, FK_BillType=0;
            decimal DeliveryCharge = 0, NetAmount=0, StandByAmount=0;
            if (pickUpDelivery.ReqMode != "")
                ReqMode = Convert.ToInt32(pickUpDelivery.ReqMode);
            if (pickUpDelivery.SubMode != "")
                SubMode = Convert.ToInt32(pickUpDelivery.SubMode);
            if (pickUpDelivery.FK_Company != "")
                FK_Company = Convert.ToInt32(pickUpDelivery.FK_Company);
            if (pickUpDelivery.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt32(pickUpDelivery.FK_BranchCodeUser);
            if (pickUpDelivery.FK_Employee != "")
                FK_Employee = Convert.ToInt64(pickUpDelivery.FK_Employee);
            if (pickUpDelivery.FK_Area != "")
                FK_Area = Convert.ToInt64(pickUpDelivery.FK_Area);
            if (pickUpDelivery.FK_Product != "")
                FK_Product = Convert.ToInt64(pickUpDelivery.FK_Product);
            if (pickUpDelivery.Status != "")
                Status = Convert.ToInt16(pickUpDelivery.Status);
            if (pickUpDelivery.ID_ProductDelivery != "")
                ID_ProductDelivery = Convert.ToInt64(pickUpDelivery.ID_ProductDelivery);
            if (pickUpDelivery.ID_ProductDeliveryFollowUp != "")
                ID_ProductDeliveryFollowUp = Convert.ToInt64(pickUpDelivery.ID_ProductDeliveryFollowUp);
            if (pickUpDelivery.FK_ProductDeliveryAssign != "")
                FK_ProductDeliveryAssign = Convert.ToInt64(pickUpDelivery.FK_ProductDeliveryAssign);
            if (pickUpDelivery.DeliveryCharge != "")
                DeliveryCharge = Convert.ToDecimal(pickUpDelivery.DeliveryCharge);
            if (pickUpDelivery.NetAmount != "")
                NetAmount = Convert.ToDecimal(pickUpDelivery.NetAmount);
            if (pickUpDelivery.Action != "")
                Action = Convert.ToInt16(pickUpDelivery.Action);
            if (pickUpDelivery.FK_Reason != "")
                FK_Reason = Convert.ToInt16(pickUpDelivery.FK_Reason);
            if (pickUpDelivery.ID_NextAction != "")
                ID_NextAction = Convert.ToInt64(pickUpDelivery.ID_NextAction);
            if (pickUpDelivery.StandByAmount != "")
                StandByAmount = Convert.ToDecimal(pickUpDelivery.StandByAmount);
            if (pickUpDelivery.FK_BillType != "")
                FK_BillType = Convert.ToInt64(pickUpDelivery.FK_BillType);

           

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + pickUpDelivery.BankKey);
            string sqlCommand = "ProProductDeliveryFollowUpUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_ProductDeliveryFollowUp", DbType.Int64, ID_ProductDeliveryFollowUp);
            db.AddInParameter(dbCommand, "@FK_ProductDeliveryAssign", DbType.Int64, FK_ProductDeliveryAssign);
            db.AddInParameter(dbCommand, "@PdfDeliveryDate", DbType.DateTime, pickUpDelivery.PdfDeliveryDate);
            db.AddInParameter(dbCommand, "@PdfDeliveryTime", DbType.String, pickUpDelivery.PdfDeliveryTime);
            db.AddInParameter(dbCommand, "@PdfCustomerNotes", DbType.String, pickUpDelivery.CustomerNotes);
            db.AddInParameter(dbCommand, "@PdfEmployeeNotes", DbType.String, pickUpDelivery.EmployeeNotes);
            db.AddInParameter(dbCommand, "@PdfDeliveryCharge", DbType.Decimal, DeliveryCharge);
            db.AddInParameter(dbCommand, "@PdfNetAmount", DbType.Decimal, NetAmount);
            db.AddInParameter(dbCommand, "@PdfAction", DbType.Int64, Action);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Int16, 1);
            db.AddInParameter(dbCommand, "@Debug", DbType.Byte, 0);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, pickUpDelivery.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int64, FK_Reason);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@productReplace", DbType.Xml, pickUpDelivery.productReplace);//xml
            db.AddInParameter(dbCommand, "@CSAstatus", DbType.Byte, Status);
            db.AddInParameter(dbCommand, "@ID_NextAction", DbType.Int64, ID_NextAction);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@StandByAmount", DbType.Decimal, StandByAmount);
            db.AddInParameter(dbCommand, "@PaymentDetail", DbType.Xml, pickUpDelivery.PaymentDetail);//xml
            db.AddInParameter(dbCommand, "@FK_BillType", DbType.Int64, FK_BillType);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, "CUPD");
            db.AddInParameter(dbCommand, "@DeliveryComplaint", DbType.Xml, pickUpDelivery.DeliveryComplaints);
           
            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalProductComplaintList(IPickUpDelivery pickUpDelivery)
        {
          
            Int64 FK_Company = 0, FK_Category = 0;
            
            if (pickUpDelivery.FK_Company != "")
                FK_Company = Convert.ToInt32(pickUpDelivery.FK_Company);
            if (pickUpDelivery.FK_Category != "")
                FK_Category = Convert.ToInt32(pickUpDelivery.FK_Category);
          
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + pickUpDelivery.BankKey);
            string sqlCommand = "ProProductComplaintList";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, FK_Category);
            
            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalProGetDeliveryDetails(IPickUpDelivery pickUpDelivery)
        {

            Int64 FK_ProductDelivery = 0;
            Int16 SubMode = 0;
            if (pickUpDelivery.ID_ProductDelivery != "")
                FK_ProductDelivery = Convert.ToInt64(pickUpDelivery.ID_ProductDelivery);

            if (pickUpDelivery.SubMode != "")
                FK_ProductDelivery = Convert.ToInt16(pickUpDelivery.SubMode);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + pickUpDelivery.BankKey);
            string sqlCommand = "ProGetDeliveryDetails";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, pickUpDelivery.TransMode);
            db.AddInParameter(dbCommand, "@FK_ProductDelivery", DbType.Int64, FK_ProductDelivery);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int16, SubMode);
            db.AddInParameter(dbCommand, "@Details", DbType.Boolean, false);

            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
    }
}

