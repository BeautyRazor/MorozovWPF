using System;
using System.Collections.Generic;
using System.Linq;

namespace MorozovWPF.Models
{
    public class Sale {
        public string Id;
        public bool IsOpened { get; set; }
        public Consumer Consumer { get; set; } 
    }
    class Sales : ModelBase<Sales>
    {
        public List<Sale> SalesList { get; private set; }

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
