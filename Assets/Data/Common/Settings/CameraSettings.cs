using Cinemachine;
using Data.Gun.Scripts;
using UnityEngine;

namespace Data.Settings
{
    public class CameraSettings : MonoBehaviour
    {
        [SerializeField] private CinemachineImpulseSource impulseSource;

        private void OnEnable()
        {
            GunController.OnShot += ShakeCamera;
        }

        private void ShakeCamera()
        {
            impulseSource.GenerateImpulse();
        }

        private void OnDisable()
        {
            GunController.OnShot -= ShakeCamera;
        }
    }
}
