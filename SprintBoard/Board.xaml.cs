using SprintBoard.Model;
using SprintBoard.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Board : Window
    {
        private bool cardDragging = false;
        private bool canvasDragging = false;
        private bool savingFile = false;
        private UserControl uc;
        private Point startMousePos;
        private Dictionary<string, Rect> columnRects;
        private string sprintName;

        private int count = 4;
        private double startYPos = 10;
        private double xPos;
        private double yPos;

        private bool testingFull = false;

        private const double CARD_WIDTH = 200;
        private const double CARD_HEIGHT = 160;
        private const double CARD_X_OFFSET = 45;
        private const double CARD_Y_OFFSET = 10;
        private const double CARD_SPACE_OFFSET = 50;
        private const double WINDOW_HEADING_OFFSET = 110;

        private Dictionary<string, double> storyYPositions = new Dictionary<string, double>();

        private SprintBoardViewModel sbvm;

        public Board(string sprintName)
        {
            InitializeComponent();

            this.sprintName = sprintName;

            sbvm = new SprintBoardViewModel();
            sbvm.ReadInData(sprintName);

            this.DataContext = sbvm;
            //task1.DataContext = sbvm;
        }

        private void CreateColumnTasks(List<StoryTask> storyTasks, Column column)
        {
            foreach (var key in storyYPositions.Keys)
            {

                xPos = SetXStartPos(column);
                yPos = storyYPositions[key];
                var cardCount = 0;
                var stackCount = 0;

                var currentstoryTasks = storyTasks.Where(t => t.StoryNumber == key).ToList();

                var maxCard = 10;

                if (column == Column.Testing)
                {
                    maxCard = 5;
                }

                if (currentstoryTasks.Count >= maxCard)
                {
                    currentstoryTasks = currentstoryTasks.Take(maxCard).ToList();
                    if (column == Column.Testing)
                    {
                        testingFull = true;
                    }
                }


                currentstoryTasks = currentstoryTasks.OrderByDescending(st => st.Number).ToList();

                foreach (var storyTask in currentstoryTasks)
                {

                    var taskControl = new TaskControl();
                    var tcvm = new TaskCardViewModel
                    {
                        Users = sbvm.Users,
                        StoryTask = storyTask
                    };

                    if (testingFull)
                        taskControl.BackgroundColour = Brushes.OrangeRed;
                    else
                        taskControl.BackgroundColour = Brushes.White;

                    taskControl.DataContext = tcvm;
                    taskControl.SetValue(Canvas.LeftProperty, xPos);
                    taskControl.SetValue(Canvas.TopProperty, yPos);

                    taskControl.MouseDown += ControlMouseDown;
                    taskControl.MouseMove += ControlMouseMove;
                    taskControl.MouseUp += ControlMouseUp;

                    cardCount++;
                    if (cardCount < 5)
                    {
                        SetXYForColumn(column);
                    }
                    else
                    {
                        stackCount++;
                        xPos = SetXStartPos(column) + (CARD_WIDTH * stackCount) + 5;
                        yPos = storyYPositions[key];
                        cardCount = 0;
                    }
                    SprintBoard.Children.Add(taskControl);

                }


                testingFull = false;

            }
        }

        private void SetXYForColumn(Column column)
        {
            switch (column)
            {
                case Column.ToDo:
                    xPos += CARD_X_OFFSET;
                    yPos += CARD_Y_OFFSET;
                    break;
                case Column.InProgress:
                    xPos += CARD_X_OFFSET;
                    yPos += CARD_Y_OFFSET;
                    break;
                case Column.Testing:
                    xPos += 10;
                    yPos += CARD_Y_OFFSET;
                    break;

            }
        }

        private double SetXStartPos(Column column)
        {
            var xPos = 0.0;
            switch (column)
            {
                case Column.ToDo:
                    xPos = Story.ActualWidth + 10;
                    break;
                case Column.InProgress:
                    xPos = Story.ActualWidth + ToDo.ActualWidth + 10;
                    break;
                case Column.Testing:
                    xPos = Story.ActualWidth + ToDo.ActualWidth + InProgress.ActualWidth + ((Testing.ActualWidth - (CARD_WIDTH + 37)) / 2);
                    break;
                case Column.Complete:
                    xPos = Story.ActualWidth + ToDo.ActualWidth + InProgress.ActualWidth + Testing.ActualWidth + ((Complete.ActualWidth - 15 - CARD_WIDTH) / 2);
                    break;

            }

            return xPos;
        }

        private void CreateTasks()
        {
            var columns = sbvm.Tasks.Select(t => t.Column).Distinct().ToList();

            foreach (var col in columns)
                CreateColumnTasks(sbvm.Tasks.Where(st => st.Column == col).ToList(), (Column)col);
        }

        private void CreateStories()
        {
            var yPos = startYPos;
            var xPos = (Story.ActualWidth - CARD_WIDTH) / 2;
            foreach (var story in sbvm.Stories)
            {
                var storyControl = new StoryControl();
                storyControl.DataContext = story;
                storyControl.SetValue(Canvas.LeftProperty, xPos);
                storyControl.SetValue(Canvas.TopProperty, yPos);

                // LightGreen, Khaki Salmon, LightSkyBlue
                storyControl.Background = story.CardColour;
                storyYPositions.Add(story.Number, yPos);
                yPos += CARD_HEIGHT + CARD_SPACE_OFFSET;

                SprintBoard.Children.Add(storyControl);

            }

            SprintBoard.Height = yPos;
            // Loops through creating storie controls.
            // Setting Data context to be the story in the OC
        }

        private Dictionary<string, Rect> CreateColumnRects()
        {
            var xPos = 0.0;
            Dictionary<string, Rect> rects = new Dictionary<string, Rect>();
            foreach (var column in SprintGrid.ColumnDefinitions)
            {
                Size size = new Size(column.ActualWidth, SprintBoard.ActualHeight);
                Point point = new Point(xPos, WINDOW_HEADING_OFFSET);
                rects.Add(column.Name, new Rect(point, size));
                xPos += column.ActualWidth;
            }

            return rects;
        }

        private void CheckColumn(Point mousePosition)
        {
            var tcvm = (TaskCardViewModel)uc.DataContext;
            var storyTask = sbvm.Tasks.Where(t => t.TaskNumber == tcvm.StoryTask.TaskNumber).FirstOrDefault();
            var cardColumn = (Column)storyTask.Column;
            foreach (var columnRect in columnRects)
            {
                if (columnRect.Value.Contains(mousePosition))
                {
                    var newColumn = (Column)Enum.Parse(typeof(Column), columnRect.Key);

                    if (newColumn != cardColumn)
                        storyTask.Column = (int)newColumn;
                }
            }
        }

        private void ControlMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender.GetType().IsSubclassOf(typeof(UserControl)))
            {
                cardDragging = true;
                canvasDragging = false;
                uc = (UserControl)sender;
                uc.SetValue(Canvas.ZIndexProperty, count++); // Slutty :D
            }
            else if (sender.GetType() == typeof(Canvas) && cardDragging != true)
            {
                cardDragging = false;
                canvasDragging = true;
                startMousePos = e.GetPosition(null);
            }
        }

        private void ControlMouseMove(object sender, MouseEventArgs e)
        {

            if (cardDragging)
            {
                if (uc != null)
                {

                    //var element = sender as FrameworkElement;
                    var currentPoint = e.GetPosition(null);
                    var scrollOffset = CanvasScroll.VerticalOffset;

                    var xPos = currentPoint.X - (uc.Width / 2);
                    var yPos = (currentPoint.Y - WINDOW_HEADING_OFFSET) - (uc.Height / 2) + scrollOffset;

                    uc.SetValue(Canvas.LeftProperty, xPos);
                    uc.SetValue(Canvas.TopProperty, yPos);
                }
            }
            else if (canvasDragging)
            {
                var currentPoint = e.GetPosition(null);
                if (startMousePos.Y < currentPoint.Y)
                    CanvasScroll.LineUp();
                else if (startMousePos.Y > currentPoint.Y)
                    CanvasScroll.LineDown();

                startMousePos = currentPoint;
            }
        }

        private void ControlMouseUp(object sender, MouseButtonEventArgs e)
        {
            var currentPoint = e.GetPosition(null);
            if (uc != null)
                CheckColumn(currentPoint);

            cardDragging = false;
            canvasDragging = false;
            startMousePos = new Point();
            uc = null;

            //SprintBoard.Children.Clear();

            //storyYPositions.Clear();
            //CreateStories();
            //CreateTasks();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            columnRects = CreateColumnRects();
            CreateStories();
            CreateTasks();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Debug.Print("Saving file is : " + savingFile);
            if (!savingFile)
            {
                savingFile = true;
                Debug.Print("Saving file is true:");
                sbvm.WriteTaskFile(sprintName);
                savingFile = false;
                Debug.Print("Saving file is false");
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            SprintBoard.Children.Clear();

            storyYPositions.Clear();
            CreateStories();
            CreateTasks();
        }
    }
}
