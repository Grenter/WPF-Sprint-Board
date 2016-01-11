using SprintBoard.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SprintBoard
{
    public class TaskCardViewModel : INotifyPropertyChanged
    {
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

        private StoryTask storyTast;
        public StoryTask StoryTask
        {
            get
            {
                return this.storyTast;
            }
            set
            {
                if (value != this.storyTast)
                {
                    this.storyTast = value;
                    NotifyPropertyChanged("StoryTask");
                }
            }
        }
    }
}
