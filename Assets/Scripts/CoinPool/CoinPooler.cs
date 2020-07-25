using System.Collections.Generic;
using UnityEngine;

namespace CoinPool
{
    public class CoinPooler
    {
        private GameObject _prefab;
        private Transform _objectsParent;
        private List<GameObject> _cachedObjects;

        public CoinPooler(GameObject prefab, int initialAmount)
        {
            _prefab = prefab;


            if (_prefab.GetComponent<PoolableObject>() == null)
            {
                _prefab.AddComponent<PoolableObject>();
            }

            _cachedObjects = new List<GameObject>(initialAmount);
            _objectsParent = new GameObject($"[{prefab.name}Parent]").transform;
            SpawnInitialObjects(initialAmount);
            //Object.DontDestroyOnLoad(_objectsParent.gameObject);
        }

        private void SpawnInitialObjects(int initialAmount)
        {
            for (int i = 0; i < initialAmount; i++)
            {
                CreateObject();
            }
        }

        public void ReturnGameObject(GameObject gameObject)
        {
            gameObject.transform.SetParent(_objectsParent);
            gameObject.SetActive(false);
        }

        public GameObject GetObject()
        {
            GameObject objectFromPool = null;

            for (int i = 0; i < _cachedObjects.Count; i++)
            {
                if (!_cachedObjects[i].activeInHierarchy)
                {
                    objectFromPool = _cachedObjects[i];
                    break;
                }
            }

            if (objectFromPool == null)
            {
                objectFromPool = CreateObject();
            }

            return objectFromPool;
        }

        private GameObject CreateObject()
        {
            var newObject = Object.Instantiate(_prefab, _objectsParent);
            newObject.name = _prefab.name + _cachedObjects.Count;
            var poolableObject = newObject.GetComponent<PoolableObject>();
            poolableObject.SetPool(this);
            newObject.SetActive(false);
            _cachedObjects.Add(newObject);
            return newObject;
        }
    }
}
