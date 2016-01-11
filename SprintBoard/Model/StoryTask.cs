using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintBoard.Model
{
    public class StoryTask : ModelBase
    {
        private string storyNumber;
        public string StoryNumber
        {
            get
            {
                return this.storyNumber;
            }

            set
            {
                if (value != this.storyNumber)
                {
                    this.storyNumber = value;
                    NotifyPropertyChanged("StoryNumber");
                }
            }
        }

        private int number;
        public int Number
        {
            get
            {
                return this.number;
            }

            set
            {
                if (value != this.number)
                {
                    this.number = value;
                    NotifyPropertyChanged("Number");
                }
            }
        }

        private string tasknumber;
        public string TaskNumber
        {
            get
            {
                TaskNumber = this.storyNumber + "." + this.number;
                return this.tasknumber;
            }
            set
            {
                if (value != this.tasknumber)
                {
                    this.tasknumber = value;
                    NotifyPropertyChanged("TaskNumber");
                }
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                if (value != this.description)
                {
                    this.description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }

        private List<User> taskUsers = new List<User>();
        public List<User> TaskUsers
        {
            get
            {
                return this.taskUsers;
            }

            set
            {
                if (value != this.taskUsers)
                {
                    this.taskUsers = value;
                    NotifyPropertyChanged("TaskUsers");
                }
            }
        }

        private List<string> taskUserAvatars = new List<string>();
        public List<string> TaskUserAvatars
        {
            get
            {
                TaskUserAvatars = taskUsers.Select(u => u.Avatar).ToList();
                return this.taskUserAvatars;
            }

            set
            {

                if (value != this.taskUserAvatars)
                {
                    this.taskUserAvatars = value;
                    NotifyPropertyChanged("TaskUserAvatars");
                }
            }
        }

        private int timeTaken;
        public int TimeTaken
        {
            get
            {
                return this.timeTaken;
            }

            set
            {
                if (value != this.timeTaken)
                {
                    this.timeTaken = value;
                    NotifyPropertyChanged("TimeTaken");
                }
            }
        }

        private int timeLeft;
        public int TimeLeft
        {
            get
            {
                return this.timeLeft;
            }

            set
            {
                if (value != this.timeLeft)
                {
                    this.timeLeft = value;
                    NotifyPropertyChanged("TimeLeft");
                }
            }
        }

        private int column;
        public int Column
        {
            get
            {
                return this.column;
            }

            set
            {
                if (value != this.column)
                {
                    this.column = value;
                    NotifyPropertyChanged("Column");
                }
            }
        }
    }
}
