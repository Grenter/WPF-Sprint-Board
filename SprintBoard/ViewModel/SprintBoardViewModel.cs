using SprintBoard.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SprintBoard.ViewModel
{
    public class SprintBoardViewModel : INotifyPropertyChanged
    {
        private string taskHeader;
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string fileStoreLocation = Environment.CurrentDirectory + @"\Files\";


        private ObservableCollection<User> users = new ObservableCollection<User>();
        public ObservableCollection<User> Users
        {
            get
            {
                return this.users;
            }

            set
            {
                if (value != this.users)
                {
                    this.users = value;
                    NotifyPropertyChanged("Users");
                }
            }
        }

        private ObservableCollection<Story> stories = new ObservableCollection<Story>();
        public ObservableCollection<Story> Stories
        {
            get
            {
                return this.stories;
            }

            set
            {
                if (value != this.stories)
                {
                    this.stories = value;
                    NotifyPropertyChanged("Stories");
                }
            }
        }

        private ObservableCollection<StoryTask> tasks = new ObservableCollection<StoryTask>();
        public ObservableCollection<StoryTask> Tasks
        {
            get
            {
                return this.tasks;
            }

            set
            {
                if (value != this.tasks)
                {
                    this.tasks = value;
                    NotifyPropertyChanged("Tasks");
                }
            }
        }


        public SprintBoardViewModel()
        {
        }

        public void WriteTaskFile(string sprintName)
        {
            // Check back up folder.
            // Create is doesn't exisit
            // Capture this is try catch
            var tasksFile = fileStoreLocation + sprintName + @"\Tasks.txt";
            var backupTaskFile = fileStoreLocation + sprintName + @"\backups\Tasks - " + DateTime.Now.ToString("dd-MM hh-mm-ss") + ".txt";
            if (File.Exists(tasksFile))
            {
                File.Move(tasksFile, backupTaskFile);
                File.Delete(tasksFile);

                var sw = new StreamWriter(tasksFile);
                sw.WriteLine(taskHeader);
                //StoryNumber,TaskNumber,TaskDescription,UserId,TimeLeft,TimeTake,Column
                foreach (var tasks in Tasks)
                {
                    string userIds = "";
                    foreach (var user in tasks.TaskUsers)
                    {
                        userIds += user.Id + "|";
                    }

                    userIds = userIds.Remove(userIds.Length - 1, 1);
                    sw.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6}", tasks.StoryNumber, tasks.Number, tasks.Description, userIds, tasks.TimeLeft, tasks.TimeTaken, tasks.Column));
                }

                sw.Close();

            }
            else
            {
                MessageBox.Show(@"Couldn't find a file to back up so create a new one.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                File.Create(tasksFile);

                var sw = new StreamWriter(tasksFile);
                sw.WriteLine(taskHeader);
                //StoryNumber,TaskNumber,TaskDescription,UserId,TimeLeft,TimeTake,Column
                foreach (var tasks in Tasks)
                {
                    string userIds = "";
                    foreach (var user in tasks.TaskUsers)
                    {
                        userIds += user.Id + "|";
                    }

                    userIds = userIds.Remove(userIds.Length - 1, 1);
                    sw.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6}", tasks.StoryNumber, tasks.Number, tasks.Description, userIds, tasks.TimeLeft, tasks.TimeTaken, tasks.Column));
                }

                sw.Close();
            }

        }

        public void ReadInData(string sprintName)
        {
            ReadUsers();
            ReadTasks(sprintName);
            ReadStories(sprintName);
        }

        private void ReadTasks(string sprintName)
        {
            var tasksFile = fileStoreLocation + sprintName + @"\tasks.txt";
            if (File.Exists(tasksFile))
            {
                var data = File.ReadAllLines(tasksFile);
                taskHeader = data[0];

                foreach (var line in data)
                {
                    try
                    {
                        Debug.Print(line);
                        if (line != data[0])
                        {
                            var lineSplit = line.Split(',');

                            if (lineSplit.Count() != 7)
                            {
                                MessageBox.Show(String.Format("Task {0}.{1} has the wrong number of Columns go find that pesky ,", lineSplit[0], lineSplit[1]));
                            }
                            else
                            { 
                                var userData = lineSplit[3].Split('|');

                                var userIds = new List<int>();
                                for (int i = 0; i < userData.Length; i++)
                                {
                                    userIds.Add(int.Parse(userData[i]));
                                }

                                StoryTask storyTask = new StoryTask
                                {
                                    StoryNumber = lineSplit[0],
                                    Number = int.Parse(lineSplit[1]),
                                    Description = lineSplit[2],
                                    TaskUsers = Users.Where(u => userIds.Contains(u.Id)).ToList(),
                                    TimeLeft = int.Parse(lineSplit[4]),
                                    TimeTaken = int.Parse(lineSplit[5]),
                                    Column = int.Parse(lineSplit[6])
                                };

                                tasks.Add(storyTask);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error with the following line \r\n" + line + " \r\n This will be ignored");
                    }
                }
            }

        }

        private void ReadStories(string sprintName)
        {
            var storiesFile = fileStoreLocation + sprintName + @"\stories.txt";
            if (File.Exists(storiesFile))
            {
                var data = File.ReadAllLines(storiesFile);
                var headers = data[0];
                try
                {
                    foreach (var line in data)
                    {
                        if (line != data[0])
                        {
                            var lineSplit = line.Split(',');
                            var tasks = Tasks.Where(t => t.StoryNumber == lineSplit[0]).ToList();

                            Story story = new Story
                            {
                                Number = lineSplit[0],
                                Description = lineSplit[1],
                                Points = int.Parse(lineSplit[2]),
                                TaskCount = tasks.Count,
                                CardColour = (Brush)new BrushConverter().ConvertFromString(lineSplit[3])
                            };

                            stories.Add(story);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in the stories file. Check here first");
                }
            }
        }

        private void ReadUsers()
        {
            var usersFile = fileStoreLocation + "users.txt";
            if (File.Exists(usersFile))
            {
                var data = File.ReadAllLines(usersFile);
                var headers = data[0];

                foreach (var line in data)
                {
                    if (line != data[0])
                    {
                        var lineSplit = line.Split(',');

                        User user = new User
                        {
                            Id = int.Parse(lineSplit[0]),
                            Name = lineSplit[1],
                            Avatar = lineSplit[2]
                        };

                        users.Add(user);
                    }
                }
            }
        }
    }
}
