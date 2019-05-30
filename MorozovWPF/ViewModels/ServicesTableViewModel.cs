using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MorozovWPF.Models;

namespace MorozovWPF.ViewModels {
    class ServicesTableViewModel : ViewModelBase {
        public ServicesTableViewModel() {
            Services servicesModel = Services.Instance;

            BindProperties(servicesModel, nameof(servicesModel.ServiceList), nameof(ServiceList));
        }

        public ObservableCollection<Service> ServiceList { get; private set; }
    }

}
