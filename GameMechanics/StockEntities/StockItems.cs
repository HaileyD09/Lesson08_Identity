namespace TheWorld.GameMechanics.StockEntities;

/// <summary>
/// Ready-made items, from more than one kind of game.
/// Every one is an archetype:  ask for a Lantern twice and you get two lanterns
/// built from the same recipe.
///
/// They are grouped by what a player ought to be able to do with them.  Right now an
/// Item can't tell you any of that:  a Statue and a Health Potion are the same Type,
/// with a Name and a Description.  The only place the difference lives is in these
/// comments.
/// </summary>
public static class StockItems
{
    // ---- Scenery:  there to look at, and nothing else. ----

    public static Item Statue =>
        ItemBuilder.FromName("Statue")
            .WithDescription("A weathered stone king.  Someone has chipped the name off the base.")
            .Build();

    public static Item Viewport =>
        ItemBuilder.FromName("Viewport")
            .WithDescription("A long window of thick glass.  The stars outside don't move.")
            .Build();

    public static Item WaterTower =>
        ItemBuilder.FromName("Water Tower")
            .WithDescription("A wooden tank on stilts.  It leaks a slow, steady drip into the dust.")
            .Build();

    // ---- Carryable:  you can pick it up, and that's all it does. ----

    public static Item Locket =>
        ItemBuilder.FromName("Locket")
            .WithDescription("A tarnished silver locket.  The picture inside has been scratched out.")
            .Build();

    public static Item DataChip =>
        ItemBuilder.FromName("Data Chip")
            .WithDescription("A sliver of black glass with a corporate logo on it.  It hums faintly.")
            .Build();

    // ---- Usable:  you can do something with it, but it stays where it is. ----

    public static Item Lever =>
        ItemBuilder.FromName("Lever")
            .WithDescription("An iron lever set into the wall.  It's in the up position.")
            .Build();

    public static Item ControlPanel =>
        ItemBuilder.FromName("Control Panel")
            .WithDescription("A bank of switches beside the airlock.  One light is blinking red.")
            .Build();

    public static Item Piano =>
        ItemBuilder.FromName("Piano")
            .WithDescription("An upright saloon piano.  Three of the keys are missing.")
            .Build();

    // ---- Carryable and usable. ----

    public static Item Lantern =>
        ItemBuilder.FromName("Lantern")
            .WithDescription("A brass oil lantern.  It sloshes when you shake it.")
            .Build();

    public static Item HealthPotion =>
        ItemBuilder.FromName("Health Potion")
            .WithDescription("A small red bottle.  It smells like cinnamon and pennies.")
            .Build();

    public static Item Revolver =>
        ItemBuilder.FromName("Revolver")
            .WithDescription("A six-shooter with a worn grip.  Four rounds in the cylinder.")
            .Build();

    // ---- Harder to sort. ----

    /// <summary>
    /// Carryable, and only useful on something else:  it does nothing until there's a lock.
    /// </summary>
    public static Item BrassKey =>
        ItemBuilder.FromName("Brass Key")
            .WithDescription("A heavy brass key.  The tag on it says CELLAR.")
            .Build();

    /// <summary>
    /// Too heavy to carry, and it opens, and there are things inside it.
    /// </summary>
    public static Item Chest =>
        ItemBuilder.FromName("Chest")
            .WithDescription("An iron-banded chest.  The lock is the size of your fist.")
            .Build();

    /// <summary>
    /// You can read it, and you could take it down, and the reward on it might matter later.
    /// </summary>
    public static Item WantedPoster =>
        ItemBuilder.FromName("Wanted Poster")
            .WithDescription("A face you almost recognize.  $500 REWARD, DEAD OR ALIVE.")
            .Build();
}
