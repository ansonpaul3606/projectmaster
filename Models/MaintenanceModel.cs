
//@*---------------------------------------------------------------------

//Created By  : Jisi Rajan
//Created On  : 23/01/2022
//Purpose	  :  Maintenance
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
    public class MaintenanceModel
    {


        public class MaintenanceView
        {

            public long SlNo { get; set; }
            public long MaintenanceID { get; set; }


            public string Maintenance { get; set; }

            public string ShortName { get; set; }

            public string Description { get; set; }
            public long AccountHead { get; set; }
            public long AccountHeadSub { get; set; }
            public string AHeadName { get; set; }
            public string ASHeadName { get; set; }
            public Int32 SortOrder { get; set; }
            public int AccountCode { get; set; }
            public int AccountSHCode { get; set; }
            public string Mode { get; set; }
            public Int64 TotalCount { get; set; }
        }


        public class MaintenanceInputFromViewModel
        {

            [Required(ErrorMessage = "NO Maintenance Selected")]
            public long MaintenanceID { get; set; }
            [Required(ErrorMessage = "Please Enter Maintenance")]
            public string Maintenance { get; set; }

            [Required(ErrorMessage = "Please Enter Short Name")]
            public string ShortName { get; set; }
            public string Description { get; set; }
            public long ?AccountHead { get; set; }
            public long? AccountHeadSub { get; set; }
            public Int32 SortOrder { get; set; }
            public string TransMode { get; set; }
            public string Mode { get; set; }
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

        public class MaintenanceInfoView
        {
            [Required(ErrorMessage = "No Maintenance Selected")]
            public Int64 MaintenanceID { get; set; }
            [Required(ErrorMessage = "Please Select The Reason")]
            public Int64 ReasonID { get; set; }
        }


        public static string _updateProcedureName = "proMaintenanceUpdate";
        public class UpdateMaintenance
        {


            public long ID_Maintenance { get; set; }
            public long FK_Maintenance { get; set; }
            public string MaintenanceName { get; set; }
            public string MaintenanceShortName { get; set; }
            public string MaintenanceDescription { get; set; }
            public Int32 SortOrder { get; set; }
            public Int64 FK_AccountHead { get; set; }
            public long FK_AccountHeadSub { get; set; }
            public string TransMode { get; set; }

            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

            public string Mode { get; set; }



        }

        public static string _selectProcedureName = "proMaintenanceSelect";
        public class GetMaintenance
        {
            public Int64 FK_Maintenance { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string Name { get; set; }
        }



        public static string _deleteProcedureName = "proMaintenanceDelete";
        public class DeleteMaintenance
        {
            public long FK_Maintenance { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }




        public class MaintenanceListModel
        {

            //public List<TypeMode> TypeModeList { get; set; }

            public int SortOrder { get; set; }
            public List<ModeList> modeList { get; set; }
        }
        public class ModeList
        {
            public int ModeID { get; set; }
            public string ModeName { get; set; }
            public string Mode { get; set; }
            public int FK_ModuleType { get; set; }
        }


        public Output UpdateMaintenanceData(UpdateMaintenance input, string companyKey)
        {
            return Common.UpdateTableData<UpdateMaintenance>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteMaintenanceData(DeleteMaintenance input, string companyKey)
        {
            return Common.UpdateTableData<DeleteMaintenance>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }


        public APIGetRecordsDynamic<MaintenanceView> GetMaintenanceData(GetMaintenance input, string companyKey)
        {
            return Common.GetDataViaProcedure<MaintenanceView, GetMaintenance>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
    }
}









