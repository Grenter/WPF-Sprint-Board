using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SprintBoard.Model
{
    public class Story : ModelBase
    {
        private string number;
        public string Number
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

        private int points;
        public int Points
        {
            get
            {
                return this.points;
            }

            set
            {
                if (value != this.points)
                {
                    this.points = value;
                    NotifyPropertyChanged("Points");
                }
            }
        }


        private int taskCount;
        public int TaskCount
        {
            get
            {
                return this.taskCount;
            }

            set
            {
                if (value != this.taskCount)
                {
                    this.taskCount = value;
                    NotifyPropertyChanged("TaskCount");
                }
            }
        }

        private List<int> storyPoints = new List<int>();
        public List<int> StoryPoints
        {
            get
            {
                return this.storyPoints;
            }

            private set
            {
                if (value != this.storyPoints)
                {
                    this.storyPoints = value;
                    NotifyPropertyChanged("StoryPoints");
                }
            }
        }

        private Brush cardColour;
        public Brush CardColour
        {
            get
            {
                return this.cardColour;
            }

            set
            {
                if (value != this.cardColour)
                {
                    this.cardColour = value;
                    NotifyPropertyChanged("CardColour");
                }
            }
        }

        public Story()
        {
            this.storyPoints.Add(1);
            this.storyPoints.Add(2);
            this.storyPoints.Add(3);
            this.storyPoints.Add(5);
            this.storyPoints.Add(8);
            this.storyPoints.Add(13);
            this.storyPoints.Add(21);
        }
    }
}
