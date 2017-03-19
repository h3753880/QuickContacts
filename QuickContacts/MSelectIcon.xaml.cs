using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace QuickContacts
{
	public partial class MSelectIcon : ContentView
	{
		public static readonly BindableProperty IsToggledProperty =
			BindableProperty.Create(
				"IsToggled",
				typeof(bool),
				typeof(MSelectIcon),
				false,
				propertyChanged: (bindable, oldValue, newValue) =>
				{
					// Set the graphic.
					MSelectIcon msIcon = (MSelectIcon)bindable;

					// Update the image
					if (!(bool)newValue)
					{
						msIcon.seImage.Source = ImageSource.FromResource("QuickContacts.Images.multiselect_off.png");
					}
					else
					{
						msIcon.seImage.Source = ImageSource.FromResource("QuickContacts.Images.multiselect_on.png");
					}

					// Fire the event.
					EventHandler<bool> eventHandler = msIcon.IsToggledChanged;
					if (eventHandler != null)
					{
						eventHandler(msIcon, (bool)newValue);
					}
				});

		public event EventHandler<bool> IsToggledChanged;

		public MSelectIcon()
		{
			InitializeComponent();
			seImage.Source = ImageSource.FromResource("QuickContacts.Images.multiselect_off.png");
		}

		public bool IsToggled
		{
			set { 
				SetValue(IsToggledProperty, value); 
				// Update the image
				if (!IsToggled)
				{
					seImage.Source = ImageSource.FromResource("QuickContacts.Images.multiselect_off.png");
				}
				else
				{
					seImage.Source = ImageSource.FromResource("QuickContacts.Images.multiselect_on.png");
				}
			}
			get { return (bool)GetValue(IsToggledProperty); }
		}

		// TapGestureRecognizer handler.
		void OnMSelectIconTapped(object sender, EventArgs args)
		{
			IsToggled = !IsToggled;
		}
	}
}
