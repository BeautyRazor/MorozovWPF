using System.Collections.Generic;
using System.Collections.ObjectModel;
using MorozovWPF.Models;

namespace MorozovWPF.ViewModels {
    class SalesTableViewModel : ViewModelBase {
        public SalesTableViewModel() {
            Sales salesModel = Sales.Instance;

            BindProperties(salesModel, nameof(salesModel.SalesList), nameof(SalesList));
        }

        public ObservableCollection<Sale> SalesList { get; private set; }
    }
}