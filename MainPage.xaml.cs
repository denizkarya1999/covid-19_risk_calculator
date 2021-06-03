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
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace covid_19_risk_calculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int times_pressed = 1;
        int number_of_people_for_int;
        string temp_no_people;
        bool there_are_symptoms = false;
        bool high_risk_places = false;
        bool medium_risk_places = false;
        bool low_risk_places = false;
        public MainPage()
        {
            this.InitializeComponent();
            educational_video.Position = TimeSpan.FromSeconds(0);
            educational_video.Play();
        }

        public void video_start(int times_pressed)
        {
            if (times_pressed % 2 == 0)
                educational_video.Pause();
            else
                educational_video.Play();
        }

        private void number_of_people_GotFocus(object sender, RoutedEventArgs e)
        {
            number_of_people.Text = "";
        }

        private void educational_video_Tapped(object sender, TappedRoutedEventArgs e)
        {
            times_pressed++;
            video_start(times_pressed);
        }

        private void calculate_risk_button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                // Evaluate prevention strategies
            if (fully_vaccinated.IsChecked == true)
                    result.risk_scale -= 10;
                if (mask_wore.IsChecked == true)
                    result.risk_scale -= 5;
                if (not_fully_vaccinated.IsChecked == true)
                    result.risk_scale += 3;
                if (no_mask.IsChecked == true)
                    result.risk_scale += 10;

                //Evaluate outdoor or indoor activity
                if (outdoor_activity.IsChecked == true)
                    result.risk_scale += 5;
                if (indoor_activity.IsChecked == true)
                    result.risk_scale += 3;

                //Evaluate the number of people
                temp_no_people = number_of_people.Text;
                number_of_people_for_int = Int32.Parse(temp_no_people);
                if (number_of_people_for_int > 5)
                {
                    result.risk_scale += 5;
                }
                else if (number_of_people_for_int > 10)
                {
                    result.risk_scale += 10;
                }
                else if (number_of_people_for_int > 20)

                {
                    result.risk_scale += 20;
                }

                //Evaluate the symptoms
                if (fever_chckbox.IsChecked == true || fatigue_chckbox.IsChecked == true || shortness_of_breath_chckbox.IsChecked == true
                    || muscle_or_body_aches_chckbox.IsChecked == true || headche_chckbox.IsChecked == true || loss_of_taste_chckbox.IsChecked == true
                    || sore_throat_chckbox.IsChecked == true || vomitting_chckbox.IsChecked == true) { there_are_symptoms = true; }
                if ((there_are_symptoms && runny_nose_chckbox.IsChecked == true) || (there_are_symptoms))
                {
                    result.risk_scale += 50;
                }
                if (runny_nose_chckbox.IsChecked == true)
                {
                    result.risk_scale += 15;
                }

                //Evaluate social distancing
                if (social_distancing.IsChecked == true)
                {
                    result.risk_scale -= 5;
                }
                if (no_social_distancing.IsChecked == true)
                {
                    result.risk_scale += 10;
                }

                //Evaluate places
                if (Hospital.IsChecked == true || religious_place.IsChecked == true || government_building.IsChecked == true || Funeral.IsChecked == true)
                { high_risk_places = true; }
                if (Gym.IsChecked == true || Sports.IsChecked == true || restaurant.IsChecked == true || Hotel.IsChecked == true || other_outdoor_place.IsChecked == true)
                { medium_risk_places = true; }
                if (School.IsChecked == true || Library.IsChecked == true || other_indoor_place.IsChecked == true) { low_risk_places = true; }
                if (high_risk_places) { result.risk_scale += 35; }
                if (medium_risk_places) { result.risk_scale += 25; }
                if (low_risk_places) { result.risk_scale += 15; }

                this.Frame.Navigate(typeof(result));

            } catch (Exception ex)
            {
                var dialog = new MessageDialog("Please fill out all the information except symptoms if you do not experience any symptoms.");
                dialog.ShowAsync();
            }
        }

        private void about_us_button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dialog = new MessageDialog("COVID-19 Risk Calculator by Deniz K. Acikbas and Hoang Nguyen. Resources: Nucleus Media, CDC");
            dialog.ShowAsync();
        }
    }
}
