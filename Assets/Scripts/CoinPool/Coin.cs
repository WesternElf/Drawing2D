using System;
using UnityEngine;
using CharacterControl;
using GameControl;

namespace CoinPool
{
    public class Coin : MonoBehaviour
    {
        public static event Action OnPickedUp;
        private const string _clipName = "Coin";

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out KnightController knight))
            {
                CoinPicked();
                GameController.Instance.AudioManager.PlaySound(_clipName);
            }
        }

        private void CoinPicked()
        {
            OnPickedUp?.Invoke();
            gameObject.GetComponent<PoolableObject>().ReturnToPool(this.gameObject);
        }
    }
}

