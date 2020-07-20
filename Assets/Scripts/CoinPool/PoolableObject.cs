using System;
using UnityEngine;

namespace CoinPool
{
    public class PoolableObject : MonoBehaviour
    {
        private CoinPooler _pooler;

        public void SetPool(CoinPooler pooler)
        {
            _pooler = pooler;
        }

        public void ReturnToPool(GameObject coin)
        {
            _pooler.ReturnGameObject(coin);
        }
    }
}

