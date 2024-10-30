using calc.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace calc.Command
{
    public class DigitButtonClickCommand : CommandBase
    {
        private readonly CalcViewModel _calcViewModel;

        public DigitButtonClickCommand(CalcViewModel calcViewModel)
        {
            _calcViewModel = calcViewModel;
        }

        public override void Execute(object? button)
        {
            if (button == null)
            {
                _calcViewModel.ClearDisplay();
                _calcViewModel.Display = "ERROR";
                return;
            }

            string? input = button?.ToString();
            if (string.IsNullOrEmpty(input))
            {
                _calcViewModel.ClearDisplay();
                _calcViewModel.Display = "ERROR";
                return;
            }

            // обрабатываем возможные значения передаваемого параметра в команду
            // , - разделитель для дробных
            // остальное - цифры
            switch (button)
            {
                case ",":
                    if (_calcViewModel.NewDisplay)
                    {
                        _calcViewModel.Display = "0,";
                        _calcViewModel.NewDisplay = false;
                        return;
                    }

                    if (!_calcViewModel.Display.Contains(','))
                    {
                        _calcViewModel.Display += ",";
                    }
                    break;

                default:
                    if (_calcViewModel.NewDisplay)
                    {
                        _calcViewModel.ClearDisplay();
                    }

                    // если сейчас на Display 0, то приравниваем инпут, чтобы не получилось "01"
                    if (_calcViewModel.Display == "0")
                    {
                        _calcViewModel.Display = input;
                    }
                    else
                    {
                        _calcViewModel.Display += input;
                    }
                    break;
            }
        }
    }
}