using System;
using System.Globalization;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Calculator
{
    /// <summary>
    /// Universal calculator page for determining the amount
	/// to pay for a given day trip.
    /// </summary>
    public sealed partial class TripCalculatorPage : Page
    {
		public TripCalculatorPage()
        {
            this.InitializeComponent();
        }

		private void DateTodayClickEvent(object sender, RoutedEventArgs e)
		{
			dateField.Date = DateTimeOffset.Now;
		}

		private void ResetButtonClick(object sender, RoutedEventArgs e)
		{
			dateField.Date = null;
			startingKmsField.Text = "";
			endingKmsField.Text = "";
			hiredDaysField.Text = "";
			pricePerDayField.Text = "";
			amountPayField.Text = "";
		}

		private async void CalculateButtonClick(object sender, RoutedEventArgs e)
		{
			if (!dateField.Date.HasValue
				|| string.IsNullOrWhiteSpace(startingKmsField.Text)
				|| string.IsNullOrWhiteSpace(endingKmsField.Text)
				|| string.IsNullOrWhiteSpace(hiredDaysField.Text)
				|| string.IsNullOrWhiteSpace(pricePerDayField.Text))
			{
				await ShowMessageDialog("Please ensure all fields are completed").ShowAsync();
				return;
			}

			if (!int.TryParse(startingKmsField.Text, out _))
			{
				await ShowMessageDialog("Please enter a whole number into the Starting Kms field").ShowAsync();
				return;
			}

			if (!int.TryParse(endingKmsField.Text, out _))
			{
				await ShowMessageDialog("Please enter a whole number into the Ending Kms field").ShowAsync();
				return;
			}

			if (!int.TryParse(hiredDaysField.Text, out var hiredDays))
			{
				await ShowMessageDialog("Please enter a whole number into the Days Hired field").ShowAsync();
				return;
			}

			if (!double.TryParse(pricePerDayField.Text, out var pricePerDay))
			{
				await ShowMessageDialog("Please enter a valid price into the Price p/Day field").ShowAsync();
				return;
			}

			amountPayField.Text = string.Format("{0:C2}", hiredDays * pricePerDay);

		}

		private async void ValidateInt(object sender, RoutedEventArgs e)
		{
			if (sender.GetType() != typeof(TextBox))
				return;

			if (!int.TryParse(((TextBox)sender).Text, out _))
			{
				await ShowMessageDialog($"{((TextBox)sender).Text} is invalid. Please enter a whole number into the {((TextBox)sender).Name}").ShowAsync();
				((TextBox)sender).Text = "";
				return;
			}
		}

		private async void ValidateDouble(object sender, RoutedEventArgs e)
		{
			if (sender.GetType() != typeof(TextBox))
				return;

			if (!double.TryParse(((TextBox)sender).Text, out _))
			{
				await ShowMessageDialog($"{((TextBox)sender).Text} is invalid. Please enter a decimal number into the {((TextBox)sender).Name}").ShowAsync();
				((TextBox)sender).Text = "";
				return;
			}
		}

		private void ExitButtonClick(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainMenu));
		}

		private MessageDialog ShowMessageDialog(string message, string title = "Input Validation")
		{
			return new MessageDialog(message, title);
		}
	}
}
