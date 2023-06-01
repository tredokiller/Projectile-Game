using Data.Common.Scripts;
using Data.Hole.Scripts;
using Data.Projectile.Scripts;
using Unity.VisualScripting;
using UnityEngine;

namespace Data.RandomCube.Scripts
{
    public class BulletCube : MonoBehaviour
    {
        [SerializeField] private float cubeSize = 1f;
        [SerializeField] private Material cubeMaterial;

        [SerializeField] private GameObject swashParticle;
        
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;

        private float _reboundSpeed;
        private float _reboundAngle;

        private const float ReboundSpeedMultiplier = 1.2f;

        private Coroutine _coroutine;

        [SerializeField] private float lifeTime = 3f;

        private bool _hasRebound;

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.material = cubeMaterial;
        }

        private void OnEnable()
        {
            _hasRebound = false;
        }

        void Start()
        {
            _meshFilter = RandomMeshGenerator.CreateCubeRandomMesh(cubeSize , _meshFilter);
        }

        public void MoveByParabola(Vector3 startPoint, Vector3 direction, float speed, float angle)
        {
            _reboundAngle = angle;
            _reboundSpeed = speed * ReboundSpeedMultiplier;

            _coroutine = StartCoroutine(ProjectileMovement.ParabolaMovement(transform, startPoint, direction, speed,
                angle, lifeTime, CheckCollision , (() => gameObject.SetActive(false))));
        }

        private void CheckCollision(Vector3 direction)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, 0.3f))
            {
                StopCoroutine(_coroutine);
                if (_hasRebound)
                {
                    DestroyCube(hit);
                    return;
                }

                _hasRebound = true;
                MoveByParabola(transform.position, Vector3.Reflect(direction, hit.normal), _reboundSpeed,
                    _reboundAngle * Mathf.Deg2Rad);
            }
        }

        private void DestroyCube(RaycastHit hit)
        {
            _hasRebound = false;
            gameObject.SetActive(false);
            Instantiate(swashParticle, transform.position , new Quaternion());
            if (hit.collider.gameObject.CompareTag(Tags.WallTag))
            {
                HoleRenderer.Instance.RenderHole(hit.point + hit.normal * 0.1f, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}