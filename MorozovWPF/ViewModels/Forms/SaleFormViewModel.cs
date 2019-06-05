using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MorozovWPF.Commands;
using MorozovWPF.Models;

namespace MorozovWPF.ViewModels.Forms {
    public class OrderFormViewModel : ViewModelBase {
        public OrderFormViewModel(Order orderToEdit) {
            if (orderToEdit != null) {
                CurrentOrder = orderToEdit;
            }
            else {
                CurrentOrder = new Order();
                CurrentOrder.ID = Orders.Instance.OrdersList.Max(it => it.ID) + 1;
            }

            Services servicesModel = Services.Instance;
            Consumers consumersModel = Consumers.Instance;

            BindProperties(servicesModel, nameof(servicesModel.ServicesList), nameof(AvailableServices));
            BindProperties(consumersModel, nameof(consumersModel.ConsumersList), nameof(AvailableConsumers));
        }

        public ObservableCollection<Consumer> AvailableConsumers { get; set; }
        public ObservableCollection<Service> AvailableServices { get; set; }

        private Order currentOrder;

        public Order CurrentOrder {
            get => currentOrder;
            set => SetProperty(ref currentOrder, value);
        }

        public RelayCommand SaveCommand => CRUDCommands.SaveCommand;
    }
}