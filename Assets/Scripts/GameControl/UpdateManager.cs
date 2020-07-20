using System;
using UnityEngine;

namespace GameControl
{
    public class UpdateManager : MonoBehaviour
    {
        public event Action OnUpdateEvent;

        private static UpdateManager _instance;

        public static UpdateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<UpdateManager>();
                }

                if (_instance == null)
                {
                    _instance = new GameObject("UpdateManager", typeof(UpdateManager)).GetComponent<UpdateManager>();
                }

                return _instance;
            }
        }

        private void Update()
        {
            OnUpdateEvent?.Invoke();
        }
    }
}

