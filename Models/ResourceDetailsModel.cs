using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ResourceDetailsModel
    {
        public class ResourceDetailsView
        {


            public long ID_ProjectResourceMarking { get; set; }
            public DateTime PrmMarkingDate { get; set; }
            public Int64 TotalCount { get; set; }
            public long FK_Project { get; set; }
            public long FK_ProjectStages { get; set; }
            public string Project { get; set; }
            public string Stage { get; set; }
            public List<ResourceTypeDetailsview> ResourceTypeDetails { get; set; }
            public decimal PrmTotalAmount { get; set; }
            public long FK_ProjectResourceMarking { get; set; }
            public string TransMode { get; set; }
            public long ReasonID { get; set; }
          public long? LastID { get; set; }
        }

        public class ResourceDetailsListModel
        {
            public List<ProjectResourceList> ResourceList { get; set; }
            public List<StageList> StageList { get; set; }
            public List<ResourceTypeDetailsview> ResourceTypeDetails { get; set; }


        }
        public class StageList
        {
            public string Mode { get; set; }
            public long StageID { get; set; }
            public string StageName { get; set; }
        }
        public class ProjectResourceList
        {
          
            public long FK_ProjectResources { get; set; }
            public string ProjectResourcesName { get; set; }
        }
        public class ResourceTypeDetailsview
        {
            private string _ResourceAmount;
            

            public long FK_ProjectResourceMarking { get; set; }
            public long FK_ProjectResources { get; set; }
            public string Quantity { get; set; }
            public string ResourceAmount { get { return String.Format("{0:0.00}", this._ResourceAmount);  } set { this._ResourceAmount = (value is null) ? "0" : value; } }  
       
           
                       
        }
        public class UpdateResourseType
        {

                public long? LastID { get; set; }
                public long ID_ProjectResourceMarking { get; set; }
                public long FK_Project { get; set; }
                public long FK_ProjectStages { get; set; }
               public DateTime PrmMarkingDate { get; set; }        
               
                public long FK_ProjectResourceMarking { get; set; }
                public long FK_Company { get; set; }
                public long FK_BranchCodeUser { get; set; }
                public string EntrBy { get; set; }
                public long FK_Machine { get; set; }
                public byte UserAction { get; set; }
                public string TransMode { get; set; }
                public long Debug { get; set; }

                public string ResourceTypeDetails { get; set; }
                public  decimal PrmTotalAmount { get; set; }


        }
        public class ResourceTypeID
        {
            public Int64 FK_ProjectResourceMarking { get; set; }
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
        public class ResourceSelectDetails
        {
            public long SlNo { get; set; }
            public long FK_ProjectResourceMarking { get; set; }
            public long FK_Project { get; set; }
            public string Project { get; set; }
            public DateTime PrmMarkingDate { get; set; }          
            public long FK_ProjectStages { get; set; }          
            public string Stage { get; set; }
            public Int64 TotalCount { get; set; }
            public long ID_ProjectResourceMarking { get; set; }
            public long? LastID { get; set; }
        }

        public class DeleteResourceType
        {
            public long FK_ProjectResourceMarking { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Reason { get; set; }
        }

        public class ResourcrgridView
        {
            private string _ResourceAmount;
            public long FK_ProjectResourceMarking { get; set; }
            public long FK_ProjectResources { get; set; }
            public string Quantity { get; set; }
            public string ResourceAmount { get { return String.Format("{0:0.00}", this._ResourceAmount); } set { this._ResourceAmount = (value is null) ? "0" : value; } }

            public string ProjectResources { get; set; }
        }

        public Output UpdateResource(UpdateResourseType input, string companyKey)
        {
            return Common.UpdateTableData<UpdateResourseType>(parameter: input, companyKey: companyKey, procedureName: "ProProjectResourceMarkingUpdate");
        }
        public APIGetRecordsDynamic<ResourceSelectDetails>GetResourceTypeData(ResourceTypeID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ResourceSelectDetails, ResourceTypeID>(companyKey: companyKey, procedureName: "ProProjectResourceMarkingSelect", parameter: input);
        }
        public APIGetRecordsDynamic<ResourceDetailsView>GetResourceData(ResourceTypeID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ResourceDetailsView, ResourceTypeID>(companyKey: companyKey, procedureName: "ProProjectResourceMarkingSelect", parameter: input);
        }
        public APIGetRecordsDynamic<ResourcrgridView> GetRenewalSelectDatanew(ResourceTypeID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ResourcrgridView, ResourceTypeID>(companyKey: companyKey, procedureName: "ProProjectResourceMarkingSelect", parameter: input);

        }
        public Output DeleteResourceTypeData(DeleteResourceType input, string companyKey)
        {
            return Common.UpdateTableData<DeleteResourceType>(parameter: input, companyKey: companyKey, procedureName: "ProProjectResourceMarkingDelete");
        }



    }



}