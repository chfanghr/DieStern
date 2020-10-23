using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.UI.SettingMenu
{
    public class VolumeSliderHelper : MonoBehaviour
    {
        private Slider Slider => GetComponent<Slider>();

        private void Update()
        {
            Slider.value = SettingsManager.Instance.GetVolume();
        }

        private void Start()
        {
            if (Common.IsMobile)
            {
                gameObject.SetActive(false);
            }
        }
    }
}