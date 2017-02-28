using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace QuickContacts
{
	public partial class ProfileEditPage : ContentPage
	{
		public ProfileEditPage()
		{
			InitializeComponent();
		}

		public void onPeOkClicked(object sender, EventArgs args)
		{
			QContact qc = new QContact();
			QContactDB qcdb = new QContactDB();
			App app = Application.Current as App;

			qc.Id = app.FbId;
			qc.Type = "1";
			qc.FirstName = peFirstName.Text;
			qc.LastName = peLastName.Text;
			qc.Whose = "";
			qc.Company = peCompany.Text;
			qc.Mobile = peMobile.Text;
			qc.HomePhone = peHomePhone.Text;
			qc.WorkPhone = peWorkFax.Text;
			qc.HomeFax = peHomeFax.Text;
			qc.WorkFax = peWorkFax.Text;
			qc.Addr = peAddr.Text;
			qc.Email = peEmail.Text;
			qc.Birthday = peBirthday.Text;
			qc.URL = peURL.Text;
			qc.Skype = peSkype.Text;
			qc.Facebook = peFacebook.Text;
			qc.LinkedIn = peLinkedIn.Text;
			qc.Twitter = peTwitter.Text;
			qc.Instagram = peInstagram.Text;
			qc.LastModified = "";

			qcdb.AddQContact(qc);

			Application.Current.MainPage = new MainPage();
		}

		public void onPeCancelClicked(object sender, EventArgs args)
		{
			Application.Current.MainPage = new MainPage();
		} 
	}
}
