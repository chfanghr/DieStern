using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.Scripts.Experiments
{
    public class ExperimentsEntry : MonoBehaviour
    {
        public Dropdown scenesMenu;
        public List<string> scenes;

        private void Start()
        {
            scenesMenu.AddOptions(scenes);
            scenesMenu.value = 0;
        }

        private string _activatedSceneName = "";
        
        public void EnterScene()
        {
            ReturnMainScene();
            _activatedSceneName = scenesMenu.captionText.text;
            SceneManager.LoadScene(_activatedSceneName, LoadSceneMode.Additive);
        }

        public void ReturnMainScene()
        {
            if(_activatedSceneName.Length>0){
                SceneManager.UnloadSceneAsync(_activatedSceneName);
            }
            _activatedSceneName = "";
        }
    }
}