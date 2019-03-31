using CodingArena.Annotations;
using System;
using System.Configuration;
using CodingArena.AI;

namespace CodingArena.Main.Battlefields.Bots
{
    public class Regeneration
    {
        private readonly Bot myBot;
        private TimeSpan myRegenerationActiveIn;
        private double myRegenerationAfterNoDamageInSeconds;

        public Regeneration([NotNull] Bot bot)
        {
            myBot = bot ?? throw new ArgumentNullException(nameof(bot));
        }

        public void Init()
        {
            myRegenerationAfterNoDamageInSeconds =
                double.Parse(ConfigurationManager.AppSettings["RegenerationAfterNoDamageInSeconds"]);
            Rate = double.Parse(ConfigurationManager.AppSettings["RegenerationPerSecond"]);
            myRegenerationActiveIn = TimeSpan.Zero;
        }

        public double Rate { get; private set; }

        public TimeSpan ActiveIn => new TimeSpan(myRegenerationActiveIn.Ticks);

        public void Regenerate() => Regenerate(myBot.RegenerationRate * myBot.DeltaTime.TotalSeconds);

        public void Regenerate(double amount)
        {
            var newActual = myBot.HitPoints.Actual + amount;
            newActual = Math.Min(newActual, myBot.HitPoints.Maximum);
            myBot.HitPoints = new Value(myBot.HitPoints.Maximum, newActual);
            myBot.BotAI.OnRegenerated();
            myBot.OnChanged();
        }


        public void Update()
        {
            if (myRegenerationActiveIn <= TimeSpan.Zero)
            {
                Regenerate();
            }
            else
            {
                myRegenerationActiveIn -= myBot.DeltaTime;
            }
        }

        public void ResetActiveIn() =>
            myRegenerationActiveIn = TimeSpan.FromSeconds(myRegenerationAfterNoDamageInSeconds);
    }
}