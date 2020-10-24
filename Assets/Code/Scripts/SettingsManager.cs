using UnityEngine;

namespace Code.Scripts
{
    public class SettingsManager : Singleton<SettingsManager>
    {
        private const string VolumeKey = "volume";

        public void ChangeVolume(float value)
        {
            ChangeVolume(value, true);
        }
        
        public void ChangeVolume(float value, bool save)
        {
            if (value < 0 || value > 1)
            {
                value = 0.5f;
            }
            
            AudioListener.volume = value;
            if(save)
            {
                PlayerPrefs.SetFloat(VolumeKey, value);
            }
        }

        public float GetVolume()
        {
            return AudioListener.volume;
        }
        
        private void Start()
        {
            Application.targetFrameRate = 300;
            ChangeVolume(PlayerPrefs.GetFloat(VolumeKey), false);
        }
    }
}