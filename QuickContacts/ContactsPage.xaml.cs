using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

			// Receive message to delete the corresponding item
			MessagingCenter.Subscribe<ContactDetailPage, string>
				(this, "DeleteInformation", (sender, arg) =>
				{
					// If the object exists, remove it.
					foreach (cItem tmp in clist)
					{
						if (tmp.cId.Equals(arg))
						{
							clist.Remove(tmp);
							break;
						}
					}
				});
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
				int index = clist.IndexOf(c);
				// ObservableCollection does not raise propertychanged event when only updating the content of the item
				// remove the item then add it back, to generate the property change event from the observablecollection 
				clist.Remove(c);
				c.cChecked = !c.cChecked;
				clist.Insert(index, c);

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
			bool hasChecked = false;
			foreach (cItem c in clist)
			{
				Debug.WriteLine(c.cChecked);
				if (c.cChecked == true)
				{
					selectedList.Add(c);
					hasChecked = true;

					//export data
					QContact qc = qcdb.GetQContact(c.cId);
					addContacts.AddContacts(qc);
				}
			}

			if (hasChecked)
			{
				await DisplayAlert("Confirm", "The contact information has been sucessfully exported", "OK");
				var qcs = qcdb.GetQContacts(fbId);
				ShowContacts((List<QContact>)qcs);
				cMultiSelect.IsToggled = false;
			}
			else
			{
				await DisplayAlert("Alert", "No items selected", "OK");
			}
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			cListView.SelectedItem = null;
		}
	}
}
