using System.Windows.Controls;
using Prism.Mvvm;

namespace MorozovWPF.Models {
    class Application : ModelBase<Application> {
        private string      userName;
        private string      currentMessage;
        private UserControl currentControl;

        public UserControl CurrentControl {
            get => currentControl;
            set => SetProperty(ref currentControl, value);
        }

        public string UserName {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public string CurrentMessage {
            get => currentMessage;
            set => SetProperty(ref currentMessage, value);
        }
    }
}