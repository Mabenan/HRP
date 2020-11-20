using HRPData.Entity;
using Simple.OData.Client;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HRP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            GetProducts();
        }

        private async Task<bool> GetProducts()
        {
            try
            {
                var client = new ODataClient("http://localhost:5000/");
                var products = await client.For<BaseProduct>().FindEntriesAsync();
                foreach (var product in products)
                {
                    Console.WriteLine(product.Id);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
