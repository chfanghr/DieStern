using System;
using UnityEngine;
using Object = System.Object;

namespace Code.Scripts.UI
{
    public class WindowManager : MonoBehaviour
    {
        public GameObject window; 
        public void CloseWindow()
        {
            window.SetActive(false);
        }

        public void OpenWindow()
        {
            window.SetActive(true);
        }

        public void ToggleWindow()
        {
            window.SetActive(!window.activeSelf);
        }
    }
}