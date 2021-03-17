using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;

namespace ShimmyMySherbet.MultipleHomes.Models
{
    public class HomesConfig : IRocketPluginConfiguration
    {
        public int DefaultMaxHomes = 3;

        public void LoadDefaults()
        {
        }
    }
}
