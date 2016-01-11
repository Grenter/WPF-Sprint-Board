using SprintBoard.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SprintBoard
{
    /// <summary>
    /// Interaction logic for UCTaskControl.xaml
    /// </summary>
    public partial class TaskControl : UserControl
    {
        public static DependencyProperty BackgorundColourProperty;
        

        static TaskControl()
        {
            BackgorundColourProperty = DependencyProperty.Register("BackgroundColour", typeof(Brush), typeof(TaskControl), new PropertyMetadata(Brushes.White));

        }


        public TaskControl()
        {
            InitializeComponent();
        }

        public Brush BackgroundColour
        {
            get { return (Brush)GetValue(BackgorundColourProperty); }
            set { SetValue(BackgorundColourProperty, value); }
        }


        private void TakenIncrementClick(object sender, RoutedEventArgs e)
        {
            var tcvm = (TaskCardViewModel)this.DataContext;
            tcvm.StoryTask.TimeTaken++;
        }

        private void LeftIncrementClick(object sender, RoutedEventArgs e)
        {
            var tcvm = (TaskCardViewModel)this.DataContext;
            tcvm.StoryTask.TimeLeft++;
        }

        private void LeftDecrementClick(object sender, RoutedEventArgs e)
        {
            var tcvm = (TaskCardViewModel)this.DataContext;
            tcvm.StoryTask.TimeLeft--;
        }

        private void InitialsClicked(object sender, RoutedEventArgs e)
        {
            var unassignedUser = new User
            {
                Id = 0,
                Name = "Unassigned",
                Avatar = @"http://img3.wikia.nocookie.net/__cb20140606001154/watchdogscombined/images/1/1f/Silhouette-question-mark.jpeg"
            };

            var tcvm = (TaskCardViewModel)this.DataContext;
            Button btnClicked = (Button)sender;

            var userSelected = tcvm.Users.Where(u => u.Id == int.Parse(btnClicked.Tag.ToString())).FirstOrDefault();

            var currentTaskUsers = tcvm.StoryTask.TaskUsers;

            if (currentTaskUsers.Contains(unassignedUser))
            {
                currentTaskUsers.Remove(unassignedUser);
            }

            if (!currentTaskUsers.Contains(userSelected))
            {
                currentTaskUsers.Add(userSelected);
            }

            tcvm.StoryTask.TaskUsers = currentTaskUsers;

            users.Visibility = Visibility.Hidden;

            
        }


        private void ChangeUserClick(object sender, RoutedEventArgs e)
        {
            users.Visibility = Visibility.Visible;
        }
    }
}
