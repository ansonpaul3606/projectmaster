using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class RentandReturnModel
    {
        public class RentReturnView
        {
            public List<ActionStatus> ActionStatusList { get; set; }
        }
        public class ActionStatus
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }

        public class RentRtnReportInput
        {
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public int FK_Mode { get; set; }
            public long ID_FIELD { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public string TransMode { get; set; }

        }

        public class RentRtnReportOutputView
        {

            public long SlNo { get; set; }
            public string EquRentType { get; set; }
            public string EquEquipmentNo { get; set; }
            public string EquRentalDate { get; set; }
            public decimal EquEquipmentDistance { get; set; }
            public decimal  EquSecurityAmount { get; set; }
            public decimal  EquRentAmount{ get; set; }
            public decimal EquTaxAmount { get; set; }
            public decimal EquReceivable { get; set; }
            public decimal EquPayable { get; set; }
            public string EquOperator { get; set; }
            public string EquOperatorMobile { get; set; }
            public int EquReturndays { get; set; }
            public string ReturnDate { get; set; }

        }
        public APIGetRecordsDynamic<ActionStatus> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }

        public APIGetRecordsDynamic<RentRtnReportOutputView> GetVehicleGeneralReport(RentRtnReportInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<RentRtnReportOutputView, RentRtnReportInput>(companyKey: companyKey, procedureName: "ProRptRentandReturnReport", parameter: input);

        }
    }
}