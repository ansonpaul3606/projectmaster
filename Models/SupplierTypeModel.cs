/*----------------------------------------------------------------------
Created By	: Aswanth
Created On	: 10/02/2023
Purpose		: SupplierType
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
-------------------------------------------------------------------------*/

using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class SupplierTypeModel
    {

        public class SupplierTypeView
        {
            [Required(ErrorMessage = "Please Enter Name")]
            public string STName { get; set; }
            [Required(ErrorMessage = "Please Enter Name")]
            public string STShortName { get; set; }
            [Required(ErrorMessage = "Please Enter Sort Order")]
            public Int32 SortOrder { get; set; }
            //public long ReasonID { get; set; }
            public string EntrBy { get; set; }
            public DateTime EntrOn { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int64 TotalCount { get; set; } 
            public long ID_SupplierType { get; set; }
            public string SupplierModeName { get; set; }
            public int SupplierMode { get; set; }

            public List<SupplierMode> listMode { get; set; }

        }

        public class UpdateSupplierType
        {
            public long ID_SupplierType { get; set; }
            public string STName { get; set; }
            public string STShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public int UserAction { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public long FK_SupplierType { get; set; }
            public int Debug { get; set; }
            public int SupplierMode { get; set; }


        }

        public class GetSupplierType
        {
            //public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            public string EntrBy { get; set; }
            public long FK_SupplierType { get; set; }
            public string Name { get; set; }
        }
    

        public static string _deleteProcedureName = "ProSupplierTypeDelete";
        public static string _updateProcedureName = "ProSupplierTypeUpdate";
        public static string _selectProcedureName = "ProSupplierTypeSelect";

        public class DeleteSupplierType
        {
            ///public long ID_SupplierType { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string TransMode { get; set; }
            public long FK_SupplierType { get; set; }
            public int Debug { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
        }
        public class SupplierMode
        {
            public int ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class Modetype
        {
            public int Mode { get; set; }
        }


        public class SupplierTypeID
        {
            [Required(ErrorMessage = "Please select a Supplier Type")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a Supplier Type")]
            public Int64 ID_SupplierType { get; set; }
            public Int64 ReasonID { get; set; }
        }
        public Output UpdateSupplierTypeData(UpdateSupplierType input, string companyKey)
        {
            return Common.UpdateTableData<UpdateSupplierType>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteSupplierTypeData(DeleteSupplierType input, string companyKey)
        {
            return Common.UpdateTableData<DeleteSupplierType>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<SupplierTypeView> GetSupplierTypeData(GetSupplierType input, string companyKey)
        {
            return Common.GetDataViaProcedure<SupplierTypeView, GetSupplierType>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<SupplierMode> GetSupplierModeData(Modetype input, string companyKey)
        {
            return Common.GetDataViaProcedure<SupplierMode, Modetype>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }

    }
}
