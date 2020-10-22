using UnityEngine;

namespace Code.Scripts
{
    public class ConsoleToGUI : MonoBehaviour
    {
        static string _myLog = "";
        private string _output;
        private string _stack;

        private void OnEnable()
        {
            Application.logMessageReceived += Log;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= Log;
        }
 
        public void Log(string logString, string stackTrace, LogType type)
        {
            if (type == LogType.Log)
            {
                return;
            }
            _output = logString;
            _stack = stackTrace;
            _myLog = _output + "\n" + _myLog;
            if (_myLog.Length > 5000)
            {
                _myLog = _myLog.Substring(0, 4000);
            }
        }

        private void OnGUI()
        {
            var style = new GUIStyle {fontSize = 16};
            _myLog = GUI.TextArea(
                new Rect(10, 10, Screen.width/2 - 10, Screen.height/2 - 10), _myLog, style);
        }
    }
}