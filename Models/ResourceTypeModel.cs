using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ResourceTypeModel
    {
        public class ResourceView
        {
            public long ID_ProjectResources { get; set; }
            public string ProResourceName { get; set; }
            public string ProResourceShortName { get; set; }
            public Int32 SortOrder { get; set; }

            public Int64 FK_ProjectResources { get; set; }

        }
        public class ResourceTypeList
        {         
            public int SortOrder { get; set; }
        }      
    
        public class UpdateResourceType
        {
            public int UserAction { get; set; }
            public int Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_ProjectResources { get; set; }
            public string ProResourceName { get; set; }
            public string ProResourceShortName { get; set; }          
            public Int32 SortOrder { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public Int64 FK_ProjectResources { get; set; }
        }
        public class GetResourceTypeDetails
        {
            public Int64 FK_ProjectResources { get; set; }
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }

        }
        public class GetResourceTypeIdDetails
        {
            public Int64 FK_ProjectResources { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }
        public class ResourceTypeSelectDetails
        {
            public long SlNo { get; set; }
            public long ID_ProjectResources { get; set; }
            public string ProResourceName { get; set; }
            public string ProResourceShortName { get; set; }        
            public Int32 SortOrder { get; set; }        
            public Int64 TotalCount { get; set; }
        }
        public class DeleteResourceType
        {
            public string TransMode { get; set; }
            public Int64 FK_ProjectResources { get; set; }
            public int Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
        }
        public class DeleteView
        {
            public long ID_ProjectResources { get; set; }
            public Int64 ReasonID { get; set; }
        }
        public class OutputTest
        {
            public long Column1 { get; set; }
            public int ErrCode { get; set; }
            public string ErrMsg { get; set; }
        }
        //public Output UpdateResourceData(UpdateResourceType input, string companyKey)
        //{
        //    return Common.UpdateTableData<UpdateResourceType>(parameter: input, companyKey: companyKey, procedureName: "ProProjectResourcesUpdate");
        //}
        public APIGetRecordsDynamic<OutputTest> UpdateResourceData(UpdateResourceType input, string companyKey)
        {
            return Common.GetDataViaProcedure<OutputTest, UpdateResourceType>(parameter: input, companyKey: companyKey, procedureName: "ProProjectResourcesUpdate");
        }
        public APIGetRecordsDynamic<ResourceTypeSelectDetails> GetResourceSelect(GetResourceTypeDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<ResourceTypeSelectDetails, GetResourceTypeDetails>(companyKey: companyKey, procedureName: "ProProjectResourcesSelect", parameter: input);

        }
        public APIGetRecordsDynamic<ResourceTypeSelectDetails> GetResourceSelectData(GetResourceTypeIdDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<ResourceTypeSelectDetails, GetResourceTypeIdDetails>(companyKey: companyKey, procedureName: "ProProjectResourcesSelect", parameter: input);

        }
        public Output DeleteResourceData(DeleteResourceType input, string companyKey)
        {
            return Common.UpdateTableData<DeleteResourceType>(parameter: input, companyKey: companyKey, procedureName: "ProProjectResourcesDelete");
        }
    }
}



   
       
       
       
      
      
     
   
       
     
