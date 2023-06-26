namespace Erdmier.ProjectTemplate.Domain.Common;

/// <summary> A base class for all entities in the domain. </summary>
/// <typeparam name="TKey"> The type to use for the entity's ID. </typeparam>
public abstract class EntityBase<TKey> where TKey : IEquatable<TKey>
{
    /// <summary> Gets or sets the unique identifier for this entity. </summary>
    public TKey Id { get; set; } = default!;
}
