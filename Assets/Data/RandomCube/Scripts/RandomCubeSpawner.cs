using Data.Common;
using UnityEngine;

namespace Data.RandomCube.Scripts
{
    public class RandomCubeSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool cubePool;
        
        public BulletCube SpawnCube(Vector3 position)
        {
            var cubeObject = cubePool.GetObjectFromPool();
            BulletCube cube = null;
            
            cubeObject.TryGetComponent(out cube);

            if (cube != null)
            {
                cubeObject.SetActive(true);
                cubeObject.transform.position = position;
            }
            
            return cube;
        }
    }
}
