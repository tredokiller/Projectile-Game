using UnityEngine;

namespace Data.Hole.Scripts
{
    public class Hole : MonoBehaviour
    {
        public Hole(Vector3 position , Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
        }
    }
}
