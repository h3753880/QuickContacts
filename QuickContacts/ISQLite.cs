using SQLite.Net;

namespace QuickContacts
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}
