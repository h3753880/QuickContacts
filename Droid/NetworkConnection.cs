using System;
using System.Diagnostics;
using Android.Content;
using Android.Net;
using Xamarin.Forms;

[assembly: Dependency(typeof(QuickContacts.Droid.NetworkConnection))]
namespace QuickContacts.Droid
{
	public class NetworkConnection : INetworkConnection
	{
		public bool IsConnected { get; set; }

		public void CheckNetworkConnection()
		{
			var connectivityManager = (ConnectivityManager)Forms.Context.GetSystemService(Context.ConnectivityService);
			NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
			bool isOnline = (activeConnection != null) && activeConnection.IsConnected;
			Debug.WriteLine(isOnline + ":android detect connection");
			IsConnected = isOnline;
		}
	}
}
