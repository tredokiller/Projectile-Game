using UnityEngine;

namespace Data.Hole.Scripts
{
    public class HoleSpawner : MonoBehaviour
    {
        private static HoleSpawner _instance;
        public static HoleSpawner Instance => _instance;

        [SerializeField] private GameObject holePrefab;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject); 
                return;
            }
            _instance = this; 
        }

        public void SpawnHole(Vector3 position, Quaternion rotation)
        {
            Instantiate(holePrefab, position, rotation);
        }
    }
}