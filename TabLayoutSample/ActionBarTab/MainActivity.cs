using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Content;
using System.Diagnostics.Contracts;

namespace ActionBarTab
{
    [Activity(Label = "ActionBarTab", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            SetContentView(Resource.Layout.Main);

            ActionBar.Tab tab = ActionBar.NewTab();
            tab.SetText("Tab 1");
            tab.TabSelected += (sender, e) =>
            {
                e.FragmentTransaction.Replace(Resource.Id.container, new FirstFragment());
            };
            ActionBar.AddTab(tab);

            tab = ActionBar.NewTab();
            tab.SetText("Tab 2");
            tab.TabSelected += (sender, e) =>
            {
                e.FragmentTransaction.Replace(Resource.Id.container, new SecondFragment());
            };
            ActionBar.AddTab(tab);
        }
    }

    public class FirstFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Tab1, null);

            var button = view.FindViewById<Button>(Resource.Id.tab1button);
            button.Click += (sender, e) =>
            {
                Toast.MakeText(inflater.Context, $"{button.Text} button is Clicked", ToastLength.Short).Show();
            };

            return view;
        }
    }

    public class SecondFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Tab2, null);

            var list = view.FindViewById<ListView>(Resource.Id.listView);

            list.Adapter = new ArrayAdapter(inflater.Context, Android.Resource.Layout.SimpleListItem1);

            var button = view.FindViewById<Button>(Resource.Id.tab2button);
            button.Click += (sender, e) =>
            {
                   
            };

            return view;
        }

    }
}

