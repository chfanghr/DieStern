using UnityEngine;

namespace Code.Scripts.InventorySystem
{
    public interface ISlotPanel
    {
        Inventory Inventory { get; set; }
        int ID { get; set; }
        void OnItemChanged();
        void Init();
        GameObject GameObject { get; }
    }
}