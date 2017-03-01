using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace QuickContacts
{
	public partial class ProfileEditPage : ContentPage
	{
		string fbId = Helpers.Settings.UserId;

		public ProfileEditPage()
		{
			InitializeComponent();

			QContactDB qcdb = new QContactDB();
			QContact qc = qcdb.GetQContact(fbId);

			if (qc != null)
			{
				showUsrData(qc);
			}
		}

		public void showUsrData(QContact qc)
		{
			if(qc.FirstName != null) peFirstName.Text = qc.FirstName;
			if(qc.LastName != null) peLastName.Text = qc.LastName;
			if(qc.Company != null) peCompany.Text = qc.Company;
			if(qc.Mobile != null) peMobile.Text = qc.Mobile;
			if(qc.HomePhone != null) peHomePhone.Text = qc.HomePhone;
			if(qc.WorkPhone != null) peWorkPhone.Text = qc.WorkPhone;
			if(qc.HomeFax != null) peHomeFax.Text= qc.HomeFax;
			if(qc.WorkFax != null) peWorkFax.Text = qc.WorkFax;
			if(qc.Addr != null) peAddr.Text = qc.Addr;
			if(qc.Email != null) peEmail.Text = qc.Email;
			if(qc.Birthday != null) peBirthday.Text = qc.Birthday;
			if(qc.URL != null) peURL.Text = qc.URL;
			if(qc.Skype != null) peSkype.Text = qc.Skype;
			if(qc.Facebook != null) peFacebook.Text = qc.Facebook;
			if(qc.LinkedIn != null) peLinkedIn.Text = qc.LinkedIn;
			if(qc.Twitter != null) peTwitter.Text = qc.Twitter;
			if(qc.Instagram != null) peInstagram.Text = qc.Instagram;
		}

		public void onPeOkClicked(object sender, EventArgs args)
		{
			QContact qc = new QContact();
			QContactDB qcdb = new QContactDB();

			qc.Id = fbId;
			qc.Type = "1";
			qc.FirstName = peFirstName.Text;
			qc.LastName = peLastName.Text;
			qc.Whose = fbId;
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

			qcdb.UpdateQContact(qc);

			Application.Current.MainPage = new MainPage();
		}

		public void onPeCancelClicked(object sender, EventArgs args)
		{
			Application.Current.MainPage = new MainPage();
		} 
	}
}
