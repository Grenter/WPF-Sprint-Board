using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintBoard.Model
{
    public class SprintPoster : ModelBase
    {
        private string url;
        public string URL
        {
            get
            {
                return this.url;
            }

            set
            {
                if (value != this.url)
                {
                    this.url = value;
                    NotifyPropertyChanged("URL");
                }
            }
        }
    }
}
