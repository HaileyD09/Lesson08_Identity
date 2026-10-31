namespace TheWorld.GameMechanics;

/// <summary>
/// Builds a <see cref="Creature"/> one part at a time.
///
/// Smaller than <see cref="PlayerBuilder"/>, on purpose.  A player is one of a
/// kind, so their numbers get rolled.  A creature comes from an archetype:
/// every goblin is built from the same goblin recipe, so its numbers get set
/// directly.
/// </summary>
public record CreatureBuilder
{
    // Default values for every part of a Creature, the same trick PlayerBuilder uses.
    private string _name = "Blob";
    private HealthPoints _healthPoints = HealthPoints.Create(1);
    private AbilityScore _abilities = AbilityScore.Create(10, 10, 10, 10, 10, 10)!;
    private int _experienceReward = 0;

    /// <summary>
    /// Does Nothing, except force every caller to start from FromName.
    /// </summary>
    private CreatureBuilder() { }

    /// <summary>
    /// Start with a FROM method.  A creature starts from the name of its archetype.
    /// </summary>
    public static CreatureBuilder FromName(string name) => new() { _name = Helpers.ValidateString(name, "Blob") };

    public CreatureBuilder WithHealthPoints(HealthPoints hp) => this with { _healthPoints = hp };
    public CreatureBuilder WithAbilities(AbilityScore abilities) => this with { _abilities = abilities };
    public CreatureBuilder WithExperienceReward(int xp) => this with { _experienceReward = xp };

    /// <summary>
    /// Puts the creature together from whatever parts have been set.
    /// StockCreatures still uses this one, so it hands back the Creature itself.
    /// </summary>
    public Creature Build() =>
        Creature.Create(_name, _healthPoints, _abilities, _experienceReward);

    /// <summary>
    /// Builds the creature and adds it to the game data, just like AreaBuilder and ItemBuilder do.
    /// Every call builds a brand-new Creature, so the same builder can make as many goblins as you want.
    /// </summary>
    /// <returns>A label for the new creature, or null if it couldn't be added.</returns>
    public CreatureReferenceLabel? Build(GameData gameData)
    {
        var creature = Build();
        if (gameData.TryAdd(creature))
            return new() { Id = creature.Id, Label = creature.Name };

        return null;
    }
}
