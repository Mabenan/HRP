using HRP.Controller;
using HRP.Structs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HRP
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            masterPage.SetItems(new NavigationList().MasterPageItems);
            masterPage.OnItemTapped += this.OnNav;
            new Task(new Action(async () =>
            {
                var server = Xamarin.Essentials.SecureStorage.GetAsync("server");
                var user = Xamarin.Essentials.SecureStorage.GetAsync("user");
                var pass = Xamarin.Essentials.SecureStorage.GetAsync("pass");

                server.Wait();
                    server = DisplayPromptAsync("Server", "Enter Server Name");

                user.Wait();
                    user = DisplayPromptAsync("User", "Enter Server Name");

                pass.Wait();
                    pass = DisplayPromptAsync("Pass", "Enter Server Name");


                server.Wait();
                user.Wait();
                pass.Wait();
                var credentials = new NetworkCredential(user.Result, pass.Result);
                //await Xamarin.Essentials.SecureStorage.SetAsync("server", server.Result);
                //await Xamarin.Essentials.SecureStorage.SetAsync("user", user.Result);
                //await Xamarin.Essentials.SecureStorage.SetAsync("pass", pass.Result);
                DataProvider.Init(server.Result, credentials);

            })).Start();
        }

        private void OnNav(ItemTappedEventArgs e)
        {
            var drawItem = e.Item as DrawerItem;
            Detail = new NavigationPage((Page)Activator.CreateInstance(drawItem.TargetType));
            IsPresented = false;
        }
    }
}
