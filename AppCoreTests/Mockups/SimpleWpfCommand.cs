using System;

namespace AppCoreTests.Mockups
{
    public class SimpleWpfCommand:System.Windows.Input.ICommand
    {
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            // does nothing
        }

        public event EventHandler? CanExecuteChanged;
    }
}