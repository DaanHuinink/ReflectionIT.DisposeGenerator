namespace ReflectionIT.DisposeGenerator.Attributes;

/// <summary>
/// Specifies the order in which an annotated member is disposed.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public class DisposeOrderAttribute : Attribute {

    /// <summary>
    /// Initializes a new instance of the <see cref="DisposeOrderAttribute"/> class.
    /// </summary>
    /// <param name="order">The order in which the annotated member is disposed.</param>
    public DisposeOrderAttribute(int order) => Order = order;

    /// <summary>
    /// Gets the order in which this member is disposed.
    /// </summary>
    public int Order { get; }
}
