using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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

	public sealed partial class MortgageCalculator : Page
	{

		public MortgageCalculator()
		{
			this.InitializeComponent();
		}

		private void Calculate(object sender, RoutedEventArgs e)
		{
			/// M = P [ i(1 + i)^n ] / [ (1 + i)^n – 1]
			/// P = principal loan amount
			/// i = monthly interest rate
			/// n = number of months required to repay the loan
			int principal = 0;
			double yearlyRate = 0.0;
			double monthlyRate = 0.0;
			double monthlyRepayment = 0.0;
			int payments;

			if (principalBorrowBox.Text == "") 
			{
				return;
			}
			else if (yearsBox.Text == "")
			{
				return;
			}
			else if (monthsBox.Text == "")
			{
				return;
			}
			else if (yearlyRateBox.Text == "")
			{
				return;
			}
			else
			principal = int.Parse(principalBorrowBox.Text);
			payments = (int.Parse(yearsBox.Text) * 12) + int.Parse(monthsBox.Text);
			yearlyRate = double.Parse(yearlyRateBox.Text) / 100;
			monthlyRate = yearlyRate / 12;
			monthlyRepayment = Math.Round(principal * (monthlyRate * Math.Pow(1 + monthlyRate, payments)) /
				(Math.Pow(1 + monthlyRate, payments) - 1), 3);

			monthlyRateBox.Text = Convert.ToString(Math.Round(monthlyRate, 4)) + "%";
			var monthlyRepaymentStr = string.Format("{0:N}", monthlyRepayment);
			monthlyRepaymentBox.Text = monthlyRepaymentStr;
		}

		private void Exit(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(MainMenu));
		}
    }
}
