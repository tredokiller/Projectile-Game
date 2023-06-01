using System;
using Data.Input;
using Data.Projectile.Scripts;
using Data.RandomCube.Scripts;
using UnityEngine;

namespace Data.Gun.Scripts
{
    [RequireComponent(typeof(Animation))]
    public class GunController : MonoBehaviour
    {
        [SerializeField] private float smoothRotationTime = 5f;
        [SerializeField] private ProjectileLine projectileLine;

        [Header("Gun Settings")] 
        [SerializeField] private RandomCubeSpawner cubeSpawner;
        [SerializeField] private Transform shotPosition;
        [SerializeField] private Transform gunMain;
        [SerializeField] private float initialVelocity;
        [SerializeField] private GameObject swashParticle;
        private const float Angle = 10f;

        public const float MinInitialVelocity = 1f;
        public const float MaxInitialVelocity = 20f;

        private Animation _animation;

        private Vector2 _playerInput;
        private GameInput.PlayerActions _playerActions;
        
        public static Action OnShot;


        private void Awake()
        {
            _playerActions = InputManager.Instance.GetPlayerActions();
            _animation = GetComponent<Animation>();
        }

        private void OnEnable()
        {
            _playerActions.Shoot.started += context => TryToShoot();
        }


        private void Update()
        {
            SetInputPlayer();
            projectileLine.DrawPath(shotPosition.up , initialVelocity, Angle * Mathf.Deg2Rad);
            Move();
        }

        private void Move()
        {
            float horizontalRotation = Mathf.Lerp(transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.y + _playerInput.x,
                smoothRotationTime * Time.deltaTime);
            
            float verticalRotation = Mathf.LerpAngle(gunMain.rotation.eulerAngles.x, gunMain.rotation.eulerAngles.x - _playerInput.y,
                smoothRotationTime * Time.deltaTime);
            
            transform.rotation = Quaternion.Euler(0 , horizontalRotation , 0);
            gunMain.localRotation =  Quaternion.Euler(verticalRotation , 0 , 0);
        }

        private void TryToShoot()
        {
            var cube = cubeSpawner.SpawnCube(shotPosition.position);
            if (cube != null)
            {
                cube.MoveByParabola(shotPosition.position, shotPosition.up , initialVelocity, Angle * Mathf.Deg2Rad);
                
                _animation.Stop();
                _animation.Play();
                
                Instantiate(swashParticle, shotPosition);
                OnShot.Invoke();
            }
        }
        

        public void SetInitialVelocity(float velocity)
        {
            initialVelocity = velocity;
        }
            
        
        private void SetInputPlayer()
        {
            _playerInput = _playerActions.Move.ReadValue<Vector2>();
        }

        private void OnDisable()
        {
            _playerActions.Shoot.started -= context => TryToShoot();
        }
    }
}
