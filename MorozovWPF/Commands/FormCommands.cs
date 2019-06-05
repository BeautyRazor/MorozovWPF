using System.Collections.ObjectModel;
using System.Windows.Controls;
using MorozovWPF.Models;
using MorozovWPF.ViewModels.Forms;
using MorozovWPF.Views.Forms;

namespace MorozovWPF.Commands {
    class FormCommands {
        
        public static RelayCommand OpenEditFormCommand => new RelayCommand(
            actionParameter => {
                if (actionParameter is  DataGrid table) {
                    switch (table.ItemsSource) {
                        case ObservableCollection<Order> _:
                            var OrderViewModel = new OrderFormViewModel((Order) table.SelectedItem);
                            var OrderForm = new OrderForm(OrderViewModel);
                            OrderForm.ShowDialog();
                            break;
                        case ObservableCollection<Consumer> _:
                            var consumerViewModel = new ConsumerFormViewModel((Consumer) table.SelectedItem);
                            var consumerForm = new ConsumerForm(consumerViewModel);
                            consumerForm.ShowDialog();
                            break;
                        case ObservableCollection<Service> _:
                            var serviceViewModel = new ServiceFormViewModel((Service)table.SelectedItem);
                            var serviceForm = new ServiceForm(serviceViewModel);
                            serviceForm.ShowDialog();
                            break;
                        default:
                            return;
                    }
                }
            });

        public static RelayCommand OpenAddFormCommand => new RelayCommand(
            actionParameter => {
                if (actionParameter is DataGrid table) {
                    switch (table.ItemsSource) {
                        case ObservableCollection<Order> _:
                            var OrderForm      = new OrderForm();
                            OrderForm.ShowDialog();
                            break;
                        case ObservableCollection<Consumer> _:
                            var consumerForm      = new ConsumerForm();
                            consumerForm.ShowDialog();
                            break;
                        case ObservableCollection<Service> _:
                            var serviceForm      = new ServiceForm();
                            serviceForm.ShowDialog();
                            break;
                        default:
                            return;
                    }
                }
            });

    }
}
