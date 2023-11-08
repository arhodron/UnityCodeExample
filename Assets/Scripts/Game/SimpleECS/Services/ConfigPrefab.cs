using UnityEngine;
using System;
using Leopotam.EcsLite;

namespace Game.ECS
{
    [CreateAssetMenu(fileName = nameof(ConfigPrefab), menuName = "ScriptableObjects/" + nameof(ConfigPrefab), order = 1)]
    [Serializable]
    public class ConfigPrefab : ScriptableObject
    {
        [SerializeField]
        private SimpleEntityMono prefab = null;
        public SimpleEntityMono Prefab => prefab;

        ///TODO: remove after add list components
        [SerializeField]
        private float lifeTime = 5;

        ///TODO: add list components

        public void InitObj(SimpleEntityMono obj)
        {
            if (obj.World != null && obj.Entity.Unpack(obj.World, out int entity))
                InitObj(obj.World, entity);
        }

        public void InitObj(EcsWorld world, int entity)
        {
            if (lifeTime > 0)
            {
                ref LifeTime lifeTime = ref world.GetPool<LifeTime>().Add(entity);
                lifeTime.lifeTime = this.lifeTime;
            }

            ///TODO: attach list components
        }
    }
}
