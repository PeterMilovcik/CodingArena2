using CodingArena.Main.Battlefields;
using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bots.AIs;
using CodingArena.Player;
using System.Collections.Generic;

namespace CodingArena.Main.Rounds
{
    public sealed class Round
    {
        public Round()
        {
            var botAIFactory = new BotAIFactory<IDeathMatchAI>();
            var botAIs = botAIFactory.CreateBotAIs();
            Battlefield = new Battlefield();
            Bots = new List<DeathMatchBot>();
            foreach (var botAI in botAIs)
            {
                var bot = new DeathMatchBot(Battlefield, botAI);
                Bots.Add(bot);
            }
        }

        public Battlefield Battlefield { get; }

        public List<DeathMatchBot> Bots { get; }
    }
}