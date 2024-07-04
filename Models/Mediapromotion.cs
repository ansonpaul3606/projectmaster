using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class Mediapromotion
    {
        public class MediaView
        {
            public int ID_Media { get; set; }
            public string PrmName { get; set; }
            public DateTime PrmStartDate { get; set; }
            public DateTime PrmEndDate { get; set; }
            public decimal PrmAmount { get; set; }
            public long FK_Media { get; set; }
            public long FK_MediaMaster { get; set; }
            public long FK_MediaSubMaster { get; set; }
            public string PrmDescription { get; set; }
            public List<MediaList> MediaList { get; set; }
            public List<SubMediaList> SubMediaList { get; set; }
            public List<PaymentDetails> PaymentDetail { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
        }
        public class PaymentDetails
        {
            public long SlNo { get; set; }
            public Int32 PaymentMethod { get; set; }
            public string Refno { get; set; }
            public decimal PAmount { get; set; }

        }
        public class MediaPromotionList
        {
            public List<MediaList> MediaList { get; set; }
            public List<SubMediaList> SubMediaList { get; set; }
            public List<PaymentDetails> PaymentDetail { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
        }
        public class MediaList
        {
            public long FK_MediaMaster { get; set; }
            public string MediaName { get; set; }
        }
        public class SubMediaList
        {
            public long FK_MediaSubMaster { get; set; }
            public string SubMediaName { get; set; }
        }
        public class UpdateMedia
        {
            public int UserAction { get; set; }
            public int Debug { get; set; }
            public string TransMode { get; set; }           
            public int ID_Media { get; set; }
            public string PrmName { get; set; }
            public string PrmDescription { get; set; }
            public DateTime PrmStartDate { get; set; }
            public DateTime PrmEndDate { get; set; }
            public decimal PrmAmount { get; set; }
            public long FK_Media { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_MediaMaster { get; set; }
            public long FK_MediaSubMaster { get; set; }
            public string PaymentDetail { get; set; }
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
        public class GetMediaDetails
        {
            public Int64 FK_Media { get; set; }
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }

        }
        public class GetMediabyIdDetails
        {
            public Int64 FK_Media { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }
        public class MediaSelectDetails
        {
            public long SlNo { get; set; }
            public long ID_Media { get; set; }
            public string PrmName { get; set; }
            public string MediaName { get; set; }
            public string SubMediaName { get; set; }
            public long FK_MediaMaster { get; set; }
            public long FK_MediaSubMaster { get; set; }
            public DateTime PrmStartDate { get; set; }
            public DateTime PrmEndDate { get; set; }
            public string PrmDescription { get; set; }
            public decimal PrmAmount { get; set; }

            public Int64 TotalCount { get; set; }
           
        }
        public class DeleteMedia
        {
            public string TransMode { get; set; }
            public Int64 FK_Media { get; set; }
            public int Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
        }
        public class DeleteView
        {
            public long ID_Media { get; set; }
            public Int64 ReasonID { get; set; }
        }

        public Output UpdateMediaData(UpdateMedia input, string companyKey)
        {
            return Common.UpdateTableData<UpdateMedia>(parameter: input, companyKey: companyKey, procedureName: "ProMediaPromotionUpdate");
        }
        public APIGetRecordsDynamic<MediaSelectDetails> GetMediaSelect(GetMediaDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<MediaSelectDetails, GetMediaDetails>(companyKey: companyKey, procedureName: "ProMediaPromotionSelect", parameter: input);

        }
        public APIGetRecordsDynamic<MediaSelectDetails> GetMediaSelectData(GetMediabyIdDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<MediaSelectDetails, GetMediabyIdDetails>(companyKey: companyKey, procedureName: "ProMediaPromotionSelect", parameter: input);

        }
        public Output DeleteMediaData(DeleteMedia input, string companyKey)
        {
            return Common.UpdateTableData<DeleteMedia>(parameter: input, companyKey: companyKey, procedureName: "ProMediaPromotionDelete");
        }
        public APIGetRecordsDynamic<PaymentDetails> GetPaymentselect(GetPaymentin input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentDetails, GetPaymentin>(companyKey: companyKey, procedureName: "ProTransactionDetailsSelect", parameter: input);

        }
    }
}





       
       