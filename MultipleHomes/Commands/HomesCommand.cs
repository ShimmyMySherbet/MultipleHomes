using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;

namespace ShimmyMySherbet.MultipleHomes.Commands
{
    public class HomesCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "Homes";

        public string Help => "Lists your homes";

        public string Syntax => Name;

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "ShimmyMySherbet.Homes" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            int c = MultipleHomes.Instance.GetPlayerHomeCount(player);
            int m = MultipleHomes.Instance.GetMaxHomes(player);
            string[] homes = MultipleHomes.Instance.GetPlayerHomes(player);
            UnturnedChat.Say(caller, $"You have {c}/{m} homes.");
            UnturnedChat.Say(caller, string.Join(", ", homes));
        }
    }
}