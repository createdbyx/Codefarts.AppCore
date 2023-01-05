using Codefarts.AppCore.Commands;

namespace AppCoreTests.Mockups
{
    public class EmptyCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            // does nothing
        }
    }
}