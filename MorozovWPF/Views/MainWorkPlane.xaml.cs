using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MorozovWPF.Models;

namespace MorozovWPF.Views
{
    /// <summary>
    /// Interaction logic for MainWorkPlane.xaml
    /// </summary>
    public partial class MainWorkPlane : UserControl
    {
        public MainWorkPlane()
        {
            InitializeComponent();
        }

        private void OrdersButton_OnClick(object sender, RoutedEventArgs e) {
            DataTable.ItemsSource = null;
            DataTable.ItemsSource = Orders.Instance.OrdersList;
        }

        private void ConsumersButton_OnClick(object sender, RoutedEventArgs e) {
            DataTable.ItemsSource = null;
            DataTable.ItemsSource = Consumers.Instance.ConsumersList;
        }

        private void ServicesButton_OnClick(object sender, RoutedEventArgs e) {
            DataTable.ItemsSource = null;
            DataTable.ItemsSource = Services.Instance.ServicesList;
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
            string displayName = GetPropertyDisplayName(e.PropertyDescriptor);
            if (!string.IsNullOrEmpty(displayName)) {
                e.Column.Header = displayName;
            }
        }

        public static string GetPropertyDisplayName(object descriptor) {

            PropertyDescriptor pd = descriptor as PropertyDescriptor;
            if (pd != null) {
                // Check for DisplayName attribute and set the column header accordingly
                DisplayNameAttribute displayName = pd.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
                if (displayName != null && displayName != DisplayNameAttribute.Default) {
                    return displayName.DisplayName;
                }

            }
            else {
                PropertyInfo pi = descriptor as PropertyInfo;
                if (pi != null) {
                    // Check for DisplayName attribute and set the column header accordingly
                    Object[] attributes = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                    for (int i = 0; i < attributes.Length; ++i) {
                        DisplayNameAttribute displayName = attributes[i] as DisplayNameAttribute;
                        if (displayName != null && displayName != DisplayNameAttribute.Default) {
                            return displayName.DisplayName;
                        }
                    }
                }
            }
            return null;
        }
    }
}
