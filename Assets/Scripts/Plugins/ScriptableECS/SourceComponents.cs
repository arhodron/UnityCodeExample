using Leopotam.EcsLite;
using System.Collections.Generic;
using UnityEngine;

namespace Plugins.ScriptableECS
{
    [CreateAssetMenu(menuName = "Scriptables/ECS/SourceComponents", fileName = "SourceComponents")]
    [System.Serializable]
    public class SourceComponents : ScriptableObject, ISourceComponents
    {
        [SerializeReference]
        private List<IComponent> components = new List<IComponent>();

        public void Attach(EcsWorld world, int entity)
        {
            foreach (IComponent com in components)
                com.Attach(world, entity);
        }

        public void Detach(EcsWorld world, int entity)
        {
            foreach (IComponent com in components)
                com.Detach(world, entity);
        }
    }

    public interface ISourceComponents
    {
        void Attach(EcsWorld world, int entity);
        void Detach(EcsWorld world, int entity);
    }
}

