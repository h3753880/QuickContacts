using System;
using System.Collections.Generic;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace QuickContacts
{
	public partial class QRScanPage : ContentPage
	{
		ZXingScannerView zxing;
		ZXingDefaultOverlay overlay;

		public QRScanPage()
		{
			Padding = Device.OnPlatform(new Thickness(0, 20, 0, 0), new Thickness(), new Thickness());

			zxing = new ZXingScannerView
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};
			zxing.OnScanResult += (result) =>
				Device.BeginInvokeOnMainThread(async () =>
				{
					// Stop analysis until we navigate away so we don't keep reading barcodes
					zxing.IsAnalyzing = false;
					zxing.IsScanning = false;
					// Store result
					var answer = await DisplayAlert(result.Text, "Bob Stone Scanned!", "Done", "Export");
					// Export
					if (!answer)
					{
						// export data to contacts
					}	
					// Navigate away
					await Navigation.PopModalAsync();
				});

			overlay = new ZXingDefaultOverlay
			{
				TopText = "Hold your phone up to the barcode",
				BottomText = "Scanning will happen automatically",
				ShowFlashButton = zxing.HasTorch
			};
			overlay.FlashButtonClicked += (sender, e) =>
			{
				zxing.IsTorchOn = !zxing.IsTorchOn;
			};

			var grid = new Grid();
			grid.RowDefinitions.Add(new RowDefinition { Height = 50 });
			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			Button cancelBtn = new Button
			{
				Text = "Cancel",
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			cancelBtn.Clicked += OnCancelBtnClicked;
			grid.Children.Add(cancelBtn, 0, 0);
			grid.Children.Add(zxing, 0, 1);
			grid.Children.Add(overlay, 0, 1);

			// The root page of your application
			Content = grid;
		}

		async void OnCancelBtnClicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			zxing.IsScanning = true;
		}

		protected override void OnDisappearing()
		{
			zxing.IsScanning = false;

			base.OnDisappearing();
		}
	}
}
