using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorozovWPF.Models {
    public class Consumer {
        public string Company { get; set; }
        public string Name    { get; set; }

        public override string ToString() => Name;
    }

    class Consumers : ModelBase<Consumers> {
        private List<Consumer> consumersList;

        public List<Consumer> ConsumersList {
            get => consumersList;
            set => SetProperty(ref consumersList, value);
        }
    }
}