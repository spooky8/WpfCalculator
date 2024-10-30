using System.Collections.ObjectModel;
using System.Windows;

namespace calc.ViewModel
{
    public class HistoryViewModel : Notifier
    {
        private Visibility _historyVisibility = Visibility.Collapsed;
        public Visibility IsHistoryVisible
        {
            get => _historyVisibility;
            set => SetProperty(ref _historyVisibility, value);
        }
    
        public void UpdateHistoryVisibility(double windowWidth)
        {
            if (windowWidth >= 560)
            {
                // Используем свойство IsHistoryVisible, чтобы вызвать PropertyChanged
                IsHistoryVisible = Visibility.Visible;
            }
            else
            {
                IsHistoryVisible = Visibility.Collapsed;
            }
        }
        
        
    }
}