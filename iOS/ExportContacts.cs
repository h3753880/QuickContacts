using System;
using Xamarin.Forms;
using AddressBook;
using AddressBookUI;
using UIKit;
using Contacts;
using ContactsUI;
using Foundation;
using System.Diagnostics;
using System.Text;

[assembly: Dependency(typeof(QuickContacts.iOS.ExportContacts))]
namespace QuickContacts.iOS
{
	public class ExportContacts : IAddContactsInfo
	{
		public bool AddContacts(QContact qc)
		{
			Console.WriteLine("export contacts ios");

			var contact = new CNMutableContact();

			// Set standard properties
			contact.GivenName = PreventNull(qc.FirstName);
			contact.FamilyName = PreventNull(qc.LastName);


			// Add email addresses
			var homeEmail = new CNLabeledValue<NSString>(CNLabelKey.Home, new NSString(PreventNull(qc.Email)));
			var email = new[] { homeEmail };
			contact.EmailAddresses = email;

			// Add work address
			var workAddress = new CNMutablePostalAddress()
			{
				Street = PreventNull(qc.Addr)
			};
			contact.PostalAddresses = new[] { new CNLabeledValue<CNPostalAddress>(CNLabelKey.Work, workAddress) };

			// ADD BIRTHday
			string[] birth = PreventNull(qc.Birthday).Split('/');
			if (birth.Length == 3)
			{
				var birthday = new NSDateComponents()
				{
					Month = int.Parse(birth[0]),
					Day = int.Parse(birth[1]),
					Year = int.Parse(birth[2])
				};
				contact.Birthday = birthday;
			}

			// add company
			contact.OrganizationName = PreventNull(qc.Company);

			// add others-> fb
			StringBuilder sb = new StringBuilder();
			sb.Append("Facebook:").Append(PreventNull(qc.Facebook)).Append(", Instagram:").Append(PreventNull(qc.Instagram))
			  .Append(", Linkedin:").Append(PreventNull(qc.LinkedIn)).Append(", Skype:").Append(PreventNull(qc.Skype))
			  .Append(", Twitter:").Append(PreventNull(qc.Twitter));
			contact.Note = sb.ToString();

			// add url
			var url = new CNLabeledValue<NSString>(CNLabelKey.UrlAddressHomePage, new NSString(PreventNull(qc.URL)));
			var myUrl = new[] { url };
			contact.UrlAddresses = myUrl;

			//mobile
			var cellPhone =
				new CNLabeledValue<CNPhoneNumber>(CNLabelPhoneNumberKey.Mobile, new CNPhoneNumber(PreventNull(qc.Mobile)));
			//var phoneNumber = new[] { cellPhone };
			//contact.PhoneNumbers = phoneNumber;

			//home phone
			var homePhone =
				new CNLabeledValue<CNPhoneNumber>("HOME", new CNPhoneNumber(PreventNull(qc.HomePhone)));

			//work phone
			var workPhone =
				new CNLabeledValue<CNPhoneNumber>("WORK", new CNPhoneNumber(PreventNull(qc.WorkPhone)));

			//homefax
			var homeFax =
				new CNLabeledValue<CNPhoneNumber>(CNLabelPhoneNumberKey.HomeFax, new CNPhoneNumber(PreventNull(qc.HomeFax)));

			//workFax
			var workFax =
				new CNLabeledValue<CNPhoneNumber>(CNLabelPhoneNumberKey.WorkFax, new CNPhoneNumber(PreventNull(qc.WorkFax)));
			var phoneNumber = new[] { cellPhone, homePhone, workPhone, homeFax, workFax };
			contact.PhoneNumbers = phoneNumber;

			// Save new contact
			var store = new CNContactStore();
			var saveRequest = new CNSaveRequest();
			saveRequest.AddContact(contact, store.DefaultContainerIdentifier);

			NSError error;
			if (store.ExecuteSaveRequest(saveRequest, out error))
			{
				Console.WriteLine("New contact saved");
				return true;
			}
			else
			{
				Console.WriteLine("Save error: {0}", error);
				return false;
			}
		}

		private string PreventNull(string input)
		{
			if (input == null)
				return string.Empty;
			return input;
		}
	}
}
