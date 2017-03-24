using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MechaFish.Wow {
    public class GameData : INotifyPropertyChanged  {
        public event PropertyChangedEventHandler PropertyChanged;
        

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}