// Check it out! An Alias!
// I'm giving a "Name" to the type which represents a Function that takes a Random number generator and a Dice,
// and then Returns an Integer (presumably from a dice-roll, but that's up to the programmer).
global using DiceRollStrategy = System.Func<System.Random, TheWorld.GameMechanics.Dice, int>;

// More Aliases!  One short name for each kind of ReferenceLabel (see GameData.cs).
global using AreaReferenceLabel = TheWorld.GameMechanics.ReferenceLabel<TheWorld.GameMechanics.Area>;
global using ItemReferenceLabel = TheWorld.GameMechanics.ReferenceLabel<TheWorld.GameMechanics.Item>;
global using CreatureReferenceLabel = TheWorld.GameMechanics.ReferenceLabel<TheWorld.GameMechanics.Creature>;

namespace TheWorld;

/// <summary>
/// Small, general-purpose tools that are useful all over the project.
/// Because this lives in the `TheWorld` namespace, every `TheWorld.Something` namespace can see it without a `using`.
/// </summary>
public static class Helpers
{
    /// <summary>
    /// Use Pattern Matching to make sure a string is Valid.
    /// The `null` case is checked first so the compiler knows `input` can't be null by the last case.
    /// `string.IsNullOrWhiteSpace` then catches "" and "                 ".
    /// The `.Trim()` method gets rid of any leading or trailing White-Space characters.
    /// Usage:  Helpers.ValidateString(name, "Frank")
    /// TODO:  Add more cases to this switch expression to catch other possible invalid strings.
    /// TODO:  Research:  Use "Regular Expressions" to validate that a name is made up ONLY of
    /// Alphabet characters, with valid punctuation inside the name.
    /// </summary>
    /// <returns>The trimmed input, or the fallback when there's nothing there but White-Space.</returns>
    public static string ValidateString(string? input, string fallback) =>
        input switch
        {
            null => fallback,
            var s when string.IsNullOrWhiteSpace(s) => fallback,
            _ => input.Trim()
        };
}
