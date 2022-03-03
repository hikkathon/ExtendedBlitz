using ExtendedBlitz.Infrastructure.Commands.Base;

namespace ExtendedBlitz.Infrastructure.Commands.Social
{
    internal class OpenTelegramCommand : CommandBase
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) 
        {
            System.Diagnostics.Process.Start("https://t.me/blitzbury");
        }
    }
}
