
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace WikiSummarizer
{
	class dialog :DialogFragment
	{
		private Button summarize;
		private EditText txtSum;
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);

			var view = inflater.Inflate (Resource.Layout.dialogStyle, container, false);
			Toast.MakeText (this.Activity, "Input text in text field and press Trimm", ToastLength.Short);

			summarize = view.FindViewById<Button> (Resource.Id.button4);
			txtSum = view.FindViewById<EditText> (Resource.Id.editText3);

			summarize.Click += (object sender, EventArgs e) => {
				string newoutput = "." + txtSum.Text;
				string[] split_strings = newoutput.Split ('.');
				if (newoutput.Length < 350) {
					try{
					txtSum.Text = split_strings [1] + "." + split_strings [3] + "." + split_strings [2] + "."
					+ split_strings [5] + "." + split_strings [6] + "." + split_strings [4] + ".";
					}catch{try{txtSum.Text = split_strings [1] + "." + split_strings [3] + "." + split_strings [2] + ".";}
						catch{}}
				} else {
					txtSum.Text = split_strings [1] + "." + split_strings [3] + "." + split_strings [2] + ".";
				}
			};
				return view;
		}
		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);
		}
	}
}

