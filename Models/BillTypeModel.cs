using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace PerfectWebERP.Models
{
    public class BillTypeModel
    {

        public class BillTypeView
        {
            public long SlNo { get; set; }
            public long BillTypeID { get; set; }
            public string BillType { get; set; }
            public string ShortName { get; set; }
            public string Mode { get; set; }
            public string CashMode { get; set; }
            public string ModeName { get; set; }
            public string CashModeName { get; set; }
            public long ?AccountHeadID { get; set; }
            public string AccountHeadName { get; set; }
            public long ?AccountHeadSubID { get; set; }
            public string AccountHeadSubName { get; set; }
            public Int32 SortOrder { get; set; }
            public Int32 TotalCount { get; set; }
            public long BTBillType { get; set; }
            public long ?FK_AccountHeadDisc { get; set; }
            public long ?FK_AccountHeadSubDisc { get; set; }
            public long ?FK_AccountHeadRoundOff { get; set; }
            public long ?FK_AccountHeadSubRoundOff { get; set; }
            public string DiscountHead { get; set; }
            public string DiscountSubHead { get; set; }
            public string RoundOffHead { get; set; }
            public string RoundOffSubHead { get; set; }
            public long? FK_AccountHeadAdv { get; set; }
            public long? FK_AccountHeadSubAdv { get; set; }
            public string AdvHead { get; set; }
            public string AdvSubHead { get; set; }
            public long? FK_AccountHeadRet { get; set; }
            public long? FK_AccountHeadSubRet { get; set; }
            public string RetHead { get; set; }
            public string RetSubHead { get; set; }

            public long? FK_SecAmtHead { get; set; }
            public long? FK_SecAmtHeadSub { get; set; }
            public string SecAmtHeadName { get; set; }
            public string SecAmtSubHeadName { get; set; }

            public long? FK_SerAmtHead { get; set; }
            public long? FK_SerAmtHeadSub { get; set; }
            public string ServAmtHeadName { get; set; }
            public string ServAmtSubHeadName { get; set; }

            public long? FK_BuyBackHead { get; set; }
            public long? FK_BuyBackHeadSub { get; set; }
            public string BuyBackHeadName { get; set; }
            public string BuyBackSubHeadName { get; set; }

            public long? FundHeadID { get; set; }
            public string FundHeadName { get; set; }

        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public class AccountHeadView
        {

            public long AccountHeadID { get; set; }

            public string AHeadName { get; set; }

        }
        public class AccountSubHeadView
        {
            public long AccountHeadSubID { get; set; }
            public string ASHeadName { get; set; }

        }
        public class BillTypeInputFromViewModel
        {
            ////[Required(ErrorMessage = "No BillType Selected")]
            public long BillTypeID { get; set; }

            ////[Required(ErrorMessage = "Please Enter BillType")]
            public string BillType { get; set; }

            ////[Required(ErrorMessage = "Please Enter Short Name")]
            public string ShortName { get; set; }

            //[Range(1, long.MaxValue, ErrorMessage = "Select BillType Mode")]
            public string Mode { get; set; }
            public string CashMode { get; set; }

            public Int32 SortOrder { get; set; }

            //[Required(ErrorMessage = "Please Enter Account Head")]
            public long ?AccountHeadID { get; set; }
            //[Required(ErrorMessage = "Please Enter Account Head Sub")]
            public long ?AccountHeadSubID { get; set; }
            public long ?DiscHeadID { get; set; }
            //[Required(ErrorMessage = "Please Enter Account Head Sub")]
            public long ?DiscHeadSubID { get; set; }
            public long ?RoundHeadID { get; set; }
            //[Required(ErrorMessage = "Please Enter Account Head Sub")]
            public long ?RoundHeadSubID { get; set; }
            public long? AdvHeadID { get; set; }
            //[Required(ErrorMessage = "Please Enter Account Head Sub")]
            public long? AdvHeadSubID { get; set; }
            public long? RetHeadID { get; set; }
            //[Required(ErrorMessage = "Please Enter Account Head Sub")]
            public long? RetHeadSubID { get; set; }
            public long BTBillType { get; set; }

            public long? SecAmtHeadID { get; set; }
            public long? SecAmtHeadSubID { get; set; }

            public long? ServiceAmtHeadID { get; set; }
            public long? ServiceAmtSubHeadID { get; set; }

            public long? BuyBackAmtHeadID { get; set; }
            public long? BuyBackAmtSubHeadID { get; set; }
            public long? FundHeadID { get; set; }

            public string TransMode { get; set; }

        }
        public class BillTypeInfoView
        {
            [Required(ErrorMessage = "No BillType Selected")]
            public Int64 BillTypeID { get; set; }
            [Required(ErrorMessage = "Please Select The Reason")]
            public Int64 ReasonID { get; set; }
        }

        public static string _updateProcedureName = "proBillTypeUpdate";
        public class UpdateBillType
        {

         

            public byte UserAction { get; set; }
            public long FK_AccountHeadSub { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_BillType { get; set; }
            public long ID_BillType { get; set; }
            public string Mode { get; set; }
            public string CashMode { get; set; }
            public string BTName { get; set; }
            public string BTShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_AccountHead { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            //public bool Passed { get; set; }
            public string EntrBy { get; set; }
            //public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public long BackupId { get; set; }
            public long BTBillType { get; set; }
            public string TransMode { get; set; }
            public long FK_AccountHeadDisc { get; set; }
            public long FK_AccountHeadSubDisc { get; set; }
            public long FK_AccountHeadRoundOff { get; set; }
            public long FK_AccountHeadSubRoundOff { get; set; }
            public long FK_AccountHeadAdv { get; set; }
            public long FK_AccountHeadSubAdv { get; set; }

            public long FK_AccountHeadRet { get; set; }
            public long FK_AccountHeadSubRet { get; set; }

            public long FK_SecAmtHead { get; set; }
            public long FK_SecAmtHeadSub { get; set; }

            public long FK_ServiceAmtHead { get; set; }
            public long FK_ServiceAmtHeadSub { get; set; }

            public long FK_BuyBackHead { get; set; }
            public long FK_BuyBackHeadSub { get; set; }
            public long FK_FundHead { get; set; }

        }

        public static string _selectProcedureName = "proBillTypeSelect";
        public class GetBillType
        {
            public Int64 FK_BillType { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Company { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
        }

        public static string _deleteProcedureName = "proBillTypeDelete";
        public class DeleteBillType
        {
            public long FK_BillType { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }
        public class ModeList
        {
            public short ModeID { get; set; }
            public string ModeName { get; set; }
            public string Mode { get; set; }
        }
        public class CashModeList
        {
            public short CashModeID { get; set; }
            public string CashModeName { get; set; }
            public string CashMode { get; set; }
        }
        public class BillTypeListModel
        {

            public List<ModeList> ModeList { get; set; }
            public List<CashModeList> CashModeList { get; set; }
            public List<billtyp> Billtyps { get; set; }

            public int SortOrder { get; set; }

        }
        public class billtyp
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }


        }
        public Output UpdateBillTypeData(UpdateBillType input, string companyKey)
        {
            return Common.UpdateTableData<UpdateBillType>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteBillTypeData(DeleteBillType input, string companyKey)
        {
            return Common.UpdateTableData<DeleteBillType>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<BillTypeView> GetBillTypeData(GetBillType input, string companyKey)
        {
            return Common.GetDataViaProcedure<BillTypeView, GetBillType>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }

        public APIGetRecordsDynamic<billtyp> Gettypelst(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<billtyp, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
    }
}