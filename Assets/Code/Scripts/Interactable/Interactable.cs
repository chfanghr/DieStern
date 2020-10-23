using System;
using Code.Scripts.InventorySystem;
using UnityEngine;

namespace Code.Scripts.Interactable
{
    public class Interactable : MonoBehaviour
    {
        private IInspector Inspector => GetComponent<IInspector>();
        private SpriteRenderer SpriteRenderer => GetComponent<SpriteRenderer>();

        private static GameObject Player => GameObject.Find("Player");

        public float thresholdDistance = 10.0f;

        private bool ClosedToPlayer =>
            Vector3.Distance(transform.position, Player.transform.position) <= thresholdDistance;

        private Material _defaultMaterial;
        private Material _highlightMaterial;
        
        private void Start()
        {
            if (Inspector == null)
            {
                enabled = false;
                return;
            }
            _highlightMaterial = 
                new Material(Shader.Find("Unlit/InnerSpriteOutline"));
            _defaultMaterial = SpriteRenderer.material;
        }

        private bool _highlighted = false;

        private Inventory Inventory => Singleton<Inventory>.Instance;
        private Item Item => GetComponent<Item>();
        
        private void Update()
        {
            if (Inspector.Collected)
            {
                if (Item && Inventory)
                {
                    Inventory.AddItem(Item.title);
                }
                Destroy(gameObject);
                return;
            }
            if (ClosedToPlayer)
            {
                if(_highlighted) return;
                HighLight();
            }
            else
            {
                Unhighlight();
            }
        }

        private void HighLight()
        {
            _highlighted = true;
            SpriteRenderer.material = _highlightMaterial;
        }
        
        private void Unhighlight()
        {
            _highlighted = false;
            SpriteRenderer.material = _defaultMaterial;
        }

        private void OnMouseDown() => Inspector.Inspect();

        private void OnMouseEnter()
        {
            HighLight();
        }

        private void OnMouseExit()
        {
            Unhighlight();
        }
    }
}