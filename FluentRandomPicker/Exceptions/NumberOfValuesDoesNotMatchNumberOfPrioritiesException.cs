﻿using System;
using System.Runtime.Serialization;

namespace FluentRandomPicker.Exceptions
{
    /// <summary>
    /// An exception indicating that the number of values does not match the number of provided priorities.
    /// </summary>
    [Serializable]
    public class NumberOfValuesDoesNotMatchNumberOfPrioritiesException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberOfValuesDoesNotMatchNumberOfPrioritiesException"/> class.
        /// </summary>
        public NumberOfValuesDoesNotMatchNumberOfPrioritiesException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberOfValuesDoesNotMatchNumberOfPrioritiesException"/> class.
        /// </summary>
        /// <param name="message">A message.</param>
        public NumberOfValuesDoesNotMatchNumberOfPrioritiesException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberOfValuesDoesNotMatchNumberOfPrioritiesException"/> class.
        /// </summary>
        /// <param name="message">A message.</param>
        /// <param name="innerException">An inner exception.</param>
        public NumberOfValuesDoesNotMatchNumberOfPrioritiesException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberOfValuesDoesNotMatchNumberOfPrioritiesException"/> class.
        /// </summary>
        /// <param name="info">Information.</param>
        /// <param name="context">The context.</param>
        protected NumberOfValuesDoesNotMatchNumberOfPrioritiesException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
