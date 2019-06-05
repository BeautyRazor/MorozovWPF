using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MorozovWPF.Commands;

namespace MorozovWPF.ViewModels {
    class HelloScreenViewModel {
        public RelayCommand GoToLoginPageCommand => AuthCommands.SignOutCommand;
    }
}
