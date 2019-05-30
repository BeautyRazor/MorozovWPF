using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MorozovWPF.Models {
    public class Sale {
        public string   SaleId      { get; set; }
        public bool     Открыт    { get; set; }
        public Consumer Consumer    { get; set; }
        public string   Description { get; set; }
    }

    class Sales : ModelBase<Sales> {
        public ObservableCollection<Sale> SalesList { get; private set; } = new ObservableCollection<Sale> {
            new Sale {
                SaleId = "1",
                Открыт = true,
                Consumer = new Consumer() {Company = "Dell", Name = "Den"},
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua"
            },
            new Sale {SaleId = "2", Открыт = true, Consumer = new Consumer() {Company = "Dell", Name = "Den"}},
            new Sale {SaleId = "3", Открыт = true, Consumer = new Consumer() {Company = "Dell", Name = "Den"}},
            new Sale {SaleId = "4", Открыт = true, Consumer = new Consumer() {Company = "Dell", Name = "Den"}}
        };

        public Sale AddSale(Sale sale) {
            throw new NotImplementedException();
        }

        public IEnumerable<Sale> GetSales(Func<Sale, bool> searchPredicate) {
            return SalesList.Where(searchPredicate);
        }

        public Sale EditSale(Sale sale) {
            throw new NotImplementedException();
        }

        public bool DeleteSale(Sale sale) {
            throw new NotImplementedException();
        }
    }
}