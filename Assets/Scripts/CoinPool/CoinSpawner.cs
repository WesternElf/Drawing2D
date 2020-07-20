using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoinPool
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private int objectsCount;
        [SerializeField] private List<GameObject> _points;
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private float delay;

        private CoinPooler _pool;
        private Transform _randomPoint;

        private void Start()
        {
            Coin.OnPickedUp += SetActivePoint;
            _pool = new CoinPooler(_coinPrefab, objectsCount);

            StartCoroutine(Spawner());
        }

        private IEnumerator Spawner()
        {
            while (IsAllPointsInactive() == true)
            {
                SpawnObject(_pool);
                yield return new WaitForSeconds(delay);
            }
        }

        private void SpawnObject(CoinPooler pool)
        {
            var pooledObject = pool.GetObject();
            var randomPoint = GetRandomPoint();
            pooledObject.transform.position = randomPoint.position;
            pooledObject.transform.rotation = randomPoint.rotation;
            pooledObject.SetActive(true);
        }


        private Transform GetRandomPoint()
        {
            var newPoint = GetPoint();
            var newTransformPoint = newPoint.GetComponent<Transform>();

            //var newPoint = _pointsTransform[Random.Range(0, _points.Count)];

            if (newPoint.activeInHierarchy)
            {
                if (_randomPoint != newTransformPoint)
                {
                    _randomPoint = newTransformPoint;
                    newPoint.SetActive(false);
                }
            }

            else
                return GetRandomPoint();

            return newTransformPoint;
        }


        private bool IsAllPointsInactive()
        {
            print("check");
            foreach (var point in _points)
            {
                if (point.activeInHierarchy == true)
                {
                    return true;
                }
            }
            return false;
        }

        private void SetActivePoint()
        {
            GetPoint().SetActive(true);
        }

        private GameObject GetPoint()
        {
            return _points[Random.Range(0, _points.Count)];
        }

        private void OnDestroy()
        {
            Coin.OnPickedUp -= SetActivePoint;
        }
    }
}


