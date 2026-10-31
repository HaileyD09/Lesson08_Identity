namespace TheWorld.GameMechanics;

/// <summary>
/// A thing in the world.
/// </summary>
public record Item
{
    /// <summary>
    /// This is a unique identifier for the item.  It is pure data, and is not displayed ever.
    /// It is used to identify the item in the Game Engine.
    /// TODO:  Guid.NewGuid() is a placeholder!  Every Item gets a brand-new identity each time it's built,
    /// so the same Item comes back with a different Id every run.  Serialization will need to re-create
    /// an Item from the identity it was saved with.
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// The name must be provided at construction time.
    /// </summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>
    /// This is the interesting description of the item, which is shown to the player when they examine it.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Sometimes it is ok to expose a constructor, but not always. 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    internal Item(string name, string description)
    {
        Name = name;
        Description = description;
    }

    /// <summary>
    /// Returns a description of the item.
    /// TODO: This is a very simple implementation.  Its gonna get a lot more exciting soon ;)
    /// </summary>
    public string Examine() => $"You see {Name}.  {Description}";
}
