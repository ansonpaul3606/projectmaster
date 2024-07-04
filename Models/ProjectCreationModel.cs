/*----------------------------------------------------------------------
Created By	: Santhisree
Created On	: 27/07/2022
Purpose		: ProjectCreation
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
-------------------------------------------------------------------------*/
using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace PerfectWebERP.Models
{
    public class ProjectCreationModel
    {

        public class ProjectCreationView
        {
            public long SlNo { get; set; }
            public long ProjectID { get; set; }
            public long LeadGenerationID { get; set; }
            public string LeadNo { get; set; }
            public string CusNumber { get; set; }
            public DateTime CreateDate { get; set; }
           
            public string Name { get; set; }

            public string ShortName { get; set; }

            public int Category { get; set; }
            public Int64 BranchID { get; set; }
            public int SubCategory { get; set; }
            public decimal FinalAmount { get; set; }
            public decimal ProjectAmount { get; set; }
            public decimal DiscountAmount { get; set; }
            public decimal BuyBackAmount { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int Duration { get; set; }
            public int DurationType { get; set; }
            public Int64 TotalCount { get; set; }
            public DateTime? LeadDates { get; set; }
            public bool ProjIncludeTax { get; set; }
            public Int64 FK_TaxGroup { get; set; }
            public long? LastID { get; set; }
            public string TransMode { get; set; }
            public Int64 Passed { get; set; }
            public Int64 FK_Customer { get; set; }
            

        }

        public class ProjectCreationInputFromViewModel
        {

            public long ProjectID { get; set; }
            public long LeadGenerationID { get; set; }

            public DateTime CreateDate { get; set; }

            public string Name { get; set; }

            public string ShortName { get; set; }

            public int Category { get; set; }

            public int SubCategory { get; set; }
            public decimal FinalAmount { get; set; }
            public decimal ProjectAmount { get; set; }
            public decimal DiscountAmount { get; set; }
            public decimal BuyBackAmount { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int Duration { get; set; }
            public int DurationType { get; set; }
            public Int64 BranchID { get; set; }
            public bool ProjIncludeTax { get; set; }
            public Int64 FK_TaxGroup { get; set; }
            public List<ImageListView> ImageList { get; set; }
            public List<BuyBackDetails> buyback { get; set; }
            public long? LastID { get; set; }
            public string TransMode { get; set; }

        }

        public class ProjectCreationInfoView
        {
            [Required(ErrorMessage = "No ProjectCreation Selected")]
            public Int64 ProjectID { get; set; }
            //[Required(ErrorMessage = "Please select the reason")]
            public string TransMode { get; set; }
            public Int64 ReasonID { get; set; }

        }
        public class BuyBackDetails
        {
            public long SlNo { get; set; }
            public long ID_BuyBackDetails { get; set; }

            public long FK_Product { get; set; }
            public string ProdName { get; set; }

            public decimal Rate { get; set; }
            public decimal Quantity { get; set; }
            public string SerialNo { get; set; }
        }
        public class BuyBack
        {
            public bool GsValue { get; set; }
            public string GsField { get; set; }

        }
        public class EnableEdit
        {
            public long FK_Project { get; set; } 
            public int PrBillMode { get; set; }

        }

        public static string _updateProcedureName = "proProjectUpdate";
        public class UpdateProjectCreation
        {
            public long FK_Project { get; set; }
            public long ID_Project { get; set; }
            public string ProjName { get; set; }
            public string ProjShortName { get; set; }
            public long FK_LeadGeneration { get; set; }
            public long FK_Category { get; set; }
            public long FK_SubCategory { get; set; }
            public long FK_Branch { get; set; }
            public decimal ProjFinalizationAmount { get; set; }
            public decimal ProjectAmount { get; set; }
            public decimal DiscountAmount { get; set; }
            public decimal BuyBackAmount { get; set; }
            public DateTime ProjCreateDate { get; set; }
            public DateTime ProjStartDate { get; set; }
            public int ProjDuration { get; set; }
            public int ProjDurationType { get; set; }
            public DateTime ProjFinishDate { get; set; }

            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public bool ProjIncludeTax { get; set; }
            public Int64 FK_TaxGroup { get; set; }
            public string ImageList { get; set; }
            public string buyback { get; set; }
            public long? LastID { get; set; }
        }

        public class SelectProjectCreation
        {

            public long FK_Project { get; set; }
            public string PrStName { get; set; }
            public string PrStShortName { get; set; }
            public long FK_Category { get; set; }
            public long FK_SubCategory { get; set; }
            public Int32 SortOrder { get; set; }
            public Int64 FK_Company { get; set; }
            public byte UserAction { get; set; }

            public Int64 FK_Machine { get; set; }
            public string UserCode { get; set; }
            public Int32 BranchCode { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string AuditData { get; set; }
            public string SqlUpdateQuery { get; set; }
            public Int64 FK_Reason { get; set; }
            public string EntrBy { get; set; }
            public Int64 BackupId { get; set; }
            public bool Cancelled { get; set; }

        }

        public static string _deleteProcedureName = "proProjectDelete";
        public class DeleteProjectCreation
        {
            public Int64 FK_Project { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }
        public class GetBuyBack
        {

            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }

            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Master { get; set; }


        }
        public class Category
        {
            public string Mode { get; set; }
            public long CategoryID { get; set; }
            public string CategoryName { get; set; }
        }
        public class Subcategory
        {
            public long CategoryID { get; set; }
            public long ID_SubCategory { get; set; }
            public string SubCatName { get; set; }
        }
        public class DurationTypeList
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }

        }
        public class ProjectCreationListModel
        {
            public int SortOrder { get; set; }
            public List<Category> CategoryList { get; set; }
            public List<Subcategory> SubCategoryList { get; set; }
            public List<DurationTypeList> DurationTypeList { get; set; }
            public List<TaxGroup> TaxgroupList { get; set; }
            public List<BranchList> BranchList { get; set; }
            
        }
        public class BranchList
        {
            public long BranchID { get; set; }
            public string Branch { get; set; }
        }

        public class TaxGroup
        {
            public long TaxGroupID { get; set; }
            public string TaxGroupName { get; set; }
        }
        public class InputProjectID
        {
            public Int64 FK_Project { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string Name { get; set; }
            public long Image { get; set; }

        }
        public class ImageListView
        {
            public long SLNo { get; set; }
            public string TransMode { get; set; }
            public long FK_Master { get; set; }
            public long FK_Customer { get; set; }
            public long FK_Product { get; set; }
            public long FK_SubProduct { get; set; }
            public string ProdImageName { get; set; }
            public string ProdImage { get; set; }
            public string ProdImageMode { get; set; }
            public string ProdImageDescription { get; set; }
            public long ID_ProductImage { get; set; }
            public long WarrantyType { get; set; }
            public string WarTypName { get; set; }
            public string ProdImageType { get; set; }

        }
        
        public class GetImagein
        {

            public string TransMode { get; set; }
            public Int64 FK_Project { get; set; }           
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }          
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public long Image { get; set; }
            public string Name { get; set; }

        }
        public class UpdateProjectCreationResult
        {
            public string Success { get; set; }
            public string CusNumber { get; set; }
            public string ProjectNo { get; set; }
            public Int32 ErrCode { get; set; }
            public string ErrMsg { get; set; }

        }
        public class CustomerID
        {
            public string TransMode { get; set; }
            public long FK_Customer { get; set; }          
            public bool CusOth { get; set; }          
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }           

        }
        public class CustomerView
        {
            public long SlNo { get; set; }
            public long CustomerID { get; set; }
            public string Number { get; set; }
            public string Name { get; set; }
            public string GSTINNo { get; set; }
            public string Address1 { get; set; }
            public string Mobile { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public long BranchTypeID { get; set; }
            public long LeadGenerateID { get; set; }
            public string LeadGenerateNo { get; set; }
            public string LGCustomer { get; set; }

            public bool Individual { get; set; }
            public bool Individuals { get; set; }
            public long CountryID { get; set; }
            public string Country { get; set; }
            public long StatesID { get; set; }
            public string States { get; set; }
            public long DistrictID { get; set; }
            public string District { get; set; }
            public long? AreaID { get; set; }
            public string Area { get; set; }
            public long? PostID { get; set; }
            public string Post { get; set; }
            public string PinCode { get; set; }
            public long? PlaceID { get; set; }
            public string Place { get; set; }
            public long CustomerTypeID { get; set; }
            public string CustomerType { get; set; }
            public long BranchID { get; set; }
            public string Branch { get; set; }
            public long? OccupationID { get; set; }
            public string Occupation { get; set; }
            public string ContactPerson { get; set; }
            public string ContactMobile { get; set; }
            public string ContactEmail { get; set; }
            public Int64 TotalCount { get; set; }
            public string TransMode { get; set; }
            public string Landmark { get; set; }
            public string CusReferenceNo { get; set; }
            public bool IGSTCust { get; set; }
            public bool CusOth { get; set; }
        }
        public APIGetRecordsDynamic<UpdateProjectCreationResult> UpdateProjectCreationDatas(UpdateProjectCreation input, string companyKey)
        {
            return Common.GetDataViaProcedure<UpdateProjectCreationResult, UpdateProjectCreation>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output UpdateProjectCreationData(UpdateProjectCreation input, string companyKey)
        {
            return Common.UpdateTableData<UpdateProjectCreation>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
       
        public Output DeleteProjectCreationData(DeleteProjectCreation input, string companyKey)
        {
            return Common.UpdateTableData<DeleteProjectCreation>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<BuyBackDetails> GetBuyBackSelect(GetBuyBack input, string companyKey)
        {
            return Common.GetDataViaProcedure<BuyBackDetails, GetBuyBack>(companyKey: companyKey, procedureName: "ProBuyBackDetailsSelect", parameter: input);

        }

        public static string _selectProcedureName = "ProProjectSelect";
        public APIGetRecordsDynamic<ProjectCreationView> GetProjectCreationData(InputProjectID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProjectCreationView, InputProjectID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<ImageListView> GetImageSelect(GetImagein input, string companyKey)
        {
            return Common.GetDataViaProcedure<ImageListView, GetImagein>(companyKey: companyKey, procedureName: "ProProjectSelect", parameter: input);

        }
        public APIGetRecordsDynamic<CustomerView> GetCustomerDetailsFill(CustomerID input, string companyKey)
        {
            return Common.GetDataViaProcedure<CustomerView, CustomerID>(companyKey: companyKey, procedureName: "ProSelectCustomerForEdit", parameter: input);

        }

    }
}