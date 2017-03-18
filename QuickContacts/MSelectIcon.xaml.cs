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

					//MSelectIcon.boxLabel.Text = (bool)newValue ? "\u2611" : "\u2610";

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
		}

		public bool IsToggled
		{
			set { SetValue(IsToggledProperty, value); }
			get { return (bool)GetValue(IsToggledProperty); }
		}

		// TapGestureRecognizer handler.
		void OnMSelectIconTapped(object sender, EventArgs args)
		{
			IsToggled = !IsToggled;
			if (!IsToggled)
			{
				iconLabel1.TextColor = Color.FromHex("777777");
			}
			else
			{
				iconLabel1.TextColor = Color.FromHex("#FF4081");
			}
		}
	}
}
