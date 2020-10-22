using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.Scripts.Experiments
{
    public class ExperimentsEntry : MonoBehaviour
    {
        public Canvas mainUICanvas;
        public Dropdown scenesMenu;
        public List<string> scenes;
        public ConsoleToGUI debugLog;
        
        private void Start()
        {
            Debug.Log($"available scenes: {scenes}");
            scenesMenu.AddOptions(scenes);
            scenesMenu.value = 0;
        }

        private string _activatedSceneName = "";
        
        public void EnterScene()
        {
            ReturnMainScene();
            _activatedSceneName = scenesMenu.captionText.text;
            SceneManager.LoadScene(_activatedSceneName, LoadSceneMode.Additive);
            Debug.Log($"{_activatedSceneName} loaded");
        }

        public void ReturnMainScene()
        {
            if(_activatedSceneName.Length>0){
                Debug.Log($"{_activatedSceneName} unloaded");
                SceneManager.UnloadSceneAsync(_activatedSceneName);
            }
            _activatedSceneName = "";
        }

        public void ToggleUI()
        {
            mainUICanvas.enabled = !mainUICanvas.enabled;
            debugLog.enabled = !debugLog.enabled;
        }
    }
}