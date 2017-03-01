using Xamarin.Forms;
using System.Collections.Generic;
using Facebook;
using System.Diagnostics;
using System;

namespace QuickContacts
{
	public partial class LoginPage : ContentPage
	{
		const string FacebookAppId = "104491250069785";
		const string extendedPermissions = "public_profile";
		string fbLoginUrl;
		FacebookClient fb;
		WebView loginPage;
		int count = 0;

		public LoginPage(string logout)
		{
			InitializeComponent();

			fb = new FacebookClient();
			fbLoginUrl = logout;

			loginPage = new WebView
			{
				Source = fbLoginUrl,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			loginPage.Navigated += fbLoggedin;
			Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTick);

			Content = loginPage;
		}

		public LoginPage()
		{
			InitializeComponent();

			CreateLoginView();
		}

		//redirect to login page after 2 sec
		public bool OnTimerTick()
		{
			count++;

			if (count == 2)
			{
				CreateLoginView();

				return false;
			}

			return true;
		}

		//show the login webview
		private void CreateLoginView()
		{
			fb = new FacebookClient();
			fbLoginUrl = GetFacebookLoginUrl(FacebookAppId, extendedPermissions);

			loginPage = new WebView
			{
				Source = fbLoginUrl,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			loginPage.Navigated += fbLoggedin;

			Content = loginPage;
		}

		private string GetFacebookLoginUrl(string appId, string extendedPermissions)
		{
			var parameters = new Dictionary<string, object>();
			parameters["client_id"] = appId;
			parameters["redirect_uri"] = "https://www.facebook.com/connect/login_success.html";
			parameters["response_type"] = "token";
			parameters["display"] = "touch";

			// add the 'scope' only if we have extendedPermissions.
			if (!string.IsNullOrEmpty(extendedPermissions))
			{
				parameters["scope"] = extendedPermissions;
			}

			return fb.GetLoginUrl(parameters).AbsoluteUri;
		}

		private async void fbLoggedin(object sender, WebNavigatedEventArgs e)
		{
			FacebookOAuthResult oauthResult;
			System.Uri tmpUrl = new System.Uri(e.Url);

			if (fb.TryParseOAuthCallbackUrl(tmpUrl, out oauthResult))
			{
				if (oauthResult.IsSuccess)
				{
					fb.AccessToken = oauthResult.AccessToken;
					var tmp = await fb.GetTaskAsync("me");
					var result = (IDictionary<string, object>)tmp;

					string fbId = result["id"].ToString();
					string name = result["name"].ToString();
					App app = Application.Current as App;

					//store user info
					Helpers.Settings.UserName = name;
					Helpers.Settings.UserId = fbId;
					app.FbId = fbId;
					app.Name = name;
					app.AccessToken = fb.AccessToken;

					//save user data into database
					QContactDB qcdb = new QContactDB();
					QContact qc = new QContact();
					if (!qcdb.ExistQContact(fbId + "," + fbId))
					{
						qc.myIdfriendId = fbId + "," + fbId;
						qcdb.AddQContact(qc);

						//for test
						testAddContacts(fbId);
					}

					MainPage nextPage = new MainPage(name + "," + fbId);
				    await Navigation.PushModalAsync(nextPage);
				}
			}
		}

		//for test
		public void testAddContacts(string fbId)
		{
			List<QContact> qcs = new List<QContact>()
			{
				new QContact {myIdfriendId=fbId + ",0", FirstName="Tommy", LastName="Chen", Mobile = "18734780"},
				new QContact {myIdfriendId=fbId + ",1", FirstName="Steven", LastName="Nash", Mobile = "21591359"},
				new QContact {myIdfriendId=fbId + ",2", FirstName="Jim", LastName="Duncan", Mobile = "2975917957"},
				new QContact {myIdfriendId=fbId + ",3", FirstName="Roger", LastName="Clemens", Mobile = "159174810"},
				new QContact {myIdfriendId=fbId + ",4", FirstName="Honus", LastName="Wagner", Mobile = "82957175"},
				new QContact {myIdfriendId=fbId + ",5", FirstName="Stan", LastName="Musial", Mobile = "89517957"},
			};

			QContactDB qcdb = new QContactDB();
			for (int i = 0; i < qcs.Count; i++)
			{
				qcdb.AddQContact(qcs[i]);
			}
		}

	}
}
