using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShimmyMySherbet.MultipleHomes.Models
{
    public struct PlayerHome
    {
        public static readonly PlayerHome Nil = new PlayerHome(null, SVector.Nil);

        public string Name;
        public SVector Position;

        public PlayerHome(string name, SVector pos)
        {
            Name = name;
            Position = pos;
        }
    }
}
