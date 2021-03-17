using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using ShimmyMySherbet.MultipleHomes.Models;
using UnityEngine;

namespace ShimmyMySherbet.MultipleHomes
{
    public class MultipleHomes : RocketPlugin<HomesConfig>
    {
        public string HomeIndexFile => Path.Combine(Directory, "Homes.json");
        private HomeIndex m_homeIndex;

        public static MultipleHomes Instance;

        public HomeIndex HomeIndex
        {
            get
            {
                if (m_homeIndex == null)
                {
                    if (File.Exists(HomeIndexFile))
                    {
                        m_homeIndex = JsonConvert.DeserializeObject<HomeIndex>(File.ReadAllText(HomeIndexFile));
                    }
                    else
                    {
                        m_homeIndex = new HomeIndex();
                    }
                }
                return m_homeIndex;
            }
        }

        public override void LoadPlugin()
        {
            base.LoadPlugin();
            Instance = this;
        }

        public override void UnloadPlugin(PluginState state = PluginState.Unloaded)
        {
            base.UnloadPlugin(state);
            Instance = null;
        }

        public void Save()
        {
            File.WriteAllText(HomeIndexFile, JsonConvert.SerializeObject(HomeIndex));
        }

        public bool TryGetHome(UnturnedPlayer player, string name, out PlayerHome home)
        {
            if (HomeIndex.Homes.ContainsKey(player.CSteamID.m_SteamID))
            {
                var res = HomeIndex.Homes[player.CSteamID.m_SteamID]
                    .Where(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    .ToList();

                if (res.Count > 0)
                {
                    home = res[0];
                    return true;
                }
            }
            home = PlayerHome.Nil;
            return false;
        }

        public bool HomeExists(UnturnedPlayer player, string name) => TryGetHome(player, name, out _);

        public int GetPlayerHomeCount(UnturnedPlayer player)
        {
            if (HomeIndex.Homes.ContainsKey(player.CSteamID.m_SteamID))
                return HomeIndex.Homes[player.CSteamID.m_SteamID].Count();
            return 0;
        }

        public void DeletePlayerHome(UnturnedPlayer player, string name)
        {
            if (HomeIndex.Homes.ContainsKey(player.CSteamID.m_SteamID))
            {
                HomeIndex.Homes[player.CSteamID.m_SteamID].RemoveAll(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
                Save();
            }
        }

        public string[] GetPlayerHomes(UnturnedPlayer player)
        {
            if (HomeIndex.Homes.ContainsKey(player.CSteamID.m_SteamID))
            {
                return HomeIndex.Homes[player.CSteamID.m_SteamID].Select(x => x.Name).ToArray();
            }
            return new string[0];
        }

        public PlayerHome CreatePlayerHome(UnturnedPlayer player, Vector3 position, string name)
        {
            PlayerHome home = new PlayerHome(name, new SVector(position));

            if (!HomeIndex.Homes.ContainsKey(player.CSteamID.m_SteamID))
            {
                HomeIndex.Homes.Add(player.CSteamID.m_SteamID, new List<PlayerHome>() { home });
            }
            else
            {
                HomeIndex.Homes[player.CSteamID.m_SteamID].Add(home);
            }

            Save();
            return home;
        }

        public int GetMaxHomes(UnturnedPlayer player)
        {
            int max = -1;
            foreach (var perm in player.GetPermissions().Where(x => x.Name.ToLower().StartsWith("multiplehomes.maxhomes.")))
            {
                string val = perm.Name.Substring(23);
                if (int.TryParse(val, out int mx) && mx > max)
                    max = mx;
            }
            if (max == -1) max = Configuration.Instance.DefaultMaxHomes;
            return max;
        }
    }
}