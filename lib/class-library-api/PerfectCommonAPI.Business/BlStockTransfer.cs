using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfectWebERPAPI.Interface;
using System.Data;
using PerfectWebERPAPI.DataAccess;

namespace PerfectWebERPAPI.Business
{
  public class BlStockTransfer : IStockTransfer
    {
        #region Variable
        private string _ReqMode { get; set; }
        private string _FK_Company { get; set; }
        private string _Token { get; set; }
        private string _SubMode { get; set; }
        private string _BankKey { get; set; }
        private string _EntrBy { get; set; }
        private string _FK_BranchCodeUser { get; set; }
        private string _TransMode { get; set; }
        private string _Name { get; set; }
        private string _PageIndex { get; set; }
        private string _PageSize { get; set; }
        private string _Critrea1 { get; set; }
        private string _Critrea2 { get; set; }
        private string _Critrea3 { get; set; }
        private string _Critrea4 { get; set; }
        private string _ID { get; set; }
        private string _Critrea5 { get; set; }
        private string _Critrea6 { get; set; }
        private string _ID_StockTransfer { get; set; }
        private string _TransDate { get; set; }
        private string _FK_EmployeeFrom { get; set; }
        private string _FK_EmployeeTo { get; set; }
        private string _FK_BranchFrom { get; set; }
        private string _FK_BranchTo { get; set; }
        private string _FK_DepartmentFrom { get; set; }
        private string _FK_DepartmentTo { get; set; }
        private string _STRequest { get; set; }
        private string _FK_StockRequest { get; set; }
        private string _EmployeeStockTransferDetails { get; set; }
        private string _UserAction { get; set; }
        private string _FK_StockTransfer { get; set; }
        private string _Detailed { get; set; }
        private string _Reason { get; set; }
        private string _ID_User { get; set; }
        #endregion
        #region Constructor
        public BlStockTransfer()
        {
            Initialize();
        }
        #endregion
        #region Initialize
        public void Initialize()
        {
            _ReqMode = string.Empty;
            _FK_Company = string.Empty;
            _Token = string.Empty;
            _SubMode = string.Empty;
            _BankKey = string.Empty;
            _EntrBy = string.Empty;
            _TransMode = string.Empty;
            _FK_BranchCodeUser = string.Empty;
            _Name = string.Empty;
            _PageIndex = string.Empty;
            _PageSize = string.Empty;
            _Critrea1 = string.Empty;
            _Critrea2 = string.Empty;
            _Critrea3 = string.Empty;
            _Critrea4 = string.Empty;
            _ID = string.Empty;
            _Critrea5 = string.Empty;
            _Critrea6 = string.Empty;
            _ID_StockTransfer = string.Empty;
            _TransDate = string.Empty;
            _FK_EmployeeFrom = string.Empty;
            _FK_EmployeeTo = string.Empty;
            _FK_BranchFrom = string.Empty;
            _FK_BranchTo = string.Empty;
            _FK_DepartmentFrom = string.Empty;
            _FK_DepartmentTo = string.Empty;
            _STRequest = string.Empty;
            _FK_StockRequest = string.Empty;
            _EmployeeStockTransferDetails = string.Empty;
            _UserAction = string.Empty;
            _FK_StockTransfer = string.Empty;
            _Detailed = string.Empty;
            _Reason = string.Empty;
            _ID_User = string.Empty;
        }
        #endregion
        #region Getters And Setters
        public string ID_User
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_User); }
            set { _ID_User = value; }
        }
        public string ReqMode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ReqMode); }
            set { _ReqMode = value; }
        }
        public string FK_Company
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Company); }
            set { _FK_Company = value; }
        }
        public string Token
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Token); }
            set { _Token = value; }
        }
        public string SubMode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_SubMode); }
            set { _SubMode = value; }
        }
        public string BankKey
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_BankKey); }
            set { _BankKey = value; }
        }
        public string EntrBy
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_EntrBy); }
            set { _EntrBy = value; }
        }
        public string TransMode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TransMode); }
            set { _TransMode = value; }
        }
        public string FK_BranchCodeUser
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_BranchCodeUser); }
            set { _FK_BranchCodeUser = value; }
        }
        public string Name
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Name); }
            set { _Name = value; }
        }
        public string PageIndex
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_PageIndex); }
            set { _PageIndex = value; }
        }
        public string PageSize
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_PageSize); }
            set { _PageSize = value; }
        }
        public string Critrea1
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Critrea1); }
            set { _Critrea1 = value; }
        }
        public string Critrea2
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Critrea2); }
            set { _Critrea2 = value; }
        }
        public string Critrea3
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Critrea3); }
            set { _Critrea3 = value; }
        }
        public string Critrea4
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Critrea4); }
            set { _Critrea4 = value; }
        }

        public string ID
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID); }
            set { _ID = value; }
        }
        public string Critrea5
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Critrea5); }
            set { _Critrea5 = value; }
        }
        public string Critrea6
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Critrea6); }
            set { _Critrea6 = value; }
        }
        public string ID_StockTransfer
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_StockTransfer); }
            set { _ID_StockTransfer = value; }
        }
        public string TransDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TransDate); }
            set { _TransDate = value; }
        }
        public string FK_EmployeeFrom
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_EmployeeFrom); }
            set { _FK_EmployeeFrom = value; }
        }
        public string FK_EmployeeTo
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_EmployeeTo); }
            set { _FK_EmployeeTo = value; }
        }
        public string FK_BranchFrom
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_BranchFrom); }
            set { _FK_BranchFrom = value; }
        }
        public string FK_BranchTo
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_BranchTo); }
            set { _FK_BranchTo = value; }
        }
        public string FK_DepartmentFrom
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_DepartmentFrom); }
            set { _FK_DepartmentFrom = value; }
        }
        public string FK_DepartmentTo
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_DepartmentTo); }
            set { _FK_DepartmentTo = value; }
        }
       
        public string STRequest
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_STRequest); }
            set { _STRequest = value; }
        }
        public string FK_StockRequest
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_StockRequest); }
            set { _FK_StockRequest = value; }
        }
        public string EmployeeStockTransferDetails
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_EmployeeStockTransferDetails); }
            set { _EmployeeStockTransferDetails = value; }
        }
        public string UserAction
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_UserAction); }
            set { _UserAction = value; }
        }
        public string FK_StockTransfer
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_StockTransfer); }
            set { _FK_StockTransfer = value; }
        }
        public string Detailed
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Detailed); }
            set { _Detailed = value; }
        }
        public string Reason
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Reason); }
            set { _Reason = value; }
        }
        #endregion
        #region Data Access Function
        public StockRTEmployeeDetails BlStockRTEmployeeDetails(BlStockTransfer objbl)
        {
            DalStockTransfer objDal = new DalStockTransfer();
            DataTable dt = objDal.DalProERPCmnSearchPopup(objbl);
            objDal = null;
            return BlStockTransferFormats.ConvertStockRTEmployee(dt);
        }
        public StockRTProductDetails BlStockRTProductDetails(BlStockTransfer objbl)
        {
            DalStockTransfer objDal = new DalStockTransfer();
            DataTable dt = objDal.DalProERPCmnSearchPopup(objbl);
            objDal = null;
            return BlStockTransferFormats.ConvertStockRTProduct(dt);
        }
        public UpdateStockTransfer BlUpdateStockTransfer(BlStockTransfer objbl)
        {
            DalStockTransfer objDal = new DalStockTransfer();
            DataTable dt = objDal.DalStockTransferUpdate(objbl);
            objDal = null;
            return BlStockTransferFormats.ConvertUpdateStockTransfer(dt);
        }       
        public StockRequest BlStockRequestDetailsList(BlStockTransfer objbl)
        {
            DalStockTransfer objDal = new DalStockTransfer();
            DataTable dt = objDal.DalStockTransferSelect(objbl);
            objDal = null;
            return BlStockTransferFormats.ConvertStockRequestList(dt);
        }
        public StockRequestProduct BlStockRequestProductDetailsList(BlStockTransfer objbl)
        {
            DalStockTransfer objDal = new DalStockTransfer();
            DataTable dt = objDal.DalStockTransferSelect(objbl);
            objDal = null;
            return BlStockTransferFormats.ConvertStockRequestProductList(dt);
        }
        public StockRequestInTransfer BlStockRequestRequestListInTransfer(BlStockTransfer objbl)
        {
            DalStockTransfer objDal = new DalStockTransfer();
            DataTable dt = objDal.DalStockRequestSelectInTransfer(objbl);
            objDal = null;
            return BlStockTransferFormats.ConvertStockRequestInTransfer(dt);
        }
        public StockSTProductDetails BlStockSTProductDetails(BlStockTransfer objbl)
        {
            DalStockTransfer objDal = new DalStockTransfer();
            DataTable dt = objDal.DalProERPCmnSearchPopup(objbl);
            objDal = null;
            return BlStockTransferFormats.ConvertStockSTProduct(dt);
        }
        public StockRTDeleteDetails BlStockRTDeleteDetails(BlStockTransfer objbl)
        {
            DalStockTransfer objDal = new DalStockTransfer();
            DataTable dt = objDal.DalStockTransferDelete(objbl);
            objDal = null;
            return BlStockTransferFormats.ConvertDeleteStockTransfer(dt);
        }
        #endregion
    }
}
