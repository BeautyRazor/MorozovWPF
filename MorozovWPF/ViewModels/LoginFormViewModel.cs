using MorozovWPF.Commands;
using MorozovWPF.Models;

namespace MorozovWPF.ViewModels {
    class LoginFormViewModel : ViewModelBase {
        public LoginFormViewModel() {
            Application appModel = Application.Instance;

            BindProperties(appModel, nameof(appModel.UserName), nameof(UserName));
        }

        private string userName;

        public string UserName {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public RelayCommand SignInCommand => AuthCommands.SignInCommand;

    }
}