using Unity.Entities; // Importing the Unity Entities namespace for ECS (Entity Component System) functionality.
using Unity.Mathematics; // Importing the Unity Mathematics namespace for mathematical structures and operations.
using Unity.Collections; // Importing the Unity Collections namespace for using Native collections like NativeArray.

/// <summary>
/// Represents a component that holds data related to a dungeon in the game.
/// </summary>
public struct DungeonComponent : IComponentData
{
    /// <summary>
    /// An array of exit locations in the dungeon, represented as 2D vectors (float3x2).
    /// Each exit location can be used to determine where entities can exit the dungeon.
    /// </summary>
    public NativeArray<float3x2> exitLocations; 
    
    /// <summary>
    /// An array of all the spawnable blocks in a dungeon.
    /// This entity will contain all blocks a given dungeon part is allowed to spawn.
    /// </summary>
    public NativeArray<Entity> spawnableDungeonBlocks; 
    
    /// <summary>
    /// The size of each block in the dungeon, represented as a float3.
    /// This can be used for positioning and scaling the blocks within the dungeon.
    /// </summary>
    public float3 blockSize;
    
    /// <summary>
    /// A boolean flag indicating whether this dungeon component is the root of the dungeon hierarchy.
    /// If true, this component may represent the main entry point or the primary dungeon area.
    /// </summary>
    public bool isRoot;
}