/// <summary>
/// Struct representing an event object.
/// </summary>
public struct EventObject
{
    /// <summary>
    /// The text associated with the event object.
    /// </summary>
    public string text;

    /// <summary>
    /// An integer value associated with the event object.
    /// </summary>
    public int integer;

    /// <summary>
    /// A floating-point value associated with the event object.
    /// </summary>
    public float floatingPoint;

    /// <summary>
    /// Returns a string representation of the EventObject.
    /// </summary>
    /// <returns>A string that represents the current EventObject.</returns>
    public override string ToString()
    {
        return $"EventObject: {{ Text: \"{text}\", Integer: {integer}, FloatingPoint: {floatingPoint} }}";
    }
}
