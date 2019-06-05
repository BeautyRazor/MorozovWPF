using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MorozovWPF.Models;
using MorozovWPF.ViewModels;
using MorozovWPF.ViewModels.Forms;

namespace MorozovWPF.Views.Forms {
    /// <summary>
    /// Interaction logic for ServiceForm.xaml
    /// </summary>
    public partial class ServiceForm : Window {
        public ServiceForm() {
            InitializeComponent();

            DataContext = new  ServiceFormViewModel(null);
        }

        public ServiceForm(ServiceFormViewModel viewModel) {
            InitializeComponent();

            DataContext = viewModel;
        }
    }


}
