using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class EquipmentServiceTypeModel
    {

        public class ServiceTypeView
        {

            public long SlNo { get; set; }
            public string EqServiceName { get; set; }
            public string EqServiceShortName { get; set; }
            public string EqServiceDescription { get; set; }
            public long SortOrder { get; set; }
            public string Mode { get; set; }
            public long ID_EquipmentServiceType { get; set; }
            public long FK_EquipmentServiceType { get; set; }

            public List<ServiceMode> ServiceList { get; set; }
            public Int64 TotalCount { get; set; }

            
        }
        public class ServiceMode
        {
            public string ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }

        public class UpdateEqService
        {
            public long ID_EquipmentServiceType { get; set; }
            public string EqServiceName { get; set; }
            public string EqServiceShortName { get; set; }
            public string EqServiceDescription { get; set; }
            public string Mode { get; set; }
            public long SortOrder { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int UserAction { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public long FK_EquipmentServiceType { get; set; }
            public int Debug { get; set; }

        }

        public class GetServiceType
        {
            public Int64 FK_Company { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            public string EntrBy { get; set; }
            public long FK_EquipmentServiceType { get; set; }
            public string Name { get; set; }
        }

        public class DeleteServiceType
        {
            public string TransMode { get; set; }
            public long FK_EquipmentServiceType { get; set; }
            public int Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
        }
        public class EqServiceTypeId
        {
            public Int64 FK_EquipmentServiceType { get; set; }
            public Int64 ReasonID { get; set; }
        }
        public APIGetRecordsDynamic<ServiceMode> GetServiceModelist(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ServiceMode, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public Output UpdateEqServiceTypeData(UpdateEqService input, string companyKey)
        {
            return Common.UpdateTableData<UpdateEqService>(parameter: input, companyKey: companyKey, procedureName: "ProEquipmentServiceTypeUpdate");
        }

        public APIGetRecordsDynamic<ServiceTypeView> GetServiceTypeData(GetServiceType input, string companyKey)
        {
            return Common.GetDataViaProcedure<ServiceTypeView, GetServiceType>(companyKey: companyKey, procedureName: "ProEquipmentServiceTypeSelect", parameter: input);
        }

        public Output DeleteServiceTypeData(DeleteServiceType input, string companyKey)
        {
            return Common.UpdateTableData<DeleteServiceType>(parameter: input, companyKey: companyKey, procedureName: "ProEquipmentServiceTypeDelete");
        }


    }
}