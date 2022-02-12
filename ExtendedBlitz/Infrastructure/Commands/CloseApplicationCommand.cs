using ExtendedBlitz.Infrastructure.Commands.Base;
using System.Windows;

namespace ExtendedBlitz.Infrastructure.Commands
{
    internal class CloseApplicationCommand : CommandBase
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => Application.Current.Shutdown();
    }
}
