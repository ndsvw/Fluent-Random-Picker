using FluentRandomPicker.Random;

namespace FluentRandomPicker
{
    /// <summary>
    /// The "Out" part of the "Out.Of" syntax.
    /// </summary>
    public static class Out
    {
        /// <summary>
        /// The "Of" part of the "Out.Of" syntax.
        /// </summary>
        /// <returns>An "Of" instance.</returns>
        public static NonGenericOf Of() => NonGenericOf.Create();

        /// <summary>
        /// The "Of" part of the "Out.Of" syntax.
        /// </summary>
        /// <param name="seed">The seed.</param>
        /// <returns>An "Of" instance.</returns>
        public static NonGenericOf Of(int seed) => NonGenericOf.Create(seed);

        /// <summary>
        /// The "Of" part of the "Out.Of" syntax.
        /// </summary>
        /// <param name="rng">A random number generator.</param>
        /// <returns>An "Of" instance.</returns>
        public static NonGenericOf Of(IRandomNumberGenerator rng) => NonGenericOf.Create(rng);

        /// <summary>
        /// The "Of&lt;T&gt;" part of the generic "Out.Of" syntax.
        /// </summary>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <returns>An "Of" instance.</returns>
        public static GenericOf<T> Of<T>() => GenericOf<T>.Create();

        /// <summary>
        /// The "Of&lt;T&gt;" part of the generic "Out.Of" syntax.
        /// </summary>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <param name="seed">The seed.</param>
        /// <returns>An "Of" instance.</returns>
        public static GenericOf<T> Of<T>(int seed) => GenericOf<T>.Create(seed);

        /// <summary>
        /// The "Of&lt;T&gt;" part of the generic "Out.Of" syntax.
        /// </summary>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <param name="rng">A random number generator.</param>
        /// <returns>An "Of" instance.</returns>
        public static GenericOf<T> Of<T>(IRandomNumberGenerator rng) => GenericOf<T>.Create(rng);
    }
}