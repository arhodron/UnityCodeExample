using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Plugins.ScriptableECS
{
    public class SourceComponenstSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<ApplySourceComponent>> componentFilter = default;
        readonly EcsWorldInject world = default;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entity in componentFilter.Value)
            {
                ref ApplySourceComponent apply = ref componentFilter.Pools.Inc1.Get(entity);
                apply.component.Attach(world.Value, entity);
                componentFilter.Pools.Inc1.Del(entity);
            }
        }
    }
}

