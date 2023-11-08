using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Plugins.UnityECS;
using UnityEngine;
using Plugins.Extensions;

namespace Game.ECS
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        readonly EcsPoolInject<Spawn> spawnPool = default;

        readonly EcsCustomInject<ConfigService> config = default;

        readonly EcsWorldInject world = default;

        public void Run(IEcsSystems systems)
        {
            if(Input.GetKeyDown(config.Value.KeySpawn))
            {
                int entity = world.Value.NewEntity();

                ref Spawn spawn = ref spawnPool.Value.Add(entity);
                spawn.config = config.Value.ConfigSpawn;
                spawn.position = Quaternion.Euler(0, Random.value * 360, 0) * Vector3.forward * config.Value.RangeSpawn.GetRandom();
            }
        }
    }
}