using FluentRandomPicker.Random;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FluentRandomPickerTests.Random
{
    [TestClass]
    public class DefaultRandomNumberGeneratorTests
    {
        const int Iterations = 1_000_000;

        [TestMethod]
        public void NextDouble_WithoutSeed_ReturnsEquallyDistributedValues()
        {
            // Arrange
            var randomValues = new double[Iterations];
            var rng = new DefaultRandomNumberGenerator();
            const int AcceptedDeviation = 1000;

            // Action
            for (var i = 0; i < Iterations; i++)
            {
                randomValues[i] = rng.NextDouble();
            }

            // Assert
            Assert.IsTrue(randomValues.Sum() > Iterations / 2 - AcceptedDeviation);
            Assert.IsTrue(randomValues.Sum() < Iterations / 2 + AcceptedDeviation);
        }

        [TestMethod]
        public void NextDouble_WithoutSeed_ReturnsValuesBetween0And1()
        {
            // Arrange
            var randomValues = new double[Iterations];
            var rng = new DefaultRandomNumberGenerator();

            // Action
            for (var i = 0; i < Iterations; i++)
            {
                randomValues[i] = rng.NextDouble();
            }

            // Assert
            Assert.IsTrue(randomValues.All(x => x >= 0 && x < 1));
        }

        [TestMethod]
        public void NextDouble_WithSeed_ReturnsAlwaysTheSameValue()
        {
            // Arrange
            var randomValues = new double[Iterations];
            var seed = new System.Random().Next();

            // Action
            for (var i = 0; i < Iterations; i++)
            {
                var rng = new DefaultRandomNumberGenerator(seed);
                randomValues[i] = rng.NextDouble();
                var a = randomValues[i];
                Console.WriteLine(randomValues[i]);
            }

            // Assert
            Assert.IsTrue(randomValues.All(x => x == randomValues[0]));
        }


        [TestMethod]
        public void NextInt_NoArgumentWithoutSeed_ReturnsEquallyDistributedValues()
        {
            // Arrange
            var bitEquals1Counters = new int[31]; // to 31 because first bit indicates +-
            var rng = new DefaultRandomNumberGenerator();
            const int AcceptedDeviation = Iterations / 100;

            // Action
            for (var i = 0; i < Iterations; i++)
            {
                var value = rng.NextInt();
                for(var j = 0; j < 31; j++)
                {
                    if ((value & (1 << j)) == (1 << j))
                        bitEquals1Counters[j]++;
                }
            }

            // Assert
            Assert.IsTrue(bitEquals1Counters.All(x => x >= Iterations / 2 - AcceptedDeviation));
            Assert.IsTrue(bitEquals1Counters.All(x => x <= Iterations / 2 + AcceptedDeviation));
        }

        [TestMethod]
        public void NextInt_NoArgumentWithSeed_ReturnsAlwaysTheSameValue()
        {
            // Arrange
            var randomValues = new int[Iterations];
            var seed = new System.Random().Next();

            // Action
            for (var i = 0; i < Iterations; i++)
            {
                var rng = new DefaultRandomNumberGenerator(seed);
                randomValues[i] = rng.NextInt();
                var a = randomValues[i];
                Console.WriteLine(randomValues[i]);
            }

            // Assert
            Assert.IsTrue(randomValues.All(x => x == randomValues[0]));
        }

        [TestMethod]
        public void NextInt_OneArgumentWithoutSeed_ReturnsEquallyDistributedValues()
        {
            // Arrange
            var randomValues = new long[Iterations];
            var rng = new DefaultRandomNumberGenerator();
            const int AcceptedDeviation = 60;

            // Action
            for (var i = 0; i < Iterations; i++)
            {
                randomValues[i] = rng.NextInt(1_200);
            }

            // Assert
            Assert.IsTrue(randomValues.Sum() / Iterations > 1_200 / 2 - AcceptedDeviation);
            Assert.IsTrue(randomValues.Sum() / Iterations < 1_200 / 2 + AcceptedDeviation);
        }

        [TestMethod]
        public void NextInt_OneArgumentWithSeed_ReturnsAlwaysTheSameValue()
        {
            // Arrange
            var randomValues = new int[Iterations];
            var seed = new System.Random().Next();

            // Action
            for (var i = 0; i < Iterations; i++)
            {
                var rng = new DefaultRandomNumberGenerator(seed);
                randomValues[i] = rng.NextInt(1_200);
                var a = randomValues[i];
                Console.WriteLine(randomValues[i]);
            }

            // Assert
            Assert.IsTrue(randomValues.All(x => x == randomValues[0]));
        }

        [TestMethod]
        public void NextInt_TwoArgumentsWithoutSeed_ReturnsEquallyDistributedValues()
        {
            // Arrange
            var randomValues = new long[Iterations];
            var rng = new DefaultRandomNumberGenerator();
            const int AcceptedDeviation = 240;

            // Action
            for (var i = 0; i < Iterations; i++)
            {
                randomValues[i] = rng.NextInt(1_200, 3_600);
            }

            // Assert
            Assert.IsTrue(randomValues.Sum() / Iterations > 2_400 - AcceptedDeviation);
            Assert.IsTrue(randomValues.Sum() / Iterations < 2_400 + AcceptedDeviation);
        }

        [TestMethod]
        public void NextInt_TwoArgumentsWithSeed_ReturnsAlwaysTheSameValue()
        {
            // Arrange
            var randomValues = new int[Iterations];
            var seed = new System.Random().Next();

            // Action
            for (var i = 0; i < Iterations; i++)
            {
                var rng = new DefaultRandomNumberGenerator(seed);
                randomValues[i] = rng.NextInt(1_200, 3_600);
                var a = randomValues[i];
                Console.WriteLine(randomValues[i]);
            }

            // Assert
            Assert.IsTrue(randomValues.All(x => x == randomValues[0]));
        }
    }
}
