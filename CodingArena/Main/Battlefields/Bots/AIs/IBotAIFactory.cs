using CodingArena.Player;
using System.Collections.Generic;

namespace CodingArena.Main.Battlefields.Bots.AIs
{
    public interface IBotAIFactory
    {
        List<IBotAI> CreateBotAIs();
    }
}