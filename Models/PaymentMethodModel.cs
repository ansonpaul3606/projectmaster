using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class PaymentMethodModel
    {
        public class PaymentMethodView
        {
            public long SlNo { get; set; }/*PaymentMethod*/
            public long PaymentmethodID { get; set; }
            public long PaymentModeId { get; set; }
            public string Name { get; set; }
            public string ShortName { get; set; }
            public string ActStatus { get; set; }
            public short PMMode { get; set; }
            public int AccountHead { get; set; }
            public int AccountHeadSub { get; set; }
            public string AHeadName { get; set; }
            public int AccountCode { get; set; }
            public int AccountSHCode { get; set; }
            public string ASHeadName { get; set; }
            public Int32 SortOrder { get; set; }
            public Int64 BranchID { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public Int64 TotalCount { get; set; }
            public string TransMode { get; set; }
            public bool PMDefault { get; set; }
            public string PMDefaultActive { get; set; }
          
        }
        public class PaymentMethodViewList
        {
            //public List<PaymentModeList> PaymentModeList { get; set; }
            public List<ActionStatus> ActionStatusList { get; set; }
            public Int32 SortOrder { get; set; }
            public List<Branchs> BranchList { get; set; }

        }
        public class PaymentModeList
        {
            public long PaymentModeId { get; set; }
            public string PaymentMode { get; set; }
        }

        public class NextSortOrder
        {
            public string TableName { get; set; }
            public string FieldName { get; set; }
            public byte Debug { get; set; }
        }
        public class NextSortOrderOutput
        {
            public int NextNo { get; set; }
        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public class ActionStatus
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }

        //public class PaymentMethodInputFromViewModel
        //{
            
        //    public long PaymentModeId { get; set; }
        //    public long PaymentmethodID { get; set; }
        //    public string Name { get; set; }
        //    public string ShortName { get; set; }
        //    public short ActStatus { get; set; }
        //    public Int32 SortOrder { get; set; }
        //    //public long AccountHead { get; set; }
        //    //public long AccountHeadSub { get; set; }
        //    public int AccountHead { get; set; }
        //    public int AccountHeadSub { get; set; }
        //    public string AHeadName { get; set; }
        //    public int AccountCode { get; set; }
        //    public int AccountSHCode { get; set; }
        //    public string ASHeadName { get; set; }

        //}
        //public class PaymentMethodInputFromViewModel
        //{

        //    public long PaymentModeId { get; set; }
        //    public long PaymentmethodID { get; set; }
        //    public string Name { get; set; }
        //    public string ShortName { get; set; }
        //    public short ActStatus { get; set; }
        //    public int AccountHead { get; set; }
        //    public int AccountHeadSub { get; set; }
        //    public string AHeadName { get; set; }
        //    public int AccountCode { get; set; }
        //    public int AccountSHCode { get; set; }
        //    public string ASHeadName { get; set; }
        //    public Int32 SortOrder { get; set; }

        //}

        public static string _updateProcedureName = "ProPaymentMethodUpdate";
        public class UpdatePaymentMethod
        {
            public long FK_PaymentMethod { get; set; }
            public long ID_PaymentMethod { get; set; }
            public short PMMode { get; set; }
            public string PMName { get; set; }
            public string PMShortName { get; set; }
            public Int64 FK_AccountHead { get; set; }
            public long FK_AccountHeadSub { get; set; }
            public Int32 SortOrder { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public bool PMDefault { get; set; }
            public long FK_Branch { get; set; }

        }

        public static string _selectProcedureName = "ProPaymentMethodSelect";
        public class GetPaymentMethod
        {
            public Int64 FK_PaymentMethod { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string Name { get; set; }
        }
        public static string _deleteProcedureName = "ProPaymentMethodDelete";

        public class DeletePaymentMethod
        {
            public long FK_PaymentMethod { get; set; }
            //public long PaymentmethodID { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }

        }
        public class PaymentMethodInfoView
        {
            public long FK_PaymentMethod { get; set; }
            public long PaymentmethodID { get; set; }
            public long ReasonID { get; set; }
        }

        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }

        }

        public APIGetRecordsDynamic<ActionStatus> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public Output UpdatePaymentMethodData(UpdatePaymentMethod input, string companyKey)
        {
            return Common.UpdateTableData<UpdatePaymentMethod>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

        public APIGetRecordsDynamic<PaymentMethodView> GetPaymentMethodData(GetPaymentMethod input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentMethodView, GetPaymentMethod>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public Output DeletePaymentMethodData(DeletePaymentMethod input, string companyKey)
        {
            return Common.UpdateTableData<DeletePaymentMethod>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
    }
}