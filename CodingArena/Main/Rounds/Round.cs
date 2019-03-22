using CodingArena.Main.Battlefields;
using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bots.AIs;
using CodingArena.Player;
using System.Collections.Generic;

namespace CodingArena.Main.Rounds
{
    public class Round
    {
        public Round()
        {
            var botAIFactory = new BotAIFactory<IDeathMatchAI>();
            var botAIs = botAIFactory.CreateBotAIs();
            var battlefield = new Battlefield();
            var bots = new List<DeathMatchBot>();
            foreach (var botAI in botAIs)
            {
                new DeathMatchBot(battlefield, botAI);
            }
        }
    }
}