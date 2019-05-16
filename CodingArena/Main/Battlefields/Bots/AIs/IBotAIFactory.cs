using System.Collections.Generic;
using CodingArena.AI;

namespace CodingArena.Main.Battlefields.Bots.AIs
{
    public interface IBotAIFactory
    {
        List<BotAI> CreateBotAIs();
    }
}