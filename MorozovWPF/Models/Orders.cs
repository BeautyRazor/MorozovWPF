using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using MorozovWPF.Helpers;

namespace MorozovWPF.Models {
    public class Order : DataModel<Order> {
        private bool     active;
        private Service  service;
        private Consumer consumer;
        private string   description;

        [SQLiteField]
        [DisplayName("Активен")]
        public bool Active {
            get => active;
            set => SetProperty(ref active, value);
        }

        [SQLiteField("ServiceId", convertToId:true)]
        [DisplayName("Услуга")]
        public Service Service {
            get => service;
            set => SetProperty(ref service, value);
        }

        [SQLiteField("ConsumerId", convertToId: true)]
        [DisplayName("Клиент")]
        public Consumer Consumer {
            get => consumer;
            set => SetProperty(ref consumer, value);
        }

        [SQLiteField]
        [DisplayName("Описание")]
        public string Description {
            get => description;
            set => SetProperty(ref description, value);
        }

        public override Order FromDataRow(DataRow row) {
            int i = 0;
            ID          = GetInt(row[i++]);
            Active      = GetBool(row[i++]);
            int consumer = GetInt(row[i++]);
            int service = GetInt(row[i++]);

            Consumer    = Consumers.Instance.ConsumersList.Single(it => it.ID == consumer);
            Service     = Services.Instance.ServicesList.Single(it => it.ID   == service);
            Description = GetString(row[i++]);

            return this;
        }
    }

    class Orders : ModelBase<Orders> {
        public Orders() {
            OrdersList = InitOrders();
        }

        private ObservableCollection<Order> InitOrders() =>
            new ObservableCollection<Order>(DatabaseHelper.GetFullData<Order>(Tables.Orders));

        public ObservableCollection<Order> OrdersList { get; private set; }

        public void               AddOrder(Order              order) {
            DatabaseHelper.AddRow(Tables.Orders, order);
            OrdersList.Add(order);
        }

        public bool               DeleteOrder(Order           order) {
            DatabaseHelper.DeleteRow(Tables.Orders, order);
            return OrdersList.Remove(order);
        }

        public IEnumerable<Order> GetOrders(Func<Order, bool> searchPredicate) => OrdersList.Where(searchPredicate);

        public void EditOrder(Order newOrder) {
            if (OrdersList.Remove(GetOrders(it => it.ID == newOrder.ID).First())) {
                DatabaseHelper.UpdateRow(Tables.Orders, newOrder);
                OrdersList.Add(newOrder);
            }
        }
    }
}