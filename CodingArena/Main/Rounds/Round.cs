﻿using CodingArena.Main.Battlefields;
using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bots.AIs;
using CodingArena.Main.Battlefields.Bullets;
using CodingArena.Main.Battlefields.Resources;
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
        private readonly Random myRandom;

        public Round()
        {
            var botAIFactory = new BotAIFactory();
            var botAIs = botAIFactory.CreateBotAIs();
            Battlefield = new Battlefield();
            Bots = new List<Bot>();
            myRandom = new Random();
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
                var newX = centerX + radius * Math.Cos(angle * Math.PI / 180);
                var newY = centerY + radius * Math.Sin(angle * Math.PI / 180);
                bot.SetPositionTo(new Point(newX, newY));
                angle += angleDif;
            }
        }

        private void AddResource()
        {
            var x = myRandom.Next((int)Battlefield.Width);
            var y = myRandom.Next((int)Battlefield.Height);
            var position = new Point(x, y);
            var resource = new Resource(position);
            Battlefield.Add(resource);
        }

        public Battlefield Battlefield { get; }

        public List<Bot> Bots { get; }

        public async Task StartAsync()
        {
            if (Bots.Any())
            {
                while (!HasWinner)
                {
                    if (!Battlefield.Resources.Any())
                    {
                        AddResource();
                    }
                    var bulletTasks = Battlefield.Bullets.ToList().OfType<Bullet>().Select(b => b.UpdateAsync());
                    await Task.WhenAll(bulletTasks);
                    var botTasks = Bots.Select(b => b.UpdateAsync());
                    await Task.WhenAll(botTasks);
                    await Task.Delay(TimeSpan.FromMilliseconds(myTurnDelay));
                }
            }
        }

        public bool HasWinner => Bots.Count(b => b.HitPoints.Actual > 0) <= 1;
    }
}