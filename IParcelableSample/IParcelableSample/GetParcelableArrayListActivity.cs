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

            // SimpleListItem2�̂��߂ɗp�ӂ���List<TableItem>���N���A�B
            tableItems.Clear();

            var intent = this.Intent;
            if (intent != null && intent.HasExtra("data"))
            {
                var data = intent.GetParcelableArrayListExtra("data");
                // �󂯎�����C���e���g��List<IParcelable>�̂��߁ATableItemParcelable�ŃL���X�g����List<TableItem>�Ɋi�[���Ă����B
                foreach (var item in data)
                {
                    tableItems.Add(((TableItemParcelable)item).Item);
                }
            }

            // SimpleListItem2 ���g�p����Adapter�N���X��p�ӂ��A�N���X���Ŋe�v�f�̊��蓖�Ă��s���B
            // �ڍׂ�SimpleListItem2Adapter��Summary�̃����N���Q�Ƃ̂��ƁB
            list.Adapter = new SimpleListItem2Adapter(this, tableItems);
            
        }
    }
}