using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace EfeApp
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            Grid g = new Grid();
            RowDefinition rd1 = new RowDefinition();
            rd1.Height = new GridLength(1,GridUnitType.Star);
            RowDefinition rd2 = new RowDefinition();
            rd1.Height = new GridLength(1, GridUnitType.Star);
            g.RowDefinitions.Add(rd1);
            g.RowDefinitions.Add(rd2);
            Button b = new Button();
            Grid.SetRow(b, 1);
            b.Text = "Bana Tikla";
            g.Children.Add(b);
            this.MainPage = new ContentPage();
            ((ContentPage)this.MainPage).Content = g;
            b.Clicked += B_Clicked;


        }

        private void B_Clicked(object sender, EventArgs e)
        {
           this.MainPage.DisplayAlert("Title","Bana Tikladin", "cancel");
        }

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
