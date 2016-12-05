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
    [Activity(Label = "PutParcelableArrayList")]
    public class PutParcelableArrayListActivity : Activity
    {
        public List<TableItemParcelable> TableItems { get; set; } = new List<TableItemParcelable>();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.PutParcelableArrayList);
            var nameText = FindViewById<EditText>(Resource.Id.putParcelableArrayListNameText);
            var timeText = FindViewById<EditText>(Resource.Id.putParcelableArrayListTimeText);
            var button = FindViewById<Button>(Resource.Id.putParcelableArrayListGoButton);

            button.Click += (sender, e) =>
            {
                // TableItemはそのままでは渡せないので、一度データを格納する。
                var item = new TableItemParcelable(nameText.Text, DateTime.Parse(timeText.Text));
                TableItems.Add(item);

                // PutParcelableArrayListExtra(string name, List<IParcelable> value)はList<IParcelable>が必要なためTableItemを移し替える
                List<IParcelable> values = new List<IParcelable>();
                foreach (var x in TableItems)
                {
                    values.Add(new TableItemParcelable(x.Item.Name, x.Item.Time));
                }
                var intent = new Intent(this, typeof(GetParcelableArrayListActivity));
                intent.PutParcelableArrayListExtra("data", values);
                StartActivity(intent);
            };
        }
    }
}