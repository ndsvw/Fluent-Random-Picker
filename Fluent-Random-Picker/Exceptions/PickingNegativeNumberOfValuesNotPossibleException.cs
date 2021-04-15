using System;
using System.Runtime.Serialization;

namespace Fluent_Random_Picker.Exceptions
{
    /// <summary>
    /// An exception indicating that picking a negative amount of values is not possible.
    /// </summary>
    [Serializable]
    public class PickingNegativeNumberOfValuesNotPossibleException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PickingNegativeNumberOfValuesNotPossibleException"/> class.
        /// </summary>
        public PickingNegativeNumberOfValuesNotPossibleException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PickingNegativeNumberOfValuesNotPossibleException"/> class.
        /// </summary>
        /// <param name="message">A message.</param>
        public PickingNegativeNumberOfValuesNotPossibleException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PickingNegativeNumberOfValuesNotPossibleException"/> class.
        /// </summary>
        /// <param name="message">A message.</param>
        /// <param name="innerException">An inner exception.</param>
        public PickingNegativeNumberOfValuesNotPossibleException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PickingNegativeNumberOfValuesNotPossibleException"/> class.
        /// </summary>
        /// <param name="info">Information.</param>
        /// <param name="context">The context.</param>
        protected PickingNegativeNumberOfValuesNotPossibleException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
