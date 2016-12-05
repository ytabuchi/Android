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
    /// <summary>
    /// ListViewのAdapterです。
    /// </summary>
    /// <remarks>
    /// 参考資料：Part 3 - Customizing a ListView's Appearance - Xamarin https://developer.xamarin.com/guides/android/user_interface/working_with_listviews_and_adapters/part_3_-_customizing_a_listview's_appearance/
    /// Sample：https://github.com/xamarin/monodroid-samples/tree/master/BuiltInViews
    /// </remarks>
    public class SimpleListItem2Adapter : BaseAdapter<TableItem>
    {
        Activity _context;
        List<TableItem> _items;

        public SimpleListItem2Adapter(Activity context, List<TableItem> items)
        {
            _context = context;
            _items = items;
        }
        public override TableItem this[int position]
        {
            get
            {
                return _items[position];
            }
        }

        public override int Count
        {
            get
            {
                return _items.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position];
            View view = convertView;
            if (view == null)
                view = _context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);

            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = item.Name;
            view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = item.Time.ToString("yyyy/MM/dd");

            return view;
        }
    }
}