using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MorozovWPF.Models {
    public class Service : DataModel<Service> {
        private string title;
        private string description;
        private double price;

        [DisplayName("Название")]
        public string Title {
            get => title;
            set => SetProperty(ref title, value);
        }

        [DisplayName("Описание")]
        public string Description {
            get => description;
            set => SetProperty(ref description, value);
        }

        [DisplayName("Стоимость")]
        public double Price {
            get => price;
            set => SetProperty(ref price, value);
        }

        public override string ToString() => Title;
    }

    class Services : ModelBase<Services> {
        public Services() {
            ServicesList = InitServices;
        }

        private static ObservableCollection<Service> InitServices => new ObservableCollection<Service> {
            new Service(){Title = "ldlld", Description = "lllfld;", Price = 12, ID = 444}
        };

        public ObservableCollection<Service> ServicesList { get; private set; }

        public void                 AddService(Service              newService)      => ServicesList.Add(newService);
        public IEnumerable<Service> GetServices(Func<Service, bool> searchPredicate) => ServicesList.Where(searchPredicate);
        public bool                 DeleteService(Service           service)         => ServicesList.Remove(service);

        public void EditService(Service newService) {
            if (ServicesList.Remove(GetServices(it => it.ID == newService.ID).First())) {
                ServicesList.Add(newService);
            }
        }
    }
}