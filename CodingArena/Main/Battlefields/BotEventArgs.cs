using CodingArena.Annotations;
using CodingArena.Main.Battlefields.Bots;
using System;

namespace CodingArena.Main.Battlefields
{
    public class BotEventArgs : EventArgs
    {
        public DeathMatchBot Bot { get; }

        public BotEventArgs([NotNull] DeathMatchBot bot)
        {
            Bot = bot ?? throw new ArgumentNullException(nameof(bot));
        }
    }
}