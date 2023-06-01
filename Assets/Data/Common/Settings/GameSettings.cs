using System.Linq;
using UnityEngine;

namespace Data.Common.Settings
{
    public class GameSettings : MonoBehaviour
    {
        private void Awake()
        {
            SetFrameRate();
            DontDestroyOnLoad(this);
        }
        
        
        private void SetFrameRate()
        {
            QualitySettings.vSyncCount = 0;
            
            Resolution[] refreshRate = Screen.resolutions;
            Application.targetFrameRate = refreshRate.Last().refreshRate;

        }
    }
}
