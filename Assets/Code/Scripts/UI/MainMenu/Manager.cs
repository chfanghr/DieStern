using System;
using UnityEngine;

namespace Code.Scripts.UI.MainMenu
{
    public class Manager : MonoBehaviour
    {
        public void NewGame()
        {
            // TODO
        }

        public void ContinueGame()
        {
            // TODO
        }

        public WindowManager settingWindowManager;   
        
        public void ToggleSettingMenu()
        {
            settingWindowManager.ToggleWindow();
        }

        private void Start()
        {
            // TODO: - detect whether archive exists
        }
    }
}