using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.Di;
using Plugins.UnityECS;

namespace Game.ECS
{
    public class GameStartup : MonoBehaviour
    {
        [SerializeField]
        private SceneService scene = null;
        [SerializeField]
        private ConfigService config = null;
        [SerializeField]
        private ConfigPrefab configPrefab = null;

        private EcsSystems systems;
        private EcsWorld world;

        private void Start()
        {
            world = new EcsWorld();

            systems = new EcsSystems(world);
            systems
                .Add(new PlayerInputSystem())
                .Add(new SpawnSystem())
                .Add(new LifeTimeSystem())
                .Add(new DestroySystem())

                .DelHere<Spawn>()

#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif

                .Inject(scene)
                .Inject(config)
                .Inject(configPrefab)

                .Init();

        }

        private void Update()
        {
            systems?.Run();
        }

        private void OnDestroy()
        {
            systems?.Destroy();
            systems = null;

            world?.Destroy();
            world = null;
        }
    }
}
