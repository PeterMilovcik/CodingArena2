# CodingArena 2.0

A game for programmers to compete against each other to show best skills for creativity, fast writing, iterative implementation and more by implementing bot artificial intelligence to fight against other bots.

> Current version will only support C# programming language.

### How to implement own bot

1. Create class assembly project for implementation of your bot.
2. Reference latest NuGet package called `CodingArena.AI`.
3. Implement interface [IBotAI](./CodingArena.AI/IBotAI.cs).
4. Build and upload assembly with bot implementation to server where the CodingArena game is hosted.

Here are few examples for implementing bot actions:

**Find closest resource**
``` csharp
var closestResource = battlefield.Resources.OrderBy(ownBot.DistanceTo).FirstOrDefault();
```

**Pickup resource**
``` csharp
return ownBot.DistanceTo(closestResource) < ownBot.Radius
   ? TurnAction.PickUpResource()
   : TurnAction.MoveTowards(closestResource);
```

**Drop resource to the base**
``` csharp
if (ownBot.HasResource)
{
   return ownBot.DistanceTo(ownBot.Home) > ownBot.Radius
      ? TurnAction.MoveTowards(ownBot.Home)
      : TurnAction.DropDownResource();
}
```

All possible actions are available by using [TurnAction](./CodingArena.AI/TurnAction.cs) static factory class.