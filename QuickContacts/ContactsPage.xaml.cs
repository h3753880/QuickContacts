using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace QuickContacts
{
	public partial class ContactsPage : ContentPage
	{
		string fbId = Helpers.Settings.UserId;
		QContactDB qcdb = new QContactDB();
		ObservableCollection<cItem> clist = new ObservableCollection<cItem>();

		public ContactsPage()
		{
			InitializeComponent();

			var qcs = qcdb.GetQContacts(fbId);

			ShowContacts((List<QContact>)qcs);
			cListView.ItemsSource = clist;
		}

		public void ShowContacts(List<QContact> qcs)
		{
			if (qcs != null)
			{
				clist.Clear();
				foreach (QContact qc in qcs)
				{
					cItem c = new cItem();
					//for test
					c.cSource = "http://graph.facebook.com/" + qc.myIdfriendId.Split(',')[1] + "/picture?type=small";
					c.cName = qc.FirstName + " " + qc.LastName;
					c.cId = qc.myIdfriendId;
					c.cChecked = false;
					clist.Add(c);
				}
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

			if (c == null) return;

			if (!cMultiSelect.IsToggled)
			{
				await Navigation.PushAsync(new ContactDetailPage(c.cId), false);
			}
			else
			{
				//bug to be fixed, tag the item, the switch should be toggled
				//int index = clist.IndexOf(c);
				c.cChecked = true;
				//clist[index] = c;

				((ListView)sender).SelectedItem = null;
			}
		}

		public void onSearchTextChanged(object sender, EventArgs args)
		{
			var keyqcs = qcdb.GetKeyQContacts(fbId, contactSearch.Text.Trim());
			ShowContacts((List<QContact>)keyqcs);
		}

		public void onCMultiSelect(object sender, ToggledEventArgs args)
		{
			if (!args.Value)
			{
				var qcs = qcdb.GetQContacts(fbId);
				ShowContacts((List<QContact>)qcs);
			}
		}

		public void onCCancelClicked(object sender, EventArgs args)
		{
			//unselect all the contacts
			var qcs = qcdb.GetQContacts(fbId);
			ShowContacts((List<QContact>)qcs);

			cMultiSelect.IsToggled = false;
		}

		public async void onCExportClicked(object sender, EventArgs args)
		{
			IAddContactsInfo addContacts = DependencyService.Get<IAddContactsInfo>();

			//get the selected contact list
			List<cItem> selectedList = new List<cItem>();
			foreach (cItem c in clist)
			{
				if (c.cChecked == true)
				{
					selectedList.Add(c);
				
					//export data
					QContact qc = qcdb.GetQContact(c.cId);
					addContacts.AddContacts(qc);
				}
			}

			var qcs = qcdb.GetQContacts(fbId);
			ShowContacts((List<QContact>)qcs);

			cMultiSelect.IsToggled = false;

			await DisplayAlert("Confirm", "The contact information has been sucessfully exported", "OK");
		}

		protected override void OnAppearing() 
		{ 	
			base.OnAppearing(); 

			var qcs = qcdb.GetQContacts(fbId);
			ShowContacts((List<QContact>)qcs);
		}
	}
}
