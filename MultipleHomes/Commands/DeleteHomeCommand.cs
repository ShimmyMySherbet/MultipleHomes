using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;

namespace ShimmyMySherbet.MultipleHomes.Commands
{
    public class DeleteHomeCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "DelHome";

        public string Help => "Deletes a home";

        public string Syntax => "DelHome [name]";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "MultipleHomes.DeleteHome" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length >= 1)
            {
                string name = command[0];
                UnturnedPlayer player = (UnturnedPlayer)caller;
                if (MultipleHomes.Instance.HomeExists(player, name))
                {
                    MultipleHomes.Instance.DeletePlayerHome(player, name);
                    UnturnedChat.Say(caller, $"Deleted home '{name}'");
                }
                else
                {
                    UnturnedChat.Say(caller, $"No home called '{name}'");
                }
            }
            else
            {
                UnturnedChat.Say(caller, Syntax);
            }
        }
    }
}