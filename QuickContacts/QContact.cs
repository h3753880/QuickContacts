using System;
using SQLite.Net.Attributes;

namespace QuickContacts
{
	public class QContact
	{
		[PrimaryKey]
		//the user's own facebook id or the user's friend's id
		public string Id { get; set; }
		//type 1 = user's own record, type 2 = user's friend's record
		public string Type { get; set; } 
		public string FirstName { get; set; }
		public string LastName { get; set; }
		//whose -> the user's own facebook id;  
		public string Whose { get; set; }
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
