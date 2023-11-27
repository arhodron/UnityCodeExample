using Leopotam.EcsLite;
using UnityEngine;

namespace Plugins.ScriptableECS
{
    /*[System.Serializable]
    public abstract class SourceComponent<T> : ISourceComponent<T>
        where T : struct
    {
        [SerializeField]
        private T component = default;

        T ISourceComponent<T>.GetComponent()
        {
            return component;
        }

        void ISourceComponent.Attach(EcsWorld world, int entity)
        {
            this.Attach(world, entity);//world.GetPool<T>().Add(entity) = value;
        }

        void ISourceComponent.Detach(EcsWorld world, int entity)
        {
            this.Detach(world, entity);
        }
    }*/

    public interface IStackComponent<T> : IComponent<T>
        where T : struct
    {
        void Attach(ref T component);
        void Detach(ref T component);
    }

    public interface IComponent<T> : IComponent
        where T : struct
    {
        T GetComponent();
    }

    public interface IComponent
    {
        void Attach(EcsWorld world, int entity);
        void Detach(EcsWorld world, int entity);
    }

    public static class ComponentExtension
    {
        public static void Attach<T>(this T comp, EcsWorld world, int entity)
            where T : struct, IComponent<T>
        {
            EcsPool<T> pool = world.GetPool<T>();
            if (pool.Has(entity))
                pool.Get(entity) = comp.GetComponent();
            else
                pool.Add(entity) = comp.GetComponent();
        }

        public static void Detach<T>(this T comp, EcsWorld world, int entity)
            where T : struct, IComponent<T>
        {
            EcsPool<T> pool = world.GetPool<T>();
            if (pool.Has(entity))
                pool.Del(entity);
        }

        public static void AttachStack<T>(this T comp, EcsWorld world, int entity)
            where T : struct, IStackComponent<T>
        {
            EcsPool<T> pool = world.GetPool<T>();
            if (pool.Has(entity))
                pool.Get(entity).Attach(ref comp);
            else
                pool.Add(entity) = comp.GetComponent();
        }

        public static void DetachStack<T>(this T comp, EcsWorld world, int entity)
            where T : struct, IStackComponent<T>
        {
            EcsPool<T> pool = world.GetPool<T>();
            if (pool.Has(entity))
                pool.Get(entity).Detach(ref comp);
        }
    }
}

