using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MorozovWPF.Models;
using MorozovWPF.Views;

namespace MorozovWPF.ViewModels {
    class MainWorkPlaneViewModel : ViewModelBase {
        public MainWorkPlaneViewModel() {
            Application appModel = Application.Instance;

            BindProperties(appModel, nameof(appModel.CurrentTable), nameof(CurrentTable));

            appModel.CurrentTable = new SalesTable();
        }
        public UserControl CurrentTable { get; private set; }
        
    }
}