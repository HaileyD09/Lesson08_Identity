namespace TheWorld.GameMechanics;

public record ItemBuilder
{
    // Default values for every part of an Item, the same trick PlayerBuilder and CreatureBuilder use.
    private string _name = "Thingamajig";
    private string _description = string.Empty;

    private ItemBuilder() { }

    /// <summary>
    /// Start with a FROM method.  An item starts from the name of its archetype.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static ItemBuilder FromName(string name) => new() { _name = Helpers.ValidateString(name, "Thingamajig") };

    /// <summary>
    /// Start with a FROM method.  An item starts from the parts of an existing item, which can then be changed.
    /// The Id isn't one of the parts:  Build gives every new Item an identity of its own.
    /// </summary>
    public static ItemBuilder FromItem(Item item) => new() { _name = item.Name, _description = item.Description };

    /// <summary>
    /// Adds a description to the item being built.  This is optional, but recommended.
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    public ItemBuilder WithDescription(string description) =>
        this with { _description = Helpers.ValidateString(description, "An unremarkable object.") };

    /// <summary>
    /// Puts the item together from whatever parts have been set, and hands back the Item itself.
    /// StockItems uses this one, just like StockCreatures uses CreatureBuilder's.
    /// Every call builds a brand-new Item, so the same builder can make as many trees as you want.
    /// </summary>
    public Item Build() => new(_name, _description);

    /// <summary>
    /// Builds the item and adds it to the game data.
    /// </summary>
    /// <param name="gameData"></param>
    /// <returns>A label for the new item, or null if it couldn't be added.</returns>
    public ItemReferenceLabel? Build(GameData gameData)
    {
        var item = Build();
        if (gameData.TryAdd(item))
            return new() { Id = item.Id, Label = item.Name };

        return null;
    }
}
