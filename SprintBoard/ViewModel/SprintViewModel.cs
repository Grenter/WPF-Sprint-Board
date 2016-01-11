using SprintBoard.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SprintBoard.ViewModel
{
    public class SprintViewModel : INotifyPropertyChanged
    {
        private string sprintHeader = "";

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string fileStoreLocation = Environment.CurrentDirectory + @"\Files\";

        private ObservableCollection<Sprint> sprints = new ObservableCollection<Sprint>();
        public ObservableCollection<Sprint> Sprints
        {
            get
            {
                return this.sprints;
            }

            set
            {
                if (value != this.sprints)
                {
                    this.sprints = value;
                    NotifyPropertyChanged("Sprints");
                }
            }
        }

        public SprintViewModel()
        {
            ReadInSprintData();
        }

        private void ReadInSprintData()
        {
            var sprintsFile = fileStoreLocation + "sprints.txt";
            if (File.Exists(sprintsFile))
            {
                var data = File.ReadAllLines(sprintsFile);
                sprintHeader = data[0];

                foreach (var line in data)
                {
                    if (line != data[0])
                    {
                        var lineSplit = line.Split(',');

                        var posters = lineSplit[1].Split('|');

                        var sprintPosters = new List<SprintPoster>();

                        foreach (var posterURL in posters)
                        {
                            sprintPosters.Add(new SprintPoster{
                                URL = posterURL
                            });
                        }

                        Sprint sprint = new Sprint
                        {
                            Name = lineSplit[0],
                            Posters = sprintPosters,
                            Dates = lineSplit[2],
                        };

                        sprints.Add(sprint);
                    }
                }
            }
        }
    }
}
