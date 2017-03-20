using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Widget;
using Xamarin.Forms;
using Android.Net;
using Java.Lang;

[assembly: Dependency(typeof(QuickContacts.Droid.ExportContacts))]
namespace QuickContacts.Droid
{
	public class ExportContacts : IAddContactsInfo
	{
		public bool AddContacts(QContact qc)
		{
			Console.WriteLine("export contacts android");

			List<ContentProviderOperation> ops = new List<ContentProviderOperation>();
			int rawContactInsertIndex = ops.Count;

			ContentProviderOperation.Builder builder =
				ContentProviderOperation.NewInsert(ContactsContract.RawContacts.ContentUri);
			builder.WithValue(ContactsContract.RawContacts.InterfaceConsts.AccountType, null);
			builder.WithValue(ContactsContract.RawContacts.InterfaceConsts.AccountName, null);
			ops.Add(builder.Build());

			//Name
			builder = ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri);
			builder.WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, rawContactInsertIndex);
			builder.WithValue(ContactsContract.Data.InterfaceConsts.Mimetype,
				ContactsContract.CommonDataKinds.StructuredName.ContentItemType);
			builder.WithValue(ContactsContract.CommonDataKinds.StructuredName.FamilyName, qc.LastName);
			builder.WithValue(ContactsContract.CommonDataKinds.StructuredName.GivenName, qc.FirstName);
			ops.Add(builder.Build());

