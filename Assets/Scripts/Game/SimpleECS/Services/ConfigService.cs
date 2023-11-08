using UnityEngine;
using System;
using Plugins.Extensions;

namespace Game.ECS
{
    [CreateAssetMenu(fileName = nameof(ConfigService), menuName = "ScriptableObjects/" + nameof(ConfigService), order = 1)]
    [Serializable]
    public class ConfigService : ScriptableObject
    {
        [Header("Spawn")]
        [SerializeField]
        private KeyCode keySpawn = KeyCode.F;
        public KeyCode KeySpawn => keySpawn;

        [SerializeField]
        private Vector2Int rangeSpawn = new Vector2Int(0, 5);
        public Vector2Int RangeSpawn => rangeSpawn;

        [SerializeField]
        private ConfigPrefab configSpawn = null;
        public ConfigPrefab ConfigSpawn => configSpawn;

        [Header("Others")]
        [SerializeField, BoolPopup("Infinity", "Custom")]
        private bool isUseLifeTime = true;
        public bool IsUseLifeTime => isUseLifeTime;
    }
}
