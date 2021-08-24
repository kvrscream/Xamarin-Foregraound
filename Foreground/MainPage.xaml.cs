using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Service.QuickSettings;
using Xamarin.Forms;

namespace Foreground
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<MainPage>(this, "show", (msg) =>
            {
                texto.Text = DateTime.Today.ToString("dd:MM:yyyy HH:mm:ss");
            });

        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<MainPage>(this, "show");
        }

    }
}
