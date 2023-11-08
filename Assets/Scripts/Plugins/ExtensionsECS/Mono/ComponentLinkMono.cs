using Leopotam.EcsLite;
using UnityEngine;

namespace Plugins.UnityECS
{
    public abstract class ComponentLinkMono<T, C> : ComponentMono
        where T : struct, IComponentLink<C>
        where C : Component
    {
        [SerializeField]
        private C component = null;

        private void Awake()
        {
            if (component == null)
                component = GetComponent<C>();

            if (component == null)
                Destroy(this);
        }

        public sealed override void Attach(EcsWorld world, EcsPackedEntity entity)
        {
            if (entity.Unpack(world, out int ent))
            {
                T comp = default;
                comp.value = component;
                world.GetPool<T>().Add(ent) = comp;
                OnAttach(world, ent);
            }
        }

        protected virtual void OnAttach(EcsWorld world, int entity) { }

        public sealed override void Detach(EcsWorld world, EcsPackedEntity entity)
        {
            if (entity.Unpack(world, out int ent))
            {
                world.GetPool<T>().Del(ent);
                OnDetach(world, ent);
            }
        }

        protected virtual void OnDetach(EcsWorld world, int entity) { }
    }

    public abstract class ComponentLinkMono<T> : ComponentMono
        where T : struct, IComponentLink<T>
    {
        [SerializeField]
        private T component = default;

        public override void Attach(EcsWorld world, EcsPackedEntity entity)
        {
            if (entity.Unpack(world, out int ent))
                world.GetPool<T>().Add(ent) = component;
        }

        public sealed override void Detach(EcsWorld world, EcsPackedEntity entity)
        {
            if (entity.Unpack(world, out int ent))
                world.GetPool<T>().Del(ent);
        }
    }

    public abstract class ComponentMono : MonoBehaviour, IComponentMono
    {
        public abstract void Attach(EcsWorld world, EcsPackedEntity entity);
        public abstract void Detach(EcsWorld world, EcsPackedEntity entity);
    }

    public interface IComponentLink<T>
    {
        T value { get; set; }
    }

    public interface IComponentMono
    {
        void Attach(EcsWorld world, EcsPackedEntity entity);
        void Detach(EcsWorld world, EcsPackedEntity entity);
    }
}

