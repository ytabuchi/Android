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

namespace IParcelableSample
{
    [Activity(Label = "Recieved data")]
    public class GetParcelableActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.GetParcelable);
            var getParcelableNameText = FindViewById<EditText>(Resource.Id.getParcelableNameText);
            var getParcelableTimeText = FindViewById<EditText>(Resource.Id.getParcelableTimeText);
            var getParcelableBackButton = FindViewById<Button>(Resource.Id.getParcelableBackButton);

            var intent = this.Intent;
            if (intent != null && intent.HasExtra("data"))
            {
                var item = intent.GetParcelableExtra("data") as TableItemParcelable;
                getParcelableNameText.Text = item.Item.Name;
                getParcelableTimeText.Text = item.Item.Time.ToString();
            }

            getParcelableBackButton.Click += (sender, e) =>
            {
                var resultIntent = new Intent();
                resultIntent.PutExtra("returned_data", 
                    new TableItemParcelable(
                        getParcelableNameText.Text, 
                        DateTime.Parse(getParcelableTimeText.Text)));
                SetResult(Result.Ok, resultIntent);
                Finish();
            };
        }
    }
}