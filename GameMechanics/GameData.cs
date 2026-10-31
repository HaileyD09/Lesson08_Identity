namespace TheWorld.GameMechanics;

/// <summary>
/// All the data in the game!  
/// This is the one place where all the Areas, Items, and Creatures are stored.  
/// It is injected into every builder, so that they can add their new creations to the game data.
/// 
/// TODO: we need to add Players here too, so that we can save and load the game state eventually.
/// </summary>
public record GameData
{
    /// <summary>
    /// All the Area's in the game!
    /// </summary>
    private Dictionary<Guid,Area> Areas { get; init; } = [];

    /// <summary>
    /// All the Item's in the game!
    /// </summary>
    private Dictionary<Guid, Item> Items { get; init; } = [];

    /// <summary>
    /// All the Creature's in the game!
    /// </summary>
    private Dictionary<Guid, Creature> Creatures { get; init; } = [];

    //--------------------------------------------------------------
    //  Methods for adding and retrieving Areas, Items, and Creatures.
    // TODO:  This is a very simple implementation.  It does not handle duplicates, and it does not handle errors.  It is just a starting point.
    //--------------------------------------------------------------

    public bool TryAddArea(Area area) => Areas.TryAdd(area.Id, area);
    public bool TryAddItem(Item item) => Items.TryAdd(item.Id, item);
    public bool TryAddCreature(Creature creature) => Creatures.TryAdd(creature.Id, creature);

    //--------------------------------------------------------------
    //  Methods for retrieving Areas, Items, and Creatures by their Id.
    // TODO:  This is a very simple implementation.  It does not handle errors.  It is just a starting point.
    //--------------------------------------------------------------
    public Area? GetArea(Guid id) => Areas.GetValueOrDefault(id);
    public Item? GetItem(Guid id) => Items.GetValueOrDefault(id);
    public Creature? GetCreature(Guid id) => Creatures.GetValueOrDefault(id);
}

/// <summary>
/// A ReferenceLabel is a thing that has a label and a unique identifier.  It is pure data, and is not displayed ever.
/// It is a reference to a thing in the GameData, and can be used to resolve that thing from the GameData.
///
/// This structure is useful for saving and loading game data, as it allows us to store a reference to a thing without storing the entire object.
/// The Label and Id are written once, and the <T> says what kind of thing the label is for.
/// The short names (ItemReferenceLabel, etc.) are Aliases, in GlobalIncludes.cs.
///
/// This is called a Strongly Typed Key.  The Guid inside could open anything, but the label's Type says what kind of thing it opens,
/// so the compiler won't let an ItemReferenceLabel go where an AreaReferenceLabel belongs.
/// </summary>
/// <typeparam name="T"></typeparam>
public record ReferenceLabel<T> where T : class
{
    public required string Label { get; init; }

    public required Guid Id { get; init; }
}

/// <summary>
/// One Resolve for each kind of label.  These are Extension Methods, so they read like the label's own:  tree.Resolve(gameData).
/// The compiler picks the right one from the label's Type, and a label with no Resolve here won't compile.
/// </summary>
public static class ReferenceLabelExtensions
{
    public static Area? Resolve(this AreaReferenceLabel label, GameData gameData) => gameData.GetArea(label.Id);
    public static Item? Resolve(this ItemReferenceLabel label, GameData gameData) => gameData.GetItem(label.Id);
    public static Creature? Resolve(this CreatureReferenceLabel label, GameData gameData) => gameData.GetCreature(label.Id);
}

