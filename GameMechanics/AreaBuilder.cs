namespace TheWorld.GameMechanics;

public record AreaBuilder
{
    private Area _area = null!;

    private AreaBuilder() { }

    /// <summary>
    /// Start with a FROM method.  An area starts from the name of its archetype.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static AreaBuilder FromName(string name) => new() { _area = new Area(Helpers.ValidateString(name, "The Void")) };

    /// <summary>
    /// Start with a FROM method.  An area starts from an existing area, which is copied and modified.
    /// </summary>
    public static AreaBuilder FromArea(Area area) => new() { _area = area with { Id = Guid.NewGuid() } };


    /// <summary>
    /// TODO: Start with a FROM method.  An area starts from a JSON string, which is parsed and modified.
    /// </summary>
    public static AreaBuilder FromJson(string json) => 
        throw new NotImplementedException("FromJson is not implemented yet.  It will be implemented in a future lesson.");

    /// <summary>
    /// Adds a description to the area being built.  This is optional, but recommended.
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    public AreaBuilder WithDescription(string description) => 
        this with 
        {
            _area = _area with { Description = Helpers.ValidateString(description, "A place of nothingness.") }
        };

    /// <summary>
    /// TODO: We need to revisit CreatureBuilder so that they are added into the GameData!
    /// </summary>
    /// <param name="creature"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public AreaBuilder WithCreature(CreatureReferenceLabel creature) =>
        throw new NotImplementedException("WithCreature is not implemented yet.");

    public AreaBuilder WithItem(ItemReferenceLabel item) =>
        this with
        {
            _area = _area with { Items = [.. _area.Items, item] }
        };

    /// <summary>
    /// Builds the area and adds it to the game data.
    /// </summary>
    /// <param name="gameData"></param>
    /// <returns>A label for the new area, or null if it couldn't be added.</returns>
    public AreaReferenceLabel? Build(GameData gameData)
    {
        if (gameData.TryAddArea(_area))
            return new() { Id = _area.Id, Label = _area.Name };

        return null;
    }
}