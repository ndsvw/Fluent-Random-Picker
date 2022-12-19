using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace FluentRandomPicker.Exceptions;

/// <summary>
/// An exception indicating that the number of specified values is less than the number of values to pick.
/// </summary>
[Serializable]
[ExcludeFromCodeCoverage]
public class NotEnoughValuesToPickException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotEnoughValuesToPickException"/> class.
    /// </summary>
    public NotEnoughValuesToPickException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotEnoughValuesToPickException"/> class.
    /// </summary>
    /// <param name="message">A message.</param>
    public NotEnoughValuesToPickException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotEnoughValuesToPickException"/> class.
    /// </summary>
    /// <param name="message">A message.</param>
    /// <param name="innerException">An inner exception.</param>
    public NotEnoughValuesToPickException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotEnoughValuesToPickException"/> class.
    /// </summary>
    /// <param name="info">Information.</param>
    /// <param name="context">The context.</param>
    protected NotEnoughValuesToPickException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}