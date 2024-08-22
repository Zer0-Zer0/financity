using Unity.Entities; // Importing the Unity Entities namespace for ECS (Entity Component System) functionality.
using Unity.Mathematics; // Importing the Unity Mathematics namespace for mathematical structures and operations.
using Unity.Collections; // Importing the Unity Collections namespace for using Native collections like NativeArray.

/// <summary>
/// Represents a component that holds data related to a dungeon in the game.
/// </summary>
public struct DungeonComponent : IComponentData
{
    /// <summary>
    /// An array of exit locations in the dungeon, represented as 3D vectors (float3).
    /// Each exit location can be used to determine where entities can exit the dungeon.
    /// </summary>
    public NativeArray<float3> exitLocations; 
    

    /// <summary>
    /// An array of exit rotations in the dungeon, represented as Quaternions.
    /// Each exit location can be used to determine where entities can exit the dungeon.
    /// </summary>
    public NativeArray<quaternion> exitRotations; 
    
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