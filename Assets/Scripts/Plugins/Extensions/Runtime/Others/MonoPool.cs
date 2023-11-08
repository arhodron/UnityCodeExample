using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Plugins.Extensions
{
    public abstract class MonoPool<T> : MonoBehaviour
        where T : MonoBehaviour, IMonoPool
    {
        public static MonoPool<T> Instance { private set; get; }

        private Dictionary<int, List<T>> pools = new Dictionary<int, List<T>>();

        private void Awake()
        {
            Instance = this;
        }

        public T ExtractFromPool(T prefab)
        {
            if (prefab == null)
                return null;

            int uid = prefab.GetInstanceID();
            if (pools.ContainsKey(uid) == false)
                pools.Add(uid, new List<T>());

            List<T> list = pools[uid];
            if (list.Count > 0)
            {
                T obj = list[0];
                obj.gameObject.SetActive(true);
                obj.transform.parent = null;
                list.RemoveAt(0);
                return obj;
            }
            else
            {
                T obj = InstantiatePrefab(prefab);
                obj.UID = uid;
                return obj;
            }
        }

        protected virtual T InstantiatePrefab(T prefab)
        {
            return Instantiate(prefab.gameObject).GetComponent<T>();
        }

        public void AddToPool(T obj)
        {
            int uid = obj.UID;
            if (pools.ContainsKey(uid) == false)
                pools.Add(uid, new List<T>());

            List<T> list = pools[uid];
            obj.transform.parent = transform;
            obj.gameObject.SetActive(false);
            list.Add(obj);
        }

        public void ClearPool(T prefab)
        {
            ClearPool(prefab.UID);
        }

        private void ClearPool(int uid)
        {
            if (pools.ContainsKey(uid) == false)
                return;

            List<T> list = pools[uid];
            for (int i = 0; i < list.Count; i++)
                Destroy(list[i].gameObject);
            list.Clear();
        }

        public void ClearPools()
        {
            foreach (var pool in pools)
                ClearPool(pool.Key);
        }
    }

    public interface IMonoPool
    {
        int UID { get; set; }
        //void OnExtractFromPool();
        //void OnReturnToPool();
    }
}

