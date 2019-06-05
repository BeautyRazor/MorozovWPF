using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
using MorozovWPF.ViewModels.Forms;

namespace MorozovWPF.Views.Forms {
    /// <summary>
    /// Interaction logic for OrderForm.xaml
    /// </summary>
    public partial class OrderForm : Window {
        public OrderForm() {
            InitializeComponent();

            DataContext = new OrderFormViewModel(null);
        }

        public OrderForm(OrderFormViewModel viewModel) {
            InitializeComponent();

            DataContext = viewModel;
        }

    }
}
