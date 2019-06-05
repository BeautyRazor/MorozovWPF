using System;
using System.Collections.Generic;
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
using MorozovWPF.ViewModels.Forms;

namespace MorozovWPF.Views.Forms {
    /// <summary>
    /// Interaction logic for ConsumerForm.xaml
    /// </summary>
    public partial class ConsumerForm : Window {
        public ConsumerForm() {
            InitializeComponent();

            DataContext = new ConsumerFormViewModel(null);
        }

        public ConsumerForm(ConsumerFormViewModel viewModel) {
            InitializeComponent();

            DataContext = viewModel;
        }

        private void DescriptionBox_TextChanged(object sender, TextChangedEventArgs e) {

        }
    }
}
