using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ServiceBookingModel
    {
        public class ServiceBookingView
        {
            public List<ServiceTypeList> serviceTypeLists { get; set; }
            public long? LastID { get; set; }
        }

        public class ServiceTypeList
        {
            public short TypeID { get; set; }
            public string TypeName { get; set; }
        }

        public class ServiceBookingViewList
        {
            public string TransMode { get; set; }
            public long TotalCount { get; set; }
            public long ID_ServiceBooking { get; set; }
            public string SerBookingNo { get; set; }
            public DateTime SerBookingDate { get; set; }
            public long FK_EquipmentServiceType { get; set; }
            public DateTime SerServiceDate { get; set; }
            public string SerServiceTime { get; set; }
            public string SerServiceCentre { get; set; }
            public string SerBookingDescription { get; set; }
            public long FK_Master { get; set; }
            public long SerPickDel { get; set; }
            public long? LastID { get; set; }

        }

        public class UpdateServiceBook
        {


            public long ID_ServiceBooking { get; set; }
            public string SerBookingNo { get; set; }
            public DateTime SerBookingDate { get; set; }
            public long FK_EquipmentServiceType { get; set; }
            public DateTime SerServiceDate { get; set; }
            public string SerServiceTime { get; set; }
            public string SerServiceCentre { get; set; }
            public string SerBookingDescription { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long UserAction { get; set; }
            public string TransMode { get; set; }
            public long FK_Master { get; set; }
            public long Debug { get; set; }
            public long FK_ServiceBooking { get; set; }
            public long SerPickDel { get; set; }
            public long? LastID { get; set; }
        }

        public class ServBookViewInput
        {
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_ServiceBooking { get; set; }

        }

        public class ServiceViewOutput
        {
            public long SlNo { get; set; }
            public long ID_ServiceBooking { get; set; }
            public string TransMode { get; set; }
            public long TotalCount { get; set; }
            public long FK_Master { get; set; }
            public string SerBookingNo { get; set; }
            public DateTime SerBookingDate { get; set; }
            public long FK_EquipmentServiceType { get; set; }
            public DateTime SerServiceDate { get; set; }
            public string SerServiceTime { get; set; }
            public string SerServiceCentre { get; set; }
            public string SerBookingDescription { get; set; }
            public string Name { get; set; }
            public string Service { get; set; }
            public int SerPickDel { get; set; }
            public long? LastID { get; set; }

        }
        public class DeleteInput
        {
            public long ReasonID { get; set; }
            public string TransMode { get; set; }
            public long ID_ServiceBooking { get; set; }
        }

        public class DeleteServiceBook
        {

            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public long FK_ServiceBooking { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }


        }

        public static string _updateProcedureName = "ProServiceBookingUpdate";
        public static string _deleteProcedureName = "ProServiceBookingDelete";

        public Output UpdateServiceBookingData(UpdateServiceBook input, string companyKey)
        {
            return Common.UpdateTableData<UpdateServiceBook>(companyKey: companyKey, procedureName: _updateProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<ServiceViewOutput> GetServiceBooklist(ServBookViewInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ServiceViewOutput, ServBookViewInput>(companyKey: companyKey, procedureName: "ProServiceBookingSelect", parameter: input);
        }
        public Output DeleteServiceBookData(DeleteServiceBook input, string companyKey)
        {
            return Common.UpdateTableData<DeleteServiceBook>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
    }
}