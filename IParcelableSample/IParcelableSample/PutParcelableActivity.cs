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
    [Activity(Label = "PutParcelable")]
    public class PutParcelableActivity : Activity
    {
        // ActivityResult‚ğˆ—‚·‚é‚½‚ß‚Ì’è”
        const int REQUEST_CODE_TABLEITEM = 1;

        protected override void OnCreate(Bundle bundel)
        {
            base.OnCreate(bundel);
            SetContentView(Resource.Layout.PutParcelable);
            var nameText = FindViewById<EditText>(Resource.Id.putParcelableNameText);
            var timeText = FindViewById<EditText>(Resource.Id.putParcelableTimeText);
            var button = FindViewById<Button>(Resource.Id.putParcelableGoButton);

            button.Click += (sender, e) =>
            {
                var item = new TableItemParcelable(nameText.Text, DateTime.Parse(timeText.Text));
                var intent = new Intent(this, typeof(GetParcelableActivity));
                intent.PutExtra("data", item);
                //StartActivity(intent); // –ß‚Á‚Ä‚«‚½Parcelable‚ğ–³‹‚·‚é
                StartActivityForResult(intent, REQUEST_CODE_TABLEITEM); // –ß‚Á‚Ä‚«‚½Parcelable‚ğˆ—‚·‚é
            };
        }

        /// <summary>
        /// –ß‚Á‚Ä‚«‚½Parcelable‚ğˆ—‚·‚éƒƒ\ƒbƒh
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="resultCode"></param>
        /// <param name="data"></param>
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            switch (requestCode)
            {
                case REQUEST_CODE_TABLEITEM:
                    if (resultCode == Result.Ok && data.HasExtra("returned_data"))
                    {
                        var item = data.GetParcelableExtra("returned_data") as TableItemParcelable;
                        Toast.MakeText(this,
                          $"{item.Item.Name}/{item.Item.Time} ‚ğó‚¯æ‚è‚Ü‚µ‚½",
                          ToastLength.Long).Show();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}