using System.Text;

namespace TheWorld.GameMechanics;

/// <summary>
/// A place in the world.
/// </summary>
public record Area : Identifiable
{
    /// <summary>
    /// This is a unique identifier for the area.  It is pure data, and is not displayed ever.
    /// It is used to identify the area in the Game Engine.
    /// TODO:  Guid.NewGuid() is a placeholder!  Every Area gets a brand-new identity each time it's built,
    /// so the same Area comes back with a different Id every run.  Serialization will need to re-create
    /// an Area from the identity it was saved with.
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// The name must be provided at construction time.
    /// </summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>
    /// This is the interesting description of the area, which is shown to the player when they enter it.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Creatures that live in this area are listed here.  
    /// This is a reference to the Creature, not the Creature itself, so that we can resolve it from the GameData.
    /// </summary>
    public IReadOnlyList<CreatureReferenceLabel> Creatures { get; init; } = [];

    /// <summary>
    /// Items that are in this area are listed here.  
    /// This is a reference to the Item, not the Item itself, so that we can resolve it from the GameData.
    /// </summary>
    public IReadOnlyList<ItemReferenceLabel> Items { get; init; } = [];   

    /// <summary>
    /// TODO: this will come later, because self-reference is WILD.
    /// </summary>
    public IReadOnlyList<AreaReferenceLabel> Neighbors { get; init; } = [];

    /// <summary>
    /// Sometimes it is ok to expose a constructor, but not always.  
    /// This one is for AreaBuilder, which is trusted to do the right thing.
    /// </summary>
    /// <param name="name"></param>
    internal Area(string name) { Name = name; }

    /// <summary>
    /// Returns a description of the area and its contents.
    /// </summary>
    /// <returns></returns>
    public string LookAround()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"You are in {Name}.");
        sb.AppendLine(Description);
        if (Creatures.Any())
        {
            sb.AppendLine("You see the following creatures:");
            foreach (var creature in Creatures)
            {
                sb.AppendLine($"- {creature.Label}");
            }
        }
        if (Items.Any())
        {
            sb.AppendLine("You see the following items:");
            foreach (var item in Items)
            {
                sb.AppendLine($"- {item.Label}");
            }
        }
        return sb.ToString();
    }
}