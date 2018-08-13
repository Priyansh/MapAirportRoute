using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using System.Collections.ObjectModel;
using System.IO;
using CsvHelper;
using System.Reflection;

namespace MapAirportRoute
{
	public partial class MainPage : ContentPage
	{
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<string> _airportsObservableCollection;
		public MainPage()
		{
			InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
		}

        protected override void OnAppearing()
        {
            //Delete 
            /*var airportEach = _airportsObservableCollection[0];
            await _connection.ExecuteAsync("Delete From Airports");
            _airportsObservableCollection.Remove(airportEach);*/

            /*await _connection.CreateTableAsync<AirPorts>();
            var airports = await _connection.Table<AirPorts>().ToListAsync();
            var result = (from xy in airports
                         select xy.Name);
            _airportsObservableCollection = new ObservableCollection<string>(result);

            //prOriginIATA.ItemsSource = _airportsObservableCollection;*/

            base.OnAppearing();
        }

        private async void btnShowRoutes_Clicked(object sender, EventArgs e)
        {
            if(txtOriginIATA.Text != null && txtDestinationIATA.Text != null)
            {
                //Excel code
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("MapAirportRoute.Files.routes.csv");
                List<Routes> lstRoutes = new List<Routes>();
                using (var reader = new System.IO.StreamReader(stream))
                {
                    var csvReader = new CsvReader(reader);
                    var records = csvReader.GetRecords<Routes>().ToList();
                    var result = records.Where(r => r.Origin == txtOriginIATA.Text.ToUpper() && r.Destination == txtDestinationIATA.Text.ToUpper()).ToList();

                    /*foreach (var record in records)
                    {
                        lstRoutes.Add(record);
                        
                    }
                    var result = lstRoutes.Where(r => r.Origin == txtOriginIATA.Text.ToUpper() && r.Destination == txtDestinationIATA.Text.ToUpper()); */


                    /*var receipeNew = new Routes { Origin = record.Origin, Destination = record.Destination };
                        await _connection.InsertAsync(receipeNew);
                        _airportsObservableCollection.Add(receipeNew.Name);*/

                    if (result.Count > 0)
                    {
                        //get latitude & longitude
                        var foundCoordinates = getGeoCordinates(result[0]);
                        await Navigation.PushAsync(new ViewMapPath(foundCoordinates));
                    }
                    else
                    {
                        await DisplayAlert("RouteError", "Route not Exist, Please Add Feedback", "OK");
                    }
                    
                }
                 
            }
            else if(txtOriginIATA.Text != null || txtDestinationIATA.Text != null)
            {
                await DisplayAlert("Missing Info Alert", "Please Enter Origin or Destination IATA", "OK");
            }
            
        }

        private List<AirPortsCSV> getGeoCordinates(Routes route)
        {
            //Excel code
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("MapAirportRoute.Files.airports.csv");
            List<AirPortsCSV> lstAirportCoordinates = new List<AirPortsCSV>();
            using (var reader = new System.IO.StreamReader(stream))
            {
                var csvReader = new CsvReader(reader);
                var records = csvReader.GetRecords<AirPortsCSV>().ToList();
                var originCoorinates = records.Where(r => r.IATA == route.Origin).ToList();
                var destinationCoordinates = records.Where(r => r.IATA == route.Destination).ToList();

                lstAirportCoordinates.Add(new AirPortsCSV { Name = originCoorinates[0].Name, City = originCoorinates[0].City, Country = originCoorinates[0].Country, IATA = originCoorinates[0].IATA, Latitute = originCoorinates[0].Latitute, Longitude = originCoorinates[0].Longitude});
                lstAirportCoordinates.Add(new AirPortsCSV { Name = destinationCoordinates[0].Name, City = destinationCoordinates[0].City, Country = destinationCoordinates[0].Country, IATA = destinationCoordinates[0].IATA, Latitute = destinationCoordinates[0].Latitute, Longitude = destinationCoordinates[0].Longitude });

                /*foreach (var record in records)
                {
                    if (record.IATA != "\\N" || record.IATA != null)
                    {
                        var airportNew = new AirPorts { IATA = record.IATA };
                        await _connection.InsertAsync(airportNew);
                        _airportsObservableCollection.Add(airportNew.IATA);
                    }
                }*/
            }
            return lstAirportCoordinates;
        }

        private async void btnSubmitFeedback_Clicked(object sender, EventArgs e)
        {
            if(txtFeedback.Text != null)
            {
                await DisplayAlert("Feedback Submission", "Thank you for submitting feedback", "OK");
                txtFeedback.Text = "";
            }
            else
            {
                await DisplayAlert("Error!!", "Please Add Proper Feedback", "OK");
            } 
            
        }

        /*private void prOriginIATA_SelectedIndexChanged(object sender, EventArgs e)
        {
            var originItem = prOriginIATA.Items[prOriginIATA.SelectedIndex];
            DisplayAlert("Alert", originItem, "OK"); 
        } */

        /*private void prDestinationIATA_SelectedIndexChanged(object sender, EventArgs e)
        {
            var destinationItem = prDestinationIATA.Items[prDestinationIATA.SelectedIndex];
            DisplayAlert("Alert", destinationItem, "OK");
        } */
    }
}
