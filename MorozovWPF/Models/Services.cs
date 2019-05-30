using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorozovWPF.Models
{
    class Service {
        public string Title;
        public string Description;
    }
    class Services : ModelBase<Services>
    {
        public ObservableCollection<Service> ServiceList { get; private set; }
    }
}
