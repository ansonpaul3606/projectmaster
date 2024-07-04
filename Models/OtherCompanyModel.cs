/*----------------------------------------------------------------------
Created By	: AmrithaAk
Created On	: 05/05/2022
Purpose		: OtherCompany
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
    public class OtherCompanyModel
    {

        public class OtherCompanyView
        {
            public long SlNo { get; set; }
            public long OtherCompanyID { get; set; }
           
            public string RegisterNo { get; set; }
           
            public string Name { get; set; }
            
            public string ShortName { get; set; }
           
            public string Address { get; set; }
         
            public string Country { get; set; }

            public long CountryID { get; set; }
           
            public string States { get; set; }

            public long? StatesID { get; set; }
          
            public string District { get; set; }
            
            public long? DistrictID { get; set; }
           
            public string Area { get; set; }

            public long? AreaID { get; set; }

           
            public string Post { get; set; }
            public long? PostID { get; set; }

           
            public string Place { get; set; }
            public long? PlaceID { get; set; }

            public string PinCode { get; set; }
            public string Mobile { get; set; }
            
            public string Email { get; set; }
           
            public string Website { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public Int64 TotalCount { get; set; }
            public string TransMode { get; set; }

        }

        public class UpdateOtherCompany
        {
            public long ID_OtherCompany { get; set; }
            public string OCRegisterNo { get; set; }
            public string OCName { get; set; }
            public string OCShortName { get; set; }
            public string OCAddress1 { get; set; }
            public long FK_Place { get; set; }
            public long FK_Post { get; set; }
            public long FK_Area { get; set; }
            public long FK_District { get; set; }
            public long FK_State { get; set; }
            public long FK_Country { get; set; }
            public string OCMobileNo { get; set; }
            public string OCEmail { get; set; }
            public string OCWebsite { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
            public string TransMode { get; set; }
            public long FK_OtherCompany { get; set; }
        }
        
        public static string _deleteProcedureName = "ProOtherCompanyDelete";
        public static string _updateProcedureName = "ProOtherCompanyUpdate";
        public static string _selectProcedureName = "ProOtherCompanySelect";

        public class DeleteOtherCompany
        {
            public long FK_OtherCompany { get; set; }
          
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }

        public class OtherCompanyInfoView
        {
            public long ID_OtherCompany { get; set; }

            public long ReasonID { get; set; }
        }

     
        public class OtherCompanyID
        {


            public Int64 FK_OtherCompany { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }


        }
        public Output UpdateOtherCompanyData(UpdateOtherCompany input, string companyKey)
        {
            return Common.UpdateTableData<UpdateOtherCompany>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteOtherCompanyData(DeleteOtherCompany input, string companyKey)
        {
            return Common.UpdateTableData<DeleteOtherCompany>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<OtherCompanyView> GetOtherCompanyData(OtherCompanyID input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherCompanyView, OtherCompanyID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
    }
}


