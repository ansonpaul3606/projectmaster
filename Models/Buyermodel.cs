using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class Buyermodel
    {
        
            public class BuyerView
            {
            public long SlNo { get; set; }
            public long ID_Buyer { get; set; }            
            public string Name { get; set; }   
            public string Phone { get; set; }  
            public string Address { get; set; }        
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
            public long? PlaceID { get; set; }
            public string Place { get; set; }   
            public string GSTINNo { get; set; }
            public string PinCode { get; set; }

            }
      

            public class UpdateBuyer
            {
                public int UserAction { get; set; }
                public int Debug { get; set; }
                public string TransMode { get; set; }           
                public long FK_Company { get; set; }
                public long FK_BranchCodeUser { get; set; }
                public string EntrBy { get; set; }
                public long FK_Machine { get; set; }            
                public long ID_Buyer { get; set; }
                public long FK_Buyer { get; set; }
                  public string BuyerName { get; set; }               
                public string BuyerPhone { get; set; }             
                public string BuyerAddress { get; set; }
                public long FK_Country { get; set; }
                public long FK_States { get; set; }
                public long FK_District { get; set; }
                public long FK_Area { get; set; }
                public long FK_Post { get; set; }
                public long FK_Place { get; set; }
                public string BuyerGSTINNo { get; set; }
          
            
         
            
            }
        public class SearchPinModel
        {
            public string Country { get; set; }

            public long CountryID { get; set; }

            public string States { get; set; }

            public long StatesID { get; set; }

            public string District { get; set; }

            public long DistrictID { get; set; }

            public string Area { get; set; }

            public long? AreaID { get; set; }


            public string Post { get; set; }
            public long? PostID { get; set; }


            public string Place { get; set; }
            public long? PlaceID { get; set; }
        }
        public class Placelist
        {
            public long PlaceID { get; set; }
            public string Place { get; set; }


        }
        public class Countrylist
        {
            public int CountryID { get; set; }
            public string Country { get; set; }


        }
        public class Postlist
        {
            public long PostID { get; set; }
            public string Post { get; set; }

            public string PinCode { get; set; }

        }

        public class InputPlace
        {
            public long DistrictID { get; set; }
            public long PlaceID { get; set; }



        }
        public class Arealist
        {
            public long AreaID { get; set; }
            public string Area { get; set; }


        }

        public class Districtlist
        {
            public long DistrictID { get; set; }
            public string District { get; set; }


        }
        public class GSTINView
        {
            public string GSTINNo { get; set; }
            public long FK_Company { get; set; }

        }
        public class GetBuyerDetails
            {
                public Int64 FK_Buyer { get; set; }
                public Int64 PageIndex { get; set; }
                public Int64 PageSize { get; set; }
                public string EntrBy { get; set; }
                public string Name { get; set; }
                public Int64 FK_Company { get; set; }
                public Int64 FK_Machine { get; set; }
                public Int64 FK_BranchCodeUser { get; set; }

            }
            public class GetBuyerbyIdDetails
            {
                public Int64 FK_Buyer { get; set; }
                public string EntrBy { get; set; }
                public Int64 FK_Company { get; set; }
                public Int64 FK_Machine { get; set; }
                public Int64 FK_BranchCodeUser { get; set; }
            }
            public class BuyerSelectDetails
            {
             public long SlNo { get; set; }
                public long ID_Buyer { get; set; }
            public string BuyerName { get; set; }
            public string BuyerPhone { get; set; }
            public long FK_Country { get; set; }
            public string District { get; set; }
            public string Area { get; set; }
            public string Post { get; set; }          
            public string Country { get; set; }
            public string Place { get; set; }
            public long FK_States { get; set; }
            public string States { get; set; }
            public long FK_District { get; set; }
            public long FK_Area { get; set; }
            public long FK_Post { get; set; }
            public long FK_Place { get; set; }
            public string BuyerGSTINNo { get; set; }
            public Int64 TotalCount { get; set; }
            public string BuyerAddress { get; set; }
        }
            public class DeleteBuyer
        {
                public string TransMode { get; set; }
                public Int64 FK_Buyer { get; set; }
                public int Debug { get; set; }
                public string EntrBy { get; set; }
                public Int64 FK_Reason { get; set; }
                public Int64 FK_Company { get; set; }
                public Int64 FK_Machine { get; set; }
                public Int32 FK_BranchCodeUser { get; set; }
            }
            public class DeleteView
            {
                public long ID_Buyer { get; set; }
                public Int64 ReasonID { get; set; }
            }
        public class InputPincode
        {
            public long FK_Company { get; set; }
            public string Pincode { get; set; }
        }
        public Output UpdateBuyerData(UpdateBuyer input, string companyKey)
            {
                return Common.UpdateTableData<UpdateBuyer>(parameter: input, companyKey: companyKey, procedureName: "ProBuyerUpdate");
            }
            public APIGetRecordsDynamic<BuyerSelectDetails> GetBuyerSelect(GetBuyerDetails input, string companyKey)
            {
                return Common.GetDataViaProcedure<BuyerSelectDetails, GetBuyerDetails>(companyKey: companyKey, procedureName: "ProBuyerSelect", parameter: input);

            }
            public APIGetRecordsDynamic<BuyerSelectDetails> GetBuyerSelectData(GetBuyerbyIdDetails input, string companyKey)
            {
                return Common.GetDataViaProcedure<BuyerSelectDetails, GetBuyerbyIdDetails>(companyKey: companyKey, procedureName: "ProBuyerSelect", parameter: input);

            }
            public Output DeleteBuyerData(DeleteBuyer input, string companyKey)
            {
                return Common.UpdateTableData<DeleteBuyer>(parameter: input, companyKey: companyKey, procedureName: "ProBuyerDelete");
            }
        public APIGetRecordsDynamic<SearchPinModel> GetDetailsByPincodeData(InputPincode input, string companyKey)
        {
            return Common.GetDataViaProcedure<SearchPinModel, InputPincode>(companyKey: companyKey, procedureName: "proGetAddressByPin", parameter: input);
        }
        public APIGetRecordsDynamic<BuyerView> GetSupplierByGST(GSTINView input, string companyKey)
        {
            return Common.GetDataViaProcedure<BuyerView, GSTINView>(companyKey: companyKey, procedureName: "proSupplierGSTSelect", parameter: input);
        }

       
    }
    }




