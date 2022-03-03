using ExtendedBlitz.Infrastructure.Commands.Base;

namespace ExtendedBlitz.Infrastructure.Commands.Social
{
    internal class OpenTwitchCommand : CommandBase
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            System.Diagnostics.Process.Start("https://www.twitch.tv/blitzbury");
        }
    }
}
