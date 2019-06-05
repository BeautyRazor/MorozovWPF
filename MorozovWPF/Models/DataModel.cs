using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorozovWPF.Models {
    public class DataModel<T> : ModelBase<T>
        where T : DataModel<T>, new() {
        private int id;

        public int ID {
            get => id;
            set => SetProperty(ref id, value);
        }
    }
}