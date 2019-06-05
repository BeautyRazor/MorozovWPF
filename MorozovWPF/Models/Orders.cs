using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MorozovWPF.Models {
    public class Order : DataModel<Order> {
        private bool     active;
        private Service  service;
        private Consumer consumer;
        private string   description;

        [DisplayName("Активен")]
        public bool Active {
            get => active;
            set => SetProperty(ref active, value);
        }

        [DisplayName("Услуга")]
        public Service Service {
            get => service;
            set => SetProperty(ref service, value);
        }

        [DisplayName("Клиент")]
        public Consumer Consumer {
            get => consumer;
            set => SetProperty(ref consumer, value);
        }

        [DisplayName("Описание")]
        public string Description {
            get => description;
            set => SetProperty(ref description, value);
        }
    }

    class Orders : ModelBase<Orders> {
        public Orders() {
            OrdersList = InitOrders();
        }

        private ObservableCollection<Order> InitOrders() {
            return new ObservableCollection<Order>() {
                new Order() {
                    ID = 1,
                    Description = "dk,ffw",
                    Consumer = Consumers.Instance.ConsumersList.First(),
                    Service = Services.Instance.ServicesList.First(),
                    Active = true
                }
            };
        }

        public ObservableCollection<Order> OrdersList { get; private set; }

        public void               AddOrder(Order              order)           => OrdersList.Add(order);
        public bool               DeleteOrder(Order           order)           => OrdersList.Remove(order);
        public IEnumerable<Order> GetOrders(Func<Order, bool> searchPredicate) => OrdersList.Where(searchPredicate);

        public void EditOrder(Order newOrder) {
            if (OrdersList.Remove(GetOrders(it => it.ID == newOrder.ID).First())) {
                OrdersList.Add(newOrder);
            }
        }
    }
}