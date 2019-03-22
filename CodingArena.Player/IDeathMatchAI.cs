using System.Collections.Generic;

namespace CodingArena.Player
{
    public interface IDeathMatchAI
    {
        string BotName { get; }

        ITurnAction Update(IBot ownBot, IReadOnlyList<IBot> enemies);
    }
}
