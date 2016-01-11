using SprintBoard.ViewModel;
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

namespace SprintBoard
{
    /// <summary>
    /// Interaction logic for CreateEditSprint.xaml
    /// </summary>
    public partial class CreateEditSprint : Window
    {
        public CreateEditSprint()
        {
            InitializeComponent();
        }

        public void SetupDataContext(SprintBoardViewModel sbvm)
        {
            this.DataContext = sbvm;

        }
    }
}
