using FluentRandomPicker.Random;

namespace FluentRandomPicker;

/// <summary>
/// The static "Out" part of the "Out.Of" syntax.<br />
/// Call one of the <see cref="Of()"/> methods to continue.
/// </summary>
public static class Out
{
    /// <summary>
    /// The static "Of" part of the "Out.Of" syntax.
    /// </summary>
    /// <returns>A <see cref="NonGenericOf"/> instance that allows specifying
    /// one or more values.</returns>
    public static NonGenericOf Of() => NonGenericOf.Create();

    /// <summary>
    /// The static "Of" part of the "Out.Of" syntax.
    /// </summary>
    /// <param name="seed">The seed.</param>
    /// <returns>A <see cref="NonGenericOf"/> instance that allows specifying
    /// one or more values.</returns>
    public static NonGenericOf Of(int seed) => NonGenericOf.Create(seed);

    /// <summary>
    /// The static "Of" part of the "Out.Of" syntax.
    /// </summary>
    /// <param name="rng">A random number generator.</param>
    /// <returns>A <see cref="NonGenericOf"/> instance that allows specifying
    /// one or more values.</returns>
    public static NonGenericOf Of(IRandomNumberGenerator rng) => NonGenericOf.Create(rng);

    /// <summary>
    /// The static "Of&lt;T&gt;" part of the generic "Out.Of" syntax.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    /// <returns>A <see cref="GenericOf{T}"/> instance that allows specifying
    /// one or more values.</returns>
    public static GenericOf<T> Of<T>() => GenericOf<T>.Create();

    /// <summary>
    /// The static "Of&lt;T&gt;" part of the generic "Out.Of" syntax.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    /// <param name="seed">The seed.</param>
    /// <returns>A <see cref="GenericOf{T}"/> instance that allows specifying
    /// one or more values.</returns>
    public static GenericOf<T> Of<T>(int seed) => GenericOf<T>.Create(seed);

    /// <summary>
    /// The static "Of&lt;T&gt;" part of the generic "Out.Of" syntax.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    /// <param name="rng">A random number generator.</param>
    /// <returns>A <see cref="GenericOf{T}"/> instance that allows specifying
    /// one or more values.</returns>
    public static GenericOf<T> Of<T>(IRandomNumberGenerator rng) => GenericOf<T>.Create(rng);
}