using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private int objectsCount;
    [SerializeField] private List<Transform> _points;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private float delay;

    private CoinPooler _pool;
    private Transform _randomPoint;

    private void Start()
    {
        Initialize();
        StartCoroutine(Spawner());
    }

    public void Initialize()
    {
        _pool = new CoinPooler(_coinPrefab, objectsCount);
    }

    public IEnumerator Spawner()
    {
        while (true)
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
        var newPoint = _points[Random.Range(0, _points.Count)];

        if (_points.Count > 1)
        {
            if (_randomPoint != newPoint)
            {
                _randomPoint = newPoint;
            }
            else
            {
                return GetRandomPoint();
            }
        }
        else
        {
            newPoint = _points[0];
        }
        return newPoint;
    }
}

