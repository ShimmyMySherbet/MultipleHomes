using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;

namespace ShimmyMySherbet.MultipleHomes.Commands
{
    public class SetHomeCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "SetHome";

        public string Help => "Sets a home";

        public string Syntax => "SetHome [Name]";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "MultipleHomes.Sethome" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length >= 1)
            {
                UnturnedPlayer player = (UnturnedPlayer)caller;

                string name = command[0];
                var plugin = MultipleHomes.Instance;

                int max = plugin.GetMaxHomes(player);
                int count = plugin.GetPlayerHomeCount(player);

                if (max >= count)
                {
                    UnturnedChat.Say(caller, $"Sorry, but you have reached the max amount of homes: {count}/{max}");
                    return;
                }
                

                if (plugin.HomeExists(player, name))
                {
                    UnturnedChat.Say(caller, "You already have a home with that name.");
                    return;
                }

                plugin.CreatePlayerHome(player, player.Position, name);

                UnturnedChat.Say(caller, $"Created home '{name}'");
            } else
            {
                UnturnedChat.Say(caller, Syntax);
            }
        }
    }
}
