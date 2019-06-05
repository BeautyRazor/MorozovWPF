using System.Linq;
using MorozovWPF.Commands;
using MorozovWPF.Models;

namespace MorozovWPF.ViewModels.Forms {
    public class ServiceFormViewModel : ViewModelBase {
        public ServiceFormViewModel(Service serviceToEdit) {
            if (serviceToEdit != null) {
                CurrentService = serviceToEdit;
            }
            else {
                CurrentService = new Service();
                CurrentService.ID = Services.Instance.ServicesList.Max(it => it.ID) + 1;
            }

        }

        private Service currentService;

        public Service CurrentService {
            get => currentService;
            set => SetProperty(ref currentService, value);
        }

        public RelayCommand SaveCommand => CRUDCommands.SaveCommand;
    }
}