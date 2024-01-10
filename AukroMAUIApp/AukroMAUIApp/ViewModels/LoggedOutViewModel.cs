using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AukroMAUIApp.ViewModels
{
    internal class LoggedOutViewModel : INotifyPropertyChanged
    {


        public LoggedOutViewModel()
        {
            ReturnCommand = new Command(Return);
        }

        private async void Return()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }








        public ICommand ReturnCommand { get; private set; }



        #region MVVM
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
