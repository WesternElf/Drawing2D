using System;
using UnityEngine;
using CharacterControl;

namespace CoinPool
{
    public class Coin : MonoBehaviour
    {
        public static event Action OnPickedUp;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out KnightController knight))
            {
                CoinPicked();
            }
        }

        private void CoinPicked()
        {
            OnPickedUp?.Invoke();
            gameObject.GetComponent<PoolableObject>().ReturnToPool(this.gameObject);
        }
    }
}

