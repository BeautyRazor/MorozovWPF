using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MorozovWPF.Commands;
using MorozovWPF.Models;
using MorozovWPF.Views;

namespace MorozovWPF.ViewModels {
    class MainWorkPlaneViewModel : ViewModelBase {
        public MainWorkPlaneViewModel() { }

        private object selectedItem;

        public object SelectedItem {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        public RelayCommand AddCommand     => FormCommands.OpenAddFormCommand;
        public RelayCommand EditCommand    => FormCommands.OpenEditFormCommand;
        public RelayCommand DeleteCommand  => CRUDCommands.DeleteCommand;
        public RelayCommand SignOutCommand => AuthCommands.SignOutCommand;
    }
}