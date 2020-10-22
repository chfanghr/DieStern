using UnityEngine;

namespace Code.Scripts
{
    public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        // ReSharper disable once StaticMemberInGenericType
        private static bool _shuttingDown = false;
        // ReSharper disable once StaticMemberInGenericType
        private static readonly object Lock = new object();
    
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_shuttingDown)
                {
                    Debug.Log($"[Singleton] Instance \'{typeof(T)}\' already destroyed.");
                    return null;
                }

                lock (Lock)
                {
                    if (_instance != null) return _instance;
                    _instance = FindObjectOfType<T>();
                
                    if (_instance != null) return _instance;
                
                    var singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>();
                    singletonObject.name = $"{typeof(T)} (Singleton)";
                        
                    DontDestroyOnLoad(singletonObject);
                }

                return _instance;
            }
        }

        private void OnApplicationQuit()
        {
            _shuttingDown = true;
        }

        private void OnDestroy()
        {
            _shuttingDown = true;
        }
    }
}