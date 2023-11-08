using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Plugins.UnityECS;
using UnityEngine;

namespace Game.ECS
{
    public class DestroySystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<GameObjectLink,Destroy>> destroyFilter = default;

        readonly EcsWorldInject world = default;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in destroyFilter.Value)
            {
                ref GameObjectLink go = ref destroyFilter.Pools.Inc1.Get(entity);
                SimpleEntityMono obj = go.value.GetComponent<SimpleEntityMono>();
                if (obj != null)
                {
                    obj.Detach();
                    PoolSimpleEntityMono.Instance.AddToPool(obj);
                    world.Value.DelEntity(entity);
                }
                else
                {
                    Object.Destroy(go.value);
                    world.Value.DelEntity(entity);
                }
            }
        }
    }
}
