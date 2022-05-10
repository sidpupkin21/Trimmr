using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Speech;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace WikiSummarizer
{
	[Activity (Label = "Trimmr (Debug Version)", MainLauncher = true)]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			TextView txt = FindViewById<TextView> (Resource.Id.editText1);
			Button frag = FindViewById<Button> (Resource.Id.button1);
			TextView sum = FindViewById<TextView> (Resource.Id.editText2);
			ProgressBar pb = FindViewById<ProgressBar> (Resource.Id.progressBar1);
			TextView lblLoading = FindViewById<TextView> (Resource.Id.textView1);

			frag.Click += (object sender, EventArgs e) => {
				FragmentTransaction transaction = FragmentManager.BeginTransaction();
				dialog d = new dialog();
				d.Show(transaction, "dialog fragment");
			};
			button.Click += delegate {
				//Task.Factory.StartNew(() => ScrapData());
				//ScrapData();
				//Scrap();
				  
				pb.Visibility = ViewStates.Visible;
				lblLoading.Visibility = ViewStates.Visible;
				sum.Text = string.Empty;
				Task.Factory.StartNew(() => Scrap());
			};

		}
		public void hideBar(){
			ProgressBar pb = FindViewById<ProgressBar> (Resource.Id.progressBar1);
			TextView lblLoading = FindViewById<TextView> (Resource.Id.textView1);
			pb.Visibility = ViewStates.Invisible;
			lblLoading.Visibility = ViewStates.Invisible;
		}
		public  void ScrapData(){
			ProgressBar pb = FindViewById<ProgressBar> (Resource.Id.progressBar1);
			try {
				TextView txt1 = FindViewById<TextView> (Resource.Id.editText1);
				TextView txt2 = FindViewById<TextView> (Resource.Id.editText2);
				//string cite_node = "<a href=\"(.*?)\"><span>(.*?)</span>(.*?)<span>(.*?)</span></a>";
				//string regex = "(\\[.*\\])|(\".*\")|('.*')|(\\(.*\\))|(\\<.*\\>)";
				string website = "https://en.wikipedia.org/wiki/" + txt1.Text;
				string mwiki = "<p>(.*?)</p>";
				HttpWebRequest wr = (HttpWebRequest)WebRequest.Create (website);
				wr.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
				string wiki = new StreamReader (wr.GetResponse ().GetResponseStream ()).ReadToEnd ();
				ServicePointManager.Expect100Continue = false;
				foreach (Match match in Regex.Matches(wiki, mwiki)) {
					string sOutput = match.Groups [1].Value;
					//string output = Regex.Replace(match.Groups[1].Value, regex, "");
					string output = Regex.Replace (sOutput, "<.*?>|([.*?])", string.Empty);
					txt2.Append (output);
				}

				string input = "." + txt2.Text;
				string newoutput = Regex.Replace (input, @"?\[.*?\]", ".");
				string[] split_strings = newoutput.Split ('.');
				if (newoutput.Length < 350) {

					txt2.Text = split_strings [1] + "." + split_strings [3] + "." + split_strings [2] + "."
					+ split_strings [5] + "." + split_strings [6] + "." + split_strings [4] + ".";
					pb.Visibility = ViewStates.Invisible;
				} else {
					txt2.Text = split_strings [1] + "." + split_strings [3] + "." + split_strings [2] + ".";
					pb.Visibility = ViewStates.Invisible;
				}
			} catch {
				Toast.MakeText (this, "Operation cancelled, check your internet!", ToastLength.Long);
			}

		}
	
			public void Scrap(){
			TextView txt1 = FindViewById<TextView> (Resource.Id.editText1);
			TextView txt2 = FindViewById<TextView> (Resource.Id.editText2);
			//txt2.Text = string.Empty;
			ProgressBar pb = FindViewById<ProgressBar> (Resource.Id.progressBar1);
			TextView lblLoading = FindViewById<TextView> (Resource.Id.textView1);
			//string cite_node = "<a href=\"(.*?)\"><span>(.*?)</span>(.*?)<span>(.*?)</span></a>";
			//string regex = "(\\[.*\\])|(\".*\")|('.*')|(\\(.*\\))|(\\<.*\\>)";
			string website = "https://en.wikipedia.org/wiki/" + txt1.Text;
			string mwiki = "<p>(.*?)</p>";
			HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(website);
			wr.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
			string wiki = new StreamReader(wr.GetResponse().GetResponseStream()).ReadToEnd();
			foreach (Match match in Regex.Matches(wiki, mwiki))
			{
				string sOutput = match.Groups[1].Value;
				//string output = Regex.Replace(match.Groups[1].Value, regex, "");
				string output = Regex.Replace(sOutput, "<.*?>|([.*?])", string.Empty);
				txt2.Append(output);
			}
			string input = "." + txt2.Text;
			string url = input;
			string newoutput = Regex.Replace(url, @" ?\[.*?\]", ".");
			string[] split_strings = newoutput.Split('.');
			if (txt2.Text.Length < 350)
			{
				string c = split_strings[1] + "." + split_strings[3] + "." + split_strings[2] + "."
					+ split_strings[5] + "." + split_strings[6] + "." + split_strings[4] + ".";
				string newoutput2 = Regex.Replace(c, @" ?\[.*?\]", ".");
				txt2.Text = newoutput2;
			}
			else { txt2.Text = split_strings[1] + "." + split_strings[3] + "." + split_strings[2] + "."; }
			//Clipboard.SetText(split_strings[1] + "." + split_strings[3] + "." + split_strings[2] + ".");
			pb.Visibility = ViewStates.Invisible;
			lblLoading.Visibility = ViewStates.Invisible;
	}
}
}



