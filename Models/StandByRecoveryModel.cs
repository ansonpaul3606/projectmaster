using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class StandByRecoveryModel
    {
        public class StandByRecoveryView
        {
            public DateTime StbyRecoveryDate { get; set; }
            public long FK_Customer { get; set; }

        }
        public class DropdownList
        {
            public List<DepartmentList> DepartmentList { get; set; }
            public DateTime StbyRecoveryDate { get; set; }
            public long FK_Customer { get; set; }
        }
        public class InsertInput
        {
            public long FK_Customer { get; set; }
            public DateTime StbyRecoveryDate { get; set; }
            public string TransMode { get; set; }
            public DateTime DeliveryDate { get; set; }
            public string DeliveryTime { get; set; }
            public long FK_Employee { get; set; }
            public string VehicleDet { get; set; }
            public string ProdRecNarration { get; set; }
            public List<StandbyRecoveryDetail> StandbyRecoveryDetails { get; set; }
            public List<ReplaceProductDetailsList> ReplaceProductDetails { get; set; }
            public long? LastID { get; set; }
        }

        public class DepartmentList
        {
            public long DepartmentID { get; set; }
            public string Department { get; set; }

        }

        public class ReplaceProductDetailsList
        {

            public string ActualProduct { get; set; }
            public long Fk_ActProduct { get; set; }
            public string ReplaceProduct { get; set; }
            public long FK_ReplaceProduct { get; set; }
            public decimal ReplaceQuantity { get; set; }
            public long UID { get; set; }
            public long stockid { get; set; }

        }

        public class StandbyRecoveryDetail
        {
            public long StandByProductID { get; set; }
            public long ActualProductID { get; set; }
            public string StProdName { get; set; }
            public string ActualProduct { get; set; }
            public string SalBillDate { get; set; }
            public string SalBillNo { get; set; }
            public decimal CWPDSalQuantity { get; set; }
            public decimal CWPDSalActualQuantity { get; set; }
            public long FK_Sales { get; set; }

            private string remark; // Private field to store the value
            public string Remark
            {
                get { return remark; }
                set { remark = value == null ? "" : value; } // Set to "" if the value is null }
            }
            public long UID { get; set; }
            public long ActIDStock { get; set; }
            public long FK_StandByStock { get; set; }




        }
        public class StandbyStockDataView
        {
            public long ActualProductID { get; set; }
            public string ActualProduct { get; set; }
            public long StandByProductID { get; set; }
            public string StandByProduct { get; set; }
            public string SalBillNo { get; set; }
            public string SalBillDate { get; set; }
            public long ActualStockID { get; set; }
            public long FK_StandByStock { get; set; }
            public decimal SpdSalActualQuantity { get; set; }
            public decimal StandByQuantity { get; set; }
            public long FK_Sales { get; set; }

        }

        public class UpdateStandbyRecovery
        {
            public long ID_StandbyRecovery { get; set; }
            public int UserAction { get; set; }
            public string TransMode { get; set; }
            public int Debug { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public DateTime StbyRecoveryDate { get; set; }

            public string StandbyRecoveryDetails { get; set; }
            public string ReplaceProductDetails { get; set; }
            public DateTime DeliveryDate { get; set; }
            public string DeliveryTime { get; set; }
            public long FK_Employee { get; set; }
            public string VehicleDet { get; set; }
            public string ProdRecNarration { get; set; }
            public long? LastID { get; set; }
            public long FK_Customer { get; set; }
        }


        public class StandbyRecoveryInput
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
            //public long FK_ProductRecovery { get; set; }
            public long GroupID { get; set; }
        }

        public class StandbyRecoveryOutput
        {

            public long ID { get; set; }

            public long TotalCount { get; set; }
            public long FK_Customer { get; set; }
            public DateTime StbyRecoveryDate { get; set; }
            public string TransMode { get; set; }
            public DateTime DeliveryDate { get; set; }
            public string DeliveryTime { get; set; }
            public long FK_Employee { get; set; }
            public string CusName { get; set; }
            public long GroupID { get; set; }

            public string StandbyRecRemark { get; set; }

        }
        public class DeleteInput
        {
            public long ReasonID { get; set; }
            public string TransMode { get; set; }
            public long StandbyRecGroupID { get; set; }
        }
        public class DeleteStandRecovery
        {
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public long StandbyRecGroupID { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
        }

        public class StandbyRecoveryOutputGrid
        {

            public long ID_StandbyRecovery { get; set; }
            public DateTime StbyRecoveryDate { get; set; }
            public string CusName { get; set; }
            public long FK_Customer { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Sales { get; set; }
            public long FK_Product { get; set; }
            public string ReplaceProduct { get; set; }
            public long FK_StandByProduct { get; set; }
            public string StandByProduct { get; set; }
            public decimal StandbyQuantity { get; set; }
            public decimal ReplaceQuantity { get; set; }
            public decimal SpdSalActualQuantity { get; set; }
            public string SalBillNo { get; set; }
            public string SalBillDate { get; set; }
            public string StandbyRemark { get; set; }
            public string StandbyRecRemark { get; set; }
            public long FK_Stock { get; set; }
            public long FK_StandByStock { get; set; }
            public long GroupID { get; set; }
            public string TransMode { get; set; }
            public string PdVehicleDetails { get; set; }
            public DateTime PdAssignedDate { get; set; }
            public string PdAssignedTime { get; set; }
            public string EmpName { get; set; }


        }

        public APIGetRecordsDynamic<StandbyStockDataView> GetStandbyStockDetails(StandByRecoveryView input, string companyKey)
        {
            return Common.GetDataViaProcedure<StandbyStockDataView, StandByRecoveryView>(companyKey: companyKey, procedureName: "ProGetStandByStockData", parameter: input);
        }

        public Output UpdateStandbyRecoverydetails(UpdateStandbyRecovery input, string companyKey)
        {
            return Common.UpdateTableData<UpdateStandbyRecovery>(parameter: input, companyKey: companyKey, procedureName: "ProStandbyRecoveryUpdate");
        }

        public APIGetRecordsDynamic<StandbyRecoveryOutput> GetStandRecoList(StandbyRecoveryInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<StandbyRecoveryOutput, StandbyRecoveryInput>(companyKey: companyKey, procedureName: "ProStandbyRecoverySelect", parameter: input);
        }

        public APIGetRecordsDynamic<StandbyRecoveryOutputGrid> GetProRecoveryGrid(StandbyRecoveryInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<StandbyRecoveryOutputGrid, StandbyRecoveryInput>(companyKey: companyKey, procedureName: "ProStandbyRecoverySelect", parameter: input);
        }
        public Output DeletePStandbyRecoveryData(DeleteStandRecovery input, string companyKey)
        {
            return Common.UpdateTableData<DeleteStandRecovery>(parameter: input, companyKey: companyKey, procedureName: "ProStandbyRecoveryDelete");
        }
    }
}