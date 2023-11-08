using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Plugins.UnityECS;
using UnityEngine;

namespace Game.ECS
{
    public class SpawnSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<Spawn>> spawnFilter = default;

        readonly EcsCustomInject<SceneService> scene = default;

        readonly EcsWorldInject world = default;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in spawnFilter.Value)
            {
                ref Spawn spawn = ref spawnFilter.Pools.Inc1.Get(entity);

                SimpleEntityMono obj = PoolSimpleEntityMono.Instance.ExtractFromPool(spawn.config.Prefab);

                obj.transform.position = spawn.position;
                obj.transform.parent = scene.Value.Root;

                obj.Attach(world.Value, world.Value.PackEntity(entity));

                spawn.config.InitObj(obj);
            }
        }
    }
}
