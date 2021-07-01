namespace FluentRandomPicker.Random
{
    /// <summary>
    /// Generates random numbers.
    /// </summary>
    public interface IRandomNumberGenerator
    {
        /// <summary>
        /// Generates a random int32 value.
        /// </summary>
        /// <returns>The random double value.</returns>
        int NextInt();

        /// <summary>
        /// Generates a random int32 value smaller than n.
        /// </summary>
        /// <param name="pN">The exclusive upper limit of the random int.</param>
        /// <returns>The random double value.</returns>
        int NextInt(int pN);

        /// <summary>
        /// Generates a random int32 value smaller than n.
        /// </summary>
        /// <param name="pMin">The lower limit of the random int (inclusive).</param>
        /// <param name="pMax">The upper limit of the random int (exclusive).</param>
        /// <returns>The random double value.</returns>
        int NextInt(int pMin, int pMax);

        /// <summary>
        /// Generates a random double value.
        /// </summary>
        /// <returns>The random double value.</returns>
        double NextDouble();
    }
}
