using UnityEngine; // Importing UnityEngine for MonoBehaviour functionality.
using Unity.Entities; // Importing Unity.Entities for ECS functionality.
using Unity.Mathematics; // Importing Unity.Mathematics for mathematical structures.
using Unity.Collections; // Importing Unity.Collections for using Native collections like NativeArray.

/// <summary>
/// Authoring component for creating a DungeonComponent in the Unity Entity Component System (ECS).
/// This class allows you to define dungeon properties in the Unity Inspector.
/// </summary>
public class DungeonComponentAuthoring : MonoBehaviour
{
    // Public properties to hold dungeon data, accessible in the Unity Inspector.
    public NativeArray<float3> exitLocations { private set; get; } // Array of exit locations in the dungeon.
    public NativeArray<quaternion> exitRotations { private set; get; } // Array of exit rotations in the dungeon.
    public float3 blockSize { private set; get; } // Size of each block in the dungeon.
    public bool isRoot { private set; get; } // Flag indicating if this is the root of the dungeon hierarchy.

    /// <summary>
    /// Inner class responsible for converting the authoring component into an ECS component.
    /// </summary>
    private class Baker : Baker<DungeonComponentAuthoring>
    {
        /// <summary>
        /// Method called to bake the authoring component into an ECS component.
        /// </summary>
        /// <param name="authoring">The DungeonComponentAuthoring instance containing the data.</param>
        public override void Bake(DungeonComponentAuthoring authoring)
        {
            // Create an entity with renderable transform usage.
            Entity entity = GetEntity(TransformUsageFlags.Renderable);
            
            // Add the DungeonComponent to the entity, populating it with data from the authoring component.
            AddComponent(entity, new DungeonComponent
            {
                exitLocations = authoring.exitLocations,
                exitRotations = authoring.exitRotations,
                blockSize = authoring.blockSize,
                isRoot = authoring.isRoot
            });
        }
    }
}