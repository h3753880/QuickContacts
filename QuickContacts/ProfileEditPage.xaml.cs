using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
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
			QContact qc = qcdb.GetQContact(fbId + "," + fbId);

			if (qc != null)
			{
				showUsrData(qc);
			}
		}

		public void showUsrData(QContact qc)
		{
			if (qc.FirstName != null) peFirstName.Text = qc.FirstName;
			if (qc.LastName != null) peLastName.Text = qc.LastName;
			if (qc.Company != null) peCompany.Text = qc.Company;
			if (qc.Mobile != null) peMobile.Text = qc.Mobile;
			if (qc.HomePhone != null) peHomePhone.Text = qc.HomePhone;
			if (qc.WorkPhone != null) peWorkPhone.Text = qc.WorkPhone;
			if (qc.HomeFax != null) peHomeFax.Text = qc.HomeFax;
			if (qc.WorkFax != null) peWorkFax.Text = qc.WorkFax;
			if (qc.Addr != null) peAddr.Text = qc.Addr;
			if (qc.Email != null) peEmail.Text = qc.Email;
			if (qc.Birthday != null) peBirthday.Date = qc.Birthday;
			if (qc.URL != null) peURL.Text = qc.URL;
			if (qc.Skype != null) peSkype.Text = qc.Skype;
			if (qc.Facebook != null) peFacebook.Text = qc.Facebook;
			if (qc.LinkedIn != null) peLinkedIn.Text = qc.LinkedIn;
			if (qc.Twitter != null) peTwitter.Text = qc.Twitter;
			if (qc.Instagram != null) peInstagram.Text = qc.Instagram;
		}

		public async void peEmailCompleted(object sender, EventArgs args)
		{
			if (!IsValidEmail(((Entry)peEmail).Text))
			{
				await DisplayAlert("Alert", "Email format error", "OK");
			}
		}

		public void onPeOkClicked(object sender, EventArgs args)
		{
			QContact qc = new QContact();
			QContactDB qcdb = new QContactDB();

			qc.myIdfriendId = fbId + "," + fbId;
			qc.FirstName = peFirstName.Text;
			qc.LastName = peLastName.Text;
			qc.Company = peCompany.Text;
			qc.Mobile = peMobile.Text;
			qc.HomePhone = peHomePhone.Text;
			qc.WorkPhone = peWorkFax.Text;
			qc.HomeFax = peHomeFax.Text;
			qc.WorkFax = peWorkFax.Text;
			qc.Addr = peAddr.Text;
			qc.Email = peEmail.Text;
			qc.Birthday = peBirthday.Date;
			qc.URL = peURL.Text;
			qc.Skype = peSkype.Text;
			qc.Facebook = peFacebook.Text;
			qc.LinkedIn = peLinkedIn.Text;
			qc.Twitter = peTwitter.Text;
			qc.Instagram = peInstagram.Text;
			qc.LastModified = DateTime.Now.ToLocalTime();
			Debug.WriteLine(DateTime.Now.ToLocalTime().ToString());

			if (qcdb.ExistQContact(qc.myIdfriendId))
			{
				qcdb.UpdateQContact(qc);
			}
			else
			{
				qcdb.AddQContact(qc);
			}
				
			Application.Current.MainPage = new MainPage();
		}

		public async void onPeCancelClicked(object sender, EventArgs args)
		{
			await Navigation.PopAsync();
		}

		bool IsValidEmail(string strIn)
		{
			if (String.IsNullOrEmpty(strIn))
				return false;
			try
			{
				// from https://msdn.microsoft.com/en-us/library/01escwtf(v=vs.110).aspx
				return Regex.IsMatch(strIn,
				@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|" +
				@"[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)" +
				@"(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|" +
				@"(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
				RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
			}
			catch (RegexMatchTimeoutException)
			{
				return false;
			}
		}
	}
}
