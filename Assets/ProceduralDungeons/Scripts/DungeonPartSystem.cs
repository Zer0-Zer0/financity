using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;

[BurstCompile]
public partial struct DungeonPartSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<DungeonSpawnConfig>();
    }

    public void OnUpdate(ref SystemState state)
    {
        EntityManager entityManager = state.EntityManager;
        foreach (var dungeonComponentAspect
         in SystemAPI.Query<DungeonComponentAspect>())
        {
            DungeonSpawnConfig dungeonSpawnConfig = SystemAPI.GetSingleton<DungeonSpawnConfig>();

            for (int i = 0; i < dungeonComponentAspect.DungeonComponent.ValueRO.exitLocations.Length; i++)
            {
                Entity spawnedEntity = entityManager.Instantiate(dungeonSpawnConfig.GetRandomBlock());
                SystemAPI.SetComponent(spawnedEntity, new LocalTransform{
                    Position = dungeonComponentAspect.DungeonComponent.ValueRO.exitLocations[i],
                    Rotation = dungeonComponentAspect.DungeonComponent.ValueRO.exitRotations[i],
                    Scale = 1f
                });
            }
        }
        state.Enabled = false;
    }

    public void PositionSelf(){

    }
}