			//MOBILE
			builder = ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri);
			builder.WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, rawContactInsertIndex);
			builder.WithValue(ContactsContract.Data.InterfaceConsts.Mimetype,
				ContactsContract.CommonDataKinds.Phone.ContentItemType);
			builder.WithValue(ContactsContract.CommonDataKinds.Phone.Number, qc.Mobile);
			builder.WithValue(ContactsContract.CommonDataKinds.StructuredPostal.InterfaceConsts.Type,
					ContactsContract.CommonDataKinds.StructuredPostal.InterfaceConsts.TypeCustom);
			builder.WithValue(ContactsContract.CommonDataKinds.Phone.InterfaceConsts.Label, "Primary Phone");
			ops.Add(builder.Build());

			//Email
			builder = ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri);
			builder.WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, rawContactInsertIndex);
			builder.WithValue(ContactsContract.Data.InterfaceConsts.Mimetype,
				ContactsContract.CommonDataKinds.Email.ContentItemType);
			builder.WithValue(ContactsContract.CommonDataKinds.Email.InterfaceConsts.Data, qc.Email);
			builder.WithValue(ContactsContract.CommonDataKinds.Email.InterfaceConsts.Type,
				ContactsContract.CommonDataKinds.Email.InterfaceConsts.TypeCustom);
			builder.WithValue(ContactsContract.CommonDataKinds.Email.InterfaceConsts.Label, "Email");
			ops.Add(builder.Build());

			//Address
			builder = ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri);
			builder.WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, rawContactInsertIndex);
			builder.WithValue(ContactsContract.Data.InterfaceConsts.Mimetype,
				ContactsContract.CommonDataKinds.StructuredPostal.ContentItemType);
			builder.WithValue(ContactsContract.CommonDataKinds.StructuredPostal.Street, qc.Addr);
			builder.WithValue(ContactsContract.CommonDataKinds.StructuredPostal.City, "");
			ops.Add(builder.Build());

			//others-> birthday fb
			StringBuilder sb = new StringBuilder();
			sb.Append("Birthday:").Append(qc.Birthday.ToString("MM/dd/yyyy")).Append(", Facebook:").Append(qc.Facebook)
			  .Append(", Instagram:").Append(qc.Instagram).Append(", Twitter:").Append(qc.Twitter)
			  .Append(", Linkedin:").Append(qc.LinkedIn).Append(", Skype:").Append(qc.Skype);
			builder = ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri);
			builder.WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, rawContactInsertIndex);
			builder.WithValue(ContactsContract.Data.InterfaceConsts.Mimetype,
							  ContactsContract.CommonDataKinds.Note.ContentItemType);
			builder.WithValue(ContactsContract.CommonDataKinds.Note.InterfaceConsts.Data1, sb.ToString());
			ops.Add(builder.Build());

			//company
			builder = ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri);
			builder.WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, rawContactInsertIndex);
			builder.WithValue(ContactsContract.Data.InterfaceConsts.Mimetype,
							  ContactsContract.CommonDataKinds.Organization.ContentItemType);
			builder.WithValue(ContactsContract.CommonDataKinds.Organization.InterfaceConsts.Data, qc.Company);
			ops.Add(builder.Build());

			//url
			builder = ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri);
			builder.WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, rawContactInsertIndex);
			builder.WithValue(ContactsContract.Data.InterfaceConsts.Mimetype,
							  ContactsContract.CommonDataKinds.Website.ContentItemType);
			builder.WithValue(ContactsContract.CommonDataKinds.Website.InterfaceConsts.Data, qc.URL);
			ops.Add(builder.Build());

			//homePhone
			builder = ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri);
			builder.WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, rawContactInsertIndex);
			builder.WithValue(ContactsContract.Data.InterfaceConsts.Mimetype,
				ContactsContract.CommonDataKinds.Phone.ContentItemType);
			builder.WithValue(ContactsContract.CommonDataKinds.Phone.Number, qc.HomePhone);
			builder.WithValue(ContactsContract.CommonDataKinds.StructuredPostal.InterfaceConsts.Type,
					ContactsContract.CommonDataKinds.StructuredPostal.InterfaceConsts.TypeCustom);
			builder.WithValue(ContactsContract.CommonDataKinds.Phone.InterfaceConsts.Label, "Home");
			ops.Add(builder.Build());

			//workPhone
			builder = ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri);
			builder.WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, rawContactInsertIndex);
			builder.WithValue(ContactsContract.Data.InterfaceConsts.Mimetype,
				ContactsContract.CommonDataKinds.Phone.ContentItemType);
			builder.WithValue(ContactsContract.CommonDataKinds.Phone.Number, qc.WorkPhone);
			builder.WithValue(ContactsContract.CommonDataKinds.StructuredPostal.InterfaceConsts.Type,
					ContactsContract.CommonDataKinds.StructuredPostal.InterfaceConsts.TypeCustom);
			builder.WithValue(ContactsContract.CommonDataKinds.Phone.InterfaceConsts.Label, "Work");
			ops.Add(builder.Build());

			//homeFax
			builder = ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri);
			builder.WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, rawContactInsertIndex);
			builder.WithValue(ContactsContract.Data.InterfaceConsts.Mimetype,
				ContactsContract.CommonDataKinds.Phone.ContentItemType);
			builder.WithValue(ContactsContract.CommonDataKinds.Phone.Number, qc.HomeFax);
			builder.WithValue(ContactsContract.CommonDataKinds.StructuredPostal.InterfaceConsts.Type,
					ContactsContract.CommonDataKinds.StructuredPostal.InterfaceConsts.TypeCustom);
			builder.WithValue(ContactsContract.CommonDataKinds.Phone.InterfaceConsts.Label, "Home Fax");
			ops.Add(builder.Build());

			//workFax
			builder = ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri);
			builder.WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, rawContactInsertIndex);
			builder.WithValue(ContactsContract.Data.InterfaceConsts.Mimetype,
				ContactsContract.CommonDataKinds.Phone.ContentItemType);
			builder.WithValue(ContactsContract.CommonDataKinds.Phone.Number, qc.WorkFax);
			builder.WithValue(ContactsContract.CommonDataKinds.StructuredPostal.InterfaceConsts.Type,
					ContactsContract.CommonDataKinds.StructuredPostal.InterfaceConsts.TypeCustom);
			builder.WithValue(ContactsContract.CommonDataKinds.Phone.InterfaceConsts.Label, "Work Fax");
			ops.Add(builder.Build());

			try
			{
				var res = Forms.Context.ContentResolver.ApplyBatch(ContactsContract.Authority, ops);
				return true;
				//Toast.MakeText(Forms.Context, "Contact Saved", ToastLength.Short).Show();
			}
			catch (System.Exception e)
			{
				Console.WriteLine(e.Message);
				//Toast.MakeText(Forms.Context, "Contact Not Saved", ToastLength.Long).Show();
				return false;
			}
		}
	}
}
