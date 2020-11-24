using HRP.Controller;
using HRP.Structs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        }

        private void OnNav(ItemTappedEventArgs e)
        {
            var drawItem = e.Item as DrawerItem;
            Detail = new NavigationPage((Page)Activator.CreateInstance(drawItem.TargetType));
            IsPresented = false;
        }
    }
}
