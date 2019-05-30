using System.Windows.Controls;
using MorozovWPF.Models;
using MorozovWPF.Views;

namespace MorozovWPF.ViewModels {
    class MainWindowViewModel : ViewModelBase {
        public MainWindowViewModel() {
            Application appModel = Application.Instance;

            BindProperties(appModel, nameof(appModel.CurrentControl), nameof(CurrentControl));
            BindProperties(appModel, nameof(appModel.CurrentMessage), nameof(ErrorMessage));
            BindProperties(appModel, nameof(appModel.CurrentMessage), nameof(ErrorTabVisibility), it => it != null);

            appModel.CurrentControl = new LoginForm();
        }

        public UserControl CurrentControl     { get; private set; }
        public string      ErrorMessage       { get; private set; }
        public string      ErrorTabVisibility { get; private set; }
    }
}