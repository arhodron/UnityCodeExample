using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.ECS
{
    public class LifeTimeSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<LifeTime>> lifeTimeFilter = default;

        readonly EcsCustomInject<ConfigService> config = default;

        readonly EcsPoolInject<Destroy> destroyPool = default;

        public void Run(IEcsSystems systems)
        {
            if (config.Value.IsUseLifeTime == false)
                return;

            foreach (int entity in lifeTimeFilter.Value)
            {
                ref LifeTime lifeTime = ref lifeTimeFilter.Pools.Inc1.Get(entity);
                lifeTime.lifeTime -= Time.deltaTime;

                if (lifeTime.lifeTime <= 0)
                {
                    lifeTimeFilter.Pools.Inc1.Del(entity);
                    destroyPool.Value.Add(entity);
                }
            }
        }
    }
}
