using HRP.Sites;
using HRP.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HRP.Controller
{
    public class NavigationList
    {
        public List<DrawerItem> MasterPageItems { get { return masterPageItems; } }

        List<DrawerItem> masterPageItems;

        public NavigationList()
        {
            masterPageItems = new List<DrawerItem>();
            masterPageItems.Add(new DrawerItem
            {
                Title = "Dashboard",
                //IconSource = "contacts.png",
                TargetType = typeof(Dashboard)
            });
            masterPageItems.Add(new DrawerItem
            {
                Title = "EditableProducts",
                //IconSource = "contacts.png",
                TargetType = typeof(EditableProductsEdit)
            });
        }
    }
}
