using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class CustomerPortalImageModel
    {
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public class ImageMode
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }

        public class CustomerPortalImage
        {
            public List<ImageMode> ModeList { get; set; }            
        }
        public class SaveImages
        {
            public string CusportalTitle { get; set; }
            public string CusportalSubTitle { get; set; }
            public bool CusportalActive { get; set; }
            public DateTime? CusportalEffectFrom { get; set; }
            public DateTime? CusportalEffectTo { get; set; }
            public string ImageList { get; set; }
            public string CusportalRedirectTo { get; set; }
            public int CusportalMode { get; set; }
            public long ID_CusPortalSlider { get; set; }
        }
        public class UpdateImageData
        {
            public int UserAction { get; set; }
            public string CusportalTitle { get; set; }
            public string CusportalSubTitle { get; set; }
            public bool CusportalActive { get; set; }
            public DateTime? CusportalEffectFrom { get; set; }
            public DateTime? CusportalEffectTo { get; set; }
            public string ImageList { get; set; }
            //public byte[] ImageList { get; set; }
            public string CusportalRedirectTo { get; set; }
            public int CusportalMode { get; set; }
            public long FK_Company   { get; set; }
            public string  Entrby { get; set; }
            public long FK_CusPortalSlider { get; set; }
            public long ID_CusPortalSlider { get; set; }
        }

        public class ImageGetInput
        {
            public Int64 FK_CusPortalSlider { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            public int Imagedetails { get; set; }
        }
        public class ImageView
        {
            public long SlNo { get; set; }
            public long FK_CusPortalSlider { get; set; }
            public string CusportalTitle { get; set; }
            public string CusportalSubTitle { get; set; }
            public string CusportalImage { get; set; }
            public bool CusportalActive { get; set; }
            public string PortalActive { get; set; }
            public DateTime? CusportalEffectFrom { get; set; }
            public DateTime? CusportalEffectTo { get; set; }
            public string CusportalRedirectTo { get; set; }
            public int CusportalMode { get; set; }
            public string PortalMode { get; set; }
            public Int64 TotalCount { get; set; }
        }
        public class ImageSelectView
        {
            public long FK_CusPortalSlider { get; set; }            
        }
        public class ImageDetails
        {
            public string ImageData { get; set; }
            public long FK_CusPortalSlider { get; set; }
        }
        public APIGetRecordsDynamic<ImageMode> GetImageModeList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ImageMode, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public Output UpdateImagesData(UpdateImageData input, string companyKey)
        {
            return Common.UpdateTableData<UpdateImageData>(parameter: input, companyKey: companyKey, procedureName: "ProCusPortalImagesUpdate");
        }

        public APIGetRecordsDynamic<ImageView> GetImageData(ImageGetInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ImageView, ImageGetInput>(companyKey: companyKey, procedureName: "ProCusPortalSliderSelect", parameter: input);
        }
        public APIGetRecordsDynamic<ImageDetails> GetImageDataDetails(ImageGetInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ImageDetails, ImageGetInput>(companyKey: companyKey, procedureName: "ProCusPortalSliderSelect", parameter: input);
        }
    }
}