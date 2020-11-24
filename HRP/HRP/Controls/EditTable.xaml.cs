using HRP.Controller;
using Simple.OData.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HRP.Controls
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTable : ContentView
    {

        public static readonly BindableProperty TypeToDisplayProperty = BindableProperty.Create("TypeToDisplay", typeof(Type), typeof(EditTable), null, BindingMode.TwoWay, propertyChanged: TypeToDisplayChanged);

        public Type TypeToDisplay { get; set; }

        ObservableCollection<object> items = new ObservableCollection<object>();
        public ObservableCollection<object> Items { get { return items; } }

        private static void TypeToDisplayChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as EditTable;
            control.TypeToDisplay = newValue as Type;

            control.ChangeLayout();


            var method = control.GetType().GetMethod("Init");
            method.MakeGenericMethod(control.TypeToDisplay).Invoke(control, null);
        }

        public EditTable()
        {
            InitializeComponent();

            BindingContext = this;


        }

        private void ChangeLayout()
        {
            var datagrid = new Grid();
            var headergrid = new StackLayout();
            var props = this.TypeToDisplay.GetProperties();

            foreach (var prop in props)
            {
                var element = new Label();
                element.SetBinding(Label.TextProperty, prop.Name);

                var header = new Label();
                header.Text = prop.Name;

                headergrid.Children.Add(header);

                datagrid.Children.Add(element);
            }

            List.Header = headergrid;
            List.ItemTemplate = new DataTemplate(() => { return new ViewCell { View = datagrid }; });

        }

        public void Init<T>() where T : class
        {
            new Task(new Action(async () =>
            {
                
                var result = await DataProvider.Client.For<T>().FindEntriesAsync();
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    this.Items.Clear();
                    foreach (T entry in result)
                    {
                        this.Items.Add(entry);
                    }
                });
            })).Start();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}