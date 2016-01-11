using SprintBoard.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SprintBoard
{
    /// <summary>
    /// Interaction logic for SprintStory.xaml
    /// </summary>s
    public partial class StoryControl : UserControl
    {
        public static DependencyProperty StoryNumberProperty;
        public static DependencyProperty StoryDescriptionProperty;
        public static DependencyProperty StoryPointProperty;

        static StoryControl()
        {
            StoryNumberProperty = DependencyProperty.Register("StoryNumber", typeof(string), typeof(StoryControl), new PropertyMetadata(""));
            StoryDescriptionProperty = DependencyProperty.Register("StoryDescription", typeof(string), typeof(StoryControl), new PropertyMetadata(""));
            StoryPointProperty = DependencyProperty.Register("StoryPoint", typeof(int), typeof(StoryControl), new PropertyMetadata(0));
        }

        public StoryControl()
        {
            InitializeComponent();
        }

        public string StoryNumber
        {
            get { return (string)GetValue(StoryNumberProperty); }
            set { SetValue(StoryNumberProperty, value); }
        }

        public string StoryDescription
        {
            get { return (string)GetValue(StoryDescriptionProperty); }
            set { SetValue(StoryDescriptionProperty, value); }
        }

        public int StoryPoint
        {
            get { return (int)GetValue(StoryPointProperty); }
            set { SetValue(StoryPointProperty, value); }
        }

        private void StoryPointClick(object sender, RoutedEventArgs e)
        {
            points.Visibility = Visibility.Visible;
        }

        private void ChangePointsClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            Story story = (Story)this.DataContext;
            story.Points = int.Parse(btn.Content.ToString());

            points.Visibility = Visibility.Hidden;
        }

        
    }
}
