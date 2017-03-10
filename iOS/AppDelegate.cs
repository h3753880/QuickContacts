﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Messier16.Forms.iOS.Controls;
using UIKit;

namespace QuickContacts.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			ZXing.Net.Mobile.Forms.iOS.Platform.Init();
			global::Xamarin.Forms.Forms.Init();
			Messier16Controls.InitAll();

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
