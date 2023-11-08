using Leopotam.EcsLite;
using UnityEngine;

namespace Plugins.UnityECS
{
    public class EntityMono : MonoBehaviour, IEntityMono
    {
        //[SerializeField]
        //private bool isIncludeChildrens = false;

        public EcsPackedEntity Entity { private set; get; } = default;
        public EcsWorld World { private set; get; } = null;

        public bool IsNull => World == null || Entity.Unpack(World, out _);

        private IChildrenEntityMono[] childrens;
        private IComponentMono[] components;

        private void Awake()
        {
            FindCache();
        }

        private void FindCache()
        {
            childrens = GetComponents<IChildrenEntityMono>();
            components = GetComponents<IComponentMono>();
        }

        public bool Attach(EcsWorld world, EcsPackedEntity entity)
        {
            if (World != null)
                return false;

            if (entity.Unpack(world, out int ent) == false)
                return false;

            World = world;
            Entity = entity;

            World.GetPool<GameObjectLink>().Add(ent).value = gameObject;
            World.GetPool<TransformLink>().Add(ent).value = transform;

            foreach (IChildrenEntityMono children in childrens)
                children.Entity = this;

            foreach (IComponentMono component in components)
                component.Attach(World, Entity);

            OnAttach();

            return true;
        }

        protected virtual void OnAttach() { }

        public bool Detach()
        {
            if (World == null)
                return false;

            if (Entity.Unpack(World, out int ent) == false)
                return false;

            World.GetPool<GameObjectLink>().Del(ent);
            World.GetPool<TransformLink>().Del(ent);

            IChildrenEntityMono[] childrens = GetComponents<IChildrenEntityMono>();
            foreach (IChildrenEntityMono children in childrens)
                children.Entity = null;

            IComponentMono[] components = GetComponents<IComponentMono>();
            foreach (IComponentMono component in components)
                component.Detach(World, Entity);

            OnDetach();

            World = null;
            Entity = default;

            return true;
        }

        protected virtual void OnDetach() { }

        private void OnDestroy()
        {
            ///TODO
            //if (World != null)
            //    Detach();

            World = null;
        }
    }

    public interface ILinkEntityMono
    {
        bool Attach(EcsWorld world, EcsPackedEntity entity);
        bool Detach();
    }

    public interface IEntityMono : ILinkEntityMono
    {
        EcsPackedEntity Entity { get; }
        EcsWorld World { get; }
    }

    public abstract class ChildrenEntityMono : MonoBehaviour, IChildrenEntityMono
    {
        IEntityMono IChildrenEntityMono.Entity { get => Entity; set => Entity = value; }
        protected IEntityMono Entity { get; private set; } = null;
    }

    public interface IChildrenEntityMono
    {
        IEntityMono Entity { get; set; }
    }
}

