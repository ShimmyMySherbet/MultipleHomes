using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;

namespace ShimmyMySherbet.MultipleHomes.Commands
{
    public class HomeCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "Home";

        public string Help => "TPs you to your home";

        public string Syntax => "Home [name]";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "MultipleHomes.Home" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length >= 1)
            {
                string name = command[0];
                UnturnedPlayer player = (UnturnedPlayer)caller;
                if (MultipleHomes.Instance.TryGetHome(player, name, out var home))
                {
                    if (player.Player.teleportToLocation(home.Position.ToVector3() + new UnityEngine.Vector3(0f, 1f, 0f), player.Rotation))
                    {
                        UnturnedChat.Say(caller, $"Teleported to home '{name}'");
                    }
                    else
                    {
                        UnturnedChat.Say(caller, "Home obstructed.");
                    }
                }
            }
            else
            {
                UnturnedChat.Say(caller, Syntax);
            }
        }
    }
}