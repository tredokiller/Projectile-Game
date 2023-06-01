using System;
using UnityEngine;

namespace Data.Hole.Scripts
{
    public class HoleRenderer : MonoBehaviour
    {
        private static HoleRenderer _instance;
        public static HoleRenderer Instance => _instance;

        [SerializeField] private GameObject holePrefab;

        public static Action OnHoleSpawned;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject); 
                return;
            }
            _instance = this; 
        }
        
        public void RenderHole(Vector3 position, Quaternion rotation)
        {
            var hole = Instantiate(holePrefab, position, rotation);
            
            OnHoleSpawned.Invoke();
            Destroy(hole);
        }
    }
}