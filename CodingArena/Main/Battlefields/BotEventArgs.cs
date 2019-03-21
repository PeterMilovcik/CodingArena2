using CodingArena.Annotations;
using CodingArena.Main.Battlefields.Bots;
using System;

namespace CodingArena.Main.Battlefields
{
    public class BotEventArgs : EventArgs
    {
        public Bot Bot { get; }

        public BotEventArgs([NotNull] Bot bot)
        {
            Bot = bot ?? throw new ArgumentNullException(nameof(bot));
        }
    }
}