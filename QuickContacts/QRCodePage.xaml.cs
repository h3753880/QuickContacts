using System;
using System.Collections.Generic;
using ZXing.Net.Mobile.Forms;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Diagnostics;

namespace QuickContacts
{
	public partial class QRCodePage : ContentPage
	{
		ZXingBarcodeImageView barcode;
		Button btn;
		StackLayout stack;
		QContactDB qcdb = new QContactDB();
		string fbId = Helpers.Settings.UserId;

		public QRCodePage()
		{
			InitializeComponent();

			//get data from database
			var qc = qcdb.GetQContact(fbId + "," + fbId);

			string myJson = CreateUserJson(qc);

			stack = new StackLayout();

			btn = new Button
			{
				Text = "Scan QR Code"
			};

			barcode = new ZXingBarcodeImageView
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			barcode.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
			barcode.BarcodeOptions.Width = 250;
			barcode.BarcodeOptions.Height = 250;
			barcode.BarcodeOptions.Margin = 10;
			barcode.BarcodeValue = myJson;

			btn.Clicked += BtnClickedEvent;

			stack.Children.Add(barcode);
			stack.Children.Add(btn);

			Content = stack;
		}

		//produce user data's json
		private string CreateUserJson(QContact qc)
		{
			string jsonStr = string.Empty;

			if (qc != null)
			{
				jsonStr = JsonConvert.SerializeObject(qc);
			}
			else
			{
				QContact emptyQc = new QContact();
				jsonStr = JsonConvert.SerializeObject(emptyQc);
			}

			Debug.WriteLine("myJson:" + jsonStr);
			return jsonStr;
		}

		async void BtnClickedEvent(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new QRScanPage());
		}
	}
}
