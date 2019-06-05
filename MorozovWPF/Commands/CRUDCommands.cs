using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MorozovWPF.Models;

namespace MorozovWPF.Commands {
    class CRUDCommands {
        private static Orders    OrdersModel    => Orders.Instance;
        private static Consumers consumersModel => Consumers.Instance;
        private static Services  servicesModel  => Services.Instance;

        public static RelayCommand SaveCommand => new RelayCommand(
            actionParameter => {
                switch (actionParameter) {
                    case Order order:
                        if (OrdersModel.GetOrders(it => it.ID == order.ID).Count() != 0) {
                            OrdersModel.EditOrder(order);
                        }
                        else {
                            OrdersModel.AddOrder(order);
                        }

                        break;
                    case Consumer consumer:
                        if (consumersModel.GetConsumers(it => it.ID == consumer.ID).Count() != 0) {
                            consumersModel.EditConsumer(consumer);
                        }
                        else {
                            consumersModel.AddConsumer(consumer);
                        }

                        break;
                    case Service service:
                        if (servicesModel.GetServices(it => it.ID == service.ID).Count() != 0) {
                            servicesModel.EditService(service);
                        }
                        else {
                            servicesModel.AddService(service);
                        }

                        break;
                }
            });

        public static RelayCommand DeleteCommand => new RelayCommand(
            actionParameter => {
                switch (actionParameter) {
                    case Order order:
                        OrdersModel.DeleteOrder(order);
                        break;
                    case Consumer consumer:
                        consumersModel.DeleteConsumer(consumer);
                        break;
                    case Service service:
                        servicesModel.DeleteService(service);
                        break;
                }
            });
    }
}