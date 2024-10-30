namespace calc.Model;

public class CalcModel
{
    private string _result;
    
    public string FOperand  { get; set; }
    public string SOperand { get; set; }
    public string Operation { get; set; }
    public string Result { get {return _result; } }

    public CalcModel()
    {
        FOperand = string.Empty;
        SOperand = string.Empty;
        Operation = string.Empty;
        _result = "0";
    }

    public void Calculate()
    {
        ValidateData();
        try
        {
            switch (Operation)
            {
                case "/":
                    _result = (Convert.ToDouble(FOperand) / Convert.ToDouble(SOperand)).ToString();
                    break;

                case "*":
                    _result = (Convert.ToDouble(FOperand) * Convert.ToDouble(SOperand)).ToString();
                    break;

                case "+":
                    _result = (Convert.ToDouble(FOperand) + Convert.ToDouble(SOperand)).ToString();
                    break;

                case "-":
                    _result = (Convert.ToDouble(FOperand) - Convert.ToDouble(SOperand)).ToString();
                    break;
            }
        }
        catch (Exception ex)
        {
            _result = ex.ToString();
            throw;
        }
    }
    
    private void ValidateData()
    {
        ValidateOperation(Operation);
        switch (Operation)
        {
            case "/":
            case "*":
            case "+":
            case "-":
                ValidateOperand(FOperand);
                ValidateOperand(SOperand);
                break;
        }
    }
    
    private void ValidateOperand(string operand)
    {
        try
        {
            Convert.ToDouble(operand);
        }
        catch
        {
            _result = "Invalid number: " + operand;
            throw;
        }
    }

    private void ValidateOperation(string operation)
    {
        switch (operation)
        {
            case "/":
            case "*":
            case "+":
            case "-":
                break;
            default:
                _result = "Invalid operation: " + operation;
                throw new ArgumentException($"Unknown Operation: {operation}");
        }
    }
}