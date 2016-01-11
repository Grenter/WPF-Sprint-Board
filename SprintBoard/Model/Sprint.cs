using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintBoard.Model
{
    public class Sprint : ModelBase
    {
        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (value != this.name)
                {
                    this.name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        private List<SprintPoster> posters;
        public List<SprintPoster> Posters
        {
            get
            {
                return this.posters;
            }

            set
            {
                if (value != this.posters)
                {
                    this.posters = value;
                    NotifyPropertyChanged("Posters");
                }
            }
        }

        private string dates;
        public string Dates
        {
            get
            {
                return this.dates;
            }

            set
            {
                if (value != this.dates)
                {
                    this.dates = value;
                    NotifyPropertyChanged("Dates");
                }
            }
        }
    }
}
