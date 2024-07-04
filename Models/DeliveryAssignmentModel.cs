using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class DeliveryAssignmentModel
    {
        public class DeliveryAssignmentview
        {
            public long ID_DeliveryAssignment { get; set; }
            public long SlNo { get; set; }
            public long ddlImportsList { get; set; }
            public string DAReferenceNo { get; set; }
            public DateTime DADate { get; set; }
            public string DACustomer { get; set; }
            public string Mobile { get; set; }
            public string Address { get; set; }
            public long EmployeeID { get; set; }
            public string Employee{ get; set; }
            public long VehicleID { get; set; }
            public string VehicleName { get; set; }
            public string EwayBillNo { get; set; }

            public bool AddressCheck { get; set; }
            public string ShpContactName { get; set; }
            public string Address1 { get; set; }
            public string Place { get; set; }
            public string PinCode { get; set; }
            public long CountryID { get; set; }
            public long StatesID { get; set; }
            public long DistrictID { get; set; }
            public long AreaID { get; set; }
            public long PostID { get; set; }
            public string ShpMobile { get; set; }
            public long TransportType { get; set; }
            public string Vehicleno { get; set; }
            public string DrvName { get; set; }
            public string DrvPhoneno { get; set; }
            public DateTime DeliveryDate { get; set; }
            public string DeliveryTime { get; set; }
            public long TotalCount { get; set; }
            public long ReasonID { get; set; }
            public string TransMode { get; set; }
            

            public Int64? LastID { get; set; }
            public decimal Quantitycheck { get; set; }
            public long BranchID { get; set; }
            public long SalesID { get; set; }
            public long SalesOrderID { get; set; }

            public List<ProductDetails> ProductDetails { get; set; }
            public List<TransporttypeMode> TransporttypeModeList { get; set; }

            public long FK_Reason { get; set; }
        }
        public class DeliverySelectData
        {
            public long SlNo { get; set; }
            public long ID_ProductDelivery { get; set; }
            public string Customer { get; set; }
            public string MobileNo { get; set; }
            public DateTime DeliveryDate { get; set; }
            public DateTime? DeliveryTime { get; set; }

            public string Module { get; set; }
            public string TransMode { get; set; }
            public long TotalCount { get; set; }

            public long PdDeliveryMode { get; set; }
            public long ImportID { get; set; }
            public long FK_Master { get; set; }
            public long PdDeliveryType { get; set; }
            public long PdPriority { get; set; }
            public DateTime PdAssignedDate { get; set; }
            public string PdAssignedTime { get; set; }
            public long FK_Employee { get; set; }
            public string Employee { get; set; }
            public string PdVehicleDetails { get; set; }
            public long CSAssignStatus { get; set; }
            public long FK_Vehicle { get; set; }
            public string PdEWayBillNo { get; set; }

            public bool AddressCheck { get; set; }
            public string DelName { get; set; }
            public string DelAddress1 { get; set; }
            public string DelAddress2 { get; set; }
            public string PinCode { get; set; }
            public long FK_Country { get; set; }
            public string Country { get; set; }
            public long FK_State { get; set; }
            public string State { get; set; }
            public long FK_District { get; set; }
            public string District { get; set; }
            public long FK_Area { get; set; }
            public string Area { get; set; }
            public long FK_Post { get; set; }
            public string Post { get; set; }
            public string DelMobileNo { get; set; }
            public long DelTransportType { get; set; }
            public string DelVehicleNo { get; set; }
            public string DelDriverName { get; set; }
            public string DelDriverMobileNo { get; set; }
            public string ReferenceNo { get; set; }
            public DateTime BillDates { get; set; }

            public long ID_ProductDeliveryAssignDetails { get; set; }
            public long FK_Product { get; set; }
            public string Product { get; set; }
            public string Quantity { get; set; }
            public string SalesQuantity { get; set; }

            


        }
        public class TransporttypeMode
        {
            public long ID_Mode { get; set; }
            public string ModeName { get; set; }

        }
        public class ChangeModeInput
        {
            public int Mode { get; set; }

        }
        public class ProductDetails
        {
            public long ProductID { get; set; }
            public string Productname { get; set; }
            public decimal Quantity { get; set; }
            public decimal EnterQuantity { get; set; }
        }
        public class InputProductDetails
        {
            public long ImportID { get; set; }
            public long ID { get; set; }
            public long FK_Company { get; set; }
        }


        public class DeliveryUpdateInput
        {
            public long ID_ProductDelivery { get; set; }
            public long ImportID { get; set; }
            public long FK_Master { get; set; }
            public long FK_Vehicle { get; set; }
            public string Vehicleno { get; set; }
            public long FK_Employee { get; set; }
            public DateTime PdAssignedDate { get; set; }
            public string PdAssignedTime { get; set; }
            public string ProductDetails { get; set; }
            public bool AddressCheck { get; set; }
            public string ShpContactName { get; set; }
            public string Address1 { get; set; }            
            public string Place { get; set; }
            public long CountryID { get; set; } = 0;
            public long StatesID { get; set; } = 0;
            public long DistrictID { get; set; } = 0;
            public long AreaID { get; set; } = 0;
            public long PostID { get; set; } = 0;
            public string ShpMobile { get; set; }
            public long TransportType { get; set; }
            public long PdDeliveryType { get; set; }
            public long PdPriority { get; set; }
            public string DrvName { get; set; }
            public string DrvPhoneno { get; set; }
            public string EwayBillNo { get; set; }
            public long Debug { get; set; }
            public string TransMode { get; set; }
           
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public byte UserAction { get; internal set; }
            public long FK_Machine { get; set; }
        }
        public class GetDeliveryAssignment
        {
            public long FK_ProductDelivery { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            public string TransMode { get; set; }
            public int Detailed { get; set; }
        }
        public class DeleteDeliveryAssign
        {
            public long FK_ProductDelivery { get; set; }
            public long FK_Reason { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }

        }
        public APIGetRecordsDynamic<ProductDetails> GetProductDetails(InputProductDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductDetails, InputProductDetails>(companyKey: companyKey, procedureName: "ProGetProductInfoDeliveryAssignment", parameter: input);

        }
        public Output UpdateDeliveryAssignmentData(DeliveryUpdateInput input, string companyKey)
        {
            return Common.UpdateTableData<DeliveryUpdateInput>(parameter: input, companyKey: companyKey, procedureName: "ProProductDeliveryAssignUpdate");
        }
        public APIGetRecordsDynamic<DeliverySelectData> GetDeliveryAssignmentData(GetDeliveryAssignment input, string companyKey)
        {
            return Common.GetDataViaProcedure<DeliverySelectData, GetDeliveryAssignment>(companyKey: companyKey, procedureName: "ProProductDeliveryAssignSelect", parameter: input);
        }
        public APIGetRecordsDynamic<DeliverySelectData> GetDeliveryAssignmentDataById(GetDeliveryAssignment input, string companyKey)
        {
            return Common.GetDataViaProcedure<DeliverySelectData, GetDeliveryAssignment>(companyKey: companyKey, procedureName: "ProProductDeliveryAssignSelect", parameter: input);
        }

        public Output DeleteDeliveryData(DeleteDeliveryAssign input, string companyKey)
        {
            return Common.UpdateTableData<DeleteDeliveryAssign>(parameter: input, companyKey: companyKey, procedureName: "ProProductDeliveryAssignDelete");
        }
    }
}