using Fluent_Random_Picker.Interfaces;

namespace Fluent_Random_Picker
{
    /// <summary>
    /// A non-generic implementation of the fluent part "Of".
    /// </summary>
    public class NonGenericOf
    {
        private NonGenericOf()
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="NonGenericOf"/>.
        /// </summary>
        /// <returns>A <see cref="NonGenericOf"/> instance.</returns>
        public static NonGenericOf Create()
        {
            return new NonGenericOf();
        }

        /// <summary>
        /// Specifies the first value.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="t">The value.</param>
        /// <returns>An object that can have an optional value priority and needs at least one more value.</returns>
#pragma warning disable CA1822 // Mark members as static; Justification: Necessary for the fluent syntax.
        public ICanHaveValuePriorityAndNeed1MoreValue<T> Value<T>(T t)
#pragma warning restore CA1822 // Mark members as static
        {
            return new RandomPicker<T>().Value(t);
        }
    }
}
