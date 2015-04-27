using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

//2. 以下の using を ToDoBroadcastReceiver クラスに追加します：
using Gcm.Client;
using Microsoft.WindowsAzure.MobileServices;
//2. ここまで

//3. permission のリクエストを using ステートメントと namespace 宣言の間に追加してください：
[assembly: Permission(Name = "<アプリのパッケージ名>.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "<アプリのパッケージ名>.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]

//GET_ACCOUNTS は Android 4.0.3 以下で必要です。
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]
//3. ここまで

namespace xamarin_mbaas
{
    //4. 既存の ToDoBroadcastReceiver クラスの定義を以下で置き換えます：
    [BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE },
        Categories = new string[] { "<アプリのパッケージ名>" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK },
        Categories = new string[] { "<アプリのパッケージ名>" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY },
        Categories = new string[] { "<アプリのパッケージ名>" })]

    // 4. 【注意】ドキュメントでは <TIntentService> が <GcmService> ですが、正しくは <PushHandlerService> です。
    public class ToDoBroadcastReceiver : GcmBroadcastReceiverBase<PushHandlerService>
    {
        // 「Google Cloud Messaging を有効にする」で控えておいた プロジェクト番号 を記述してください。
        public static string[] senderIDs = new string[] { "xxxxxxxxxxxx" };
    }
    //4. ここまで

    //5. ToDoBroadcastReceiver.cs 内に PushHandlerService クラスを定義する以下のコードを追加します：
    // このサービス定義はこのクラスに適用しなければいけません。
    [Service]
    public class PushHandlerService : GcmServiceBase
    {
        public static string RegistrationID { get; private set; }

        public PushHandlerService() : base(ToDoBroadcastReceiver.senderIDs) { }

        //6. PushHandlerService クラスで OnRegistered イベントハンドラーを override します。
        // 【注意】ドキュメントでは ToDoBroadcastReceiver クラスで override していますが、正しくは PushHandlerService クラス内です。
        protected override void OnRegistered(Context context, string registrationId)
        {
            System.Diagnostics.Debug.WriteLine("The device has been registered with GCM.", "Success!");

            // Get the MobileServiceClient from the current activity instance.
            MobileServiceClient client = ToDoActivity.CurrentActivity.CurrentClient;
            var push = client.GetPush();

            List<string> tags = null;

            //// (Optional) Uncomment to add tags to the registration.
            //var tags = new List<string>() { "myTag" }; // create tags if you want

            try
            {
                // Make sure we run the registration on the same thread as the activity, 
                // to avoid threading errors.
                ToDoActivity.CurrentActivity.RunOnUiThread(
                    async () => await push.RegisterNativeAsync(registrationId, tags));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    string.Format("Error with Azure push registration: {0}", ex.Message));
            }
        }
        //6. ここまで

        //7. PushHandlerService クラスで OnMessage メソッドを override します：
        protected override void OnMessage(Context context, Intent intent)
        {
            string message = string.Empty;

            // Extract the push notification message from the intent.
            if (intent.Extras.ContainsKey("message"))
            {
                message = intent.Extras.Get("message").ToString();
                var title = "アイテムが追加されました:";

                // Create a notification manager to send the notification.
                var notificationManager =
                    GetSystemService(Context.NotificationService) as NotificationManager;

                // Create a new intent to show the notification in the UI. 
                PendingIntent contentIntent =
                    PendingIntent.GetActivity(context, 0,
                    new Intent(this, typeof(ToDoActivity)), 0);

                // Create the notification using the builder.
                var builder = new Notification.Builder(context);
                builder.SetAutoCancel(true);
                builder.SetContentTitle(title);
                builder.SetContentText(message);
                builder.SetSmallIcon(Resource.Drawable.ic_launcher);
                builder.SetContentIntent(contentIntent);
                var notification = builder.Build();

                // Display the notification in the Notifications Area.
                notificationManager.Notify(1, notification);

            }
        }
        //7. ここまで

        //8. OnUnRegistered() と OnError() メソッドも override します
        protected override void OnUnRegistered(Context context, string registrationId)
        {
            throw new NotImplementedException();
        }

        protected override void OnError(Context context, string errorId)
        {
            System.Diagnostics.Debug.WriteLine(
                string.Format("Error occurred in the notification: {0}.", errorId));
        }
        //8. ここまで
    }



}