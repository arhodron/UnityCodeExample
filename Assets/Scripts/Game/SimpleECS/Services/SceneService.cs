using UnityEngine;

namespace Game.ECS
{
    public class SceneService : MonoBehaviour
    {
        [SerializeField]
        private Transform root = null;
        public Transform Root => root;
    }
}
