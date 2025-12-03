namespace Lab3.DeliverySystem.Patterns.Command;

public class CommandInvoker
{
    private Stack<ICommand> _history = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _history.Push(command);
    }

    public void Undo()
    {
        if (_history.Count > 0)
        {
            var command = _history.Pop();
            command.Undo();
        }
    }
    
    public void ShowHistory()
    {
        if (_history.Count == 0)
        {
            Console.WriteLine("История пуста");
            return;
        }
        
        Console.WriteLine("\n=== История команд ===");
        int index = 1;
        foreach (var command in _history.Reverse())
        {
            Console.WriteLine($"{index++}. {command.GetDescription()}");
        }
    }
}