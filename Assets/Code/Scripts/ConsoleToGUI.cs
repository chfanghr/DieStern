using UnityEngine;

namespace Code.Scripts
{
    public class ConsoleToGUI : MonoBehaviour
    {
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
            // if (type == LogType.Log)
            // {
            //     return;
            // }
            _output = $"[{type}] {logString}\n";
            _stack = stackTrace;
        }

        public int fontSize = 20;
        private void OnGUI()
        {
            var style = new GUIStyle {fontSize = fontSize};
            GUI.Label(
                new Rect(10, 10, Screen.width/2 - 10, Screen.height/2 - 10), _output, style);
        }

        public void IncreaseFontSize(int by = 1)
        {
            fontSize += by;
        }
    }
}