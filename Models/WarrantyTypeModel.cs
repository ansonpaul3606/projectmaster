using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace PerfectWebERP.Models
{
    public class WarrantyTypeModel
    {
       
        public class WarrantyTypeView
        {
            public Int64 WarrantyTypeID { get; set; }
            public string WarrantyName { get; set; }
            public string ShortName { get; set; }
            public Int16 SortOrder { get; set; }
            public string EntrBy { get; set; }
            public DateTime EntrOn { get; set; }
            public string UserCode { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public bool IsExtended { get; set; }
            public Int64 TotalCount { get; set; }
        }
        public class WarantyTypeInputView
        {
            //[Required(ErrorMessage = "Please select Warranty Type")]
            //[Range(1, long.MaxValue, ErrorMessage = "Please select Warranty Type")]
            public Int64 WarrantyTypeID { get; set; }
            [Required(ErrorMessage = "Please enter Warranty Type Name")]
            [StringLength(150, ErrorMessage = "Maximum 150 characture")]
            public string WarrantyTypeName { get; set; }
            [StringLength(10, ErrorMessage = "Maximum 10 characture")]
            [Required(ErrorMessage = "Please Enter Warranty Type Short Name")]
            public string WarrantyTypeShortName { get; set; }
            public Int16 SortOrder { get; set; }

            public Int16 IsExtended { get; set; }
            public string TransMode { get; set; }
        }

        public class GetWarrantyInfo
        {
            [Required(ErrorMessage = "Please select a Warranty")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a Warranty")]
            public Int64 WarrantyTypeID { get; set; }
            [Required(ErrorMessage = "Please select the reason")]
            public Int64 ReasonID { get; set; }

        }

        public class UpdateWarranty
        {

            public byte UserAction { get; set; }
            public long ID_WarrantyType { get; set; }
            public long FK_WarrantyType { get; set; }
            public string WartyName { get; set; }
            public string WartyShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public Int16 IsExtended { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
           // public bool Passed { get; set; }
            public string EntrBy { get; set; }
           // public DateTime EntrOn { get; set; }
            public long FK_Machine { get; set; }
            public long BackupId { get; set; }
            public int Debug { get; set; }
            public string TransMode { get; set; }          


        }
     



        public class GetWarrantyType //Common Procedure
        {
            public Int64 FK_WarrantyType { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }

            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
        }

        public class WarrantyList
        {
            public int SortOrder { get; set; }
        }

        public class WarrantyTypeInfoView
        {
            [Required(ErrorMessage = "Please select a Warranty Type")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a Warranty Type")]
            public Int64 WarrantyTypeID { get; set; }
            [Required(ErrorMessage = "Please select the reason")]
            public Int64 ReasonID { get; set; }


        }


        public static string _deleteProcedureName = "proWarrantyTypeDelete"; //commonprocedure
        public static string _updateProcedureName = "proWarrantyTypeUpdate";  //commonprocedure
        public static string _selectProcedureName = "proWarrantyTypeSelect"; //commonprocedure

        //CommonProcedureDelete
        public class DeleteWarrantyType
        {
            public string TransMode { get; set; }
            public Int64 FK_WarrantyType { get; set; }
            public Int64 FK_Reason { get; set; }
            public int Debug { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Company { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }


        }
      
        public Output UpdateWarrantyData(UpdateWarranty input, string companyKey)
        {
            return Common.UpdateTableData<UpdateWarranty>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteWarrantyData(DeleteWarrantyType input, string companyKey)
        {
            return Common.UpdateTableData<DeleteWarrantyType>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
    
        public APIGetRecordsDynamic<WarrantyTypeView> GetWarrantyTypeData(GetWarrantyType input, string companyKey)
        {
            return Common.GetDataViaProcedure<WarrantyTypeView, GetWarrantyType>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);


        }
    }
}