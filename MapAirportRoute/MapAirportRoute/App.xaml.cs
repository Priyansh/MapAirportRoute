using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace MapAirportRoute
{
	public partial class App : Application
	{
        static AirPortRouteDB database;
        public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new MainPage());
		}

        //public static AirPortRouteDB Database
        //{
        //    get
        //    {
        //        if (database == null)
        //        {
        //            database = new AirPortRouteDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AirportRouteSQLite.db3"));
        //        }
        //        return database;
        //    }
        //}

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
