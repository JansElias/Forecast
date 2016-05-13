using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Windows.UI.Popups;
using Forecast2.Common;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;


// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Forecast2
{

     

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public MainPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            if (App.WO.cityName != null)
            {
                txtPlaats.Text = App.WO.cityName;
            }
            else
            {
                txtPlaats.Text = "";
            }

            if (App.WO.tempMeter == "celsius")
            {
                rbCelsius.IsChecked = true;
            }
            if (App.WO.tempMeter == "imperial")
            {
                rbFahrenheit.IsChecked = true;
            }
            if (App.WO.tempMeter == "")
            {
                rbKelvin.IsChecked = true;
            }
            
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void btnGetData_Click(object sender, RoutedEventArgs e)
        {

            if (txtPlaats.Text == "")
            {
                MessageDialog msgbox = new MessageDialog("No city was found.");
                await msgbox.ShowAsync();
            }
            else
            {
                App.WO.tempMeter = "celsius";

                if (rbFahrenheit.IsChecked == true)
                {
                    App.WO.tempMeter = "imperial";
                }
                if (rbCelsius.IsChecked == true)
                {
                    App.WO.tempMeter = "metric";
                }
                if (rbKelvin.IsChecked == true)
                {
                    App.WO.tempMeter = "";
                }


                App.WO.cityName = txtPlaats.Text;

                var myList = new List<string>()
                {
                App.WO.cityName,
                App.WO.tempMeter,
                };



                Frame.Navigate(typeof(DataPage), myList);
            }

            


            //if (!Frame.Navigate(typeof(DataPage), city))
            //{
            //    throw new Exception("Navigation failed.");
            //}


            //try
            //{
            //    using (HttpClient client = new HttpClient())
            //    {
            //        pbWeather.Visibility = Visibility.Visible;
            //        client.BaseAddress = new Uri("http://api.openweathermap.org");

            //        var url = "data/2.5/forecast/daily?q="+city+"&mode=json&units="+tempMeter+"&cnt=7&APPID=9ddd4403f5f5ee8c9504363e8908598d";

            //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //        HttpResponseMessage response = await client.GetAsync(String.Format(url));

            //        if (response.IsSuccessStatusCode)
            //        {
            //            var data = response.Content.ReadAsStringAsync();
            //            var weatherdata = JsonConvert.DeserializeObject<WeatherObject>(data.Result.ToString());

            //            spWeatherInfo.DataContext = weatherdata;

            //        }

            //        pbWeather.Visibility = Visibility.Collapsed;

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageDialog dialog = new MessageDialog("Some Error Has Occured");
            //    //await dialog.ShowAsync();
            //    pbWeather.Visibility = Visibility.Collapsed;
            //    //}



            //}



        }

        private void btnPostcode_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGemeente_Click(object sender, RoutedEventArgs e)
        {

        }

        private void rbCelsius_Checked(object sender, RoutedEventArgs e)
        {
            //if (rbCelsius.IsChecked == true)
            //{
            //    rbFahrenheit.IsChecked = false;
            //}
            //else
            //{
            //    rbCelsius.IsChecked = true;
            //}
            
        }

        private void rbFahrenheit_Checked(object sender, RoutedEventArgs e)
        {
            //if (rbFahrenheit.IsChecked == true)
            //{
            //    rbCelsius.IsChecked = false;
            //}
            //else
            //{
            //    rbCelsius.IsChecked = true;
            //}
        }

        private void rbCelsius_Click(object sender, RoutedEventArgs e)
        {
            if (rbCelsius.IsChecked == true)
            {
                rbFahrenheit.IsChecked = false;
                rbKelvin.IsChecked = false;
            }
            else
            {
                rbCelsius.IsChecked = true;
            }
        }

        private void rbFahrenheit_Click(object sender, RoutedEventArgs e)
        {
            if (rbFahrenheit.IsChecked == true)
            {
                rbCelsius.IsChecked = false;
                rbKelvin.IsChecked = false;
            }
            else
            {
                rbCelsius.IsChecked = true;
            }
        }

        private void rbKelvin_Click(object sender, RoutedEventArgs e)
        {
            if (rbKelvin.IsChecked == true)
            {
                rbCelsius.IsChecked = false;
                rbFahrenheit.IsChecked = false;
            }
            else
            {
                rbCelsius.IsChecked = true;
            }
        }

        private async void btnGetLocation_Click(object sender, RoutedEventArgs e)
        {
            pbWeather.Visibility = Visibility.Visible;
            Geolocator geolocator = new Geolocator();
            Geoposition geoposition = await geolocator.GetGeopositionAsync();
 
            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAtAsync(geoposition.Coordinate.Point);
            if (result.Status == MapLocationFinderStatus.Success){
                MapAddress address = result.Locations.FirstOrDefault().Address;
            string fullAddress = string.Format("{0}", address.Town);
            txtPlaats.Text = fullAddress;
            }

            pbWeather.Visibility = Visibility.Collapsed;

            //Longitude.Text = position.Coordinate.Point.Position.Longitude.ToString();
            
        }


    }
}
