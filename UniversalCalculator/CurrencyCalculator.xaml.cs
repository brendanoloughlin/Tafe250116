using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class CurrencyCalculator : Page
	{
		//Conversion Rates constants
		private const double USD_TO_EURO = 0.85189982;
		private const double USD_TO_POUND = 0.72872436;
		private const double USD_TO_INR = 74.257327;

		private const double EURO_TO_USD = 1.1739732;
		private const double EURO_TO_POUND = 0.8556672;
		private const double EURO_TO_INR = 87.00755;

		private const double POUND_TO_USD = 1.371907;
		private const double POUND_TO_EURO = 1.1686692;
		private const double POUND_TO_INR = 101.68635;

		private const double INR_TO_USD = 0.011492628;
		private const double INR_TO_EURO = 0.013492774;
		private const double INR_TO_POUND = 0.0098339397;

		private string conversionRate = "";
		public CurrencyCalculator()
		{
			this.InitializeComponent();
		}
		// Conversion Button Click
		private async void Conversion_Button_Click(object sender, RoutedEventArgs e)
		{
			int amount;

			// Check if the entered amount is a valid number and not empty
			if (string.IsNullOrEmpty(EnterAmount.Text))
			{
				var dialogMessage = new MessageDialog("Error! Please enter a valid amount");
				await dialogMessage.ShowAsync();
				EnterAmount.Focus(FocusState.Programmatic);
				EnterAmount.SelectAll();
				return;
			}

			// Try and catch errors for invalid amount input
			try
			{
				amount = int.Parse(EnterAmount.Text);
			}
			catch
			{
				var dialogMessage = new MessageDialog("Error! Please enter a valid amount");
				await dialogMessage.ShowAsync();
				EnterAmount.Focus(FocusState.Programmatic);
				EnterAmount.SelectAll();
				return;
			}

			MoneyToConvertTextBox.Text = amount.ToString();

			double currency_from = 0;

			// Convert the amount based on the selected "From" currency
			switch (FromComboBox.SelectedIndex)
			{
				case 0: // USD
					currency_from = CurrencyFromUSD(amount); // Convert USD to target currency
					break;
				case 1: // EUR
					currency_from = CurrencyFromEUR(amount); // Convert EUR to target currency
					break;
				case 2: // Pound
					currency_from = CurrencyFromPOUND(amount); // Convert Pound to target currency
					break;
				case 3: // INR
					currency_from = CurrencyFromINR(amount); // Convert INR to target currency
					break;
				default:
					currency_from = amount; // Default case (USD to USD)
					break;
			}
			// Display the converted amount
			TotalAmountConverted.Text = currency_from.ToString();
			ConversionRateTextBox.Text = conversionRate;
		}
		//METHODS
		private double CurrencyFromUSD(double amount)
		{
			double currency_from = amount;

			// Convert USD to the selected target currency
			switch (ToCurrencyComboBox.SelectedIndex)
			{
				case 0: // USD to USD
					currency_from = amount;
					conversionRate = "1 USD = 1 USD";
					break;
				case 1: // USD to EUR
					currency_from = amount * USD_TO_EURO;
					conversionRate = $"1 USD = {USD_TO_EURO} EUR";
					break;
				case 2: // USD to Pound
					currency_from = amount * USD_TO_POUND;
					conversionRate = $"1 USD = {USD_TO_POUND} GBP";
					break;
				case 3: // USD to INR
					currency_from = amount * USD_TO_INR;
					conversionRate = $"1 USD = {USD_TO_INR} INR";
					break;
				default:// Default case (USD to USD)
					currency_from = amount;
					conversionRate = "1 USD = 1 USD";
					break;
			}

			return currency_from;
		}

		private double CurrencyFromEUR(double amount)
		{
			double currency_from = amount;

			// Convert EUR to the selected target currency
			switch (ToCurrencyComboBox.SelectedIndex)
			{
				case 0: // EUR to USD
					currency_from = amount * EURO_TO_USD;
					conversionRate = $"1 EUR = {EURO_TO_USD} USD";
					break;
				case 1: // EUR to EUR
					currency_from = amount;
					conversionRate = "1 EUR = 1 EUR";
					break;
				case 2: // EUR to Pound
					currency_from = amount * EURO_TO_POUND;
					conversionRate = $"1 EUR = {EURO_TO_POUND} GBP";
					break;
				case 3: // EUR to INR
					currency_from = amount * EURO_TO_INR;
					conversionRate = $"1 EUR = {EURO_TO_INR} INR";
					break;
				default:// Default case (EUR to EUR)
					currency_from = amount;
					conversionRate = "1 EUR = 1 EUR";
					break;
			}

			return currency_from;
		}

		private double CurrencyFromPOUND(double amount)
		{
			double currency_from = amount;

			// Convert Pound to the selected target currency
			switch (ToCurrencyComboBox.SelectedIndex)
			{
				case 0: // Pound to USD
					currency_from = amount * POUND_TO_USD;
					conversionRate = $"1 GBP = {POUND_TO_USD} USD";
					break;
				case 1: // Pound to EUR
					currency_from = amount * POUND_TO_EURO;
					conversionRate = $"1 GBP = {POUND_TO_EURO} EUR";
					break;
				case 2: // Pound to Pound
					currency_from = amount;
					conversionRate = "1 GBP = 1 GBP";
					break;
				case 3: // Pound to INR
					currency_from = amount * POUND_TO_INR;
					conversionRate = $"1 GBP = {POUND_TO_INR} INR";
					break;
				default:// Default case (Pound to Pound)
					currency_from = amount;
					conversionRate = "1 GBP = 1 GBP";
					break;
			}

			return currency_from;
		}
		private double CurrencyFromINR(double amount)
		{
			double currency_from = amount;

			// Convert INR to the selected target currency
			switch (ToCurrencyComboBox.SelectedIndex)
			{
				case 0: // INR to USD
					currency_from = amount * INR_TO_USD;
					conversionRate = $"1 INR = {INR_TO_USD} USD";
					break;
				case 1: // INR to EUR
					currency_from = amount * INR_TO_EURO;
					conversionRate = $"1 INR = {INR_TO_EURO} EUR";
					break;
				case 2: // INR to Pound
					currency_from = amount * INR_TO_POUND;
					conversionRate = $"1 INR = {INR_TO_POUND} GBP";
					break;
				case 3: // INR to INR
					currency_from = amount;
					conversionRate = "1 INR = 1 INR";
					break;
				default:// Default case (INR to INR)
					currency_from = amount;
					conversionRate = "1 INR = 1 INR";
					break;
			}

			return currency_from;
		}
		private void ExitButtonClick_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainMenu));

		}
	}
}

