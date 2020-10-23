using System;
using UnityEngine;

namespace Code.Scripts
{
    public class CameraController : MonoBehaviour
    {
        public GameObject player;

        private void Start()
        {
            if (player == null)
            {
                player = GameObject.Find("Player");
            }

            if (player == null)
            {
                enabled = false;
                return;
            }

            _offset = transform.position - player.transform.position;
        }

        private Vector3 _offset;
        
        private void Update()
        {
            transform.position = player.transform.position + _offset;
        }
    }
}