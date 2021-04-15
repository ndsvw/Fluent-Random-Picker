using Fluent_Random_Picker.Interfaces;

namespace Fluent_Random_Picker
{
    /// <summary>
    /// A generic implementation of the fluent part "Of".
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    public class GenericOf<T>
    {
        private GenericOf()
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="GenericOf{T}"/>.
        /// </summary>
        /// <returns>A <see cref="GenericOf{T}"/> instance.</returns>
        public static GenericOf<T> Create()
        {
            return new GenericOf<T>();
        }

        /// <summary>
        /// Specifies the first value.
        /// </summary>
        /// <param name="t">The value.</param>
        /// <returns>An object that can have an optional value priority and needs at least one more value.</returns>
        public ICanHaveValuePriorityAndNeed1MoreValue<T> Value(T t)
        {
            return new RandomPicker<T>().Value(t);
        }
    }
}
