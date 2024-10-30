using calc.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;
using WpfCalculator;

namespace calc
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() { }

        protected override void OnStartup(StartupEventArgs e)
        {
            CalcViewModel calcViewModel = new CalcViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = calcViewModel
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}