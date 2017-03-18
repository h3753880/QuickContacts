using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace QuickContacts
{
	public partial class NoInternetPage : ContentPage
	{
		INetworkConnection networkConnection = DependencyService.Get<INetworkConnection>();

		public NoInternetPage()
		{
			InitializeComponent();

			Device.StartTimer(TimeSpan.FromSeconds(5), OnTimerTick);
		}

		public bool OnTimerTick()
		{
			networkConnection.CheckNetworkConnection();
			bool networkStatus = networkConnection.IsConnected;
			Debug.WriteLine("deteceted:" + networkStatus);

			if (networkStatus)
			{
				Navigation.PushModalAsync(new LoginPage());
				return false;
			}
			return true;
		}
	}
}
