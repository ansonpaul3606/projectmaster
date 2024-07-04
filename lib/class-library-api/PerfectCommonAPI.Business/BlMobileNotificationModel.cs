using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfectWebERPAPI.Interface;

namespace PerfectWebERPAPI.Business
{
   public class BlMobileNotificationModel
    {
        public class RootSendNotification : CommonAPIResponse
        {
            public SendNotification SendNotificationResult { get; set; }
            public FireBaseInfoList GetNotificationSettings { get; set; }
        }
  

        /*
         {
            "registration_ids": ["token1", "token2", "token3"],
            "notification": {
            "title": "My Notification Title",
            "body": "This is the notification body.",
            "icon": "ic_notification",
            "sound": "default",
            "click_action": "my_activity"
            },
            "data": {
            "key1": "value1",
            "key2": "value2"
            }
        }


        {
          "to": "token1",
          "notification": {
            "title": "My Notification Title",
            "body": "This is the notification body.",
            "icon": "ic_notification",
            "sound": "default",
            "click_action": "my_activity"
          },
          "data": {
            "key1": "value1",
            "key2": "value2"
          }
        }
         
         */
        public class FirebaseAPIInputJson {
            public string to { get; set; }
            public List<string> registration_ids { get; set; }
           // public FirebaseAPIInputJson_notification notification { get; set; }
            public FirebaseAPIInputJson_notification data { get; set; }
        }

        public class FirebaseAPIInputJson_notification
        {
            public string title { get; set; }
            public string body { get; set; }
            public string icon { get; set; }
            public string sound { get; set; }
            public string click_action { get; set; }
        }
        public class FirebaseAPIInputJson_data
        {
            public string key1 { get; set; }
            public string key2 { get; set; }
        }


        // model to return response after saving the customer token info
        public class RootSaveCustomerToken: CommonAPIResponse
        {
            public SaveFireBaseSetings FireBaseSetingsInfo { get; set; }
        }

        public class FireBaseNotificationStatus
        {
            public long ID_PushNotificationData { get; set; }
            public string Response { get;set; }
        }
    }
}
