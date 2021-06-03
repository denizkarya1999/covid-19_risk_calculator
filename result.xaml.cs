using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace covid_19_risk_calculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class result : Page
    {
        public static int risk_scale = 0;
        public result()
        {
            this.InitializeComponent();
            if (risk_scale <= 25)
            {
              risk_label.Text = "Low";
            }
            else if (risk_scale > 25 && risk_scale <= 55)
            {
              risk_label.Text = "Medium";
            }
            else if (risk_scale > 55)
            {
              risk_label.Text = "High";
            }
            set_testing_status(risk_label.Text);
        }

        private void set_testing_status(string risk_type)
        {
            if (risk_type.Equals("Low"))
            {
                testing_status.Text = "You do not have to get tested for COVID-19 based on this activity.\n" +
                    "If you are really worried about your activity or you can get tested for COVID-19, the window period for COVID-19 is usually 2 to 15 days.";
            }
            if (risk_type.Equals("Medium"))
            {
                testing_status.Text = "You may need to get tested for COVID-19 based on this activity.\n" +
                    "Based on your case, it is likely that you have contracted SARS COV-2 virus. The window period for COVID-19 is usually 2 to 15 days." +
                    "Earlier diagnosis saves lives and protect others.";
            }
            if (risk_type.Equals("High"))
            {
                testing_status.Text = "You definitely need to get tested for COVID-19 based on this activity.\n" +
                    "Based on your case, it is highly likely that you have contracted SARS COV-2 virus. The window period for COVID-19 is usually 2 to 15 days. " +
                    "Earlier diagnosis saves lives and protects others.";
            }
        }

        private void back_button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            result.risk_scale = 0;
            this.Frame.Navigate(typeof(MainPage));
        }

        private void about_covid_button_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

    }
}
