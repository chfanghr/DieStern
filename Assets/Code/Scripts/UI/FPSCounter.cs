using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.UI
{
    [RequireComponent(typeof (Text))]
    public class FPSCounter : MonoBehaviour
    {
        private Text _text;
        private const float FPSMeasurePeriod = 0.5f;
        private int _fpsAccumulator;
        private float _fpsNextPeriod;
        private int _currentFps;
        private const string DisplayTemplate = "{0} FPS";
        
        private void Start()
        {
            _text = GetComponent<Text>();
            _fpsNextPeriod = Time.realtimeSinceStartup + FPSMeasurePeriod;
        }

        private void Update()
        {
            _fpsAccumulator++;
            if (!(Time.realtimeSinceStartup > _fpsNextPeriod)) return;
            _currentFps = (int) (_fpsAccumulator/FPSMeasurePeriod);
            _fpsAccumulator = 0;
            _fpsNextPeriod += FPSMeasurePeriod;
            _text.text = string.Format(DisplayTemplate, _currentFps);
        }
    }
}