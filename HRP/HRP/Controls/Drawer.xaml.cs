using HRP.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HRP.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Drawer : ContentPage
    {

        public delegate void OnItemTappedDelegate(ItemTappedEventArgs e);

        public OnItemTappedDelegate OnItemTapped { get; set; }

        public Drawer()
        {
            InitializeComponent();
        }

        internal void SetItems(List<DrawerItem> masterPageItems)
        {
            this.listView.ItemsSource = masterPageItems;
        }

        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (OnItemTapped != null)
            {
                OnItemTapped.Invoke(e);
            }
        }
    }
}