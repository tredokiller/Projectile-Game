using System.Collections.Generic;
using UnityEngine;

namespace Data.Common
{
    public class ObjectPool : MonoBehaviour
    {
        public GameObject prefab;
        public int poolSize = 10;

        private List<GameObject> _pool; 

        void Start()
        {
            _pool = new List<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(prefab , transform);
                obj.SetActive(false);
                _pool.Add(obj);
            }
        }

        public GameObject GetObjectFromPool()
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (!_pool[i].activeInHierarchy)
                {
                    _pool[i].SetActive(true);
                    return _pool[i];
                }
            }
            
            GameObject newObj = Instantiate(prefab);
            _pool.Add(newObj);
            return newObj;
        }

        public void ReturnObjectToPool(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}