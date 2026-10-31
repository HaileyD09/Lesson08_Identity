namespace TheWorld.GameMechanics;

public record ItemBuilder
{
    private Item _item = null!;

    private ItemBuilder() { }

    /// <summary>
    /// Start with a FROM method.  An item starts from the name of its archetype.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static ItemBuilder FromName(string name) => new() { _item = new Item(Helpers.ValidateString(name, "Thingamajig"), string.Empty) };

    /// <summary>
    /// Start with a FROM method.  An item starts from an existing item, which is copied and modified.
    /// </summary>
    public static ItemBuilder FromItem(Item item) => new() { _item = item with { Id = Guid.NewGuid() } };

    /// <summary>
    /// Adds a description to the item being built.  This is optional, but recommended.
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    public ItemBuilder WithDescription(string description) =>
        this with
        {
            _item = _item with { Description = Helpers.ValidateString(description, "An unremarkable object.") }
        };

    /// <summary>
    /// Builds the item and adds it to the game data.
    /// </summary>
    /// <param name="gameData"></param>
    /// <returns>A label for the new item, or null if it couldn't be added.</returns>
    public ItemReferenceLabel? Build(GameData gameData)
    {
        if (gameData.TryAdd(_item))
            return new() { Id = _item.Id, Label = _item.Name };

        return null;
    }
}