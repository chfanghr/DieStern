using UnityEngine;

namespace Code.Scripts.Experiments.SwipeToDisplayUIComponent
{
    public class ToggleDisplay : MonoBehaviour
    {
        public GameObject toDisplay;

        public void OnSwipeUp()
        {
            toDisplay.SetActive(false);
        }

        public void OnSwipeDown()
        {
            toDisplay.SetActive(true);
        }
    }
}