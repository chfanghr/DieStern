using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.UI.PauseMenu
{
    public class VolumeSliderHelper : MonoBehaviour
    {
        private Slider Slider => GetComponent<Slider>();

        private void Update()
        {
            Slider.value = SettingsManager.Instance.GetVolume();
        }
    }
}