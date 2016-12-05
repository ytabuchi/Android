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
using Java.Interop;
using Java.Lang;

namespace IParcelableSample
{
    public class TableItem
    {
        public string Name { get; set; }
        public DateTime Time { get; set; }
    }

    /// <summary>
    /// TableItemを継承したParcelableなアダプタークラス
    /// </summary>
    /// <remarks>
    /// 参考資料：Xamarin.Androidで画面遷移を行うには？ - Build Insider http://www.buildinsider.net/mobile/xamarintips/0004
    /// </remarks>
    public class TableItemParcelable : Java.Lang.Object, IParcelable
    {
        public TableItem Item { get; set; } = new TableItem();

        public TableItemParcelable(string name, DateTime time)
        {
            Item.Name = name;
            Item.Time = time;
        }

        public int DescribeContents()
        {
            return 0;
        }

        // CreateFromParcelで読み込む順番を間違えないように注意する。
        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            dest.WriteString(Item.Name);
            dest.WriteString(Item.Time.ToString());
        }

        [ExportField("CREATOR")]
        public static IParcelableCreator GetCreator()
        {
            return new TableItemParcelableCreator();
        }
    }

    public class TableItemParcelableCreator : Java.Lang.Object, IParcelableCreator
    {
        // WriteToParcelで指定している書き込み順に読み込む。
        public Java.Lang.Object CreateFromParcel(Parcel source)
        {
            TableItemParcelable item = new TableItemParcelable(
                source.ReadString(), 
                DateTime.Parse(source.ReadString()));
            return item;
        }

        public Java.Lang.Object[] NewArray(int size)
        {
            return new Java.Lang.Object[size];
        }
    }
}