using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class OtherChargesController : Controller
    {        
        [HttpPost]
        public ActionResult GetOtherCharges(OtherChargesModel.GetOtherCharge obj)
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  :
            OtherChargesModel Model = new OtherChargesModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var OtherChargeList = Model.GetOtherChargeList(input: new OtherChargesModel.GetOtherCharge
            {
                TransMode = obj.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Transaction = obj.FK_Transaction
            }, companyKey: _userLoginInfo.CompanyKey);
            var OtherChargeTaxList = Model.GetOtherChargeTaxDataList(input: new OtherChargesModel.GetOtherChargeTaxData
            {
                TransMode = obj.TransMode,               
                FK_Transaction = obj.FK_Transaction
            }, companyKey: _userLoginInfo.CompanyKey);
            
            List<pssOtherCharge> objSessionList = Common.GetOtherCharges(obj.TransMode);
            List<OtherChargesModel.OtherChargeList> objList = new List<OtherChargesModel.OtherChargeList>();
            if (OtherChargeList.Data != null)
            {
                if (OtherChargeList.Data.Count > 0)
                {                 
                    foreach (var data in OtherChargeList.Data.ToList())
                    {
                        OtherChargesModel.OtherChargeList objData = new OtherChargesModel.OtherChargeList();
                        objData.SlNo = data.SlNo.ToString();
                        objData.ID_OtherChargeType = data.ID_OtherChargeType;
                        objData.OctyName = data.OctyName;                      
                        objData.FK_TaxGroup = data.FK_TaxGroup;
                        objData.OctyTransType = data.OctyTransType;
                        objData.OctyTransTypeCus = data.OctyTransType;
                        objData.OctyAmount = data.OctyAmount;
                        objData.OctyTaxAmount = data.OctyTaxAmount; 
                        objData.OctyIncludeTaxAmount = data.OctyIncludeTaxAmount;
                        objData.OctyTransTypeActive = data.OctyTransTypeActive;
                        objData.OctranRemarks = data.OctranRemarks;
                        if (objSessionList!=null)
                        {
                            foreach(var row in objSessionList)
                            {
                                if(row.ID_OtherChargeType== data.ID_OtherChargeType)
                                {
                                    objData.OctyTransTypeCus = row.OctyTransType;

                                    objData.OctyAmount = row.OctyAmount;
                                    objData.OctyTaxAmount = row.OctyTaxAmount;
                                    objData.OctyIncludeTaxAmount = row.OctyIncludeTaxAmount;
                                    objData.OctranRemarks = row.OctranRemarks;
                                }
                            }                            
                        }
                        objList.Add(objData);                       
                    }
                }
            }
            OtherChargeList.Data = objList;
            return Json(new { OtherChargeList, OtherChargeTaxList }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetOtherChargesTax(OtherChargesModel.GetOtherChargeTax obj)
        {
            #region ::  Check User Session to verifyLogin  ::
            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  :
            OtherChargesModel Model = new OtherChargesModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var OtherChargeTaxList = Model.GetOtherChargeTaxList(input: new OtherChargesModel.GetOtherChargeTax
            {
                Amount = obj.Amount,
                IncludeTax = obj.IncludeTax,
                FK_TaxGroup = obj.FK_TaxGroup,
                FK_Transaction = obj.FK_Transaction,
                TransMode=obj.TransMode,
                FK_OtherChargeType=obj.FK_OtherChargeType,
            }, companyKey: _userLoginInfo.CompanyKey);

                return Json(new { OtherChargeTaxList }, JsonRequestBehavior.AllowGet);
            }
        
        [HttpPost]
        public ActionResult UpdateOtherCharges(OtherChargesModel.UpdateOtherChargeList obj)
        {           

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            if(obj.OtherChargeList!=null)
            {
                string strOtherCharge = "pssOtherCharge-" + obj.TransMode;
                List<pssOtherCharge> lstOC = new List<pssOtherCharge>();
                foreach(var row in obj.OtherChargeList)
                {
                    pssOtherCharge lst = new pssOtherCharge();
                    lst.ID_OtherChargeType = row.ID_OtherChargeType;
                    lst.OctyTransType = row.OctyTransType;
                    lst.FK_TaxGroup = row.FK_TaxGroup;
                    lst.OctyAmount = row.OctyAmount;
                    lst.OctyTaxAmount = row.OctyTaxAmount;
                    lst.OctyIncludeTaxAmount = row.OctyIncludeTaxAmount;
                    lst.OctranRemarks = row.OctranRemarks;
                    lstOC.Add(lst);
                }                  
                Session[strOtherCharge] = lstOC;
            }
            if(obj.OtherChargeTaxList!=null)
            {
                string strOtherChargeTax = "pssOtherChargeTax-" + obj.TransMode;
                List<pssOtherChargeTax> lstOCT = new List<pssOtherChargeTax>();
                foreach (var row in obj.OtherChargeTaxList)
                {
                    pssOtherChargeTax lst = new pssOtherChargeTax();
                    lst.ID_OtherChargeType = row.ID_OtherChargeType;
                    lst.ID_TaxSettings = row.ID_TaxSettings;
                    lst.Amount = row.Amount;
                    lst.TaxPercentage = row.TaxPercentage;
                    lst.TaxGrpID = row.TaxGrpID;
                    lst.FK_TaxType = row.FK_TaxType;
                    lst.TaxTyName = row.TaxTyName;                    
                    lstOCT.Add(lst);
                }
                Session[strOtherChargeTax] = lstOCT;
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }        
    }
}