using System;
using SQLite.Net.Attributes;

namespace QuickContacts
{
	public class QContact
	{
		[PrimaryKey]
		//myId,friendId
		public string myIdfriendId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Company { get; set; }
		public string Mobile { get; set; }
		public string HomePhone { get; set; }
		public string WorkPhone { get; set; }
		public string HomeFax { get; set; }
		public string WorkFax { get; set; }
		public string Addr { get; set; }
		public string Email { get; set; }
		public string Birthday { get; set; }
		public string URL { get; set; }
		public string Skype { get; set; }
		public string Facebook { get; set; }
		public string LinkedIn { get; set; }
		public string Twitter { get; set; }
		public string Instagram { get; set; }
		public string LastModified { get; set; }

		public QContact()
		{
		}
	}
}
