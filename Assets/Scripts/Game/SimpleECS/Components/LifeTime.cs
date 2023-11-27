using Leopotam.EcsLite;
using Plugins.ScriptableECS;
using UnityEngine;

namespace Game.ECS
{
    public struct LifeTime : IStackComponent<LifeTime>
    {
        public float value;

        #region IComponent

        LifeTime IComponent<LifeTime>.GetComponent() => this;

        void IComponent.Attach(EcsWorld world, int entity) => this.Attach(world, entity);

        void IComponent.Detach(EcsWorld world, int entity) => this.Detach(world, entity);

        #endregion

        #region IStackComponent

        void IStackComponent<LifeTime>.Attach(ref LifeTime component) => value += component.value;

        void IStackComponent<LifeTime>.Detach(ref LifeTime component) => value -= component.value;

        #endregion
    }
}