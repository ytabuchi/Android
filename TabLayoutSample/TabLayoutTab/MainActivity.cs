using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.Design.Widget;

namespace TabLayoutTab
{
    [Activity(Label = "TabLayoutTab", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var tabLayout = FindViewById<TabLayout>(Resource.Id.tabContainer);
            tabLayout.AddTab(tabLayout.NewTab().SetText("Tab1"));
            tabLayout.AddTab(tabLayout.NewTab().SetText("Tab2"));
        }
    }
}

