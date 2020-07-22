using GameControl;
using System;
using UnityEngine;

namespace CharacterControl
{
    public class InteractionMovement : MonoBehaviour
    {
        [SerializeField] private float _rotateSpeed;
        private Transform _objTransform;

        private void Start()
        {
            _objTransform = gameObject.GetComponent<Transform>();
            UpdateManager.Instance.OnUpdateEvent += SawRotate;
        }

        private void SawRotate()
        {
            _objTransform.Rotate(new Vector3(0f, 0f, -1f) * _rotateSpeed);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out KnightController knight))
            {
                PlayerTriggered();
            }
        }

        protected virtual void PlayerTriggered()
        {
            return;
        }

        private void OnDestroy()
        {
            UpdateManager.Instance.OnUpdateEvent -= SawRotate;
        }
    }
}

