using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShimmyMySherbet.MultipleHomes.Models
{
    public class HomeIndex
    {
        public Dictionary<ulong, List<PlayerHome>> Homes = new Dictionary<ulong, List<PlayerHome>>();
    }
}
