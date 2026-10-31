namespace TheWorld.GameMechanics;

/// <summary>
/// A player character.  Everything a character sheet says, one property per
/// line.
///
/// The constructor is private, and there is one door in.  The internal
/// Create is for PlayerBuilder, and it trusts the builder to have done the
/// checking already.
/// </summary>
public record Player
{
    // TODO: Give Player a Guid Id, just like Creature does.
    // Then add Players to the GameData (with a PlayerReferenceLabel), and inject the GameData into PlayerBuilder.Build,
    // just like AreaBuilder and ItemBuilder do.

    public string Name { get; }
    
    public PlayerLevel Level { get; }

    public HealthPoints HealthPoints { get; }

    public AbilityScore Abilities { get; }

    public Money MyWallet { get; }

    private Player(string name, 
        PlayerLevel level,
        HealthPoints healthPoints,
        AbilityScore abilities,
        Money myWallet)
    {
        Name = name;
        Level = level;
        HealthPoints = healthPoints;
        Abilities = abilities;
        MyWallet = myWallet;
    }
    
    internal static Player Create(string name, PlayerLevel level, HealthPoints healthPoints,
        AbilityScore abilities, Money myWallet) => new(name, level, healthPoints, abilities, myWallet);
}
