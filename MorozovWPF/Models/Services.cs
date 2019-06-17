using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using MorozovWPF.Helpers;

namespace MorozovWPF.Models {
    public class Service : DataModel<Service>, IConvertiblrToId { 
        private string title;
        private string description;
        private double price;

        [SQLiteField]
        [DisplayName("Название")]
        public string Title {
            get => title;
            set => SetProperty(ref title, value);
        }

        [SQLiteField()]
        [DisplayName("Описание")]
        public string Description {
            get => description;
            set => SetProperty(ref description, value);
        }

        [SQLiteField]
        [DisplayName("Стоимость")]
        public double Price {
            get => price;
            set => SetProperty(ref price, value);
        }

        public override string ToString() => Title;
        public int convertToId() {
            return ID;
        }

        public override Service FromDataRow(DataRow row) {
            int i = 0;
            ID          = GetInt(row[i++]);
            Title       = GetString(row[i++]);
            Description = GetString(row[i++]);
            Price       = GetDouble(row[i++]);
            return this;
        }
    }

    class Services : ModelBase<Services> {
        public Services() {
            ServicesList = InitServices;
        }

        private static ObservableCollection<Service> InitServices =>
            new ObservableCollection<Service>(DatabaseHelper.GetFullData<Service>(Tables.Services));

        public ObservableCollection<Service> ServicesList { get; private set; }

        public void                 AddService(Service              newService) {
            DatabaseHelper.AddRow(Tables.Services, newService);
            ServicesList.Add(newService);
        }

        public IEnumerable<Service> GetServices(Func<Service, bool> searchPredicate) => ServicesList.Where(searchPredicate);
        public bool                 DeleteService(Service           service) {
            DatabaseHelper.DeleteRow(Tables.Services, service);
            return ServicesList.Remove(service);
        }

        public void EditService(Service newService) {
            if (ServicesList.Remove(GetServices(it => it.ID == newService.ID).First())) {
                DatabaseHelper.UpdateRow(Tables.Services, newService);
                ServicesList.Add(newService);
            }
        }
    }
}