using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using PerfectWebERPAPI.Interface;
using PerfectWebERPAPI.DataAccess;
using Newtonsoft.Json;

namespace PerfectWebERPAPI.Business
{
    public class BlMobileNotification : IMobileNotification
    {
        // All variable 
        private string _BankKey { get; set; }
        private string _EntrBy { get; set; }
        private string _TransMode { get; set; }
        private string _FK_Company { get; set; }
        private string _FK_Machine { get; set; }
        private string _CustomerRoleID { get; set; }
        private string _CustomerID { get; set; }
        private string _ReceiverRole { get; set; }// to get the customer details from the procedure
        private string _ReceiverID { get; set; }// to get the customer details from the procedure
       /* private string _NotificationTitle { get; set; }
        private string _NotificationBody { get; set; }
       */
        /*
        private List<NotificationUserInfo> _ReceiverList { get; set; }
        private string _ReceiverListJSON { get; set; }
        */

        //userd to save the customer details
        private string _ID_User { get; set; }
        private string _User_Type { get; set; }
        private string _Device_ID { get; set; }
        private string _User_Token { get; set; }
        private string _Notification_Status_Json { get; set; }
        private string _Action { get; set; }



        //initi
        public BlMobileNotification()
        {
            Initialize();
        }

        public void Initialize()
        {
            _BankKey = string.Empty;
            _EntrBy = string.Empty;
            _TransMode = string.Empty;
            _FK_Company = string.Empty;
            _FK_Machine = string.Empty;
            _CustomerRoleID = string.Empty;
            _CustomerID = string.Empty;
            _ReceiverRole = string.Empty;
            _ReceiverID = string.Empty;
          /*  _NotificationTitle = string.Empty;
            _NotificationBody = string.Empty;
          */
           /*
            _ReceiverList = null;
            _ReceiverListJSON = string.Empty;
            */
            _ID_User = string.Empty;
            _User_Type = string.Empty;
            _Device_ID = string.Empty;
            _User_Token = string.Empty;
            _Notification_Status_Json = string.Empty;
            _Action = string.Empty;
        }


        //Getters And Setters
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
        public string FK_Company
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Company); }
            set { _FK_Company = value; }
        }
        public string FK_Machine
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Machine); }
            set { _FK_Machine = value; }
        }
        public string CustomerRoleID
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CustomerRoleID); }
            set { _CustomerRoleID = value; }
        }
        public string CustomerID
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CustomerID); }
            set { _CustomerID = value; }
        }
        public string ReceiverRole
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ReceiverRole); }
            set { _ReceiverRole = value; }
        }
        public string ReceiverID
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ReceiverID); }
            set { _ReceiverID = value; }
        }
     /*   public string NotificationTitle
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_NotificationTitle); }
            set { _NotificationTitle = value; }
        }
        public string NotificationBody
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_NotificationBody); }
            set { _NotificationBody = value; }
        }
     */
        /*
        public List<NotificationUserInfo> ReceiverList
        {
            get { return _ReceiverList; }
            set { _ReceiverList = value; }
        }
        public class NotificationUserInfo {
            public int UserType { get; set; }
            public long UserID { get; set; }
        }
        public string ReceiverListJSON
        {
            get { return (ReceiverList is null || ReceiverList.Count==0)?"[]":BlDecryptFormat.DecryptDataForAuthCode(JsonConvert.SerializeObject(ReceiverList)); }
        }
        */


        public string ID_User
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_User); }
            set { _ID_User = value; }
        }
        public string User_Type
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_User_Type); }
            set { _User_Type = value; }
        }
        public string Device_ID
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Device_ID); }
            set { _Device_ID = value; }
        }
        public string User_Token
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_User_Token); }
            set { _User_Token = value; }
        }
        public string Notification_Status_Json
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Notification_Status_Json); }
            set { _Notification_Status_Json = value; }
        }
        public string Action
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Action); }
            set { _Action = value; }
        }


        // create the function

        public SendNotification BlSendNotification(BlMobileNotification objbl)
        {
            DalMobileNotification objDal = new DalMobileNotification();
            DataTable dt = objDal.DalSendNotification(objbl);
            objDal = null;


            return BlMobileNotificationFormat.ConvertToSendNotification(dt);
        }
        /*
        public FireBaseInfoList BlGetFirebaseInfo(BlMobileNotification objbl)
        {
            DalMobileNotification objDal = new DalMobileNotification();
            DataSet data = objDal.DalGetNotificationSettings(objbl);

            FireBaseInfoList infoList = new FireBaseInfoList();
            objDal = null;

            infoList= BlMobileNotificationFormat.ConvertToFireBaseInfo(data);

            return infoList;
        }
        */
        public SaveFireBaseSetings BlSaveCustomerToken(BlMobileNotification objbl)
        {
            DalMobileNotification objDal = new DalMobileNotification();
            DataTable data = objDal.DalSaveCustomerFirebaseInfo(objbl);

            SaveFireBaseSetings infoList = new SaveFireBaseSetings();
            objDal = null;

            infoList = BlMobileNotificationFormat.ConvertToSaveFireBaseSetings(data);

            return infoList;
        }
        public FireBaseInfoList BlGetFirebaseNotifications(BlMobileNotification objbl)
        {
            DalMobileNotification objDal = new DalMobileNotification();
            objbl.Action = "0";
            DataSet data = objDal.DalUpdateNotificationStatus(objbl);

            FireBaseInfoList infoList = new FireBaseInfoList();
            objDal = null;

            infoList = BlMobileNotificationFormat.ConvertToFireBaseInfo(data);

            return infoList;
        }
        public CommonAPIR BlUpdateNotificationStatus(BlMobileNotification objbl)
        {
            DalMobileNotification objDal = new DalMobileNotification();
            objbl.Action = "1";
            DataSet data = objDal.DalUpdateNotificationStatus(objbl);

            CommonAPIR infoList = new CommonAPIR();
            objDal = null;

            infoList = BlMobileNotificationFormat.ConvertToResponse(data);

            return infoList;
        }
    }
}
