using System.Text;

namespace BracketAlgorithm;

public class InfixToPostfixConverter
{
    private readonly Queue<string> _numbers = new Queue<string>();
    private readonly Stack<Operators> _operators = new Stack<Operators>();

    enum Operators
    {
        Plus,
        Minus,
        Divide,
        Multiply,
        LeftBracket,
        RightBracket
    }
    
    public string Convert(string expression)
    {
        foreach (var el in expression)
        {
            if(char.IsDigit(el))
                ExecuteNumber(el.ToString());
            switch (el)
            {
                case ('('): ExecuteLeftBracket();
                    break;
                case (')'): ExecuteRightBracket();
                    break;
                case ('+'): ExecuteOperator(Operators.Plus);
                    break;
                case ('-'): ExecuteOperator(Operators.Minus);
                    break;
                case ('/'): ExecuteOperator(Operators.Divide);
                    break;
                case ('*'): ExecuteOperator(Operators.Multiply);
                    break;
            }
        }
        MoveReminderToQueue();
        return ConvertQueueToString();
    }

    private string ConvertQueueToString()
    {
        var sb = new StringBuilder();
        while (_numbers.Count != 0)
        {
            sb.Append(_numbers.Dequeue());
            
        }
        
        return sb.ToString();
    }

    private void ExecuteNumber(string number)
    {
        _numbers.Enqueue(number);   
    }

    private void ExecuteOperator(Operators oper)
    {
        if(_operators.Count == 0 || _operators.Peek() == Operators.LeftBracket)
            _operators.Push(oper);
        else if(CompareOperators(oper, _operators.Peek()) == -1)
            _operators.Push(oper);
        else
        {
            while (_operators.Count != 0 
                   && CompareOperators(oper, _operators.Peek()) >= 0 
                   && _operators.Peek() != Operators.LeftBracket)
            {
                _numbers.Enqueue(ConvertOperatorToString(_operators.Pop()));
            }
            _operators.Push(oper);
        }
    }

    private Operators ConvertCharToOperator(char c)
    {
        switch (c)
        {
            case('+'): return Operators.Plus;
            case('-'): return Operators.Minus;
            case('*'): return Operators.Multiply;
            case('/'): return Operators.Divide;
            case('('): return Operators.LeftBracket;
            case(')'): return Operators.RightBracket;
        }

        throw new AggregateException("u make a mistake, dibil");
    }

    private int CompareOperators(Operators op1, Operators op2)
    {
        if (op1 is Operators.Plus or Operators.Minus
            && op2 is Operators.Multiply or Operators.Divide) return 1;
        if (op1 is Operators.Multiply or Operators.Divide
               && op2 is Operators.Plus or Operators.Minus) return -1; 
        return 0;
    }

    private string ConvertOperatorToString(Operators op)
    {
        switch (op)
        {
            case Operators.Plus: return "+";
            case Operators.Minus: return "-";
            case Operators.Multiply: return "*";
            case Operators.Divide: return "/";
            default: return string.Empty;
        }
    }

    private void ExecuteLeftBracket()
    {
        _operators.Push(Operators.LeftBracket);
    }

    private void ExecuteRightBracket()
    {
        while (_operators.Peek() != Operators.LeftBracket)
        {
            _numbers.Enqueue(ConvertOperatorToString(_operators.Pop()));
        }

        var _ = _operators.Pop();
        
    }

    private void MoveReminderToQueue()
    {
        while (_operators.Count != 0)
        {
            _numbers.Enqueue(ConvertOperatorToString(_operators.Pop())); 
        }
    }
}

class Program
{
    static void Main()
    {
        var converter = new InfixToPostfixConverter();
        var expression = "(3 + 7 + 8) * 1"; ;
        Console.WriteLine(converter.Convert(expression));
    }
}