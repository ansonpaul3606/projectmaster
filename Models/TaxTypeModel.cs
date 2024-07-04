//@*---------------------------------------------------------------------

//Created By  : Jisi Rajan
//Created On  : 18/01/2022
//Purpose		: TaxType
//-------------------------------------------------------------------------
//Modification
//On          By OMID/Remarks
//-------------------------------------------------------------------------
//-------------------------------------------------------------------------*@

using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class TaxTypeModel
    {
        public class TaxTypesView
        {

            public long SlNo { get; set; }
            public long TaxTypeID { get; set; }


            public string TaxType { get; set; }

            public string ShortName { get; set; }

            public bool Interstate { get; set; }
            public bool Intrastate { get; set; }
            public short TypeModeID { get; set; }
            public string TypeModeName { get; set; }
            public long AccountHead { get; set; }
            public long AccountHeadSub { get; set; }
            public string AHeadName { get; set; }
            public int AccountCode { get; set; }
            public int AccountSHCode { get; set; }
            public string ASHeadName { get; set; }
            public Int32 SortOrder { get; set; }
            public Int64 TotalCount { get; set; }

        }

        public class AccountHeadView
        {

            public long AccountHead { get; set; }
          
            public string AHeadName { get; set; }
            public int AccountCode { get; set; }
           

        }

        public class AccountSubHeadView
        {
            public long AccountHeadSub { get; set; }
            public int AccountSHCode { get; set; }
            public string ASHeadName { get; set; }
       
        }
        public class TaxTypesInputFromViewModel
        {
            [Required(ErrorMessage = "No TaxType Selected")]
            public long TaxTypeID { get; set; }
            [Required(ErrorMessage = "Please Enter TaxType")]
            public string TaxType { get; set; }

            [Required(ErrorMessage = "Please Enter Short Name")]
            public string ShortName { get; set; }

            //[Range(1, long.MaxValue, ErrorMessage = "Select Tax Type Mode")]
            public short TypeModeID { get; set; }

            //[Required(ErrorMessage = "Please Enter Interstate")]
            public bool Interstate { get; set; }

            //[Required(ErrorMessage = "Please Enter Intrastate")]
            public bool Intrastate { get; set; }
           
            public Int32 SortOrder { get; set; }

            //[Required(ErrorMessage = "Please Enter Account Head")]
            public long AccountHead { get; set; }

            //[Required(ErrorMessage = "Please Enter Account Head Sub")]
            public long AccountHeadSub { get; set; }

        }

        
        public class TaxTypeInfoView
        {
            [Required(ErrorMessage = "No TaxType Selected")]
            public Int64 TaxTypeID { get; set; }
            [Required(ErrorMessage = "Please Select The Reason")]
            public Int64 ReasonID { get; set; }
        }


        public static string _updateProcedureName = "proTaxTypeUpdate";
        public class UpdateTaxType
        {
            public long FK_TaxType { get; set; }
            public long ID_TaxType { get; set; }
            public short FK_TypeMode { get; set; }
           
            public string TaxtyName { get; set; }
            public string TaxtyShortName { get; set; }
            public bool TaxtyInterstate { get; set; }
            public bool TaxtyIntrastate { get; set; }
            public Int64 FK_AccountHead { get; set; }
            public long FK_AccountHeadSub { get; set; }
            public Int32 SortOrder { get; set; }
            public string TransMode { get; set; }

            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }



        }

        public static string _selectProcedureName = "proTaxTypeSelect";
        public class GetTaxType
        {
            public Int64 FK_TaxType { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
           
        
            public string Name { get; set; }
        }
       


        public static string _deleteProcedureName = "proTaxTypeDelete";
        public class DeleteTaxType
        {
            public long FK_TaxType { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }

      
        public class TypeMode
        {
            public short TypeModeID { get; set; }
            public string TypeModeName { get; set; }
        }
       
        public class TaxTypeListModel
        {
         
            public List<TypeMode> TypeModeList { get; set; }
       
            public int SortOrder { get; set; }

        }
      


        public Output UpdateTaxTypeData(UpdateTaxType input, string companyKey)
        {
            return Common.UpdateTableData<UpdateTaxType>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteTaxTypeData(DeleteTaxType input, string companyKey)
        {
            return Common.UpdateTableData<DeleteTaxType>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<TaxTypesView> GetTaxTypeData(GetTaxType input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxTypesView, GetTaxType>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
    }
}


        
       
     
      

       
       
        