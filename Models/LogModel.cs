using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class LogModel
    {
        public class LogListModel
        {
            public List<TypeList> TypeListdata { get; set; }
            public int SortOrder { get; set; }
            public List<ActionStatus> ActionStatusList { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
        }
        public class TypeList
        {
            public short TypeID { get; set; }
            public string TypeName { get; set; }
        }
        public class VehicleAndToolLogView
        {
            public long ID_VehicleAndToolLog { get; set; }
            public long Employee { get; set; }

            public long Vehicle { get; set; }

            public DateTime VtlLogStartDate { get; set; }

            public string VtlLogStartTime { get; set; }

            public DateTime VtlLogEndDate { get; set; }

            public string VtlLogEndTime { get; set; }
            public string VtlLogDescription { get; set; }

            public string VtlLogRemarks { get; set; }

            public decimal VtlLogTotalAmount { get; set; }

            public decimal VtlLogOtherCharges { get; set; }

            public decimal VtlLogNetAmount { get; set; }

            public string TransMode { get; set; }

            public decimal VtlogFuelAmount { get; set; }
            public long FK_FuelType { get; set; }
            public decimal VtlLogStartKm { get; set; }
            public decimal VtlLogEndKm { get; set; }
            public long TotalCount { get; set; }

            public List<MaintenanceDetails> MaintenanceDetails { get; set; }
            public List<OtherCharges> OtherChgDetails { get; set; }
            public List<PaymentDetails> PaymentDetail { get; set; }
            public DateTime VtlLogDate { get; set; }
            public long? LastID { get; set; }
           
        }
        public class PaymentDetails
        {
            public long SlNo { get; set; }
            public Int32 PaymentMethod { get; set; }
            public string Refno { get; set; }
            public decimal PAmount { get; set; }

        }

        public class MaintenanceDetails
        {

            public long FK_Maintenance { get; set; }
            public decimal VtlDetAmount { get; set; }
            public decimal VtlDetTaxAmount { get; set; }
            public decimal VtlDetNetAmount { get; set; }
            public string VtlDetRemarks { get; set; }

        }
        public class BindOtherCharge
        {
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }

        }


        public class VtLogViewInput
        {

            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public string Name { get; set; }
            public byte Detailed { get; set; }

            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_VehicleAndToolLog { get; set; }
        }

        public class UpdateVehicleAndToolLog
        {

            public long FK_Employee { get; set; }
            public long FK_Vehicle { get; set; }
            public DateTime VtlLogStartDate { get; set; }
            public string VtlLogStartTime { get; set; }
            public DateTime VtlLogEndDate { get; set; }
            public string VtlLogEndTime { get; set; }
            public string VtlLogDescription { get; set; }
            public string VtlLogRemarks { get; set; }
            public decimal VtlLogTotalAmount { get; set; }
            public decimal VtlLogOtherCharges { get; set; }
            public decimal VtlLogNetAmount { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public long UserAction { get; set; }
            public string TransMode { get; set; }
            public long ID_VehicleAndToolLog { get; set; }
            public string MaintenanceDetails { get; set; }
            //public decimal VtlogFuelAmount { get; set; }
            //public long FK_Fuel { get; set; }
            public decimal VtLogStartKm { get; set; }
            public decimal VtLogEndKm { get; set; }
            public string OtherChDetails { get; set; }
            public string TaxDetails { get; set; }
            public string PaymentDetail { get; set; }
            public DateTime VtlLogDate { get; set; }
            public long? LastID { get; set; }
        }

        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public class VtLogViewOutput
        {
            public long SlNo { get; set; }
            public long ID_VehicleAndToolLog { get; set; }
            public string TransMode { get; set; }
            public long FK_Employee { get; set; }
            public string Employee { get; set; }
            public string Vehicle { get; set; }
            public long FK_Vehicle { get; set; }
            public DateTime VtLogStartDate { get; set; }
            public string VtLogStartTime { get; set; }
            public DateTime VtLogEndDate { get; set; }
            public string VtLogEndTime { get; set; }
            public decimal VtlogStartKm { get; set; }
            public decimal VtlogEndKm { get; set; }
            public long VtlogFuelType { get; set; }
            public string VtLogDescription { get; set; }
            public string VtLogRemarks { get; set; }
            public decimal VtLogTotalAmount { get; set; }
            public decimal VtLogFuelAmount { get; set; }
            public decimal VtLogOtherCharges { get; set; }
            public decimal VtLogNetAmount { get; set; }
            public long TotalCount { get; set; }

            public long FK_VehicleLog { get; set; }
            public long FK_Maintenance { get; set; }
            public string Maintenance { get; set; }
            public decimal VtlDetAmount { get; set; }
            public decimal VtlDetTaxAmount { get; set; }
            public decimal VtlDetNetAmount { get; set; }
            public string VtlDetRemarks { get; set; }
            public DateTime VtlLogDate { get; set; }
            public long? LastID { get; set; }

            //public List<MaintenanceDetails> MaintenanceDetails { get; set; }
        }

        public class GetMaintenenaceGrid
        {
            public long FK_VehicleLog { get; set; }
            public long FK_Maintenance { get; set; }
            public string Maintenance { get; set; }
            public string VtlDetAmount { get; set; }
            public string VtlDetTaxAmount { get; set; }
            public string VtlDetNetAmount { get; set; }
            public string VtlDetRemarks { get; set; }
        }

        public class ActionStatus
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }

        public class OtherCharges
        {
            public long SlNo { get; set; }
            public long ID_OtherChargeType { get; set; }
            public Int64 OctyTransType { get; set; }
            public string OctyName { get; set; }
            public decimal OctyAmount { get; set; }
            public string TransType { get; set; }
            public Int64 TransTypeID { get; set; }
        }
        public class DeleteInput
        {
            public long ReasonID { get; set; }
            public string TransMode { get; set; }
            public long ID_VehicleAndToolLog { get; set; }
        }

        public class GetSubTableSales
        {
            public string Mode { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Transaction { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
        }

        public class deleteLog
        {

            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public long FK_VehicleAndToolLog { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }


        }
        public static string _updateProcedureName = "ProVehicleAndToolLogUpdate";
        public static string _deleteProcedureName = "ProVehicleAndToolLogDelete";




        public Output UpdateVehicleAndToolLogData(UpdateVehicleAndToolLog input, string companyKey)
        {
            return Common.UpdateTableData<UpdateVehicleAndToolLog>(companyKey: companyKey, procedureName: _updateProcedureName, parameter: input);
        }

        public APIGetRecordsDynamic<ActionStatus> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }

        public APIGetRecordsDynamic<VtLogViewOutput> GetLoglist(VtLogViewInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<VtLogViewOutput, VtLogViewInput>(companyKey: companyKey, procedureName: "ProVehicleAndToolLogSelect", parameter: input);
        }


        public APIGetRecordsDynamic<GetMaintenenaceGrid> GetMainteneceGrid(VtLogViewInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetMaintenenaceGrid, VtLogViewInput>(companyKey: companyKey, procedureName: "ProVehicleAndToolLogSelect", parameter: input);
        }


        public Output DeleteLogData(deleteLog input, string companyKey)
        {
            return Common.UpdateTableData<deleteLog>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<OtherCharges> GetOthrChargeDetails(GetSubTableSales input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherCharges, GetSubTableSales>(companyKey: companyKey, procedureName: "ProOthChgTransactionDetailsSelect", parameter: input);

        }

        public class GetPaymentin
        {

            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }

            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Master { get; set; }


        }


        public class EqlogReportInput
        {
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long ID_Report { get; set; }
            public long ID_FIELD { get; set; }
            public long EmployeeID { get; set; }
            public long FK_Maintenance { get; set; }

            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
        }


        public class EqlogReportOutput
        {

            public long FK_Maintenance { get; set; }
            public string Name { get; set; }
            public string EmpName { get; set; }
            public string MaintenanceName { get; set; }
            public string VtlDetRemarks { get; set; }
            public DateTime VtLogStartDate { get; set; }
            public DateTime VtLogEndDate { get; set; }
            public long FK_VehicleLog { get; set; }
            public decimal VtLogNetAmount { get; set; }
            public decimal VtlogStartKm { get; set; }
            public decimal VtlogEndKm { get; set; }
            public string VtLogDescription { get; set; }


        }
        public class LogViewTable
        {
            public long FK_VehicleLog { get; set; }
            public string Name { get; set; }
            public List<LogSubViewOutputTable> Logout { get; set; }
            public List<LogSubViewOutMaintanence> LogMaintain { get; }
        }

        public class LogSubViewOutMaintanence
        {
               public string MaintenanceName { get; set; }
               public string MaintenanceDescription { get; set; }
               public long FK_Maintenance { get; set; }

        }
        public class LogSubViewOutputTable
        {
            public long FK_Maintenance { get; set; }
            public string Name { get; set; }
            public string EmpName { get; set; }
            public string MaintenanceName { get; set; }
            public string VtlDetRemarks { get; set; }
            public string VtLogStartDate { get; set; }
            public string VtLogEndDate { get; set; }
            public long FK_VehicleLog { get; set; }
            public decimal VtLogNetAmount { get; set; }
            public decimal VtlogStartKm { get; set; }
            public decimal VtlogEndKm { get; set; }
            public string VtLogDescription { get; set; }
        }
        public class GetStKm
        {
            public long FK_Vehicle { get; set; }
            public decimal VtlogStartKm { get; set; }

        }
        public APIGetRecordsDynamic<PaymentDetails> GetPaymentselect(GetPaymentin input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentDetails, GetPaymentin>(companyKey: companyKey, procedureName: "ProTransactionDetailsSelect", parameter: input);

        }

        public APIGetRecordsDynamic<EqlogReportOutput> GetEqlogGeneralReport(EqlogReportInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<EqlogReportOutput, EqlogReportInput>(companyKey: companyKey, procedureName: "proEqlogeneralReportView", parameter: input);

        }

    }
}