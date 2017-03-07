using System;
using Xamarin.Forms;
using AddressBook;
using AddressBookUI;
using UIKit;
using Contacts;
using ContactsUI;
using Foundation;
using System.Diagnostics;

[assembly: Dependency(typeof(QuickContacts.iOS.ExportContacts))]
namespace QuickContacts.iOS
{
	public class ExportContacts: IAddContactsInfo
	{
		public void AddContacts(QContact qc)
		{
			Console.WriteLine("export contacts ios");

			var contact = new CNMutableContact();

			// Set standard properties
			contact.GivenName = PreventNull(qc.FirstName);
			contact.FamilyName = PreventNull(qc.LastName);

			//mobile
			var cellPhone =
				new CNLabeledValue<CNPhoneNumber>(CNLabelPhoneNumberKey.Mobile, new CNPhoneNumber(PreventNull(qc.Mobile)));
			var phoneNumber = new[] { cellPhone };
			contact.PhoneNumbers = phoneNumber;

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

			// Save new contact
			var store = new CNContactStore();
			var saveRequest = new CNSaveRequest();
			saveRequest.AddContact(contact, store.DefaultContainerIdentifier);

			NSError error;
			if (store.ExecuteSaveRequest(saveRequest, out error))
			{
				Console.WriteLine("New contact saved");
			}
			else
			{
				Console.WriteLine("Save error: {0}", error);
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
