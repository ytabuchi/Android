using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace IParcelableSample
{
    [Activity(Label = "IParcelableSample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            var goParcelableButton = FindViewById<Button>(Resource.Id.mainGoParcelableButton);
            var goParcelableArrayListButton = FindViewById<Button>(Resource.Id.mainGoParcelableArrayListButton);

            goParcelableButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(PutParcelableActivity));
                StartActivity(intent);
            };
            goParcelableArrayListButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(PutParcelableArrayListActivity));
                StartActivity(intent);
            };
        }
    }
}

