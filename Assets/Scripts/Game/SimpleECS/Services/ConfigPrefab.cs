using UnityEngine;
using System;
using Leopotam.EcsLite;
using System.Collections.Generic;
using Plugins.ScriptableECS;

namespace Game.ECS
{
    [CreateAssetMenu(fileName = nameof(ConfigPrefab), menuName = "ScriptableObjects/" + nameof(ConfigPrefab), order = 1)]
    [Serializable]
    public class ConfigPrefab : ScriptableObject
    {
        [SerializeField]
        private SimpleEntityMono prefab = null;
        public SimpleEntityMono Prefab => prefab;

        [SerializeReference]
        private List<IComponent> components = new List<IComponent>();

        public void InitObj(SimpleEntityMono obj)
        {
            if (obj.World != null && obj.Entity.Unpack(obj.World, out int entity))
                InitObj(obj.World, entity);
        }

        public void InitObj(EcsWorld world, int entity)
        {
            foreach (IComponent com in components)
                com.Attach(world, entity);
        }
    }
}
