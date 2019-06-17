using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using MorozovWPF.Helpers;

namespace MorozovWPF.Models {
    public class Consumer : DataModel<Consumer>, IConvertiblrToId {
        private string company;
        private string name;
        private string phone;

        [DisplayName("Компания")]
        [SQLiteField]
        public string Company {
            get => company;
            set => SetProperty(ref company, value);
        }

        [DisplayName("Имя представителя")]
        [SQLiteField]
        public string Name {
            get => name;
            set => SetProperty(ref name, value);
        }

        [DisplayName("Телефон")]
        [SQLiteField]
        public string Phone {
            get => phone;
            set => SetProperty(ref phone, value);
        }

        public override string ToString() => Name;
        public int convertToId() {
            return ID;
        }

        public override Consumer FromDataRow(DataRow row) {
            int i = 0;
            ID      = GetInt(row[i++]);
            Company = GetString(row[i++]);
            Name    = GetString(row[i++]);
            Phone   = GetString(row[i++]);

            return this;
        }
    }

    class Consumers : ModelBase<Consumers> {
        public Consumers() {
            ConsumersList = InitConsumers();
        }

        private ObservableCollection<Consumer> InitConsumers() {
            return new ObservableCollection<Consumer>(DatabaseHelper.GetFullData<Consumer>(Tables.Consumers));
 
        }

        private ObservableCollection<Consumer> consumersList;

        public ObservableCollection<Consumer> ConsumersList {
            get => consumersList;
            set => SetProperty(ref consumersList, value);
        }


        public void                  AddConsumer(Consumer              newConsumer) {
            DatabaseHelper.AddRow(Tables.Consumers, newConsumer);
            ConsumersList.Add(newConsumer);
        }

        public bool                  DeleteConsumer(Consumer           consumer) {
            DatabaseHelper.DeleteRow(Tables.Consumers, consumer);
            return ConsumersList.Remove(consumer);
        }

        public IEnumerable<Consumer> GetConsumers(Func<Consumer, bool> searchPredicate) => ConsumersList.Where(searchPredicate);

        public void EditConsumer(Consumer newConsumer) {
            if (ConsumersList.Remove(GetConsumers(it => it.ID == newConsumer.ID).First())) {
                DatabaseHelper.UpdateRow(Tables.Consumers, newConsumer);
                ConsumersList.Add(newConsumer);
            }
        }
    }
}