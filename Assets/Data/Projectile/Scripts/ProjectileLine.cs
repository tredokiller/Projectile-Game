using UnityEngine;

namespace Data.Projectile.Scripts
{
    [RequireComponent(typeof(LineRenderer))]
    public class ProjectileLine : MonoBehaviour
    {
        [Header("Main")]
        [SerializeField] private Transform startPoint;
        
        private const float LineStep = 0.02f;

        private Transform _previousPosition;
        private LineRenderer _projectileLine;


        private void Awake()
        {
            _projectileLine = GetComponent<LineRenderer>();
        }
        
        
        public void DrawPath(Vector3 direction, float v0, float angle, float time = 10)
        {
            _projectileLine.positionCount = (int)(time / LineStep) + 1;
            Vector3 previousPosition = Vector3.zero;
            int count = 0;

            for (float i = 0; i < time; i+=LineStep)
            {
                float x = v0 * i * Mathf.Cos(angle);
                float y = v0 * i * Mathf.Sin(angle) - 0.5f * -Physics.gravity.y * Mathf.Pow(i, 2);

                var currentPosition = startPoint.position + direction * x + Vector3.up * y;
                RaycastHit hit;

                Vector3 rayDirection;
                if (count == 0)
                {
                    rayDirection = currentPosition - Vector3.zero;
                }
                else
                {
                    rayDirection = currentPosition - previousPosition;
                }
                
                
                if (Physics.SphereCast(currentPosition, 0.05f, rayDirection,  out hit, 0.5f))
                {
                    _projectileLine.positionCount = count;
                    break;
                }

                _projectileLine.SetPosition(count , currentPosition);
                previousPosition = currentPosition;
                count++;
            }
        }
    }
}
