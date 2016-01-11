using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintBoard.Model
{
    public class User : ModelBase
    {
        private int id;
        public int Id 
        {
            get
            {
                return this.id;
            }

            set
            {
                if (value != this.id)
                {
                    this.id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }

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

        private string initials;
        public string Initials
        {
            get
            {
                var uppers = this.name.Where(Char.IsUpper).ToArray();
                string s = "";
                foreach (var c in uppers)
                    s += c.ToString();

                Initials = s;

                return this.initials;
            }

            private set
            {
                if (value != this.initials)
                {
                    this.initials = value;
                    NotifyPropertyChanged("Initials");
                }
            }
        }

        private string avatar;
        public string Avatar
        {
            get
            {
                return this.avatar;
            }

            set
            {
                if (value != this.avatar)
                {
                    this.avatar = value;
                    NotifyPropertyChanged("Avatar");
                }
            }
        }

    }
}
