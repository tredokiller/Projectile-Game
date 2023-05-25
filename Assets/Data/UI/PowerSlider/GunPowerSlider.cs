using Data.Gun.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Data.UI.PowerSlider
{
    [RequireComponent(typeof(Slider))]
    public class GunPowerSlider : MonoBehaviour
    {
        private Slider _slider;
        [SerializeField] private GunController gunController;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.minValue = GunController.MinInitialVelocity;
            _slider.maxValue = GunController.MaxInitialVelocity;
            _slider.value = _slider.maxValue / 2f;
            
            _slider.onValueChanged.AddListener(gunController.SetInitialVelocity);
            
            gunController.SetInitialVelocity(_slider.value);
        }
        
        
        
        
    }
}
