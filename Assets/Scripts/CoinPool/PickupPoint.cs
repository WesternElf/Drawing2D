using UnityEngine;
using CharacterControl;

namespace CoinPool
{
    public class PickupPoint : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out KnightController knight))
            {
                gameObject.SetActive(true);
            }
        }
    }
}

