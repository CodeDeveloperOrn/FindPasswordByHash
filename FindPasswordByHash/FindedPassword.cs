using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FindPasswordByHash
{
    public class FindedPassword : INotifyPropertyChanged
    {
        private string text;
        public string Value {
            get { return this.text; }
            set
            {
                this.text = value;
                OnPropertyChanged("Value");
            }
        }

        public FindedPassword(){}
        public FindedPassword(string val)
        {
            this.Value = val;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
