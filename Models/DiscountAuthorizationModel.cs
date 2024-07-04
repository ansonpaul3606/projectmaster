using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace PerfectWebERP.Models
{
    public class DiscountAuthorizationModel
    {
        public class DiscountAuthorizationViewList
        {
            public List<Category> CategoryList { get; set; }
            public List<Subcategory> SubCategoryList { get; set; }
            //public List<ComplaintList> ComplaintLists { get; set; }
            public IEnumerable<Userrole> UserRolelists { get; set; }
            public IEnumerable<MenuGroupModel.MenuGroupView> ModuleList { get; set; }


        }
        public class DiscountAuthorizationView
        {
            public long ID_DiscountAuthorizationSettings { get; set; }
            public DateTime EffectDate { get; set; }
            public long FK_SubCategory { get; set; }
           // public long FK_ComplaintList { get; set; }
            public long FK_Product { get; set; }
            public long FK_Category { get; set; }
            public long FK_DiscountAuthorizationSettings { get; set; }
            public List<DetailsView> DetailsView { get; set; }             
            public string TransMode { get; set; }
            public long TotalCount { get; set; }
            public long ReasonID { get; set; }
            public string Product { get; set; }
            public string SubCatName { get; set; }
            public long MenuGroupID { get; set; }
            public string MenuGroup { get; set; }
            public long MenuListID { get; set; }
            public string MenuList { get; set; }
            public long FK_MenuGroup { get; set; }
            public long FK_MenuList { get; set; }
            public bool SkipPreviousLevel { get; set; }
            public bool ActiveCorrectionOption { get; set; }


        }
        public class DetailsView   {

            public long FK_DiscountAuthorizationSettings { get; set; }

            public long FK_UserRole { get; set; }
            public long FK_Users { get; set; }
            public long DiscSettingsCriteria { get; set; }
            public decimal DisCountFrom { get; set; }
            public decimal DisCountTo { get; set; }         
           



        }
        public class DetailsView1
        {

            public long FK_DiscountAuthorizationSettings { get; set; }
            public long FK_UserRole { get; set; }
            public decimal DisCountFrom { get; set; }
            public decimal DisCountTo { get; set; }
            public string UserRole { get; set; }
            public string DiscSettingsCriteria { get; set; }    
            public long FK_Users { get; set; }
            public string UserName { get; set; }



        }
        public class MenuGroups
        {
            public int MenuGroupID { get; set; }
            public string MenuGroup { get; set; }
        }
        public class MenuLists
        {
            public int MenuListID { get; set; }
            public string MenuList { get; set; }
        }
        public class Users
        {
            public int UserID { get; set; }
            public string UserName { get; set; }
        }
        public class Usersrelated
        {
            public long FK_BranchMode { get; set; }
            public long FK_Company { get; set; }
            public int FK_UserRole { get; set; }
        }
     

        public class UpdateDiscountAuthorization
        {
	        public long ID_DiscountAuthorizationSettings { get; set; }
            public DateTime EffectDate { get; set; }      
            public long FK_SubCategory { get; set; }
           public long FK_ComplaintList { get; set; }
            public long FK_Product { get; set; }               
            public long FK_Category { get; set; }
            public long FK_DiscountAuthorizationSettings { get; set; }
            public string TransMode { get; set; }  

            public string DetailsView { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int UserAction { get; set; }
            public int Debug { get; set; }
            public long FK_MenuGroup { get; set; }
            public long FK_MenuList { get; set; }
            public bool SkipPreviousLevel { get; set; }
            public bool ActiveCorrectionOption { get; set; }
        }
        public class Category        {
          
            public long CategoryID { get; set; }
            public string CategoryName { get; set; }
        }
       
        public class Subcategory
        {
            public long CategoryID { get; set; }
            public long ID_SubCategory { get; set; }
            public string SubCatName { get; set; }
            public bool Project { get; set; }
        }

        //public class ComplaintList
        //{
        //    public int ?ComplaintListID { get; set; }
        //    public string Complaint { get; set; }
        //}
        public class Userrole
        {
            public int UserRoleID { get; set; }
            public string UserRole { get; set; }
            public long FK_UserRole { get; set; }
        }
        public class DiscountAuthorizationID
        {
            public Int64 FK_DiscountAuthorizationSettings { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            public Byte Detailed { get; set; }
        }
        public class DiscountSelectDetails
        {
            public long SlNo { get; set; }
            public long FK_DiscountAuthorizationSettings { get; set; }
            public string Module { get; set; }
            public DateTime EffectDate { get; set; }
            public string Category { get; set; }
            public Int64 TotalCount { get; set; }
            public long ID_DiscountAuthorizationSettings { get; set; }
        }
        public class DeleteDiscountType
        {
            public long FK_DiscountAuthorizationSettings { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Reason { get; set; }
        }

        public Output UpdateDiscountData(UpdateDiscountAuthorization input, string companyKey)
        {
            return Common.UpdateTableData<UpdateDiscountAuthorization>(parameter: input, companyKey: companyKey, procedureName: "ProDiscountAuthorizationSettingsUpdate");
        }
        public APIGetRecordsDynamic<DiscountSelectDetails> GetDiscountAuthorizationData(DiscountAuthorizationID input, string companyKey)
        {
            return Common.GetDataViaProcedure<DiscountSelectDetails, DiscountAuthorizationID>(companyKey: companyKey, procedureName: "ProDiscountAuthorizationSettingsSelect", parameter: input);
        }
        public APIGetRecordsDynamic<DiscountAuthorizationView> GetDiscountData(DiscountAuthorizationID input, string companyKey)
        {
            return Common.GetDataViaProcedure<DiscountAuthorizationView, DiscountAuthorizationID>(companyKey: companyKey, procedureName: "ProDiscountAuthorizationSettingsSelect", parameter: input);
        }
        public APIGetRecordsDynamic<DetailsView1> GetDiscountDatanew(DiscountAuthorizationID input, string companyKey)
        {
            return Common.GetDataViaProcedure<DetailsView1, DiscountAuthorizationID>(companyKey: companyKey, procedureName: "ProDiscountAuthorizationSettingsSelect", parameter: input);

        }
        public Output DeleteDiscountAuthorizationData(DeleteDiscountType input, string companyKey)
        {
            return Common.UpdateTableData<DeleteDiscountType>(parameter: input, companyKey: companyKey, procedureName: "ProDiscountAuthorizationSettingsDelete");
        }
        public APIGetRecordsDynamic<Users> GetAuthorizationlevelUsersnamelist(Usersrelated input, string companyKey)
        {
            return Common.GetDataViaProcedure<Users, Usersrelated>(companyKey: companyKey, procedureName: "ProAuthorizationlevelUsersnamelist", parameter: input);
        }



    }
}