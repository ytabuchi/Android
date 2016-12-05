using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;

using Toolbar = Android.Support.V7.Widget.Toolbar;


namespace MeterialAppCompat
{
    [Activity(MainLauncher = true)]
    public class MainActivity : ActionBarActivity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            // Toolbar は、デフォルトの ActionBar になります
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Hello from Appcompat Toolbar";

            // Get our button from the layout resource,
            // and attach an event to it
            var button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate {
                button.Text = string.Format("{0} clicks!", count++); 
            };
        }

        /// <Docs>The options menu in which you place your items.</Docs>
        /// <returns>To be added.</returns>
        /// <summary>
        /// Toolbar/Action Bar で使われるメニューです。
        /// </summary>
        /// <param name="menu">Menu</param>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.home, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Toast.MakeText(this, "Top ActionBar pressed: " + item.TitleFormatted, ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }
    }
}

