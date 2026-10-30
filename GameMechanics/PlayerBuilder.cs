using TheWorld.Rules;
using static TheWorld.Rules.DiceRollStrategies;
namespace TheWorld.GameMechanics;

/// <summary>
/// Builds a <see cref="Player"/> one part at a time.
///
/// Start from a name with FromName.  Each part gets its own With... method,
/// and Build() puts the finished player together.  Any part you skip keeps its
/// default.
///
/// The builder is a record, so every With... hands back a new copy with one
/// part changed.  The builder you called it on is left exactly as it was.
/// </summary>
public record PlayerBuilder
{
    
    // Default values for every part of a Character.  This is ONE WAY (not
    // necessarily a GOOD way) to guarantee that Build() always makes a valid
    // Character...
    // TODO:  Find a better way!
    private string _name = "Frank";
    private PlayerLevel _level = PlayerLevel.Level1;
    private HealthPoints _healthPoints = HealthPoints.Create(20);
    private AbilityScore _abilities = AbilityScore.Create(10, 10, 10, 10, 10, 10)!;
    private Money _wallet = Money.Default;

    /// <summary>
    /// Does Nothing, except force every caller to start from one of the From... Factory Methods.
    /// </summary>
    private PlayerBuilder() { }

    /// <summary>
    /// Start with a FROM method.  The first natural place to start is the player's name.
    /// </summary>
    /// <param name="name">Checked by Helpers.ValidateString, in GlobalIncludes.cs.</param>
    public static PlayerBuilder FromName(string name) => new() { _name = Helpers.ValidateString(name, "Frank") };

    //------------------------------------------------------------------------------------
    // With methods are how we "Build" the Player from its constituent parts.
    // Blindly handing the values to the class is the most basic way to do this,
    // and it checks no more than the Factory method we wrote in Lesson 5.
    // Which means we can do better!
    //------------------------------------------------------------------------------------
    
    public PlayerBuilder WithLevel(PlayerLevel level) => this with { _level = level };
    public PlayerBuilder WithHealthPoints(HealthPoints hp) => this with { _healthPoints = hp };
    public PlayerBuilder WithAbilities(AbilityScore abilities) => this with { _abilities = abilities };
    public PlayerBuilder WithWallet(Money wallet) => this with { _wallet = wallet };
    
    /// <summary>
    /// Life is more exciting when you've got randomness in play.
    /// Rolls the hit dice for max health, using ClampRoll's rules:  anything from 3 up to the
    /// most those dice can roll.
    /// </summary>
    /// <param name="rng">Where the randomness comes from.</param>
    /// <param name="hitDice">The dice this character rolls for health.  They're kept on the HealthPoints too.</param>
    public PlayerBuilder WithHealthPoints(Random rng, Dice hitDice) => this with 
        { _healthPoints = HealthPoints.Roll(rng, hitDice, ClampRoll(3, hitDice.Maximum)) };

    /// <summary>
    /// Here's the traditional hard-core way to do this.
    /// 3d6 for each score, rolled in order, and whatever comes up is what you get.
    /// You could end up with all 3s or all 18s.  And you only get one shot.
    /// </summary>
    /// <param name="rng">Where the randomness comes from.</param>
    public PlayerBuilder WithAbilities(Random rng) => this with
        { _abilities = AbilityScore.Roll(rng, Dice.D6 with { Count = 3 }, Default) };
    
    /// <summary>
    /// The Fully Strategy Based approach gives the programmer the power to manipulate the outcome, or to
    /// create undesirable ones.  The scores come back unchecked.  That responsibility is outsourced to the
    /// DiceRollStrategy you hand in.
    /// </summary>
    /// <param name="rng">Where the randomness comes from.</param>
    /// <param name="dice">The dice rolled for each score.</param>
    /// <param name="strategy">How each score gets rolled.  Called six times, once per score.</param>
    public PlayerBuilder WithAbilities(Random rng, Dice dice, DiceRollStrategy strategy) => this with
        { _abilities = AbilityScore.Roll(rng, dice, strategy) };

    /// <summary>
    /// Rolls for starting gold, the same way WithAbilities rolls for scores.
    /// The gold comes back unchecked too, so the strategy decides what's sensible.
    /// </summary>
    /// <param name="rng">Where the randomness comes from.</param>
    /// <param name="dice">The dice rolled for gold.</param>
    /// <param name="strategy">How the gold gets rolled.</param>
    public PlayerBuilder WithWallet(Random rng, Dice dice, DiceRollStrategy strategy) => this with
        { _wallet = Money.Roll(rng, dice, strategy) };

    /// <summary>
    /// Puts the player together from whatever parts have been set.
    /// </summary>
    public Player Build() =>
        Player.Create(_name, _level, _healthPoints, _abilities, _wallet);
    
    #region Future Projects!
    /// <summary>
    /// TODO:  Research:  JSON Serialization of Objects.
    /// </summary>
    /// <exception cref="NotImplementedException">Always, for now.</exception>
    public static PlayerBuilder FromJson(string jsonString) => 
        throw new NotImplementedException("Not yet implemented");
    #endregion
}
