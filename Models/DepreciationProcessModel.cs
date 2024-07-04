using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class DepreciationProcessModel
    {

        public class DepreciationProcessNew
        {
            public long SlNo { get; set; }

            public long ID_DepreciationProcessDetails { get; set; }
            public DateTime DepreProcessDate { get; set; }
            public DateTime DepreFromDate { get; set; }
            public DateTime DepreToDate { get; set; }
            public long FK_Category { get; set; }
            public string Mode { get; set; }
            public long FK_Product { get; set; }
            public int FK_Reason { get; set; }
            public long TotalCount { get; set; }
            public string TransMode { get; set; }
            public long FK_Branch { get; set; }
            public long ItemQty { get; set; }
            public long DeprePeriod { get; set; }
            public decimal DepreAmt { get; set; }
            public decimal DepreMaxAmt { get; set; }
            public long FK_ItemStock { get; set; }
            public long FK_DepreciationSettings { get; set; }
            public int ReasonID { get; set; }
            public long ID_DepreciationProcess { get; set; }

            public List<DepreciationProcessDetails> DepreciationProcessDetails { get; set; }
        }
        public class DepreciationProcessNewdelete
        {
            public long SlNo { get; set; }

            public long ID_DepreciationProcessDetails { get; set; }
            public DateTime ?DepreProcessDate { get; set; }
            public DateTime? DepreFromDate { get; set; }
            public DateTime ?DepreToDate { get; set; }
            public long ?FK_Category { get; set; }
            public string Mode { get; set; }
            public long ?FK_Product { get; set; }
            public int FK_Reason { get; set; }
          
            public string TransMode { get; set; }
            public long FK_Branch { get; set; }
            public long ?ItemQty { get; set; }
            public long ?DeprePeriod { get; set; }
            public decimal ?DepreAmt { get; set; }
            public decimal ?DepreMaxAmt { get; set; }
            public long ?FK_ItemStock { get; set; }
            public long ?FK_DepreciationSettings { get; set; }
            public int ReasonID { get; set; }
            public long ID_DepreciationProcess { get; set; }

            public List<DepreciationProcessDetails> DepreciationProcessDetails { get; set; }
        }
        public class DepreciationProcessUpdateData
        {
            public int UserAction { get; set; }
             public int Debug { get; set; }
            public long ID_DepreciationProcess { get; set; }
             public long FK_ItemCategory  { get; set; }
              public long FK_ItemMaster { get; set; }
              public long FK_ItemStock { get; set; }
              public string Mode  { get; set; }
             public DateTime DepreProcessDate { get; set; }
              public DateTime DepreFromDate  { get; set; }
              public DateTime DepreToDate  { get; set; }
              public long FK_DepreciationSettings { get; set; }
              public long ItemQty  { get; set; }
              public long DeprePeriod  { get; set; }
              public decimal DepreAmt  { get; set; }
              public decimal DepreMaxAmt  { get; set; }
              public long FK_Branch  { get; set; }
              public long FK_Company  { get; set; }
              public long FK_BranchCodeUser  { get; set; }
              public string EntrBy  { get; set; }
            public long FK_Machine { get; set; }
              public string XMLDepreciationProcessDetail { get; set; }
              public int FK_Reason   { get; set; }
              public long FK_DepreciationProcess  { get; set; }
           
        }

  
        public class DepreciationProcessView
        {
            public long SlNo { get; set; }
            public long ID_DepreciationProcess { get; set; }

            public DateTime ProcessDate { get; set; }
            public DateTime Fromdate { get; set; }
            public DateTime Todate { get; set; }
            public List<ModeType> ModeList { get; set; }

            public List<CategoryList> CategoryList { get; set; }
           
            public List<DepreciationProcessDetails> DepreciationProcessDetails { get; set; }
            public string TransMode { get; set; }

            public string Mode { get; set; }
            public string ModeType { get; set; }

            public long FK_Branch { get; set; }

            public string Product { get; set; }
            public long FK_Product { get; set; }
            public Int16 FK_Category { get; set; }
            public long ReasonID { get; set; }

            public Int64 TotalCount { get; set; }

            public DateTime DepreFromDate { get; set; }
            public DateTime DepreToDate { get; set; }
            public DateTime DepreProcessDate { get; set; }
            public long FK_ItemCategory { get; set; }
            public long FK_ItemMaster { get; set; }
            public long FK_ItemStock { get; set; }
            public long FK_DepreciationSettings { get; set; }
            public long ItemQty { get; set; }
            public long DeprePeriod { get; set; }
            public decimal DepreAmt { get; set; }
            public decimal DepreMaxAmt { get; set; }

            public int Delete { get; set; }

            public long Debug { get; set; }
            public string DepreciationProcessDetail { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public byte UserAction { get; internal set; }
            public long FK_Machine { get; set; }

        }
        public class DepreciationProcessModelView
        {
            public long SlNo { get; set; }
            public DateTime DepreProcessDate { get; set; }
            public DateTime Fromdate { get; set; }
            public DateTime Todate { get; set; }
            
            public string Product { get; set; }
            public string ModeName { get; set; }
            public string CatName { get; set; }

            public long TotalCount { get; set; }
            public long ID_DepreciationProcess { get; set; }

        }
        public class DepreciationProcessDetails
        {
            public long FK_ItemCategory { get; set; }
            public long FK_ItemMaster { get; set; }
            public long FK_ItemStock { get; set; }
          

            public string CategoryName { get; set; }
            public string ItemName { get; set; }
            public long DeprePeriod { get; set; }

            public string ModeName { get; set; }
            public int ItemQty { get; set; }
            public decimal DepreAmt { get; set; }
            public decimal DepreMaxAmt { get; set; }
            public long FK_DepreciationSettings { get; set; }

        }
        public class CategoryList
        {
            public Int32 FK_Category { get; set; }
            public string Category { get; set; }

        }

        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }


        public class ModeType
        {
            public string ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class Modes
        {
            public Int32 Mode { get; set; }
        }
       
        public class DepreciationProcessID
        {
            public long FK_DepreciationProcess { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public int Detail { get; set; }        
            public Int32 PageIndex { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public string TransMode { get; set; }
 
        }
      
        public class GetDepreciationData
        {
            public DateTime ProcessDate { get; set; }
            public string Mode { get; set; }
            public long FK_Category { get; set; }
            public long FK_Product { get; set; }

            public DateTime Fromdate { get; set; }
            public DateTime Todate { get; set; }
            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Machine { get; set; }
            public string EntrBy { get; set; }
            public int Detail { get; set; }


        }


        public class DepreciationProcessDelete
        {

            public int UserAction { get; set; }
            public int ?Debug { get; set; }
            public long ID_DepreciationProcess { get; set; }
            public long ?FK_ItemCategory { get; set; }
            public long? FK_ItemMaster { get; set; }
            public long ?FK_ItemStock { get; set; }
            public string Mode { get; set; }
            public DateTime ?DepreProcessDate { get; set; }
            public DateTime ?DepreFromDate { get; set; }
            public DateTime? DepreToDate { get; set; }
            public long ?FK_DepreciationSettings { get; set; }
            public long ?ItemQty { get; set; }
            public long ?DeprePeriod { get; set; }
            public decimal ?DepreAmt { get; set; }
            public decimal ?DepreMaxAmt { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string XMLDepreciationProcessDetail { get; set; }
            public int FK_Reason { get; set; }
            public long FK_DepreciationProcess { get; set; }
            public int ?Delete { get; set; }


        }
        public class GetDepreciationdetails
        {
            public DateTime ProcessDate { get; set; }
            public string Mode { get; set; }
            public long FK_Category { get; set; }
            public long FK_Product { get; set; }

            public DateTime Fromdate { get; set; }
            public DateTime Todate { get; set; }
            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Machine { get; set; }
            public string EntrBy { get; set; }
            public int Detail { get; set; }
        }
 
        public class XMLDepreciationProcessDetail
        {
            public long? ID_DepreciationProcessDetails { get; set; }

            public long FK_ItemCategory { get; set; }
            public long FK_ItemMaster { get; set; }
            public long FK_ItemStock { get; set; }
          
            public long FK_DepreciationSettings { get; set; }
          

            public DateTime ProcessDate { get; set; }
        
            public long ItemQty { get; set; }
            public long DeprePeriod { get; set; }
            public decimal DepreAmt { get; set; }
            public decimal DepreMaxAmt { get; set; }
          

        }

     
        public class Depreciationgridout
        {
            public long FK_ItemCategory { get; set; }
            public string CategoryName { get; set; }
            public long FK_ItemMaster { get; set; }
            public string ItemName { get; set; }

            public long FK_ItemStock { get; set; }
            public string Mode { get; set; }
            public string ModeName { get; set; }
            public long FK_DepreciationSettings { get; set; }
            public string ItemQty { get; set; } 
            public long DeprePeriod { get; set; }
            public decimal DepreAmt { get; set; }
            public decimal DepreMaxAmt { get; set; }
            public long? ErrCode { get; set; }
            public string ErrMsg { get; set; }
        }
        public class DepreciationProcessModelViews
        {
            public DateTime DepreProcessDate { get; set; }
            public DateTime DepreFromdate { get; set; }
            public DateTime DepreTodate { get; set; }

            public long FK_ItemCategory { get; set; }
            public long FK_ItemMaster { get; set; }
            public string Mode { get; set; }
            public string ModeName { get; set; }
            public string CatName { get; set; }
            public string ItemName { get; set; }
            public long FK_Product { get; set; }


        }
        //public class DepreciationProcessDetailsView
        //{
        //    public long ID_DepreciationProcessDetails { get; set; }
        //    public DateTime DepreProcessDate { get; set; }
        //    public DateTime DepreFromDate { get; set; }
        //    public DateTime DepreToDate { get; set; }
        //    public long FK_Category { get; set; }
        //    public string Mode { get; set; }
        //    public long FK_Product { get; set; }
        //    public long FK_Branch { get; set; }
        //    public long ItemQty { get; set; }
        //    public long DeprePeriod { get; set; }
        //    public decimal DepreAmt { get; set; }
        //    public decimal DepreMaxAmt { get; set; }
        //    public long FK_ItemStock { get; set; }
        //    public long FK_DepreciationSettings { get; set; }
        //}
        public class DepreciationProcessDetailsView
        {

            public long FK_Category { get; set; }

            public string CatName { get; set; }
            public long FK_ItemMaster { get; set; }
            public string ItemName { get; set; }
            public long FK_ItemStock { get; set; }
            public string Mode { get; set; }
            public string ModeName { get; set; }
            public long ItemQty { get; set; }
            public long DeprePeriod { get; set; }
            public decimal DepreAmt { get; set; }
            public decimal DepreMaxAmt { get; set; }
        }
        public class DepreciationProcessDeleteData
        {
            public int UserAction { get; set; }
            public long ID_DepreciationProcess { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_DepreciationProcess { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }           
            public int FK_Reason { get; set; }


        }
        public APIGetRecordsDynamic<ModeType> GetModeTypList(Modes input, string companyKey)
        {
            return Common.GetDataViaProcedure<ModeType, Modes>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }

        public APIGetRecordsDynamic<Depreciationgridout> GetDepreciationDetailsData(GetDepreciationData input, string companyKey)
        {
            return Common.GetDataViaProcedure<Depreciationgridout, GetDepreciationData>(companyKey: companyKey, procedureName: "ProDepreciationProcess", parameter: input);

        }
        public APIGetRecordsDynamic<DepreciationProcessModelView> GetDepreciationProcessSelect(DepreciationProcessID input, string companyKey)
        {
            return Common.GetDataViaProcedure<DepreciationProcessModelView, DepreciationProcessID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public Output UpdateDepreciationProcessData(DepreciationProcessUpdateData input, string companyKey)
        {
            return Common.UpdateTableData<DepreciationProcessUpdateData>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public APIGetRecordsDynamic<DepreciationProcessModelViews> GetDepreciationDatainfoid(DepreciationProcessID input, string companyKey)
        {
            return Common.GetDataViaProcedure<DepreciationProcessModelViews, DepreciationProcessID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<XMLDepreciationProcessDetail> GetDepreciationProcessDetails(GetDepreciationdetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<XMLDepreciationProcessDetail, GetDepreciationdetails>(companyKey: companyKey, procedureName: "ProDepreciationProcess", parameter: input);

        }
        public APIGetRecordsDynamic<DepreciationProcessDetailsView> GetDepreciationDatainfodetails(DepreciationProcessID input, string companyKey)
        {
            return Common.GetDataViaProcedure<DepreciationProcessDetailsView, DepreciationProcessID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        //public Output DeleteDepreciationProcess(DepreciationProcessDelete input, string companyKey)
        //{
        //    return Common.UpdateTableData<DepreciationProcessDelete>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        //}

        public Output DeleteDepreciationProcess(DepreciationProcessDeleteData input, string companyKey)
        {
            return Common.UpdateTableData<DepreciationProcessDeleteData>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public static string _updateProcedureName = "ProDepreciationProcessUpdate";
        public static string _deleteProcedureName = "ProDepreciationProcessUpdate";
        public static string _selectProcedureName = "ProDepreciationProcessSelect";
    }
}