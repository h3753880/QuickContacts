using System.Collections.Generic;
using System.Linq;
using SQLite.Net;
using Xamarin.Forms;

namespace QuickContacts
{
	public class QContactDB
	{
		private SQLiteConnection _sqlconnection;

		public QContactDB()
		{
			//Getting conection and Creating table  
			_sqlconnection = DependencyService.Get<ISQLite>().GetConnection();
			_sqlconnection.CreateTable<QContact>();
		}

		//Get all QContact items  
		public IEnumerable<QContact> GetQContacts(string id)
		{
			return (from t in _sqlconnection.Table<QContact>() where t.myIdfriendId == id select t).ToList();
		}

		//Get specific QContact  
		public QContact GetQContact(string id)
		{
			return _sqlconnection.Table<QContact>().FirstOrDefault(t => t.myIdfriendId == id);
		}

		//Delete specific QContact  
		public void DeleteQContact(string id)
		{
			_sqlconnection.Delete<QContact>(id);
		}

		//Add new QContact to DB  
		public void AddQContact(QContact qcontact)
		{
			_sqlconnection.Insert(qcontact);
		}

		//Update QContact to DB  
		public void UpdateQContact(QContact qcontact)
		{
			_sqlconnection.Update(qcontact);
		}

		public bool ExistQContact(string id)
		{
			return _sqlconnection.Table<QContact>().FirstOrDefault(t => t.myIdfriendId == id) != null;
		}
	}
}
