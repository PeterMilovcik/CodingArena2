using CodingArena.Main.Battlefields;
using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bots.AIs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CodingArena.Main.Rounds
{
    public sealed class Round
    {
        private readonly double myTurnDelay;

        public Round()
        {
            var botAIFactory = new BotAIFactory();
            var botAIs = botAIFactory.CreateBotAIs();
            Battlefield = new Battlefield();
            Bots = new List<Bot>();

            foreach (var botAI in botAIs)
            {
                var bot = new Bot(Battlefield, botAI);
                Battlefield.Add(bot);
                Bots.Add(bot);
            }

            InitializePositions();

            myTurnDelay = double.Parse(ConfigurationManager.AppSettings["TurnDelayInMilliseconds"]);
        }

        private void InitializePositions()
        {
            var centerX = Battlefield.Width / 2;
            var centerY = Battlefield.Height / 2;
            var d = Math.Min(Battlefield.Width, Battlefield.Height) / 2;
            var radius = d - d / 10;
            double angle = 0;
            double angleDif = 360 / Bots.Count;
            foreach (var bot in Bots)
            {
                var newX = centerX + radius * Math.Cos(angle);
                var newY = centerY + radius * Math.Sin(angle);
                bot.SetPositionTo(new Point(newX, newY));
                angle += angleDif;
            }
        }

        public Battlefield Battlefield { get; }

        public List<Bot> Bots { get; }

        public async Task StartAsync()
        {
            if (Bots.Any())
            {
                while (!HasWinner)
                {
                    var tasks = Bots.Select(b => b.UpdateAsync());
                    await Task.WhenAll(tasks);
                    await Task.Delay(TimeSpan.FromMilliseconds(myTurnDelay));
                }
            }
        }

        public bool HasWinner { get; set; }
    }
}