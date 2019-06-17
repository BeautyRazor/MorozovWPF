using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MorozovWPF.Commands;
using MorozovWPF.Models;

namespace MorozovWPF.ViewModels.Forms {
    public class ConsumerFormViewModel: ViewModelBase {
        public ConsumerFormViewModel(Consumer consumerToEdit) {
            if (consumerToEdit != null) {
                CurrentConsumer = consumerToEdit;
            }
            else {
                CurrentConsumer = new Consumer();
                if (Consumers.Instance.ConsumersList.Any()) {
                    CurrentConsumer.ID = Consumers.Instance.ConsumersList.Max(it => it.ID) + 1;
                }
                else {
                    CurrentConsumer.ID = 0;
                }
            }
        }

        private Consumer currentConsumer;
        public Consumer CurrentConsumer {
            get => currentConsumer;
            set => SetProperty(ref currentConsumer, value);
        }

        public RelayCommand SaveCommand => CRUDCommands.SaveCommand;
    }
}
