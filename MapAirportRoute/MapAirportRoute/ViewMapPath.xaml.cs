using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;

namespace MapAirportRoute
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewMapPath : ContentPage
	{
        List<AirPortsCSV> lstCoordinates = new List<AirPortsCSV>();
        public ViewMapPath (List<AirPortsCSV> lstCoordinates)
		{
			InitializeComponent ();
            this.lstCoordinates = lstCoordinates;
            
        }

        protected override void OnAppearing()
        {
            
            foreach (var item in this.lstCoordinates)
            {
                double latitute = Convert.ToDouble(item.Latitute);
                double longitude = Convert.ToDouble(item.Longitude);
                var position = new Position(latitute, longitude); // Latitude, Longitude
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = position,
                    Label = item.IATA,
                    Address = item.City + " " + item.Country
                };
                MyMap.Pins.Add(pin);
                MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(new Position(latitute, longitude), Distance.FromMiles(1500)));
            }
        }
    }
}