using System;
namespace QuickContacts
{
	public interface INetworkConnection
	{
		bool IsConnected { get; }
		void CheckNetworkConnection();
	}
}
