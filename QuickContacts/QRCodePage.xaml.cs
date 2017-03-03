using System;
using System.Collections.Generic;
using ZXing.Net.Mobile.Forms;
using Xamarin.Forms;

namespace QuickContacts
{
	public partial class QRCodePage : ContentPage
	{
		ZXingBarcodeImageView barcode;
		Button btn;
		StackLayout stack;

		public QRCodePage()
		{
			InitializeComponent();

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
			barcode.BarcodeValue = "{name=\"test\",id=\"12345678\"}";

			btn.Clicked += BtnClickedEvent;

			stack.Children.Add(barcode);
			stack.Children.Add(btn);

			Content = stack;
		}

		async void BtnClickedEvent(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new QRScanPage());
		}
	}
}
