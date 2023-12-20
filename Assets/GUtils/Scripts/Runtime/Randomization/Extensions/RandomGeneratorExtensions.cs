using System;
using System.Collections.Generic;
using System.Numerics;
using GUtils.Enums.Utils;
using GUtils.Optionals;
using GUtils.Randomization.Generators;
using GUtils.Extensions;

namespace GUtils.Randomization.Extensions
{
    public static class RandomGeneratorExtensions
    {
        /// <summary>
        /// Creates a new <see cref="Vector2"/> using the provided <see cref="IRandomGenerator"/> in
        /// the range [float.MinValue, float.MaxValue]
        /// </summary>
        /// <param name="randomGenerator">The random number generator to use.</param>
        /// <returns>A new instance of <see cref="Vector2"/> with randomly generated x and y values
        /// in the range [float.MinValue, float.MaxValue]</returns>
        public static Vector2 NewVector2(this IRandomGenerator randomGenerator)
        {
            return new Vector2(randomGenerator.NewFloat(), randomGenerator.NewFloat());
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> using the provided <see cref="IRandomGenerator"/> and
        /// specified range for x and y values.
        /// </summary>
        /// <param name="randomGenerator">The random number generator to use.</param>
        /// <param name="minInclusive">The minimum inclusive values for x and y.</param>
        /// <param name="maxInclusive">The maximum inclusive values for x and y.</param>
        /// <returns>A new instance of <see cref="Vector2"/> with randomly generated x and y values
        /// within the specified range.</returns>
        public static Vector2 NewVector2(this IRandomGenerator randomGenerator, Vector2 minInclusive, Vector2 maxInclusive)
        {
            return new Vector2(
                randomGenerator.NewFloat(minInclusive.X, maxInclusive.X),
                randomGenerator.NewFloat(minInclusive.Y, maxInclusive.Y)
            );
        }

        /// <summary>
        /// Performs a percentage roll using the provided <see cref="IRandomGenerator"/> with
        /// the given normalized percentage.
        /// </summary>
        /// <param name="randomGenerator">The random number generator to use.</param>
        /// <param name="normalizedPercentage">The normalized percentage (between 0 and 1) at which the roll succeeds.</param>
        /// <returns><c>true</c> if the roll is successful; otherwise, <c>false</c>.</returns>
        public static bool NewPercentageRoll(this IRandomGenerator randomGenerator, float normalizedPercentage)
        {
            normalizedPercentage = Math.Clamp(normalizedPercentage, 0f, 1f);

            float rollResult = randomGenerator.NewFloat(0f, 1f);

            return rollResult <= normalizedPercentage;
        }

        /// <summary>
        /// Creates a new <see cref="Optional{T}"/> by rolling a random percentage using the provided
        /// <see cref="IRandomGenerator"/>.
        /// </summary>
        /// <typeparam name="T">The type of value to generate.</typeparam>
        /// <param name="randomGenerator">The random number generator to use.</param>
        /// <param name="normalizedPercentage">The normalized percentage (between 0 and 1) at which
        /// to generate the value.</param>
        /// <param name="func">The function that generates the value.</param>
        /// <returns>An <see cref="Optional{T}"/> instance with the generated value if the roll is
        /// successful; otherwise, <see cref="Optional{T}.None"/>.</returns>
        public static Optional<T> NewPercentageRollOptional<T>(this IRandomGenerator randomGenerator, float normalizedPercentage, Func<T> func)
        {
            var roll = randomGenerator.NewPercentageRoll(normalizedPercentage);
            if (!roll)
            {
                return Optional<T>.None;
            }

            var value = func.Invoke();
            return Optional<T>.Some(value);
        }

        /// <summary>
        /// Tries to retrieve a random value from the specified array using the provided <see cref="IRandomGenerator"/>.
        /// </summary>
        /// <typeparam name="T">The type of value in the array.</typeparam>
        /// <param name="randomGenerator">The random number generator to use.</param>
        /// <param name="array">The array from which to retrieve a random value.</param>
        /// <param name="randomValue">The randomly selected value from the array.</param>
        /// <returns><c>true</c> if a random value was successfully retrieved; otherwise, <c>false</c>.</returns>
        public static bool TryGetRandom<T>(this IRandomGenerator randomGenerator, T[] array, out T randomValue)
        {
            int randomIndex = randomGenerator.NewInt(0, array.Length);

            return array.TryGet(randomIndex, out randomValue);
        }

        /// <summary>
        /// Tries to retrieve a random value from the specified read-only list using the provided
        /// <see cref="IRandomGenerator"/>.
        /// </summary>
        /// <typeparam name="T">The type of value in the list.</typeparam>
        /// <param name="randomGenerator">The random number generator to use.</param>
        /// <param name="list">The read-only list from which to retrieve a random value.</param>
        /// <param name="randomValue">The randomly selected value from the list.</param>
        /// <returns><c>true</c> if a random value was successfully retrieved; otherwise, <c>false</c>.</returns>
        public static bool TryGetRandom<T>(this IRandomGenerator randomGenerator, IReadOnlyList<T> list, out T randomValue)
        {
            int randomIndex = randomGenerator.NewInt(0, list.Count);

            return list.TryGet(randomIndex, out randomValue);
        }
        
        /// <summary>
        /// Tries to retrieve a random value from the specified array HashSet the provided <see cref="IRandomGenerator"/>.
        /// </summary>
        /// <typeparam name="T">The type of value in the HashSet.</typeparam>
        /// <param name="randomGenerator">The random number generator to use.</param>
        /// <param name="hashSet">The HashSet from which to retrieve a random value.</param>
        /// <param name="randomValue">The randomly selected value from the array.</param>
        /// <returns><c>true</c> if a random value was successfully retrieved; otherwise, <c>false</c>.</returns>
        public static bool TryGetRandom<T>(this IRandomGenerator randomGenerator, HashSet<T> hashSet, out T randomValue)
        {
            int randomIndex = randomGenerator.NewInt(0, hashSet.Count);
            
            int currentIndex = 0;
            foreach (T value in hashSet)
            {
                if (currentIndex == randomIndex)
                {
                    randomValue = value;
                    return true;
                }

                currentIndex++;
            }

            randomValue = default;
            return false;
        }
        
        /// <summary>
        /// Tries to retrieve a random value from the specified Dictionary the provided <see cref="IRandomGenerator"/>.
        /// </summary>
        /// <param name="randomGenerator">The random number generator to use.</param>
        /// <param name="dictionary">The Dictionary from which to retrieve a random value.</param>
        /// <param name="randomValue">The randomly selected value from the Dictionary.</param>
        /// <returns><c>true</c> if a random value was successfully retrieved; otherwise, <c>false</c>.</returns>
        public static bool TryGetRandomValue<T, Y>(this IRandomGenerator randomGenerator, Dictionary<T, Y> dictionary, out Y randomValue)
        {
            int randomIndex = randomGenerator.NewInt(0, dictionary.Count);
            
            int currentIndex = 0;
            foreach (Y value in dictionary.Values)
            {
                if (currentIndex == randomIndex)
                {
                    randomValue = value;
                    return true;
                }

                currentIndex++;
            }

            randomValue = default;
            return false;
        }
        
        /// <summary>
        /// Tries to retrieve a random value from the specified Dictionary the provided <see cref="IRandomGenerator"/>.
        /// </summary>
        /// <param name="randomGenerator">The random number generator to use.</param>
        /// <param name="dictionary">The Dictionary from which to retrieve a random value.</param>
        /// <param name="randomKey">The randomly selected key from the Dictionary.</param>
        /// <returns><c>true</c> if a random value was successfully retrieved; otherwise, <c>false</c>.</returns>
        public static bool TryGetRandomKey<T, Y>(this IRandomGenerator randomGenerator, Dictionary<T, Y> dictionary, out T randomKey)
        {
            int randomIndex = randomGenerator.NewInt(0, dictionary.Count);
            
            int currentIndex = 0;
            foreach (T value in dictionary.Keys)
            {
                if (currentIndex == randomIndex)
                {
                    randomKey = value;
                    return true;
                }

                currentIndex++;
            }

            randomKey = default;
            return false;
        }

        /// <summary>
        /// Tries to retrieve a random value from the specified <see cref="Enum"/> type using the
        /// provided <see cref="IRandomGenerator"/>.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="Enum"/>.</typeparam>
        /// <param name="randomGenerator">The random number generator to use.</param>
        /// <param name="randomValue">The randomly selected value from the <see cref="Enum"/>.</param>
        /// <returns><c>true</c> if a random value was successfully retrieved; otherwise, <c>false</c>.</returns>
        public static bool TryGetRandom<T>(
            this IRandomGenerator randomGenerator,
            out T randomValue
        ) where T : Enum
        {
            int lenght = EnumInfo<T>.Values.Length;

            if (lenght == 0)
            {
                randomValue = default;
                return false;
            }

            int randomIndex = randomGenerator.NewInt(0, lenght);

            randomValue = EnumInfo<T>.Values[randomIndex];
            return true;
        }

        /// <summary>
        /// Retrieves a random value from the specified <see cref="Enum"/> type using the
        /// provided <see cref="IRandomGenerator"/>.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="Enum"/>.</typeparam>
        /// <param name="randomGenerator">The random number generator to use.</param>
        /// <returns>A randomly selected value from the <see cref="Enum"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown if enum has no values.</exception>
        public static T UnsafeGetRandom<T>(this IRandomGenerator randomGenerator) where T : Enum
        {
            bool found = randomGenerator.TryGetRandom(out T randomValue);

            if (!found)
            {
                throw new InvalidOperationException($"Could not get random enum value from enum {typeof(T).Name}");
            }

            return randomValue;
        }
    }
}
