using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorozovWPF.Models {
    public class Consumer : DataModel<Consumer> {
        private string company;
        private string name;
        private string phone;
        
        [DisplayName("Компания")]
        public string Company {
            get => company;
            set => SetProperty(ref company, value);
        }

        [DisplayName("Имя представителя")]
        public string Name {
            get => name;
            set => SetProperty(ref name, value);
        }

        [DisplayName("Телефон")]
        public string Phone {
            get => phone;
            set => SetProperty(ref phone, value);
        }

        public override string ToString() => Name;
    }

    class Consumers : ModelBase<Consumers> {
        public Consumers() {
            ConsumersList = InitConsumers();
        }

        private ObservableCollection<Consumer> InitConsumers() {
            return new ObservableCollection<Consumer>() {
                new Consumer() {ID = 1, Name = "Den", Company  = ",dlf"},
                new Consumer() {ID = 2, Name = "Tom", Company  = ",dlf"},
                new Consumer() {ID = 3, Name = "Alex", Company = ",dlf"}
            };
        }

        private ObservableCollection<Consumer> consumersList;

        public ObservableCollection<Consumer> ConsumersList {
            get => consumersList;
            set => SetProperty(ref consumersList, value);
        }


        public void                  AddConsumer(Consumer              newConsumer)     => ConsumersList.Add(newConsumer);
        public bool                  DeleteConsumer(Consumer           consumer)        => ConsumersList.Remove(consumer);
        public IEnumerable<Consumer> GetConsumers(Func<Consumer, bool> searchPredicate) => ConsumersList.Where(searchPredicate);

        public void EditConsumer(Consumer newConsumer) {
            if (ConsumersList.Remove(GetConsumers(it => it.ID == newConsumer.ID).First())) {
                ConsumersList.Add(newConsumer);
            }
        }
    }
}