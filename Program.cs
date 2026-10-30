using TheWorld.GameMechanics;
using TheWorld.Display;

using static TheWorld.Rules.DiceRollStrategies;

// This is the one Random Number Generator for the whole program.  We inject it everywhere a roll happens.
Random rng = new();
// This is the one GameData for the whole program.  We inject it everywhere a new entity is created, so that it can be added to the game data.
GameData gameData = new();

var player = PlayerBuilder
    .FromName("Kyroki")
    .WithHealthPoints(rng, Dice.D6 with { Count = 2, Modifier = 1 })
    .WithAbilities(rng, Dice.D20, ClampRoll(strategy: Advantage))
    .WithWallet(Money.Roll(rng, Dice.D100, Default))
    .Build();

// Build hands back a ReferenceLabel:  a name for people to read, and an Id for the GameData to look up.
var tree = ItemBuilder
    .FromName("Tree")
    .WithDescription("A big tree.  It has a lot of leaves.")
    .Build(gameData);

Console.WriteLine(tree?.Resolve(gameData)?.Examine() ?? "nothing to look at");

// A second tree, copied from the first.  Same name, same description, an identity of its own.
var tree2 = ItemBuilder
    .FromItem(tree!.Resolve(gameData)!) // TODO: Those ! are a hack.  We need to do something better someday...
    .WithDescription("Another big tree.")
    .Build(gameData);

var forest = AreaBuilder
    .FromName("Forest")
    .WithDescription("A dark forest.  It has a lot of trees.")
    .WithItem(tree)
    .WithItem(tree2!)
    .Build(gameData);

Console.WriteLine(forest?.Resolve(gameData)?.LookAround() ?? "nothing to look at");

player.ShowSheet();
