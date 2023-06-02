using System;
using Data.Hole.Scripts;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Data.Wall.Scripts
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Wall : MonoBehaviour
    {
        [SerializeField] private int renderTextureHeight = 2000;
        [SerializeField] private int renderTextureWidth = 2000;

        [SerializeField] private Camera wallCamera;
        
        private RenderTexture _renderTexture;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _renderTexture = RenderTexture.GetTemporary(renderTextureWidth , renderTextureHeight , 2, RenderTextureFormat.RGB565);

            HoleRenderer.OnHoleSpawned += RenderWall;
            
            wallCamera.targetTexture = _renderTexture;
            wallCamera.enabled = false;

            RenderTexture.active = _renderTexture;
        }

        private void Start()
        {
            RenderWall();
            Material newMaterial = new Material(_meshRenderer.material);
            newMaterial.mainTexture = _renderTexture;
            _meshRenderer.material = newMaterial;

        }

        private void RenderWall()
        {
            wallCamera.Render(); 
        }
    }
}
