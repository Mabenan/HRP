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
                Title = "Contacts",
                IconSource = "contacts.png",
                //TargetType = typeof(ContactsPageCS)
            });
            masterPageItems.Add(new DrawerItem
            {
                Title = "TodoList",
                IconSource = "todo.png",
                //TargetType = typeof(TodoListPageCS)
            });
            masterPageItems.Add(new DrawerItem
            {
                Title = "Reminders",
                IconSource = "reminders.png",
                //TargetType = typeof(ReminderPageCS)
            });
        }
    }
}
