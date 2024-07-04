using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWebERPAPI.Interface
{
    public interface IMobileNotification
    {
        string BankKey { get; set; }
        string EntrBy { get; set; }
        string TransMode { get; set; }
        string FK_Company { get; set; }
        string FK_Machine { get; set; }

        string CustomerRoleID { get; set; }
        string CustomerID { get; set; }

        string ReceiverRole { get; set; }
       
        string ReceiverID { get; set; }
       /*
        string ReceiverListJSON { get; }
       */

        //string UserToken { get; set; }
        //string DeviceKey { get; set; }

        string ID_User { get; set; }
        string User_Type { get; set; }
        string Device_ID { get; set; }
        string User_Token { get; set; }
        string Notification_Status_Json { get; set; }
        string Action { get; set; }



    }
    public class SendNotification : CommonAPIR
    {
        public FirebaseSettingsID FirebaseSettingsID { get; set; }
    }

    public class FireBaseInfoList : CommonAPIR
    {
        public string AuthorizationKey { get; set; }
        public string APIURL { get; set; }
        public List<FireBaseInfo> FireBaseInfo { get; set; }
    }

    public class FireBaseInfo
    {
        public long ID_PushNotificationData { get; set; }
        public string PnUserToken { get; set; }
        public string PnTitle { get; set; }
        public string PnBody { get; set; }
        public string PnImage { get; set; }
        public string PnURL { get; set; }
        public bool IsSend { get; set; }
        public string PnReferenceNo { get; set; }
    }

    public class SaveFireBaseSetings : CommonAPIR
    {
        public FirebaseSettingsID FirebaseSettingsID { get; set; }
    }
    public class FirebaseSettingsID {
        public string FK_FireBaseSettings { get; set; }
    }

}
