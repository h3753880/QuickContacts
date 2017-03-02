using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace QuickContacts
{
	public partial class ContactsPage : ContentPage
	{
		string fbId = Helpers.Settings.UserId;
		QContactDB qcdb = new QContactDB();
		List<cItem> clist = new List<cItem>();

		public ContactsPage()
		{
			InitializeComponent();

			var qcs = qcdb.GetQContacts(fbId);
			ShowContacts((List<QContact>)qcs);
		}

		public void ShowContacts(List<QContact> qcs)
		{
			if (qcs != null)
			{
				foreach (QContact qc in qcs)
				{
					cItem c = new cItem();
					//for test
					c.cSource = "http://graph.facebook.com/" + fbId + "/picture?type=small";
					c.cName = qc.FirstName + " " + qc.LastName;
					c.cId = qc.myIdfriendId;
					c.cChecked = false;
					clist.Add(c);
				}

				cListView.ItemsSource = clist;
			}
		}

		public class cItem
		{
			public string cSource { set; get; }
			public string cName { set; get; }
			public string cId { set; get; }
			public bool cChecked { set; get; }
 		}

		public async void cItemSelected(object sender, EventArgs args)
		{
			ListView lv = (ListView)sender;
			cItem c = lv.SelectedItem as cItem;

			if (!cMultiSelect.IsToggled)
			{
				await Navigation.PushAsync(new ContactDetailPage(c.cId), false);
				//Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTick);
			}
		}

		/*bool OnTimerTick()
		{
			cListView.SelectedItem = null;
			return false;
		}*/

		public void onSearchTextChanged(object sender, EventArgs args)
		{
			var keyqcs = qcdb.GetKeyQContacts(fbId, contactSearch.Text.Trim());
			ShowContacts((List<QContact>)keyqcs);
		}

		public void onCMultiSelect(object sender, ToggledEventArgs args)
		{
			if (!args.Value)
			{
				foreach (cItem c in clist)
				{
					c.cChecked = false;
				}
			}
			cListView.ItemsSource = clist;
		}

		public void onCCancelClicked(object sender, EventArgs args)
		{
			//unselect all the contacts
			foreach (cItem c in clist)
			{
				c.cChecked = false;
			}
			cMultiSelect.IsToggled = false;
			cListView.ItemsSource = clist;
		}

		public void onCExportClicked(object sender, EventArgs args)
		{
			DisplayAlert("Confirm", "The contact information has been sucessfully exported", "OK");

			//get the selected contact list
			List<cItem> selectedList = new List<cItem>();
			foreach (cItem c in clist)
			{
				if (c.cChecked == true) selectedList.Add(c);
			}

			foreach (cItem c in clist)
			{
				c.cChecked = false;
			}
			cMultiSelect.IsToggled = false;
			cListView.ItemsSource = clist;
		}
	}
}
