using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace QuickContacts
{
	public partial class CheckBox : ContentView
	{
		public static readonly BindableProperty IsCheckedProperty =
			BindableProperty.Create(
				"IsChecked",
				typeof(bool),
				typeof(CheckBox),
				false,
				propertyChanged: (bindable, oldValue, newValue) =>
				{
					CheckBox checkbox = (CheckBox)bindable;
					
					// Update the image
					if (!(bool)newValue)
					{
						checkbox.boxImage.Source = ImageSource.FromResource("QuickContacts.Images.checkbox_uncheck.png");
					} 
					else 
					{
						checkbox.boxImage.Source = ImageSource.FromResource("QuickContacts.Images.checkbox_tick.png");
					}
					
					// Fire the event.
					EventHandler<bool> eventHandler = checkbox.CheckedChanged;
					if (eventHandler != null)
					{
						eventHandler(checkbox, (bool)newValue);
					}
				});

		public event EventHandler<bool> CheckedChanged;

		public CheckBox()
		{
			InitializeComponent();
			boxImage.Source = ImageSource.FromResource("QuickContacts.Images.checkbox_uncheck.png");
		}

		public bool IsChecked
		{
			set { 
				SetValue(IsCheckedProperty, value);
				// Update the image
				if (IsChecked)
				{
					boxImage.Source = ImageSource.FromResource("QuickContacts.Images.checkbox_tick.png");
				}
				else
				{
					boxImage.Source = ImageSource.FromResource("QuickContacts.Images.checkbox_uncheck.png");
				}
			}
			get { return (bool)GetValue(IsCheckedProperty); }
		}

		// TapGestureRecognizer handler.
		void OnCheckBoxTapped(object sender, EventArgs args)
		{
			IsChecked = !IsChecked;
		}
	}
}
