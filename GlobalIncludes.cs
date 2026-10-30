// Check it out! An Alias!
// I'm giving a "Name" to the type which represents a Function that takes a Random number generator and a Dice,
// and then Returns an Integer (presumably from a dice-roll, but that's up to the programmer).
global using DiceRollStrategy = System.Func<System.Random, TheWorld.GameMechanics.Dice, int>;

namespace TheWorld;

/// <summary>
/// Small, general-purpose tools that are useful all over the project.
/// Because this lives in the `TheWorld` namespace, every `TheWorld.Something` namespace can see it without a `using`.
/// </summary>
public static class Helpers
{
    /// <summary>
    /// A First-Class Function: it's a Func stored in a field, so it can be called, passed around, or stored like any other value.
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
    public static readonly Func<string?, string, string> ValidateString = (input, fallback) =>
        input switch
        {
            null => fallback,
            var s when string.IsNullOrWhiteSpace(s) => fallback,
            _ => input.Trim()
        };
}
