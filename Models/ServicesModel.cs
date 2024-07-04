
//@*---------------------------------------------------------------------

//Created By  : Jisi Rajan
//Created On  : 23/01/2022
//Purpose	  : Services
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
    public class ServicesModel
    {
        public class ServicesView
        {

            public long SlNo { get; set; }
            public long ServicesID { get; set; }


            public string Services { get; set; }

            public string ShortName { get; set; }

            public bool Paid { get; set; }
            public string Description { get; set; }
            public long AccountHead { get; set; }
            public long AccountHeadSub { get; set; }
            public string AHeadName { get; set; }
            public string ASHeadName { get; set; }
            public Int32 SortOrder { get; set; }
            public int AccountCode { get; set; }
            public int AccountSHCode { get; set; }
            public bool ServiceChargeIncludeTax { get; set; }
            public long FK_TaxGroup { get; set; }
          
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

        public class ServicesInputFromViewModel
        {

            [Required(ErrorMessage = "NO Service Selected")]
            public long ServicesID { get; set; }

            [Required(ErrorMessage = "Please Enter Services")]
            public string Services { get; set; }

            [Required(ErrorMessage = "Please Enter Short Name")]
            public string ShortName { get; set; }

            //[Required(ErrorMessage = "Select Services Paid")]
            public bool Paid { get; set; }
            public string Description { get; set; }
            public long AccountHead { get; set; }
            public long AccountHeadSub { get; set; }
            public Int32 SortOrder { get; set; }
            public bool IncludeTax { get; set; }
            public long? TaxGroupID { get; set; }

        }


        public class ServicesInfoView
        {
            [Required(ErrorMessage = "No Services Selected")]
            public Int64 ServicesID { get; set; }
            [Required(ErrorMessage = "Please Select The Reason")]
            public Int64 ReasonID { get; set; }
        }


        public static string _updateProcedureName = "proServicesUpdate";
        public class UpdateServices
        {

            public long FK_Services { get; set; }
            public long ID_Services { get; set; }
         
            public string ServicesName { get; set; }
            public string ServicesShortName { get; set; }
            public string ServicesDescription { get; set; }
           
            public bool ServicesPaid { get; set; }
            public Int64 FK_AccountHead { get; set; }
            public long FK_AccountHeadSub { get; set; }
            public Int32 SortOrder { get; set; }
            public string TransMode { get; set; }

            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long? FK_TaxGroup { get; set; }
            public bool ServiceChargeIncludeTax { get; set; }

        }

        public static string _selectProcedureName = "proServicesSelect";
        public class GetServices
        {
            public Int64 FK_Services { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
        }



        public static string _deleteProcedureName = "proServicesDelete";
        public class DeleteServices
        {
            public long FK_Services { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }




        public class ServicesListModel
        {
            public int SortOrder { get; set; }
            public List<TaxGroup> TaxgroupList { get; set; }

        }
        public class TaxGroup
        {
            public long? TaxGroupID { get; set; }
            public string TaxGroupName { get; set; }
        }


        public Output UpdateServicesData(UpdateServices input, string companyKey)
        {
            return Common.UpdateTableData<UpdateServices>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteServicesData(DeleteServices input, string companyKey)
        {
            return Common.UpdateTableData<DeleteServices>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }


        public APIGetRecordsDynamic<ServicesView> GetServicesData(GetServices input, string companyKey)
        {
            return Common.GetDataViaProcedure<ServicesView, GetServices>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
    }
}









