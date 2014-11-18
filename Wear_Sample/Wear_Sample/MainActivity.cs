using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Android.Support.V4.App;

namespace Wear_Sample
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate
            {
                // Wear の入力画面を構築
                // RemoteInput.Builder だと Android.RemoteInput と競合するのでフルで指定しています。
                // 正式な書き方は不明です。。
                var remoteInput = new Android.Support.V4.App.RemoteInput.Builder("Reply")
                .SetLabel(GetText(Resource.String.Reply))  // "Reply Label"
                .Build();
                // pendingIntent で起動する Activity
                var intent = new Intent(this, typeof(StartActionActivity));
                // Wear の Update を待つ処理
                var replyPendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.UpdateCurrent);
                // Wear でスワイプ (Extender？) した際のアクション
                var replyAction = new NotificationCompat.Action.Builder(
                    Android.Resource.Drawable.IcButtonSpeakNow,
                    GetText(Resource.String.Reply), replyPendingIntent)  // "リプライ"
                    .AddRemoteInput(remoteInput)
                    .Build();
                // Wear の Extender を構築
                var wealableExtender = new NotificationCompat.WearableExtender()
                .AddAction(replyAction);
                // Wear の Notification を構築
                var notificationBuilder = new NotificationCompat.Builder(this)
                .SetSmallIcon(Android.Resource.Drawable.IcDialogAlert)
                .SetContentTitle(GetText(Resource.String.AlertTitle))  // "アラート"
                .SetContentText(GetText(Resource.String.AlertMessage))  // "左にスワイプしてください"
                .Extend(wealableExtender);
                // Notification を送る Activity を指定
                var notificationManager = NotificationManagerCompat.From(this);
                // 実際に Notification を送る
                notificationManager.Notify(1, notificationBuilder.Build());

            };
        }
    }
}

