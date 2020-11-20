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
        public Drawer()
        {
            InitializeComponent();
        }

        internal void SetItems(List<DrawerItem> masterPageItems)
        {
            this.listView.ItemsSource = masterPageItems;
        }
    }
}