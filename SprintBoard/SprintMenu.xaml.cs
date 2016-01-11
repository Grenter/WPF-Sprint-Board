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
    /// Interaction logic for SprintMenu.xaml
    /// </summary>
    public partial class SprintMenu : Window
    {
        private SprintViewModel svm;
         
        public SprintMenu()
        {
            InitializeComponent();

            svm = new SprintViewModel();

            this.DataContext = svm;
        }

         private void btnNewSprint_Click(object sender, RoutedEventArgs e)
        {
            CreateEditSprint ces = new CreateEditSprint();
            ces.Show();

            SprintBoardViewModel sbvm2 = new SprintBoardViewModel();

            ces.DataContext = sbvm2;
        }

        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Button btn = (Button)sender;
            Board sb = new Board(btn.Tag.ToString());
            sb.Show();
        }
    }
}
