using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MorozovWPF.Models;
using MorozovWPF.Views;

namespace MorozovWPF.Commands {
    class NavigationCommands {
        public static RelayCommand GoToSalesCommand => new RelayCommand(
            actionParameter => {
                Application.Instance.CurrentTable = new SalesTable();
                Application.Instance.CurrentMessage = null;
            });
    }
}
