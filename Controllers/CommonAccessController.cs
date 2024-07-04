using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using PerfectWebERP.General;

namespace PerfectWebERP.Controllers
{
    public class CommonAccessController : Controller
    {
        #region [Index]
        public ActionResult Index(string ModeId)
        {
            string jsonString_decode = Base64UrlEncoder.Decode(ModeId);
            CommonAccessModel.EncodeUrl moduleinput = JsonSerializer.Deserialize<CommonAccessModel.EncodeUrl>(jsonString_decode);
            CommonAccessModel model = new CommonAccessModel();
            var _companyKey = ConfigurationManager.AppSettings["companyKey"];

            var Outdata = model.GetFeedbackDetails(new CommonAccessModel.InputTable
            {
                ModeId = moduleinput.ModeId,
                FK_Company = moduleinput.FK_Company,

            }, companyKey: _companyKey);

            CommonAccessModel.OutputLoadCustomerFeedback vueOutput = new CommonAccessModel.OutputLoadCustomerFeedback {
                FeedbackDetails = Outdata.Data,
            };

            return View(vueOutput);
        }
        #endregion

        #region[GetFeedbackDetails]
        [HttpPost]
        public ActionResult GetFeedbackDetails(CommonAccessModel.Moduleinput input)
        {
            CommonAccessModel model = new CommonAccessModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var Url = Common.GetDataViaQuery<CommonAccessModel.InputTable>(parameters: new APIParameters
            {
                TableName = "CommonBaseUrl",
                SelectFields = "ID_CommonBaseUrl as ID_CommonBaseUrl,Type as Type, BaseUrl as BaseUrl,IdType as IdType",
                Criteria = "cancelled=0  AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);

            var TypeDta = "";
            var baseUrl = "";
            if (Url.Data != null)
            {
                foreach (var item in Url.Data)
                {
                    if (item.IdType==101)
                    {
                        TypeDta = item.Type;
                        baseUrl = item.BaseUrl;
                        break;
                    }
                }
            }

            CommonAccessModel.EncodeUrl cmnAcees = new CommonAccessModel.EncodeUrl()
            {
                ModeId = input.ModeId,// based on the feedquestion settings module- for sale its 2.
                FK_Company = _userLoginInfo.FK_Company,
                CompanyCode = _userLoginInfo.CompanyKey,
                Type = TypeDta,//which type like Feedback,Invoice etc.
                ID_Module = input.ID_Module//id for the module like saleid, invoiceid

            };
            string JsonSer = JsonSerializer.Serialize(cmnAcees);
            string jsonString_encode = Base64UrlEncoder.Encode(JsonSer);

            if (!string.IsNullOrEmpty(baseUrl))
            {
                var returnUrl = $"{baseUrl}Common/FeedbackDetails/{(jsonString_encode)}";
               
                var result = model.SendDataFeedback(new CommonAccessModel.updateInput
                {
                    ID_Module = input.ID_Module,
                    customerId = input.customerId,
                    MobNo = input.MobNo,
                    ModeId = input.ModeId,
                    returnUrl = returnUrl,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser

                }, companyKey: _userLoginInfo.CompanyKey);

                return Json(new { result });
            }
            return Json(new { error = "baseUrl is empty" });
        }

        #endregion
    }
}
