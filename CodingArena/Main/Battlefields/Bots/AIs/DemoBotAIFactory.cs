using CodingArena.Main.Battlefields.Bots.AIs.Demo;
using System.Collections.Generic;
using CodingArena.AI;

namespace CodingArena.Main.Battlefields.Bots.AIs
{
    internal class DemoBotAIFactory : IBotAIFactory
    {
        public List<IBotAI> CreateBotAIs() => new List<IBotAI>
        {
            new Proto(),
            new Scrappie(),
            new Rust(),
            new Scyther(),
            new Sparky(),
            new Tinker(),
            new Twobit(),
            new Hammer(),
            new Juggernaut(),
            new Golem()
        };
    }
}