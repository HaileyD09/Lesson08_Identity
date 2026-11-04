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

Console.WriteLine(gameData.Get(tree)?.Examine() ?? "nothing to look at");

// A second tree, copied from the first.  Same name, same description, an identity of its own.
var tree2 = ItemBuilder
    .FromItem(gameData.Get(tree)!) // TODO: That ! is a hack.  We need to do something better someday...
    .WithDescription("Another big tree.")
    .Build(gameData);

// One recipe, two goblins.  Each call to Build makes a brand-new Creature with an identity of its own.
var goblinRecipe = CreatureBuilder
    .FromName("Goblin")
    .WithExperienceReward(50);
var goblin = goblinRecipe.Build(gameData);
var goblin2 = goblinRecipe.Build(gameData);

var g1 = gameData.Get(goblin)!;
var g2 = gameData.Get(goblin2)!;

var goblin3Ref = goblin! with { Label = "Goblin3" };

var g3 = g1 with { };

Console.WriteLine(ReferenceEquals(g1, g3));


var forest = AreaBuilder
    .FromName("Forest")
    .WithDescription("A dark forest.  It has a lot of trees.")
    .WithItem(tree!)
    .WithItem(tree2!)
    .WithCreature(goblin!)
    .WithCreature(goblin2!)
    .Build(gameData);

Console.WriteLine(gameData.Get(forest)?.LookAround() ?? "nothing to look at");

var wolf = CreatureBuilder
    .FromName("Wolf")
    .WithExperienceReward(50)
    .Build(gameData);

var bat = CreatureBuilder
    .FromName("Bat")
    .WithExperienceReward(50)
    .Build(gameData);

var lantern = ItemBuilder
    .FromName("Lantern")
    .WithDescription("A rusty lantern. Somebody left it here, still burning.")
    .Build(gameData);

var cave = AreaBuilder
    .FromName("Cave")
    .WithDescription("Damp and dark. Something drips, and something else squeaks.")
    .WithCreature(goblinRecipe.Build(gameData)!)
    .WithCreature(goblinRecipe.Build(gameData)!)
    .WithCreature(wolf!)
    .WithCreature(bat!)
    .WithItem(lantern!)
    .Build(gameData);

var forestArea = gameData.Get(forest)!;

forest = AreaBuilder.FromArea(forestArea)
    .WithNeighbor(cave!)
    .Build(gameData);

var caveArea = gameData.Get(cave)!;

cave = AreaBuilder.FromArea(caveArea)
    .WithNeighbor(forest with {Label = "Outside"})
    .Build(gameData);

player.ShowSheet();

// strings are stupid
// - have no context
// creaturereferencelabel
//  + name
//  + ID
// an ID and name that refers to a creature strongly
// ID and label that refer to item strongly for ItemReferenceLabel --> same properties, different type