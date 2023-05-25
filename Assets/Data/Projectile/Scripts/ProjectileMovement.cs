using System;
using System.Collections;
using UnityEngine;

namespace Data.Projectile.Scripts
{
    
    public static class ProjectileMovement
    {
        public static IEnumerator ParabolaMovement(Transform obj , Vector3 startPointPosition , Vector3 direction, float v0, float angle, float time , Action<Vector3> changeDirectionCallback = null , Action finishedCallback = null
        )
        {
            bool hasChangeDirectionCallback = changeDirectionCallback != null;
            bool hasFinishedCallback = finishedCallback != null;
            Vector3 previousPosition = startPointPosition;

            float t = 0;
            while (t < time)
            {
                float x = v0 * t * Mathf.Cos(angle);
                float y = v0 * t * Mathf.Sin(angle) - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(t, 2);
                obj.position = startPointPosition + direction * x + Vector3.up * y;
                if (hasChangeDirectionCallback)
                {
                    changeDirectionCallback.Invoke((obj.position - previousPosition).normalized); 
                }
                previousPosition = obj.position;
                t += Time.deltaTime; ;
                yield return null ;
            }

            if (hasFinishedCallback)
            {
                finishedCallback.Invoke();
            }
        }
    }
}
