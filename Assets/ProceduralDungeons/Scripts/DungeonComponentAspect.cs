using Unity.Entities;
using Unity.Burst;

/// <summary>
/// Represents an aspect of a dungeon component in the ECS (Entity Component System).
/// This struct provides access to the local transform and dungeon component data.
/// </summary>
[BurstCompile]
public readonly partial struct DungeonComponentAspect : IAspect
{
    /// <summary>
    /// Gets a read-only reference to the dungeon component associated with the entity.
    /// </summary>
    public readonly RefRO<DungeonComponent> DungeonComponent;
}
