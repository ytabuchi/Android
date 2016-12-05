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
    [Activity(Label = "GetParcelableArrayListActivity")]
    public class GetParcelableArrayListActivity : Activity
    {
        List<TableItem> tableItems { get; set; } = new List<TableItem>();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.GetParcelableArrayList);
            var list = FindViewById<ListView>(Resource.Id.listView1);

            // SimpleListItem2のために用意したList<TableItem>をクリア。
            tableItems.Clear();

            var intent = this.Intent;
            if (intent != null && intent.HasExtra("data"))
            {
                var data = intent.GetParcelableArrayListExtra("data");
                // 受け取ったインテントはList<IParcelable>のため、TableItemParcelableでキャストしてList<TableItem>に格納していく。
                foreach (var item in data)
                {
                    tableItems.Add(((TableItemParcelable)item).Item);
                }
            }

            // SimpleListItem2 を使用するAdapterクラスを用意し、クラス内で各要素の割り当てを行う。
            // 詳細はSimpleListItem2AdapterのSummaryのリンクを参照のこと。
            list.Adapter = new SimpleListItem2Adapter(this, tableItems);
            
        }
    }
}