using calc.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calc.Command
{
    public class OperatorButtonClickCommand : CommandBase
    {
        private readonly CalcViewModel _calcViewModel;

        public OperatorButtonClickCommand(CalcViewModel calcViewModel)
        {
            _calcViewModel = calcViewModel;
        }

        public override void Execute(object? input)
        {
            if (input == null)
            {
                _calcViewModel.ClearDisplay();
                _calcViewModel.Display = "ERROR";
                return;
            }

            string? operation = input?.ToString();
            if (string.IsNullOrEmpty(operation))
            {
                _calcViewModel.ClearDisplay();
                _calcViewModel.Display = "ERROR";
                return;
            }

            // Обрабатываем возможные случаи, в которых может быть нажата кнопка оператора
            // 1 случай - мы ввели первое значение в Display и нажимаем на кнопку оператора / прошлый нажатый оператор был "=" / нам нужно обновить дисплей.
            // 1.1 - мы просто забираем наш ввод из Display и кладем его в первый операнд. Запоминаем операцию. Обновляем верхнюю строку дисплея, где показывается полностью выражение
            // 1.2 - в данном случае в Display будет лежать результат, который получился после равенства. Его мы просто кладем в первый операнд.
            // 1.3 - в данном случае в Display будет лежать результат каких-то вычислений. Аналогично с 1.2 кладем результат в первый операнд.
            if (string.IsNullOrEmpty(_calcViewModel.FirstOperand) || _calcViewModel.NewDisplay || _calcViewModel.SecondOperation == "=")
            {
                _calcViewModel.FirstOperand = _calcViewModel.Display;
                _calcViewModel.SecondOperation = operation;
                _calcViewModel.FullExpression = _calcViewModel.FirstOperand + " " + operation + " ";
            }
            // 2 случай - первый операнд не пустой и мы вводим оператор "="
            // В таком случае нам нужно вывести полное выражение в FullExpression и результат его вычисления в Display. 
            else if (operation == "=")
            {
                _calcViewModel.SecondOperand = _calcViewModel.Display;
                _calcViewModel.Operation = _calcViewModel.SecondOperation;

                _calcViewModel.Calculation.Calculate();

                _calcViewModel.FullExpression += _calcViewModel.SecondOperand + " " + operation;
                _calcViewModel.Display = _calcViewModel.Result;
                _calcViewModel.SecondOperand = operation;
                _calcViewModel.FirstOperand = _calcViewModel.Result;
                _calcViewModel.SecondOperand = string.Empty;
            }
            // 3 случай - все остальные случаи, а именно, когда первый операнд не пустой и мы нажимаем любую из возможных операций.
            // В таком случае полное выражение в FullExpression нам указывать не нужно. Там мы указываем сразу результат вычислений и оператор, который был нажат в данный момент. Так же выводим результат в Display.
            else
            {
                _calcViewModel.SecondOperand = _calcViewModel.Display;
                _calcViewModel.Operation = _calcViewModel.SecondOperation;

                _calcViewModel.Calculation.Calculate();

                _calcViewModel.FullExpression = _calcViewModel.Result + " " + operation + " ";
                _calcViewModel.Display = _calcViewModel.Result;
                _calcViewModel.SecondOperation = operation;
                _calcViewModel.FirstOperand = _calcViewModel.Result;
                _calcViewModel.SecondOperand = string.Empty;
            }

            _calcViewModel.NewDisplay = true;
        }
    }
}