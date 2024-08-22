using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Burst;

[BurstCompile]
public class DungeonSpawnConfigAuthoring : MonoBehaviour
{
    public GameObject[] SpawnableDungeonBlocks;

    private class Baker : Baker<DungeonSpawnConfigAuthoring>
    {
        public override void Bake(DungeonSpawnConfigAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new DungeonSpawnConfig
            {
                SpawnableDungeonBlocksEntities = ConvertList(authoring)
            });
        }

        private NativeArray<Entity> ConvertList(DungeonSpawnConfigAuthoring authoring)
        {
            int length = authoring.SpawnableDungeonBlocks.Length;
            NativeArray<Entity> arrayOfEntities = new NativeArray<Entity>(length, Allocator.Persistent);

            for (int i = 0; i < length; i++)
                arrayOfEntities[i] = GetEntity(authoring.SpawnableDungeonBlocks[i], TransformUsageFlags.Renderable);

            return arrayOfEntities;
        }
    }
}

public struct DungeonSpawnConfig : IComponentData
{
    public NativeArray<Entity> SpawnableDungeonBlocksEntities;
    /// <summary>
    /// Retrieves a random block entity from the spawnable dungeon blocks.
    /// </summary>
    /// <returns>The entity of a randomly selected dungeon block.</returns>
    public Entity GetRandomBlock()
    {
        int randomIndex = GetRandomIndexFromArray(SpawnableDungeonBlocksEntities);
        return SpawnableDungeonBlocksEntities[randomIndex];
    }

    /// <summary>
    /// Generates a random index within the bounds of the provided NativeArray.
    /// </summary>
    /// <param name="array">The NativeArray from which to generate a random index.</param>
    /// <returns>A random index between 0 and the length of the array.</returns>
    private int GetRandomIndexFromArray(NativeArray<Entity> array) =>
        new Unity.Mathematics.Random((uint)System.DateTime.Now.Ticks).NextInt(0, array.Length);
}