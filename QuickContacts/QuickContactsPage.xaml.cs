using Xamarin.Forms;
using System.Collections.Generic;
using Facebook;
using System.Diagnostics;
using System;

namespace QuickContacts
{
	public partial class QuickContactsPage : ContentPage
	{
		const string FacebookAppId = "104491250069785";
		const string extendedPermissions = "public_profile";
		string fbLoginUrl;
		FacebookClient fb;
		WebView loginPage;

		public QuickContactsPage()
		{
			InitializeComponent();

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
					MainPage nextPage = new MainPage();
					App app = Application.Current as App;

					//store user info
					app.Name = name;
					app.FbId = fbId;
				    await Navigation.PushModalAsync(nextPage);

				}
			}
		}
	}
}
