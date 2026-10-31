namespace TheWorld.GameMechanics;

/// <summary>
/// Anything the players fight.  A goblin, a wolf, a dragon.
///
/// Made of parts, the same as <see cref="Player"/>.  The constructor is
/// private, and there is one door in.  The internal Create is for
/// CreatureBuilder, and it trusts the builder to have done the checking
/// already.
/// </summary>
public record Creature
{
    /// <summary>
    /// This is a unique identifier for the creature.  It is pure data, and is not displayed ever.
    /// It is used to identify the creature in the Game Engine.
    /// TODO:  Guid.NewGuid() is a placeholder!  Every Creature gets a brand-new identity each time it's built,
    /// so the same Creature comes back with a different Id every run.  Serialization will need to re-create
    /// a Creature from the identity it was saved with.
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; }

    public HealthPoints HealthPoints { get; }

    public AbilityScore Abilities { get; }

    /// <summary>TODO: This is probably an object in its own rite. Experience the party earns for defeating it.</summary>
    public int ExperienceReward { get; }

    private Creature(string name,
        HealthPoints healthPoints,
        AbilityScore abilities,
        int experienceReward)
    {
        Name = name;
        HealthPoints = healthPoints;
        Abilities = abilities;
        ExperienceReward = experienceReward;
    }

    internal static Creature Create(string name, HealthPoints healthPoints,
        AbilityScore abilities, int experienceReward) => new(name, healthPoints, abilities, experienceReward);
}
