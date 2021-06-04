namespace Fluent_Random_Picker.Random
{
    /// <summary>
    /// The default implementation for <see cref="IRandomNumberGenerator"/>.
    /// </summary>
    public class DefaultRandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly System.Random m_Random;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRandomNumberGenerator"/> class.
        /// </summary>
        public DefaultRandomNumberGenerator()
        {
            m_Random = new System.Random();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRandomNumberGenerator"/> class.
        /// </summary>
        /// <param name="pSeed">The seed.</param>
        public DefaultRandomNumberGenerator(int pSeed)
        {
            m_Random = new System.Random(pSeed);
        }

        /// <inheritdoc/>
        public double NextDouble()
        {
            return m_Random.NextDouble();
        }

        /// <inheritdoc/>
        public int NextInt()
        {
            return m_Random.Next();
        }

        /// <inheritdoc/>
        public int NextInt(int pN)
        {
            return m_Random.Next(pN);
        }

        /// <inheritdoc/>
        public int NextInt(int pMin, int pMax)
        {
            return m_Random.Next(pMin, pMax);
        }
    }
}
